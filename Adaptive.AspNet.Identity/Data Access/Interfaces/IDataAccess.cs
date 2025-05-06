namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the signature definition for generic data access instances.
/// </summary>
/// <seealso cref="IDisposable" />
/// <typeparam name="T">
/// The data type of the data transfer object being worked with.
/// </typeparam>
public interface IDataAccess<T> : IDisposable 
    where T : IDataTransfer
{
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
    Task<Guid?> CreateNewAsync(T dataRecord);

    /// <summary>
    /// Marks the specified record as deleted.
    /// </summary>
    /// <param name="recordId">
    /// A <see cref="Guid"/> containing the ID of the record to mark as deleted.
    /// </param>
    /// <returns>
    /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
    /// </returns>
    Task<bool> DeleteAsync(Guid recordId);

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
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Updates the specified record.
    /// </summary>
    /// <param name="dataRecord">
    /// The <typeparamref name="T"/> data transfer object instance containing the data record to be updated.
    /// </param>
    /// <returns>
    /// A new <typeparamref name="T"/> instance containing the updated data.
    /// </returns>
    Task<T?> UpdateAsync(T dataRecord);
}
