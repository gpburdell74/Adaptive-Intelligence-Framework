using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.Shared;

/// <summary>
/// Provides the base implementation for a collection that contains a list of business objects.
/// </summary>
/// <typeparam name="T">
/// The data type being stored in the collection instance.
/// </typeparam>
/// <seealso cref="BusinessBase" />
/// <seealso cref="List{T}"/>
public class BusinessCollectionBase<T> : List<T>
    where T : BusinessBase
{
    #region Constructor / Destructor Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessCollectionBase{T}"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BusinessCollectionBase()
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessCollectionBase{T}"/> class.
    /// </summary>
    /// <param name="capacity">
    /// The number of elements that the new list can initially store.
    /// </param>
    public BusinessCollectionBase(int capacity) : base(capacity)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessCollectionBase{T, E}"/> class.
    /// </summary>
    /// <param name="sourceList">
    /// An <see cref="IEnumerable{T}"/> instance containing the objects used to
    /// populate the collection.
    /// </param>
    public BusinessCollectionBase(IEnumerable<T>? sourceList)
    {
        if (sourceList != null)
        {
            AddRange(sourceList);
        }
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="BusinessCollectionBase{T, E}"/> class.
    /// </summary>
    ~BusinessCollectionBase()
    {
        Clear();
    }
    #endregion

    #region Protected Methods / Functions
    /// <summary>
    /// Records, logs, or otherwise stores the exception information when an exception is caught.
    /// </summary>
    /// <param name="ex">
    /// The <see cref="Exception"/> instance that was caught.
    /// </param>
    protected virtual void RecordException(Exception ex)
    {
        ExceptionLog.LogException(ex);
    }
    #endregion

    #region Public Static Methods / Functions
    /// <summary>
    /// Determines whether the specified list is null or empty.
    /// </summary>
    /// <param name="collection">
    /// The collection instance to be checked.
    /// </param>
    /// <returns>
    ///   <c>true</c> if [is null or empty] [the specified collection]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNullOrEmpty(BusinessCollectionBase<T>? collection)
    {
        return (collection == null || collection.Count == 0);
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Combines the tasks of removing the item from the collection and marking it as deleted
    /// on the server.
    /// </summary>
    /// <remarks>
    /// If the delete operation fails, the instance will remain in the collection.
    /// </remarks>
    /// <param name="itemToDelete">
    /// The <typeparamref name="T"/> item to be removed and deleted.
    /// </param>
    /// <returns>
    /// <b>true</b> if the operation is successful; otherwise, returns 
    /// <b>false</b>.
    /// </returns>
    public async Task<bool> RemoveAndDeleteAsync(T itemToDelete)
    {
        // Remove the instance from the collection
        bool success = InnerRemove(itemToDelete);
        if (success)
        {
            // Delete from the server,
            success = await PerformDeleteAsync(itemToDelete).ConfigureAwait(false);

            // If successful, ensure the object is disposed.
            if (success)
            {
                itemToDelete.Dispose();
            }
            else
                // Otherwise, re-add to the collection since it was not deleted.
            {
                Add(itemToDelete);
            }
        }

        return success;
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="BusinessCollectionBase{T, E}"/>.
    /// </summary>
    /// <param name="itemToRemove">
    /// The object to remove from the collection.
    /// </param>
    /// <returns>
    /// <b>true</b> if item was successfully removed from the collection; otherwise, false. This method
    /// also returns false if item is not found in the collection.
    /// </returns>
    private bool InnerRemove(T itemToRemove)
    {
        bool success = false;
        try
        {
            success = Remove(itemToRemove);
        }
        catch (Exception ex)
        {
            RecordException(ex);
        }

        return success;
    }
    /// <summary>
    /// Performs the deletion of the specified object.
    /// </summary>
    /// <param name="itemToRemove">
    /// The <typeparamref name="T"/> instance to be deleted.
    /// </param>
    /// <returns>
    /// <b>true</b> if the operation is successful; otherwise, returns 
    /// <b>false</b>.
    /// </returns>
    private async Task<bool> PerformDeleteAsync(T itemToRemove)
    {
        bool success = false;
        try
        {
            success = await itemToRemove.DeleteAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            RecordException(ex);
        }

        return success;
    }
    #endregion
}