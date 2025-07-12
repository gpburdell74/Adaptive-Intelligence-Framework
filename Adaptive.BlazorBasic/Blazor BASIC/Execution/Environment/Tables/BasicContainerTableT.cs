using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the base implementation for scope-level items that are stored by name, such
/// as variables, procedures, parameters, and functions.
/// </summary>
/// <typeparam name="T">
/// The data type of the element being stored.
/// </typeparam>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IContainerTable" />
public abstract class BasicContainerTable<T> : DisposableObjectBase, IContainerTable
    where T : IScopedElement
{
    #region Private Member Declarations
    /// <summary>
    /// The list of items.
    /// </summary>
    private Dictionary<string, T>? _itemList;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariableTable"/> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    public BasicContainerTable(IScopeContainer? parent)
    {
        Parent = parent;
        _itemList = new Dictionary<string, T>();
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _itemList?.Clear();
        }

        _itemList = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets the number of variables in the table.
    /// </summary>
    /// <value>
    /// An integer containing the count of items.
    /// </value>
    public int Count
    {
        get
        {
            if (_itemList == null)
                return 0;
            return _itemList.Count;
        }
    }
    /// <summary>
    /// Gets the reference to the parent scope container.
    /// </summary>
    /// <value>
    /// A reference to the parent <see cref="IScopeContainer" /> instance.
    /// </value>
    public IScopeContainer? Parent { get; }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Adds the specified instance to the current list.
    /// </summary>
    /// <param name="element">
    /// The <see cref="IScopedElement"/> variable instance to add.
    /// </param>
    public void Add(T element)
    {
        if (_itemList != null)
        {
            string? normalized = NormalizeForKey(element.Name);
            if (normalized != null)
                _itemList.Add(normalized, element);
        }
    }
    /// <summary>
    /// Adds the specified instance to the current list.
    /// </summary>
    /// <param name="element">The <see cref="IScopedElement" /> variable instance to add.</param>
    public void Add(IScopedElement element)
    {
        Add((T)element);
    }

    /// <summary>
    /// Removes all entries.
    /// </summary>
    public void Clear()
    {
        _itemList?.Clear();
    }
    /// <summary>
    /// Determines if an item with the specified name in the table exists.
    /// </summary>
    /// <param name="name">A string containing the name to query for.</param>
    /// <returns>
    /// <b>true</b> if an entry with the specified name exists; otherwise,
    /// returns <b>false</b>/
    /// </returns>
    public bool Exists(string? name)
    {
        string? normalized = NormalizeForKey(name);
        if (_itemList == null || string.IsNullOrEmpty(name) || normalized == null)
            return false;

        return _itemList.ContainsKey(normalized);
    }

    /// <summary>
    /// Gets the reference to the instance with the specified name.
    /// </summary>
    /// <param name="name">
    /// A string containing the name to check for.
    /// </param>
    /// <returns>
    /// The <see cref="IScopedElement"/> instance, or <b>null</b> if not found.
    /// </returns>
    public IScopedElement? GetItem(string? name)
    {
        string? normalized = NormalizeForKey(name);
        if (_itemList == null || string.IsNullOrEmpty(name) || normalized == null)
            return null;

        return _itemList[normalized];
    }
    #endregion

    #region Protected Methods / Functions    
    /// <summary>
    /// Normalizes the supplied string for use as a key value.
    /// </summary>
    /// <param name="original">
    /// A string containing the original value.
    /// </param>
    /// <returns>
    /// The normalized version of the specified string, or <b>null</b>.
    /// </returns>
    protected virtual string? NormalizeForKey(string? original)
    {
        string? normalized = null;
        if (!string.IsNullOrEmpty(original))
        {
            normalized = original.Trim().ToLower();
        }
        return normalized;
    }
    #endregion
}
