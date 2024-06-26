using Adaptive.Intelligence.Shared.Logging;
using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Provides static initialization and de-initialization methods, and properties for the SQL Server Management Object (SMO)
    /// instances used in the application.
    /// </summary>
    internal static class SMOProviderFactory
    {
        #region Private Static Member Declarations
        /// <summary>
        /// The SMO Server instance.
        /// </summary>
        private static Server? _server;
        /// <summary>
        /// The SMO server connection.
        /// </summary>
        private static ServerConnection? _serverConnection;
        /// <summary>
        /// The SMO database instance.
        /// </summary>
        private static Database? _db;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the static reference to the SMO DB instance.
        /// </summary>
        /// <value>
        /// The <see cref="Database"/> instance if connected; otherwise, <b>null</b>.
        /// </value>
        public static Database? Db => _db;
        /// <summary>
        /// Gets the static reference to the SMO server instance.
        /// </summary>
        /// <value>
        /// The <see cref="Microsoft.SqlServer.Management.Smo.Server"/> instance if connected; otherwise, <b>null</b>.
        /// </value>
        public static Server? Server => _server;
        /// <summary>
        /// Gets the static reference to the SMO list of tables.
        /// </summary>
        /// <value>
        /// The <see cref="TableCollection"/> instance if connected; otherwise, <b>null</b>.
        /// </value>
        public static TableCollection? Tables
        {
            get
            {
                TableCollection? tableList = null;

                if (_db != null && _db.Tables != null)
                {
                    EnsureTables();
                    tableList = _db.Tables;
                }
                return tableList;
            }
        }
        /// <summary>
        /// Gets the static reference to the SMO list of stored procedures.
        /// </summary>
        /// <value>
        /// The <see cref="StoredProcedureCollection"/> instance if connected; otherwise, <b>null</b>.
        /// </value>
        public static StoredProcedureCollection? Procedures
        {
            get
            {
                StoredProcedureCollection? procList = null;

                if (_db != null && _db.StoredProcedures != null)
                {
                    EnsureProcedures();
                    procList = _db.StoredProcedures;
                }
                return procList;
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Initializes the static object references and opens the SMO connection to the server and database
        /// as specified in the <i>provider</i>'s connection string.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance used to connect to the server.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, <b>false</b>.
        /// </returns>
        public static bool Initialize(SqlDataProvider provider)
        {
            bool success = false;

            if (_server != null && _db != null)
                // Already connected, return true.
                success = true;
            else
            {
                // Create the Server and connection.
                _server = CreateServerConnection(provider);
                if (_server != null)
                {
                    // Get the DB reference.
                    _db = GetDatabase(_server, provider);

                    if (_db != null)
                    {
                        // Refresh the DB Contents, with focus on the tables.
                        try
                        {
                            _db.Refresh();
                            _db.EnumObjects(DatabaseObjectTypes.Table);
                            while (_db.Tables.Count == 0)
                            {
                                Thread.Sleep(100);
                            }
                            success = true;
                        }
                        catch
                        {
                            _serverConnection?.Disconnect();
                            _serverConnection = null;
                            _server = null;
                        }
                    }
                    else
                    {
                        _serverConnection?.Disconnect();
                        _serverConnection = null;
                        _server = null;
                    }
                }
            }
            return success;
        }
        /// <summary>
        /// Initializes the static references.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance used to connect to the server.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, <b>false</b>.
        /// </returns>
        public async static Task<bool> InitializeAsync(SqlDataProvider provider)
        {
            await Task.Yield();
            return Initialize(provider);
        }
        /// <summary>
        /// Closes the static SMO instances.
        /// </summary>
        public static void Close()
        {
            try
            {
                _serverConnection?.Disconnect();
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }

            _db = null;
            _serverConnection = null;
            _server = null;
        }
        public static Table? FindTable(string tableName)
        {
            Table? item = null;

            TableCollection? tableList = Tables;
            foreach (Table table in tableList)
            {
                if (table.ToString() == tableName)
                    item = table;
            }
            return item;
		}
        /// <summary>
        /// Resets the SMO objects and connection.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance used to connect to the server.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, <b>false</b>.
        /// </returns>
        public static bool Reset(SqlDataProvider provider)
        {
            Close();
            return Initialize(provider);
        }
        /// <summary>
        /// Resets the SMO objects and connection.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance used to connect to the server.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, <b>false</b>.
        /// </returns>
        public static async Task<bool> ResetAsync(SqlDataProvider provider)
        {
            await Task.Yield();
            Close();
            return await InitializeAsync(provider);
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Ensures the tables on the database object are enumerated.
        /// </summary>
        private static void EnsureTables()
        {
            if (_db != null)
            {
                try
                {
                    _db.EnumObjects(DatabaseObjectTypes.Table, Microsoft.SqlServer.Management.Smo.SortOrder.Name);
                    int waitCount = 0;
                    while (_db.Tables.Count == 0 && waitCount < 10)
                    {
                        Thread.Sleep(100);
                        waitCount++;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }
        }
        /// <summary>
        /// Ensures the stored procedures on the database object are enumerated.
        /// </summary>
        private static void EnsureProcedures()
        {
            if (_db != null)
            {
                try
                {
                    _db.EnumObjects(DatabaseObjectTypes.StoredProcedure, Microsoft.SqlServer.Management.Smo.SortOrder.Name);
                    int waitCount = 0;
                    while (_db.StoredProcedures.Count == 0 && waitCount < 10)
                    {
                        Thread.Sleep(100);
                        waitCount++;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }
        }
        /// <summary>
        /// Gets the database object reference.
        /// </summary>
        /// <param name="server">
        /// The <see cref="Microsoft.SqlServer.Management.Smo.Server"/> instance in which the database is stored.
        /// </param>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance used to connect to the server.
        /// </param>
        /// <returns>
        /// The SMO <see cref="Database"/> instance if successful; otherwise, returns <b>null</b>.
        /// </returns>
        private static Database? GetDatabase(Server server, SqlDataProvider provider)
        {
            // Extract the database name.
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(provider.ConnectionString);
            string dbName = builder.InitialCatalog;
            builder.Clear();

            Database? db = null;
            try
            {
                db = new Database(server, dbName);
                db.Initialize(true);
                ScriptingOptions options = new ScriptingOptions
                {
                    ExtendedProperties = true,
                    AllowSystemObjects = false,
                    BatchSize = 20000
                };

                db.PrefetchObjects(typeof(StoredProcedure), options);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }

            return db;
        }
        /// <summary>
        /// Gets the server object reference.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> instance used to connect to the server.
        /// </param>
        /// <returns>
        /// The <see cref="Microsoft.SqlServer.Management.Smo.Server"/> instance in which the database is stored.
        /// </returns>
        private static Server? CreateServerConnection(SqlDataProvider provider)
        {
            Server? server = null;
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(provider.ConnectionString);
                SqlConnectionInfo info = new SqlConnectionInfo();
                info.ServerName = builder.DataSource;
                info.DatabaseName = builder.InitialCatalog;
                info.UserName = builder.UserID;
                info.Password = builder.Password;
                info.TrustServerCertificate = builder.TrustServerCertificate;
                switch(builder.Authentication)
                {
                    case SqlAuthenticationMethod.SqlPassword:
						info.Authentication = SqlConnectionInfo.AuthenticationMethod.SqlPassword;
						break;

                    case SqlAuthenticationMethod.ActiveDirectoryPassword:
                        info.Authentication = SqlConnectionInfo.AuthenticationMethod.ActiveDirectoryPassword;
                        break;

					case SqlAuthenticationMethod.ActiveDirectoryIntegrated:
						info.Authentication = SqlConnectionInfo.AuthenticationMethod.ActiveDirectoryIntegrated;
						break;

				}

				ServerConnection serverConnection = new ServerConnection(info);
                builder.Clear();

                server = new Server(serverConnection);
                server.GetDefaultInitFields(typeof(StoredProcedure));
                server.SetDefaultInitFields(typeof(StoredProcedure), "IsSystemObject");
                server.SetDefaultInitFields(typeof(StoredProcedure), true);
                if (!server.ConnectionContext.IsOpen)
                    server.ConnectionContext.Connect();
            }
            catch(Exception ex)
            {
                ExceptionLog.LogException(ex);
            }

            return server;
        }
        #endregion
    }
}
