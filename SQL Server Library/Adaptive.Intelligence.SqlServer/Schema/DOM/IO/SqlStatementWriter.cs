using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.CodeDom.CodeProvider;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Adaptive.Intelligence.SqlServer.CodeDom.IO
{
    /// <summary>
    /// Provides a mechanism for writing SQL statements.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    internal sealed class SqlStatementWriter : SafeGeneratorWriterBase
    {
        #region Private Member Declarations
        /// <summary>
        /// The expression writer instance used to write SQL clause instances.
        /// </summary>
        private SqlClauseWriter? _clauseWriter;
        /// <summary>
        /// The expression writer instance used to write SQL expression instances.
        /// </summary>
        private SqlExpressionWriter? _expressionWriter;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStatementWriter"/> class.
        /// </summary>
        /// <param name="codeProvider">
        /// An <see cref="ISqlCodeProvider"/> implementation used to provide the SQL language specific code to
        /// the writer.
        /// </param>
        /// <param name="clauseWriter">
        /// The SQL clause writer instance used to write SQL clause instances.
        /// </param>
        /// <param name="expressionGeneratorWriter">
        /// The expression writer instance used to write SQL expression instances.
        /// </param>
        /// <param name="writer">
        /// The <see cref="SqlTextWriter"/> instance being written to by the parent caller.
        /// </param>
        public SqlStatementWriter(ISqlCodeProvider? codeProvider, SqlClauseWriter? clauseWriter,
            SqlExpressionWriter? expressionGeneratorWriter, SqlTextWriter? writer) : base(codeProvider, writer)
        {
            _clauseWriter = clauseWriter ?? throw new ArgumentNullException(nameof(clauseWriter));
            _expressionWriter = expressionGeneratorWriter ?? throw new ArgumentNullException(nameof(expressionGeneratorWriter));
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _clauseWriter?.Dispose();
                _expressionWriter?.Dispose();
            }
            _clauseWriter = null;
            _expressionWriter = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Writes the comment statement.
        /// </summary>
        /// <param name="commentStatement">
        /// The <see cref="SqlCodeCommentStatement"/> instance to be rendered and written.
        /// </param>
        public void WriteCommentStatement(SqlCodeCommentStatement? commentStatement)
        {
            if (_expressionWriter != null && commentStatement != null && commentStatement.Comments.Count > 0)
            {
                if (commentStatement.Comments.Count == 1)
                {
                    SafeWriteTabs();
                    _expressionWriter.WriteCommentExpression(commentStatement.Comments[0]);
                }
                else
                {
                    SafeWriteTabs();
                    _expressionWriter.WriteCommentBlockExpressionStart();
                    SafeWriteLine();
                    foreach (SqlCodeCommentExpression expression in commentStatement.Comments)
                    {
                        SafeWriteTabs();
                        _expressionWriter.WriteCommentBlockExpression(expression);
                        SafeWriteLine();
                    }
                    SafeWriteTabs();
                    _expressionWriter.WriteCommentBlockExpressionEnd();
                    SafeWriteLine();
                }
            }
        }
        /// <summary>
        /// Writes the comment statement.
        /// </summary>
        /// <param name="commentStatement">
        /// The <see cref="SqlCodeCommentStatement"/> instance to be rendered and written.
        /// </param>
        public async Task WriteCommentStatementAsync(SqlCodeCommentStatement? commentStatement)
        {
            if (_expressionWriter != null && commentStatement != null && commentStatement.Comments.Count > 0)
            {
                if (commentStatement.Comments.Count == 1)
                {
                    await SafeWriteTabsAsync().ConfigureAwait(false);
                    await _expressionWriter.WriteCommentExpressionAsync(commentStatement.Comments[0]).ConfigureAwait(false);
                }
                else
                {
                    await SafeWriteTabsAsync().ConfigureAwait(false);
                    await _expressionWriter.WriteCommentBlockExpressionStartAsync().ConfigureAwait(false);
                    await SafeWriteLineAsync().ConfigureAwait(false);
                    foreach (SqlCodeCommentExpression expression in commentStatement.Comments)
                    {
                        await SafeWriteTabsAsync().ConfigureAwait(false);
                        await _expressionWriter.WriteCommentBlockExpressionAsync(expression).ConfigureAwait(false);
                        await SafeWriteLineAsync().ConfigureAwait(false);
                    }
                    await SafeWriteTabsAsync().ConfigureAwait(false);
                    await _expressionWriter.WriteCommentBlockExpressionEndAsync().ConfigureAwait(false);
                    await SafeWriteLineAsync().ConfigureAwait(false);
                }
            }
        }
        /// <summary>
        /// Writes the SQL CREATE PROCEDURE statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeCreateStoredProcedureStatement"/> instance to be rendered and written.
        /// </param>
        public void WriteCreateStoredProcedureStatement(SqlCodeCreateStoredProcedureStatement? statement)
        {
            if (statement != null && _expressionWriter != null)
            {
                //  CREATE PROCEDURE [owner].[name]
                SafeWriteTabs();
                SafeWrite(RenderCreateProcedureOpenStatement(statement.Owner, statement.Name));
                SafeWriteLine();

                // Write the parameter list
                //
                // @ParamName       type definition
                int len = statement.Parameters.Count;
                if (len > 0)
                    SafeIndent();
                for (int count = 0; count < len; count++)
                {
                    SafeWriteTabs();
                    _expressionWriter.WriteParameterDefinitionExpression(statement.Parameters[count]);
                    // Append a comma if not the last item in the list.
                    if (count < len - 1)
                        SafeWrite(", ");
                    SafeWriteLine();
                }
                if (len > 0)
                    SafeUnIndent();

                // AS
                SafeWriteTabs();
                SafeWrite(RenderSpBodyStart());
                SafeWriteLine();

                // BEGIN
                SafeWriteTabs();
                SafeWrite("  ");
                SafeWrite(RenderBlockBodyStart());
                SafeWriteLine();
                SafeIndent();

                // Write the statements that make up the stored procedure.
                foreach (SqlCodeStatement innerStatement in statement.Statements)
                {
                    WriteStatement(innerStatement);
                    SafeWriteLine();
                }
                SafeUnIndent();

                // END
                SafeWriteTabs();
                SafeWrite(Constants.TwoSpaces);
                SafeWrite(RenderBlockBodyEnd());
                SafeWriteLine();
                SafeIndent();
            }
        }
        /// <summary>
        /// Writes the SQL CREATE PROCEDURE statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeCreateStoredProcedureStatement"/> instance to be rendered and written.
        /// </param>
        public async Task WriteCreateStoredProcedureStatementAsync(SqlCodeCreateStoredProcedureStatement? statement)
        {
            if (_expressionWriter != null && statement != null)
            {
                //  CREATE PROCEDURE [owner].[name]
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync(RenderCreateProcedureOpenStatement(statement.Owner, statement.Name)).ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);
                SafeIndent();

                // Write the parameter list
                //
                // @ParamName       type definition
                int len = statement.Parameters.Count;
                for (int count = 0; count < len; count++)
                {
                    await SafeWriteTabsAsync().ConfigureAwait(false);
                    await _expressionWriter.WriteParameterDefinitionExpressionAsync(statement.Parameters[count]).ConfigureAwait(false);
                    // Append a comma if not the last item in the list.
                    if (count < len - 1)
                        await SafeWriteAsync(", ").ConfigureAwait(false);
                    await SafeWriteLineAsync().ConfigureAwait(false);
                }
                SafeUnIndent();

                // AS
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync(RenderSpBodyStart()).ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);

                // BEGIN
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync("  ").ConfigureAwait(false);
                await SafeWriteAsync(RenderBlockBodyStart()).ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);
                SafeIndent();

                // Write the statements that make up the stored procedure.
                foreach (SqlCodeStatement innerStatement in statement.Statements)
                {
                    await WriteStatementAsync(innerStatement);
                    await SafeWriteLineAsync().ConfigureAwait(false);
                }
                SafeUnIndent();

                // END
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync("  ").ConfigureAwait(false);
                await SafeWriteAsync(RenderBlockBodyEnd()).ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);
                SafeIndent();
            }
        }
        /// <summary>
        /// Writes the SQL INSERT statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeSelectStatement"/> instance to be rendered and written.
        /// </param>
        public void WriteInsertStatement(SqlCodeInsertStatement? statement)
        {
            if (statement != null && _expressionWriter != null)
            {
                // INSERT INTO [dbo].[SomeTable]
                SafeWriteTabs();
                SafeWrite(RenderInsertStart());
                SafeWrite(" ");
                _expressionWriter.WriteTableReferenceExpression(statement.Table);
                SafeWriteLine();

                // ([a],[b],[c],[d],...)
                SafeIndent();
                SafeWriteTabs();
                SafeWrite(OpenParenthesis);

                int len = statement.InsertColumnList.Count;
                for (int count = 0; count < len; count++)
                {
                    _expressionWriter.WriteColumnNameExpression(statement.InsertColumnList[count]);
                    if (count < len - 1)
                        SafeWrite(", ");
                }
                SafeWrite(CloseParenthesis);
                SafeWriteLine();

                // VALUES
                SafeUnIndent();
                SafeWriteTabs();
                SafeWriteLine(RenderInsertValues());

                // (value1, value2, value3, ...
                SafeIndent();
                SafeWriteTabs();
                SafeWrite(OpenParenthesis);

                len = statement.ValuesList.Count;
                for (int count = 0; count < len; count++)
                {
                    _expressionWriter.WriteExpression(statement.ValuesList[count]);
                    if (count < len - 1)
                        SafeWrite(", ");
                }
                SafeWrite(CloseParenthesis);
                SafeWriteLine();
                SafeUnIndent();
            }
        }
        /// <summary>
        /// Writes the SQL INSERT statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeSelectStatement"/> instance to be rendered and written.
        /// </param>
        public async Task WriteInsertStatementAsync(SqlCodeInsertStatement? statement)
        {
            if (statement != null && _expressionWriter != null)
            {
                // INSERT INTO [dbo].[SomeTable]
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync(RenderInsertStart()).ConfigureAwait(false);
                await SafeWriteAsync(" ").ConfigureAwait(false);
                await _expressionWriter.WriteTableReferenceExpressionAsync(statement.Table).ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);

                // ([a],[b],[c],[d],...)
                SafeIndent();
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync(OpenParenthesis).ConfigureAwait(false);

                int len = statement.InsertColumnList.Count;
                for (int count = 0; count < len; count++)
                {
                    await _expressionWriter.WriteColumnNameExpressionAsync(statement.InsertColumnList[count]).ConfigureAwait(false);
                    if (count < len - 1)
                        await SafeWriteAsync(Constants.CommaWithSpace).ConfigureAwait(false);
                }
                await SafeWriteAsync(CloseParenthesis).ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);

                // VALUES
                SafeUnIndent();
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteLineAsync(RenderInsertValues()).ConfigureAwait(false);

                // (value1, value2, value3, ...
                SafeIndent();
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync(OpenParenthesis).ConfigureAwait(false);

                len = statement.ValuesList.Count;
                for (int count = 0; count < len; count++)
                {
                    await _expressionWriter.WriteExpressionAsync(statement.ValuesList[count]).ConfigureAwait(false);
                    if (count < len - 1)
                        await SafeWriteAsync(Constants.CommaWithSpace).ConfigureAwait(false);
                }
                await SafeWriteAsync(CloseParenthesis).ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);
                SafeUnIndent();
            }
        }
        /// <summary>
        /// Writes the literal text as a SQL statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeLiteralStatement"/> instance to be rendered and written.
        /// </param>
        public void WriteLiteralStatement(SqlCodeLiteralStatement? statement)
        {
            if (statement != null)
            {
                SafeWriteTabs();
                SafeWriteLine(statement.Text);
            }
        }
        /// <summary>
        /// Writes the literal text as a SQL statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeLiteralStatement"/> instance to be rendered and written.
        /// </param>
        public async Task WriteLiteralStatementAsync(SqlCodeLiteralStatement? statement)
        {
            if (statement != null)
            {
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteLineAsync(statement.Text).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Writes the SQL SELECT statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeSelectStatement"/> instance to be rendered and written.
        /// </param>
        public void WriteSelectStatement(SqlCodeSelectStatement? statement)
        {
            if (statement != null && _clauseWriter != null)
            {
                // Write the SELECT .... portion.
                _clauseWriter.WriteSelectClause(statement.SelectClause);

                // FROM  (with or without JOINS) ...
                _clauseWriter.WriteFromClause(statement.FromClause);

                // WHERE ...
                _clauseWriter.WriteWhereClause(statement.WhereClause);

                //TODO: Future
                //// GROUP BY
                //_clauseWriter.WriteGroupByClause(statement.WhereClause);

                //// ORDER BY
                //_clauseWriter.WriteOrderByClause(statement.WhereClause);
            }
        }
        /// <summary>
        /// Writes the SQL SELECT statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeSelectStatement"/> instance to be rendered and written.
        /// </param>
        public async Task WriteSelectStatementAsync(SqlCodeSelectStatement? statement)
        {
            if (statement != null && _clauseWriter != null)
            {
                // Write the SELECT .... portion.
                await _clauseWriter.WriteSelectClauseAsync(statement.SelectClause).ConfigureAwait(false);

                // FROM  (with or without JOINS) ...
                await _clauseWriter.WriteFromClauseAsync(statement.FromClause).ConfigureAwait(false);

                // WHERE ...
                await _clauseWriter.WriteWhereClauseAsync(statement.WhereClause).ConfigureAwait(false);

                //TODO: Future
                //// GROUP BY
                //_clauseWriter.WriteGroupByClause(statement.WhereClause);

                //// ORDER BY
                //_clauseWriter.WriteOrderByClause(statement.WhereClause);
            }
        }
        /// <summary>
        /// Writes the SQL statement to the output.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeStatement"/> instance to be rendered and written.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <see cref="SqlCodeStatement"/> derived instance is not yet supported.
        /// </exception>
        public void WriteStatement(SqlCodeStatement? statement)
        {
            switch (statement)
            {
                case SqlCodeCommentStatement commentStatement:
                    WriteCommentStatement(commentStatement);
                    break;

                case SqlCodeCreateStoredProcedureStatement createSpStatement:
                    WriteCreateStoredProcedureStatement(createSpStatement);
                    break;

                case SqlCodeInsertStatement insertStatement:
                    WriteInsertStatement(insertStatement);
                    break;

                case SqlCodeLiteralStatement literalStatement:
                    WriteLiteralStatement(literalStatement);
                    break;

                case SqlCodeSelectStatement selectStatement:
                    WriteSelectStatement(selectStatement);
                    break;

                case SqlCodeUpdateStatement updateStatement:
                    WriteUpdateStatement(updateStatement);
                    break;

                case SqlCodeDeleteStatement deleteStatement:
                    WriteDeleteStatement(deleteStatement);
                    break;

                case SqlCodeVariableDeclarationStatement variableDeclareStatement:
                    WriteVariableDeclarationStatement(variableDeclareStatement);
                    break;

                case SqlCodeAssignmentStatement assignStatement:
                default:
                    throw new ArgumentOutOfRangeException(nameof(statement), @"Statement type is not yet supported.");

            }
        }
        /// <summary>
        /// Writes the SQL statement to the output.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeStatement"/> instance to be rendered and written.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <see cref="SqlCodeStatement"/> derived instance is not yet supported.
        /// </exception>
        public async Task WriteStatementAsync(SqlCodeStatement? statement)
        {
            switch (statement)
            {
                case SqlCodeCommentStatement commentStatement:
                    await WriteCommentStatementAsync(commentStatement);
                    break;

                case SqlCodeCreateStoredProcedureStatement createSpStatement:
                    await WriteCreateStoredProcedureStatementAsync(createSpStatement);
                    break;

                case SqlCodeInsertStatement insertStatement:
                    await WriteInsertStatementAsync(insertStatement);
                    break;

                case SqlCodeLiteralStatement literalStatement:
                    await WriteLiteralStatementAsync(literalStatement);
                    break;

                case SqlCodeSelectStatement selectStatement:
                    await WriteSelectStatementAsync(selectStatement);
                    break;

                case SqlCodeUpdateStatement updateStatement:
                    await WriteUpdateStatementAsync(updateStatement);
                    break;

                case SqlCodeDeleteStatement deleteStatement:
                    await WriteDeleteStatementAsync(deleteStatement);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(statement),
                        @"Statement type is not yet supported.");
            }
        }
        /// <summary>
        /// Writes the SQL UPDATE statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeUpdateStatement"/> instance to be rendered and written.
        /// </param>
        public void WriteUpdateStatement(SqlCodeUpdateStatement? statement)
        {
            if (statement != null && _expressionWriter != null)
            {
                // UPDATE [dbo].[TableName]
                //   SET
                SafeWriteTabs();
                SafeWrite(RenderUpdate());
                SafeWrite(" ");
                _expressionWriter.WriteTableReferenceExpression(statement.Table);
                SafeWriteLine();
                SafeWriteTabs();
                SafeWrite("  " + RenderSet());
                SafeWriteLine();
                SafeIndent();

                //     [column] = [value]
                int len = statement.UpdateColumnList.Count;
                for (int count = 0; count < len; count++)
                {
                    SafeWriteTabs();
                    _expressionWriter.WriteAssignmentExpression(statement.UpdateColumnList[count]);
                    if (count < len - 1)
                        SafeWrite(",");
                    SafeWriteLine();
                }
                SafeUnIndent();

                // WHERE
                if (statement.WhereClause != null && _clauseWriter != null)
                    _clauseWriter.WriteWhereClause(statement.WhereClause);
            }
        }
        /// <summary>
        /// Writes the SQL DELETE statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeUpdateStatement"/> instance to be rendered and written.
        /// </param>
        /// <param name="hardDelete">
        /// A value indicating whether to generate a hard DELETE statement.
        /// </param>
        public void WriteDeleteStatement(SqlCodeDeleteStatement? statement, bool hardDelete = false)
        {
            if (hardDelete)
                WriteHardDeleteStatement(statement);
            else
                WriteSoftDeleteStatement(statement);
        }
        /// <summary>
        /// Writes the SQL DELETE statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeUpdateStatement"/> instance to be rendered and written.
        /// </param>
        /// <param name="hardDelete">
        /// A value indicating whether to generate a hard DELETE statement.
        /// </param>
        public async Task WriteDeleteStatementAsync(SqlCodeDeleteStatement? statement, bool hardDelete = false)
        {
            await Task.Yield();
            if (hardDelete)
                WriteHardDeleteStatement(statement);
            else
                WriteSoftDeleteStatement(statement);
        }
        /// <summary>
        /// Writes a hard SQL DELETE statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeUpdateStatement"/> instance to be rendered and written.
        /// </param>
        public void WriteHardDeleteStatement(SqlCodeDeleteStatement? statement)
        {
            if (statement != null && _expressionWriter != null)
            {
                // DELETE FROM [dbo].[TableName]
                SafeWriteTabs();
                SafeWrite(RenderDelete());

                // FROM  (with or without JOINS) ...
                _clauseWriter?.WriteFromClause(statement.FromClause, true);

                // WHERE ...
                if (statement.WhereClause != null && _clauseWriter != null)
                    _clauseWriter.WriteWhereClause(statement.WhereClause);
            }
        }
        /// <summary>
        /// Writes a hard SQL DELETE statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeUpdateStatement"/> instance to be rendered and written.
        /// </param>
        public void WriteSoftDeleteStatement(SqlCodeDeleteStatement? statement)
        {
            if (statement != null && _expressionWriter != null)
            {
                // UPDATE [dbo].[TableName]
                //   SET
                SafeWriteTabs();
                SafeWrite(RenderUpdate());
                SafeWrite(Constants.Space);
                _expressionWriter.WriteTableReferenceExpression(statement.FromClause.SourceTable);
                SafeWriteLine();
                // SET
                SafeWriteTabs();
                SafeWrite(Constants.Space + Constants.Space);
                SafeWrite(RenderSet());
                SafeWriteLine();
                SafeIndent();

                //     [Deleted] = 0
                SafeWriteTabs();
                SqlCodeAssignmentExpression assignExpression = new SqlCodeAssignmentExpression(
                    new SqlCodeColumnNameExpression("Deleted"),
                    new SqlCodeLiteralExpression("0"));
                _expressionWriter.WriteAssignmentExpression(assignExpression);
                SafeUnIndent();
                SafeWriteLine();
                SafeWriteLine();

                // WHERE
                if (statement.WhereClause != null && _clauseWriter != null)
                    _clauseWriter.WriteWhereClause(statement.WhereClause);
            }
        }
        /// <summary>
        /// Writes the SQL UPDATE statement.
        /// </summary>
        /// <param name="statement">
        /// The <see cref="SqlCodeUpdateStatement"/> instance to be rendered and written.
        /// </param>
        public async Task WriteUpdateStatementAsync(SqlCodeUpdateStatement? statement)
        {
            if (statement != null && _expressionWriter != null)
            {
                // UPDATE [dbo].[TableName]
                //   SET
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync(RenderUpdate()).ConfigureAwait(false);
                await SafeWriteAsync(" ").ConfigureAwait(false);
                await _expressionWriter.WriteTableReferenceExpressionAsync(statement.Table).ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync("  " + RenderSet()).ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);
                SafeIndent();

                //     [column] = [value]
                int len = statement.UpdateColumnList.Count;
                for (int count = 0; count < len; count++)
                {
                    await SafeWriteTabsAsync().ConfigureAwait(false);
                    await _expressionWriter.WriteAssignmentExpressionAsync(statement.UpdateColumnList[count]).ConfigureAwait(false);
                    if (count < len - 1)
                        await SafeWriteAsync(",").ConfigureAwait(false);
                    await SafeWriteLineAsync().ConfigureAwait(false);
                }
                SafeUnIndent();

                // WHERE
                if (_clauseWriter != null && statement.WhereClause != null)
                    await _clauseWriter.WriteWhereClauseAsync(statement.WhereClause).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Writes the variable declaration statement.
        /// </summary>
        /// <param name="variableDeclareStatement">
        /// The <see cref="SqlCodeVariableDeclarationStatement"/> instance to be rendered and written.
        /// </param>
        public void WriteVariableDeclarationStatement(SqlCodeVariableDeclarationStatement? variableDeclareStatement)
        {
            if (variableDeclareStatement != null && _expressionWriter != null)
            {
                // DECLARE @<variable_name> <data_type> <dt_specs>
                SafeWriteTabs();
                SafeWrite(RenderDeclare() + Constants.Space);
                _expressionWriter.WriteExpression(variableDeclareStatement.VariableDefinitionExpression);

                // Optional:  = <some_expression>
                //
                // e.g. DECLARE @Id NVARCHAR(128) = NEWID()

                if (variableDeclareStatement.ValueExpression != null)
                {
                    SafeWrite(" ");
                    SafeWrite(AssignmentOperator);
                    SafeWrite(" ");
                    _expressionWriter.WriteExpression(variableDeclareStatement.ValueExpression);
                }
            }
        }
        /// <summary>
        /// Writes the variable declaration statement.
        /// </summary>
        /// <param name="variableDeclareStatement">
        /// The <see cref="SqlCodeVariableDeclarationStatement"/> instance to be rendered and written.
        /// </param>
        public async Task WriteVariableDeclarationstatementAsync(SqlCodeVariableDeclarationStatement? variableDeclareStatement)
        {
            if (variableDeclareStatement != null && _expressionWriter != null)
            {
                // DECLARE @<variable_name> <data_type> <dt_specs>
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync(RenderDeclare() + " ").ConfigureAwait(false);
                await _expressionWriter.WriteExpressionAsync(variableDeclareStatement.VariableDefinitionExpression).ConfigureAwait(false);

                // Optional:  = <some_expression>
                //
                // e.g. DECLARE @Id NVARCHAR(128) = NEWID()

                if (variableDeclareStatement.ValueExpression != null)
                {
                    await SafeWriteAsync(
                        Constants.Space +
                                AssignmentOperator +
                                Constants.Space)
                        .ConfigureAwait(false);
                    await _expressionWriter.WriteExpressionAsync(variableDeclareStatement.ValueExpression).ConfigureAwait(false);
                }
            }
        }
        #endregion
    }
}