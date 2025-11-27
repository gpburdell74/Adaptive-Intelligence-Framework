using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.CodeDom.CodeProvider;

namespace Adaptive.Intelligence.SqlServer.CodeDom.IO
{
    /// <summary>
    /// Provides a mechanism for writing SQL clauses.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    internal sealed class SqlClauseWriter : SafeGeneratorWriterBase
    {
        #region Private Member Declarations
        /// <summary>
        /// The expression writer instance used to write SQL expression instances.
        /// </summary>
        private SqlExpressionWriter? _expressionWriter;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlClauseWriter"/> class.
        /// </summary>
        /// <param name="codeProvider">
        /// An <see cref="ISqlCodeProvider"/> implementation used to provide the SQL language specific code to
        /// the writer.
        /// </param>
        /// <param name="expressionGeneratorWriter">
        /// <summary>
        /// The expression writer instance used to write SQL expression instances.
        /// </summary>
        /// </param>
        /// <param name="writer">
        /// The <see cref="SqlTextWriter"/> instance being written to by the parent caller, or <b>null</b>.
        /// </param>
        public SqlClauseWriter(ISqlCodeProvider? codeProvider, SqlExpressionWriter? expressionGeneratorWriter, SqlTextWriter? writer)
            : base(codeProvider, writer)
        {
            if (writer == null && writer == null)
            {
                throw new InvalidOperationException("A StringBuilder or StreamWriter destination must be provided.");
            }

            _expressionWriter = expressionGeneratorWriter ?? throw new ArgumentNullException(nameof(expressionGeneratorWriter));
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _expressionWriter?.Dispose();
            _expressionWriter = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Writes the FROM clause of a SQL SELECT statement.
        /// </summary>
        /// <param name="fromClause">
        /// The <see cref="SqlCodeFromClause"/> instance to be rendered and written.
        /// </param>
        /// <param name="onSameLine">
        /// A value indicating whether to render the FROM clause content on a single line.
        /// </param>
        public void WriteFromClause(SqlCodeFromClause? fromClause, bool onSameLine = false)
        {
            if (fromClause != null && fromClause.SourceTable != null && _expressionWriter != null)
            {
                // FROM [source table expression]
                if (onSameLine)
                {
                    SafeWrite(Constants.Space);
                }
                else
                {
                    SafeWriteTabs();
                }

                SafeWrite(RenderFrom() + Constants.Space);
                _expressionWriter.WriteTableReferenceExpression(fromClause.SourceTable);

                SafeWriteLine();

                // JOIN ...
                WriteJoinClause(fromClause.Joins);
                SafeUnIndent();
                SafeWriteLine();
            }
        }
        /// <summary>
        /// Writes the FROM clause of a SQL SELECT statement.
        /// </summary>
        /// <param name="fromClause">
        /// The <see cref="SqlCodeFromClause"/> instance to be rendered and written.
        /// </param>
        public async Task WriteFromClauseAsync(SqlCodeFromClause? fromClause)
        {
            if (fromClause != null && fromClause.SourceTable != null && _expressionWriter != null)
            {
                // FROM [source table expression]
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync(RenderFrom() + Constants.Space).ConfigureAwait(false);
                await _expressionWriter.WriteTableReferenceExpressionAsync(fromClause.SourceTable).ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);

                // JOIN ...
                await WriteJoinClauseAsync(fromClause.Joins).ConfigureAwait(false);
                SafeUnIndent();
                await SafeWriteLineAsync().ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Writes the JOIN clause for a SQL statement.
        /// </summary>
        /// <param name="joinClauseList">
        /// A <see cref="SqlCodeJoinClauseCollection"/> containing the definitions of items to join on
        /// </param>
        public void WriteJoinClause(SqlCodeJoinClauseCollection? joinClauseList)
        {

            if (joinClauseList != null && _expressionWriter != null)
            {
                // [INNER | LEFT] JOIN [table expression]
                //     ON [leftTable].[column] [= | !=] [rightTable].[column]
                SafeIndent();
                foreach (SqlCodeJoinClause joinItem in joinClauseList)
                {
                    // LEFT or INNER JOIN
                    SafeWriteTabs();
                    if (joinItem.IsLeftJoin)
                    {
                        SafeWrite(RenderLeftJoin() + " ");
                    }
                    else
                    {
                        SafeWrite(RenderInnerJoin() + " ");
                    }

                    // [dbo].[Table] [Alias]
                    _expressionWriter.WriteTableReferenceExpression(joinItem.ReferencedTable);
                    if (joinItem.NoLock)
                    {
                        SafeWrite(" WITH (NOLOCK) ");
                    }

                    SafeWriteLine();

                    // ON <left expression> <operator> <right expression>
                    SafeIndent();
                    SafeWriteTabs();

                    SafeWrite(RenderJoinOn() + " ");
                    _expressionWriter.WriteTableColumnReferenceExpression(joinItem.LeftColumn);
                    SafeWrite(" ");
                    SafeWrite(RenderSqlComparisonOperator(joinItem.Operator));
                    SafeWrite(" ");
                    _expressionWriter.WriteTableColumnReferenceExpression(joinItem.RightColumn);
                    SafeWriteLine();
                    SafeUnIndent();
                }
            }
        }
        /// <summary>
        /// Writes the JOIN clause for a SQL statement.
        /// </summary>
        /// <param name="joinClauseList">
        /// A <see cref="SqlCodeJoinClauseCollection"/> containing the definitions of items to join on
        /// </param>
        public async Task WriteJoinClauseAsync(SqlCodeJoinClauseCollection? joinClauseList)
        {
            if (joinClauseList != null && _expressionWriter != null)
            {
                // [INNER | LEFT] JOIN [table expression]
                //     ON [leftTable].[column] [= | !=] [rightTable].[column]
                SafeIndent();
                foreach (SqlCodeJoinClause joinItem in joinClauseList)
                {
                    // LEFT or INNER JOIN
                    await SafeWriteTabsAsync().ConfigureAwait(false);
                    if (joinItem.IsLeftJoin)
                    {
                        await SafeWriteAsync(RenderLeftJoin()).ConfigureAwait(false);
                    }
                    else
                    {
                        await SafeWriteAsync(RenderInnerJoin()).ConfigureAwait(false);
                    }

                    // [dbo].[Table] [Alias]
                    await _expressionWriter.WriteTableReferenceExpressionAsync(joinItem.ReferencedTable).ConfigureAwait(false);
                    await SafeWriteLineAsync().ConfigureAwait(false);

                    // ON <left expression> <operator> <right expression>
                    SafeIndent();
                    await SafeWriteTabsAsync().ConfigureAwait(false);

                    await SafeWriteAsync(RenderJoinOn() + " ").ConfigureAwait(false);
                    await _expressionWriter.WriteTableColumnReferenceExpressionAsync(joinItem.LeftColumn).ConfigureAwait(false);
                    await SafeWriteAsync(" ").ConfigureAwait(false);
                    await SafeWriteAsync(RenderSqlComparisonOperator(joinItem.Operator)).ConfigureAwait(false);
                    await SafeWriteAsync(" ").ConfigureAwait(false);
                    await _expressionWriter.WriteTableColumnReferenceExpressionAsync(joinItem.RightColumn).ConfigureAwait(false);
                    await SafeWriteLineAsync().ConfigureAwait(false);
                    SafeUnIndent();
                }
            }
        }
        /// <summary>
        /// Writes the selection clause portion of a SELECT statement.
        /// </summary>
        /// <example>
        /// SELECT
        ///   [table].[columnA],
        ///   [table].[columnB],
        ///   [...]
        /// </example>
        /// <param name="selectClause">
        /// The <see cref="SqlCodeSelectClause"/> instance to be rendered and written.
        /// </param>
        public void WriteSelectClause(SqlCodeSelectClause selectClause)
        {
            // SELECT [DISTINCT | TOP x]
            SafeWriteTabs();
            SafeWriteLine(RenderSelect(selectClause.Distinct, selectClause.TopValue));
            SafeIndent();

            // Write the list of items to select...
            //
            //    <item to select> [, ]
            //    <item to select> [, ]
            //    <item to select> [, ]
            WriteSelectItemsListClause(selectClause.SelectItemsList);
            SafeUnIndent();
        }
        /// <summary>
        /// Writes the selection clause portion of a SELECT statement.
        /// </summary>
        /// <example>
        /// SELECT
        ///   [table].[columnA],
        ///   [table].[columnB],
        ///   [...]
        /// </example>
        /// <param name="selectClause">
        /// The <see cref="SqlCodeSelectClause"/> instance to be rendered and written.
        /// </param>
        public async Task WriteSelectClauseAsync(SqlCodeSelectClause selectClause)
        {
            // SELECT [DISTINCT | TOP x]
            await SafeWriteTabsAsync().ConfigureAwait(false);
            await SafeWriteLineAsync(RenderSelect(selectClause.Distinct, selectClause.TopValue)).ConfigureAwait(false);
            SafeIndent();

            // Write the list of items to select...
            //
            //    <item to select> [, ]
            //    <item to select> [, ]
            //    <item to select> [, ]
            await WriteSelectItemsListClauseAsync(selectClause.SelectItemsList);
            SafeUnIndent();
        }
        /// <summary>
        /// Writes the items to be selected in the SELECT clause portion.
        /// </summary>
        /// <param name="selectItemsList">
        /// The <see cref="SqlCodeSelectListItemExpressionCollection"/> instance containing the items to be selected.
        /// </param>
        private void WriteSelectItemsListClause(SqlCodeSelectListItemExpressionCollection? selectItemsList)
        {
            if (selectItemsList != null)
            {
                // Locate the last item in the list that is a field or other selection item.
                int lastIndex = selectItemsList.FindLastNonWhiteSpaceItem();

                int len = selectItemsList.Count;
                for (int count = 0; count < len; count++)
                {
                    SqlCodeSelectListItemExpression listItem = selectItemsList[count];
                    SafeWriteTabs();

                    switch (listItem.Expression)
                    {
                        case SqlCodeCommentExpression:
                            WriteSelectListItemExpression(listItem, string.Empty);
                            break;

                        case SqlCodeLiteralExpression literalExpression:
                            if (string.IsNullOrEmpty(literalExpression.Expression))
                            {
                                WriteSelectListItemExpression(listItem, string.Empty);
                            }
                            else
                            {
                                WriteSelectListItemExpression(listItem, SelectDelimiter);
                            }

                            break;

                        default:
                            if (count < lastIndex)
                            {
                                WriteSelectListItemExpression(listItem, SelectDelimiter);
                            }
                            else
                            {
                                WriteSelectListItemExpression(listItem, string.Empty);
                            }

                            break;
                    }
                    SafeWriteLine();
                }
            }
        }
        /// <summary>
        /// Writes the items to be selected in the SELECT clause portion.
        /// </summary>
        /// <param name="selectItemsList">
        /// The <see cref="SqlCodeSelectListItemExpressionCollection"/> instance containing the items to be selected.
        /// </param>
        private async Task WriteSelectItemsListClauseAsync(SqlCodeSelectListItemExpressionCollection? selectItemsList)
        {
            if (selectItemsList != null)
            {
                // Locate the last item in the list that is a field or other selection item.
                int lastIndex = selectItemsList.FindLastNonWhiteSpaceItem();

                int len = selectItemsList.Count;
                for (int count = 0; count < len; count++)
                {
                    SqlCodeSelectListItemExpression listItem = selectItemsList[count];
                    await SafeWriteTabsAsync().ConfigureAwait(false);

                    switch (listItem.Expression)
                    {
                        case SqlCodeCommentExpression:
                            await WriteSelectListItemExpressionAsync(listItem, string.Empty);
                            break;

                        case SqlCodeLiteralExpression literalExpression:
                            if (string.IsNullOrEmpty(literalExpression.Expression))
                            {
                                await WriteSelectListItemExpressionAsync(listItem, string.Empty);
                            }
                            else
                            {
                                await WriteSelectListItemExpressionAsync(listItem, SelectDelimiter);
                            }

                            break;

                        default:
                            if (count < lastIndex)
                            {
                                await WriteSelectListItemExpressionAsync(listItem, SelectDelimiter);
                            }
                            else
                            {
                                await WriteSelectListItemExpressionAsync(listItem, string.Empty);
                            }

                            break;
                    }
                    await SafeWriteLineAsync().ConfigureAwait(false);
                }
            }
        }
        /// <summary>
        /// Writes the SQL code select list item expression.
        /// </summary>
        /// <param name="listItem">
        /// The <see cref="SqlCodeSelectListItemExpression"/> instance to be rendered and written.
        /// </param>
        /// <param name="delimiter">
        /// The delimiter to append to the end of the rendering.
        /// </param>
        private void WriteSelectListItemExpression(SqlCodeSelectListItemExpression? listItem, string? delimiter)
        {
            if (listItem != null && delimiter != null && _expressionWriter != null)
                // Render and write the expression.
            {
                _expressionWriter.WriteExpression(listItem.Expression);
            }

            // Add the comma, if specified.
            if (!string.IsNullOrEmpty(delimiter))
            {
                SafeWrite(delimiter);
            }
        }
        /// <summary>
        /// Writes the SQL code select list item expression.
        /// </summary>
        /// <param name="listItem">
        /// The <see cref="SqlCodeSelectListItemExpression"/> instance to be rendered and written.
        /// </param>
        /// <param name="delimiter">
        /// The delimiter to append to the end of the rendering.
        /// </param>
        private async Task WriteSelectListItemExpressionAsync(SqlCodeSelectListItemExpression? listItem, string? delimiter)
        {
            if (listItem != null && _expressionWriter != null)
            {
                // Render and write the expression.
                await _expressionWriter.WriteExpressionAsync(listItem.Expression).ConfigureAwait(false);

                // Add the comma, if specified.
                if (!string.IsNullOrEmpty(delimiter))
                {
                    await SafeWriteAsync(delimiter).ConfigureAwait(false);
                }
            }
        }
        /// <summary>
        /// Writes the WHERE clause for a SQL statement.
        /// </summary>
        /// <param name="whereClause">
        /// The <see cref="SqlCodeWhereClause"/> instance containing the WHERE clause to be rendered and written.
        /// </param>
        public void WriteWhereClause(SqlCodeWhereClause? whereClause)
        {
            if (_expressionWriter != null && whereClause != null && whereClause.Conditions != null && whereClause.Conditions.Count > 0)
            {

                // WHERE
                SafeWriteTabs();
                SafeWrite(RenderWhere() + " ");
                SafeWriteLine();
                SafeIndent();

                // [(] <condition> <operator> <condition> [)] [AND|OR]

                bool useParens = whereClause.Conditions.Count > 1;
                foreach (SqlCodeConditionListExpression expression in whereClause.Conditions)
                {
                    SafeWriteTabs();
                    _expressionWriter.WriteConditionListExpression(expression, useParens);
                    SafeWriteLine();
                }
                SafeUnIndent();
            }
        }
        /// <summary>
        /// Writes the WHERE clause for a SQL statement.
        /// </summary>
        /// <param name="whereClause">
        /// The <see cref="SqlCodeWhereClause"/> instance containing the WHERE clause to be rendered and written.
        /// </param>
        public async Task WriteWhereClauseAsync(SqlCodeWhereClause? whereClause)
        {
            if (_expressionWriter != null && whereClause != null && whereClause.Conditions != null && whereClause.Conditions.Count > 0)
            {
                // WHERE
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync(RenderWhere() + " ").ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);
                SafeIndent();

                // [(] <condition> <operator> <condition> [)] [AND|OR]

                foreach (SqlCodeConditionListExpression expression in whereClause.Conditions)
                {
                    await SafeWriteTabsAsync().ConfigureAwait(false);
                    await _expressionWriter.WriteConditionListExpressionAsync(expression).ConfigureAwait(false);
                    await SafeWriteLineAsync().ConfigureAwait(false);
                }
                await SafeWriteLineAsync().ConfigureAwait(false);
                SafeUnIndent();
            }
        }
        #endregion
    }
}