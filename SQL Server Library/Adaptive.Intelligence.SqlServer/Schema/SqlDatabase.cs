using Adaptive.Intelligence.Shared;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a database in SQL Server.
    /// </summary>
    public sealed class SqlDatabase : DisposableObjectBase
    {
        #region Private Member Declarations        
        /// <summary>
        /// The list of SQL Server data types .
        /// </summary>
        private SqlDataTypeCollection? _dataTypes;
        /// <summary>
        /// The list of table types.
        /// </summary>
        private SqlTableTypeCollection? _tableTypes;
        /// <summary>
        /// The list of actual tables.
        /// </summary>
        private SqlTableCollection? _tables;
        /// <summary>
        /// The database name.
        /// </summary>
        private string? _name;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDatabase"/> class.
        /// </summary>
        /// <param name="databaseName">
        /// The name of the database.
        /// </param>
        public SqlDatabase(string? databaseName)
        {
            _name = databaseName;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _tables?.Clear();
                _tableTypes?.Clear();
                _dataTypes?.Clear();
            }

            _dataTypes = null;
            _tableTypes = null;
            _tables = null;
            _name = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the list of data types defined in the database.
        /// </summary>
        /// <value>
        /// A <see cref="SqlTableCollection"/> containing the standard data types defined in the database.
        /// </value>
        public SqlDataTypeCollection? DataTypes => _dataTypes;
        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <value>
        /// A string containing the name of the database.
        /// </value>
        public string? Name => _name;
        /// <summary>
        /// Gets the reference to the list of tables defined in the database.
        /// </summary>
        /// <value>
        /// A <see cref="SqlTableCollection"/> containing the tables defined in the database.
        /// </value>
        public SqlTableCollection? Tables => _tables;
        /// <summary>
        /// Gets the reference to the list of table data types defined in the database.
        /// </summary>
        /// <value>
        /// A <see cref="SqlTableCollection"/> containing the table data types defined in the database.
        /// </value>
        public SqlTableTypeCollection? TableTypes => _tableTypes;
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Gets the list of stored procedures that are named after the specified table.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string used to connect to SQL Server.
        /// </param>
        /// <param name="tableName">
        /// A string containing the name of the table.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedureCollection"/> instance containing the list.
        /// </returns>
        public static SqlStoredProcedureCollection? GetStoredProceduresForTable(string connectionString, string tableName)
        {
            SqlStoredProcedureCollection? storedProcList;

            using (SqlDataProvider provider = SqlDataProviderFactory.CreateProvider(connectionString))
            {
                storedProcList = SchemaLoader.GetStoredProceduresForTable(provider, tableName);
            }
            return storedProcList;
        }
        /// <summary>
        /// Gets the list of stored procedures that are named after the specified table.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to be used.
        /// </param>
        /// <param name="tableName">
        /// A string containing the name of the table.
        /// </param>
        /// <returns>
        /// A <see cref="SqlStoredProcedureCollection"/> instance containing the list.
        /// </returns>
        public static SqlStoredProcedureCollection? GetStoredProceduresForTable(SqlDataProvider? provider,
            string tableName)
        {
            if (provider != null)
                return SchemaLoader.GetStoredProceduresForTable(provider, tableName);
            else
                return null;
        }
        /// <summary>
		/// Gets the list of stored procedures that are named after the specified table.
		/// </summary>
		/// <param name="connectionString">
		/// The connection string value used to connect to SQL Server.
		/// </param>
		/// <param name="tableName">
		/// A string containing the name of the table.
		/// </param>
		/// <returns>
		/// A <see cref="SqlStoredProcedureCollection"/> instance containing the list.
		/// </returns>
		public async Task<SqlStoredProcedureCollection?> GetStoredProceduresForTableAsync(string connectionString, string tableName)
        {
            SqlStoredProcedureCollection? storedProcList;

            using (SqlDataProvider provider = SqlDataProviderFactory.CreateProvider(connectionString))
            {
                storedProcList = await SchemaLoader.GetStoredProceduresForTableAsync(provider, tableName).ConfigureAwait(false);
            }
            return storedProcList;
        }
        /// <summary>
        /// Attempts to load the schema from the database.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string used to connect to SQL Server.
        /// </param>
        /// <remarks>
        /// This method overload will read and utilize the connection string from the app.config file.
        /// </remarks>
        public async Task LoadSchemaAsync(string connectionString)
        {
            using (SqlDataProvider provider = SqlDataProviderFactory.CreateProvider(connectionString))
            {
                await LoadSchemaAsync(provider).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Attempts to load the schema from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use to perform the SQL queries.
        /// </param>
        public void LoadSchema(SqlDataProvider provider)
        {
            // Step 1. Load the defined data types from the SQL database.
            _dataTypes = SchemaLoader.GetSqlDataTypes(provider);

            // Step 2. Load the list of tables. (Ensure we are pointing the correct database).
            provider.SetDatabase(_name!);
            _tables = SchemaLoader.GetTables(provider);

            // Step 3. Load the indexes defined on the table.
            SqlIndexCollection? indexList = SchemaLoader.GetIndexes(provider);

            // Step 4. Load any foreign key constraints.
            SqlForeignKeyCollection? keysList = SchemaLoader.GetForeignKeys(provider);

            // Step 5. Load the table type definitions.
            _tableTypes = SchemaLoader.GetTableTypes(provider);

            // Step 6. Add the index definitions to the appropriate tables.
            if (_tables != null && indexList != null && _dataTypes != null)
            {
                SchemaLoader.AppendIndexes(_tables, indexList, _dataTypes);

                // Step 7. Add the foreign key definitions to the appropriate tables.
                SchemaLoader.AppendForeignKeys(_tables, keysList);

                // Step 8. Cross-reference the indexes and data types.
                SchemaLoader.CrossReferenceIndexes(_tables, indexList, _dataTypes);
            }
        }
        /// <summary>
        /// Attempts to load the schema from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use to perform the SQL queries.
        /// </param>
        public async Task LoadSchemaAsync(SqlDataProvider provider)
        {
            // Ensure we query the right database!
            provider.SetDatabase(_name!);

            // Step 1. Load the defined data types from the SQL database.
            _dataTypes = await SchemaLoader.GetSqlDataTypesAsync(provider).ConfigureAwait(false);

            // Step 2. Load the list of tables.
            _tables = await SchemaLoader.GetTablesAsync(provider).ConfigureAwait(false);

            // Step 3. Load the indexes defined on the table.
            SqlIndexCollection? indexList = await SchemaLoader.GetIndexesAsync(provider).ConfigureAwait(false);

            // Step 4. Load any foreign key constraints.
            SqlForeignKeyCollection? keysList = await SchemaLoader.GetForeignKeysAsync(provider).ConfigureAwait(false);

            // Step 5. Load the table type definitions.
            _tableTypes = await SchemaLoader.GetTableTypesAsync(provider).ConfigureAwait(false);

            // Step 6. Add the index definitions to the appropriate tables.
            if (_tables != null && indexList != null && _dataTypes != null)
            {
                SchemaLoader.AppendIndexes(_tables, indexList, _dataTypes);

                // Step 7. Add the foreign key definitions to the appropriate tables.
                SchemaLoader.AppendForeignKeys(_tables, keysList);

                // Step 8. Cross-reference the indexes and data types.
                SchemaLoader.CrossReferenceIndexes(_tables, indexList, _dataTypes);
            }
        }
        #endregion
    }
}
