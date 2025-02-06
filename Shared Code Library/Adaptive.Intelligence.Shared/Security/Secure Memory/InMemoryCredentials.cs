using Adaptive.Intelligence.Shared.Security;
using System.Text;

namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Represents and manages a set of user and password credentials in memory.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class InMemoryCredentials : DisposableObjectBase
    {
        #region Private Member Declarations
        /// <summary>
        /// The user identifier stored as a secure string.
        /// </summary>
        private SecureByteArray? _userId;
        /// <summary>
        /// The password value stored as a secure string.
        /// </summary>
        private SecureByteArray? _password;
        /// <summary>
        /// The primary key and IV values.
        /// </summary>
        private SecureByteArray? _primaryKey;
        /// <summary>
        /// The secondary key and IV values.
        /// </summary>
        private SecureByteArray? _secondaryKey;
        /// <summary>
        /// The tertiary key and IV values.
        /// </summary>
        private SecureByteArray? _tertiaryKey;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryCredentials"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public InMemoryCredentials()
        {
            GenerateKeyData();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryCredentials"/> class.
        /// </summary>
        /// <param name="userId">
        /// A string containing the user ID value.
        /// </param>
        /// <param name="password">
        /// A string containing the password value.
        /// </param>
        public InMemoryCredentials(string? userId, string? password)
        {
            GenerateKeyData();
            _userId = EncodeString(userId);
            _password = EncodeString(password);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _userId?.Dispose();
                _password?.Dispose();
                _primaryKey?.Dispose();
                _secondaryKey?.Dispose();
                _tertiaryKey?.Dispose();
            }

            _userId = null;
            _password = null;
            _primaryKey = null;
            _secondaryKey = null;
            _tertiaryKey = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the user ID value.
        /// </summary>
        /// <value>
        /// A string containing the user ID value.
        /// </value>
        public string? UserId
        {
            get => DecodeString(_userId);
            set => _userId = EncodeString(value);
        }
        /// <summary>
        /// Gets or sets the password value.
        /// </summary>
        /// <value>
        /// A string containing the password value.
        /// </value>
        public string? Password
        {
            get => DecodeString(_password);
            set => _password = EncodeString(value);
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Generates AES key data from the user's login and and password values.
        /// </summary>
        /// <returns>
        /// An <see cref="AesKeyStore"/> instance containing three sets of AES keys.
        /// vector.
        /// </returns>
        private void GenerateKeyData()
        {
            // This has to remain the same in order to generate the same values for the same password each time.
            byte[] saltValuePrimary = new byte[] { 228, 128, 2, 47, 90, 89, 212, 244 };
            byte[] saltValueSecondary = new byte[] { 128, 128, 95, 02, 90, 19, 212, 176 };
            byte[] saltValueTertiary = new byte[] { 32, 128, 22, 47, 90, 89, 212, 084 };

            byte[]? primaryKey = KeyGenerator.CreateKeyData(saltValuePrimary);
            byte[]? secondaryKey = KeyGenerator.CreateKeyData(saltValueSecondary);
            byte[]? tertiaryKey = KeyGenerator.CreateKeyData(saltValueTertiary);

            if (primaryKey != null)
            {
                _primaryKey = new SecureByteArray(primaryKey);
                Array.Clear(primaryKey, 0, primaryKey.Length);
            }

            if (secondaryKey != null)
            {
                _secondaryKey = new SecureByteArray(secondaryKey);
                Array.Clear(secondaryKey, 0, secondaryKey.Length);
            }

            if (tertiaryKey != null)
            {
                _tertiaryKey = new SecureByteArray(tertiaryKey);
                Array.Clear(tertiaryKey, 0, tertiaryKey.Length);
            }

            Array.Clear(saltValuePrimary, 0, saltValuePrimary.Length);
            Array.Clear(saltValueSecondary, 0, saltValueSecondary.Length);
            Array.Clear(saltValueTertiary, 0, saltValueTertiary.Length);

        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Encodes the string as a byte array.
        /// </summary>
        /// <param name="original">
        /// A string containing the original data.
        /// </param>
        /// <returns>
        /// A byte array representing the string, or <b>null.</b>
        /// </returns>
        private SecureByteArray? EncodeString(string? original)
        {
            SecureByteArray? result = null;

            if (!string.IsNullOrEmpty(original))
            {
                byte[]? data = Encoding.ASCII.GetBytes(original);
                if (data != null)
                {
                    AesProvider provider = new AesProvider();
                    provider.SetKeyIV(_primaryKey?.Value);
                    byte[]? first = provider.Encrypt(data);

                    provider.SetKeyIV(_secondaryKey?.Value);
                    byte[]? second = provider.Encrypt(first);

                    provider.SetKeyIV(_tertiaryKey?.Value);
                    byte[]? third = provider.Encrypt(second);
                    provider.Dispose();

                    if (third != null)
                    {
                        result = new SecureByteArray(third);
                        Array.Clear(third, 0, third.Length);
                    }
                    if (first != null)
                        Array.Clear(first, 0, first.Length);
                    if (second != null)
                        Array.Clear(second, 0, second.Length);
                }

            }
            return result;
        }
        /// <summary>
        /// Decodes the byte array into a string.
        /// </summary>
        /// <param name="data">
        /// A byte array containing the text data, or <b>null</b>.
        /// </param>
        /// <returns>
        /// A string containing the data or <see cref="string.Empty"/> if the data is <b>null</b>.
        /// </returns>
        private string? DecodeString(SecureByteArray? data)
        {
            string? clearValue = null;

            if (data != null)
            {
                byte[]? content = data.Value;
                if (content == null || content.Length == 0)
                    clearValue = string.Empty;
                else
                {
                    AesProvider provider = new AesProvider();
                    byte[]? encrypted = data.Value;

                    provider.SetKeyIV(_tertiaryKey?.Value);
                    byte[]? third = provider.Decrypt(encrypted);

                    provider.SetKeyIV(_secondaryKey?.Value);
                    byte[]? second = provider.Decrypt(third);

                    provider.SetKeyIV(_primaryKey?.Value);
                    byte[]? first = provider.Decrypt(second);
                    provider.Dispose();

                    if (first != null)
                    {
                        clearValue = Encoding.ASCII.GetString(first);
                        Array.Clear(first, 0, first.Length);
                    }

                    Array.Clear(content, 0, content.Length);

                    if (second != null)
                        Array.Clear(second, 0, second.Length);
                    if (third != null)
                        Array.Clear(third, 0, third.Length);
                    if (encrypted != null)
                        Array.Clear(encrypted, 0, encrypted.Length);
                }
            }
            return clearValue;
        }
        #endregion
    }
}
