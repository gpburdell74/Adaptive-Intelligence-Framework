using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.SqlServer.Maintenance;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Data_Access
{
    /// <summary>
    /// Provides the data access methods for performing SQL maintenance operations.
    /// </summary>
    /// <seealso cref="DataAccessBase" />
    public sealed class SqlMaintenanceDataAccess : SqlDataAccessBase
    {
        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlMaintenanceDataAccess"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the connection information for the data store.
        /// </param>
        public SqlMaintenanceDataAccess(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlMaintenanceDataAccess"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use.
        /// </param>
        public SqlMaintenanceDataAccess(SqlDataProvider provider) : base(provider)
        {
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Reads the list of tables and indexes from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="DatabaseStatistic"/> instance populated with schema information instances, if
        /// successful; otherwise, returns <b>null</b>.
        /// </returns>
        public DatabaseStatistic? ReadBasicDatabaseSchema()
        {
            DatabaseStatistic? db = null;

            // Load the schema information data sets.
            string sql = TSqlConstants.SqlBasicSchemaQuery;
            ISafeSqlDataReader? reader = GetReaderForMultipleResultSets(sql);

            if (reader != null)
            {
                db = new DatabaseStatistic();

                // Read the contents.
                ReadDbInfo(reader, db);
                ReadTableInfo(reader, db);
                ReadIndexInfo(reader, db);

                reader.Dispose();
            }
            return db;
        }
        /// <summary>
        /// Reads the list of tables and indexes from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="DatabaseInfo"/> instance populated with schema information instances, if
        /// successful; otherwise, returns <b>null</b>.
        /// </returns>
        public async Task<DatabaseStatistic?> ReadBasicDatabaseSchemaAsync()
        {
            DatabaseStatistic? db = null;

            // Load the schema information datasets.
            string sql = TSqlConstants.SqlBasicSchemaQuery;
            ISafeSqlDataReader? reader = await GetReaderForMultipleResultSetsAsync(sql).ConfigureAwait(false);

            if (reader != null)
            {
                db = new DatabaseStatistic();

                // Read the contents.
                ReadDbInfo(reader, db);
                ReadTableInfo(reader, db);
                ReadIndexInfo(reader, db);

                reader.Dispose();
            }
            return db;
        }
        /// <summary>
        /// Reads the fragmentation statistics for the indexes on the specified table.
        /// </summary>
        /// <param name="objectId">
        /// The integer containing the unique object ID for the table to read the statistics for.
        /// </param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="IndexStatistic"/> instances if successful;
        /// otherwise, returns <b>null</b>.
        /// </returns>
        public List<IndexStatistic>? ReadIndexFragmentationStatistics(int objectId)
        {
            List<IndexStatistic>? list = null;

            // Execute the fragment query for the specified object ID.
            string sql = TSqlConstants.SqlFragmentedIndexQuery;
            ISafeSqlDataReader? reader = GetReaderForParameterizedCommandText(
                sql,
                new[] {
                    CreateParameter(TSqlConstants.SqlParamObjectId, objectId)
                });

            if (reader != null)
            {
                list = new List<IndexStatistic>();
                while (reader.Read())
                {
                    int index = 0;
                    IndexStatistic stat = new IndexStatistic
                    {
                        DatabaseId = reader.GetInt16(index++),
                        ObjectId = reader.GetInt32(index++),
                        IndexId = reader.GetInt32(index++),
                        AverageFragmentationPercent = reader.GetDouble(index++),
                        FragmentCount = reader.GetInt64(index++),
                        PageCount = reader.GetInt64(index)
                    };
                    list.Add(stat);
                }
                reader.Dispose();
            }
            return list;
        }
        /// <summary>
        /// Reads the fragmentation statistics for the indexes on the specified table.
        /// </summary>
        /// <param name="objectId">
        /// The integer containing the unique object ID for the table to read the statistics for.
        /// </param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="IndexStatistic"/> instances if successful;
        /// otherwise, returns <b>null</b>.
        /// </returns>
        public async Task<List<IndexStatistic>?> ReadIndexFragmentationStatisticsAsync(int objectId)
        {
            List<IndexStatistic>? list = null;

            // Execute the fragment query for the specified object Id.
            string sql = TSqlConstants.SqlFragmentedIndexQuery;
            ISafeSqlDataReader? reader = await GetReaderForParameterizedCommandTextAsync(sql,
                new[] { CreateParameter(TSqlConstants.SqlParamObjectId, objectId) });

            if (reader != null)
            {
                list = new List<IndexStatistic>();
                while (reader.Read())
                {
                    int index = 0;
                    IndexStatistic stat = new IndexStatistic
                    {
                        DatabaseId = reader.GetInt16(index++),
                        ObjectId = reader.GetInt32(index++),
                        IndexId = reader.GetInt32(index++),
                        AverageFragmentationPercent = reader.GetDouble(index++),
                        FragmentCount = reader.GetInt64(index++),
                        PageCount = reader.GetInt64(index)
                    };
                    list.Add(stat);
                }
                reader.Dispose();
            }
            return list;
        }
        /// <summary>
        /// Executes the query to mark the specified table for recompilation.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public bool RecompileTable(string? tableName)
        {
            // Execute.
            if (!string.IsNullOrEmpty(tableName))
            {
                int result = ExecuteSql(string.Format(TSqlConstants.SqlRecompile, tableName));
                return result > TSqlConstants.ExecuteFailed;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Executes the query to mark the specified table for recompilation.
        /// </summary>
        /// <param name="schema">
        /// A string specifying the schema.
        /// </param>
        /// <param name="tableName">
        /// A string containing the name of the table.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> RecompileTableAsync(string? schema, string? tableName)
        {
            // Execute.
            if (!string.IsNullOrEmpty(tableName))
            {
                tableName = TSqlConstants.RenderSchemaAndTableName(schema, tableName);
                int result = await ExecuteSqlAsync(string.Format(TSqlConstants.SqlRecompile, tableName)).ConfigureAwait(false);
                return result > TSqlConstants.ExecuteFailed;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Tests the ability to connect to the remote database.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the connection information for SQL Server.
        /// </param>
        /// <returns>
        /// <b>true</b> if the connection is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public static bool TestConnection(string? connectionString)
        {
            SqlDataProvider? provider = null;
            bool success = false;

            if (!string.IsNullOrEmpty(connectionString))
            {
                try
                {
                    provider = SqlDataProviderFactory.CreateProvider(connectionString);
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
                if (provider != null)
                {
                    IOperationalResult result = provider.TestConnection();
                    success = result.Success;
                    result.Dispose();
                    provider.Dispose();
                }
            }
            return success;
        }
        /// <summary>
        /// Tests the ability to connect to the remote database.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the connection information for SQL Server.
        /// </param>
        /// <returns>
        /// <b>true</b> if the connection is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> TestConnectionAsync(string? connectionString)
        {
            SqlDataProvider? provider = null;
            bool success = false;

            if (!string.IsNullOrEmpty(connectionString))
            {
                try
                {
                    provider = SqlDataProviderFactory.CreateProvider(connectionString);
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
                if (provider != null)
                {
                    IOperationalResult result = await provider.TestConnectionAsync().ConfigureAwait(false);
                    success = result.Success;
                    result.Dispose();
                    provider.Dispose();
                }
            }

            return success;
        }
        /// <summary>
        /// Executes the SQL command to update the statistics for the specified table.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table to update.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public bool UpdateStatisticsForTable(string? tableName)
        {
            // Execute.
            if (!string.IsNullOrEmpty(tableName))
            {
                int result = ExecuteSql(string.Format(TSqlConstants.SqlUpdateStats, tableName));
                return result > TSqlConstants.ExecuteFailed;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Executes the SQL command to update the statistics for the specified table.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table to update.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> UpdateStatisticsForTableAsync(string? tableName)
        {
            // Execute.
            if (!string.IsNullOrEmpty(tableName))
            {
                Exceptions.Clear();
                int result = await ExecuteSqlAsync(string.Format(TSqlConstants.SqlUpdateStats, tableName)).ConfigureAwait(false);
                return (Exceptions.Count == 0);
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Reads the database schema information.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the data.
        /// </param>
        /// <param name="db">
        /// The <see cref="DatabaseStatistic"/> instance to be populated.
        /// </param>
        private static void ReadDbInfo(ISafeSqlDataReader? reader, DatabaseStatistic? db)
        {
            if (reader != null && reader.Read() && db != null)
            {
                int index = 0;
                db.Id = reader.GetInt16(index++);
                db.Name = reader.GetString(index);

                reader.NextResult();
            }
        }
        /// <summary>
        /// Reads the table schema information for the supplied database.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the data.
        /// </param>
        /// <param name="db">
        /// The <see cref="DatabaseInfo"/> instance to be populated.
        /// </param>
        private static void ReadTableInfo(ISafeSqlDataReader? reader, DatabaseStatistic? db)
        {
            if (reader != null)
            {
                while (reader.Read())
                {
                    int index = 0;
                    TableStatistic table = new TableStatistic
                    {
                        Schema = reader.GetString(index++),
                        Name = reader.GetString(index++),
                        ObjectId = reader.GetInt32(index)
                    };
                    if (db != null)
                    {
                        db.AddTable(table);
                    }
                }
                reader.NextResult();
            }
        }
        /// <summary>
        /// Reads the index schema information for the tables in the supplied database.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the data.
        /// </param>
        /// <param name="db">
        /// The <see cref="DatabaseInfo"/> instance to be populated.
        /// </param>
        private static void ReadIndexInfo(ISafeSqlDataReader? reader, DatabaseStatistic? db)
        {
            if (reader != null)
            {
                while (reader.Read())
                {
                    int index = 0;
                    IndexStatisticsInfo newIndex = new IndexStatisticsInfo
                    {
                        TableId = reader.GetInt32(index++),
                        IndexId = reader.GetInt32(index++),
                        IndexType = reader.GetByte(index++),
                        Name = reader.GetString(index++),
                        PrimaryKey = reader.GetBoolean(index)
                    };
                    if (db != null)
                    {
                        db.AddIndexToTable(newIndex.TableId, newIndex);
                    }
                }
                reader.NextResult();
            }
        }
        #endregion
    }
}