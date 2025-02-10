using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Provides static methods / functions for loading the schema of a database.
    /// </summary>
    internal static class SchemaLoader
    {
        /// <summary>
        /// Appends the foreign key definitions to the parent table objects.
        /// </summary>
        /// <param name="tableList">
        /// A <see cref="SqlTableCollection"/> containing the table definitions.
        /// </param>
        /// <param name="foreignKeyList">
        /// A <see cref="SqlForeignKeyCollection"/> containing the foreign key definitions.
        /// </param>
        public static void AppendForeignKeys(SqlTableCollection tableList, SqlForeignKeyCollection? foreignKeyList)
        {
            if (foreignKeyList != null)
            {
                foreach (SqlForeignKey fk in foreignKeyList)
                {
                    SqlTable? table = tableList.FirstOrDefault(t => t.TableName == fk.ParentTableName);
                    if (table != null && table.ForeignKeys != null)
                        table.ForeignKeys.Add(fk);
                }
            }
        }
        /// <summary>
        /// Appends the index definitions to the parent table objects.
        /// </summary>
        /// <param name="tableList">
        /// A <see cref="SqlTableCollection"/> containing the table definitions.
        /// </param>
        /// <param name="indexList">
        /// A <see cref="SqlIndexCollection"/> containing the index definitions.
        /// </param>
        /// <param name="dataTypes">
        /// A <see cref="SqlDataTypeCollection"/> containing the data type definitions.
        /// </param>
        public static void AppendIndexes(SqlTableCollection tableList,
            SqlIndexCollection indexList, SqlDataTypeCollection dataTypes)
        {
            if (indexList != null)
            {
                foreach (SqlIndex index in indexList)
                {
                    SqlTable? table = tableList.Find(t => t.TableName == index.TableName);
                    table?.Indexes.Add(index);
                }
            }
        }
        /// <summary>
        /// Cross-references the table column definitions with the related index column definitions.
        /// </summary>
        /// <param name="tableList">
        /// A <see cref="SqlTableCollection"/> containing the table definitions.
        /// </param>
        /// <param name="indexList">
        /// A <see cref="SqlIndexCollection"/> containing the index definitions.
        /// </param>
        /// <param name="dataTypes">
        /// A <see cref="SqlDataTypeCollection"/> containing the data type definitions.
        /// </param>
        public static void CrossReferenceIndexes(SqlTableCollection tableList,
            SqlIndexCollection indexList, SqlDataTypeCollection dataTypes)
        {
            if (indexList != null)
            {
                foreach (SqlIndex index in indexList)
                {
                    SqlTable? table = tableList.FirstOrDefault(t => t.TableName == index.TableName);
                    if (table != null && index.Columns != null)
                    {
                        foreach (SqlIndexColumn indexCol in index.Columns)
                        {
                            indexCol.TableColumn = table.Columns.FirstOrDefault(c => c.ColumnId == indexCol.TableColumnId);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Gets the list of foreign key definitions from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlForeignKeyCollection"/> containing the foreign key definition instances.
        /// </returns>
        public static SqlForeignKeyCollection? GetForeignKeys(SqlDataProvider provider)
        {
            SqlForeignKeyCollection? result;
            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = da.GetForeignKeys();
            }

            return result;
        }
        /// <summary>
        /// Gets the list of foreign key definitions from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlForeignKeyCollection"/> containing the foreign key definition instances.
        /// </returns>
        public static async Task<SqlForeignKeyCollection?> GetForeignKeysAsync(SqlDataProvider provider)
        {
            SqlForeignKeyCollection? result;
            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = await da.GetForeignKeysAsync().ConfigureAwait(false);
            }

            return result;
        }
        /// <summary>
        /// Gets the list of table index definitions from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlIndexCollection"/> containing the index definition instances.
        /// </returns>
        public static SqlIndexCollection? GetIndexes(SqlDataProvider provider)
        {
            SqlIndexCollection? result;
            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = da.GetIndexes();
            }

            return result;
        }
        /// <summary>
        /// Gets the list of table index definitions from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlIndexCollection"/> containing the index definition instances.
        /// </returns>
        public static async Task<SqlIndexCollection?> GetIndexesAsync(SqlDataProvider provider)
        {
            SqlIndexCollection? result;
            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = await da.GetIndexesAsync().ConfigureAwait(false);
            }

            return result;
        }
        /// <summary>
        /// Gets the list of SQL data type definitions from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlDataTypeCollection"/> containing the SQL data type definition instances.
        /// </returns>
        public static SqlDataTypeCollection? GetSqlDataTypes(SqlDataProvider provider)
        {
            SqlDataTypeCollection? result;
            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = da.GetSqlDataTypes();
            }

            return result;
        }
        /// <summary>
        /// Gets the list of SQL data type definitions from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlDataTypeCollection"/> containing the SQL data type definition instances.
        /// </returns>
        public static async Task<SqlDataTypeCollection?> GetSqlDataTypesAsync(SqlDataProvider provider)
        {
            SqlDataTypeCollection? result;

            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = await da.GetSqlDataTypesAsync().ConfigureAwait(false);
            }

            return result;
        }
        /// <summary>
        /// Gets the list of stored procedures from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedureCollection"/> containing the list.
        /// </returns>
        public static SqlStoredProcedureCollection? GetProcedures(SqlDataProvider provider)
        {
            SqlStoredProcedureCollection? result = null;

            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = da.GetProcedures();
            }
            return result;
        }
        /// <summary>
        /// Gets the list of stored procedures from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedureCollection"/> containing the list.
        /// </returns>
        public static async Task<SqlStoredProcedureCollection?> GetProceduresAsync(SqlDataProvider provider)
        {
            SqlStoredProcedureCollection? result;

            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = await da.GetProceduresAsync().ConfigureAwait(false);
            }
            return result;
        }
        /// <summary>
        /// Gets the stored procedure definition by name.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <param name="procedureName">
        /// A string containing the name of the stored procedure to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedure"/> instance containing the definition, or
        /// <b>null</b> if not found.
        /// </returns>
        public static SqlStoredProcedure? GetProcedureByName(SqlDataProvider provider, string procedureName)
        {
            SqlStoredProcedure? procedure;

            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                procedure = da.GetProcedureByName(procedureName);
            }
            return procedure;
        }
        /// <summary>
        /// Gets the stored procedure definition by name.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <param name="procedureName">
        /// A string containing the name of the stored procedure to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedure"/> instance containing the definition, or
        /// <b>null</b> if not found.
        /// </returns>
        public static async Task<SqlStoredProcedure?> GetProcedureByNameAsync(SqlDataProvider provider, string procedureName)
        {
            SqlStoredProcedure? procedure;

            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                procedure = await da.GetProcedureByNameAsync(procedureName).ConfigureAwait(false);
            }
            return procedure;
        }
        /// <summary>
        /// Gets the list of stored procedures that are named after the specified table.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <param name="tableName">
        /// A string containing the name of the table.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedureCollection"/> instance containing the list.
        /// </returns>
        public static SqlStoredProcedureCollection? GetStoredProceduresForTable(SqlDataProvider provider, string tableName)
        {
            SqlStoredProcedureCollection? result;

            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = da.GetStandardProceduresForTable(tableName);
            }
            return result;
        }
        /// <summary>
        /// Gets the list of stored procedures that are named after the specified table.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <param name="tableName">
        /// A string containing the name of the table.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedureCollection"/> instance containing the list.
        /// </returns>
        public static async Task<SqlStoredProcedureCollection?> GetStoredProceduresForTableAsync(SqlDataProvider provider, string tableName)
        {
            SqlStoredProcedureCollection? result;

            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = await da.GetStandardProceduresForTableAsync(tableName).ConfigureAwait(false);
            }
            return result;
        }

        /// <summary>
        /// Gets the list of SQL table and column definitions from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlDataTypeCollection"/> containing the table definition instances.
        /// </returns>
        public static SqlTableCollection? GetTables(SqlDataProvider provider)
        {
            SqlTableCollection? result;
            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = da.GetTables();
            }

            return result;
        }
        /// <summary>
        /// Gets the list of SQL table and column definitions from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlDataTypeCollection"/> containing the table definition instances.
        /// </returns>
        public static async Task<SqlTableCollection?> GetTablesAsync(SqlDataProvider provider)
        {
            SqlTableCollection? result;
            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = await da.GetTablesAsync().ConfigureAwait(false);
            }

            return result;
        }
        /// <summary>
        /// Gets the list of SQL user-defined table definitions from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlTableTypeCollection"/> containing the user-defined table definition instances.
        /// </returns>
        public static SqlTableTypeCollection? GetTableTypes(SqlDataProvider provider)
        {
            SqlTableTypeCollection? result;
            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = da.GetTableTypes();
            }

            return result;
        }
        /// <summary>
        /// Gets the list of SQL user-defined table definitions from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use for queries.
        /// </param>
        /// <returns>
        /// A <see cref="SqlTableTypeCollection"/> containing the user-defined table definition instances.
        /// </returns>
        public static async Task<SqlTableTypeCollection?> GetTableTypesAsync(SqlDataProvider provider)
        {
            SqlTableTypeCollection? result = null;
            using (SqlSchemaDataAccess da = new SqlSchemaDataAccess(provider))
            {
                result = await da.GetTableTypesAsync().ConfigureAwait(false);
            }

            return result;
        }
    }
}
