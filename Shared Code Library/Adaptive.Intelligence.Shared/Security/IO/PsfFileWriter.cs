using Adaptive.Intelligence.Shared.Properties;

namespace Adaptive.Intelligence.Shared.Security.IO
{
    /// <summary>
    /// Provides the class for writing content to an open PSF File.
    /// </summary>
    /// <seealso cref="ExceptionTrackingBase" />
    internal sealed class PsfFileWriter : ExceptionTrackingBase
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
        /// The binary data writer instance.
        /// </summary>
        private BinaryWriter? _writer;
        /// <summary>
        /// The destination stream to write the output to.
        /// </summary>
        private Stream? _destinationStream;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="PsfFileWriter"/> class.
        /// </summary>
        /// <param name="destinationStream">
        /// The destination <see cref="Stream"/> to write the output to.
        /// </param>
        public PsfFileWriter(Stream destinationStream)
        {
            _destinationStream = destinationStream;
            _writer = new BinaryWriter(_destinationStream);
            destinationStream.Seek(0, SeekOrigin.Begin);
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
                _keyTable?.Dispose();
                _topCrypt?.Dispose();
                _writer?.Dispose();
            }

            _keyTable = null;
            _topCrypt = null;
            _writer = null;
            _destinationStream = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Initializes the writer instance to begin writing data to the output stream.
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
        /// <b>true</b> if the file is successfully opened for writing; otherwise, returns <b>false</b>.
        /// </returns>
        public bool Open(string userId, string passCode, int pinValue)
        {
            if (_writer != null)
            {
                // 1. Each time the file is written, create 6 new cryptographic keys for the internal operations
                _keyTable = AesKeyTable.CreateRandom();

                // 2. Create the top-level cryptographic instance and encrypt the new keys.
                _topCrypt = new SuperCrypt(userId, passCode, pinValue);
                byte[]? aprime = _topCrypt.Encrypt(_keyTable.Primary);
                byte[]? bprime = _topCrypt.Encrypt(_keyTable.Secondary);
                byte[]? cprime = _topCrypt.Encrypt(_keyTable.Tertiary);
                byte[]? dprime = _topCrypt.Encrypt(_keyTable.Quaternary);
                byte[]? eprime = _topCrypt.Encrypt(_keyTable.Quinary!);
                byte[]? fprime = _topCrypt.Encrypt(_keyTable.Senary!);

                // 3. Open the new file and write the encrypted keys.
                WriteArray(aprime);
                WriteArray(bprime);
                WriteArray(cprime);
                WriteArray(dprime);
                WriteArray(eprime);
                WriteArray(fprime);
            }
            return (_writer != null);
        }
        /// <summary>
        /// Writes the content from the source stream to the output stream.
        /// </summary>
        /// <param name="sourceStream">
        /// The source <see cref="Stream"/> containing the original content to be encrypted and 
        /// written to the output stream.
        /// </param>
        public void Write(Stream sourceStream)
        {
            if (_keyTable != null && _topCrypt != null && _writer != null)
            {
                // 1. Prepare the Secure writer.
                MemoryStream innerStream = new MemoryStream();
                SecureWriter secWriter = new SecureWriter(innerStream);
                secWriter.InitializeKeys(_keyTable);

                // 2. Write the data to the inner stream.
                secWriter.WriteStream(sourceStream);

                // 3. Write the inner stream content to the file.
                innerStream.Seek(0, SeekOrigin.Begin);
                byte[]? topLevel = _topCrypt.Encrypt(innerStream.ToArray());
                WriteArray(topLevel);

                // Clear memory.
                secWriter.Dispose();
                _writer.Flush();
                CryptoUtil.SecureClear(topLevel);
                innerStream.Dispose();
            }
            else
            {
                throw new InvalidOperationException(Resources.ErrorNotOpenForWrite);
            }
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Writes the byte array to the output stream with an Int32 length indicator.
        /// </summary>
        /// <param name="dataArray">
        /// A byte array containing the data to write, or <b>null</b>.
        /// </param>
        private void WriteArray(byte[]? dataArray)
        {
            // Do nothing if the writer does not exist.
            if (_writer != null)
            {
                // Write zero if the array is null or empty,
                // otherwise write the length of the array.
                int length = 0;
                if (dataArray == null || dataArray.Length == 0)
                {
                    length = 0;
                }
                else
                {
                    length = dataArray.Length;
                }

                try
                {
                    _writer.Write(length);
                }
                catch (Exception ex)
                {
                    Exceptions.Add(ex);
                }

                // Write the array if not null or empty.
                if (dataArray != null && length > 0)
                {
                    try
                    {
                        _writer.Write(dataArray);
                        _writer.Flush();
                    }
                    catch (Exception ex)
                    {
                        Exceptions.Add(ex);
                    }
                }
            }
        }
        #endregion
    }
}
