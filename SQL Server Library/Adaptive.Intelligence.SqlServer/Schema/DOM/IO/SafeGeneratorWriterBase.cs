using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.CodeDom.CodeProvider;
using Adaptive.Intelligence.SqlServer.Schema;

namespace Adaptive.Intelligence.SqlServer.CodeDom.IO
{
    /// <summary>
    /// Provides a base class for writing SQL expressions, clauses, statements, and other elements.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    internal abstract class SafeGeneratorWriterBase : ExceptionTrackingBase
    {
        #region Private Member Declarations
        /// <summary>
        /// The SQL provider instance to provide language-specific code.
        /// </summary>
        private ISqlCodeProvider? _codeProvider;
        /// <summary>
        /// The SQL text writer instance.
        /// </summary>
        private SqlTextWriter? _writer;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeGeneratorWriterBase"/> class.
        /// </summary>
        /// <param name="codeProvider">
        /// An <see cref="ISqlCodeProvider"/> implementation used to provide the SQL language specific code to
        /// the writer.
        /// </param>
        /// <param name="writer">
        /// The <see cref="SqlTextWriter"/> instance being written to by the parent caller, or <b>null</b>.
        /// </param>
        public SafeGeneratorWriterBase(ISqlCodeProvider? codeProvider, SqlTextWriter? writer)
        {
            _codeProvider = codeProvider ?? throw new ArgumentNullException(nameof(codeProvider));
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _writer = null;
            _codeProvider = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets the assignment operator representation.
        /// </summary>
        /// <value>
        /// A string containing the assignment operator.
        /// </value>
        protected string? AssignmentOperator
        {
            get
            {
                if (_codeProvider == null)
                {
                    return null;
                }

                return _codeProvider.AssignmentOperator;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance can write.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can write; otherwise, <c>false</c>.
        /// </value>
        protected bool CanWrite => _writer != null && _codeProvider != null;
        /// <summary>
        /// Gets the close operation/parenthesis delimiter value.
        /// </summary>
        /// <remarks>
        /// This is used for order of operations of conditional comparisons.
        /// </remarks>
        /// <example>
        /// (table.field = value) AND (table.otherField != value2)
        /// </example>
        /// <value>
        /// A string specifying the delimiter to use.
        /// </value>
        protected string? CloseParenthesis
        {
            get
            {
                if (_codeProvider == null)
                {
                    return null;
                }

                return _codeProvider.CloseParenthesis;
            }
        }
        /// <summary>
        /// Gets the opening/starting delimiter string for object names.
        /// </summary>
        /// <value>
        /// A string specifying the delimiter to use.
        /// </value>
        protected string? ObjectNameStartDelimiter
        {
            get
            {
                if (_codeProvider == null)
                {
                    return null;
                }

                return _codeProvider.ObjectNameStartDelimiter;
            }
        }
        /// <summary>
        /// Gets the closing/ending delimiter string for object names.
        /// </summary>
        /// <value>
        /// A string specifying the delimiter to use.
        /// </value>
        protected string? ObjectNameEndDelimiter
        {
            get
            {
                if (_codeProvider == null)
                {
                    return null;
                }

                return _codeProvider.ObjectNameEndDelimiter;
            }
        }
        /// <summary>
        /// Gets the open operation/parenthesis delimiter value.
        /// </summary>
        /// <remarks>
        /// This is used for order of operations of conditional comparisons.
        /// </remarks>
        /// <example>
        /// (table.field = value) AND (table.otherField != value2)
        /// </example>
        /// <value>
        /// A string specifying the delimiter to use.
        /// </value>
        protected string? OpenParenthesis
        {
            get
            {
                if (_codeProvider == null)
                {
                    return null;
                }

                return _codeProvider.OpenParenthesis;
            }
        }
        /// <summary>
        /// Gets the variable or parameter name prefix.
        /// </summary>
        /// <value>
        /// A string specifying the delimiter prefix to use that indicates a variable or parameter name.
        /// </value>
        protected string? ParameterNamePrefix
        {
            get
            {
                if (_codeProvider == null)
                {
                    return null;
                }

                return _codeProvider.ParameterNamePrefix;
            }
        }
        /// <summary>
        /// Gets the string used to delimit the items in a SELECT clause.
        /// </summary>
        /// <value>
        /// A string specifying the delimiter to use.
        /// </value>
        protected string? SelectDelimiter
        {
            get
            {
                if (_codeProvider == null)
                {
                    return null;
                }

                return _codeProvider.SelectDelimiter;
            }
        }
        /// <summary>
        /// Gets the string used to indicate a comment.
        /// </summary>
        /// <value>
        /// A string specifying the delimiter to use.
        /// </value>
        protected string? SqlCommentLineDelimiter
        {
            get
            {
                if (_codeProvider == null)
                {
                    return null;
                }

                return _codeProvider.SqlCommentLineDelimiter;
            }
        }
        #endregion

        #region Protected Methods / Functions
        /// <summary>
        /// Renders the code block body end keyword, for procedures or conditional blocks.
        /// </summary>
        /// <returns>
        /// A string containing the block body end keyword.
        /// </returns>
        protected string? RenderBlockBodyEnd()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderBlockBodyEnd();
        }
        /// <summary>
        /// Renders the code block body start keyword, for procedures or conditional blocks.
        /// </summary>
        /// <returns>
        /// A string containing the block body start keyword.
        /// </returns>
        protected string? RenderBlockBodyStart()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderBlockBodyStart();
        }
        /// <summary>
        /// Renders the SQL text indicating the start of a comment block.
        /// </summary>
        /// <returns>
        /// A string containing the rendered text.
        /// </returns>
        protected string? RenderCommentBlockEnd()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderCommentBlockEnd();
        }
        /// <summary>
        /// Renders the SQL text indicating the text used at the start of each comment text line.
        /// </summary>
        /// <returns>
        /// A string containing the rendered text.
        /// </returns>
        protected string? RenderCommentBlockPrefix()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderCommentBlockPrefix();

        }
        /// <summary>
        /// Renders the SQL text indicating the end of a comment block.
        /// </summary>
        /// <returns>
        /// A string containing the rendered text.
        /// </returns>
        protected string? RenderCommentBlockStart()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderCommentBlockEnd();
        }
        /// <summary>
        /// Renders the CREATE stored procedure open statement line.
        /// </summary>
        /// <example>
        /// CREATE PROCEDURE [dbo].[procedure name]
        /// </example>
        /// <param name="owner">
        /// A <see cref="SqlCodeDatabaseNameOwnerNameExpression"/> instance indicating the database owner object,
        /// or <b>null</b>.
        /// </param>
        /// <param name="name">
        /// A string containing the name of the stored procedure.</param>
        /// <returns>
        /// A string containing the rendered SQL.
        /// </returns>
        protected string? RenderCreateProcedureOpenStatement(SqlCodeDatabaseNameOwnerNameExpression owner, string name)
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderCreateProcedureOpenStatement(owner, name);

        }
        /// <summary>
        /// Renders the name of the data type.
        /// </summary>
        /// <param name="sqlDataType">
        /// A <see cref="SqlDataTypes"/> enumerated value indicating the data type.
        /// </param>
        /// <returns>
        /// A string containing the data type name.
        /// </returns>
        protected string? RenderDataTypeName(SqlDataTypes sqlDataType)
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderDataTypeName(sqlDataType);
        }
        /// <summary>
        /// Renders the DECLARE keyword when defining a variable or memory table type.
        /// </summary>
        /// <returns>
        /// A string containing the rendered keyword.
        /// </returns>
        protected string? RenderDeclare()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderDeclare();
        }
        /// <summary>
        /// Renders the DELETE keyword.
        /// </summary>
        /// <returns>
        /// A string containing the rendered SQL.
        /// </returns>
        protected string? RenderDelete()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderDelete();
        }
        /// <summary>
        /// Renders the start of a FROM clause of a SQL statement.
        /// </summary>
        /// <returns>
        /// A string containing the start of the FROM clause of a SQL statement.
        /// </returns>
        protected string? RenderFrom()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderFrom();
        }
        /// <summary>
        /// Renders the INNER JOIN keywords for a SQL statement.
        /// </summary>
        /// <returns>
        /// A string containing the start of the INNER JOIN clause of a SQL statement.
        /// </returns>
        public string? RenderInnerJoin()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderInnerJoin();
        }
        /// <summary>
        /// Renders the INSERT INTO [table] portion for an insert statement
        /// </summary>
        /// <returns>
        /// A string containing the start of the INSERT INTO clause of a SQL statement.
        /// </returns>
        public string? RenderInsertStart()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderInsertStart();

        }
        /// <summary>
        /// Renders the VALUES portion for an insert statement
        /// </summary>
        /// <returns>
        /// A string containing the start of the VALUES clause of a SQL insert statement.
        /// </returns>
        public string? RenderInsertValues()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderInsertValues();
        }
        /// <summary>
        /// Renders the ON keyword for a JOIN clause.
        /// </summary>
        /// <returns>
        /// A string containing the ON keyword for the sub-clause of a join statement.
        /// </returns>
        public string? RenderJoinOn()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderJoinOn();
        }
        /// <summary>
        /// Renders the LEFT JOIN keywords for a SQL statement.
        /// </summary>
        /// <returns>
        /// A string containing the start of the LEFT JOIN clause of a SQL statement.
        /// </returns>
        public string? RenderLeftJoin()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderLeftJoin();
        }
        /// <summary>
        /// Renders the start of a SELECT statement.
        /// </summary>
        /// <param name="distinct">
        /// <b>true</b> to use SELECT and DISTINCT; otherwise, <b>false</b>.
        /// </param>
        /// <param name="topRecordsCount">
        /// A value indicating the top number of records to select. If <b>distinct</b> is specified, this 
        /// value is ignored.  If the <i>topRecordsCount</i> value is zero or less, the TOP specification
        /// is not rendered.
        /// </param>
        /// <returns>
        /// A string containing the start of the SELECT statement.
        /// </returns>
        protected string? RenderSelect(bool distinct, int topRecordsCount)
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderSelect(distinct, topRecordsCount);
        }
        /// <summary>
        /// Renders the SET keyword.
        /// </summary>
        /// <returns>
        /// A string containing the rendered SQL.
        /// </returns>
        protected string? RenderSet()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderSet();

        }
        /// <summary>
        /// Renders the stored procedure body start keyword.
        /// </summary>
        /// <returns>
        /// A string containing the stored procedure body start keyword.
        /// </returns>
        protected string? RenderSpBodyStart()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderSpBodyStart();
        }
        /// <summary>
        /// Renders the SQL comparison operator.
        /// </summary>
        /// <param name="sqlOperator">
        /// A <see cref="SqlComparisonOperator"/> enumerated value indicating the operator.
        /// </param>
        /// <returns>
        /// A string containing the rendering of the operator.
        /// </returns>
        protected string? RenderSqlComparisonOperator(SqlComparisonOperator sqlOperator)
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderSqlComparisonOperator(sqlOperator);

        }
        /// <summary>
        /// Renders the SQL conditional operator.
        /// </summary>
        /// <param name="sqlOperator">
        /// A <see cref="SqlConditionOperator"/> enumerated value indicating the operator.
        /// </param>
        /// <returns>
        /// A string containing the rendering of the operator.
        /// </returns>
        protected string? RenderSqlConditionOperator(SqlConditionOperator sqlOperator)
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderSqlConditionOperator(sqlOperator);
        }
        /// <summary>
        /// Renders the UPDATE keyword.
        /// </summary>
        /// <returns>
        /// A string containing the rendered SQL.
        /// </returns>
        protected string? RenderUpdate()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderUpdate();
        }
        /// <summary>
        /// Renders the start of a WHERE clause of a SQL statement.
        /// </summary>
        /// <returns>
        /// A string containing the start of the WHERE clause of a SQL statement.
        /// </returns>
        protected string? RenderWhere()
        {
            if (_codeProvider == null)
            {
                return null;
            }

            return _codeProvider.RenderWhere();

        }
        /// <summary>
        /// Increases the indentation level.
        /// </summary>
        protected void SafeIndent()
        {
            if (_writer != null)
            {
                _writer.Indent();
            }
        }
        /// <summary>
        /// Decreases the indentation level.
        /// </summary>
        protected void SafeUnIndent()
        {
            if (_writer != null)
            {
                _writer.UnIndent();
            }
        }

        /// <summary>
        /// Safely writes the content to the output stream.
        /// </summary>
        /// <param name="content">
        /// A string containing the content to be written.
        /// </param>
        protected void SafeWrite(string? content)
        {
            if (_writer != null && content != null)
            {
                _writer.Write(content);
            }
        }
        /// <summary>
        /// Safely writes the content to the output stream.
        /// </summary>
        /// <param name="content">
        /// A string containing the content to be written.
        /// </param>
        protected async Task SafeWriteAsync(string? content)
        {
            if (_writer != null && content != null)
            {
                await _writer.WriteAsync(content).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Safely writes a new line to the output stream.
        /// </summary>
        protected void SafeWriteLine()
        {
            if (_writer != null)
            {
                _writer.WriteLine();
            }
        }
        /// <summary>
        /// Safely writes a new line to the output stream.
        /// </summary>
        /// <param name="lineContent">
        /// A string containing the line content to be written.
        /// </param>
        protected void SafeWriteLine(string? lineContent)
        {
            if (_writer != null && lineContent != null)
            {
                _writer.WriteLine(lineContent);
            }
        }
        /// <summary>
        /// Safely writes a new line to the output stream.
        /// </summary>
        /// <param name="lineContent">
        /// A string containing the line content to be written.
        /// </param>
        protected async Task SafeWriteLineAsync(string? lineContent)
        {
            if (_writer != null && lineContent != null)
            {
                await _writer.WriteLineAsync(lineContent).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Safely writes a new line to the output stream.
        /// </summary>
        protected async Task SafeWriteLineAsync()
        {
            if (_writer != null)
            {
                await _writer.WriteLineAsync().ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Safely writes the number of tabs to the output stream.
        /// </summary>
        protected void SafeWriteTabs()
        {
            if (_writer != null)
            {
                _writer.WriteTabs();
            }
        }
        /// <summary>
        /// Safely writes the number of tabs to the output stream.
        /// </summary>
        protected async Task SafeWriteTabsAsync()
        {
            if (_writer != null)
            {
                await _writer.WriteTabsAsync().ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Safely writes the current indentation tabs to the output stream.
        /// </summary>
        /// <param name="tabCount">
        /// The number of tab characters to be written.
        /// </param>
        protected void SafeWriteTabs(int tabCount)
        {
            if (_writer != null)
            {
                _writer.Write(new string(Constants.TabChar, tabCount));
            }
        }
        /// <summary>
        /// Safely writes the number of tabs to the output stream.
        /// </summary>
        /// <param name="tabCount">
        /// The number of tab characters to be written.
        /// </param>
        protected async Task SafeWriteTabsAsync(int tabCount)
        {
            if (_writer != null)
            {
                await _writer.WriteAsync(new string(Constants.TabChar, tabCount)).ConfigureAwait(false);
            }
        }
        #endregion
    }
}
