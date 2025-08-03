using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

/// <summary>
/// Manages a list of open files by file handle.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public sealed class OpenFileTable : DisposableObjectBase
{
    #region Private Member Declarations    
    /// <summary>
    /// The file table keyed by handle value.
    /// </summary>
    private Dictionary<int, FileStream>? _fileTable;
    /// <summary>
    /// The file handles list.
    /// </summary>
    private List<int>? _fileHandles;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenFileTable"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public OpenFileTable()
    {
        _fileTable = new Dictionary<int, FileStream>();
        _fileHandles = new List<int>();
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
            _fileHandles?.Clear();
            _fileTable?.Clear();
        }

        _fileHandles = null;
        _fileTable = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Determines whether the specified handle is in use.
    /// </summary>
    /// <param name="handle">
    /// An integer specifying the handle to check for.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the specified handle is in use; otherwise, <c>false</c>.
    /// </returns>
    public bool ContainsHandle(int handle)
    {
        if (_fileHandles == null)
            return false;

        return _fileHandles.Contains(handle);
    }

    /// <summary>
    /// Gets the reference to the file stream represented by the file handle.
    /// </summary>
    /// <param name="lineNumber">
    /// The current line number.
    /// </param>
    /// <param name="fileHandle">
    /// An integer specifying the file handle.
    /// </param>
    /// <returns>
    /// The <see cref="FileStream"/> being represented by the file handle.
    /// </returns>
    /// <exception cref="BasicInvalidArgumentException">
    /// Occurs when an invalid file handle is specified.
    /// </exception>
    public FileStream GetFileStream(int lineNumber, int fileHandle)
    {
        if (_fileHandles == null || _fileTable==null || !_fileTable.ContainsKey(fileHandle))
            throw new BasicInvalidArgumentException(lineNumber, "Invalid file handle specified.");

        return _fileTable[fileHandle];
    }

    /// <summary>
    /// Ensures all the open files are closed.
    /// </summary>
    public void SafeShutdown()
    {
        if (_fileTable != null)
        {
            foreach (FileStream fs in _fileTable.Values)
            {
                try
                {
                    fs.Close();
                    fs.Dispose();
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }
            _fileTable?.Clear();
            _fileHandles?.Clear();
        }
    }

    /// <summary>
    /// Registers the specified file handle and open file stream for use.
    /// </summary>
    /// <param name="fileHandle">The file handle.</param>
    /// <param name="stream">The stream.</param>
    public void Register(int lineNumber, int fileHandle, FileStream stream)
    {
        if (_fileHandles == null || _fileTable == null)
            throw new BasicEngineExecutionException(lineNumber, "File handle register not initialized.");

        if (_fileTable.ContainsKey(fileHandle))
        {
            try
            {
                stream.Close();
                stream.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
            throw new BasicHandleInUseException(lineNumber, "File handle in use.");
        }

        _fileHandles.Add(fileHandle);
        _fileTable.Add(fileHandle, stream);

    }
    /// <summary>
    /// Unregisters the file stream for system use.
    /// </summary>
    /// <param name="lineNumber">
    /// The current line number.
    /// </param>
    /// <param name="fileHandle">
    /// An integer specifying the file handle.
    /// </param>
    /// <returns>
    /// The <see cref="FileStream"/> being represented by the file handle.
    /// </returns>
    /// <exception cref="BasicInvalidArgumentException">
    /// Occurs when an invalid file handle is specified.
    /// </exception>
    public FileStream UnRegister(int lineNumber, int fileHandle)
    {
        if (_fileHandles == null || _fileTable== null || !_fileTable.ContainsKey(fileHandle))
            throw new BasicInvalidArgumentException(lineNumber, "Invalid file handle specified.");

        FileStream fs = _fileTable[fileHandle];
        _fileTable.Remove(fileHandle);
        _fileHandles.Remove(fileHandle);

        return fs;
    }
    #endregion

    #region
    #endregion

}
