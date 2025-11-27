namespace Adaptive.Intelligence.Shared.Security
{
    /// <summary>
    /// Provides a simple storage mechanism for storing AES Key and Initialization Vector
    /// information.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class AesKeyStore : DisposableObjectBase, ICloneable
    {
        #region Private Member Declarations
        /// <summary>
        /// The 32-byte key data.
        /// </summary>
        private SecureByteArray? _key;
        /// <summary>
        /// The 16-byte initialization vector.
        /// </summary>
        private SecureByteArray? _iv;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="AesKeyStore"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public AesKeyStore()
        {
            _key = new SecureByteArray();
            _iv = new SecureByteArray();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AesKeyStore"/> class.
        /// </summary>
        /// <param name="key">
        /// A 32-element byte array containing the AES key data.
        /// </param>
        /// <param name="iv">
        /// A 16-element byte array containing the AES initialization data.
        /// </param>
        public AesKeyStore(byte[] key, byte[] iv)
        {
            _key = new SecureByteArray(key);
            _iv = new SecureByteArray(iv);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AesKeyStore"/> class.
        /// </summary>
        /// <param name="keyData">
        /// A 48-element byte array containing the AES key data followed by the AES
        /// initialization vector data.
        /// </param>
        public AesKeyStore(byte[] keyData)
        {
            // Assuming a 48-byte (or longer) encryption key byte array where the first 32 bytes are
            // the AES encryption key, and the next 16 bytes are the AES initialization vector (IV).
            if (keyData.Length >= 48)
            {
                SetKeyIVData(keyData);
            }
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            ClearKeyMemory();
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the AES initialization vector data.
        /// </summary>
        /// <value>
        /// A 16-element byte array containing the AES initialization vector data.
        /// </value>
        public byte[]? IV
        {
            get
            {
                if (_iv == null || _iv.IsNull)
                {
                    return null;
                }
                else
                {
                    return _iv.Value;
                }
            }
            set
            {
                if (value == null || value.Length != 16)
                {
                    _iv?.Dispose();
                    _iv = null;
                }
                else
                {
                    if (_iv == null)
                    {
                        _iv = new SecureByteArray(value);
                    }
                    else
                    {
                        _iv.Value = value;
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets the AES key data.
        /// </summary>
        /// <value>
        /// A 32-element byte array containing the AES key data.
        /// </value>
        public byte[]? Key
        {
            get
            {
                if (_key == null || _key.IsNull)
                {
                    return null;
                }
                else
                {
                    return _key.Value;
                }
            }
            set
            {
                if (value == null || value.Length != 32)
                {
                    _key?.Dispose();
                    _key = null;
                }
                else
                {
                    if (_key == null)
                    {
                        _key = new SecureByteArray(value);
                    }
                    else
                    {
                        _key.Value = value;
                    }
                }
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Clears the byte arrays, erasing the key data memory content.
        /// </summary>
        public void ClearKeyMemory()
        {
            _key?.Dispose();
            _iv?.Dispose();
            _key = null;
            _iv = null;
        }
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// An <see cref="AesKeyStore"/> instance that is a copy of this instance.
        /// </returns>
        public AesKeyStore Clone()
        {
            if (_iv == null || _key == null || _key.Value == null || _iv.Value == null)
            {
                return new AesKeyStore();
            }
            else
            {
                return new AesKeyStore(_key.Value, _iv.Value);
            }
        }
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// An <see cref="AesKeyStore"/> instance that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
        /// <summary>
        /// Gets the AES key and IV data as a concatenated byte array.
        /// </summary>
        /// <returns>
        /// A 48-element byte array where
        /// the first 32 elements contain the key data and the last 16 elements
        /// contain the IV data.
        /// </returns>
        public byte[]? GetKeyIVData()
        {
            if (_key == null || _key.IsNull || _key.Value == null || _iv == null || _iv.IsNull || _iv.Value == null)
            {
                return null;
            }
            else
            {
                byte[] data = new byte[48];
                Array.Copy(_key.Value, 0, data, 0, 32);
                Array.Copy(_iv.Value, 0, data, 32, 16);
                return data;
            }
        }
        /// <summary>
        /// Gets the AES key and IV data as a base-64 encoded string.
        /// </summary>
        /// <returns>
        /// A base-64 encoded string representing a 48-element byte array where
        /// the first 32 elements contain the key data and the last 16 elements
        /// contain the IV data.
        /// </returns>
        public string? GetKeyIVDataAsBase64String()
        {
            string? content = null;

            if (_key != null && _iv != null)
            {
                byte[]? data = GetKeyIVData();
                if (data != null)
                {
                    content = Convert.ToBase64String(data);
                }
            }
            return content;
        }
        /// <summary>
        /// Sets the AES Key and IV data from the byte array.
        /// </summary>
        /// <param name="keyData">
        /// A 48-element byte array where
        /// the first 32 elements contain the key data and the last 16 elements
        /// contain the IV data.
        /// </param>
        public void SetKeyIVData(byte[]? keyData)
        {
            // Assuming a 48-byte (or longer) encryption key byte array where the first 32 bytes are
            // the AES encryption key, and the next 16 bytes are the AES initialization vector (IV).
            if (keyData != null && keyData.Length >= 48)
            {
                // Read the key data from the original array.
                byte[]? key = new byte[32];
                Array.Copy(keyData, 0, key, 0, 32);
                _key = new SecureByteArray(key);
                Array.Clear(key, 0, 32);
                key = null;

                // Read the IV data from the original array.
                byte[]? iv = new byte[16];
                Array.Copy(keyData, 32, iv, 0, 16);
                _iv = new SecureByteArray(iv);
                Array.Clear(iv, 0, 16);
                iv = null;
            }

        }
        /// <summary>
        /// Sets the AES key and IV data from the specified string.
        /// </summary>
        /// <param name="keyData">
        /// A base-64 encoded string representing a 48-element byte array where
        /// the first 32 elements contain the key data and the last 16 elements
        /// contain the IV data.
        /// </param>
        public void SetKeyIVDataFromBase64String(string? keyData)
        {
            if (keyData != null && keyData.Length == 64)
            {
                byte[] content = Convert.FromBase64String(keyData);
                SetKeyIVData(content);
                Array.Clear(content, 0, 48);
            }
        }
        #endregion
    }
}
