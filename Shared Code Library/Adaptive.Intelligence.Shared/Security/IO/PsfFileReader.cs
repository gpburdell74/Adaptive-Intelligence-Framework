using Adaptive.Intelligence.Shared.Properties;

namespace Adaptive.Intelligence.Shared.Security.IO
{
    /// <summary>
    /// Provides the class for reading content from an open PSF File.
    /// </summary>
    /// <seealso cref="ExceptionTrackingBase" />
    internal sealed class PsfFileReader : ExceptionTrackingBase
    {
        #region Private Member Declarations
        /// <summary>
        /// The AES key table that stores the 6 key and IV pairs used in cryptographic operations.
        /// </summary>
        private AesKeyTable? _keyTable;
        /// <summary>
        /// The top-level cryptographic provider.
        /// </summary>
        private SuperCrypt? _topCrypt;
        /// <summary>
        /// The binary data reader instance.
        /// </summary>
        private BinaryReader? _reader;
        /// <summary>
        /// The source stream to read from.
        /// </summary>
        private Stream? _sourceStream;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="PsfFileReader"/> class.
        /// </summary>
        /// <param name="sourceStream">
        /// The source <see cref="Stream"/> instance to read content from.
        /// </param>
        public PsfFileReader(Stream sourceStream)
        {
            _sourceStream = sourceStream;
            _reader = new BinaryReader(_sourceStream);
            sourceStream.Seek(0, SeekOrigin.Begin);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            Close();
            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Closes the reader and the underlying stream.
        /// </summary>
        public void Close()
        {
            _keyTable?.Dispose();
            _topCrypt?.Dispose();
            _reader?.Dispose();
            _sourceStream?.Dispose();

            _keyTable = null;
            _topCrypt = null;
            _reader = null;
            _sourceStream = null;
        }
        /// <summary>
        /// Initializes the reader and the cryptographic providers for reading from the source (PSF) stream.
        /// </summary>
        /// <param name="userId">
        /// A string containing the user identifier value or other such passkey value to use.
        /// </param>
        /// <param name="passCode">
        /// A string containing the password or other pass code value to use.
        /// </param>
        /// <param name="pinValue">
        /// An integer containing the PIN value to use.
        /// </param>
        /// <returns>
        /// <b>true</b> if the file is successfully opened for reading; otherwise, returns <b>false</b>.
        /// </returns>
        public bool Open(string userId, string passCode, int pinValue)
        {
            // 1. Create the top-level cryptographic instance.
            _topCrypt = new SuperCrypt(userId, passCode, pinValue);

            // 2. Read the encrypted key data, 
            //	  decrypt it, and create the key table with the actual
            //	  encryption keys to use.
            _reader = new BinaryReader(_sourceStream!);
            _keyTable = ReadAndCreateKeyTable();

            if (_keyTable == null)
            {
                Close();
            }

            return (_reader != null);
        }
        /// <summary>
        /// Reads the contents of the PSF encrypted file to a new memory stream.
        /// </summary>
        /// <returns>
        /// A <see cref="MemoryStream"/> containing the decrypted content if successful; otherwise,
        /// returns <b>null</b>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Occurs if <see cref="Open"/> was not called, or the call failed.
        /// </exception>
        public MemoryStream? Read()
        {
            MemoryStream? destinationStream = null;

            if (_keyTable != null && _reader != null)
            {
                // 1. Read and decrypt the original data stream.
                MemoryStream? contentStream = DecryptWrapperStream();
                if (contentStream != null)
                {
                    // 2. Create the memory storage for the decrypted contents.
                    destinationStream = new MemoryStream((int)contentStream.Length);

                    // 3. Initialize the secure reader instance.
                    SecureReader secReader = new SecureReader(contentStream);
                    secReader.InitializeKeys(_keyTable);

                    // 4. Decrypt the original content and store the clear data in the return stream.
                    secReader.ReadStream(destinationStream);

                    // Clear memory.
                    secReader.Dispose();
                    contentStream.Dispose();
                }
            }
            else
            {
                throw new InvalidOperationException(Resources.ErrorNotOpenForRead);
            }

            return destinationStream;
        }
        #endregion

        #region Private Methods / Functions		
        /// <summary>
        /// Decrypts the wrapper stream content.
        /// </summary>
        /// <returns>
        /// A <see cref="MemoryStream"/> containing the decrypted content if successful; otherwise,
        /// returns <b>null</b>.
        /// </returns>
        private MemoryStream? DecryptWrapperStream()
        {
            MemoryStream? contentStream = null;

            if (_reader != null && _topCrypt != null)
            {
                // Read the full encrypted byte array.
                int length = _reader.ReadInt32();
                byte[] encryptedContentStream = _reader.ReadBytes(length);

                // Decrypt the content and return it to a memory stream for further use.
                byte[]? wrappedContent = _topCrypt.Decrypt(encryptedContentStream);
                if (wrappedContent != null)
                {
                    contentStream = new MemoryStream(wrappedContent);
                }

                // Clear memory.
                CryptoUtil.SecureClear(encryptedContentStream);

            }

            return contentStream;
        }
        /// <summary>
        /// Reads the byte array from the stream that is prefixed with an Int32 length indicator.
        /// </summary>
        /// <remarks>
        /// A zero-length value indicates a null array.
        /// </remarks>
        /// <param name="reader">
        /// The <see cref="BinaryReader"/> instance used to read the data content.
        /// </param>
        /// <returns>
        /// The byte array read from the source, or <b>null</b>.
        /// </returns>
        private static byte[]? ReadMarkedByteArray(BinaryReader reader)
        {
            byte[]? readData = null;

            int length = reader.ReadInt32();
            if (length > 0)
            {
                readData = reader.ReadBytes(length);
            }

            return readData;

        }
        /// <summary>
        /// Reads and decrypts the key data stored in the PSF file and creates the key table.
        /// </summary>
        /// <returns>
        /// The <see cref="AesKeyTable"/> to use when reading data, or <b>null</b> if the operation
        /// fails.
        /// </returns>
        private AesKeyTable? ReadAndCreateKeyTable()
        {
            AesKeyTable? keyTable = null;

            // Read and decrypt the six encryption keys in the file.
            byte[]? primary = ReadAndDecryptKey();
            byte[]? secondary = ReadAndDecryptKey();
            byte[]? tertiary = ReadAndDecryptKey();
            byte[]? quarternary = ReadAndDecryptKey();
            byte[]? quinary = ReadAndDecryptKey();
            byte[]? senary = ReadAndDecryptKey();

            if (primary != null && secondary != null && tertiary != null && quarternary != null && quinary != null && senary != null)
            {
                keyTable = new AesKeyTable(primary, secondary, tertiary, quarternary, quinary, senary);
            }

            CryptoUtil.SecureClear(primary);
            CryptoUtil.SecureClear(secondary);
            CryptoUtil.SecureClear(tertiary);
            CryptoUtil.SecureClear(quarternary);
            CryptoUtil.SecureClear(quinary);
            CryptoUtil.SecureClear(senary);

            return keyTable;
        }
        /// <summary>
        /// Attempts to read and decrypt a cryptographic key from the source stream.
        /// </summary>
        /// <returns>
        /// A byte array containing the key data, or <b>null</b> if the operation fails.
        /// </returns>
        private byte[]? ReadAndDecryptKey()
        {
            byte[]? clearContent = null;

            if (_reader != null && _topCrypt != null)
            {
                byte[]? data = ReadMarkedByteArray(_reader);
                if (data != null)
                {
                    clearContent = _topCrypt.Decrypt(data);
                    CryptoUtil.SecureClear(data);
                }
            }
            return clearContent;
        }
        #endregion
    }
}
