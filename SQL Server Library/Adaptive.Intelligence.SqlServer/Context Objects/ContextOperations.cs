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
using System.CodeDom;
using System.Collections.Specialized;
using System.Data;
using System.Text;

namespace Adaptive.Intelligence.SqlServer
{
	/// <summary>
	/// Provides a central container for SQL Server operations within an application.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class ContextOperations : DisposableObjectBase
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
        private DatabaseInfoCollection? _dbInfoList;
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
            _connectString = new SqlConnectionStringBuilder();
		}
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextOperations"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public ContextOperations(string connectionString) : this()
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
                _dbInfoList?.Clear();
                _maintenanceProcessor?.Dispose();
                _connectString?.Clear();
            }

            _connectString = null;
            _maintenanceProcessor = null;
            _dbInfoList = null;
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
        public bool IsConnected => _dbInfoList != null;
        /// <summary>
        /// Gets the connection string builder instance.
        /// </summary>
        /// <value>
        /// The <see cref="SqlConnectionStringBuilder"/> instance used to connect to SQL Server.
        /// </value>
        public SqlConnectionStringBuilder ConnectionString
        {
            get
            {
                if (_connectString == null)
                    _connectString = new SqlConnectionStringBuilder();
                return _connectString;
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Gets the reference to the database connection, schema and metadata information container.
        /// </summary>
        /// <value>
        /// An <see cref="DatabaseInfo"/> instance, or <b>null</b>.
        /// </value>
        public DatabaseInfo? GetDbInfo(string databaseName)
        {
            if (_dbInfoList == null)
                return null;
            else
                return _dbInfoList[databaseName];
        }

    	#region C# Code DOM Operations
	    /// <summary>
	    /// Creates the data definition class.
	    /// </summary>
        /// <param name="databaseName">
        /// The name of the database in which the table is contained.
        /// </param>
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
	    public string? CreateDataDefinitionClass(string databaseName, SqlTable? table, string nameSpace, string? parentClass, bool disposable)
        {
            string? generatedCode = null;

            DatabaseInfo? dbInfo = GetDbInfo(databaseName);
            if (table != null && dbInfo != null)
            {
                // Capture the object references and name values.
                AdaptiveTableProfile? profile = dbInfo.GetTableProfile(table.TableName);
                if (profile != null)
                {
                    // Create the namespace container.
                    CodeNamespace nameSpaceContainer = new CodeNamespace(nameSpace);

                    // Create the class name.
                    string className = profile.DataDefinitionClassName!;

                    // Create the list of properties to be defined on the data definition class.
                    PropertyProfileCollection? propertyList = SqlCodeDomFactory.GeneratePropertyProfiles(dbInfo, table.Columns, profile);

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
		/// <param name="databaseName">
		/// The name of the database in which the table is contained.
		/// </param>
		/// <param name="table">
		/// The <see cref="SqlTable"/> instance to create the data access class for.
		/// </param>
		/// <returns>
		/// A string containing the C# code for the new class.
		/// </returns>
		public string? CreateDataAccessClass(string databaseName, SqlTable? table)
        {
            string? generatedCode = null;
			DatabaseInfo? dbInfo = GetDbInfo(databaseName);

			if (dbInfo != null && table != null)
            {
                AdaptiveTableProfile? profile = dbInfo.GetTableProfile(table.TableName);
                DataAccessClassBuilder builder = new DataAccessClassBuilder(dbInfo, table, profile, "Data.Access");
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
        public async Task PerformDatabaseMaintenanceAsync(string databaseName, int passCount)
        {
			DatabaseInfo? dbInfo = GetDbInfo(databaseName);
			if (dbInfo != null)
            {
                if (_maintenanceProcessor == null)
                {
                    _maintenanceProcessor = new GeneralMaintenanceProcessor(dbInfo.Provider);
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
		/// Connects to the specified SQL Server.
		/// </summary>
		/// <param name="connectionString">
		/// A string containing the SQL Server connection information.
		/// </param>
		/// <returns>
        /// <b>true</b> if the operation completes successfully; otherwise, returns <b>false</b>.
        /// </returns>
		public async Task<bool> ConnectToSqlServerAsync(string connectionString)
        {
            bool success = false;

            // Connect to the master database and get a list of databases.
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog = "master";
			Schema.SqlServer newServer = new Schema.SqlServer(builder.DataSource);
            List<string>? dbList = await newServer.LoadDatabaseNamesAsync(builder.ToString());
            if (dbList != null)
            {
                _dbInfoList = new DatabaseInfoCollection();
				// Iterate over the list of databases and connect to each one.

				foreach (string dbName in dbList)
                {
                    builder.InitialCatalog = dbName;
					DatabaseInfo info = new DatabaseInfo();
                    await info.ConnectAsync(builder.ToString()).ConfigureAwait(false);
                    _dbInfoList.Add(info);
                }
            }

			return success;
        }
        /// <summary>
        /// Disconnects from the database if connected.
        /// </summary>
        public void Disconnect()
        {
            if (_dbInfoList != null)
            {
                _dbInfoList.Clear();
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
        public async Task<bool> DropProcedureAsync(string databaseName, SqlStoredProcedure procedure)
        {
            bool success = false;

			DatabaseInfo? dbInfo = GetDbInfo(databaseName);
			if (dbInfo != null && dbInfo.Provider != null)
            {
				dbInfo.Provider.ClearExceptions();

                // Create and execute the DROP PROCEDURE statement.
                string sql = "DROP PROCEDURE [" + procedure.Name + "]";
                IOperationalResult<int> result = await dbInfo.Provider.ExecuteSqlAsync(sql).ConfigureAwait(false);
                success = result.Success;
                result.Dispose();
            }

            return success;
        }
		/// <summary>
		/// Gets the data table object populated with the data result from the query.
		/// </summary>
		/// <param name="databaseName">
        /// A string containing the name of the database.
        /// </param>
		/// <param name="sqlQuery">
        /// A string containing the SQL SELECT query to execute.
        /// </param>
		/// <returns>
        /// A <see cref="DataTable"/> containing the data, if successful; otherwise, returns <b>null</b>.
        /// </returns>
		public async Task<DataTable?> GetDataTableAsync(string databaseName, string? sqlQuery)
        {
            DataTable? table = null;

			DatabaseInfo? dbInfo = GetDbInfo(databaseName);
			if (sqlQuery != null)
            {
                IOperationalResult<DataTable> result = await dbInfo.Provider.FillDataTableAsync(sqlQuery).ConfigureAwait(false);
                if (result.Success)
                    table = result.DataContent;
                result.Dispose();
            }
            return table;
        }
		/// <summary>
		/// Executes the SQL query text.
		/// </summary>
		/// <param name="databaseName">
		/// A string containing the name of the database.
		/// </param>
		/// <param name="sqlQuery">
		/// A string containing the SQL query text to be executed.
		/// </param>
		/// <returns>
		/// A <see cref="UserSqlExecutionResult"/> instances describing the result of the operation.
		/// </returns>
		public async Task<UserSqlExecutionResult> ExecuteQueryAsync(string databaseName, string? sqlQuery)
        {
            UserSqlExecutionResult result = new UserSqlExecutionResult();

			DatabaseInfo? dbInfo = GetDbInfo(databaseName);
			if (sqlQuery != null && dbInfo != null && dbInfo.Provider != null)
            { 
                SqlQueryErrorCollection? errorList = await dbInfo.Provider.ExecuteUserSqlAsync(sqlQuery).ConfigureAwait(false);
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
                    if (dbInfo.Provider.HasExceptions)
                    {
                        if (dbInfo.Provider.Exceptions != null && dbInfo.Provider.Exceptions.Count > 0)
                            result.Message = dbInfo.Provider.Exceptions[0].Message;
                    }
                }
            }
            return result;
        }
		/// <summary>
		/// Parses the SQL query text, to ensure it can be executed successfully.
		/// </summary>
		/// <param name="databaseName">
		/// A string containing the name of the database.
		/// </param>
		/// <param name="sqlQuery">
		/// A string containing the SQL query text to be parsed.
		/// </param>
		/// <returns>
		/// A <see cref="UserSqlExecutionResult"/> instances describing the result of the operation.
		/// </returns>
		public async Task<UserSqlExecutionResult> ParseQueryAsync(string databaseName, string? sqlQuery)
        {
            UserSqlExecutionResult result = new UserSqlExecutionResult();

			DatabaseInfo? dbInfo = GetDbInfo(databaseName);
			if (dbInfo != null && dbInfo.Provider != null)
            {
                SqlQueryErrorCollection? errorList = await dbInfo.Provider.ParseSqlAsync(sqlQuery)
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
                    result.Message = dbInfo.Provider.ExceptionMessages;
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
        /// <param name="databaseName">
        /// A string containing the name of the database to operate in.
        /// </param>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance to create the stored procedure for.
        /// </param>
        /// <returns>
        /// A string containing the SQL query text.
        /// </returns>
        public string? CreateDeleteStoredProcedureText(string databaseName, SqlTable? table)
        {
            if (table == null)
                return null;

            string sql = string.Empty;

			DatabaseInfo? dbInfo = GetDbInfo(databaseName);
            if (dbInfo != null)
            {
                AdaptiveSqlCodeDomGenerator codeDom = new AdaptiveSqlCodeDomGenerator(dbInfo.TableData);
                SqlCodeCreateStoredProcedureStatement? statement = codeDom.CreateDeleteStoredProcedure(table, true);
                if (statement != null)
                {
                    sql = statement.ToString();
                    statement.Dispose();
                }
                codeDom.Dispose();
            }
            return sql;
        }
		/// <summary>
		/// Creates the SQL query text for the GetAllRecords() stored procedure.
		/// </summary>
		/// <remarks>
		/// This is used to generate a stored procedure for reading all records in a table.
		/// </remarks>
		/// <param name="databaseName">
		/// A string containing the name of the database to operate in.
		/// </param>
		/// <param name="table">
		/// The <see cref="SqlTable"/> instance to create the stored procedure for.
		/// </param>
		/// <returns>
		/// A string containing the SQL query text.
		/// </returns>
		public string? CreateGetAllStoredProcedureText(string databaseName, SqlTable? table)
        {
            if (table == null)
                return null;

			string sql = string.Empty;

			DatabaseInfo? dbInfo = GetDbInfo(databaseName);
			if (dbInfo != null)
			{
				AdaptiveSqlCodeDomGenerator codeDom = new AdaptiveSqlCodeDomGenerator(dbInfo.TableData);
                SqlCodeCreateStoredProcedureStatement? statement = codeDom.CreateGetAllStoredProcedure(table);
				if (statement != null)
				{
					sql = statement.ToString();
					statement.Dispose();
				}
				codeDom.Dispose();
			}
			return sql;
        }
		/// <summary>
		/// Creates the SQL query text for the GetById() stored procedure.
		/// </summary>
		/// <remarks>
		/// This is used to generate a stored procedure for reading a record from a table by ID value.
		/// </remarks>
		/// <param name="databaseName">
		/// A string containing the name of the database to operate in.
		/// </param>
		/// <param name="table">
		/// The <see cref="SqlTable"/> instance to create the stored procedure for.
		/// </param>
		/// <returns>
		/// A string containing the SQL query text.
		/// </returns>
		public string? CreateGetByIdProcedureText(string databaseName, SqlTable? table)
        {
			if (table == null)
				return null;

			string sql = string.Empty;

			DatabaseInfo? dbInfo = GetDbInfo(databaseName);
			if (dbInfo != null)
			{
				AdaptiveSqlCodeDomGenerator codeDom = new AdaptiveSqlCodeDomGenerator(dbInfo.TableData);
                SqlCodeCreateStoredProcedureStatement? statement = codeDom.CreateGetByIdStoredProcedure(table);
				if (statement != null)
				{
					sql = statement.ToString();
					statement.Dispose();
				}
				codeDom.Dispose();
			}
			return sql;
        }
		/// <summary>
		/// Creates the SQL query text for the Insert() stored procedure.
		/// </summary>
		/// <remarks>
		/// This is used to generate a stored procedure for inserting a record into a table.
		/// </remarks>
		/// <param name="table">
		/// The <see cref="SqlTable"/> instance to create the stored procedure for.
		/// </param>
		/// <returns>
		/// A string containing the SQL query text.
		/// </returns>
		public string? CreateInsertStoredProcedureText(string databaseName, SqlTable table)
        {
			if (table == null)
				return null;

			string sql = string.Empty;

			DatabaseInfo? dbInfo = GetDbInfo(databaseName);
			if (dbInfo != null)
			{
				AdaptiveSqlCodeDomGenerator codeDom = new AdaptiveSqlCodeDomGenerator(dbInfo.TableData);
				SqlCodeCreateStoredProcedureStatement? statement = codeDom.CreateInsertStoredProcedure(table);
				if (statement != null)
				{
					sql = statement.ToString();
					statement.Dispose();
				}
				codeDom.Dispose();
			}
			return sql;
        }
		/// <summary>
		/// Creates the SQL query text for the Update() stored procedure.
		/// </summary>
		/// <remarks>
		/// This is used to generate a stored procedure for updating a record in a table.
		/// </remarks>
		/// <param name="table">
		/// The <see cref="SqlTable"/> instance to create the stored procedure for.
		/// </param>
		/// <returns>
		/// A string containing the SQL query text.
		/// </returns>
		public string? CreateUpdateStoredProcedureText(string databaseName, SqlTable table)
        {
			if (table == null)
				return null;

			string sql = string.Empty;

			DatabaseInfo? dbInfo = GetDbInfo(databaseName);
			if (dbInfo != null)
			{
				AdaptiveSqlCodeDomGenerator codeDom = new AdaptiveSqlCodeDomGenerator(dbInfo.TableData);
				SqlCodeCreateStoredProcedureStatement? statement = codeDom.CreateUpdateStoredProcedure(table);
				if (statement != null)
				{
					sql = statement.ToString();
					statement.Dispose();
				}
				codeDom.Dispose();
			}
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
        public async Task<string?> GenerateCreateScriptAsync(string databaseName, SqlTable table)
        {
            string? script = null;
            DatabaseInfo? dbInfo = GetDbInfo(databaseName);
            if (dbInfo != null && dbInfo.ConnectionString != null)
            {
                SqlDataProvider provider = SqlDataProviderFactory.CreateProvider(dbInfo.ConnectionString);
                script = await table.GenerateCreateScriptAsync(provider).ConfigureAwait(false);
                provider.Dispose();
            }

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
        public string? LoadSqlFile(string fileName)
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
            bool success = true;
            if (_dbInfoList != null)
            {
                foreach (DatabaseInfo dbInfo in _dbInfoList)
                {
                    success = success && dbInfo.TableData.Profiles.Save();
                }
            }
            return success;
        }
        /// <summary>
        /// Saves the table profile content to a local file.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, <b>false</b>.
        /// </returns>
        public async Task<bool> SaveProfilesAsync()
        {
			bool success = true;
			if (_dbInfoList != null)
			{
				foreach (DatabaseInfo dbInfo in _dbInfoList)
				{
                    success = success && await dbInfo.TableData.Profiles.SaveAsync().ConfigureAwait(false);
				}
			}
			return success;
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
            DatabaseInfo? leftDbInfo = GetDbInfo(leftDatabase);
			DatabaseInfo? rightDbInfo = GetDbInfo(leftDatabase);
			StoredProcedureAnalysisResult result = new StoredProcedureAnalysisResult();

            if (leftDbInfo != null && rightDbInfo != null)
            {
                // Download the content of all the stored procedures in each database.
                OnStatusUpdate(new ProgressUpdateEventArgs($"Reading Procedures From {leftDatabase}", null, 0));
                result.PrimaryDatabaseProcedures = await leftDbInfo.DownloadAllStoredProceduresAsync(leftDatabase);

                OnStatusUpdate(new ProgressUpdateEventArgs($"Reading Procedures From {rightDatabase}", null, 0));
                result.SecondaryDatabaseProcedures = await rightDbInfo.DownloadAllStoredProceduresAsync(rightDatabase);

                OnStatusUpdate(new ProgressUpdateEventArgs("Analyzing...", null, 0));
                result.Analyze(leftDatabase, rightDatabase);
            }

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
    }
}