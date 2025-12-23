using Adaptive.Intelligence.Shared.Logging;

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
    protected AdaptiveCollectionBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdaptiveCollectionBase{T}"/> class.
    /// </summary>
    /// <param name="capacity">
    /// The number of elements that the new list can initially store.
    /// </param>
    protected AdaptiveCollectionBase(int capacity) : base(capacity)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdaptiveCollectionBase{T}"/> class.
    /// </summary>
    /// <param name="sourceList">
    /// An <see cref="IEnumerable{T}"/> instance containing the objects used to
    /// populate the collection.
    /// </param>
    protected AdaptiveCollectionBase(IEnumerable<T>? sourceList)
    {
        if (sourceList != null)
        {
            AddRange(sourceList);
        }
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