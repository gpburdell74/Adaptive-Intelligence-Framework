using Adaptive.Intelligence.SqlServer;
using Adaptive.SqlServer.Client;

namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the base definition for instances used to perform data access operations.
/// </summary>
/// <typeparam name="T">
/// The data type of the data transfer object being used.
/// </typeparam>
/// <seealso cref="SqlDataAccessBase{T}" />
/// <seealso cref="IDataAccess{T}"/>
/// <seealso cref="IDataTransfer"/>
public abstract class IdentityDataAccessBase<T> : SqlDataAccessBase<T>, IDataAccess<T>
    where T : IDataTransfer
{
    #region Private Member Declarations
    /// <summary>
    /// The name of the stored procedure to call to delete a record.
    /// </summary>
    private string? _deleteSp;
    /// <summary>
    /// The standard name of the ID parameter.
    /// </summary>
    private string? _idParamName;
    /// <summary>
    /// The name of the stored procedure to call to insert a record.
    /// </summary>
    private string? _insertSp;
    /// <summary>
    /// The name of the stored procedure to call to update a record.
    /// </summary>
    private string? _updateSp;
    /// <summary>
    /// The name of the stored procedure to call to get a record by its ID value.
    /// </summary>
    private string? _getByIdSp;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityDataAccessBase{T}"/> class.
    /// </summary>
    /// <param name="deleteSp">
    /// A string containing the name of the delete stored procedure.
    /// </param>
    /// <param name="getByIdSp">
    /// A string containing the name of the get record by ID stored procedure.
    /// </param>
    /// <param name="idParamName">
    /// A string containing the standard name of the ID value.
    /// </param>
    /// <param name="insertSp">
    /// A string containing the name of the insert stored procedure.
    /// </param>
    /// <param name="updateSp">
    /// A string containing the name of the update stored procedure.
    /// </param>
    protected IdentityDataAccessBase(string insertSp, string updateSp, string getByIdSp, string deleteSp, string idParamName) : base(GetConnectionString())
    {
        _insertSp = insertSp;
        _updateSp = updateSp;
        _deleteSp = deleteSp;
        _getByIdSp = getByIdSp;
        _idParamName = idParamName;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _insertSp = null;
        _idParamName = null;
        _deleteSp = null;
        _getByIdSp = null;
        _updateSp = null;

        base.Dispose(disposing);
    }
    #endregion

    #region CRUD Methods
    /// <summary>
    /// Creates / inserts a new data record.
    /// </summary>
    /// <param name="dataRecord">
    /// The <typeparamref name="T"/> data record to be created.
    /// </param>
    /// <returns>
    /// A <see cref="Guid"/> containing the ID of the new record, if successful; otherwise,
    /// returns <b>null</b>.
    /// </returns>
    public virtual async Task<Guid?> CreateNewAsync(T dataRecord)
    {
        return await InsertAsync(_insertSp!, dataRecord).ConfigureAwait(false);
    }

    /// <summary>
    /// Marks the specified record as deleted.
    /// </summary>
    /// <param name="recordId">
    /// A <see cref="Guid"/> containing the ID of the record to mark as deleted.
    /// </param>
    /// <returns>
    /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
    /// </returns>
    public virtual async Task<bool> DeleteAsync(Guid recordId)
    {
        return await DeleteAsync(_deleteSp!, _idParamName!, recordId).ConfigureAwait(false);
    }
    /// <summary>
    /// Gets the data record by the specified ID value.
    /// </summary>
    /// <param name="id">
    /// A <see cref="Guid"/> specifying the ID of the record to be loaded.
    /// </param>
    /// <returns>
    /// The associated <typeparamref name="T"/> record, if successful;
    /// otherwise, returns <b>null</b>.
    /// </returns>
    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        T? dataRecord = default(T);

        ISafeSqlDataReader? reader = await
            GetReaderForParameterizedStoredProcedureAsync(_getByIdSp!,
                CreateParameterList(_idParamName!, id))
            .ConfigureAwait(false);

        if (reader != null)
        {
            dataRecord = await ReadSingleRecordAsync(reader).ConfigureAwait(false);
            reader.Dispose();
        }

        return dataRecord;
    }

    /// <summary>
    /// Updates the specified record.
    /// </summary>
    /// <param name="dataRecord">
    /// The <typeparamref name="T"/> data transfer object instance containing the data record to be updated.
    /// </param>
    /// <returns>
    /// A new <typeparamref name="T"/> instance containing the updated data.
    /// </returns>
    public virtual async Task<T?> UpdateAsync(T dataRecord)
    {
        return await UpdateAsync(_updateSp!, dataRecord).ConfigureAwait(false);
    }
    #endregion

    #region Private Methods / Functions        
    /// <summary>
    /// Gets the connection string.
    /// </summary>
    /// <returns>
    /// A string containing the connection string to use.
    /// </returns>
    private static string GetConnectionString()
    {
        return "Server=tcp:mwwg.database.windows.net,1433;Initial Catalog=Mwwg;Persist Security Info=False;User ID=mwwg_application;Password=77329!Wlbs74-14MxxTim2TI.3.niv;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=10;";
    }
    #endregion
}
