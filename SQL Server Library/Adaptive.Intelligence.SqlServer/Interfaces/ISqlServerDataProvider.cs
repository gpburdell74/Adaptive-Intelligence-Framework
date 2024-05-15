using Adaptive.Intelligence.Shared;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Adaptive.SqlServer.Client
{
    /// <summary>
    /// Provides the signature definitions for methods and functions for performing standard data source
    /// operations and creating standard data objects for a SQL Server client.
    /// </summary>
    /// <remarks>
    /// This data provider instance is explicitly coupled to the SQL Server connection and related objects
    /// since this is a SQL Server data source client.
    /// </remarks>
    public interface ISqlServerDataProvider : IExceptionTracking
	{
		#region Public Properties
		/// <summary>
		/// Gets the connection string being used.
		/// </summary>
		/// <value>
		/// A string containing the connection string value to connect to the data source.
		/// </value>
		string? ConnectionString { get; }
        #endregion

        #region Public Methods / Functions

        #region SQL Connection Object Methods
        /// <summary>
        /// Attempts to connect the specified connection object to the
        /// data source.
        /// </summary>
        /// <param name="connection">
        /// The <see cref="SqlConnection"/> instance to use.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult"/> containing the result of the operation.
        /// </returns>
        Task<IOperationalResult<SqlConnection>> ConnectInstanceAsync(SqlConnection connection);
        /// <summary>
        /// Attempts to create and open the SQL Server connection object.
        /// </summary>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the connected
        /// <see cref="SqlConnection"/> instance to use or the exception
        /// if the operation fails.
        /// </returns>
        IOperationalResult<SqlConnection> CreateAndOpenConnection();
        /// <summary>
        /// Attempts to create and open the SQL Server connection object.
        /// </summary>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the connected
        /// <see cref="SqlConnection"/> instance to use or the exception
        /// if the operation fails.
        /// </returns>
        Task<IOperationalResult<SqlConnection>> CreateAndOpenConnectionAsync();
        /// <summary>
        /// Creates a disconnected version of the data source connection object to use.
        /// </summary>
        /// <returns>
        /// A disconnected <see cref="SqlConnection"/> instance.
        /// </returns>
        SqlConnection? CreateConnectionInstance();
		#endregion

		#region SQL Command Object Methods

		#region Create Command Methods
		/// <summary>
		/// Creates the SQL command instance for the provided query,
		/// </summary>
		/// <param name="query">
		/// A string containing the SQL query to be executed.
		/// </param>
		/// <returns>
		/// A <see cref="IOperationalResult{T}"/> containing the <see cref="SqlCommand"/>
		/// instance.
		/// </returns>
		IOperationalResult<SqlCommand> CreateCommandInstance(string query);
		/// <summary>
		/// Creates the SQL command instance for the provided query and set of parameters.
		/// </summary>
		/// <param name="query">
		/// A string containing the SQL query to be executed.
		/// </param>
		/// <param name="parameterList">
		/// An <see cref="IEnumerable{T}"/> of <see cref="SqlParameter"/> instances
		/// to add to the <see cref="SqlCommand"/>.
		/// </param>
		/// <returns>
		/// A <see cref="IOperationalResult{T}"/> containing the <see cref="SqlCommand"/>
		/// instance.
		/// </returns>
		IOperationalResult<SqlCommand> CreateCommandInstance(string query, IEnumerable<SqlParameter> parameterList);
        /// <summary>
        /// Creates the SQL command instance for the provided query using the specified connection instance.
        /// </summary>
        /// <param name="query">
        /// A string containing the SQL query to be executed.
        /// </param>
        /// <param name="connection">
        /// The <see cref="SqlConnection"/> instance to attach to the command.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the The constructed <see cref="SqlCommand" /> instance with the maximum
        /// command timeout value.
        /// </returns>
        IOperationalResult<SqlCommand> CreateCommandInstance(string query, SqlConnection connection);
        /// <summary>
        /// Creates the command instance for the provided query.
        /// </summary>
        /// <param name="query">
        /// A string containing the SQL query to be executed.
        /// </param>
        /// <param name="createConnection">
        /// <b>true</b> to create the SQL connection instance when creating the command object.
        /// </param>
        /// <param name="connectionString">
        /// A string containing the connection data for connecting to a SQL Server.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the <see cref="SqlCommand"/> instance.
        /// </returns>
        Task<IOperationalResult<SqlCommand>> CreateCommandInstanceAsync(string query, bool createConnection, string connectionString);
		#endregion

		#region Execute Command Methods		
		/// <summary>
		/// Attempts to execute the list of SQL statements as a batch operation.
		/// </summary>
		/// <remarks>
		/// Implementations of this method should wrap these executions within a transaction.
		/// </remarks>
		/// <param name="sqlStatements">
		/// An <see cref="IEnumerable{T}"/> list of <see cref="string"/> values containing the SQL Statements to be executed.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="int"/> containing the result of the operation.
		/// If successful, the result returns the number of rows affected.
		/// </returns>
		IOperationalResult<int> ExecuteBatch(IEnumerable<string> sqlStatements);
        /// <summary>
        /// Attempts to execute the list of SQL statements as a batch operation.
        /// </summary>
        /// <remarks>
        /// Implementations of this method should wrap these executions within a transaction.
        /// </remarks>
        /// <param name="commands">
        /// An <see cref="IEnumerable{T}"/> list of <see cref="SqlCommand"/> instances containing the SQL Statements to be executed.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> of <see cref="int"/> containing the result of the operation.
        /// If successful, the result returns the number of rows affected.
        /// </returns>
        IOperationalResult<int> ExecuteBatch(IEnumerable<SqlCommand> commands);
        /// <summary>
        /// Attempts to execute the list of SQL statements as a batch operation.
        /// </summary>
        /// <remarks>
        /// Implementations of this method should wrap these executions within a transaction.
        /// </remarks>
        /// <param name="commands">
        /// An <see cref="IEnumerable{T}"/> list of <see cref="string"/> values containing the SQL Statements to be executed.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> of <see cref="int"/> containing the result of the operation.
        /// If successful, the result returns the number of rows affected.
        /// </returns>
        Task<IOperationalResult<int>> ExecuteBatchAsync(IEnumerable<string> commands);
		/// <summary>
		/// Attempts to execute the list of SQL statements as a batch operation.
		/// </summary>
		/// <remarks>
		/// Implementations of this method should wrap these executions within a transaction.
		/// </remarks>
		/// <param name="commands">
		/// An <see cref="IEnumerable{T}"/> list of <see cref="SqlCommand"/> instances containing the SQL Statements to be executed.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="int"/> containing the result of the operation.
		/// If successful, the result returns the number of rows affected.
		/// </returns>
		Task<IOperationalResult<int>> ExecuteBatchAsync(IEnumerable<SqlCommand> commands);
		/// <summary>
		/// Executes the specified SQL command object.
		/// </summary>
		/// <param name="command">
		/// The <see cref="SqlCommand"/> instance to be executed.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="int"/> containing the result of the operation.
		/// If successful, the result returns the number of rows affected.
		/// </returns>
		IOperationalResult<int> ExecuteCommand(SqlCommand command);
		/// <summary>
		/// Executes the specified SQL command object.
		/// </summary>
		/// <param name="command">
		/// The <see cref="SqlCommand"/> instance to be executed.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="int"/> containing the result of the operation.
		/// If successful, the result returns the number of rows affected.
		/// </returns>
		Task<IOperationalResult<int>> ExecuteCommandAsync(SqlCommand command);
		/// <summary>
		/// Executes the list of SQL commands within the specified transaction.
		/// </summary>
		/// <param name="transaction">
		/// The <see cref="SqlTransaction"/> instance to be used.
		/// </param>
		/// <param name="commandList">
		/// The <see cref="IEnumerable{T}"/> list of <see cref="SqlCommand"/> instances to be executed.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult"/> containing the result of the operation.  If the operation fails,
		/// the transaction is rolled back.
		/// </returns>
		IOperationalResult ExecuteInTransaction(SqlTransaction transaction, IEnumerable<SqlCommand> commandList);
        /// <summary>
        /// Executes the list of SQL commands within the specified transaction.
        /// </summary>
        /// <param name="transaction">
        /// The <see cref="SqlTransaction"/> instance to be used.
        /// </param>
        /// <param name="statementList">
        /// The <see cref="IEnumerable{T}"/> list of <see cref="string"/> containing the SQL Statements to be executed.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult"/> containing the result of the operation.  If the operation fails,
        /// the transaction is rolled back.
        /// </returns>
        IOperationalResult ExecuteInTransaction(SqlTransaction transaction, IEnumerable<string> statementList);
        /// <summary>
        /// Executes the list of SQL commands within the specified transaction.
        /// </summary>
        /// <param name="transaction">
        /// The <see cref="SqlTransaction"/> instance to be used.
        /// </param>
        /// <param name="commandList">
        /// The <see cref="IEnumerable{T}"/> list of <see cref="SqlCommand"/> instances to be executed.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult"/> containing the result of the operation.  If the operation fails,
        /// the transaction is rolled back.
        /// </returns>
        Task<IOperationalResult> ExecuteInTransactionAsync(SqlTransaction transaction, IEnumerable<SqlCommand> commandList);
		/// <summary>
		/// Executes the list of SQL commands within the specified transaction.
		/// </summary>
		/// <param name="transaction">
		/// The <see cref="SqlTransaction"/> instance to be used.
		/// </param>
		/// <param name="statementList">
		/// The <see cref="IEnumerable{T}"/> list of <see cref="string"/> containing the SQL Statements to be executed.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult"/> containing the result of the operation.  If the operation fails,
		/// the transaction is rolled back.
		/// </returns>
		Task<IOperationalResult> ExecuteInTransactionAsync(SqlTransaction transaction, IEnumerable<string> statementList);
		/// <summary>
		/// Attempts to execute the specified SQL command text with the supplied
		/// parameters.
		/// </summary>
		/// <param name="sqlText">
		/// A string containing the SQL to be executed.
		/// </param>
		/// <param name="parameterList">
		/// An <see cref="IEnumerable{T}"/> of <see cref="SqlParameter"/> instances to send to the command.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult"/> instance containing the result of the operation.
		/// </returns>
		IOperationalResult<int> ExecuteParameterizedSql(string sqlText, IEnumerable<SqlParameter> parameterList);
		/// <summary>
		/// Attempts to execute the specified SQL command text with the supplied
		/// parameters.
		/// </summary>
		/// <param name="sqlText">
		/// A string containing the SQL to be executed.
		/// </param>
		/// <param name="parameterList">
		/// An <see cref="IEnumerable{T}"/> of <see cref="SqlParameter"/> instances to send to the command.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult"/> instance containing the result of the operation.
		/// </returns>
		Task<IOperationalResult<int>> ExecuteParameterizedSqlAsync(string sqlText, IEnumerable<SqlParameter> parameterList);
		/// <summary>
		/// Attempts to execute the supplied SQL statement.
		/// </summary>
		/// <param name="sql">
		/// A string containing the SQL statement to be executed.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="int"/> containing the result of the operation.
		/// If successful, the result contains an integer indicating the number of affected rows.
		/// </returns>
		IOperationalResult<int> ExecuteSql(string sql);
		/// <summary>
		/// Attempts to execute the supplied SQL statement.
		/// </summary>
		/// <param name="sql">
		/// A string containing the SQL statement to be executed.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="int"/> containing the result of the operation.
		/// If successful, the result contains an integer indicating the number of affected rows.
		/// </returns>
		Task<IOperationalResult<int>> ExecuteSqlAsync(string sql);
		/// <summary>
		/// Attempts to execute the specified stored procedure with the specified
		/// parameters.
		/// </summary>
		/// <param name="storedProcedure">
		/// A string containing the name of the SQL stored procedure to be executed.
		/// </param>
		/// <param name="parameterList">
		/// An <see cref="IEnumerable{T}"/> of <see cref="SqlParameter"/> instances to send to the command.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="int"/> containing the result of the operation.
		/// If successful, the result contains an integer indicating the number of affected rows.
		/// </returns>
		IOperationalResult<int> ExecuteStoredProcedure(string storedProcedure, IEnumerable<SqlParameter> parameterList);
		/// <summary>
		/// Attempts to execute the specified stored procedure with the specified
		/// parameters.
		/// </summary>
		/// <param name="storedProcedure">
		/// A string containing the name of the SQL stored procedure to be executed.
		/// </param>
		/// <param name="parameterList">
		/// An <see cref="IEnumerable{T}"/> of <see cref="SqlParameter"/> instances to send to the command.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="int"/> containing the result of the operation.
		/// If successful, the result contains an integer indicating the number of affected rows.
		/// </returns>
		Task<IOperationalResult<int>> ExecuteStoredProcedureAsync(string storedProcedure, IEnumerable<SqlParameter> parameterList);
		#endregion

		#region SQL Adapter Object Methods
		/// <summary>
		/// Creates a data adapter instance for the provided SQL command instance.
		/// </summary>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="SqlDataAdapter"/> containing the result of the operation.
		/// If successful, the result returns the <see cref="SqlDataAdapter"/> instance.
		/// </returns>
		IOperationalResult<SqlDataAdapter> CreateAdapterInstance(SqlCommand command);
		#endregion

		#region SQL Data Reader Object Methods
		/// <summary>
		/// Executes the supplied SQL Query and returns the resulting content in a data table.
		/// </summary>
		/// <param name="command">
		/// The <see cref="SqlCommand"/> to be executed.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="DataTable"/> containing the result of the operation.
		/// If successful, the result returns the <see cref="DataTable"/> resulting from the query.
		/// </returns>
		IOperationalResult<DataTable> FillDataTable(SqlCommand command);
		/// <summary>
		/// Executes the supplied SQL Query and returns the resulting SQL data reader.
		/// </summary>
		/// <param name="sqlCommand">
		/// A string containing the SQL to be executed.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="SqlDataReader"/> containing the result of the operation.
		/// If successful, the result returns a the <see cref="SqlDataReader"/> resulting from the query.
		/// </returns>
		IOperationalResult<ISafeSqlDataReader> GetDataReader(string sqlCommand);
        /// <summary>
        /// Executes the supplied SQL Query and returns the resulting SQL data reader.
        /// </summary>
        /// <param name="command">
        /// The <see cref="SqlCommand"/> to be executed.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> of <see cref="SqlDataReader"/> containing the result of the operation.
        /// If successful, the result returns a the <see cref="SqlDataReader"/> resulting from the query.
        /// </returns>
        IOperationalResult<ISafeSqlDataReader> GetDataReader(SqlCommand command);
        /// <summary>
        /// Executes the supplied SQL Query and returns the resulting SQL data reader.
        /// </summary>
        /// <param name="sqlCommand">
        /// A string containing the SQL to be executed.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> of <see cref="SqlDataReader"/> containing the result of the operation.
        /// If successful, the result returns the <see cref="SqlDataReader"/> resulting from the query.
        /// </returns>
        Task<IOperationalResult<ISafeSqlDataReader>> GetDataReaderAsync(string sqlCommand);
		/// <summary>
		/// Executes the supplied SQL Query and returns the resulting SQL data reader.
		/// </summary>
		/// <param name="command">
		/// The <see cref="SqlCommand"/> to be executed.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="SqlDataReader"/> containing the result of the operation.
		/// If successful, the result returns the <see cref="SqlDataReader"/> resulting from the query.
		/// </returns>
		Task<IOperationalResult<ISafeSqlDataReader>> GetDataReaderAsync(SqlCommand command);
        #endregion

        #region SQL Safe Data Reader Object Methods
        /// <summary>
        /// Attempts to execute the specified stored procedure and return an <see cref="ISafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="command">
        /// A <see cref="SqlCommand"/> instance containing the command definition.
        /// </param>
        /// <param name="sqlParamsList">
        /// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
        /// and populated for the stored procedure.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
        /// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
        /// </returns>
        IOperationalResult<ISafeSqlDataReader?> GetMultiResultSetReaderForParameterizedCommand(SqlCommand command, IEnumerable<SqlParameter> sqlParamsList);
        /// <summary>
        /// Attempts to execute the specified stored procedure and return an <see cref="ISafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="command">
        /// A <see cref="SqlCommand"/> instance containing the command definition.
        /// </param>
        /// <param name="sqlParamsList">
        /// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
        /// and populated for the stored procedure.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
        /// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
        /// </returns>
        Task<IOperationalResult<ISafeSqlDataReader?>> GetMultiResultSetReaderForParameterizedCommandAsync(SqlCommand command, IEnumerable<SqlParameter> sqlParamsList);
        /// <summary>
        /// Attempts to execute the specified query and return an <see cref="ISafeSqlDataReader"/>
        /// instance containing zero or one rows in the results.
        /// </summary>
        /// <param name="sqlToExecute">
        /// A string containing the SQL query to execute.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
        /// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
        /// </returns>
        IOperationalResult<ISafeSqlDataReader?> GetOneRowReaderForQuery(string sqlToExecute);
        /// <summary>
        /// Attempts to execute the specified query and return an <see cref="ISafeSqlDataReader"/>
        /// instance containing zero or one rows in the results.
        /// </summary>
        /// <param name="command">
        /// The <see cref="SqlCommand"/> to execute to retrieve the data.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
        /// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
        /// </returns>
        IOperationalResult<ISafeSqlDataReader?> GetOneRowReaderForQuery(SqlCommand command);
        /// <summary>
        /// Attempts to execute the specified query and return an <see cref="ISafeSqlDataReader"/>
        /// instance containing zero or one rows in the results.
        /// </summary>
        /// <param name="sqlToExecute">
        /// A string containing the SQL query to execute.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
        /// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
        /// </returns>
        Task<IOperationalResult<ISafeSqlDataReader?>> GetOneRowReaderForQueryAsync(string sqlToExecute);
		/// <summary>
		/// Attempts to execute the specified query and return an <see cref="ISafeSqlDataReader"/>
		/// instance containing zero or one rows in the results.
		/// </summary>
		/// <param name="command">
		/// The <see cref="SqlCommand"/> to execute to retrieve the data.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
		/// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
		/// </returns>
		Task<IOperationalResult<ISafeSqlDataReader?>> GetOneRowReaderForQueryAsync(SqlCommand command);
		/// <summary>
		/// Attempts to execute the specified query and return an <see cref="ISafeSqlDataReader"/>
		/// instance containing the results.
		/// </summary>
		/// <param name="sqlToExecute">
		/// A string containing the SQL query to execute.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
		/// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
		/// </returns>
		IOperationalResult<ISafeSqlDataReader?> GetReaderForMultiResultQuery(string sqlToExecute);
        /// <summary>
        /// Attempts to execute the specified query and return an <see cref="ISafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="command">
        /// The <see cref="SqlCommand"/> to execute to retrieve the data.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
        /// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
        /// </returns>
        IOperationalResult<ISafeSqlDataReader?> GetReaderForMultiResultQuery(SqlCommand command);
        /// <summary>
        /// Attempts to execute the specified query and return an <see cref="ISafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="sqlToExecute">
        /// A string containing the SQL query to execute.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
        /// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
        /// </returns>
        Task<IOperationalResult<ISafeSqlDataReader?>> GetReaderForMultiResultQueryAsync(string sqlToExecute);
		/// <summary>
		/// Attempts to execute the specified query and return an <see cref="ISafeSqlDataReader"/>
		/// instance containing the results.
		/// </summary>
		/// <param name="command">
		/// The <see cref="SqlCommand"/> to execute to retrieve the data.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
		/// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
		/// </returns>
		Task<IOperationalResult<ISafeSqlDataReader?>> GetReaderForMultiResultQueryAsync(SqlCommand? command);
		/// <summary>
		/// Attempts to execute the specified SQL command and return an <see cref="ISafeSqlDataReader"/>
		/// instance containing the results.
		/// </summary>
		/// <param name="command">
		/// A <see cref="SqlCommand"/> instance containing the command definition.
		/// </param>
		/// <param name="sqlParamsList">
		/// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
		/// and populated for the stored procedure.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
		/// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
		/// </returns>
		IOperationalResult<ISafeSqlDataReader?> GetReaderForParameterizedCommand(SqlCommand command, IEnumerable<SqlParameter>? sqlParamsList);
		/// <summary>
		/// Attempts to execute the specified SQL command and return an <see cref="ISafeSqlDataReader"/>
		/// instance containing the results.
		/// </summary>
		/// <param name="command">
		/// A <see cref="SqlCommand"/> instance containing the command definition.
		/// </param>
		/// <param name="sqlParamsList">
		/// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
		/// and populated for the stored procedure.
		/// </param>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
		/// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
		/// </returns>
		Task<IOperationalResult<ISafeSqlDataReader?>> GetReaderForParameterizedCommandAsync(SqlCommand command, IEnumerable<SqlParameter>? sqlParamsList);
        /// <summary>
        /// Attempts to execute the specified SQL command as a stored procedure and return an <see cref="ISafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="storedProcedure">
        /// A <see cref="SqlCommand"/> instance containing the name of the stored procedure to execute.
        /// </param>
        /// <param name="sqlParamsList">
        /// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
        /// and populated for the stored procedure.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
        /// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
        /// </returns>
        IOperationalResult<ISafeSqlDataReader?> GetReaderForStoredProcedure(SqlCommand storedProcedure, IEnumerable<SqlParameter> sqlParamsList);
        /// <summary>
        /// Attempts to execute the specified SQL command as a stored procedure and return an <see cref="ISafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="storedProcedure">
        /// A <see cref="SqlCommand"/> instance containing the name of the stored procedure to execute.
        /// </param>
        /// <param name="sqlParamsList">
        /// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
        /// and populated for the stored procedure.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> containing the result of the operation.
        /// If successful, the result contains an <see cref="ISafeSqlDataReader"/> containing the results for reading.
        /// </returns>
        Task<IOperationalResult<ISafeSqlDataReader?>> GetReaderForStoredProcedureAsync(SqlCommand storedProcedure, IEnumerable<SqlParameter> sqlParamsList);
        #endregion

        #region Geberal SQL Server - specific operations.
        /// <summary>
        /// Gets the names of the database for the SQL Server instance currently connected to.
        /// </summary>
        /// <returns>
        /// An <see cref="IOperationalResult{T}"/> of <see cref="List{T}"/> of <see cref="string"/> containing hte result of the
        /// operation.  If successful, the result contains the <see cref="List{T}"/> of <see cref="string"/> values containing the
        /// names of the databases on the server.
        /// </returns>
        IOperationalResult<List<string>> GetDatabaseNames();
		/// <summary>
		/// Gets the names of the database for the SQL Server instance currently connected to.
		/// </summary>
		/// <returns>
		/// An <see cref="IOperationalResult{T}"/> of <see cref="List{T}"/> of <see cref="string"/> containing hte result of the
		/// operation.  If successful, the result contains the <see cref="List{T}"/> of <see cref="string"/> values containing the
		/// names of the databases on the server.
		/// </returns>
		Task<IOperationalResult<List<string>>> GetDatabaseNamesAsync();
		/// <summary>
		/// Tests the ability to connect to SQL Server asynchronously.
		/// </summary>
		/// <returns>
		/// An <see cref="IOperationalResult"/> containing the result of the operation.
		/// </returns>
		IOperationalResult TestConnection();
        /// <summary>
        /// Tests the ability to connect to SQL Server asynchronously.
        /// </summary>
        /// <param name="connectionString">
        /// A <see cref="SqlConnectionStringBuilder"/> containing the data needed to connect to a SQL Server.
        /// </param>
        /// <returns>
        /// An <see cref="IOperationalResult"/> containing the result of the operation.
        /// </returns>
        IOperationalResult TestConnection(SqlConnectionStringBuilder connectionString);
        /// <summary>
        /// Tests the ability to connect to SQL Server asynchronously.
        /// </summary>
        /// <returns>
        /// <b>true</b> if a connection can be established; otherwise, returns <b>false</b>.
        /// </returns>
        Task<IOperationalResult> TestConnectionAsync();
		/// <summary>
		/// Tests the ability to connect to SQL Server asynchronously.
		/// </summary>
		/// <param name="connectionString">
		/// A <see cref="SqlConnectionStringBuilder"/> containing the data needed to connect to a SQL Server.
		/// </param>
		/// <returns>
		/// <b>true</b> if a connection can be established; otherwise, returns <b>false</b>.
		/// </returns>
		Task<IOperationalResult> TestConnectionAsync(SqlConnectionStringBuilder connectionString);
		#endregion

		#endregion

		#endregion
	}
}
