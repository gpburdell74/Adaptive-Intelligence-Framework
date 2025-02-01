using Adaptive.Intelligence.Shared.Logging;
using System.Collections;

namespace Adaptive.Intelligence.Shared;

/// <summary>
/// Provides the base implementation for a collection that contains a list of business objects.
/// </summary>
/// <typeparam name="T">
/// The data type being stored in the collection instance.
/// </typeparam>
/// <seealso cref="List{T}"/>
public class AdaptiveCollectionBase<T> : List<T>
{
    #region Constructor / Destructor Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="AdaptiveCollectionBase{T}"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public AdaptiveCollectionBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdaptiveCollectionBase{T}"/> class.
    /// </summary>
    /// <param name="capacity">
    /// The number of elements that the new list can initially store.
    /// </param>
    public AdaptiveCollectionBase(int capacity) : base(capacity)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdaptiveCollectionBase{T}"/> class.
    /// </summary>
    /// <param name="sourceList">
    /// An <see cref="IEnumerable{T}"/> instance containing the objects used to
    /// populate the collection.
    /// </param>
    public AdaptiveCollectionBase(IEnumerable<T>? sourceList)
    {
        if (sourceList != null)
        {
            AddRange(sourceList);
        }
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="AdaptiveCollectionBase{T}"/> class.
    /// </summary>
    ~AdaptiveCollectionBase()
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
}

public static class ListExtensions
{
    #region Public Static Methods / Functions
    /// <summary>
    /// Gets a value indicating whether the specified list is null or empty.
    /// </summary>
    /// <param name="listToCheck">
    /// The <see cref="IList"/> to be checked.
    /// </param>
    /// <returns>
    /// <b>true</b> if <i>listToCheck</i> is <b>null</b>, or <i>listToChecks</i> Count property is zero.
    /// Otherwise, returns <b>false</b>.
    /// </returns>
    public static bool IsNullOrEmpty(this IList? listToCheck)
    {
        return listToCheck == null || listToCheck.Count == 0;
    }
    #endregion
}
