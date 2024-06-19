// Ignore Spelling: Sql

using Adaptive.Intelligence.SqlServer.Properties;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Provides the constants for standard SQL Server queries used to retrieve the schema information for a specified database.
    /// </summary>
    public static class SchemaQueryConstants
    {
        #region Get Foreign Keys Definition SQL        
        /// <summary>
        /// Contains the query to retrieve the list of defined foreign key definitions.
        /// </summary>
        public const string GetForeignKeysSql =
            "SELECT " +
            "	[ParentTables].[name]				parentTableName, " +
            "	[FKeys].[name]						foreignKeyName,	 " +
            "	[FKeys].[object_id]					foreignKeyObjectId, " +
            "	[FKeys].[parent_object_id]			parentTableObjectId, " +
            "	[ReferencedTables].[name]			referencedTableName, " +
            "	[FKeys].[referenced_object_id]		referencedTableObjectId, " +
            "	[FKeys].[key_index_id]				keyIndexId, " +
            "	[FKeyColumns].[constraint_object_id] constraintObjectId, " +
            "	[FKeyColumns].[constraint_column_id] constraintColumnId, " +
            "	[FKeyColumns].[parent_object_id]	 parentObjectId, " +
            "	[FKeyColumns].[parent_column_id]	 parentColumnId, " +
            "	[FKeyColumns].[referenced_object_id] referencedObjectId, " +
            "	[FKeyColumns].[referenced_column_id] referencedColumnId " +
            "FROM " +
            "	[sys].[foreign_keys] [FKeys] " +
            "		INNER JOIN sys.tables [ParentTables] " +
            "			ON	[FKeys].[parent_object_id] = [ParentTables].[object_id] AND " +
            "				[ParentTables].[type] = 'U' AND " +
            "				[ParentTables].[is_ms_shipped] = 0 " +
            "		INNER JOIN sys.tables [ReferencedTables] " +
            "			ON	[FKeys].[referenced_object_id] = [ReferencedTables].[object_id] AND " +
            "				[ReferencedTables].[type] = 'U' AND " +
            "				[ReferencedTables].[is_ms_shipped] = 0 " +
            "		INNER JOIN [sys].[foreign_key_columns] [FKeyColumns] " +
            "			ON	[FKeys].[object_id] = [FKeyColumns].[constraint_object_id]				 " +
            "ORDER BY " +
            "	[ParentTables].[name], " +
            "	[FKeyColumns].[parent_column_id] ";
        #endregion

        #region Get Index Definitions SQL
        /// <summary>
        /// Contains the query to retrieve the list of defined table indexes.
        /// </summary>
        public const string GetIndexesSql =
            "SELECT " +
            "	[Indexes].[object_id]		tableObjectId, " +
            "	[Tables].[name]				tableName, " +
            "	[Indexes].[name]			indexName, " +
            "	[Indexes].[index_id]		indexId, " +
            "	[Indexes].[type]			indexType, " +
            "	[Indexes].[type_desc]		indexTypeDesc, " +
            "	[Indexes].[is_unique]		isUnique, " +
            "	[Indexes].[is_primary_key]	isPrimaryKey, " +
            "	[Indexes].[is_unique_constraint]	isUniqueConstraint, " +
            "	[IndexColumns].[index_column_id]	indexColumnId, " +
            "	[IndexColumns].[column_id]			tableColumnId, " +
            "	[IndexColumns].[key_ordinal]		keyOrdinal, " +
            "	[IndexColumns].[is_descending_key]	isDescending, " +
            "	[IndexColumns].[is_included_column]	isIncluded " +
            "FROM " +
            "	sys.indexes [Indexes] " +
            "		INNER JOIN sys.tables [Tables] " +
            "			ON	[Indexes].[object_id] = [Tables].[object_id] AND " +
            "				[Tables].[type] = 'U' AND " +
            "				[Tables].[is_ms_shipped] = 0 " +
            "		INNER JOIN sys.index_columns [IndexColumns] " +
            "			ON [Indexes].[object_id] = [IndexColumns].[object_id] AND " +
            "			   [Indexes].[index_id] = [IndexColumns].[index_id] " +
            "ORDER BY " +
            "	[Tables].[name], " +
            "	[Indexes].[index_id], " +
            "	[IndexColumns].[index_column_id] ";
        #endregion

        #region Get SQL Data Types SQL
        /// <summary>
        /// Contains the query to retrieve the list of defined table indexes.
        /// </summary>
        public const string GetSqlDataTypesSql =
            "SELECT " +
            "   [Types].[name]              typeName, " +
            "   [Types].[system_type_id]    typeId, " +
            "	[Types].[max_length]        [maxLength], " +
            "	[Types].[precision]         [precision], " +
            "	[Types].[scale]             [scale], " +
            "	[Types].[is_nullable]       isNullable " +
            "FROM " +
            "   [sys].[types] " +
            "        [Types] " +
            "WHERE " +
            "   [Types].[is_user_defined] = 0 " +
            "ORDER BY " +
            "    [Types].[system_type_id] ";
        #endregion

        #region Get Tables And Columns SQL
        /// <summary>
        /// Contains the query to retrieve the list of tables and table columns.
        /// </summary>
        public static string GetTablesAndColumnsSql = Resources.TSqlGetTableColumnsQuery;
        #endregion

        #region Get Table Types SQL
        /// <summary>
        /// Contains the query to retrieve the list of user-defined tables types from SQL Server.
        /// </summary>
        public const string GetTableTypesSql =
              "SELECT  " +
              "	[TableTypes].[name]					typeName, " +
              "	[TableTypes].[system_type_id]		systemTypeId, " +
              "	[TableTypes].[type_table_object_id]	objectId, " +
              "	[TableTypes].[is_user_defined]		isUserDefined, " +
              "	[TableTypes].[is_table_type]		isTableType, " +
              "	[Columns].[name]					columnName, " +
              "	[Columns].[column_id]				columnId, " +
              "	[Columns].[system_type_id]			systemTypeId, " +
              "	[Columns].[max_length]				[maxLength], " +
              "	[Columns].[precision]				[precision], " +
              "	[Columns].[scale]					[scale], " +
              "	[Columns].[is_nullable]				isNullable, " +
              "	[Columns].[is_identity]				isIdentity, " +
              "	[Columns].[is_computed]				isComputed " +
              "FROM [sys].[table_types] [TableTypes] " +
              "	INNER JOIN [sys].[all_columns] [Columns] " +
              "			ON [TableTypes].[type_table_object_id] = [Columns].[object_id] " +
              "ORDER BY " +
              "	[TableTypes].[name], " +
              "	[Columns].[column_id] ";
        #endregion

        #region Get Triggers SQL
        /// <summary>
        /// Contains the query to retrieve the list of trigger definitions for all the tables in the database.
        /// </summary>
        public const string GetTriggersSql =
            "SELECT  " +
            "    [Tables].[name]               tableName, " +
            "    [Triggers].[name]             triggerName, " +
            "    [Triggers].[object_id]        triggerObjectId, " +
            "    [Triggers].[parent_id]        tableObjectId, " +
            "    [Modules].[definition]        sqlCode " +
            "FROM [sys].[triggers] [Triggers] " +
            "        INNER JOIN [sys].[sql_modules] [Modules] " +
            "            ON [Triggers].[object_id] = [Modules].[object_id] " +
            "        INNER JOIN [sys].[tables] [Tables] " +
            "            ON  [Triggers].[parent_id] = [Tables].[object_id] AND " +
            "                [Tables].[type] = 'U' AND  " +
            "                [Tables].[is_ms_shipped] = 0 " +
            "ORDER BY " +
            "    [Tables].[name], " +
            "    [Triggers].[name] ";
        #endregion

        #region Get Procedures SQL        
        /// <summary>
        /// Contains the query to retrieve the list of all stored procedure definitions in the database.
        /// </summary>
        public const string GetProceduresSql =
            "SELECT " +
            "   [StoredProcedures].[name], " +
            "   [StoredProcedures].[object_id], " +
            "	[StoredProcedures].[create_date], " +
            "	[StoredProcedures].[modify_date], " +
            "	[Modules].[definition], " +
            "	[Modules].[is_recompiled] " +
            "FROM " +
            "   [sys].[procedures] [StoredProcedures] " +
            "        INNER JOIN [sys].[sql_modules] [Modules] " +
            "           ON [StoredProcedures].[object_id] = [Modules].[object_id] " +
            "ORDER BY " +
            "   [StoredProcedures].[name] ";

        /// <summary>
        /// Contains the query to retrieve the stored procedure with the specified name.
        /// </summary>
        public const string GetProcedureByNameSql =
            "SELECT " +
            "   [StoredProcedures].[name], " +
            "   [StoredProcedures].[object_id], " +
            "	[StoredProcedures].[create_date], " +
            "	[StoredProcedures].[modify_date], " +
            "	[Modules].[definition], " +
            "	[Modules].[is_recompiled] " +
            "FROM " +
            "   [sys].[procedures] [StoredProcedures] " +
            "        INNER JOIN [sys].[sql_modules] [Modules] " +
            "           ON [StoredProcedures].[object_id] = [Modules].[object_id] " +
            "WHERE " +
            "   [StoredProcedures].[name] = @ProcedureName";

        /// <summary>
        /// Contains the query to retrieve the stored procedures with the specified partial name.
        /// </summary>
        public const string GetProceduresByPartialNameSql =
            "SELECT " +
            "   [StoredProcedures].[name], " +
            "   [StoredProcedures].[object_id], " +
            "	[StoredProcedures].[create_date], " +
            "	[StoredProcedures].[modify_date], " +
            "	[Modules].[definition], " +
            "	[Modules].[is_recompiled] " +
            "FROM [sys].[procedures] [StoredProcedures] " +
            "   INNER JOIN [sys].[sql_modules] [Modules] " +
            "       ON [StoredProcedures].[object_id] = [Modules].[object_id]  " +
            "WHERE " +
            "   [StoredProcedures].[name] LIKE @TableName + '%' " +
            "ORDER BY " +
            "    [StoredProcedures].[name] ";
        #endregion
    }
}
