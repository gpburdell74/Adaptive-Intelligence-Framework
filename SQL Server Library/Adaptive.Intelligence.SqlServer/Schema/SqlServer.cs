using Adaptive.Intelligence.Shared;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a SQL Server instance with one or more databases.
    /// </summary>
    /// <seealso cref="DisposableObjectBase"/>
    /// <seealso cref="SqlDatabaseCollection"/>
    public sealed class SqlServer : DisposableObjectBase
    {
        #region Private Member Declarations
        /// <summary>
        /// The list of databases on the server.
        /// </summary>
        private SqlDatabaseCollection? _databases;
        /// <summary>
        /// The server name.
        /// </summary>
        private string? _name;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServer"/> class.
        /// </summary>
        /// <param name="serverName">
        /// A string containing the name of the server.
        /// </param>
        public SqlServer(string serverName)
        {
            _name = serverName;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _databases?.Clear();
            }

            _databases = null;
            _name = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the list of databases on the server.
        /// </summary>
        /// <value>
        /// A <see cref="SqlDatabaseCollection"/> containing the representations for the databases.
        /// </value>
        public SqlDatabaseCollection? Databases => _databases;
        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <value>
        /// A string containing the name of the database.
        /// </value>
        public string? Name => _name;
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Attempts to load the schema from the database.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string used to connect to SQL Server.
        /// </param>
        public async Task<bool> LoadSchemaAsync(string connectionString)
        {
            bool success;
            using (SqlDataProvider provider = SqlDataProviderFactory.CreateProvider(connectionString))
            {
                success = await LoadSchemaAsync(provider).ConfigureAwait(false);
            }
            return success;
        }
        /// <summary>
        /// Attempts to load the schema from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use to perform the SQL queries.
        /// </param>
        public bool LoadSchema(SqlDataProvider provider)
        {
            if (_databases == null)
            {
                _databases = new SqlDatabaseCollection();
            }
            else
            {
                _databases.Clear();
            }

            // Step 1. Load the list of databases.
            IOperationalResult<List<string>> result = provider.GetDatabaseNames();
            bool success = result.Success;
            if (success)
            {
                List<string>? dbList = result.DataContent;

                // Step 2. Load the schema for each of the databases.
                if (dbList != null)
                {
                    foreach (string dbName in dbList)
                    {
                        SqlDatabase db = new SqlDatabase(dbName);
                        db.LoadSchema(provider);
                        _databases.Add(db);
                    }
                }
            }
            result.Dispose();

            return success;
        }
        /// <summary>
        /// Attempts to load the schema from the database.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance to use to perform the SQL queries.
        /// </param>
        public async Task<bool> LoadSchemaAsync(SqlDataProvider provider)
        {
            if (_databases == null)
            {
                _databases = new SqlDatabaseCollection();
            }
            else
            {
                _databases.Clear();
            }

            // Step 1. Load the list of databases.
            IOperationalResult<List<string>> result = await provider.GetDatabaseNamesAsync().ConfigureAwait(false);
            bool success = result.Success;
            if (result.Success)
            {
                List<string>? dbList = result.DataContent;

                // Step 2. Load the schema for each of the databases.
                if (dbList != null)
                {
                    foreach (string dbName in dbList)
                    {
                        if (dbName != "master")
                        {
                            SqlDatabase db = new SqlDatabase(dbName);
                            await db.LoadSchemaAsync(provider).ConfigureAwait(false);
                            _databases.Add(db);
                        }
                    }
                }
            }
            result.Dispose();

            return success;
        }
        /// <summary>
        /// Attempts to load the schema from the database.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the database connection information.
        /// </param>
        public async Task<List<string>?> LoadDatabaseNamesAsync(string connectionString)
        {
            List<string>? dbNames = null;
            SqlDataProvider provider = SqlDataProviderFactory.CreateProvider(connectionString);

            // Step 1. Load the list of databases.
            IOperationalResult<List<string>> result = await provider.GetDatabaseNamesAsync().ConfigureAwait(false);
            bool success = result.Success;
            if (result.Success)
            {
                dbNames = new List<string>();
                dbNames.AddRange(result.DataContent!);
            }
            result.Dispose();
            provider.Dispose();

            return dbNames;
        }
        #endregion
    }
}
