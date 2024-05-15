using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Code;
using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.SqlServer.Analysis;
using Adaptive.Intelligence.SqlServer.CodeDom;
using Adaptive.Intelligence.SqlServer.ORM;
using Adaptive.Intelligence.SqlServer.Schema;
using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.CodeDom;
using System.Collections.Specialized;
using System.Data;
using System.Text;

namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Provides a central container for SQL Server operations within an application.
    /// </summary>
    /// <seealso cref="Adaptive.Intelligence.Shared.DisposableObjectBase" />
    public sealed class ContextOperations : DisposableObjectBase, ICloneable
    {
        #region Public Events        
        /// <summary>
        /// Occurs when the object is connected to SQL Server.
        /// </summary>
        public event EventHandler? Connected;
        /// <summary>
        /// Occurs when the object disconnects from SQL Server.
        /// </summary>
        public event EventHandler? Disconnected;
        /// <summary>
        /// Occurs when the operational status is being updated for the UI.
        /// </summary>
        public event ProgressUpdateEventHandler? StatusUpdate;
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// The database connection, schema and meta data information container.
        /// </summary>
        private DatabaseInfo? _dbInfo;
        /// <summary>
        /// The SQL code DOM generator instance.
        /// </summary>
        private AdaptiveSqlCodeDomGenerator? _codeDom;
        /// <summary>
        /// The maintenance processor instance.
        /// </summary>
        private GeneralMaintenanceProcessor? _maintenanceProcessor;
        /// <summary>
        /// The connection string builder.
        /// </summary>
        private SqlConnectionStringBuilder? _connectString;
        /// <summary>
        /// The in the middle of trying to connect flag.
        /// </summary>
        private bool _connecting;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextOperations"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public ContextOperations()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextOperations"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public ContextOperations(string connectionString)
        {
            _connectString = new SqlConnectionStringBuilder(connectionString);
        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _codeDom?.Dispose();
                _dbInfo?.Dispose();
                _maintenanceProcessor?.Dispose();
                _connectString?.Clear();
            }

            _connectString = null;
            _maintenanceProcessor = null;
            _codeDom = null;
            _dbInfo = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value indicating whether the instance is connected to a database.
        /// </summary>
        /// <value>
        ///   <b>true</b> if connected; otherwise, <b>false</b>.
        /// </value>
        public bool IsConnected => _dbInfo != null;
        /// <summary>
        /// Gets the connection string builder instance.
        /// </summary>
        /// <value>
        /// The <see cref="SqlConnectionStringBuilder"/> instance used to connect to SQL Server.
        /// </value>
        public SqlConnectionStringBuilder ConnectionString => _connectString;
        /// <summary>
        /// Gets the reference to the database connection, schema and metadata information container.
        /// </summary>
        /// <value>
        /// An <see cref="DatabaseInfo"/> instance, or <b>null</b>.
        /// </value>
        public DatabaseInfo? DbInfo => _dbInfo;
        #endregion

        #region Public Methods / Functions

        #region C# Code DOM Operations
        /// <summary>
        /// Creates the data definition class.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the data definition for.
        /// </param>
        /// <param name="nameSpace">
        /// A string containing the namespace value to include the new class in.
        /// </param>
        /// <param name="parentClass">
        /// A string containing the name of the parent class the new instance derives from, or <b>null</b>.
        /// </param>
        /// <param name="disposable">
        /// A value indicating whether the new class is a <see cref="IDisposable"/> instance.
        /// </param>
        /// <returns>
        /// A string containing the C# code for the new class.
        /// </returns>
        public string? CreateDataDefinitionClass(SqlTable? table, string nameSpace, string? parentClass, bool disposable)
        {
            string? generatedCode = null;

            if (table != null && _dbInfo != null)
            {
                // Capture the object references and name values.
                AdaptiveTableProfile? profile = _dbInfo.GetTableProfile(table.TableName);
                if (profile != null)
                {
                    // Create the namespace container.
                    CodeNamespace nameSpaceContainer = new CodeNamespace(nameSpace);

                    // Create the class name.
                    string className = profile.DataDefinitionClassName!;

                    // Create the list of properties to be defined on the data definition class.
                    PropertyProfileCollection? propertyList = SqlCodeDomFactory.GeneratePropertyProfiles(_dbInfo, table.Columns, profile);

                    // Create the class definition.
                    //
                    // Pattern:
                    // public sealed class [TableName]
                    if (parentClass == null)
                    {
                        if (disposable)
                            parentClass = "IDisposable";
                        else
                            parentClass = string.Empty;
                    }
                    // public sealed class [TableName]Record 
                    // 
                    // OR 
                    // 
                    // public sealed class [TableName]Record : [User-Specified Parent Class]
                    CodeTypeDeclaration dataDefinitionClass = CodeDomFactory.CreateClass(className,
                    parentClass,
                    "Represents a record in the " + table.TableName + " table.", string.Empty);
                    nameSpaceContainer.Types.Add(dataDefinitionClass);

                    if (disposable)
                    {
                        //  protected override void Dispose(bool disposing)
                        //  {
                        CodeMemberMethod disposeMethod = CodeDomFactory.CreateDisposeMethod();
                        //  #region Dispose Method
                        disposeMethod.StartDirectives.Add(CodeDomFactory.StartRegion("Dispose Method"));
                        CodeDomFactory.CreateDisposeMethodContent(disposeMethod, propertyList);
                        dataDefinitionClass.Members.Add(disposeMethod);
                        //  }
                        //  #endregion
                        disposeMethod.EndDirectives.Add(CodeDomFactory.EndRegion());
                    }

                    // Create the properties.
                    if (propertyList != null && propertyList.Count > 0)
                    {
                        bool isFirst = true;
                        CodeMemberProperty? first = null;
                        CodeMemberProperty? last = null;

                        // Create the code from the property profiles, and remember which is the first property
                        // and which is the last for the region and endregion markings.
                        foreach (PropertyProfile propertyItem in propertyList)
                        {
                            CodeMemberProperty propertyDef = CodeDomFactory.CreatePropertyDefinition(propertyItem);
                            if (isFirst)
                            {
                                first = propertyDef;
                                isFirst = false;
                            }
                            last = propertyDef;
                            dataDefinitionClass.Members.Add(propertyDef);
                        }

                        //  #region Public Properties
                        if (first != null)
                            first.StartDirectives.Add(CodeDomFactory.StartRegion("Public Properties"));

                        //  #endregion
                        if (last != null)
                        {
                            last.EndDirectives.Add(CodeDomFactory.EndRegion());
                            last.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.None, string.Empty));
                        }
                        propertyList.Clear();
                    }

                    // Render the code.
                    generatedCode = CodeDomFactory.RenderCode(nameSpaceContainer);
                }
            }

            return generatedCode;
        }
        /// <summary>
        /// Creates the data access class.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the data access class for.
        /// </param>
        /// <returns>
        /// A string containing the C# code for the new class.
        /// </returns>
        public string? CreateDataAccessClass(SqlTable? table)
        {
            string? generatedCode = null;

            if (_dbInfo != null && table != null)
            {
                AdaptiveTableProfile? profile = _dbInfo.GetTableProfile(table.TableName);
                DataAccessClassBuilder builder = new DataAccessClassBuilder(_dbInfo, table, profile, "Data.Access");
                generatedCode = builder.GenerateDataAccessClass();
                builder.Dispose();
            }   

            return generatedCode;
        }
        #endregion

        #region SQL Query Operations
        /// <summary>
        /// Cancels the current database maintenance operation, if one is occurring.
        /// </summary>
        public async Task CancelDatabaseMaintenanceAsync()
        {
            if (_maintenanceProcessor != null)
                _maintenanceProcessor.Cancel = true;

            while (_maintenanceProcessor != null && _maintenanceProcessor.IsExecuting)
            {
                await Task.Yield();
            }
        }
        /// <summary>
        /// Copies the stored procedures from the primary database that are missing in the secondary database.
        /// </summary>
        /// <param name="connectionString">
        /// A <see cref="SqlConnectionStringBuilder"/> containing the connection parameters for SQL Server.
        /// </param>
        /// <param name="result">
        /// A <see cref="StoredProcedureAnalysisResult"/> instance containing the analysis result.
        /// </param>
        public async Task CopyMissingProceduresFromLeftAsync(SqlConnectionStringBuilder connectionString, StoredProcedureAnalysisResult? result)
        {
            if (result != null && result.Comparisons != null)
            {
                // Create the provider instances for the primary database.
                connectionString.InitialCatalog = result.PrimaryDatabaseName;
                SqlDataProvider primaryProvider = SqlDataProviderFactory.CreateProvider(connectionString);

                // Create the provider instances for the secondary database.
                connectionString.InitialCatalog = result.SecondaryDatabaseName;
                SqlDataProvider secondaryProvider = SqlDataProviderFactory.CreateProvider(connectionString);

                int count = 0;
                int len = result.Comparisons.Values.Count;
                // Iterate over the list of procedures in the primary database, copying any missing procs
                // to the secondary database.
                foreach (StoredProcedureComparisonResult compareResult in result.Comparisons.Values)
                {
                    count++;
                    string? currentProc = compareResult.StoredProcedureName;
                    if (currentProc != null)
                    {
                        OnStatusUpdate(
                            new ProgressUpdateEventArgs(
                                "Copying Procedures",
                            currentProc,
                        Adaptive.Math.Percent(count, len)));

                        // If this stored procedure is missing in the secondary database, copy the procedure to
                        // the secondary database.
                        if (compareResult.MissingInRight)
                        {
                            // Get the procedure text.
                            StringCollection? procedureText = result.PrimaryDatabaseProcedures![currentProc];
                            StringBuilder builder = new StringBuilder();
                            foreach (string line in procedureText)
                                builder.Append(line);

                            // Execute the stored procedure creation.
                            await secondaryProvider.ExecuteSqlAsync(builder.ToString()).ConfigureAwait(false);
                            builder.Clear();
                            procedureText.Clear();
                        }
                    }
                }

                // Disconnect and clear memory.
                primaryProvider.Dispose();
                secondaryProvider.Dispose();
            }
        }
        /// <summary>
        /// Copies the stored procedures from the secondary database that are missing in the primary database.
        /// </summary>
        /// <param name="connectionString">
        /// A <see cref="SqlConnectionStringBuilder"/> containing the connection parameters for SQL Server.
        /// </param>
        /// <param name="result">
        /// A <see cref="StoredProcedureAnalysisResult"/> instance containing the analysis result.
        /// </param>
        public async Task CopyMissingProceduresFromRightAsync(SqlConnectionStringBuilder connectionString, StoredProcedureAnalysisResult? result)
        {
            if (result != null && result.Comparisons != null)
            {
                // Create the provider instances for the primary database.
                connectionString.InitialCatalog = result.PrimaryDatabaseName;
                SqlDataProvider primaryProvider = SqlDataProviderFactory.CreateProvider(connectionString);

                // Create the provider instances for the secondary database.
                connectionString.InitialCatalog = result.SecondaryDatabaseName;
                SqlDataProvider secondaryProvider = SqlDataProviderFactory.CreateProvider(connectionString);

                int count = 0;
                int len = result.Comparisons.Values.Count;

                foreach (StoredProcedureComparisonResult compareResult in result.Comparisons.Values)
                {
                    count++;
                    string? currentProc = compareResult.StoredProcedureName;
                    if (currentProc != null)
                    {
                        OnStatusUpdate(new ProgressUpdateEventArgs(
                            "Copying Procedures",
                            currentProc,
                            Math.Percent(count, len)));


                        // If this stored procedure is missing in the primary database, copy the procedure from 
                        // the secondary database.
                        if (compareResult.MissingInLeft)
                        {
                            // Get the procedure text.
                            StringCollection? procedureText = result.SecondaryDatabaseProcedures![currentProc];
                            StringBuilder builder = new StringBuilder();
                            foreach (string line in procedureText)
                                builder.Append(line);

                            // Execute the stored procedure creation.
                            await primaryProvider.ExecuteSqlAsync(builder.ToString()).ConfigureAwait(false);
                            builder.Clear();
                            procedureText.Clear();
                        }
                    }
                }

                // Disconnect and clear memory.
                primaryProvider.Dispose();
                secondaryProvider.Dispose();
            }
        }
        /// <summary>
        /// Performs the database maintenance functions.
        /// </summary>
        /// <param name="passCount">
        /// An integer specifying the number of passes to perform.
        /// </param>
        public async Task PerformDatabaseMaintenanceAsync(int passCount)
        {
            if (_dbInfo != null)
            {
                if (_maintenanceProcessor == null)
                {
                    _maintenanceProcessor = new GeneralMaintenanceProcessor(_dbInfo.Provider);
                    _maintenanceProcessor.NumberOfPasses = passCount;
                    _maintenanceProcessor.StatusUpdate += HandleStatusUpdate;

                    try
                    {
                        await _maintenanceProcessor.PerformDatabaseMaintenanceAsync().ConfigureAwait(false);
                    }
                    catch(Exception ex)
                    {
                        ExceptionLog.LogException(ex);
                    }
                    _maintenanceProcessor.StatusUpdate -= HandleStatusUpdate;
                    _maintenanceProcessor.Dispose();
                    _maintenanceProcessor = null;
                }
            }
        }
        /// <summary>
        /// Attempts to connect to the specified database.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the SQL Server connection information.
        /// </param>
        /// <returns></returns>
        public async Task<bool> ConnectToDatabaseAsync(string connectionString)
        {
            bool success = false;

            if (!_connecting)
            {
                success = await ConnectToDatabaseAsync(new SqlConnectionStringBuilder(connectionString)).ConfigureAwait(false);
            }
            return success;
        }
        /// <summary>
        /// Attempts to connect to the specified database.
        /// </summary>
        /// <param name="connectionString">
        /// The <see cref="SqlConnectionStringBuilder"/> instance containing the SQL Server connection information.
        /// </param>
        /// <returns></returns>
        public async Task<bool> ConnectToDatabaseAsync(SqlConnectionStringBuilder connectionString)
        {
            bool success = false;

            if (!_connecting)
            {
                // Connect to the specified database.
                _connecting = true;
                _dbInfo = new DatabaseInfo();
                _dbInfo.StatusUpdate += HandleStatusUpdate;
                _connectString = connectionString;

                success = await _dbInfo.ConnectAsync(connectionString.ToString()).ConfigureAwait(false);

                if (!success)
                {
                    _dbInfo.StatusUpdate -= HandleStatusUpdate;
                    _dbInfo.Dispose();
                    _dbInfo = null;
                    OnDisconnected(EventArgs.Empty);
                }
                else
                {
                    // Create the Code DOM generator and store the database references and information globally.
                    _codeDom = new AdaptiveSqlCodeDomGenerator(_dbInfo.TableData);
                    OnConnnected(EventArgs.Empty);
                }
                _connecting = false;
            }
            return success;
        }
        /// <summary>
        /// Disconnects from the database if connected.
        /// </summary>
        public void Disconnect()
        {
            _codeDom?.Dispose();
            _codeDom = null;

            if (_dbInfo != null)
            {
                _dbInfo.StatusUpdate -= HandleStatusUpdate;
                _dbInfo.Dispose();
                _dbInfo = null;
                _connecting = false;
                _connectString?.Clear();
                _connectString = null;
                OnDisconnected(EventArgs.Empty);
            }
        }
        /// <summary>
        /// Attempts to drop the specified stored procedure.
        /// </summary>
        /// <param name="procedure">
        /// A <see cref="SqlStoredProcedure"/> instance representing the instance to be dropped.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation executes successfully; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> DropProcedureAsync(SqlStoredProcedure procedure)
        {
            bool success = false;

            if (_dbInfo != null && _dbInfo.Provider != null)
            {
                _dbInfo.Provider.ClearExceptions();

                // Create and execute the DROP PROCEDURE statement.
                string sql = "DROP PROCEDURE [" + procedure.Name + "]";
                IOperationalResult<int> result = await _dbInfo.Provider.ExecuteSqlAsync(sql).ConfigureAwait(false);
                success = result.Success;
                result.Dispose();
            }

            return success;
        }

        public async Task<DataTable?> GetDataTableAsync(string? sqlQuery)
        {
            DataTable? table = null;

            if (sqlQuery != null)
            {
                IOperationalResult<DataTable> result = await _dbInfo.Provider.FillDataTableAsync(sqlQuery).ConfigureAwait(false);
                if (result.Success)
                    table = result.DataContent;
                result.Dispose();
            }

            return table;
                
        }
        /// <summary>
        /// Executes the SQL query text.
        /// </summary>
        /// <param name="sqlQuery">
        /// A string containing the SQL query text to be executed.
        /// </param>
        /// <returns>
        /// A <see cref="UserSqlExecutionResult"/> instances describing the result of the operation.
        /// </returns>
        public async Task<UserSqlExecutionResult> ExecuteQueryAsync(string? sqlQuery)
        {
            UserSqlExecutionResult result = new UserSqlExecutionResult();

            if (sqlQuery != null && _dbInfo != null && _dbInfo.Provider != null)
            { 
                SqlQueryErrorCollection? errorList = await _dbInfo.Provider.ExecuteUserSqlAsync(sqlQuery).ConfigureAwait(false);
                if (errorList == null)
                {
                    // Everything worked.
                    result.Error = false;
                    result.Success = true;
                }
                else
                {
                    result.Error = true;
                    result.Success = false;
                    result.Errors = errorList;
                    if (_dbInfo.Provider.HasExceptions)
                    {
                        if (_dbInfo.Provider.Exceptions != null && _dbInfo.Provider.Exceptions.Count > 0)
                            result.Message = _dbInfo.Provider.Exceptions[0].Message;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Parses the SQL query text, to ensure it can be executed successfully.
        /// </summary>
        /// <param name="sqlQuery">
        /// A string containing the SQL query text to be parsed.
        /// </param>
        /// <returns>
        /// A <see cref="UserSqlExecutionResult"/> instances describing the result of the operation.
        /// </returns>
        public async Task<UserSqlExecutionResult> ParseQueryAsync(string? sqlQuery)
        {
            UserSqlExecutionResult result = new UserSqlExecutionResult();

            if (_dbInfo != null && _dbInfo.Provider != null)
            {
                SqlQueryErrorCollection? errorList = await _dbInfo.Provider.ParseSqlAsync(sqlQuery)
                    .ConfigureAwait(false);

                if (errorList == null)
                {
                    // Everything worked.
                    result.Error = false;
                    result.Success = true;
                }
                else
                {
                    result.Error = true;
                    result.Success = false;
                    result.Errors = errorList;
                    result.Message = _dbInfo.Provider.ExceptionMessages;
                }
            }
            return result;
        }
        /// <summary>
        /// If the object is currently connecting, waits until the operation completes.
        /// </summary>
        public async Task WaitForConnectAsync()
        {
            while (_connecting)
                await Task.Yield();
        }
        #endregion

        #region SQL Code DOM Operations
        /// <summary>
        /// Creates the SQL query text for the Delete() stored procedure.
        /// </summary>
        /// <remarks>
        /// This is used to generate a stored procedure for deleting a record in the table.
        /// </remarks>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the stored procedure for.
        /// </param>
        /// <returns>
        /// A string containing the SQL query text.
        /// </returns>
        public string CreateDeleteStoredProcedureText(SqlTable table)
        {
            SqlCodeCreateStoredProcedureStatement statement =
                _codeDom.CreateDeleteStoredProcedure(table, true);
            string sql = statement.ToString();
            statement.Dispose();
            return sql;
        }
        /// <summary>
        /// Creates the SQL query text for the GetAllRecords() stored procedure.
        /// </summary>
        /// <remarks>
        /// This is used to generate the get all records stored procedure for the table in the
        /// standard EasyVote CRUD operations.
        /// </remarks>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the stored procedure for.
        /// </param>
        /// <returns>
        /// A string containing the SQL query text.
        /// </returns>
        public string? CreateGetAllStoredProcedureText(SqlTable? table)
        {
            if (table == null)
                return null;

            SqlCodeCreateStoredProcedureStatement statement =
                _codeDom.CreateGetAllStoredProcedure(table, table.TableName + "GetAll");
            string sql = statement.ToString();
            statement.Dispose();
            return sql;
        }
        /// <summary>
        /// Creates the SQL query text for the GetById() stored procedure.
        /// </summary>
        /// <remarks>
        /// This is used to generate the get by id stored procedure for the table in the
        /// standard EasyVote CRUD operations.
        /// </remarks>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the stored procedure for.
        /// </param>
        /// <returns>
        /// A string containing the SQL query text.
        /// </returns>
        public string CreateGetByIdProcedureText(SqlTable table)
        {
            SqlCodeCreateStoredProcedureStatement statement =
                _codeDom.CreateGetByIdStoredProcedure(table);
            string sql = statement.ToString();
            statement.Dispose();
            return sql;
        }
        /// <summary>
        /// Creates the SQL query text for the Insert() stored procedure.
        /// </summary>
        /// <remarks>
        /// This is used to generate the Insert stored procedure for the table in the
        /// standard EasyVote CRUD operations.
        /// </remarks>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the stored procedure for.
        /// </param>
        /// <returns>
        /// A string containing the SQL query text.
        /// </returns>
        public string CreateInsertStoredProcedureText(SqlTable table)
        {
            SqlCodeCreateStoredProcedureStatement statement =
                _codeDom.CreateInsertStoredProcedure(table);
            string sql = statement.ToString();
            statement.Dispose();
            return sql;
        }
        /// <summary>
        /// Creates the SQL query text for the Update() stored procedure.
        /// </summary>
        /// <remarks>
        /// This is used to generate the Update stored procedure for the table in the
        /// standard EasyVote CRUD operations.
        /// </remarks>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the stored procedure for.
        /// </param>
        /// <returns>
        /// A string containing the SQL query text.
        /// </returns>
        public string CreateUpdateStoredProcedureText(SqlTable table)
        {
            SqlCodeCreateStoredProcedureStatement statement =
                _codeDom.CreateUpdateStoredProcedure(table);
            string sql = statement.ToString();
            statement.Dispose();
            return sql;
        }
        /// <summary>
        /// Generates the T-SQL script for creating the table.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> to render a create script for.
        /// </param>
        /// <returns>
        /// A string containing the SQL script, if successful; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<string> GenerateCreateScriptAsync(SqlTable table)
        {
            SqlDataProvider provider = SqlDataProviderFactory.CreateProvider(_dbInfo.ConnectionString);
            string script = await table.GenerateCreateScriptAsync(provider);
            provider.Dispose();
            return script;
        }
        #endregion

        #region File I/O
        /// <summary>
        /// Loads the SQL query text from the specified file.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the fully-qualified path and name of the file to read.
        /// </param>
        /// <returns>
        /// A string containing the text, if successful; otherwise, returns <b>null</b>.
        /// </returns>
        public string LoadSqlFile(string fileName)
        {
            return SafeIO.ReadTextFromFile(fileName, false);
        }
        /// <summary>
        /// Saves the table profile content to a local file.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, <b>false</b>.
        /// </returns>
        public bool SaveProfiles()
        {
            return _dbInfo.TableData.Profiles.Save();
        }
        /// <summary>
        /// Saves the table profile content to a local file.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, <b>false</b>.
        /// </returns>
        public Task<bool> SaveProfilesAsync()
        {
            return _dbInfo.TableData.Profiles.SaveAsync();
        }
        /// <summary>
        /// Saves the SQL query text to the specified file.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the fully-qualified path and name of the file to read.
        /// </param>
        /// <param name="sql">
        /// A string containing the text, if successful; otherwise, returns <b>null</b>.
        /// </param>
        public void SaveSqlFile(string fileName, string sql)
        {
            if (SafeIO.FileExists(fileName))
                SafeIO.DeleteFile(fileName);

            using (TextFile file = new TextFile(fileName))
            {
                file.WriteLine(sql);
                file.Close();
            }
        }
        #endregion

        #region Business Object Operations
        /// <summary>
        /// Analyzes the stored procedures across the three databases to determine differences and find stored
        /// procedures present in one or more databases that are missing in another.
        /// </summary>
        /// <param name="leftDatabase">
        /// A string containing the name of the first / left database to compare.
        /// </param>
        /// <param name="rightDatabase">
        /// A string containing the name of the second / right database to compare.
        /// </param>
        /// <returns>
        /// A <see cref="StoredProcedureAnalysisResult"/> containing the results of the analysis.
        /// </returns>
        public async Task<StoredProcedureAnalysisResult> AnalyzeStoredProceduresAsync(string leftDatabase, string rightDatabase)
        {
            StoredProcedureAnalysisResult result = new StoredProcedureAnalysisResult();

            // Download the content of all the stored procedures in each database.
            OnStatusUpdate(new ProgressUpdateEventArgs($"Reading Procedures From {leftDatabase}", null, 0));
            result.PrimaryDatabaseProcedures = await _dbInfo.DownloadAllStoredProceduresAsync(leftDatabase);

            OnStatusUpdate(new ProgressUpdateEventArgs($"Reading Procedures From {rightDatabase}", null, 0));
            result.SecondaryDatabaseProcedures = await _dbInfo.DownloadAllStoredProceduresAsync(rightDatabase);

            OnStatusUpdate(new ProgressUpdateEventArgs("Analyzing...", null, 0));
            result.Analyze(leftDatabase, rightDatabase);
            return result;

        }
        #endregion

        #endregion

        #region Event / Event Handler Methods
        /// <summary>
        /// Raises the <see cref="StatusUpdate" /> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="ProgressUpdateEventArgs"/> instance containing the event data.
        /// </param>
        private void OnStatusUpdate(ProgressUpdateEventArgs e)
        {
            StatusUpdate?.Invoke(this, e);
        }
        /// <summary>
        /// Handles the status update event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ProgressUpdateEventArgs"/> instance containing the event data.</param>
        private void HandleStatusUpdate(object sender, ProgressUpdateEventArgs e)
        {
            OnStatusUpdate(e);
        }
        /// <summary>
        /// Raises the <see cref="E:Connnected" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnConnnected(EventArgs e)
        {
            Connected?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the <see cref="E:Disconnected" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnDisconnected(EventArgs e)
        {
            Disconnected?.Invoke(this, e);
        }
        #endregion

        #region Public Clone Methods
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public ContextOperations Clone()
        {
            ContextOperations ops = new ContextOperations();
            ops.ConnectToDatabaseAsync(_connectString.ToString());
            return ops;
        }
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion
    }
}