using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using Adaptive.Taz.Cryptography;
using Adaptive.Taz.IO;

namespace Adaptive.Taz
{
    /// <summary>
    /// Represents and manages a TAZ archive file.
    /// </summary>
    /// <seealso cref="ExceptionTrackingBase" />
    public sealed class TazFile : ExceptionTrackingBase
    {
        #region Public Events		
        /// <summary>
        /// Occurs when the archive creation process starts.
        /// </summary>
        public event EventHandler? CreateArchiveStart;
        /// <summary>
        /// Occurs when the directory write process starts.
        /// </summary>
        public event EventHandler? DirectoryWriteStart;
        /// <summary>
        /// Occurs when the directory write process is completed.
        /// </summary>
        public event EventHandler? DirectoryWriteCompleted;
        /// <summary>
        /// Occurs when the archive creation process completes.
        /// </summary>
        public event EventHandler? CreateArchiveComplete;
        /// <summary>
        /// Occurs when an individual file compression operation starts.
        /// </summary>
        public event FileEventHandler? FileCompressionStart;
        /// <summary>
        /// Occurs when an individual file compression operation completes.
        /// </summary>
        public event FileEventHandler? FileCompressionComplete;
        /// <summary>
        /// Occurs when the overall compression operation completes.
        /// </summary>
        public event EventHandler? CompressionComplete;
        /// <summary>
        /// Occurs when the overall compression operation starts.
        /// </summary>
        public event EventHandler? CompressionStart;

        #endregion

        #region Private Member Declarations		
        /// <summary>
        /// The header content.
        /// </summary>
        private TazFileHeader? _header;
        /// <summary>
        /// The file directory.
        /// </summary>
        private TazDirectory? _directory;
        /// <summary>
        /// The file path and name.
        /// </summary>
        private string? _fileName;
        /// <summary>
        /// The cryptographic key data manager.
        /// </summary>
        private KeyManager? _keyManager;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="TazFile"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public TazFile()
        {
            _header = new TazFileHeader();
            _directory = new TazDirectory();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TazFile"/> class.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the fully-qualified path and name of the file.
        /// </param>
        public TazFile(string fileName) : this()
        {
            _fileName = fileName;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PKXFile"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is used to create or open encrypted archive files.
        /// </remarks>
        /// <param name="fileName">
        /// A string containing the fully-qualified path and name of the file.
        /// </param>
        /// <param name="userId">
        /// A string containing the user ID value.
        /// </param>
        /// <param name="password">
        /// A string containing the user password value.
        /// </param>
        /// <param name="userPIN">
        /// A string containing the user PIN value.
        /// </param>
        public TazFile(string userId, string password, string userPIN) : this()
        {
            // Create the key manager.
            _keyManager = new KeyManager();
            _keyManager.SetForUser(userId, password, userPIN);

            // Header for the secure file.
            _header = new TazFileHeader(true);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PKXFile"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is used to create or open encrypted archive files.
        /// </remarks>
        /// <param name="userId">
        /// A string containing the user ID value.
        /// </param>
        /// <param name="password">
        /// A string containing the user password value.
        /// </param>
        /// <param name="userPIN">
        /// A string containing the user PIN value.
        /// </param>
        public TazFile(string fileName, string userId, string password, string userPIN) : this(fileName)
        {
            // Create the key manager.
            _keyManager = new KeyManager();
            _keyManager.SetForUser(userId, password, userPIN);

            // Header for the secure file.
            _header = new TazFileHeader(true);
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
                _keyManager?.Dispose();
                _header?.Dispose();
                _directory?.Dispose();
            }

            _keyManager = null;
            _directory = null;
            _fileName = null;
            _header = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the directory instance.
        /// </summary>
        /// <value>
        /// The <see cref="TazDirectory"/> instance.
        /// </value>
        public TazDirectory? Directory => _directory;
        /// <summary>
        /// Gets the reference to the file header instance.
        /// </summary>
        /// <value>
        /// The <see cref="TazFileHeader"/> instance.
        /// </value>
        public TazFileHeader? Header => _header;
        /// <summary>
        /// Gets or sets the name of the TAZ file.
        /// </summary>
        /// <value>
        /// A string containing the fully-qualified path and name of the file, or <b>null</b> if not set.
        /// </value>
        public string? FileName
        {
            get => _fileName;
            set => _fileName = value;
        }
        /// <summary>
        /// Gets a value indicating whether the file is open.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the file data has been loaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsOpen
        {
            get => (_header != null && _directory != null);
        }
        /// <summary>
        /// Gets a value indicating whether the current archive is an encrypted archive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the current archive is an encrypted; otherwise, <c>false</c>.
        /// </value>
        public bool IsSecure
        {
            get => _header != null && _header.IsSecureFile;
        }
        /// <summary>
        /// Gets the path of the file.
        /// </summary>
        /// <value>
        /// A string containing the file path.
        /// </value>
        public string? Path
        {
            get
            {
                if (_fileName == null)
                    return null;
                else
                    return System.IO.Path.GetDirectoryName(_fileName);
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates the new archive, and includes the compressed version of the specified files.
        /// </summary>
        /// <param name="pathAndFileName">
        /// A string containing the fully-qualified path and name of the file to be created.
        /// </param>
        /// <param name="listOfFiles">
        /// An <see cref="IEnumerable{T}"/> of <see cref="string"/> containing the fully-qualified path and name of the
        /// files to be archived.
        /// </param>
        public async Task CreateArchiveAsync(string pathAndFileName, IEnumerable<string>? listOfFiles)
        {
            OnCreateArchiveStart();
            // Validate.
            if (!string.IsNullOrEmpty(pathAndFileName) && _header !=null &&
                _directory != null)
            {
                // Store the file name. 
                _fileName = pathAndFileName;

                // Create the writer manager based on file status, and open the file for writing.
                ITazContentWriter writer = CreateWriter(pathAndFileName);
                await writer.InitializeFileAsync().ConfigureAwait(false);
                if (writer.CanWrite)
                {
                    // Write the file header so we start in the right place.
                    writer.WriteHeader(_header);

                    if (listOfFiles != null && listOfFiles.Count() > 0)
                    {
                        // Compress each specified source file and write the compressed data to the archive file.
                        await CompressAndWriteFilesAsync(writer, listOfFiles).ConfigureAwait(false);
                    }

                    // Write the directory and close the file.
                    OnDirectoryWriteStart();
                    long dirPosition = writer.WriteDirectory(_directory);

                    // Re-write the header now that we have complete information.
                    _header.DirectoryStart = dirPosition;
                    writer.WriteHeader(_header);

                    await writer.CloseFileAsync().ConfigureAwait(false);
                    OnDirectoryWriteCompleted();
                }
                await writer.DisposeAsync().ConfigureAwait(false);
            }
            OnCreateArchiveCompleted();
        }
        /// <summary>
        /// Extracts the file from compressed storage.
        /// </summary>
        /// <param name="entry">
        /// The <see cref="TazDirectoryEntry"/> describing the compressed file.
        /// </param>
        /// <param name="reader">
        /// The <see cref="ITazContentReader"/> implementation used to read the compressed content.
        /// </param>
        /// <param name="outputDirectory">
        /// A string containing the output directory path name.
        /// </param>
        public async Task ExtractFileAsync(TazDirectoryEntry entry, ITazContentReader reader, string outputDirectory)
        {
            await Task.Yield();

            if (reader.CanRead && entry.OrigName != null)
            {
                byte[]? compressedData = reader.ReadArray(entry.Position);
                if (compressedData != null)
                {
                    DecompressToFile(compressedData, outputDirectory, entry.OrigName);
                    ByteArrayUtil.Clear(compressedData);
                }
            }
        }
        /// <summary>
        /// Extracts the files from the archive to the specified location.
        /// </summary>
        /// <param name="pathAndFileName">
        /// A string containing the fully-qualified path and name of the archive file.
        /// </param>
        /// <param name="outputDirectory">
        /// A string specifying the path of the output directory in which to write the files.
        /// </param>
        public async Task ExtractFilesAsync(string pathAndFileName, string outputDirectory)
        {
            await Task.Yield();

            // Validate.
            if (!string.IsNullOrEmpty(pathAndFileName) && File.Exists(pathAndFileName))
            {
                // Store the file name. 
                _fileName = pathAndFileName;

                // Create the writer manager based on file status, and open the file for writing.
                ITazContentReader reader = CreateReader(pathAndFileName);
                await reader.InitializeFileAsync().ConfigureAwait(false);
                if (reader.CanRead)
                {
                    // Read the header data.
                    _header?.Dispose();
                    _header = reader.ReadHeader();

                    // Ensure we are reading the correct file format.
                    if (_header != null && _header.IsTazFile)
                    {
                        // Read the directory data.
                        _directory?.Dispose();
                        _directory = reader.ReadDirectory(_header.DirectoryStart);

                        // Extract the files.
                        long mem = GC.GetTotalMemory(false);

                        if (_directory != null && _directory.Entries != null)
                        {
                            foreach (TazDirectoryEntry entry in _directory.Entries)
                            {
                                await ExtractFileAsync(entry, reader, outputDirectory).ConfigureAwait(false);
                                long currentMem = GC.GetTotalMemory(false);
                                if (currentMem - mem > 4096000)
                                {
                                    GC.WaitForPendingFinalizers();
                                    GC.Collect();
                                    GC.WaitForFullGCApproach();
                                    mem = GC.GetTotalMemory(true);
                                }
                            }
                        }
                    }
                }
                await reader.CloseFileAsync().ConfigureAwait(false);
                reader.Dispose();
            }
        }
        /// <summary>
        /// Loads the file directory content into memory.
        /// </summary>
        /// <param name="pathAndFileName">
        /// A string containing the fully-qualified path and name of the file.
        /// </param>
        public async Task LoadDirectoryContentAsync(string pathAndFileName)
        {
            // Validate.
            if (!string.IsNullOrEmpty(pathAndFileName) && File.Exists(pathAndFileName))
            {
                // Store the file name. 
                _fileName = pathAndFileName;

                // Create the writer manager based on file status, and open the file for writing.
                ITazContentReader reader = CreateReader(pathAndFileName);
                await reader.InitializeFileAsync().ConfigureAwait(false);
                if (reader.CanRead)
                {
                    // Read the header data.
                    _header?.Dispose();
                    _header = reader.ReadHeader();

                    // Ensure we are reading the correct file format.
                    if (_header != null && _header.IsTazFile)
                    {
                        // Read the directory data.
                        _directory?.Dispose();
                        _directory = reader.ReadDirectory(_header.DirectoryStart);
                    }
                }
                await reader.CloseFileAsync().ConfigureAwait(false);
                reader.Dispose();
            }
        }
        #endregion

        #region Private Event Methods		
        /// <summary>
        /// Raises the <see cref="CreateArchiveStart"/> event.
        /// </summary>
        private void OnCreateArchiveStart()
        {
            CreateArchiveStart?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Raises the <see cref="DirectoryWriteStart"/> event.
        /// </summary>
        private void OnDirectoryWriteStart()
        {
            DirectoryWriteStart?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Raises the <see cref="DirectoryWriteCompleted"/> event.
        /// </summary>
        private void OnDirectoryWriteCompleted()
        {
            DirectoryWriteCompleted?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Raises the <see cref="CreateArchiveCompleted"/> event.
        /// </summary>
        private void OnCreateArchiveCompleted()
        {
            CreateArchiveComplete?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Raises the <see cref="CreateArchiveCompleted"/> event.
        /// </summary>
        private void OnCompressionStart()
        {
            CompressionStart?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Raises the <see cref="FileCompressionStart"/> event.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the name of the file.
        /// </param>
        /// <param name="fileIndex">
        /// AN integer specifying the original index of the file.
        /// </param>
        /// <param name="length">
        /// A <see cref="long"/> specifying the size of the file.
        /// </param>
        private void OnFileCompressionStart(string fileName, int fileIndex, int length)
        {
            FileEventArgs evArgs = new FileEventArgs
            {
                FileName = fileName,
                FileIndex = fileIndex,
                FileSize = length
            };
            FileCompressionStart?.Invoke(this, evArgs);
        }
        /// <summary>
        /// Raises the <see cref="FileCompressionComplete"/> event.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the name of the file.
        /// </param>
        /// <param name="fileIndex">
        /// AN integer specifying the original index of the file.
        /// </param>
        /// <param name="fileSize">
        /// A <see cref="long"/> specifying the size of the file.
        /// </param>
        /// <param name="compressedSize">
        /// A <see cref="long"/> specifying the size of the compressed data.
        /// </param>
        private void OnFileCompressionComplete(string fileName, int fileIndex, long fileSize, long compressedSize)
        {
            FileEventArgs evArgs = new FileEventArgs
            {
                FileName = fileName,
                FileIndex = fileIndex,
                FileSize = fileSize,
                CompressedSize = compressedSize
            };
            FileCompressionComplete?.Invoke(this, evArgs);
        }
        /// <summary>
        /// Raises the <see cref="CompressionComplete"/> event.
        /// </summary>
        private void OnCompressionCompleted()
        {
            CompressionComplete?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Public Static Methods / Functions 		
        /// <summary>
        /// Determines whether the specified file is a TAZ file.
        /// </summary>
        /// <param name="pathAndFileName">
        /// A string containing the fully-qualified path and name of the file.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified file is a TAZ file; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsTazFile(string? pathAndFileName)
        {
            bool isTazFile = false;

            if (string.IsNullOrEmpty(pathAndFileName))
                return false;
            else if (!SafeIO.FileExists(pathAndFileName))
                return false;
            else
            {
                TazFileHeader ?header = TazFileHeader.ReadFromClosedFile(pathAndFileName);
                isTazFile = header != null &&  header.IsTazFile;
                header?.Dispose();
            }
            return isTazFile;
        }
        /// <summary>
        /// Determines whether the specified file is an encrypted TAZ file.
        /// </summary>
        /// <param name="pathAndFileName">
        /// A string containing the fully-qualified path and name of the file.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified file is an encrypted TAZ file; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsTazSecureFile(string? pathAndFileName)
        {
            bool isSecureTazFile = false;

            if (string.IsNullOrEmpty(pathAndFileName))
                return false;
            else if (!SafeIO.FileExists(pathAndFileName))
                return false;
            else
            {
                TazFileHeader? header = TazFileHeader.ReadFromClosedFile(pathAndFileName);
                isSecureTazFile = header != null &&  header.IsTazFile && header.IsSecureFile;
                header?.Dispose();
            }
            return isSecureTazFile;
        }
        #endregion

        #region Private Methods / Functions		
        /// <summary>
        /// For each provided file, compresses the file and writes the compressed data to the archive file.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="ITazContentWriter"/> writer instance used to write the content.
        /// </param>
        /// <param name="listOfFiles">
        /// An <see cref="IEnumerable{T}"/> of <see cref="string"/> containing the list of files to include
        /// in the archive.
        /// </param>
        private async Task CompressAndWriteFilesAsync(ITazContentWriter writer, IEnumerable<string> listOfFiles)
        {
            OnCompressionStart();

            List<string> fileList = listOfFiles.ToList();
            int length = fileList.Count;
            for (int index = 0; index < length; index++)
            {
                string fileName = fileList[index];
                OnFileCompressionStart(fileName, index, length);

                long compressedSize = await PerformFileCompressionOperationAsync(fileName, writer).ConfigureAwait(false);

                OnFileCompressionComplete(fileName, index, length, compressedSize);

            }

            OnCompressionCompleted();
        }
        /// <summary>
        /// Creates the writer instance to use to write the file contents.
        /// </summary>
        /// <param name="pathAndFileName">
        /// A string containing the fully-qualified path and name of the file.
        /// </param>
        /// <returns>
        /// The <see cref="ITazContentWriter"/> instance to use.
        /// </returns>
        private ITazContentWriter CreateWriter(string pathAndFileName)
        {
            ITazContentWriter writer;

            if (_keyManager == null)
            {
                // Expecting a clear archive file.
                writer = new TazWriter(pathAndFileName);
            }
            else
            {
                // Expecting an encrypted archive file.
                writer = new SecureTazWriter(pathAndFileName, _keyManager);
            }
            return writer;
        }
        /// <summary>
        /// Creates the writer instance to use to read the file contents.
        /// </summary>
        /// <param name="pathAndFileName">
        /// A string containing the fully-qualified path and name of the file.
        /// </param>
        /// <returns>
        /// The <see cref="ITazContentReader"/> instance to use.
        /// </returns>
        private ITazContentReader CreateReader(string pathAndFileName)
        {
            ITazContentReader reader;

            if (_keyManager == null)
            {
                // Expecting a clear archive file.
                reader = new TazReader(pathAndFileName);
            }
            else
            {
                // Expecting an encrypted archive file.
                reader = new SecureTazReader(pathAndFileName, _keyManager);
            }
            return reader;
        }
        /// <summary>
        /// Performs the file compression operation asynchronous.
        /// </summary>
        /// <param name="fileToCompress">
        /// The path and name of the file to be compressed.
        /// </param>
        /// <param name="writer">
        /// The <see cref="ITazContentWriter"/> instance used to write the data content.
        /// </param>
        private async Task<long> PerformFileCompressionOperationAsync(string fileToCompress, ITazContentWriter writer)
        {
            long compressedSize = -1;

            FileCompressionOperation operation = new FileCompressionOperation(fileToCompress);
            if (operation.IsValid)
            {
                // Try to gather the file meta data, and then hash the original file data.
                await operation.LoadFileDataAsync().ConfigureAwait(false);
                operation.CreateHash(writer);

                // Compress the data.
                operation.CompressFile();
                if (operation.CompressedData != null)
                {
                    // Create and store the directory entry.
                    _directory?.AddNewFile(operation, writer.CurrentPosition);

                    // Write the compressed data to the underlying storage stream.
                    writer.WriteArray(operation.CompressedData);
                    compressedSize = operation.CompressedData.Length;
                }

                // Clear.
                operation.Dispose();
            }

            // Ensure everything is written and return the new position.
            writer.Flush();
            return compressedSize;
        }
        /// <summary>
        /// Decompresses the data content and writes it to the new file.
        /// </summary>
        /// <param name="compressedData">The compressed data.</param>
        /// <param name="outputDirectory">The output directory.</param>
        /// <param name="newFileName">New name of the file.</param>
        private void DecompressToFile(byte[] compressedData, string outputDirectory, string newFileName)
        {
            FileStream? outputStream = null;
            string actualName;
            if (outputDirectory.EndsWith("\\"))
                actualName = outputDirectory + newFileName;
            else
                actualName = outputDirectory + "\\" + newFileName;
            try
            {
                if (File.Exists(actualName))
                    File.Delete(actualName);
                outputStream = new FileStream(actualName, FileMode.CreateNew, FileAccess.Write);
            }
            catch (Exception ex)
            {
                Exceptions?.Add(ex);
            }

            if (outputStream != null)
            {
                AdaptiveCompression.Decompress(compressedData, outputStream);
                outputStream.Dispose();
            }
        }
        #endregion

    }
}