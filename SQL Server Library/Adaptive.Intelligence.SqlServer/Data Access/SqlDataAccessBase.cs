using Adaptive.Intelligence.Shared;
using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Provides the base definition for direct SQL data access implementations.
    /// </summary>
    /// <remarks>
    /// This class provides the definition for implementing a general data-access class
    /// used to query SQL Server.
    /// </remarks>
    /// <seealso cref="DataAccessBase"/>
    public abstract class SqlDataAccessBase : DataAccessBase
    {
        #region Private Member Declarations
        /// <summary>
        /// The SQL data provider instance.
        /// </summary>
        private SqlDataProvider? _provider;
        /// <summary>
        /// The provider supplied externally flag.
        /// </summary>
        private bool _providerExternal;
        /// <summary>
        /// The connection string.
        /// </summary>
        private string _connectionString;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataAccessBase"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the connection string to use to connect to SQL Server.
        /// </param>
        protected SqlDataAccessBase(string connectionString)
        {
            _connectionString = connectionString;
            _provider = SqlDataProviderFactory.CreateProvider(connectionString);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataAccessBase"/> class.
        /// </summary>
        /// <param name="builder">
        /// A <see cref="SqlConnectionStringBuilder"/> instance used to build the 
        /// connection string to use to connect to SQL Server.
        /// </param>
        protected SqlDataAccessBase(SqlConnectionStringBuilder builder)
        {
            _connectionString = builder.ToString();
            _provider = SqlDataProviderFactory.CreateProvider(builder);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessBase"/> class.
        /// </summary>
        /// <param name="providerReference">
        /// A reference to the <see cref="SqlDataProvider"/> instance to use instead of locally creating one.
        /// </param>
        protected SqlDataAccessBase(SqlDataProvider providerReference)
        {
            _connectionString = providerReference.ConnectionString ?? string.Empty;
            _provider = providerReference;
            _providerExternal = true;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing && !_providerExternal)
            {
                _provider?.Dispose();
            }

            _provider = null;
            _providerExternal = false;
            base.Dispose(disposing);
        }
        #endregion

        #region Protected Properties        
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// A string containing the connection information for SQL Server.
        /// </value>
        protected string ConnectionString
        {
            get => _connectionString;
        }
        #endregion

        #region Internal Properties
        /// <summary>
        /// Gets the reference to the SQL Server data provider instance used
        /// to perform queries against SQL Server.
        /// </summary>
        /// <remarks>
        /// This value is created when the instance is constructed, if possible.
        /// If this value is <b>null</b>, check the App.config file's connection
        /// strings.
        /// </remarks>
        /// <value>
        /// The <see cref="SqlDataProvider"/> instance if created, or <b>null</b>.
        /// </value>
        internal SqlDataProvider? Provider => _provider;
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Attempts to execute the supplied SQL statement.
        /// </summary>
        /// <param name="sqlToExecute">
        /// A string containing the SQL statement to be executed.
        /// </param>
        /// <returns>
        /// An integer indicating the number of affected rows if successful;
        /// otherwise, returns -1.
        /// </returns>
        public int ExecuteSql(string sqlToExecute)
        {
            int rowsAffected = -1;

            if (_provider != null)
            {
                IOperationalResult<int> result = _provider.ExecuteSql(sqlToExecute);
                if (result.Success)
                {
                    rowsAffected = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
            }

            return rowsAffected;
        }
        /// <summary>
        /// Attempts to execute the supplied SQL statement.
        /// </summary>
        /// <param name="sqlToExecute">
        /// A string containing the SQL statement to be executed.
        /// </param>
        /// <returns>
        /// An integer indicating the number of affected rows if successful;
        /// otherwise, returns -1.
        /// </returns>
        public async Task<int> ExecuteSqlAsync(string sqlToExecute)
        {
            int rowsAffected = -1;

            if (_provider != null)
            {
                IOperationalResult<int> result = await _provider.ExecuteSqlAsync(sqlToExecute).ConfigureAwait(false);
                if (result.Success)
                {
                    rowsAffected = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
            }

            return rowsAffected;
        }
        /// <summary>
        /// Executes the specified SQL stored procedure, with parameters.
        /// </summary>
        /// <param name="sqlText">
        /// A string containing the content of the parameterized SQL.
        /// </param>
        /// <param name="parameterList">
        /// An array of <see cref="SqlParameter"/> instance containing the populated
        /// SQL Parameters to supply to the stored procedure.
        /// </param>
        /// <returns>
        /// An integer indicating the number of rows affected by the execution of
        /// the query, or -1 if the operation fails.
        /// </returns>
        public int ExecuteSql(string sqlText, SqlParameter[] parameterList)
        {
            int rowsAffected = -1;

            if (_provider != null)
            {
                IOperationalResult<int> result = _provider.ExecuteParameterizedSql(sqlText, parameterList);
                if (result.Success)
                {
                    rowsAffected = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
            }

            return rowsAffected;
        }
        /// <summary>
        /// Executes the specified SQL stored procedure, with parameters.
        /// </summary>
        /// <param name="sql">
        /// A string containing the parameterized SQL string.
        /// </param>
        /// <param name="parameterList">
        /// An array of <see cref="SqlParameter"/> instance containing the populated
        /// SQL Parameters to supply to the stored procedure.
        /// </param>
        /// <returns>
        /// An integer indicating the number of rows affected by the execution of
        /// the query, or -1 if the operation fails.
        /// </returns>
        public async Task<int> ExecuteSqlAsync(string sql, SqlParameter[] parameterList)
        {
            int rowsAffected = -1;

            if (_provider != null)
            {
                IOperationalResult<int> result = await _provider.ExecuteParameterizedSqlAsync(sql, parameterList).ConfigureAwait(false);
                if (result.Success)
                {
                    rowsAffected = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
            }

            return rowsAffected;
        }
        /// <summary>
        /// Attempts to execute the specified SQL command text with the supplied
        /// parameters.
        /// </summary>
        /// <param name="sqlText">
        /// A string containing the SQL to be executed.
        /// </param>
        /// <param name="parameterList">
        /// An array of <see cref="SqlParameter"/> instances to send to the command.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation executes successfully; otherwise, returns <b>false</b>.
        /// </returns>
        public bool ExecuteParameterizedSql(string sqlText, IEnumerable<SqlParameter> parameterList)
        {
            bool success = false;

            if (_provider != null)
            {
                IOperationalResult<int> result = _provider.ExecuteParameterizedSql(sqlText, parameterList);
                success = result.Success;
                CopyExceptions(result);
                result.Dispose();
            }
            return success;
        }
        /// <summary>
        /// Attempts to execute the specified SQL command text with the supplied
        /// parameters.
        /// </summary>
        /// <param name="sqlText">
        /// A string containing the SQL to be executed.
        /// </param>
        /// <param name="parameterList">
        /// An array of <see cref="SqlParameter"/> instances to send to the command.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation executes successfully; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> ExecuteParameterizedSqlAsync(string sqlText, IEnumerable<SqlParameter> parameterList)
        {
            bool success = false;

            if (_provider != null)
            {
                IOperationalResult<int> result = await _provider.ExecuteParameterizedSqlAsync(sqlText, parameterList)
                    .ConfigureAwait(false);

                success = result.Success;
                CopyExceptions(result);
                result.Dispose();
            }
            return success;
        }
        /// <summary>
        /// Attempts to execute the specified SQL command text with the supplied
        /// parameters.
        /// </summary>
        /// <param name="storedProcedureName">
        /// A string containing the name of the stored procedure to be executed.
        /// </param>
        /// <param name="parameterList">
        /// An array of <see cref="SqlParameter"/> instances to send to the command.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation executes successfully; otherwise, returns <b>false</b>.
        /// </returns>
        public bool ExecuteStoredProcedure(string storedProcedureName, IEnumerable<SqlParameter> parameterList)
        {
            bool success = false;

            if (_provider != null)
            {
                IOperationalResult<int> result = _provider.ExecuteStoredProcedure(storedProcedureName, parameterList);
                success = result.Success;
                CopyExceptions(result);
                result.Dispose();
            }
            return success;
        }
        /// <summary>
        /// Attempts to execute the specified SQL command text with the supplied
        /// parameters.
        /// </summary>
        /// <param name="storedProcedureName">
        /// A string containing the name of the stored procedure to be executed.
        /// </param>
        /// <param name="parameterList">
        /// An array of <see cref="SqlParameter"/> instances to send to the command.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation executes successfully; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> ExecuteStoredProcedureAsync(string storedProcedureName, IEnumerable<SqlParameter> parameterList)
        {
            bool success = false;

            if (_provider != null)
            {
                IOperationalResult<int> result = await _provider.ExecuteStoredProcedureAsync(storedProcedureName, parameterList)
                    .ConfigureAwait(false);
                success = result.Success;
                CopyExceptions(result);
                result.Dispose();
            }
            return success;
        }
        /// <summary>
        /// Attempts to execute the specified query and return an <see cref="SafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="sqlToExecute">
        /// A string containing the SQL query to execute.
        /// </param>
        /// <returns>
        /// An <see cref="ISafeSqlDataReader"/> containing the results for reading, or
        /// <b>null</b> if the query operation failed.
        /// </returns>
        public ISafeSqlDataReader? GetReaderForQuery(string sqlToExecute)
        {
            ISafeSqlDataReader? reader = null;
            if (_provider != null)
            {
                IOperationalResult<ISafeSqlDataReader> result = _provider.GetDataReader(sqlToExecute);
                if (result.Success)
                {
                    reader = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
            }
            return reader;
        }
        /// <summary>
        /// Attempts to execute the specified query and return an <see cref="ISafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="sqlToExecute">
        /// A string containing the SQL query to execute.
        /// </param>
        /// <returns>
        /// An <see cref="ISafeSqlDataReader"/> containing the results for reading, or
        /// <b>null</b> if the query operation failed.
        /// </returns>
        public async Task<ISafeSqlDataReader?> GetReaderForQueryAsync(string sqlToExecute)
        {
            ISafeSqlDataReader? reader = null;

            if (_provider != null)
            {
                IOperationalResult<ISafeSqlDataReader> result = await _provider.GetDataReaderAsync(sqlToExecute)
                    .ConfigureAwait(false);
                if (result.Success)
                {
                    reader = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
            }

            return reader;
        }
        /// <summary>
        /// Attempts to execute the specified stored procedure and return an <see cref="SafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="commandText">
        /// A string containing the SQL text for the parameterized query.
        /// </param>
        /// <param name="sqlParamsList">
        /// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
        /// and populated for the stored procedure.
        /// </param>
        /// <returns>
        /// An <see cref="SafeSqlDataReader"/> containing the results for reading, or
        /// <b>null</b> if the query operation failed.
        /// </returns>
        public ISafeSqlDataReader? GetReaderForParameterizedCommandText(string commandText,
            IEnumerable<SqlParameter>? sqlParamsList)
        {
            ISafeSqlDataReader? reader = null;

            if (_provider != null)
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = commandText
                };
                IOperationalResult<ISafeSqlDataReader?> result = _provider.GetReaderForParameterizedCommand(command,
                    sqlParamsList);
                if (result.Success)
                {
                    reader = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();

            }
            return reader;
        }
        /// <summary>
        /// Attempts to execute the specified stored procedure and return an <see cref="SafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="commandText">
        /// A string containing the SQL text for the parameterized query.
        /// </param>
        /// <param name="sqlParamsList">
        /// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
        /// and populated for the stored procedure.
        /// </param>
        /// <returns>
        /// An <see cref="SafeSqlDataReader"/> containing the results for reading, or
        /// <b>null</b> if the query operation failed.
        /// </returns>
        public async Task<ISafeSqlDataReader?> GetReaderForParameterizedCommandTextAsync(string? commandText,
            IEnumerable<SqlParameter>? sqlParamsList)
        {
            ISafeSqlDataReader? reader = null;

            if (_provider != null)
            {
                SqlCommand command = new SqlCommand(commandText);
                IOperationalResult<ISafeSqlDataReader?> result = await _provider.GetReaderForParameterizedCommandAsync(command,
                    sqlParamsList).ConfigureAwait(false);

                if (result.Success)
                {
                    reader = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
            }
            return reader;
        }
        /// <summary>
        /// Attempts to execute the specified stored procedure and return an <see cref="SafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="storedProcedure">
        /// A string containing the name of the stored procedure to execute.
        /// </param>
        /// <param name="sqlParamsList">
        /// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
        /// and populated for the stored procedure.
        /// </param>
        /// <returns>
        /// An <see cref="SafeSqlDataReader"/> containing the results for reading, or
        /// <b>null</b> if the query operation failed.
        /// </returns>
        public ISafeSqlDataReader? GetReaderForParameterizedStoredProcedure(string storedProcedure,
            IEnumerable<SqlParameter> sqlParamsList)
        {
            ISafeSqlDataReader? reader = null;

            if (_provider != null)
            {
                SqlCommand command = new SqlCommand(storedProcedure);
                IOperationalResult<ISafeSqlDataReader?> result = _provider.GetReaderForStoredProcedure(
                    command,
                    sqlParamsList);
                if (result.Success)
                {
                    reader = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
                command.Dispose();
            }
            return reader;
        }
        /// <summary>
        /// Attempts to execute the specified stored procedure and return an <see cref="SafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="storedProcedure">
        /// A string containing the name of the stored procedure to execute.
        /// </param>
        /// <param name="sqlParamsList">
        /// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
        /// and populated for the stored procedure.
        /// </param>
        /// <returns>
        /// An <see cref="SafeSqlDataReader"/> containing the results for reading, or
        /// <b>null</b> if the query operation failed.
        /// </returns>
        public async Task<ISafeSqlDataReader?> GetReaderForParameterizedStoredProcedureAsync(string storedProcedure,
            IEnumerable<SqlParameter>? sqlParamsList)
        {
            ISafeSqlDataReader? reader = null;

            if (_provider != null)
            {
                SqlCommand command = new SqlCommand(storedProcedure);
                IOperationalResult<ISafeSqlDataReader?> result = await _provider.GetReaderForStoredProcedureAsync(
                    command,
                    sqlParamsList).ConfigureAwait(false);

                if (result.Success)
                {
                    reader = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
                command.Dispose();
            }
            return reader;
        }
        /// <summary>
        /// Attempts to execute the specified stored procedure and return an <see cref="SafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="storedProcedure">
        /// A string containing the name of the stored procedure to execute.
        /// </param>
        /// <param name="sqlParamsList">
        /// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
        /// and populated for the stored procedure.
        /// </param>
        /// <returns>
        /// An <see cref="ISafeSqlDataReader"/> containing the results for reading, or
        /// <b>null</b> if the query operation failed.
        /// </returns>
        public ISafeSqlDataReader? GetMultiResultSetReaderForParameterizedCommand(string storedProcedure,
            IEnumerable<SqlParameter>? sqlParamsList)
        {
            ISafeSqlDataReader? reader = null;

            if (_provider != null && sqlParamsList != null)
            {
                SqlCommand command = new SqlCommand(storedProcedure);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                IOperationalResult<ISafeSqlDataReader?> result = _provider.GetMultiResultSetReaderForParameterizedCommand(
                    command,
                    sqlParamsList);

                if (result.Success)
                {
                    reader = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
                command.Dispose();

            }
            return reader;
        }
        /// <summary>
        /// Attempts to execute the specified stored procedure and return an <see cref="SafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="storedProcedure">
        /// A string containing the name of the stored procedure to execute.
        /// </param>
        /// <param name="sqlParamsList">
        /// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
        /// and populated for the stored procedure.
        /// </param>
        /// <returns>
        /// An <see cref="SafeSqlDataReader"/> containing the results for reading, or
        /// <b>null</b> if the query operation failed.
        /// </returns>
        public async Task<ISafeSqlDataReader?> GetMultiResultSetReaderForParameterizedCommandAsync(string storedProcedure,
            IEnumerable<SqlParameter> sqlParamsList)
        {
            ISafeSqlDataReader? reader = null;

            if (_provider != null)
            {
                SqlCommand command = new SqlCommand(storedProcedure);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                IOperationalResult<ISafeSqlDataReader?> result = await _provider.GetMultiResultSetReaderForParameterizedCommandAsync(command,
                    sqlParamsList).ConfigureAwait(false);

                if (result.Success)
                {
                    reader = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
                command.Dispose();

            }
            return reader;
        }
        /// <summary>
        /// Attempts to execute the specified stored procedure and return an <see cref="SafeSqlDataReader"/>
        /// instance containing the results.
        /// </summary>
        /// <param name="storedProcedure">
        /// A string containing the name of the stored procedure to execute.
        /// </param>
        /// <param name="sqlParamsList">
        /// A <see cref="List{T}"/> of <see cref="SqlParameter"/> instances defined
        /// and populated for the stored procedure.
        /// </param>
        /// <returns>
        /// An <see cref="ISafeSqlDataReader"/> containing the results for reading, or
        /// <b>null</b> if the query operation failed.
        /// </returns>
        public async Task<ISafeSqlDataReader?> GetReaderForParameterizedCommandAsync(string storedProcedure,
            IEnumerable<SqlParameter> sqlParamsList)
        {
            ISafeSqlDataReader? reader = null;

            if (_provider != null)
            {
                SqlCommand command = new SqlCommand(storedProcedure);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                IOperationalResult<ISafeSqlDataReader?> result = await _provider.GetReaderForParameterizedCommandAsync(
                    command,
                    sqlParamsList).ConfigureAwait(false);


                if (result.Success)
                {
                    reader = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
                command.Dispose();

            }

            return reader;
        }
        /// <summary>
        /// Attempts to execute the specified query and return an <see cref="SafeSqlDataReader"/>
        /// instance containing the multiple result sets that were returned.
        /// </summary>
        /// <param name="sqlToExecute">
        /// A string containing the SQL query to execute.
        /// </param>
        /// <returns>
        /// An <see cref="ISafeSqlDataReader"/> containing the results for reading, or
        /// <b>null</b> if the query operation failed.
        /// </returns>
        public ISafeSqlDataReader? GetReaderForMultipleResultSets(string sqlToExecute)
        {
            ISafeSqlDataReader? reader = null;

            if (_provider != null)
            {
                IOperationalResult<ISafeSqlDataReader?> result = _provider.GetReaderForMultiResultQuery(sqlToExecute);
                if (result.Success)
                {
                    reader = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
            }

            return reader;
        }
        /// <summary>
        /// Attempts to execute the specified query and return an <see cref="SafeSqlDataReader"/>
        /// instance containing the multiple result sets that were returned.
        /// </summary>
        /// <param name="sqlToExecute">
        /// A string containing the SQL query to execute.
        /// </param>
        /// <returns>
        /// An <see cref="ISafeSqlDataReader"/> containing the results for reading, or
        /// <b>null</b> if the query operation failed.
        /// </returns>
        public async Task<ISafeSqlDataReader?> GetReaderForMultipleResultSetsAsync(string sqlToExecute)
        {
            ISafeSqlDataReader? reader = null;

            if (_provider != null)
            {
                IOperationalResult<ISafeSqlDataReader?> result = await _provider.GetReaderForMultiResultQueryAsync(sqlToExecute).ConfigureAwait(false);
                if (result.Success)
                {
                    reader = result.DataContent;
                }

                CopyExceptions(result);
                result.Dispose();
            }

            return reader;
        }
        #endregion

        #region Protected Methods / Functions
        /// <summary>
        /// Creates the parameter list for the specified parameter name and value.
        /// </summary>
        /// <param name="parameterName">
        /// A string containing the name of the parameter.
        /// </param>
        /// <param name="parameterValue">
        /// An object containing the boxed parameter value.
        /// </param>
        /// <returns>
        /// An array of <see cref="SqlParameter"/> instance(s).
        /// </returns>
        protected static SqlParameter[] CreateParameterList(string parameterName, object parameterValue)
        {
            return new[] { CreateParameter(parameterName, parameterValue) };
        }
        /// <summary>
        /// Creates the parameter list for the specified parameter name and value pairs.
        /// </summary>
        /// <param name="firstParameterName">
        /// A string containing the name of the first parameter.
        /// </param>
        /// <param name="firstParameterValue">
        /// An object containing the first boxed parameter value.
        /// </param>
        /// <param name="secondParameterName">
        /// A string containing the name of the second parameter.
        /// </param>
        /// <param name="secondParameterValue">
        /// An object containing the second boxed parameter value.
        /// </param>
        /// <returns>
        /// An array of <see cref="SqlParameter"/> instance(s).
        /// </returns>
        protected static SqlParameter[] CreateParameterList(string firstParameterName, object firstParameterValue,
            string secondParameterName, object secondParameterValue)
        {
            return new[]
            {
                CreateParameter(firstParameterName, firstParameterValue),
                CreateParameter(secondParameterName, secondParameterValue)
            };
        }
        /// <summary>
        /// Creates the parameter list for the specified parameter name and value pairs.
        /// </summary>
        /// <param name="firstParameterName">
        /// A string containing the name of the first parameter.
        /// </param>
        /// <param name="firstParameterValue">
        /// An object containing the first boxed parameter value.
        /// </param>
        /// <param name="secondParameterName">
        /// A string containing the name of the second parameter.
        /// </param>
        /// <param name="secondParameterValue">
        /// An object containing the second boxed parameter value.
        /// </param>
        /// <param name="thirdParameterName">
        /// A string containing the name of the third parameter.
        /// </param>
        /// <param name="thirdParameterValue">
        /// An object containing the third boxed parameter value.
        /// </param>
        /// <returns>
        /// An array of <see cref="SqlParameter"/> instance(s).
        /// </returns>
        protected static SqlParameter[] CreateParameterList(string firstParameterName, object firstParameterValue,
            string secondParameterName, object secondParameterValue,
            string thirdParameterName, object thirdParameterValue)
        {
            return new[]
            {
                CreateParameter(firstParameterName, firstParameterValue),
                CreateParameter(secondParameterName, secondParameterValue),
                CreateParameter(thirdParameterName, thirdParameterValue)
            };
        }
        /// <summary>
        /// Creates the <see cref="SqlParameter"/> instance for the provided
        /// parameter name and value.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the parameter.
        /// </param>
        /// <param name="value">
        /// The value to assign to the parameter.
        /// </param>
        /// <returns>
        /// The <see cref="SqlParameter"/> instance to use.
        /// </returns>
        protected static SqlParameter CreateParameter(string name, string? value)
        {
            SqlParameter parameter = new SqlParameter(name, value);

            if (value != null)
            {
                value = value.Replace(Constants.SingleQuote, Constants.SingleQuote + Constants.SingleQuote);
            }

            if (value == null)
            {
                parameter.IsNullable = true;
                parameter.Value = DBNull.Value;
            }
            return parameter;
        }
        /// <summary>
        /// Creates the <see cref="SqlParameter"/> instance for the provided
        /// parameter name and value.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the parameter.
        /// </param>
        /// <param name="value">
        /// The value to assign to the parameter.
        /// </param>
        /// <returns>
        /// The <see cref="SqlParameter"/> instance to use.
        /// </returns>
        protected static SqlParameter CreateParameter(string name, object? value)
        {
            SqlParameter parameter = new SqlParameter(name, value);
            if (value == null)
            {
                parameter.IsNullable = true;
                parameter.Value = DBNull.Value;
            }
            else if (value is decimal)
            {
                parameter.SqlDbType = System.Data.SqlDbType.Decimal;
                parameter.Precision = 18;
                parameter.Scale = 2;
            }
            else if (value is string stringValue)
            {
                parameter = CreateParameter(name, stringValue);
            }

            return parameter;
        }
        /// <summary>
        /// Creates the <see cref="SqlParameter"/> instance for the provided
        /// parameter name and value.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the parameter.
        /// </param>
        /// <param name="value">
        /// The value to assign to the parameter.
        /// </param>
        /// <typeparamref name="T">The data type of the parameter.</typeparamref>
        /// <returns>
        /// The <see cref="SqlParameter"/> instance to use.
        /// </returns>
        protected static SqlParameter CreateParameter<T>(string name, T value)
        {
            SqlParameter parameter = new SqlParameter(name, value);
            if (value == null)
            {
                parameter.IsNullable = true;
                parameter.Value = DBNull.Value;
            }
            else if (value is decimal)
            {
                parameter.SqlDbType = System.Data.SqlDbType.Decimal;
                parameter.Precision = 18;
                parameter.Scale = 2;
            }
            else if (value is string)
            {
                parameter = CreateParameter(name, (string)(object)value);
            }

            return parameter;
        }
        /// <summary>
        /// Translates the boolean value for inclusion in a SQL query string.
        /// </summary>
        /// <param name="value">The value to translate.</param>
        /// <returns>
        /// "1" if the value is <b>true</b>; otherwise, returns "0";
        /// </returns>
        protected static string ToSqlBoolean(bool value)
        {
            if (value)
            {
                return TSqlConstants.BooleanTrueNumber;
            }
            else
            {
                return TSqlConstants.BooleanFalseNumber;
            }
        }
        /// <summary>
        /// Translates the specified string value for inclusion in a SQL query string.
        /// </summary>
        /// <param name="value">
        /// A string containing the value to translate, or <b>null</b>.</param>
        /// <returns>
        /// The text "NULL" if the value is <b>null</b>, or a single-quoted character string
        /// if the value is not null.
        /// </returns>
        protected static string ToSqlString(string? value)
        {
            if (value == null)
            {
                return TSqlConstants.SqlNull;
            }
            else
            {
                return
                    TSqlConstants.SqlSingleQuote +
                    value.Replace(TSqlConstants.SqlSingleQuote, TSqlConstants.SqlSingleQuoteEscaped) +
                    TSqlConstants.SqlSingleQuote;
            }
        }
        /// <summary>
        /// Translates the specified list of string values for inclusion in a SQL query string.
        /// </summary>
        /// <param name="valueList">
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the values to translate, or <b>null</b>.</param>
        /// <returns>
        /// The text "NULL" if the value is <b>null</b>, or a single-quoted character string
        /// containing a comma-delimited list of the string value items.
        /// </returns>
        protected static string ToSqlListString(List<string>? valueList)
        {
            string sql = TSqlConstants.SqlNull;

            // If the list is null or empty, return "NULL",
            // else ...
            if (valueList != null && valueList.Count > 0)
            {
                StringBuilder builder = new StringBuilder(1000);

                // Append each element to the SQL string with single quotes surrounding
                // the entire string (instead of each element), where is element is followed
                // delimited by a comma, except for the last element.
                builder.Append(TSqlConstants.SqlSingleQuoteChar);
                int length = valueList.Count;
                for (int count = 0; count < length - 1; count++)
                {
                    builder.Append(valueList[count]);
                    builder.Append(TSqlConstants.SqlCommaChar);
                }

                // Append the last element with a closing quote and no comma.
                builder.Append(valueList[length - 1]);
                builder.Append(TSqlConstants.SqlSingleQuoteChar);

                sql = builder.ToString();
            }

            return sql;
        }
        /// <summary>
        /// Translates the specified list of string values for inclusion in a SQL query
        /// string.
        /// </summary>
        /// <param name="valueList">
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the values to translate, or <b>null</b>.</param>
        /// <returns>
        /// The text "NULL" if the value is <b>null</b>, or a single-quoted character string
        /// containing a comma-delimited list of the string value items.
        /// </returns>
        protected static string ToSeparatedSqlListString(List<string>? valueList)
        {
            string sql = TSqlConstants.SqlNull;

            if (valueList != null && valueList.Count > 0)
            {
                StringBuilder builder = new StringBuilder(1000);

                // Append each element to the SQL string with quotes surrounding
                // each string value, followed by a comma, except for the last
                // element.

                int length = valueList.Count;
                for (int count = 0; count < length - 1; count++)
                {
                    builder.Append(ToSqlString(valueList[count]) + TSqlConstants.SqlCommaChar);
                }

                builder.Append(ToSqlString(valueList[length - 1]));
                sql = builder.ToString();
            }

            return sql;
        }
        /// <summary>
        /// Translates the specified date/time offset value for inclusion in a SQL query string.
        /// </summary>
        /// <param name="dateTimeValue">
        /// A nullable <see cref="DateTimeOffset"/> containing the value to translate, or <b>null</b>.</param>
        /// <returns>
        /// The text "NULL" if the value is <b>null</b>, or a single-quoted character string
        /// if the value is not null.
        /// </returns>
        protected static string ToSqlDateTimeOffset(DateTimeOffset? dateTimeValue)
        {
            if (dateTimeValue == null)
            {
                return TSqlConstants.SqlNull;
            }
            else
            {
                return TSqlConstants.SqlSingleQuote + dateTimeValue.ToString() + TSqlConstants.SqlSingleQuote;
            }
        }
        /// <summary>
        /// Translates the specified date/time value for inclusion in a SQL query string.
        /// </summary>
        /// <param name="dateTimeValue">
        /// A nullable <see cref="DateTime"/> containing the value to translate, or <b>null</b>.</param>
        /// <returns>
        /// The text "NULL" if the value is <b>null</b>, or a single-quoted character string
        /// if the value is not null.
        /// </returns>
        protected static string ToSqlDateTime(DateTime? dateTimeValue)
        {
            if (dateTimeValue == null)
            {
                return TSqlConstants.SqlNull;
            }
            else
            {
                return
                    TSqlConstants.SqlSingleQuote +
                    dateTimeValue.Value.ToString(TSqlConstants.DateTimeFormatUS) +
                    TSqlConstants.SqlSingleQuote;
            }
        }
        #endregion
    }
}


