using Adaptive.Intelligence.Shared.Properties;
using System.Security.Cryptography;

namespace Adaptive.Intelligence.Shared.Security.IO
{
    /// <summary>
    /// Provides a secure writer implementation for writing encrypted data streams.
    /// </summary>
    /// <seealso cref="ExceptionTrackingBase" />
    /// <seealso cref="ICryptoWriter" />
    public sealed class SecureWriter : ExceptionTrackingBase, ICryptoWriter
    {
        #region Private Member Declarations		
        /// <summary>
        /// The destination stream to write to.
        /// </summary>
        private Stream? _destinationStream;
        /// <summary>
        /// The writer instance.
        /// </summary>
        private BinaryWriter? _writer;
        /// <summary>
        /// The key table.
        /// </summary>
        private AesKeyTable? _keyTable;
        /// <summary>
        /// The SHA-512 hash provider.
        /// </summary>
        private SHA512? _hasher;
        /// <summary>
        /// The AES symmetric cryptographic provider.
        /// </summary>
        private AesProvider? _provider;
        /// <summary>
        /// The data block size
        /// </summary>
        private const int BlockSize = 65536;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="SecureWriter"/> class.
        /// </summary>
        /// <param name="destinationStream">
        /// The destination <see cref="Stream"/> to write to.
        /// </param>
        public SecureWriter(Stream destinationStream)
        {
            _destinationStream = destinationStream;
            _hasher = SHA512.Create();
            _provider = new AesProvider();
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
                _provider?.Dispose();
                _hasher?.Dispose();
                _keyTable?.Dispose();
            }

            _provider = null;
            _writer = null;
            _destinationStream = null;
            _keyTable = null;
            _hasher = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods / Functions.		
        /// <summary>
        /// Initializes the key data being used by the writer.
        /// </summary>
        /// <param name="keyTable">The reference to the <see cref="AesKeyTable" /> implementation.
        /// </param>
        public void InitializeKeys(IKeyTable keyTable)
        {
            _keyTable = (AesKeyTable)keyTable;
        }
        /// <summary>
        /// Encrypts and writes the stream content.
        /// </summary>
        /// <param name="sourceStream">The source <see cref="Stream" /> from which the data is to be read and then encrypted and written.</param>
        /// <exception cref="System.InvalidOperationException">
        /// Keys have not been initialized.
        /// or
        /// Specified source stream cannot be read from.
        /// </exception>
        public void WriteStream(Stream sourceStream)
        {
            if (_keyTable == null)
                throw new InvalidOperationException(Resources.ErrorKeysNotInitialized);
            if (!sourceStream.CanRead)
                throw new InvalidOperationException(Resources.ErrorStreamRead);
            if (_destinationStream == null)
                throw new InvalidOperationException(Resources.ErrorStreamWrite);

            BinaryReader? reader = CreateReader(sourceStream);
            if (reader != null && CreateWriter())
            {
                WriteStreamContent(reader);
                reader.Dispose();
            }
        }
        #endregion

        #region Private Methods / Functions		
        /// <summary>
        /// Creates the reader instance.
        /// </summary>
        /// <param name="sourceStream">
        /// The reference to the source <see cref="Stream"/> to read the data content from.
        /// </param>
        /// <returns>
        /// The <see cref="BinaryReader"/> instance to use, or <b>null</b> if the operation fails.
        /// </returns>
        private BinaryReader? CreateReader(Stream sourceStream)
        {
            BinaryReader? reader = null;
            try
            {
                sourceStream.Seek(0, SeekOrigin.Begin);
                reader = new BinaryReader(sourceStream);
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
                reader = null;
            }
            return reader;
        }
        /// <summary>
        /// Creates the writer instance.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        private bool CreateWriter()
        {
            if (_destinationStream != null)
            {
                try
                {
                    _writer = new BinaryWriter(_destinationStream);
                }
                catch (Exception ex)
                {
                    Exceptions.Add(ex);
                    _writer = null;
                }
            }
            return (_writer != null);
        }
        /// <summary>
        /// Encrypts and writes the content of the source stream.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="BinaryReader"/> instance used to write the data content.
        /// </param>
        private void WriteStreamContent(BinaryReader reader)
        {
            // Determine the number of full 64K blocks to be read.
            int sourceLength = (int)reader.BaseStream.Length;
            int blockCount = (sourceLength / BlockSize);

            // Determine the size of the last block of data.
            int lastBlockSize = sourceLength - (blockCount * BlockSize);

            // Total blocks.
            blockCount++;

            // Encrypt and then write the actual counts/size values.
            byte[] encryptedCount = EncryptInt(blockCount, _keyTable!.Senary);
            byte[] encryptedLastBlockSize = EncryptInt(lastBlockSize, _keyTable.Senary);
            _writer!.Write(encryptedCount.Length);
            _writer!.Write(encryptedCount);
            _writer!.Write(encryptedLastBlockSize.Length);
            _writer!.Write(encryptedLastBlockSize);
            _writer!.Flush();

            int position = 0;
            _keyTable.Reset();
            do
            {
                byte[] sizeKey = _keyTable.Next();
                // Read the next block of data until the last one is reached.  Calculate an SHA-512 hash for 
                // verification when read.
                byte[] data = reader.ReadBytes(BlockSize);
                byte[] hash = _hasher!.ComputeHash(data);

                // Encrypt the data and the hash.
                byte[]? encryptedData = EncryptBlock(data, hash);
                if (encryptedData == null)
                    break;

                // Encrypt the size of the encrypted data.  Write the size of the encrypted size, and then
                // the encrypted actual size value.
                byte[] encryptedSize = EncryptInt(encryptedData.Length, sizeKey);
                _writer!.Write(encryptedSize.Length);
                _writer!.Write(encryptedSize);
                CryptoUtil.SecureClear(encryptedSize);

                // Now write the actual data.
                _writer!.Write(encryptedData);
                _writer!.Flush();

                // Clear memory.
                CryptoUtil.SecureClear(encryptedData);
                CryptoUtil.SecureClear(data);
                CryptoUtil.SecureClear(hash);
                position++;
            } while (position < blockCount);
        }
        /// <summary>
        /// Encrypts the integer value.
        /// </summary>
        /// <param name="value">
        /// The value to be encrypted.
        /// </param>
        /// <param name="keyData">
        /// A byte array containing the key data.
        /// </param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// Occurs when the encryption operation fails.
        /// </exception>
        private byte[] EncryptInt(int value, byte[] keyData)
        {
            // Set the key and initialization vector for the AES data.
            _provider!.SetKeyIV(keyData);

            // Translate the integer to a 4-byte array.
            byte[] intBits = BitConverter.GetBytes(value);

            // Encrypt the integer value.
            byte[]? encryptedInt = _provider!.Encrypt(intBits);
            if (encryptedInt == null)
                throw new InvalidOperationException(Resources.ErrorCantEncrypt);

            // Clear memory and return the encrypted data.
            CryptoUtil.SecureClear(intBits);
            return encryptedInt;
        }
        /// <summary>
        /// Encrypts the data block.
        /// </summary>
        /// <param name="clearContent">
        /// A byte array containing the clear data content.
        /// </param>
        /// <param name="hashValue">
        /// A byte array containing the SHA-512 hash of the clear data content.
        /// </param>
        /// <returns>
        /// A byte array containing the encrypted data.
        /// </returns>
        private byte[]? EncryptBlock(byte[] clearContent, byte[] hashValue)
        {
            // Set the key and initialization vector to the next set of keys in the rotation.
            _provider!.SetKeyIV(_keyTable!.Next());

            // Combine the clear data and the hash value,
            // and encrypt both values.  Then, splice the bits of the final array as a defense
            // against pattern-based cryptanalysis.
            byte[] combined = ByteArrayUtil.ConcatenateArrays(clearContent, hashValue);
            byte[]? encrypted = _provider!.Encrypt(combined);
            byte[]? obfuscated = BitSplicer.SpliceBits(encrypted);

            // Clear memory.
            CryptoUtil.SecureClear(combined);
            CryptoUtil.SecureClear(encrypted);

            return obfuscated;
        }
        #endregion
    }
}
