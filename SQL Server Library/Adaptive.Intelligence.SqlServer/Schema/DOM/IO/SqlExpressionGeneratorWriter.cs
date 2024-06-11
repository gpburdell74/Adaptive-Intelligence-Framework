using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.CodeDom.CodeProvider;

namespace Adaptive.Intelligence.SqlServer.CodeDom.IO
{
    /// <summary>
    /// Provides a mechanism for writing SQL expressions.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    internal sealed class SqlExpressionWriter : SafeGeneratorWriterBase
    {
        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlExpressionWriter"/> class.
        /// </summary>
        /// <param name="codeProvider">
        /// An <see cref="ISqlCodeProvider"/> implementation used to provide the SQL language specific code to
        /// the writer.
        /// </param>
        /// <param name="writer">
        /// The <see cref="SqlTextWriter"/> instance being written to by the parent caller, or <b>null</b>.
        /// </param>
        public SqlExpressionWriter(ISqlCodeProvider? codeProvider, SqlTextWriter? writer) : base(codeProvider, writer)
        {
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Renders and writes the SQL object alias name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeAliasExpression"/> instance to be rendered.
        /// </param>
        public void WriteAliasExpression(SqlCodeAliasExpression? expression)
        {
            if (expression != null && !string.IsNullOrEmpty(expression.Name))
            {
                // [<rendering of alias expression>]
                SafeWrite(ObjectNameStartDelimiter + expression.Name + ObjectNameEndDelimiter);
            }
        }
        /// <summary>
        /// Renders and writes the SQL object alias name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeAliasExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteAliasExpressionAsync(SqlCodeAliasExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Name))
                await SafeWriteAsync(ObjectNameStartDelimiter + expression.Name +ObjectNameEndDelimiter)
                    .ConfigureAwait(false);
        }
        /// <summary>
        /// Renders and writes the assignment expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeAssignmentExpression"/> instance to be rendered.
        /// </param>
        public void WriteAssignmentExpression(SqlCodeAssignmentExpression? expression)
        {
            if (expression != null && expression.AssignedToExpression != null && expression.ValueExpression != null)
            {
                // <left expression rendering> = <right expression rendering>
                WriteExpression(expression.AssignedToExpression);
                SafeWrite(Constants.Space + AssignmentOperator + Constants.Space);
                WriteExpression(expression.ValueExpression);
            }
        }
        /// <summary>
        /// Renders and writes the assignment expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeAssignmentExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteAssignmentExpressionAsync(SqlCodeAssignmentExpression? expression)
        {
            if (CanWrite && expression != null && expression.AssignedToExpression != null && expression.ValueExpression != null)
            {
                await WriteExpressionAsync(expression.AssignedToExpression).ConfigureAwait(false);
                await SafeWriteAsync(Constants.Space + AssignmentOperator + Constants.Space).ConfigureAwait(false);
                await WriteExpressionAsync(expression.ValueExpression).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Renders and writes the SQL column name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeColumnNameExpression"/> instance to be rendered.
        /// </param>
        public void WriteColumnNameExpression(SqlCodeColumnNameExpression? expression)
        {
            if (expression != null && !string.IsNullOrEmpty(expression.ColumnName))
            {
                // [<column name> ]
                SafeWrite(ObjectNameStartDelimiter + expression.ColumnName + ObjectNameEndDelimiter);
            }
        }
        /// <summary>
        /// Renders and writes the SQL column name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeColumnNameExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteColumnNameExpressionAsync(SqlCodeColumnNameExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.ColumnName))
                await SafeWriteAsync(
                    ObjectNameStartDelimiter + expression.ColumnName +
                        ObjectNameEndDelimiter).ConfigureAwait(false);
        }
        /// <summary>
        /// Writes the comment line as a line in a comment block.
        /// </summary>
        /// <param name="commentExpression">
        /// A <see cref="SqlCodeCommentExpression"/> containing the comment text.
        /// </param>
        public void WriteCommentBlockExpression(SqlCodeCommentExpression? commentExpression)
        {
            if (CanWrite && commentExpression != null)
                SafeWrite(RenderCommentBlockPrefix() + commentExpression.Comment);
        }
        /// <summary>
        /// Writes the comment line as a line in a comment block.
        /// </summary>
        /// <param name="commentExpression">
        /// A <see cref="SqlCodeCommentExpression"/> containing the comment text.
        /// </param>
        public async Task WriteCommentBlockExpressionAsync(SqlCodeCommentExpression? commentExpression)
        {
            if (CanWrite && commentExpression != null)
                await SafeWriteAsync(
                    RenderCommentBlockPrefix() +
                            commentExpression.Comment
                            ).ConfigureAwait(false);
        }
        /// <summary>
        /// Writes the comment block expression ending text.
        /// </summary>
        public void WriteCommentBlockExpressionEnd()
        {
            if (CanWrite)
                SafeWrite(RenderCommentBlockEnd());
        }
        /// <summary>
        /// Writes the comment block expression ending text.
        /// </summary>
        public async Task WriteCommentBlockExpressionEndAsync()
        {
            if (CanWrite)
                await SafeWriteAsync(RenderCommentBlockEnd()).ConfigureAwait(false);
        }
        /// <summary>
        /// Writes the comment block expression start text.
        /// </summary>
        public void WriteCommentBlockExpressionStart()
        {
            if (CanWrite)
                SafeWrite(RenderCommentBlockStart());
        }
        /// <summary>
        /// Writes the comment block expression start text.
        /// </summary>
        public async Task WriteCommentBlockExpressionStartAsync()
        {
            if (CanWrite)
                await SafeWriteAsync(RenderCommentBlockStart()).ConfigureAwait(false);
        }
        /// <summary>
        /// Renders and writes the SQL comment expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeCommentExpression"/> instance to be rendered.
        /// </param>
        public void WriteCommentExpression(SqlCodeCommentExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Comment))
                SafeWrite(SqlCommentLineDelimiter + expression.Comment);
        }
        /// <summary>
        /// Renders and writes the SQL comment expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeCommentExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteCommentExpressionAsync(SqlCodeCommentExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Comment))
                await SafeWriteAsync(SqlCommentLineDelimiter + expression.Comment)
                    .ConfigureAwait(false);
        }
        /// <summary>
        /// Renders and writes the SQL expression for a condition to be evaluated.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeConditionExpression"/> instance to be rendered.
        /// </param>
        public void WriteConditionExpression(SqlCodeConditionExpression? expression)
        {
            if (CanWrite && expression != null && expression.LeftExpression != null && expression.RightExpression != null)
            {
                // Write the left-side expression type.
                WriteExpression(expression.LeftExpression);

                // Write the operator type.
                SafeWrite(Constants.Space + RenderSqlComparisonOperator(expression.Operator) + Constants.Space);

                // Write the right-side expression type.
                WriteExpression(expression.RightExpression);
            }
        }
        /// <summary>
        /// Renders and writes the SQL expression for a condition to be evaluated.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeConditionExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteConditionExpressionAsync(SqlCodeConditionExpression? expression)
        {
            if (CanWrite &&
                expression != null &&
                expression.LeftExpression != null &&
                expression.RightExpression != null)
            {
                // Write the left-side expression type.
                await WriteExpressionAsync(expression.LeftExpression).ConfigureAwait(false);

                // Write the operator type.
                await SafeWriteAsync(
                    Constants.Space +
                            RenderSqlComparisonOperator(expression.Operator) +
                            Constants.Space).ConfigureAwait(false);

                // Write the right-side expression type.
                await WriteExpressionAsync(expression.RightExpression).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Renders and writes the SQL condition list expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeConditionListExpression"/> instance to be rendered.
        /// </param>
        public void WriteConditionListExpression(SqlCodeConditionListExpression? expression, bool useParens = true)
        {
            if (CanWrite && expression != null && expression.Expression != null)
            {
                if (useParens)
                    SafeWrite(OpenParenthesis);

                WriteConditionExpression(expression.Expression);

                if (useParens)
                    SafeWrite(CloseParenthesis);

                if (expression.Operator != SqlConditionOperator.NotSpecified)
                {
                    SafeWrite(Constants.Space);
                    SafeWrite(RenderSqlConditionOperator(expression.Operator));
                }
            }
        }
        /// <summary>
        /// Renders and writes the SQL condition list expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeConditionListExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteConditionListExpressionAsync(SqlCodeConditionListExpression? expression)
        {
            if (CanWrite && expression != null && expression.Expression != null)
            {
                await SafeWriteAsync(OpenParenthesis).ConfigureAwait(false);
                await WriteConditionExpressionAsync(expression.Expression).ConfigureAwait(false);
                await SafeWriteAsync(CloseParenthesis).ConfigureAwait(false);

                if (expression.Operator != SqlConditionOperator.NotSpecified)
                    await SafeWriteAsync(RenderSqlConditionOperator(expression.Operator))
                        .ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Renders and writes the SQL database name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeDatabaseNameExpression"/> instance to be rendered.
        /// </param>
        public void WriteDatabaseNameExpression(SqlCodeDatabaseNameExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Name))
                SafeWrite(ObjectNameStartDelimiter + expression.Name + ObjectNameEndDelimiter);
        }
        /// <summary>
        /// Renders and writes the SQL database name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeDatabaseNameExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteDatabaseNameExpressionAsync(SqlCodeDatabaseNameExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Name))
                await SafeWriteAsync(ObjectNameStartDelimiter + expression.Name +
                                         ObjectNameEndDelimiter).ConfigureAwait(false);
        }
        /// <summary>
        /// Renders and writes the SQL database owner name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeDatabaseNameOwnerNameExpression"/> instance to be rendered.
        /// </param>
        public void WriteDatabaseOwnerNameExpression(SqlCodeDatabaseNameOwnerNameExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Name))
                SafeWrite(ObjectNameStartDelimiter + expression.Name + ObjectNameEndDelimiter);
        }
        /// <summary>
        /// Renders and writes the SQL database owner name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeDatabaseNameOwnerNameExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteDatabaseOwnerNameExpressionAsync(SqlCodeDatabaseNameOwnerNameExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Name))
                await SafeWriteAsync(ObjectNameStartDelimiter + expression.Name + ObjectNameEndDelimiter)
                    .ConfigureAwait(false);
        }
        /// <summary>
        /// Renders and writes the data type expression.
        /// </summary>
        /// <param name="expression">
        /// A <see cref="SqlCodeDataTypeSpecificationExpression"/> containing the data type name, type, length, precision, scale,
        ///  etc. as needed to define the data type.
        /// </param>
        public void WriteDataTypeExpression(SqlCodeDataTypeSpecificationExpression? expression)
        {
            if (expression != null && CanWrite)
            {
                SafeWrite(RenderDataTypeName(expression.DataType));

                // Certain types require length specification; others require precision and scale specification.
                switch (expression.DataType)
                {
                    case Schema.SqlDataTypes.Binary:
                    case Schema.SqlDataTypes.VarBinary:
                    case Schema.SqlDataTypes.NChar:
                    case Schema.SqlDataTypes.VarChar:
                        // Specify length value, if specified in expression.
                        if (expression.MaxLength > 0)
                            SafeWrite(OpenParenthesis + expression.MaxLength + CloseParenthesis);
                        break;

                    case Schema.SqlDataTypes.NVarCharOrSysName:
                        if (expression.MaxLength > 0)
                        {
                            if (!expression.IsPadded)
                                SafeWrite(OpenParenthesis + expression.MaxLength + CloseParenthesis);
                            else
                                SafeWrite(OpenParenthesis + (expression.MaxLength / 2).ToString() + CloseParenthesis);
                        }
                        break;

                    case Schema.SqlDataTypes.DateTimeOffset:
                        SafeWrite(OpenParenthesis + "7" + CloseParenthesis);
                        break;

                    case Schema.SqlDataTypes.Decimal:
                    case Schema.SqlDataTypes.Float:
                    case Schema.SqlDataTypes.Real:
                        // Specify scale and precision.
                        SafeWrite(
                            OpenParenthesis +
                            expression.Precision +
                            Constants.Comma +
                             expression.Scale +
                             CloseParenthesis);
                        break;
                }
            }
        }
        /// <summary>
        /// Renders and writes the data type expression.
        /// </summary>
        /// <param name="expression">
        /// A <see cref="SqlCodeDataTypeSpecificationExpression"/> containing the data type name, type, length, precision, scale,
        ///  etc. as needed to define the data type.
        /// </param>
        public async Task WriteDataTypeExpressionAsync(SqlCodeDataTypeSpecificationExpression? expression)
        {
            if (CanWrite && expression != null)
            { 
                await SafeWriteAsync(RenderDataTypeName(expression.DataType)).ConfigureAwait(false);

                // Certain types require length specification; others require precision and scale specification.
                switch (expression.DataType)
                {
                    case Schema.SqlDataTypes.Binary:
                    case Schema.SqlDataTypes.VarBinary:
                    case Schema.SqlDataTypes.NChar:
                    case Schema.SqlDataTypes.NVarCharOrSysName:
                    case Schema.SqlDataTypes.VarChar:
                        // Specify length value, if specified in expression.
                        if (expression.MaxLength > 0)
                            await SafeWriteAsync(
                                OpenParenthesis +
                                expression.MaxLength +
                                                 CloseParenthesis
                                                 ).ConfigureAwait(false);
                        break;

                    case Schema.SqlDataTypes.DateTimeOffset:
                        await SafeWriteAsync(OpenParenthesis + "7" + CloseParenthesis).ConfigureAwait(false);
                        break;

                    case Schema.SqlDataTypes.Decimal:
                    case Schema.SqlDataTypes.Float:
                    case Schema.SqlDataTypes.Real:
                        // Specify scale and precision.
                        await SafeWriteAsync(
                            OpenParenthesis +
                            expression.Precision +
                            Constants.Comma +
                            expression.Scale +
                            CloseParenthesis).ConfigureAwait(false);
                        break;
                }
            }
        }
        /// <summary>
        /// Writes the SQL expression to the output, based on its actual type.
        /// </summary>
        /// <param name="expression">
        /// A <see cref="SqlCodeExpression"/> implementation to be written.
        /// </param>
        public void WriteExpression(SqlCodeExpression? expression)
        {
            if (expression != null)
            {
                switch (expression)
                {
                    case SqlCodeAliasExpression aliasExpression:
                        WriteAliasExpression(aliasExpression);
                        break;

                    case SqlCodeColumnNameExpression columNameExpression:
                        WriteColumnNameExpression(columNameExpression);
                        break;

                    case SqlCodeCommentExpression commentExpression:
                        WriteCommentExpression(commentExpression);
                        break;

                    case SqlCodeConditionExpression conditionExpression:
                        WriteConditionExpression(conditionExpression);
                        break;

                    case SqlCodeConditionListExpression conditionListExpression:
                        WriteConditionListExpression(conditionListExpression);
                        break;

                    case SqlCodeDatabaseNameExpression dbNameExpression:
                        WriteDatabaseNameExpression(dbNameExpression);
                        break;

                    case SqlCodeDatabaseNameOwnerNameExpression ownerNameExpression:
                        WriteDatabaseOwnerNameExpression(ownerNameExpression);
                        break;

                    case SqlCodeLiteralExpression literalExpression:
                        WriteLiteralExpression(literalExpression);
                        break;

                    case SqlCodeParameterNameExpression parameterNameExpression:
                        WriteParameterNameExpression(parameterNameExpression);
                        break;

                    case SqlCodeParameterReferenceExpression parameterReferenceExpression:
                        WriteParameterReferenceExpression(parameterReferenceExpression);
                        break;

                    case SqlCodeSelectListItemExpression listItemExpression:
                        WriteListItemExpression(listItemExpression);
                        break;

                    case SqlCodeTableReferenceExpression sourceTableExpression:
                        WriteTableReferenceExpression(sourceTableExpression);
                        break;

                    case SqlCodeStoredProcedureNameExpression spNameExpression:
                        WriteStoredProcedureNameExpression(spNameExpression);
                        break;

                    case SqlCodeTableColumnReferenceExpression tableColumnReferenceExpression:
                        WriteTableColumnReferenceExpression(tableColumnReferenceExpression);
                        break;

                    case SqlCodeTableNameExpression tableNameExpression:
                        WriteTableNameExpression(tableNameExpression);
                        break;

                    case SqlCodeDataTypeSpecificationExpression dataTypeExpression:
                        WriteDataTypeExpression(dataTypeExpression);
                        break;

                    case SqlCodeParameterDefinitionExpression paramDefExpression:
                        WriteParameterDefinitionExpression(paramDefExpression);
                        break;

                    case SqlCodeFunctionCallExpression functionCallExpression:
                        WriteFunctionCallExpression(functionCallExpression);
                        break;

                    case SqlCodeVariableDefinitionExpression variableDefExpression:
                        WriteVariableDefinitionExpression(variableDefExpression);
                        break;

                    case SqlCodeVariableNameExpression variableNameExpression:
                        WriteVariableNameExpression(variableNameExpression);
                        break;

                    case SqlCodeVariableReferenceExpression variableReferenceExpression:
                        WriteVariableReferenceExpression(variableReferenceExpression);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(expression), 
                            @"This expression type is not supported.");
                }
            }
        }
        /// <summary>
        /// Writes the SQL expression to the output, based on its actual type.
        /// </summary>
        /// <param name="expression">
        /// A <see cref="SqlCodeExpression"/> implementation to be written.
        /// </param>
        public async Task WriteExpressionAsync(SqlCodeExpression? expression)
        {
            if (expression != null)
            {
                switch (expression)
                {
                    case SqlCodeAliasExpression aliasExpression:
                        await WriteAliasExpressionAsync(aliasExpression);
                        break;

                    case SqlCodeColumnNameExpression columNameExpression:
                        await WriteColumnNameExpressionAsync(columNameExpression);
                        break;

                    case SqlCodeCommentExpression commentExpression:
                        await WriteCommentExpressionAsync(commentExpression);
                        break;

                    case SqlCodeConditionExpression conditionExpression:
                        await WriteConditionExpressionAsync(conditionExpression);
                        break;

                    case SqlCodeConditionListExpression conditionListExpression:
                        await WriteConditionListExpressionAsync(conditionListExpression);
                        break;

                    case SqlCodeDatabaseNameExpression dbNameExpression:
                        await WriteDatabaseNameExpressionAsync(dbNameExpression);
                        break;

                    case SqlCodeDatabaseNameOwnerNameExpression ownerNameExpression:
                        await WriteDatabaseOwnerNameExpressionAsync(ownerNameExpression);
                        break;

                    case SqlCodeLiteralExpression literalExpression:
                        await WriteLiteralExpressionAsync(literalExpression);
                        break;

                    case SqlCodeParameterNameExpression parameterNameExpression:
                        await WriteParameterNameExpressionAsync(parameterNameExpression);
                        break;

                    case SqlCodeParameterReferenceExpression parameterReferenceExpression:
                        await WriteParameterReferenceExpressionAsync(parameterReferenceExpression);
                        break;

                    case SqlCodeSelectListItemExpression listItemExpression:
                        await WriteListItemExpressionAsync(listItemExpression);
                        break;

                    case SqlCodeTableReferenceExpression tableReferenceExpression:
                        await WriteTableReferenceExpressionAsync(tableReferenceExpression);
                        break;

                    case SqlCodeStoredProcedureNameExpression spNameExpression:
                        await WriteStoredProcedureNameExpressionAsync(spNameExpression);
                        break;

                    case SqlCodeTableColumnReferenceExpression tableColumnReferenceExpression:
                        await WriteTableColumnReferenceExpressionAsync(tableColumnReferenceExpression);
                        break;

                    case SqlCodeTableNameExpression tableNameExpression:
                        await WriteTableNameExpressionAsync(tableNameExpression);
                        break;

                    case SqlCodeDataTypeSpecificationExpression dataTypeExpression:
                        await WriteDataTypeExpressionAsync(dataTypeExpression);
                        break;

                    case SqlCodeParameterDefinitionExpression paramDefExpression:
                        await WriteParameterDefinitionExpressionAsync(paramDefExpression);
                        break;

                    case SqlCodeFunctionCallExpression functionCallExpression:
                        await WriteFunctionCallExpressionAsync(functionCallExpression);
                        break;

                    case SqlCodeVariableDefinitionExpression variableDefExpression:
                        await WriteVariableDefinitionExpressionAsync(variableDefExpression);
                        break;

                    case SqlCodeVariableNameExpression variableNameExpression:
                        await WriteVariableNameExpressionAsync(variableNameExpression);
                        break;

                    case SqlCodeVariableReferenceExpression variableReferenceExpression:
                        await WriteVariableReferenceExpressionAsync(variableReferenceExpression);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(expression), @"This expression type is not supported.");
                }
            }
        }
        /// <summary>
        /// Renders and writes the function call expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeFunctionCallExpression"/> instance to be rendered.
        /// </param>
        public void WriteFunctionCallExpression(SqlCodeFunctionCallExpression? expression)
        {
            if (CanWrite && expression != null)
            {
                SafeWrite(expression.FunctionName + OpenParenthesis);
                if (expression.ParameterValueList != null && expression.ParameterValueList.Count > 0)
                {
                    int len = expression.ParameterValueList.Count;
                    for (int count = 0; count < len; count++)
                    {
                        WriteExpression(expression.ParameterValueList[count]);
                        if (count < len - 1)
                            SafeWrite(Constants.CommaWithSpace);
                    }
                }
                SafeWrite(CloseParenthesis);
            }
        }
        /// <summary>
        /// Renders and writes the function call expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeFunctionCallExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteFunctionCallExpressionAsync(SqlCodeFunctionCallExpression? expression)
        {
            if (CanWrite && expression != null)
            {
                await SafeWriteAsync(expression.FunctionName + OpenParenthesis).ConfigureAwait(false);
                if (expression.ParameterValueList != null && expression.ParameterValueList.Count > 0)
                {
                    int len = expression.ParameterValueList.Count;
                    for (int count = 0; count < len; count++)
                    {
                        await WriteExpressionAsync(expression.ParameterValueList[count]);
                        if (count < len - 1)
                            await SafeWriteAsync(Constants.CommaWithSpace).ConfigureAwait(false);
                    }
                }
                await SafeWriteAsync(CloseParenthesis).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Renders and writes the SQL select list item expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeColumnNameExpression"/> instance to be rendered.
        /// </param>
        public void WriteListItemExpression(SqlCodeSelectListItemExpression? expression)
        {
            if (expression != null)
                WriteExpression(expression.Expression);
        }
        /// <summary>
        /// Renders and writes the SQL select list item expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeColumnNameExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteListItemExpressionAsync(SqlCodeSelectListItemExpression? expression)
        {
            if (expression != null)
                await WriteExpressionAsync(expression.Expression).ConfigureAwait(false);
        }
        /// <summary>
        /// Renders and writes the SQL literal expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeLiteralExpression"/> instance to be rendered.
        /// </param>
        public void WriteLiteralExpression(SqlCodeLiteralExpression? expression)
        {
            if (expression != null && !string.IsNullOrEmpty(expression.Expression))
                SafeWrite(expression.Expression);
        }
        /// <summary>
        /// Renders and writes the SQL literal expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeLiteralExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteLiteralExpressionAsync(SqlCodeLiteralExpression? expression)
        {
            if (expression != null && !string.IsNullOrEmpty(expression.Expression))
                await SafeWriteAsync(expression.Expression).ConfigureAwait(false);
        }
        /// <summary>
        /// Writes the SQL parameter definition expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeParameterDefinitionExpression"/> instance to be rendered and written.
        /// </param>
        public void WriteParameterDefinitionExpression(SqlCodeParameterDefinitionExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Name))
            {
                SafeWriteTabs();
                //@ParameterName
                if (ParameterNamePrefix != null && !expression.Name.StartsWith(ParameterNamePrefix))
                    SafeWrite(ParameterNamePrefix + expression.Name);
                else
                    SafeWrite(expression.Name);

                // Tab over some...
                SafeWriteTabs(4);

                // e.g.
                // NVARCHAR(128) or
                // INT or
                // DATETIMEOFFSET(7) or
                // BIT
                WriteDataTypeExpression(expression.DataTypeExpression);
            }
        }
        /// <summary>
        /// Writes the SQL parameter definition expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeParameterDefinitionExpression"/> instance to be rendered and written.
        /// </param>
        public async Task WriteParameterDefinitionExpressionAsync(SqlCodeParameterDefinitionExpression? expression)
        {
            if (CanWrite && expression != null)
            {
                //@ParameterName
                await SafeWriteTabsAsync().ConfigureAwait(false);
                await SafeWriteAsync(ParameterNamePrefix + expression.Name).ConfigureAwait(false);

                // Tab over some...
                await SafeWriteTabsAsync(4).ConfigureAwait(false);

                // e.g.
                // NVARCHAR(128) or
                // INT or
                // DATETIMEOFFSET(7) or
                // BIT
                await WriteDataTypeExpressionAsync(expression.DataTypeExpression).ConfigureAwait(false);
                await SafeWriteLineAsync().ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Renders and writes the SQL parameter name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeParameterNameExpression"/> instance to be rendered.
        /// </param>
        public void WriteParameterNameExpression(SqlCodeParameterNameExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Name))
                SafeWrite(ParameterNamePrefix + expression.Name);
        }
        /// <summary>
        /// Renders and writes the SQL parameter name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeParameterNameExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteParameterNameExpressionAsync(SqlCodeParameterNameExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Name))
            {
                if (ParameterNamePrefix != null && !expression.Name.StartsWith(ParameterNamePrefix))
                    await SafeWriteAsync(ParameterNamePrefix + expression.Name).ConfigureAwait(false);
                else
                    await SafeWriteAsync(expression.Name).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Renders and writes the SQL parameter reference expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeParameterReferenceExpression"/> instance to be rendered.
        /// </param>
        public void WriteParameterReferenceExpression(SqlCodeParameterReferenceExpression? expression)
        {
            if (expression != null)
                WriteParameterNameExpression(expression.Name);
        }
        /// <summary>
        /// Renders and writes the SQL parameter reference expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeParameterReferenceExpression"/> instance to be rendered.
        /// </param>
        public Task WriteParameterReferenceExpressionAsync(SqlCodeParameterReferenceExpression? expression)
        {
            return WriteParameterNameExpressionAsync(expression?.Name);
        }
        /// <summary>
        /// Renders and writes the SQL stored procedure name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeStoredProcedureNameExpression"/> instance to be rendered.
        /// </param>
        public void WriteStoredProcedureNameExpression(SqlCodeStoredProcedureNameExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Name))
                SafeWrite(
                    ObjectNameStartDelimiter + 
                    expression.Name + 
                    ObjectNameEndDelimiter);
        }
        /// <summary>
        /// Renders and writes the SQL stored procedure name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeStoredProcedureNameExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteStoredProcedureNameExpressionAsync(SqlCodeStoredProcedureNameExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.Name))
            {
                await SafeWriteAsync(
                    ObjectNameStartDelimiter +
                    expression.Name +
                    ObjectNameEndDelimiter).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Renders and writes the SQL table and column name reference expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeTableColumnReferenceExpression"/> instance to be rendered.
        /// </param>
        public void WriteTableColumnReferenceExpression(SqlCodeTableColumnReferenceExpression? expression)
        {
            if (expression != null)
            {
                if (expression.TableName != null)
                {
                    WriteTableNameExpression(expression.TableName);
                    SafeWrite(Constants.Dot);
                }
                if (expression.ColumnName != null)
                    WriteColumnNameExpression(expression.ColumnName);
            }
        }
        /// <summary>
        /// Renders and writes the SQL table and column name reference expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeTableColumnReferenceExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteTableColumnReferenceExpressionAsync(SqlCodeTableColumnReferenceExpression? expression)
        {
            if (expression != null)
            {
                if (expression.TableName != null)
                {
                    await WriteTableNameExpressionAsync(expression.TableName).ConfigureAwait(false);
                    await SafeWriteAsync(Constants.Dot).ConfigureAwait(false);
                }
                if (expression.ColumnName != null)
                    await WriteColumnNameExpressionAsync(expression.ColumnName).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Renders and writes the SQL table name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeTableNameExpression"/> instance to be rendered.
        /// </param>
        public void WriteTableNameExpression(SqlCodeTableNameExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.TableName))
                SafeWrite(ObjectNameStartDelimiter + expression.TableName + ObjectNameEndDelimiter);
        }
        /// <summary>
        /// Renders and writes the SQL table name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeTableNameExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteTableNameExpressionAsync(SqlCodeTableNameExpression? expression)
        {
            if (CanWrite && expression != null && !string.IsNullOrEmpty(expression.TableName))
                await SafeWriteAsync(ObjectNameStartDelimiter + expression.TableName +
                                         ObjectNameEndDelimiter)
                    .ConfigureAwait(false);
        }
        /// <summary>
        /// Renders and writes the SQL table source expression.
        /// </summary>
        /// <example>
        /// [dbo].[TableName] [AsAlias]
        /// </example>
        /// <param name="expression">
        /// The <see cref="SqlCodeTableReferenceExpression"/> instance to be rendered.
        /// </param>
        public void WriteTableReferenceExpression(SqlCodeTableReferenceExpression? expression)
        {
            if (expression != null && expression.TableName != null)
            {
                if (expression.OwnerName != null)
                {
                    WriteDatabaseOwnerNameExpression(expression.OwnerName);
                    SafeWrite(Constants.Dot);
                }

                WriteTableNameExpression(expression.TableName);
                if (expression.Alias != null)
                {
                    SafeWrite(Constants.Space);
                    WriteAliasExpression(expression.Alias);
                }
            }
        }
        /// <summary>
        /// Renders and writes the SQL table source expression.
        /// </summary>
        /// <example>
        /// [dbo].[TableName] [AsAlias]
        /// </example>
        /// <param name="expression">
        /// The <see cref="SqlCodeColumnNameExpression"/> instance to be rendered.
        /// </param>
        public async Task WriteTableReferenceExpressionAsync(SqlCodeTableReferenceExpression? expression)
        {
            if (expression != null && expression.TableName != null)
            {
                if (expression.OwnerName != null)
                    await WriteDatabaseOwnerNameExpressionAsync(expression.OwnerName).ConfigureAwait(false);

                await WriteTableNameExpressionAsync(expression.TableName);
                if (expression.Alias != null)
                {
                    await SafeWriteAsync(Constants.Space).ConfigureAwait(false);
                    await WriteAliasExpressionAsync(expression.Alias).ConfigureAwait(false);
                }
            }
        }
        /// <summary>
        /// Writes the variable definition expression.
        /// </summary>
        /// <param name="expression">
        /// A <see cref="SqlCodeVariableDefinitionExpression"/> instance used to define a variable.
        /// </param>
        public void WriteVariableDefinitionExpression(SqlCodeVariableDefinitionExpression? expression)
        {
            if (CanWrite && expression != null)
            {
                // @Variable
                SafeWrite(ParameterNamePrefix + expression.Name);
                SafeWrite(Constants.Space);

                // @DataType [(123) or (x,y)] etc.
                if (expression.DataTypeExpression != null)
                    WriteDataTypeExpression(expression.DataTypeExpression);
            }
        }
        /// <summary>
        /// Writes the variable definition expression.
        /// </summary>
        /// <param name="expression">
        /// A <see cref="SqlCodeVariableDefinitionExpression"/> instance used to define a variable.
        /// </param>
        public async Task WriteVariableDefinitionExpressionAsync(SqlCodeVariableDefinitionExpression? expression)
        {
            if (CanWrite && expression != null)
            {
                // @Variable
                await SafeWriteAsync(ParameterNamePrefix + expression.Name).ConfigureAwait(false);
                await SafeWriteAsync(Constants.Space).ConfigureAwait(false);

                // @DataType [(123) or (x,y)] etc.
                if (expression.DataTypeExpression != null)
                    await WriteDataTypeExpressionAsync(expression.DataTypeExpression).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Writes the variable name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeVariableNameExpression"/> instance containing the variable name.
        /// </param>
        public void WriteVariableNameExpression(SqlCodeVariableNameExpression? expression)
        {
            if (CanWrite && expression != null && expression.Name != null)
            {
                if (ParameterNamePrefix != null && !expression.Name.StartsWith(ParameterNamePrefix))
                    SafeWrite(ParameterNamePrefix + expression.Name);
                else
                    SafeWrite(expression.Name);
            }
        }
        /// <summary>
        /// Writes the variable name expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeVariableNameExpression"/> instance containing the variable name.
        /// </param>
        public async Task WriteVariableNameExpressionAsync(SqlCodeVariableNameExpression? expression)
        {
            if (CanWrite && expression != null && expression.Name != null)
            {
                if (ParameterNamePrefix != null && !expression.Name.StartsWith(ParameterNamePrefix))
                    await SafeWriteAsync(ParameterNamePrefix + expression.Name).ConfigureAwait(false);
                else
                    await SafeWriteAsync(expression.Name).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Writes the variable reference expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeVariableReferenceExpression"/> instance containing the variable reference.
        /// </param>
        public void WriteVariableReferenceExpression(SqlCodeVariableReferenceExpression? expression)
        {
            if (expression != null && expression.Name != null)
                WriteVariableNameExpression(expression.Name);
        }
        /// <summary>
        /// Writes the variable reference expression.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlCodeVariableReferenceExpression"/> instance containing the variable reference.
        /// </param>
        public async Task WriteVariableReferenceExpressionAsync(SqlCodeVariableReferenceExpression? expression)
        {
            if (expression != null && expression.Name != null)
                await WriteVariableNameExpressionAsync(expression.Name).ConfigureAwait(false);
        }
        #endregion

}
}
