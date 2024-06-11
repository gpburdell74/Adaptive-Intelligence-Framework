using Microsoft.SqlServer.Management.Smo;

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
        /// The date time format for the US.
        /// </summary>
        public const string DateTimeFormatUS = "MM/dd/yyyy hh:mm:ss tt";
        /// <summary>
        /// The SQL execution failed value.
        /// </summary>
        public const int ExecuteFailed = -1;
        /// <summary>
        /// A SQL BIT value of true as a number / string.
        /// </summary>
        public const string BooleanTrueNumber = "1";
        /// <summary>
        /// A SQL BIT value of false as a number / string.
        /// </summary>
        public const string BooleanFalseNumber = "0";
        /// <summary>
        /// The SQL wildcard character.
        /// </summary>
        public const string Wildcard = "*";
        #endregion

        #region T-SQL Data Type Names
        /// <summary>
        /// IMAGE data type.
        /// </summary>
        public const string SqlDataTypeImage = "IMAGE";
        /// <summary>
        /// TEXT data type.
        /// </summary>
        public const string SqlDataTypeText = "TEXT";
        /// <summary>
        /// UNIQUEIDENTIFIER data type.
        /// </summary>
        public const string SqlDataTypeUniqueIdentifier = "UNIQUEIDENTIFIER";
        /// <summary>
        /// DATE data type.
        /// </summary>
        public const string SqlDataTypeDate = "DATE";
        /// <summary>
        /// TIME data type.
        /// </summary>
        public const string SqlDataTypeTime = "TIME";
        /// <summary>
        /// DATETIME2 data type.
        /// </summary>
        public const string SqlDataTypeDateTime2 = "DATETIME2";
        /// <summary>
        /// DATETIMEOFFSET data type.
        /// </summary>
        public const string SqlDataTypeDateTimeOffset = "DATETIMEOFFSET";
        /// <summary>
        /// TINYINT data type.
        /// </summary>
        public const string SqlDataTypeTinyInt = "TINYINT";
        /// <summary>
        /// SMALLINT data type.
        /// </summary>
        public const string SqlDataTypeSmallInt = "SMALLINT";
        /// <summary>
        /// INT data type.
        /// </summary>
        public const string SqlDataTypeInt = "INT";
        /// <summary>
        /// SMALLDATETIME data type.
        /// </summary>
        public const string SqlDataTypeSmallDateTime = "SMALLDATETIME";
        /// <summary>
        /// REAL data type.
        /// </summary>
        public const string SqlDataTypeReal = "REAL";
        /// <summary>
        /// MONEY data type.
        /// </summary>
        public const string SqlDataTypeMoney = "MONEY";
        /// <summary>
        /// DATETIME data type.
        /// </summary>
        public const string SqlDataTypeDateTime = "DATETIME";
        /// <summary>
        /// FLOAT data type.
        /// </summary>
        public const string SqlDataTypeFloat = "FLOAT";
        /// <summary>
        /// SQLVARIANT data type.
        /// </summary>
        public const string SqlDataTypeSqlVariant = "SQLVARIANT";
        /// <summary>
        /// NTEXT data type.
        /// </summary>
        public const string SqlDataTypeNText = "NTEXT";
        /// <summary>
        /// BIT data type.
        /// </summary>
        public const string SqlDataTypeBit = "BIT";
        /// <summary>
        /// DECIMAL data type.
        /// </summary>
        public const string SqlDataTypeDecimal = "DECIMAL";
        /// <summary>
        /// NUMERIC data type.
        /// </summary>
        public const string SqlDataTypeNumeric = "NUMERIC";
        /// <summary>
        /// SMALLMONEY data type.
        /// </summary>
        public const string SqlDataTypeSmallMoney = "SMALLMONEY";
        /// <summary>
        /// BIGINT data type.
        /// </summary>
        public const string SqlDataTypeBigInt = "BIGINT";
        /// <summary>
        /// VARBINARY data type.
        /// </summary>
        public const string SqlDataTypeVarBinary = "VARBINARY";
        /// <summary>
        /// VARCHAR data type.
        /// </summary>
        public const string SqlDataTypeVarChar = "VARCHAR";
        /// <summary>
        /// BINARY data type.
        /// </summary>
        public const string SqlDataTypeBinary = "BINARY";
        /// <summary>
        /// CHAR data type.
        /// </summary>
        public const string SqlDataTypeChar = "CHAR";
        /// <summary>
        /// TIMESTAMP data type.
        /// </summary>
        public const string SqlDataTypeTimeStamp = "TIMESTAMP";
        /// <summary>
        /// NVARCHAR data type.
        /// </summary>
        public const string SqlDataTypeNVarCharOrSysName = "NVARCHAR";
        /// <summary>
        /// NCHAR data type.
        /// </summary>
        public const string SqlDataTypeNChar = "NCHAR";
        /// <summary>
        /// SPATIALTYPE data type.
        /// </summary>
        public const string SqlDataTypeSpatialType = "SPATIALTYPE";
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
        /// The SQL comma delimiter.
        /// </summary>
        public const string SqlComma = ",";
        /// <summary>
        /// The SQL single quote character.
        /// </summary>
        public const string SqlSingleQuote = "'";
        /// <summary>
        /// The escaped SQL single quote character.
        /// </summary>
        public const string SqlSingleQuoteEscaped = "''";
        /// <summary>
        /// The SQL close parenthesis delimiter
        /// </summary>
        public const string SqlCloseParenthesisDelimiter = ")";
        /// <summary>
        /// The SQL comment line delimiter.
        /// </summary>
        public const string SqlCommentLineDelimiter = "-- ";
        /// <summary>
        /// The SQL comment block end text.
        /// </summary>
        public const string SqlCommentBlockEnd = " */";
        /// <summary>
        /// The SQL comment block start text.
        /// </summary>
        public const string SqlCommentBlockStart = "/* ";
        /// <summary>
        /// The SQL comment block line prefix text.
        /// </summary>
        public const string SqlCommentBlockPrefix = " * ";
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
        /// The opening object name delimiter string.
        /// </summary>
        public const string SqlStartObjectDelimiter = "[";
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
        /// ANSI NULLS keyword.
        /// </summary>
        public const string SqlAnsiNulls = "ANSI_NULLS";
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
        /// The default database owner value.
        /// </summary>
        public const string DefaultDbOwner = "dbo";
        /// <summary>
        /// The standard Azure ID column name.
        /// </summary>
        public const string StandardColumnId = "Id";
        /// <summary>
        /// The standard Azure CreatedAt column name.
        /// </summary>
        public const string StandardColumnCreatedAt = "CreatedAt";
        /// <summary>
        /// The standard Azure UpdatedAt column name.
        /// </summary>
        public const string StandardColumnUpdatedAt = "UpdatedAt";
        /// <summary>
        /// The standard Azure Deleted column name.
        /// </summary>
        public const string StandardColumnDeleted = "Deleted";
        /// <summary>
        /// The standard Azure Version column name.
        /// </summary>
        public const string StandardColumnVersion = "Version";
        /// <summary>
        /// The standard Azure ID parameter name. (@Id)
        /// </summary>
        public const string StandardParameterId = "Id";
        /// <summary>
        /// The bit/boolean true value as a string.
        /// </summary>
        public const string BitValueTrue = "1";
        /// <summary>
        /// The bit/boolean false value as a string.
        /// </summary>
        public const string BitValueFalse = "0";

        #endregion

        #region Database Maintenance Queries
        /// <summary>
        /// A basic query to find the fragmented indexes in the database.
        /// </summary>
        public const string FragmentedIndexQuery =
            "SELECT " +
            "   [database_id], " +
            "   [object_id], " +
            "   [index_id], " +
            "   [avg_fragmentation_in_percent], " +
            "   [fragment_count], " +
            "   [page_count] " +
            "FROM " +
            "   sys.dm_db_index_physical_stats(DB_ID(), @ObjectId, NULL, NULL, NULL) ";

        /// <summary>
        /// A basic query to read the database schema.
        /// </summary>
        public const string TSqlBasicSchemaQuery =
            "SELECT " +
            "   DB_ID()          [id], " +
            "	DB_NAME(DB_ID()) [name] " +
            " " +
            "SELECT " +
            "   [tables].[name], " +
            "	[tables].[object_id] " +
            "FROM " +
            "   [sys].[tables] " +
            "WHERE " +
            "   [tables].[type] = 'U' OR " +
            "   [tables].[type] = 'IT' " +
            "ORDER BY " +
            "    [tables].[name] " +
            " " +
            "SELECT " +
            "   [indexes].[object_id], " +
            "   [indexes].[index_id], " +
            "   [indexes].[type], " +
            "   [indexes].[name], " +
            "   [indexes].[is_primary_key] " +
            "FROM " +
            "   [sys].[indexes] " +
            "       INNER JOIN[sys].[tables] " +
            "           ON [indexes].[object_id] = [tables].[object_id] " +
            "WHERE " +
            "           ([tables].[type] = 'U' OR[tables].[type] = 'IT') " +
            "    AND    [indexes].[type] > 0 " +
            "ORDER BY " +
            "   [indexes].[object_id], " +
            "	[indexes].[index_id] ";
        #endregion

        /// <summary>
        /// The SQL update statistics command.
        /// </summary>
        public const string SqlUpdateStats = "UPDATE STATISTICS [{0}]";
        /// <summary>
        /// The SQL recompile command.
        /// </summary>
        public const string SqlRecompile = "exec sp_recompile [{0}]";
        /// <summary>
        /// The SQL parameter object identifier parameter name.
        /// </summary>
        public const string SqlParamObjectId = "@ObjectId";

        /// <summary>
        /// The SQL query to get database names.
        /// </summary>
        public const string SqlGetDbNamesQuery = "SELECT [name] FROM sys.databases ORDER BY [name]";

    }
}
