using Adaptive.Intelligence.Shared;
using System.Text;
using Adaptive.Intelligence.SqlServer.Schema;
namespace Adaptive.Intelligence.SqlServer.CodeDom.CodeProvider
{
    /// <summary>
    /// Provides a mechanism for generating Transact SQL (T-SQL) code for SQL Server Databases.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class TransactSqlCodeProvider : DisposableObjectBase, ISqlCodeProvider
	{
		#region Public Properties
		/// <summary>
		/// Gets the assignment operator representation.
		/// </summary>
		/// <value>
		/// A string containing the assignment operator.
		/// </value>
		public string AssignmentOperator => TSqlConstants.SqlAssignmentOperator;
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
		public string CloseParenthesis => TSqlConstants.SqlCloseParenthesisDelimiter;
		/// <summary>
		/// Gets the closing/ending delimiter string for object names.
		/// </summary>
		/// <value>
		/// A string specifying the delimiter to use.
		/// </value>
		public string ObjectNameEndDelimiter => TSqlConstants.SqlEndObjectDelimiter;
		/// <summary>
		/// Gets the opening/starting delimiter string for object names.
		/// </summary>
		/// <value>
		/// A string specifying the delimiter to use.
		/// </value>
		public string ObjectNameStartDelimiter => TSqlConstants.SqlStartObjectDelimiter;
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
		public string OpenParenthesis => TSqlConstants.SqlOpenParenthesisDelimiter;
		/// <summary>
		/// Gets the variable or parameter name prefix.
		/// </summary>
		/// <value>
		/// A string specifying the delimiter prefix to use that indicates a variable or parameter name.
		/// </value>
		public string ParameterNamePrefix => TSqlConstants.SqlParameterPrefix;
		/// <summary>
		/// Gets the string used to delimit the items in a SELECT clause.
		/// </summary>
		/// <value>
		/// A string specifying the delimiter to use.
		/// </value>
		public string SelectDelimiter => TSqlConstants.SqlComma;
		/// <summary>
		/// Gets the string used to indicate a comment.
		/// </summary>
		/// <value>
		/// A string specifying the delimiter to use.
		/// </value>
		public string SqlCommentLineDelimiter => TSqlConstants.SqlCommentLineDelimiter;
		#endregion

		#region Public Methods / Functions
		/// <summary>
		/// Renders the edit, alter, or modify stored procedure open statement line.
		/// </summary>
		/// <example>
		/// ALTER PROCEDURE [dbo].[procedure name]
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
		public string RenderAlterProcedureOpenStatement(SqlCodeDatabaseNameOwnerNameExpression? owner, string name)
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(TSqlConstants.SqlAlterProcedure + " ");
			if (owner != null)
				builder.Append(TSqlConstants.SqlStartObjectDelimiter + owner.Name + TSqlConstants.SqlEndObjectDelimiter + ".");

			builder.Append(TSqlConstants.SqlStartObjectDelimiter + name + TSqlConstants.SqlEndObjectDelimiter + ".");
			return builder.ToString();
		}
		/// <summary>
		/// Renders the code block body end keyword, for procedures or conditional blocks.
		/// </summary>
		/// <returns>
		/// A string containing the block body end keyword.
		/// </returns>
		public string RenderBlockBodyEnd()
		{
			return TSqlConstants.SqlEnd;
		}
		/// <summary>
		/// Renders the code block body start keyword, for procedures or conditional blocks.
		/// </summary>
		/// <returns>
		/// A string containing the block body start keyword.
		/// </returns>
		public string RenderBlockBodyStart()
		{
			return TSqlConstants.SqlBegin;
		}
		/// <summary>
		/// Renders the SQL text indicating the start of a comment block.
		/// </summary>
		/// <returns>
		/// A string containing the rendered text.
		/// </returns>
		public string RenderCommentBlockEnd()
		{
			return TSqlConstants.SqlCommentBlockEnd;
		}
		/// <summary>
		/// Renders the SQL text indicating the text used at the start of each comment text line.
		/// </summary>
		/// <returns>
		/// A string containing the rendered text.
		/// </returns>
		public string RenderCommentBlockPrefix()
		{
			return TSqlConstants.SqlCommentBlockPrefix;
		}
		/// <summary>
		/// Renders the SQL text indicating the end of a comment block.
		/// </summary>
		/// <returns>
		/// A string containing the rendered text.
		/// </returns>
		public string RenderCommentBlockStart()
		{
			return TSqlConstants.SqlCommentBlockStart;
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
		public string RenderCreateProcedureOpenStatement(SqlCodeDatabaseNameOwnerNameExpression? owner, string name)
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(TSqlConstants.SqlCreateProcedure + " ");
			if (owner != null)
				builder.Append(TSqlConstants.SqlStartObjectDelimiter + owner.Name + TSqlConstants.SqlEndObjectDelimiter + ".");

			builder.Append(TSqlConstants.SqlStartObjectDelimiter + name + TSqlConstants.SqlEndObjectDelimiter);
			return builder.ToString();
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
		public string RenderDataTypeName(SqlDataTypes sqlDataType)
		{
			string returnValue = string.Empty;

			switch (sqlDataType)
			{
				case SqlDataTypes.BigInt:
					returnValue = TSqlConstants.SqlDataTypeBigInt;
					break;

				case SqlDataTypes.Binary:
					returnValue = TSqlConstants.SqlDataTypeBinary;
					break;

				case SqlDataTypes.Bit:
					returnValue = TSqlConstants.SqlDataTypeBit;
					break;

				case SqlDataTypes.Char:
					returnValue = TSqlConstants.SqlDataTypeChar;
					break;

				case SqlDataTypes.Date:
					returnValue = TSqlConstants.SqlDataTypeDate;
					break;

				case SqlDataTypes.DateTime:
					returnValue = TSqlConstants.SqlDataTypeDateTime;
					break;

				case SqlDataTypes.DateTime2:
					returnValue = TSqlConstants.SqlDataTypeDateTime2;
					break;

				case SqlDataTypes.DateTimeOffset:
					returnValue = TSqlConstants.SqlDataTypeDateTimeOffset;
					break;

				case SqlDataTypes.Decimal:
					returnValue = TSqlConstants.SqlDataTypeDecimal;
					break;

				case SqlDataTypes.Float:
					returnValue = TSqlConstants.SqlDataTypeFloat;
					break;

				case SqlDataTypes.Image:
					returnValue = TSqlConstants.SqlDataTypeImage;
					break;

				case SqlDataTypes.Int:
					returnValue = TSqlConstants.SqlDataTypeInt;
					break;

				case SqlDataTypes.Money:
					returnValue = TSqlConstants.SqlDataTypeMoney;
					break;

				case SqlDataTypes.NChar:
					returnValue = TSqlConstants.SqlDataTypeNChar;
					break;

				case SqlDataTypes.NText:
					returnValue = TSqlConstants.SqlDataTypeNText;
					break;

				case SqlDataTypes.Numeric:
					returnValue = TSqlConstants.SqlDataTypeNumeric;
					break;

				case SqlDataTypes.NVarCharOrSysName:
					returnValue = TSqlConstants.SqlDataTypeNVarCharOrSysName;
					break;

				case SqlDataTypes.Real:
					returnValue = TSqlConstants.SqlDataTypeReal;
					break;

				case SqlDataTypes.SmallDateTime:
					returnValue = TSqlConstants.SqlDataTypeSmallDateTime;
					break;

				case SqlDataTypes.SmallInt:
					returnValue = TSqlConstants.SqlDataTypeSmallInt;
					break;

				case SqlDataTypes.SmallMoney:
					returnValue = TSqlConstants.SqlDataTypeSmallMoney;
					break;

				case SqlDataTypes.SpatialType:
					returnValue = TSqlConstants.SqlDataTypeSpatialType;
					break;

				case SqlDataTypes.SqlVariant:
					returnValue = TSqlConstants.SqlDataTypeSqlVariant;
					break;

				case SqlDataTypes.Text:
					returnValue = TSqlConstants.SqlDataTypeText;
					break;

				case SqlDataTypes.Time:
					returnValue = TSqlConstants.SqlDataTypeTime;
					break;

				case SqlDataTypes.TimeStamp:
					returnValue = TSqlConstants.SqlDataTypeTimeStamp;
					break;

				case SqlDataTypes.TinyInt:
					returnValue = TSqlConstants.SqlDataTypeTinyInt;
					break;

				case SqlDataTypes.UniqueIdentifier:
					returnValue = TSqlConstants.SqlDataTypeUniqueIdentifier;
					break;

				case SqlDataTypes.VarBinary:
					returnValue = TSqlConstants.SqlDataTypeVarBinary;
					break;

				case SqlDataTypes.VarChar:
					returnValue = TSqlConstants.SqlDataTypeVarChar;
					break;

				case SqlDataTypes.Xml:
					returnValue = TSqlConstants.SqlDataTypeXml;
					break;
			}

			return returnValue;
		}
		/// <summary>
		/// Renders the DECLARE keyword when defining a variable or memory table type.
		/// </summary>
		/// <returns>
		/// A string containing the rendered keyword.
		/// </returns>
		public string RenderDeclare()
		{
			return TSqlConstants.SqlDeclare;
		}
		/// <summary>
		/// Renders the start of a FROM clause of a SQL statement.
		/// </summary>
		/// <returns>
		/// A string containing the start of the FROM clause of a SQL statement.
		/// </returns>
		public string RenderFrom()
		{
			return TSqlConstants.SqlFrom;
		}
		/// <summary>
		/// Renders the INNER JOIN keywords for a SQL statement.
		/// </summary>
		/// <returns>
		/// A string containing the start of the INNER JOIN clause of a SQL statement.
		/// </returns>
		public string RenderInnerJoin()
		{
			return TSqlConstants.SqlInnerJoin;
		}
		/// <summary>
		/// Renders the INSERT INTO [table] portion for an insert statement
		/// </summary>
		/// <returns>
		/// A string containing the start of the INSERT INTO clause of a SQL statement.
		/// </returns>
		public string RenderInsertStart()
		{
			return TSqlConstants.SqlInsert;
		}
		/// <summary>
		/// Renders the VALUES portion for an insert statement
		/// </summary>
		/// <returns>
		/// A string containing the start of the VALUES clause of a SQL insert statement.
		/// </returns>
		public string RenderInsertValues()
		{
			return TSqlConstants.SqlValues;
		}
		/// <summary>
		/// Renders the ON keyword for a JOIN clause.
		/// </summary>
		/// <returns>
		/// A string containing the ON keyword for the sub-clause of a join statement.
		/// </returns>
		public string RenderJoinOn()
		{
			return TSqlConstants.SqlOn;
		}
		/// <summary>
		/// Renders the LEFT JOIN keywords for a SQL statement.
		/// </summary>
		/// <returns>
		/// A string containing the start of the LEFT JOIN clause of a SQL statement.
		/// </returns>
		public string RenderLeftJoin()
		{
			return TSqlConstants.SqlLeftJoin;
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
		public string RenderSelect(bool distinct, int topRecordsCount)
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(TSqlConstants.SqlSelect);
			if (distinct)
				builder.Append(" " + TSqlConstants.SqlDistinct);
			else if (topRecordsCount > 0)
				builder.Append(" " + TSqlConstants.SqlTop + " " + topRecordsCount.ToString());

			return builder.ToString();
		}
		/// <summary>
		/// Renders the SET keyword.
		/// </summary>
		/// <returns>
		/// A string containing the rendered SQL.
		/// </returns>
		public string RenderSet()
		{
			return TSqlConstants.SqlSet;
		}
		/// <summary>
		/// Renders the stored procedure body start keyword.
		/// </summary>
		/// <returns>
		/// A string containing the stored procedure body start keyword.
		/// </returns>
		public string RenderSpBodyStart()
		{
			return TSqlConstants.SqlAs;
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
		public string RenderSqlComparisonOperator(SqlComparisonOperator sqlOperator)
		{
			string returnValue = string.Empty;

			switch (sqlOperator)
			{
				case SqlComparisonOperator.EqualTo:
					returnValue = TSqlConstants.SqlOperatorEqualTo;
					break;

				case SqlComparisonOperator.GreaterThan:
					returnValue = TSqlConstants.SqlOperatorGreaterThan;
					break;

				case SqlComparisonOperator.GreaterThanOrEqualTo:
					returnValue = TSqlConstants.SqlOperatorGreaterThanOrEqualTo;
					break;

				case SqlComparisonOperator.IsNotNull:
					returnValue = TSqlConstants.SqlOperatorIsNotNull;
					break;

				case SqlComparisonOperator.IsNull:
					returnValue = TSqlConstants.SqlOperatorIsNull;
					break;

				case SqlComparisonOperator.LessThan:
					returnValue = TSqlConstants.SqlOperatorLessThan;
					break;

				case SqlComparisonOperator.LessThanOrEqualTo:
					returnValue = TSqlConstants.SqlOperatorLessThanOrEqualTo;
					break;

				case SqlComparisonOperator.Not:
					returnValue = TSqlConstants.SqlOperatorNot;
					break;

				case SqlComparisonOperator.NotEqualTo:
					returnValue = TSqlConstants.SqlOperatorNotEqualTo;
					break;

				case SqlComparisonOperator.NotIn:
					returnValue = TSqlConstants.SqlOperatorNotIn;
					break;
			}

			return returnValue;
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
		public string RenderSqlConditionOperator(SqlConditionOperator sqlOperator)
		{
			string returnValue = string.Empty;

			switch (sqlOperator)
			{
				case SqlConditionOperator.And:
					returnValue = TSqlConstants.SqlAnd;
					break;

				case SqlConditionOperator.Or:
					returnValue = TSqlConstants.SqlOr;
					break;
			}
			return returnValue;
		}
		/// <summary>
		/// Renders the UPDATE keyword.
		/// </summary>
		/// <returns>
		/// A string containing the rendered SQL.
		/// </returns>
		public string RenderUpdate()
		{
			return TSqlConstants.SqlUpdate;
		}
		/// <summary>
		/// Renders the start of a WHERE clause of a SQL statement.
		/// </summary>
		/// <returns>
		/// A string containing the start of the WHERE clause of a SQL statement.
		/// </returns>
		public string RenderWhere()
		{
			return TSqlConstants.SqlWhere;
		}

		#endregion
	}
}
