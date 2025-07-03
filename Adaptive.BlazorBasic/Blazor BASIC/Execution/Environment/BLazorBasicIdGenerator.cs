using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

/// <summary>
/// Provides the global ID generator engine for Blazor Basic.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IIdGenerator" />
public class BLazorBasicIdGenerator : DisposableObjectBase, IIdGenerator
{
    #region Private Member Declarations    
    /// <summary>
    /// The thread synchronization root instance.
    /// </summary>
    private static readonly object _syncRoot = new object();

    /// <summary>
    /// The list of ID values that have been allocated.
    /// </summary>
    private List<int>? _idValuesInUse;

    /// <summary>
    /// The list of ID values that had been allocated and then released.
    /// </summary>
    private List<int>? _idValuesNotUsed;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BLazorBasicIdGenerator"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BLazorBasicIdGenerator()
    {
        _idValuesInUse = new List<int>();
        _idValuesNotUsed = new List<int>();
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
            _idValuesInUse?.Clear();
            _idValuesNotUsed?.Clear();
        }

        _idValuesInUse = null;
        _idValuesNotUsed = null;

        base.Dispose(disposing);

    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Gets the next available ID value.
    /// </summary>
    /// <returns>
    /// An integer containing the ID value.
    /// </returns>
    public int Next()
    {
        int newId = 0;
        lock (_syncRoot)
        {
            // Ensure objects exist.
            if (_idValuesInUse == null)
                _idValuesInUse = new List<int>();
            if (_idValuesNotUsed == null)
                _idValuesNotUsed = new List<int>();

            // First use.
            if (_idValuesInUse.Count == 0)
            {
                _idValuesInUse.Add(1);
                newId = 1;
            }
            else
            {
                // Re-use any released ID value, if present.
                if (_idValuesNotUsed.Count > 0)
                {
                    newId = _idValuesNotUsed[0];
                    _idValuesNotUsed.RemoveAt(0);
                }
                else
                {
                    // Otherwise, get the next integer.
                    newId = _idValuesInUse[_idValuesInUse.Count - 1] + 1;
                }
            }

            // Mark the new ID as in use.
            _idValuesInUse.Add(newId);
        }
        return newId;
    }
    /// <summary>
    /// Releases the specified ID value from use.
    /// </summary>
    /// <param name="id">An integer containing the ID value to be released.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void Release(int id)
    {
        lock (_syncRoot)
        {
            // Ensure objects exist.
            if (_idValuesInUse == null)
                _idValuesInUse = new List<int>();
            if (_idValuesNotUsed == null)
                _idValuesNotUsed = new List<int>();

            // Remove the value from the list of used ID values.
            int index = _idValuesInUse.IndexOf(id);
            if (index > -1)
                _idValuesInUse.RemoveAt(index);

            // Add to the previously-used-but-released list.
            _idValuesNotUsed.Add(id);
        }
    }
    #endregion
}
