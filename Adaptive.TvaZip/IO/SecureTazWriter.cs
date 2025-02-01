using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using Adaptive.Taz.Cryptography;
using System.Security.Cryptography;

namespace Adaptive.Taz.IO
{
    /// <summary>
    /// Provides the writer mechanism for storing TAZ files in an encrypted format.	
    /// </summary>
    /// <seealso cref="ExceptionTrackingBase" />
    /// <seealso cref="ITazContentWriter" />
    public sealed class SecureTazWriter : ExceptionTrackingBase, ITazContentWriter
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
        private FileStream? _outputStream;
        /// <summary>
        /// The writer instance.
        /// </summary>
        private ISafeBinaryWriter? _writer;
        /// <summary>
        /// The encryption key manager.
        /// </summary>
        private KeyManager? _keyManager;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="TazWriter"/> class.
        /// </summary>
        /// <param name="pathAndFileName">
        /// A string containing the fully-qualified path and name of the archive file.
        /// </param>
        public SecureTazWriter(string pathAndFileName, KeyManager keyManager)
        {
            _path = Path.GetDirectoryName(pathAndFileName);
            _fileName = Path.GetFileName(pathAndFileName);
            _hashProvider = SHA512.Create();
            _keyManager = keyManager;
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

            _keyManager = null;
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
        public bool CanWrite => _writer != null && _outputStream != null && _outputStream.CanWrite;
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
                if (_outputStream == null)
                    return -1;
                else
                    return _outputStream.Position;
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Closes the underlying file and disposes of all stream and writer instances.
        /// </summary>
        public async Task CloseFileAsync()
        {
            if (_writer != null)
                await _writer.DisposeAsync().ConfigureAwait(false);

            if (_outputStream != null)
                await _outputStream.DisposeAsync().ConfigureAwait(false);

            _writer = null;
            _outputStream = null;
        }
        /// <summary>
        /// Generates an SHA-512 hash of the provided data.
        /// </summary>
        /// <param name="data">
        /// A byte array containing the data to be hashed.
        /// </param>
        /// <returns>
        /// A byte array containing the hash value.
        /// </returns>
        public byte[]? CreateHash(byte[] data)
        {
            byte[]? hash = null;
            try
            {
                hash = _hashProvider?.ComputeHash(data);
            }
            catch (Exception ex)
            {
                Exceptions?.Add(ex);
            }
            return hash;
        }
        /// <summary>
        /// Ensures the content in the write buffers to written to the underlying stream.
        /// </summary>
        public void Flush()
        {
            if (_writer != null)
            {
                try
                {
                    _writer.Flush();
                }
                catch (Exception ex)
                {
                    Exceptions?.Add(ex);
                }
            }
        }
        /// <summary>
        /// Ensures the content in the write buffers to written to the underlying stream.
        /// </summary>
        public async Task FlushAsync()
        {
            if (_writer != null && _outputStream != null)
            {
                try
                {
                    _writer.Flush();
                    await _outputStream.FlushAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Exceptions?.Add(ex);
                }
            }
        }
        /// <summary>
        /// Performs the initialization and other operations to prepare to write an archive file.
        /// </summary>
        public async Task<bool> InitializeFileAsync()
        {
            bool success = false;
            await Task.Yield();

            if (OpenFileForWriting() && _outputStream != null && _keyManager != null)
            {
                _writer = new SecureBinaryWriter(_keyManager, _outputStream);
                success = true;
            }
            else
            {
                _writer = null;
                _outputStream = null;
            }
            return success;
        }
        /// <summary>
        /// Writes the specified byte array to the underlying stream.
        /// </summary>
        /// <remarks>
        /// The data format is left to the specific implementation; the data may or may nit be preceded by a length
        /// or a null indicator, or any other meta data needed to read and understand the provided byte array.
        /// </remarks>
        /// <param name="data">
        /// A byte array containing the data to be written.
        /// </param>
        public void WriteArray(byte[] data)
        {
            if (CanWrite && data != null && data.Length > 0)
            {
                _writer?.Write(data);
                _writer?.Flush();
            }
        }
        /// <summary>
        /// Writes the directory to the underlying file.
        /// </summary>
        /// <param name="directory">
        /// The <see cref="TazDirectory"/> instance whose contents are to be written.
        /// </param>
        /// <returns>
        /// A <see cref="long"/> value indicating the position at which the directory begins.
        /// </returns>
        public long WriteDirectory(TazDirectory directory)
        {
            long position = -1;
            if (CanWrite && _outputStream != null && _writer != null)
            {
                // Use the key variants for the directory.
                ((SecureBinaryWriter)_writer).SetForKeyVariant();

                // Capture the position, and then write the length indicator.
                position = _outputStream.Position;
                byte[] directoryData = directory.ToBytes();
                _writer.Write(directoryData.Length);
                _writer.Write(directoryData);
                _writer.Flush();

                ByteArrayUtil.Clear(directoryData);
                ((SecureBinaryWriter)_writer).SetForKeyStandard();
            }
            return position;
        }
        /// <summary>
        /// Writes the file header to the underlying file.
        /// </summary>
        /// <param name="directory">
        /// The <see cref="TazFileHeader"/> instance whose contents are to be written.
        /// </param>
        public void WriteHeader(TazFileHeader header)
        {
            if (CanWrite)
            {
                // Ensure we are at the start of the file.
                _outputStream?.Seek(0, SeekOrigin.Begin);

                // Write the content.
                if (_writer != null)
                {
                    TazFileHeader.WriteHeader(header, _writer);
                }
            }
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Creates the new file.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        private bool OpenFileForWriting()
        {
            bool success = false;
            string fn = _path + "\\" + _fileName;

            if (!string.IsNullOrEmpty(fn))
            {
                // Delete the file if it already exists.	
                SafeIO.DeleteFile(fn);

                // Try to open the file for writing.	
                try
                {
                    _outputStream = new FileStream(fn, FileMode.CreateNew, FileAccess.Write);
                    success = _outputStream.CanWrite;
                }
                catch
                {
                    _outputStream = null;
                }
            }

            return success;
        }
        #endregion
    }
}
