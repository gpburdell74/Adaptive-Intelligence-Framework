// Ignore Spelling: Sql
// Ignore Spelling: dbo

using Adaptive.Intelligence.SqlServer.Properties;

namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Contains the static definitions used in rendering T-SQL statements and portions
    /// of T-SQL code.
    /// </summary>
    public static class TSqlConstants
    {
        #region General Purpose
        /// <summary>
        /// A SQL BIT value of false as a number / string.
        /// </summary>
        public const string BooleanFalseNumber = "0";
        /// <summary>
        /// A SQL BIT value of true as a number / string.
        /// </summary>
        public const string BooleanTrueNumber = "1";
        /// <summary>
        /// The SQL execution failed value.
        /// </summary>
        public const int ExecuteFailed = -1;
        /// <summary>
        /// The SQL wild card character.
        /// </summary>
        public const string WildCard = "*";
        /// <summary>
        /// The date time format for the US.
        /// </summary>
        public static string DateTimeFormatUS = Resources.DateTimeFormat;
        #endregion

        #region T-SQL Data Type Names
        /// <summary>
        /// BIGINT data type.
        /// </summary>
        public const string SqlDataTypeBigInt = "BIGINT";
        /// <summary>
        /// BINARY data type.
        /// </summary>
        public const string SqlDataTypeBinary = "BINARY";
        /// <summary>
        /// BIT data type.
        /// </summary>
        public const string SqlDataTypeBit = "BIT";
        /// <summary>
        /// CHAR data type.
        /// </summary>
        public const string SqlDataTypeChar = "CHAR";
        /// <summary>
        /// DATE data type.
        /// </summary>
        public const string SqlDataTypeDate = "DATE";
        /// <summary>
        /// DATETIME data type.
        /// </summary>
        public const string SqlDataTypeDateTime = "DATETIME";
        /// <summary>
        /// DATETIME2 data type.
        /// </summary>
        public const string SqlDataTypeDateTime2 = "DATETIME2";
        /// <summary>
        /// DATETIMEOFFSET data type.
        /// </summary>
        public const string SqlDataTypeDateTimeOffset = "DATETIMEOFFSET";
        /// <summary>
        /// DECIMAL data type.
        /// </summary>
        public const string SqlDataTypeDecimal = "DECIMAL";
        /// <summary>
        /// FLOAT data type.
        /// </summary>
        public const string SqlDataTypeFloat = "FLOAT";
        /// <summary>
        /// IMAGE data type.
        /// </summary>
        public const string SqlDataTypeImage = "IMAGE";
        /// <summary>
        /// INT data type.
        /// </summary>
        public const string SqlDataTypeInt = "INT";
        /// <summary>
        /// MONEY data type.
        /// </summary>
        public const string SqlDataTypeMoney = "MONEY";
        /// <summary>
        /// NCHAR data type.
        /// </summary>
        public const string SqlDataTypeNChar = "NCHAR";
        /// <summary>
        /// NTEXT data type.
        /// </summary>
        public const string SqlDataTypeNText = "NTEXT";
        /// <summary>
        /// NUMERIC data type.
        /// </summary>
        public const string SqlDataTypeNumeric = "NUMERIC";
        /// <summary>
        /// NVARCHAR data type.
        /// </summary>
        public const string SqlDataTypeNVarCharOrSysName = "NVARCHAR";
        /// <summary>
        /// REAL data type.
        /// </summary>
        public const string SqlDataTypeReal = "REAL";
        /// <summary>
        /// SMALLDATETIME data type.
        /// </summary>
        public const string SqlDataTypeSmallDateTime = "SMALLDATETIME";
        /// <summary>
        /// SMALLINT data type.
        /// </summary>
        public const string SqlDataTypeSmallInt = "SMALLINT";
        /// <summary>
        /// SMALLMONEY data type.
        /// </summary>
        public const string SqlDataTypeSmallMoney = "SMALLMONEY";
        /// <summary>
        /// SPATIALTYPE data type.
        /// </summary>
        public const string SqlDataTypeSpatialType = "SPATIALTYPE";
        /// <summary>
        /// SQLVARIANT data type.
        /// </summary>
        public const string SqlDataTypeSqlVariant = "SQLVARIANT";
        /// <summary>
        /// TEXT data type.
        /// </summary>
        public const string SqlDataTypeText = "TEXT";
        /// <summary>
        /// TIME data type.
        /// </summary>
        public const string SqlDataTypeTime = "TIME";
        /// <summary>
        /// TIMESTAMP data type.
        /// </summary>
        public const string SqlDataTypeTimeStamp = "TIMESTAMP";
        /// <summary>
        /// TINYINT data type.
        /// </summary>
        public const string SqlDataTypeTinyInt = "TINYINT";
        /// <summary>
        /// UNIQUEIDENTIFIER data type.
        /// </summary>
        public const string SqlDataTypeUniqueIdentifier = "UNIQUEIDENTIFIER";
        /// <summary>
        /// VARBINARY data type.
        /// </summary>
        public const string SqlDataTypeVarBinary = "VARBINARY";
        /// <summary>
        /// VARCHAR data type.
        /// </summary>
        public const string SqlDataTypeVarChar = "VARCHAR";
        /// <summary>
        /// XML data type.
        /// </summary>
        public const string SqlDataTypeXml = "XML";
        #endregion

        #region T-SQL Delimiters As Characters
        /// <summary>
        /// The SQL comma delimiter.
        /// </summary>
        public const char SqlCommaChar = ',';
        /// <summary>
        /// The SQL single quote character.
        /// </summary>
        public const char SqlSingleQuoteChar = '\'';
        /// <summary>
        /// The SQL statement termination character.
        /// </summary>
        public const char SqlTerminationCharacter = ';';
        #endregion

        #region T-SQL Delimiters
        /// <summary>
        /// The SQL close parenthesis delimiter
        /// </summary>
        public const string SqlCloseParenthesisDelimiter = ")";
        /// <summary>
        /// The SQL comma delimiter.
        /// </summary>
        public const string SqlComma = ",";
        /// <summary>
        /// The SQL comment block end text.
        /// </summary>
        public const string SqlCommentBlockEnd = " */";
        /// <summary>
        /// The SQL comment block line prefix text.
        /// </summary>
        public const string SqlCommentBlockPrefix = " * ";
        /// <summary>
        /// The SQL comment block start text.
        /// </summary>
        public const string SqlCommentBlockStart = "/* ";
        /// <summary>
        /// The SQL comment line delimiter.
        /// </summary>
        public const string SqlCommentLineDelimiter = "-- ";
        /// <summary>
        /// The closing object name delimiter string.
        /// </summary>
        public const string SqlEndObjectDelimiter = "]";
        /// <summary>
        /// The SQL open parenthesis delimiter
        /// </summary>
        public const string SqlOpenParenthesisDelimiter = "(";
        /// <summary>
        /// The SQL parameter or variable name prefix.
        /// </summary>
        public const string SqlParameterPrefix = "@";
        /// <summary>
        /// The SQL single quote character.
        /// </summary>
        public const string SqlSingleQuote = "'";
        /// <summary>
        /// The escaped SQL single quote character.
        /// </summary>
        public const string SqlSingleQuoteEscaped = "''";
        /// <summary>
        /// The opening object name delimiter string.
        /// </summary>
        public const string SqlStartObjectDelimiter = "[";
        /// <summary>
        /// The SQL object delimiter.
        /// </summary>
        public const string SqlObjectDelimiter = ".";
        #endregion

        #region T-SQL Names
        /// <summary>
        /// The default database owner object name.
        /// </summary>
        public const string DefaultDatabaseOwner = "dbo";
        /// <summary>
        /// The SQL NULL keyword.
        /// </summary>
        public const string SqlNull = "NULL";
        #endregion

        #region T-SQL KeyWords
        /// <summary>
        /// ALTER keyword.
        /// </summary>
        public const string SqlAlter = "ALTER";
        /// <summary>
        /// ALTER PROCEDURE keyword.
        /// </summary>
        public const string SqlAlterProcedure = "ALTER PROCEDURE";
        /// <summary>
        /// AND keyword.
        /// </summary>
        public const string SqlAnd = "AND";
        /// <summary>
        /// ANSI NULLS keyword.
        /// </summary>
        public const string SqlAnsiNulls = "ANSI_NULLS";
        /// <summary>
        /// AS keyword.
        /// </summary>
        public const string SqlAs = "AS";
        /// <summary>
        /// BEGIN keyword.
        /// </summary>
        public const string SqlBegin = "BEGIN";
        /// <summary>
        /// CREATE keyword.
        /// </summary>
        public const string SqlCreate = "CREATE";
        /// <summary>
        /// CREATE PROCEDURE keyword.
        /// </summary>
        public const string SqlCreateProcedure = "CREATE PROCEDURE";
        /// <summary>
        /// DECLARE keyword.
        /// </summary>
        public const string SqlDeclare = "DECLARE";
        /// <summary>
        /// DELETE keyword.
        /// </summary>
        public const string SqlDelete = "DELETE";
        /// <summary>
        /// DISTINCT keyword.
        /// </summary>
        public const string SqlDistinct = "DISTINCT";
        /// <summary>
        /// END keyword.
        /// </summary>
        public const string SqlEnd = "END";
        /// <summary>
        /// FROM keyword.
        /// </summary>
        public const string SqlFrom = "FROM";
        /// <summary>
        /// INNER keyword.
        /// </summary>
        public const string SqlInner = "INNER";
        /// <summary>
        /// INNER JOIN keyword.
        /// </summary>
        public const string SqlInnerJoin = "INNER JOIN";
        /// <summary>
        /// INSERT INTO keyword.
        /// </summary>
        public const string SqlInsert = "INSERT INTO";
        /// <summary>
        /// INTO keyword.
        /// </summary>
        public const string SqlInto = "INTO";
        /// <summary>
        /// JOIN keyword.
        /// </summary>
        public const string SqlJoin = "JOIN";
        /// <summary>
        /// LEFT keyword.
        /// </summary>
        public const string SqlLeft = "LEFT";
        /// <summary>
        /// LEFT JOIN keyword.
        /// </summary>
        public const string SqlLeftJoin = "LEFT JOIN";
        /// <summary>
        /// ON keyword.
        /// </summary>
        public const string SqlOn = "ON";
        /// <summary>
        /// OR keyword.
        /// </summary>
        public const string SqlOr = "OR";
        /// <summary>
        /// PRIMARY keyword.
        /// </summary>
        public const string SqlPrimary = "PRIMARY";
        /// <summary>
        /// PROCEDURE keyword.
        /// </summary>
        public const string SqlProcedure = "PROCEDURE";
        /// <summary>
        /// $ROWGUID keyword.
        /// </summary>
        public const string SqlRowGuid = "$ROWGUID";
        /// <summary>
        /// SELECT keyword.
        /// </summary>
        public const string SqlSelect = "SELECT";
        /// <summary>
        /// SET keyword.
        /// </summary>
        public const string SqlSet = "SET";
        /// <summary>
        /// TOP keyword.
        /// </summary>
        public const string SqlTop = "TOP";
        /// <summary>
        /// UPDATE keyword.
        /// </summary>
        public const string SqlUpdate = "UPDATE";
        /// <summary>
        /// VALUES keyword.
        /// </summary>
        public const string SqlValues = "VALUES";
        /// <summary>
        /// WHERE keyword.
        /// </summary>
        public const string SqlWhere = "WHERE";
        #endregion

        #region T-SQL Operators
        /// <summary>
        /// The T-SQL assignment operator.
        /// </summary>
        public const string SqlAssignmentOperator = "=";
        /// <summary>
        /// The T-SQL equal to operator.
        /// </summary>
        public const string SqlOperatorEqualTo = "=";
        /// <summary>
        /// The T-SQL greater than operator.
        /// </summary>
        public const string SqlOperatorGreaterThan = ">";
        /// <summary>
        /// The T-SQL greater than or equal to operator.
        /// </summary>
        public const string SqlOperatorGreaterThanOrEqualTo = ">=";
        /// <summary>
        /// The T-SQL is not null operator.
        /// </summary>
        public const string SqlOperatorIsNotNull = "IS NOT NULL";
        /// <summary>
        /// The T-SQL is null operator.
        /// </summary>
        public const string SqlOperatorIsNull = "IS NULL";
        /// <summary>
        /// The T-SQL less than operator.
        /// </summary>
        public const string SqlOperatorLessThan = "<";
        /// <summary>
        /// The T-SQL less than or equal to operator.
        /// </summary>
        public const string SqlOperatorLessThanOrEqualTo = "<=";
        /// <summary>
        /// The T-SQL not operator.
        /// </summary>
        public const string SqlOperatorNot = "NOT";
        /// <summary>
        /// The T-SQL not equal to operator.
        /// </summary>
        public const string SqlOperatorNotEqualTo = "!=";
        /// <summary>
        /// The T-SQL not in operator.
        /// </summary>
        public const string SqlOperatorNotIn = "NOT IN";
        #endregion

        #region System Functions
        /// <summary>
        /// The SQL system function, NEWID
        /// </summary>
        public const string SqlSysFunctionNewId = "NEWID";
        /// <summary>
        /// The SQL system function, SYSUTCDATETIME
        /// </summary>
        public const string SqlSysFunctionSysUtcDateTime = "SYSUTCDATETIME";
        #endregion

        #region T-SQL / Adaptive Intelligence Values
        /// <summary>
        /// The bit/boolean false value as a string.
        /// </summary>
        public const string BitValueFalse = "0";
        /// <summary>
        /// The bit/boolean true value as a string.
        /// </summary>
        public const string BitValueTrue = "1";
        /// <summary>
        /// The standard (soft-delete) Deleted column name.
        /// </summary>
        public const string StandardColumnDeleted = "Deleted";
        /// <summary>
        /// The standard ID column name.
        /// </summary>
        public const string StandardColumnId = "Id";
        /// <summary>
        /// The standard Azure ID parameter name. (@Id)
        /// </summary>
        public const string StandardParameterId = "Id";
        #endregion

        #region Database Maintenance Queries
        /// <summary>
        /// The SQL parameter object identifier parameter name.
        /// </summary>
        public const string SqlParamId = "@Id";
        /// <summary>
        /// The SQL parameter object identifier parameter name.
        /// </summary>
        public const string SqlParamObjectId = "@ObjectId";
        /// <summary>
        /// A basic query to read the database schema.
        /// </summary>
        public static string SqlBasicSchemaQuery = Resources.TSqlBasicSchemaQuery;
        /// <summary>
        /// A basic query to find the fragmented indexes in the database.
        /// </summary>
        public static string SqlFragmentedIndexQuery = Resources.TSqlIndexFragmentationQuery;
        /// <summary>
        /// The SQL query to get all the database names.
        /// </summary>
        public static string SqlGetAllDbNamesQuery = Resources.TSqlGetAllDbNamesQuery;
        /// <summary>
        /// The SQL query to get names of the user (non-system) databases.
        /// </summary>
        public static string SqlGetDbNamesQuery = Resources.TSqlGetDbNamesQuery;
        /// <summary>
        /// The SQL recompile command.
        /// </summary>
        public static string SqlRecompile = Resources.TSqlUpdateStatisticsQuery;
        /// <summary>
        /// The SQL update statistics command.
        /// </summary>
        public static string SqlUpdateStats = Resources.TSqlUpdateStatisticsQuery;
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Wraps the specified name value in brackets ("[", "]").
        /// </summary>
        /// <param name="name">
        /// A string containing the name value.
        /// </param>
        /// <returns>
        /// A string containing the bracketed name, or an empty string if <i>name</i> is null or empty.
        /// </returns>
        public static string BracketName(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            else
            {
                return SqlStartObjectDelimiter + name + SqlEndObjectDelimiter;
            }
        }
        /// <summary>
        /// Combines the schema and table name into a single string.	
        /// </summary>
        /// <param name="schema">
        /// A string containing the schema name value, or <b>null</b>.
        /// </param>
        /// <param name="tableName">
        /// A string containing the name of the table, or <b>null</b>.
        /// Name of the table.</param>
        /// <returns>
        /// A string containing the bracketed schema and name of the table, such as: "[dbo].[Customers]".
        /// </returns>
        public static string RenderSchemaAndTableName(string? schema, string? tableName)
        {
            if (string.IsNullOrEmpty(schema) && string.IsNullOrEmpty(tableName))
            {
                return string.Empty;
            }
            else
            {
                string prefix = BracketName(schema);
                string suffix = BracketName(tableName);

                if (!string.IsNullOrEmpty(prefix) && !string.IsNullOrEmpty(suffix))
                {
                    return prefix + SqlObjectDelimiter + suffix;
                }

                else if (!string.IsNullOrEmpty(prefix))
                {
                    return prefix;
                }

                else
                {
                    return suffix;
                }
            }

        }
        #endregion

    }
}
