using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Security;
using System.Security.Cryptography;

namespace Adaptive.Taz.Cryptography
{
    /// <summary>
    /// Provides the mechanism for creating encryption keys based on user input.
    /// </summary>
    /// <seealso cref="ExceptionTrackingBase" />
    internal sealed class KeyGenerator : ExceptionTrackingBase
    {
        #region Private Member Declarations		
        /// <summary>
        /// The SHA-512 hash provider instance.
        /// </summary>
        private SHA512? _hashProvider;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyGenerator"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public KeyGenerator()
        {
            _hashProvider = SHA512.Create();
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

            _hashProvider = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Generates the user key data.
        /// </summary>
        /// <param name="userId">
        /// A string containing the user ID value.
        /// </param>
        /// <param name="password">
        /// A string containing the user password value.
        /// </param>
        /// <param name="userPIN">
        /// A string containing the user PIN value.
        /// </param>
        /// <returns>
        /// An <see cref="OpsKeyContainer"/> instance containing the generated key data, if successful;
        /// otherwise, returns <b>null</b>.
        /// </returns>
        public OpsKeyContainer? GenerateUserKeyData(string userId, string password, string userPIN)
        {
            OpsKeyContainer? keyContainer = null;

            // 1. Convert the user PIN to an integer.
            int pin = TranslateUserPin(userPIN);
            if (pin > 0)
            {
                // 2. Create a random salt value.
                byte[] salt = RandomNumberGenerator.GetBytes(8);

                // 3. Translate the user ID and password into a byte array hash and 
                //	  then into a hexadecimal string representation.
                string userIdHash = GenerateHashDataForString(userId);
                string passwordHash = GenerateHashDataForString(password);

                // 4. Create the key data.
                keyContainer = GenerateUserKeyData(userIdHash, passwordHash, salt, pin);
                ByteArrayUtil.Clear(salt);
            }
            return keyContainer;
        }
        #endregion

        #region Private Methods / Functions		
        /// <summary>
        /// Generates the hash value data for the specified string data.
        /// </summary>
        /// <param name="sourceData">
        /// The source data to create the hash value for.
        /// </param>
        /// <returns>
        /// A string containing a hexadecimal representation of a byte array containing the SHA-512 hash.
        /// </returns>
        private string GenerateHashDataForString(string sourceData)
        {
            string result = string.Empty;
            byte[]? hashData = null;

            try
            {
                if (_hashProvider != null)
                {
                    hashData = _hashProvider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(sourceData));
                }
            }
            catch (Exception ex)
            {
                Exceptions?.Add(ex);
                hashData = null;
            }

            if (hashData != null)
            {
                // Convert to a hex string and split the string in half.
                string? hashAsHex = Convert.ToHexString(hashData);
                string? partTwo = hashAsHex.Substring(64, 64);
                string? partOne = hashAsHex.Substring(0, 64);

                // Clear memory.
                ByteArrayUtil.Clear(hashData);
                hashData = null;
                hashAsHex = null;

                // Create the hash string.
                byte[] keyData = PasswordKeyCreator.CreatePrimaryKeyData(partOne, partTwo);
                result = Convert.ToHexString(keyData);

                // Clear memory.
                ByteArrayUtil.Clear(keyData);
                ByteArrayUtil.Clear(hashData);
                partOne = null;
                partTwo = null;
            }

            hashData = null;
            return result;
        }
        /// <summary>
        /// Generates the user key data.
        /// </summary>
        /// <param name="userIdHash">
        /// A string containing the hexadecimal representation of the hash of the user identifier data 
        /// from <see cref="GenerateHashDataForString"/>.
        /// </param>
        /// <param name="passwordHash">
        /// A string containing the hexadecimal representation of the hash of the password data 
        /// from <see cref="GenerateHashDataForString"/>.
        /// </param>
        /// <param name="salt">
        /// A byte array containing the randomized salt value.
        /// </param>
        /// <param name="pin">
        /// An integer specifying the user PIN value.
        /// </param>
        /// <returns>
        /// An <see cref="OpsKeyContainer"/> containing the generated user encryption key and key variant for
        /// use in AES operations.
        /// </returns>
        private OpsKeyContainer GenerateUserKeyData(string userIdHash, string passwordHash, byte[] salt, int pin)
        {
            // Derive the first and second key parts.
            byte[] firstKeyPart = GenerateKeyPart(userIdHash, salt, pin);
            byte[] secondKeyPart = GenerateKeyPart(passwordHash, salt, pin);

            // Concatenate the arrays.
            byte[] keyData = ByteArrayUtil.ConcatenateArrays(firstKeyPart, secondKeyPart);

            // Create the primary key variant.
            byte[] keyVariant = PasswordKeyCreator.CreateKeyVariant(keyData);

            OpsKeyContainer container = new OpsKeyContainer(keyData, keyVariant);

            // Clear and return.
            ByteArrayUtil.Clear(firstKeyPart);
            ByteArrayUtil.Clear(secondKeyPart);
            ByteArrayUtil.Clear(keyData);
            ByteArrayUtil.Clear(keyVariant);

            return container;
        }
        /// <summary>
        /// Generates a key part from the provided data.
        /// </summary>
        /// <param name="sourceData">
        /// A string containing the source data to work with.
        /// </param>
        /// <param name="salt">
        /// A byte array containing the randomized salt value.
        /// </param>
        /// <param name="pin">
        /// An integer specifying the user PIN value.
        /// </param>
        /// <returns>
        /// A byte array containing half of the key data.
        /// </returns>
        private byte[] GenerateKeyPart(string sourceData, byte[] salt, int pin)
        {
            // Derive a partial encryption key from the provided string data.
            byte[]? keyPart = CryptoFactory.DeriveEncryptionKey(sourceData, salt);
            if (keyPart != null)
            {
                // 1. Translate the bytes to numbers. (we are assuming the byte array is 24 elements).
                long[] parts = CryptoFactory.BytesToInt64Array(keyPart);

                // 2. Perform an XOR operation with the user PIN.
                parts[0] = parts[0] ^ (pin * CryptoConstants.PinMultiplierOne);
                parts[1] = parts[1] ^ (pin * CryptoConstants.PinMultiplierTwo);
                parts[2] = parts[2] ^ (pin * CryptoConstants.PinMultiplierThree);

                // Translate back to a byte array.
                byte[] newKeyPart = CryptoFactory.Int64ArrayToBytes(parts);

                // Clear Memory.
                ByteArrayUtil.Clear(parts);

                return newKeyPart;
            }
            else
            {
                return [];
            }
        }
        /// <summary>
        /// Translates the user pin from a string to an integer.
        /// </summary>
        /// <param name="userPIN">
        /// A string containing the user PIN value.
        /// </param>
        /// <returns>
        /// The integer value representing the PIN value.
        /// </returns>
        private int TranslateUserPin(string? userPIN)
        {
            int value = 0;
            if (!string.IsNullOrEmpty(userPIN) && (userPIN.Length >= FileSpecConstants.MinPinSize))
            {
                value = SafeConverter.ToInt32(userPIN);
            }
            return value;
        }
        #endregion
    }
}
