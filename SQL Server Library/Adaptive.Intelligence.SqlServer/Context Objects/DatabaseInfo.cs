using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.Analysis;
using Adaptive.Intelligence.SqlServer.Schema;
using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Specialized;

namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Contains the current database information and other context data.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class DatabaseInfo : DisposableObjectBase
    {
        #region Public Events
        /// <summary>
        /// Occurs when the operational status is being updated for the UI.
        /// </summary>
        public event ProgressUpdateEventHandler? StatusUpdate;
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// The database name.
        /// </summary>
        private string? _databaseName;
        /// <summary>
        /// The connection string.
        /// </summary>
        private SqlConnectionStringBuilder? _connectionString;
        /// <summary>
        /// The database provider instance.
        /// </summary>
        private SqlDataProvider? _provider;
        /// <summary>
        /// The database schema definition.
        /// </summary>
        private SqlDatabase? _db;
        /// <summary>
        /// The database table profiles and meta data container.
        /// </summary>
        private AdaptiveTableMetadata? _tableData;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseInfo"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public DatabaseInfo()
        {
            _connectionString = new SqlConnectionStringBuilder();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseInfo"/> class.
        /// </summary>
        /// <param name="databaseName">
        /// A string containing the name of the database to use.
        /// </param>
        /// <param name="builder">
        /// The <see cref="SqlConnectionStringBuilder"/> instance used to generate the connection string.
        /// </param>
        public DatabaseInfo(string databaseName, SqlConnectionStringBuilder builder)
        {
            _databaseName = databaseName;
            _connectionString = builder;
            _provider = new SqlDataProvider(builder);
            _db = new SqlDatabase(_databaseName);
        }
        /// <param name="databaseName">
        /// A string containing the name of the database to use.
        /// </param>
        /// <param name="connectionString">
        /// The connection <see cref="string"/> used to connect to SQL Server.
        /// </param>
        public DatabaseInfo(string databaseName, string connectionString)
        {
            _databaseName = databaseName;
            _connectionString = new SqlConnectionStringBuilder(connectionString);
            _provider = new SqlDataProvider(_connectionString);
            _db = new SqlDatabase(_databaseName);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseInfo"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance connected to SQL Server.
        /// </param>
        public DatabaseInfo(SqlDataProvider provider)
        {
            _connectionString = new SqlConnectionStringBuilder(provider.ConnectionString);
            _databaseName = _connectionString.InitialCatalog;
            _provider = provider;
            _db = new SqlDatabase(_databaseName);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        /// <returns></returns>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _connectionString?.Clear();
                _provider?.Dispose();
                _tableData?.Dispose();
                _db?.Dispose();
            }

            _provider = null;
            _tableData = null;
            _db = null;
            _databaseName = null;
            _connectionString = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the connection string builder.
        /// </summary>
        /// <value>
        /// The <see cref="SqlConnectionStringBuilder"/> used to render the connection string value.
        /// </value>
        public SqlConnectionStringBuilder? ConnectionString => _connectionString;
        /// <summary>
        /// Gets the reference to the database schema container.
        /// </summary>
        /// <value>
        /// A <see cref="SqlDatabase"/> instance containing the schema information.
        /// </value>
        public SqlDatabase? Database => _db;
        /// <summary>
        /// Gets the database server name.
        /// </summary>
        /// <value>
        /// A string indicating the name of the current database context.
        /// </value>
        public string? DatabaseName => _databaseName;
        /// <summary>
        /// Gets the reference to the SQL data provider.
        /// </summary>
        /// <value>
        /// A <see cref="SqlDataProvider"/> instance.
        /// </value>
        public SqlDataProvider? Provider => _provider;
        /// <summary>
        /// Gets the reference to the table profile and meta data container.
        /// </summary>
        /// <value>
        /// A <see cref="AdaptiveTableMetadata"/> instance containing the table information.
        /// </value>
        public AdaptiveTableMetadata TableData
        {
            get
            {
                if (_tableData == null)
                {
                    _tableData = new AdaptiveTableMetadata(null);
                }

                return _tableData;
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Attempts to connect to the specified database server.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the connection information for connecting to SQL Server.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public bool Connect(string connectionString)
        {
            _connectionString = new SqlConnectionStringBuilder(connectionString);
            _databaseName = _connectionString.InitialCatalog;
            _provider = SqlDataProviderFactory.CreateProvider(_connectionString);

            // Try to connect to the data source.
            IOperationalResult result = _provider.TestConnection();
            bool canConnect = result.Success;
            if (!result.Success)
            {
                _provider.Dispose();
                _provider = null;
                _connectionString.Clear();
                _databaseName = null;
                _connectionString = null;
            }
            else
            {
                // Load the database schema.,
                _db = new SqlDatabase(_databaseName);
                _db.LoadSchema(_provider);
                _tableData = new AdaptiveTableMetadata(_db);

                // Initialize the SMO instances.
                OnStatusUpdate(new ProgressUpdateEventArgs("Initializing SMO..."));
                SMOProviderFactory.Initialize(_provider);

            }
            result.Dispose();

            return canConnect;
        }
        /// <summary>
        /// Attempts to connect to the specified database server.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the connection information for connecting to SQL Server.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> ConnectAsync(string connectionString)
        {
            OnStatusUpdate(new ProgressUpdateEventArgs("Connecting To Database..."));

            _connectionString = new SqlConnectionStringBuilder(connectionString);
            _databaseName = _connectionString.InitialCatalog;
            _provider = SqlDataProviderFactory.CreateProvider(_connectionString);

            // Try to connect to the data source.
            IOperationalResult result = await _provider.TestConnectionAsync().ConfigureAwait(false);
            bool canConnect = result.Success;
            if (!result.Success)
            {
                _provider.Dispose();
                _provider = null;
                _connectionString.Clear();
                _databaseName = null;
                _connectionString = null;
            }
            else
            {
                // Load the database schema.
                _db = new SqlDatabase(_databaseName);
                if (_databaseName != "master")
                {
                    // Load the DB Schema.
                    await _db.LoadSchemaAsync(_provider).ConfigureAwait(false);

                    // Create / load the table meta data as needed.
                    _tableData = new AdaptiveTableMetadata(_db);
                    await _tableData.StartAnalysisAsync(_provider).ConfigureAwait(false);
                }

                // Initialize the SMO instances.
                OnStatusUpdate(new ProgressUpdateEventArgs("Initializing SMO..."));
                await SMOProviderFactory.InitializeAsync(_provider).ConfigureAwait(false);
            }
            result.Dispose();

            return canConnect;
        }
        /// <summary>
        /// Downloads the content of all stored procedures in the database.
        /// </summary>
        /// <returns>
        /// A <see cref="Dictionary{TKey, TValue}"/> using the stored procedure name
        /// as the key, and a <see cref="StringCollection"/> containing the code as the value.
        /// </returns>
        public async Task<Dictionary<string, StringCollection>> DownloadAllStoredProceduresAsync()
        {
            Dictionary<string, StringCollection> _spList = new Dictionary<string, StringCollection>(1000);
            await Task.Yield();

            StoredProcedureCollection? proceduresList = SMOProviderFactory.Procedures;
            if (proceduresList != null)
            {
                // Look at only non-system stored procedures.
                int len = proceduresList.Count;
                int count = 1;
                List<StoredProcedure> userProceduresList = new List<StoredProcedure>(len);
                foreach (StoredProcedure p in proceduresList)
                {
                    if (!p.IsSystemObject)
                    {
                        userProceduresList.Add(p);
                    }
                }

                // Download the script content for each stored procedure.
                foreach (StoredProcedure sp in userProceduresList)
                {
                    OnStatusUpdate(
                        new ProgressUpdateEventArgs("Reading Stored Procedure Contents...",
                            sp.Name, Math.Percent(count + 1, len)));

                    //StringCollection code = sp.Script();
                    StringCollection code = new StringCollection
                    {
                        sp.TextHeader,
                        sp.TextBody
                    };

                    _spList.Add(sp.Name, code);
                    count++;
                }
            }
            return _spList;
        }
        /// <summary>
        /// Downloads the content of all stored procedures in the database.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the connection information for connecting to SQL Server.
        /// </param>
        /// <returns>
        /// A <see cref="Dictionary{TKey, TValue}"/> using the stored procedure name
        /// as the key, an a <see cref="StringCollection"/> containing the code as the value.
        /// </returns>
        public async Task<Dictionary<string, StringCollection>> DownloadAllStoredProceduresAsync(string connectionString)
        {
            Dictionary<string, StringCollection> _spList = new Dictionary<string, StringCollection>(1000);

            // Close the original connection, and re-initialize for the specified database.
            SMOProviderFactory.Close();
            await SMOProviderFactory.InitializeAsync(SqlDataProviderFactory.CreateProvider(connectionString)).ConfigureAwait(false);
            StoredProcedureCollection? proceduresList = SMOProviderFactory.Procedures;
            if (proceduresList != null)
            {
                // Look at only non-system stored procedures.
                int len = proceduresList.Count;
                int count = 1;
                List<StoredProcedure> userProceduresList = new List<StoredProcedure>(proceduresList.Count);
                foreach (StoredProcedure p in proceduresList)
                {
                    if (!p.IsSystemObject)
                    {
                        userProceduresList.Add(p);
                    }
                }

                // Download the script content for each stored procedure.
                foreach (StoredProcedure sp in userProceduresList)
                {
                    if (count % 10 == 0)
                    {
                        OnStatusUpdate(
                            new ProgressUpdateEventArgs("Reading Stored Procedure Contents...",
                                sp.Name, (int)((count / (float)len) * 100)));
                    }
                    //StringCollection code = sp.Script();
                    StringCollection code = new StringCollection();
                    code.Add(sp.TextHeader);
                    code.Add(sp.TextBody);

                    if (!_spList.ContainsKey(sp.Name))
                    {
                        _spList.Add(sp.Name, code);
                    }

                    count++;
                }
            }
            return _spList;
        }
        /// <summary>
        /// Gets the table profile by name.
        /// </summary>
        /// <param name="schema">
        /// A string containing the table schema name.
        /// </param>
        /// <param name="tableName">
        /// A string containing the name of the table to look for.
        /// </param>
        /// <returns>
        /// The <see cref="AdaptiveTableProfile"/> instance for the specified table, or <b>null</b>.
        /// </returns>
        public AdaptiveTableProfile? GetTableProfile(string? schema, string? tableName)
        {
            AdaptiveTableProfile? tableProfile = null;

            if (!string.IsNullOrEmpty(tableName) && _tableData != null)
            {
                tableProfile = _tableData[tableName];
                if (tableProfile == null && schema != null)
                {
                    tableProfile = _tableData.FindBySchemaAndName(schema, tableName);
                }
            }
            return tableProfile;
        }
        #endregion

        #region Private Event / Event Handler Methods
        /// <summary>
        /// Raises the <see cref="StatusUpdate" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ProgressUpdateEventArgs"/> instance containing the event data.</param>
        private void OnStatusUpdate(ProgressUpdateEventArgs e)
        {
            StatusUpdate?.Invoke(this, e);
        }
        #endregion
    }
}
