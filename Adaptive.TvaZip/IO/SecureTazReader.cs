using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using Adaptive.Taz.Cryptography;
using System.Security.Cryptography;

namespace Adaptive.Taz.IO;

/// <summary>
/// Provides the reader mechanism for reading TAZ files from an encrypted format.	
/// </summary>
/// <seealso cref="ExceptionTrackingBase" />
/// <seealso cref="ITazContentReader" />
public sealed class SecureTazReader : ExceptionTrackingBase, ITazContentReader
{
    #region Private Member Declarations		
    /// <summary>
    /// The SHA-512 hash provider instance.
    /// </summary>
    private SHA512? _hashProvider;
    /// <summary>
    /// The path in which the archive will be created.
    /// </summary>
    private string? _path;
    /// <summary>
    /// The file name.
    /// </summary>
    private string? _fileName;
    /// <summary>
    /// The output stream to write to.
    /// </summary>
    private FileStream? _sourceStream;
    /// <summary>
    /// The reader instance.
    /// </summary>
    private ISafeBinaryReader? _reader;
    /// <summary>
    /// The encryption key manager.
    /// </summary>
    private KeyManager? _manager;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="TazReader"/> class.
    /// </summary>
    /// <param name="pathAndFileName">
    /// A string containing the fully-qualified path and name of the archive file.
    /// </param>
    /// <param name="manager">
    /// The <see cref="KeyManager"/> instance used to provide the encryption key data.
    /// </param>
    public SecureTazReader(string pathAndFileName, KeyManager manager)
    {
        _path = Path.GetDirectoryName(pathAndFileName);
        _fileName = Path.GetFileName(pathAndFileName);
        _hashProvider = SHA512.Create();
        _manager = manager;
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
            _hashProvider?.Dispose();
        }

        _manager = null;
        _hashProvider = null;
        _path = null;
        _fileName = null;
        base.Dispose(disposing);
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous dispose operation.
    /// </returns>
    public async ValueTask DisposeAsync()
    {
        await Task.Yield();
        Dispose(true);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets a value indicating whether this instance can be used to write data.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
    /// </value>
    public bool CanRead => _reader != null && _sourceStream != null && _manager != null && _sourceStream.CanRead;
    /// <summary>
    /// Gets the current position in the file being written.
    /// </summary>
    /// <value>
    /// A <see cref="long"/> specifying the index of current position in the file.
    /// </value>
    public long CurrentPosition
    {
        get
        {
            if (_sourceStream == null)
                return -1;
            else
                return _sourceStream.Position;
        }
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Closes the underlying file stream.
    /// </summary>
    public async Task CloseFileAsync()
    {
        try
        {
            if (_reader != null)
                await _reader.DisposeAsync().ConfigureAwait(false);
            if (_sourceStream != null)
                await _sourceStream.DisposeAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Exceptions?.Add(ex);
        }
        _reader = null;
        _sourceStream = null;
    }
    /// <summary>
    /// Performs the initialization and other operations to prepare to read an archive file.
    /// </summary>
    /// <returns>
    /// A value indicating whether the operation was successful.
    /// </returns>
    public async Task<bool> InitializeFileAsync()
    {
        bool success = false;
        await Task.Yield();

        if (OpenFileForReading() && _sourceStream != null)
        {
            if (_manager != null)
            {
                _reader = new SecureBinaryReader(_manager, _sourceStream);
                success = true;
            }
        }
        else
        {
            _reader = null;
            _sourceStream = null;
        }
        return success;
    }
    /// <summary>
    /// Reads the next byte array from the underlying stream.
    /// </summary>
    /// <param name="position">A <see cref="long" /> value indicating the position at which to begin reading.</param>
    /// <returns>
    /// The byte array that was read, or <b>null</b> if the operation fails.
    /// </returns>
    /// <remarks>
    /// This method assumes the array is preceded by an integer length indicator.
    /// </remarks>
    public byte[]? ReadArray(long position)
    {
        byte[]? data = null;

        if (CanRead)
        {
            _sourceStream?.Seek(position, SeekOrigin.Begin);
            data = ReadArray();
        }
        return data;
    }
    /// <summary>
    /// Reads the next byte array from the underlying stream.
    /// </summary>
    /// <returns>
    /// The byte array that was read, or <b>null</b> if the operation fails.
    /// </returns>
    /// <remarks>
    /// This method assumes the array is preceded by an integer length indicator.
    /// </remarks>
    public byte[]? ReadArray()
    {
        byte[]? data = null;

        if (CanRead)
        {
            data = _reader?.ReadByteArray();
        }
        return data;
    }
    /// <summary>
    /// Reads the directory content into memory.
    /// </summary>
    /// <param name="directoryStart">A <see cref="long" /> specifying the location in the file at which to read the directory data.</param>
    /// <returns>
    /// A <see cref="TazDirectory" /> instance that was read from the file, or <b>null</b> if the operation failed.
    /// </returns>
    public TazDirectory? ReadDirectory(long directoryStart)
    {
        TazDirectory? directory = null;

        if (CanRead && _sourceStream != null && _reader != null)
        {
            ((SecureBinaryReader)_reader).SetForKeyVariant();
            // Move to the directory start position.	
            _sourceStream.Seek(directoryStart, SeekOrigin.Begin);

            byte[]? directoryData = _reader.ReadByteArray();
            if (directoryData != null)
            {
                directory = new TazDirectory(directoryData);
                ByteArrayUtil.Clear(directoryData);
            }
            ((SecureBinaryReader)_reader).SetForKeyStandard();
        }
        return directory;
    }
    /// <summary>
    /// Reads the file header content into memory.
    /// </summary>
    /// <returns>
    /// A <see cref="TazFileHeader" /> instance that was read from the file, or <b>null</b> if the operation failed.
    /// </returns>
    public TazFileHeader? ReadHeader()
    {
        TazFileHeader? header = null;

        if (CanRead)
        {
            // Move to the start of the file.
            _sourceStream?.Seek(0, SeekOrigin.Begin);

            // Read the header data (always in the clear).
            byte[]? headerData = _reader?.Reader?.ReadBytes(FileSpecConstants.FileHeaderLength);

            header = new TazFileHeader(true);
            if (headerData != null)
            {
                header.FromBytes(headerData);
                ByteArrayUtil.Clear(headerData);
            }
        }
        return header;
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Opens the file for reading.
    /// </summary>
    /// <returns>
    /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
    /// </returns>
    private bool OpenFileForReading()
    {
        bool success = false;
        string fn = _path + "\\" + _fileName;

        if (!string.IsNullOrEmpty(fn))
        {
            // Try to open the file for reading.	
            try
            {
                _sourceStream = new FileStream(fn, FileMode.Open, FileAccess.Read, FileShare.Read);
                success = _sourceStream.CanRead;
            }
            catch
            {
                _sourceStream = null;
            }
        }

        return success;
    }
    #endregion

}
