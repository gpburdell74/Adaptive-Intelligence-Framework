using Adaptive.Intelligence.Shared.Logging;
using System.Collections;

namespace Adaptive.Intelligence.Shared;

/// <summary>
/// Provides the base implementation for a collection that contains a list of business objects.
/// </summary>
/// <typeparam name="T">
/// The data type being stored in the collection instance.
/// </typeparam>
/// <typeparam name="E">
/// The data type of the underlying entity instance.
/// </typeparam>
/// <seealso cref="BusinessBase" />
/// <seealso cref="List{T}"/>
public class BusinessCollectionBase<T, E> : BusinessCollectionBase<T>
    where T : BusinessBase<E>
    where E : class
{
    #region Constructor / Destructor Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessCollectionBase{T, E}"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BusinessCollectionBase() : base()
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessCollectionBase{T, E}"/> class.
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
    public BusinessCollectionBase(IEnumerable<T>? sourceList) : base(sourceList)
    {
        if (sourceList != null)
            AddRange(sourceList);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessCollectionBase{T, E}"/> class.
    /// </summary>
    /// <param name="entityList">
    /// The <see cref="List{E}"/> of entity instance containing the data for the business object.
    /// </param>
    protected BusinessCollectionBase(List<E> entityList) : base()
    {
        // Create the new list of business objects from the provided entity list.
        CreateAndAddBusinessObjects(entityList);
    }


    /// <summary>
    /// Finalizes an instance of the <see cref="BusinessCollectionBase{T, E}"/> class.
    /// </summary>
    ~BusinessCollectionBase()
    {
        Clear();
    }
    #endregion

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
    public static bool IsNullOrEmpty(IList? listToCheck)
    {
        return listToCheck == null || listToCheck.Count == 0;
    }
    #endregion

    #region Private Methods / Functions

    /// <summary>
    /// Creates all the business objects from the provided list of entity instances and adds them
    /// to the current collection.
    /// </summary>
    /// <param name="entityList">
    /// The <see cref="IEnumerable{E}"/> list of entity instance of type <typeparamref name="E"/>.
    /// </param>
    private void CreateAndAddBusinessObjects(IEnumerable<E> entityList)
    {
        Clear();

        // Add a business object for each entity.
        foreach (E entity in entityList)
        {
            T? businessObject = CreateBusinessObject(entity);
            if (businessObject != null)
            {
                Add(businessObject);
            }
        }
    }

    /// <summary>
    /// Creates the business object from the provided entity instance.
    /// </summary>
    /// <param name="entity">
    /// The entity instance of type <typeparamref name="E"/>.
    /// </param>
    /// <returns>
    /// The new <typeparamref name="T"/> instance for the entity.
    /// </returns>
    private static T? CreateBusinessObject(E entity)
    {
        T? newInstance;

        // Create a new business object instance.
        try
        {
            newInstance = Activator.CreateInstance(typeof(T), entity) as T;
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
            newInstance = null;
        }

        return newInstance;
    }
    #endregion
}