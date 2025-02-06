using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;

namespace Adaptive.Taz
{
    /// <summary>
    /// Represents and contains the data for a file compression operation.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class FileCompressionOperation : DisposableObjectBase
    {
        #region Public Events		
        /// <summary>
        /// Occurs when the compression operation starts.
        /// </summary>
        public event EventHandler? CompressionStart;
        /// <summary>
        /// Occurs when the compression operation completes.
        /// </summary>
        public event EventHandler? CompressionComplete;
        /// <summary>
        /// Occurs when an error occurs during the compression operation.
        /// </summary>
        public event FileEventHandler? CompressionError;
        /// <summary>
        /// Occurs when the file load operation starts.
        /// </summary>
        public event FileEventHandler? FileLoadStart;
        /// <summary>
        /// Occurs when an error occurs during the file load operation.
        /// </summary>
        public event FileEventHandler? FileLoadError;
        /// <summary>
        /// Occurs when the file load operation completes.
        /// </summary>
        public event FileEventHandler? FileLoadComplete;
        #endregion

        #region Private Member Declarations		
        /// <summary>
        /// The fully-qualified path and name of the file to be compressed and archived.
        /// </summary>
        private string? _fileName;
        /// <summary>
        /// The file name component.
        /// </summary>
        private string? _name;
        /// <summary>
        /// The original path component.
        /// </summary>
        private string? _path;
        /// <summary>
        /// The original file size.
        /// </summary>
        private long _originalSize;
        /// <summary>
        /// The computed SHA-512 hash of the file data.
        /// </summary>
        private byte[]? _computedHash;
        /// <summary>
        /// The original file data in memory.
        /// </summary>
        private byte[]? _fileData;
        /// <summary>
        /// The compressed data in memory.
        /// </summary>
        private byte[]? _compressedData;
        /// <summary>
        /// The size of the compressed data in memory.
        /// </summary>
        private long _compressedSize;
        /// <summary>
        /// The position in the output file at which the data is to be written.
        /// </summary>
        private long _outputFilePosition;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="FileCompressionOperation"/> class.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the fully-qualified path and name of the file to be operated on.
        /// </param>
        public FileCompressionOperation(string fileName)
        {
            _fileName = fileName;
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
                ByteArrayUtil.Clear(_compressedData);
                ByteArrayUtil.Clear(_computedHash);
                ByteArrayUtil.Clear(_fileData);
            }

            _fileData = null;
            _compressedData = null;
            _computedHash = null;
            _fileName = null;
            _name = null;
            _path = null;
            _originalSize = 0;
            _outputFilePosition = 0;
            _compressedSize = 0;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties				
        /// <summary>
        /// Gets the reference to the byte array containing the compressed data.
        /// </summary>
        /// <value>
        /// A byte array containing the compressed data, or <b>null</b>.
        /// </value>
        public byte[]? CompressedData => _compressedData;
        /// <summary>
        /// Gets the name of the file being compressed.
        /// </summary>
        /// <value>
        /// A string containing the fully-qualified path and name of the file.
        /// </value>
        public string? FileName => _fileName;
        /// <summary>
        /// Determines whether the specified file name value is valid.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the file name is valid; otherwise, returns <b>false</b>.
        /// </returns>
        public bool IsValid => !string.IsNullOrEmpty(_fileName) && SafeIO.FileExists(_fileName);
        /// <summary>
        /// Gets the reference to the byte array containing the hash value data.
        /// </summary>
        /// <value>
        /// A byte array containing the hash value data, or <b>null</b>.
        /// </value>
        public byte[]? Hash => ByteArrayUtil.CopyToNewArray(_computedHash);
        /// <summary>
        /// Gets the original file name.
        /// </summary>
        /// <value>
        /// A string containing the value, or <b>null</b>.
        /// </value>
        public string? OrigName => _name;
        /// <summary>
        /// Gets the original file path.
        /// </summary>
        /// <value>
        /// A string containing the value, or <b>null</b>.
        /// </value>
        public string? OrigPath => _path;
        /// <summary>
        /// Gets the size of the original file, in bytes.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the original file size.
        /// </value>
        public long OrigSize => _originalSize;
        /// <summary>
        /// Gets the size of the compressed file, in bytes.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the compressed file size.
        /// </value>
        public long CompressedSize => _compressedSize;
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Performs the task of compressing the original file data.
        /// </summary>
        public void CompressFile()
        {
            OnCompressionStart();
            ByteArrayUtil.Clear(_compressedData);
            _compressedSize = 0;
            _compressedData = null;

            try
            {
                if (_fileData != null)
                {
                    _compressedData = AdaptiveCompression.Compress(_fileData);
                    _compressedSize = _compressedData.Length;
                }
            }
            catch (Exception ex)
            {
                OnCompressionError(_fileName ?? string.Empty, ex);
                _compressedData = null;
            }
            OnCompressionComplete();
        }
        /// <summary>
        /// Creates the hash value for the original file data.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="ITazContentWriter"/> instance that contains the hash provider instance.
        /// </param>
        public void CreateHash(ITazContentWriter writer)
        {
            if (_fileData != null)
                _computedHash = writer.CreateHash(_fileData);
        }
        /// <summary>
        /// Loads the file data and meta data into memory.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> LoadFileDataAsync()
        {
            bool success = IsValid;
            OnFileLoadStart(_fileName);
            if (success)
            {
                // Read the file meta data.
                if (_fileName != null)
                {
                    _name = Path.GetFileName(_fileName);
                    _path = Path.GetDirectoryName(_fileName);
                    _originalSize = SafeIO.GetFileSizeNative(_fileName);
                }

                // Load the actual file data.
                await ReadFileDataAsync().ConfigureAwait(false);
            }
            OnFileLoadComplete(_fileName ?? string.Empty);
            return success;
        }
        /// <summary>
        /// Converts the current instance to a <see cref="TazDirectoryEntry"/> instance.
        /// </summary>
        /// <returns>
        /// A <see cref="TazDirectoryEntry"/> instance representing the file entry.
        /// </returns>
        public TazDirectoryEntry ToDirEntry()
        {
            return new TazDirectoryEntry
            {
                OrigName = _name,
                OrigPath = _path,
                OrigSize = _originalSize,
                Position = _outputFilePosition,
                CompressedSize = _compressedSize,
                Hash = ByteArrayUtil.CopyToNewArray(_computedHash)
            };
        }
        /// <summary>
        /// Converts the current instance to a <see cref="TazDirectoryEntry"/> instance.
        /// </summary>
        /// <param name="currentPosition">
        /// A value indicating the current position in the output file the actual compressed data is written to.
        /// </param>
        /// <returns>
        /// A <see cref="TazDirectoryEntry"/> instance representing the file entry.
        /// </returns>
        public TazDirectoryEntry ToDirEntry(long currentPosition)
        {
            return new TazDirectoryEntry
            {
                OrigName = _name,
                OrigPath = _path,
                OrigSize = _originalSize,
                Position = currentPosition,
                CompressedSize = _compressedSize,
                Hash = ByteArrayUtil.CopyToNewArray(_computedHash)
            };
        }
        #endregion

        #region Private Event Methods		
        /// <summary>
        /// Raises the <see cref="CompressionStart"/> event.
        /// </summary>
        private void OnCompressionStart()
        {
            CompressionStart?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Raises the <see cref="CompressionComplete"/> event.
        /// </summary>
        private void OnCompressionComplete()
        {
            CompressionComplete?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Raises the <see cref="CompressionError" /> event.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the name of the file.
        /// </param>
        /// <param name="ex">
        /// The <see cref="Exception"/> that was caught.
        /// </param>
        private void OnCompressionError(string fileName, Exception ex)
        {
            FileEventArgs evArgs = new FileEventArgs
            {
                FileName = fileName,
                Exception = ex
            };
            CompressionError?.Invoke(this, evArgs);
        }
        /// <summary>
        /// Raises the <see cref="FileLoadStart"/> event.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the name of the file.
        /// </param>
        private void OnFileLoadStart(string? fileName)
        {
            FileEventArgs evArgs = new FileEventArgs { FileName = fileName };
            FileLoadStart?.Invoke(this, evArgs);
        }
        /// <summary>
        /// Raises the <see cref="FileLoadError"/> event.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the name of the file.
        /// </param>
        /// <param name="ex">
        /// The <see cref="Exception"/> that was caught.
        /// </param>
        private void OnFileLoadError(string? fileName, Exception ex)
        {
            FileEventArgs evArgs = new FileEventArgs
            {
                FileName = fileName,
                Exception = ex
            };
            FileLoadError?.Invoke(this, evArgs);
        }
        /// <summary>
        /// Raises the <see cref="FileLoadComplete"/> event.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the name of the file.
        /// </param>
        private void OnFileLoadComplete(string fileName)
        {
            FileEventArgs evArgs = new FileEventArgs { FileName = fileName };
            FileLoadComplete?.Invoke(this, evArgs);
        }
        #endregion

        #region Private Methods / Functions		
        /// <summary>
        /// Loads the file data into memory.
        /// </summary>
        private async Task ReadFileDataAsync()
        {
            ByteArrayUtil.Clear(_fileData);
            _fileData = null;

            // Try to load the data.
            if (_fileName != null)
            {
                IOperationalResult<byte[]> result = await SafeIO.ReadBytesFromFileWithResultAsync(_fileName).ConfigureAwait(false);
                _fileData = ByteArrayUtil.CopyToNewArray(result.DataContent);

                if (!result.Success || _fileData == null || _fileData.Length == 0)
                {
                    if (result.HasExceptions)
                        OnFileLoadError(_fileName, result.FirstException!);
                    else
                        OnFileLoadError(_fileName, new FileLoadException("Failed to load file data."));
                }

                result.Dispose();
            }
        }
        #endregion
    }
}
