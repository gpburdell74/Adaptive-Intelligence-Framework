using Adaptive.Intelligence.Shared;
using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;

namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Provides the base definition for direct SQL data access implementations.
    /// </summary>
    /// <remarks>
    /// This class provides the definition for implementing a general data-access class
    /// used to query SQL Server.
    /// </remarks>
    /// <seealso cref="SqlDataAccessBase"/>
    public abstract class SqlDataAccessBase<T> : SqlDataAccessBase
    {
        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataAccessBase"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// A string containing the connection string to use to connect to SQL Server.
        /// </param>
        protected SqlDataAccessBase(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataAccessBase"/> class.
        /// </summary>
        /// <param name="builder">
        /// A <see cref="SqlConnectionStringBuilder"/> instance used to build the 
        /// connection string to use to connect to SQL Server.
        /// </param>
        protected SqlDataAccessBase(SqlConnectionStringBuilder builder) : base(builder)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessBase"/> class.
        /// </summary>
        /// <param name="providerReference">
        /// A reference to the <see cref="SqlDataProvider"/> instance to use instead of locally creating one.
        /// </param>
        protected SqlDataAccessBase(SqlDataProvider providerReference) : base(providerReference)
        {
        }
        #endregion

        #region Protected and Abstract Methods
        /// <summary>
        /// Creates the array of SQL parameters used to insert a new record.
        /// </summary>
        /// <param name="dataRecord">
        /// The <typeparamref name="T"/> data transfer object instance containing the new record.
        /// </param>
        /// <returns>
        /// An array of <see cref="SqlParameter"/> instances to be used in a stored procedure.
        /// </returns>
        protected abstract SqlParameter[] CreateInsertParameters(T dataRecord);

        /// <summary>
        /// Creates the array of SQL parameters used to update a record.
        /// </summary>
        /// <param name="dataRecord">
        /// The <typeparamref name="T"/> data transfer object instance containing the data record to be updated.
        /// </param>
        /// <returns>
        /// An array of <see cref="SqlParameter"/> instances to be used in a stored procedure.
        /// </returns>
        protected abstract SqlParameter[] CreateUpdateParameters(T dataRecord);

        /// <summary>
        /// Populates the supplied DTO instance from the specified reader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> reader instance to use to read the data content.
        /// </param>
        /// <param name="instance">
        /// The data object instance to be populated.
        /// </param>
        protected abstract void Populate(ISafeSqlDataReader reader, T instance);
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Marks the specified record as deleted.
        /// </summary>
        /// <param name="storedProcedure">
        /// A string containing the name of the stored procedure.
        /// </param>
        /// <param name="paramName">
        /// A string containing the name of the ID parameter for the procedure.
        /// </param>
        /// <param name="recordId">
        /// A <see cref="Guid"/> containing the record's ID value.</param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> DeleteAsync(string storedProcedure, string paramName, Guid recordId)
        {
            return await 
                ExecuteStoredProcedureAsync(
                    storedProcedure, 
                    CreateParameterList(paramName, recordId)
                    ).ConfigureAwait(false);
        }

        /// <summary>
        /// Inserts a new record for the specified data.
        /// </summary>
        /// <param name="storedProcedure">
        /// A string containing the stored procedure to be executed to perform the task.
        /// </param>
        /// <param name="dataRecord">
        /// The <typeparamref name="T"/> data transfer object instance containing the new record.
        /// </param>
        /// <returns>
        /// A <see cref="Guid"/> containing the ID of the new record if successful; otherwise,
        /// returns <b>null</b>.
        /// </returns>
        public async Task<Guid?> InsertAsync(string storedProcedure, T dataRecord)
        {
            Guid? newId = null;
            SqlParameter[] paramList = CreateInsertParameters(dataRecord);

            ISafeSqlDataReader? reader = await
            GetReaderForParameterizedStoredProcedureAsync(storedProcedure, paramList)
            .ConfigureAwait(false);

            if (reader != null)
            {
                if (await reader.ReadAsync().ConfigureAwait(false))
                    newId = reader.GetGuid(0);
                reader.Dispose();
            }
            return newId;
        }

        /// <summary>
        /// Reads a single data record from the reader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="ISafeSqlDataReader"/> instance used to read the data.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> if successful; otherwise, returns <b>null</b>.
        /// </returns>
        public async Task<T?> ReadSingleRecordAsync(ISafeSqlDataReader? reader)
        {
            T? dto = default(T);

            if (reader != null)
            {
                if (await reader.ReadAsync().ConfigureAwait(false))
                {
                    dto = Activator.CreateInstance<T>();
                    Populate(reader, dto);
                }
                reader.Dispose();
            }
            return dto;
        }

        /// <summary>
        /// Reads a list of data records from the reader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="ISafeSqlDataReader"/> instance used to read the data.
        /// </param>
        /// <returns>
        /// A <see cref="List{T}"/> of <typeparamref name="T"/> instances if successful; otherwise, returns <b>null</b>.
        /// </returns>
        public async Task<List<T>?> ReadMultipleRecordsAsync(ISafeSqlDataReader? reader)
        {
            List<T>? list = null;

            if (reader != null)
            {
                list = new List<T>();
                while(await reader.ReadAsync().ConfigureAwait(false))
                {
                    T dto = Activator.CreateInstance<T>();
                    Populate(reader, dto);
                    list.Add(dto);
                }
                reader.Dispose();
            }
            return list;
        }

        /// <summary>
        /// Updates the specified record.
        /// </summary>
        /// <param name="storedProcedure">
        /// A string containing the stored procedure to be executed to perform the task.
        /// </param>
        /// <param name="dto">
        /// The <typeparamref name="T"/> data transfer object instance containing the data record to be updated.
        /// </param>
        /// <returns>
        /// A new <typeparamref name="T"/> instance containing the updated data.
        /// </returns>
        public async Task<T?> UpdateAsync(string storedProcedure, T dto)
        {
            T? updatedRecord = default(T);

            SqlParameter[] paramList = CreateUpdateParameters(dto);
            ISafeSqlDataReader? reader = await GetReaderForParameterizedStoredProcedureAsync(storedProcedure, paramList)
                .ConfigureAwait(false);
            if (reader != null)
            {
                if (await reader.ReadAsync().ConfigureAwait(false))
                {
                    updatedRecord = Activator.CreateInstance<T>();
                    Populate(reader!, updatedRecord);
                }
                reader.Dispose();
            }

            return updatedRecord;
        }
        #endregion
    }
}
