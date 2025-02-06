using Adaptive.Intelligence.SqlServer.Schema;

namespace Adaptive.Intelligence.SqlServer.CodeDom.CodeProvider
{
    /// <summary>
    /// Provides the base signature for objects that generate SQL-code for data systems.  This can include
    /// ANSI SQL, PL/SQL, T-SQL, or others.  
    /// </summary>
    /// <remarks>
    /// The <see cref="IO.SqlWriter"/> will require an <see cref="ISqlCodeProvider"/> implementation in order to 
    /// write the SQL DOM instances to an output destination.
    /// </remarks>
    public interface ISqlCodeProvider
    {
        #region Properties        
        /// <summary>
        /// Gets the assignment operator representation.
        /// </summary>
        /// <value>
        /// A string containing the assignment operator.
        /// </value>
        string AssignmentOperator { get; }
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
        string CloseParenthesis { get; }
        /// <summary>
        /// Gets the closing/ending delimiter string for object names.
        /// </summary>
        /// <value>
        /// A string specifying the delimiter to use.
        /// </value>
        string ObjectNameEndDelimiter { get; }
        /// <summary>
        /// Gets the opening/starting delimiter string for object names.
        /// </summary>
        /// <value>
        /// A string specifying the delimiter to use.
        /// </value>
        string ObjectNameStartDelimiter { get; }
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
        string OpenParenthesis { get; }
        /// <summary>
        /// Gets the variable or parameter name prefix.
        /// </summary>
        /// <value>
        /// A string specifying the delimiter prefix to use that indicates a variable or parameter name.
        /// </value>
        string ParameterNamePrefix { get; }
        /// <summary>
        /// Gets the string used to delimit the items in a SELECT clause.
        /// </summary>
        /// <value>
        /// A string specifying the delimiter to use.
        /// </value>
        string SelectDelimiter { get; }
        /// <summary>
        /// Gets the string used to indicate a comment.
        /// </summary>
        /// <value>
        /// A string specifying the delimiter to use.
        /// </value>
        string SqlCommentLineDelimiter { get; }
        #endregion

        #region Methods / Functions        
        /// <summary>
        /// Renders the edit, alter, or modify stored procedure open statement line.
        /// </summary>
        /// <example>
        /// ALTER PROCEDURE [{schema}].[procedure name]
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
        string RenderAlterProcedureOpenStatement(SqlCodeDatabaseNameOwnerNameExpression owner, string name);
        /// <summary>
        /// Renders the code block body end keyword, for procedures or conditional blocks.
        /// </summary>
        /// <returns>
        /// A string containing the block body end keyword.
        /// </returns>
        string RenderBlockBodyEnd();
        /// <summary>
        /// Renders the code block body start keyword, for procedures or conditional blocks.
        /// </summary>
        /// <returns>
        /// A string containing the block body start keyword.
        /// </returns>
        string RenderBlockBodyStart();
        /// <summary>
        /// Renders the SQL text indicating the start of a comment block.
        /// </summary>
        /// <returns>
        /// A string containing the rendered text.
        /// </returns>
        string RenderCommentBlockEnd();
        /// <summary>
        /// Renders the SQL text indicating the text used at the start of each comment text line.
        /// </summary>
        /// <returns>
        /// A string containing the rendered text.
        /// </returns>
        string RenderCommentBlockPrefix();
        /// <summary>
        /// Renders the SQL text indicating the end of a comment block.
        /// </summary>
        /// <returns>
        /// A string containing the rendered text.
        /// </returns>
        string RenderCommentBlockStart();
        /// <summary>
        /// Renders the CREATE stored procedure open statement line.
        /// </summary>
        /// <example>
        /// CREATE PROCEDURE [{schema}].[procedure name]
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
        string RenderCreateProcedureOpenStatement(SqlCodeDatabaseNameOwnerNameExpression owner, string name);
        /// <summary>
        /// Renders the name of the data type.
        /// </summary>
        /// <param name="sqlDataType">
        /// A <see cref="SqlDataTypes"/> enumerated value indicating the data type.
        /// </param>
        /// <returns>
        /// A string containing the data type name.
        /// </returns>
        string RenderDataTypeName(SqlDataTypes sqlDataType);
        /// <summary>
        /// Renders the DECLARE keyword when defining a variable or memory table type.
        /// </summary>
        /// <returns>
        /// A string containing the rendered keyword.
        /// </returns>
        string RenderDeclare();
        /// <summary>
        /// Renders the DELETE keyword when deleting data.
        /// </summary>
        /// <returns>
        /// A string containing the rendered DELETE keyword.
        /// </returns>
        string RenderDelete();
        /// <summary>
        /// Renders the start of a FROM clause of a SQL statement.
        /// </summary>
        /// <returns>
        /// A string containing the start of the FROM clause of a SQL statement.
        /// </returns>
        string RenderFrom();
        /// <summary>
        /// Renders the INNER JOIN keywords for a SQL statement.
        /// </summary>
        /// <returns>
        /// A string containing the start of the INNER JOIN clause of a SQL statement.
        /// </returns>
        string RenderInnerJoin();
        /// <summary>
        /// Renders the INSERT INTO [table] portion for an insert statement
        /// </summary>
        /// <returns>
        /// A string containing the start of the INSERT INTO clause of a SQL statement.
        /// </returns>
        string RenderInsertStart();
        /// <summary>
        /// Renders the VALUES portion for an insert statement
        /// </summary>
        /// <returns>
        /// A string containing the start of the VALUES clause of a SQL insert statement.
        /// </returns>
        string RenderInsertValues();
        /// <summary>
        /// Renders the ON keyword for a JOIN clause.
        /// </summary>
        /// <returns>
        /// A string containing the ON keyword for the sub-clause of a join statement.
        /// </returns>
        string RenderJoinOn();
        /// <summary>
        /// Renders the LEFT JOIN keywords for a SQL statement.
        /// </summary>
        /// <returns>
        /// A string containing the start of the LEFT JOIN clause of a SQL statement.
        /// </returns>
        string RenderLeftJoin();
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
        string RenderSelect(bool distinct, int topRecordsCount);
        /// <summary>
        /// Renders the SET keyword.
        /// </summary>
        /// <returns>
        /// A string containing the rendered SQL.
        /// </returns>
        string RenderSet();
        /// <summary>
        /// Renders the stored procedure body start keyword.
        /// </summary>
        /// <returns>
        /// A string containing the stored procedure body start keyword.
        /// </returns>
        string RenderSpBodyStart();
        /// <summary>
        /// Renders the SQL comparison operator.
        /// </summary>
        /// <param name="sqlOperator">
        /// A <see cref="SqlComparisonOperator"/> enumerated value indicating the operator.
        /// </param>
        /// <returns>
        /// A string containing the rendering of the operator.
        /// </returns>
        string RenderSqlComparisonOperator(SqlComparisonOperator sqlOperator);
        /// <summary>
        /// Renders the SQL conditional operator.
        /// </summary>
        /// <param name="sqlOperator">
        /// A <see cref="SqlConditionOperator"/> enumerated value indicating the operator.
        /// </param>
        /// <returns>
        /// A string containing the rendering of the operator.
        /// </returns>
        string RenderSqlConditionOperator(SqlConditionOperator sqlOperator);
        /// <summary>
        /// Renders the UPDATE keyword.
        /// </summary>
        /// <returns>
        /// A string containing the rendered SQL.
        /// </returns>
        string RenderUpdate();
        /// <summary>
        /// Renders the start of a WHERE clause of a SQL statement.
        /// </summary>
        /// <returns>
        /// A string containing the start of the WHERE clause of a SQL statement.
        /// </returns>
        string RenderWhere();
        #endregion
    }

}
