namespace Adaptive.Intelligence.Shared.Security
{
    /// <summary>
    /// Provides the signature definition for storing a key value in memory securely.
    /// information.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface ISecureKeyStore : IDisposable, ICloneable
    {
        #region Properties
        /// <summary>
        /// Gets or sets the secure key data.
        /// </summary>
        /// <value>
        /// A <see cref="SecureByteArray"/> instance containing the byte array containing the cryptographic key content.
        /// </value>
        SecureByteArray KeyContent { get; set; }
        #endregion

        #region Methods / Functions
        /// <summary>
        /// Clears the byte arrays, erasing the key data memory content.
        /// </summary>
        void ClearKeyMemory();
        /// <summary>
        /// Gets the key data content.
        /// </summary>
        /// <returns>
        /// A byte array containing the encryption key content.
        /// </returns>
        byte[] GetKeyData();
        /// <summary>
        /// Gets a portion of the key data content.
        /// </summary>
        /// <param name="startPosition">
        /// An integer specifying the zero-index ordinal position at which to start reading the key data.
        /// </param>
        /// <param name="length">
        /// The number of bytes of the key value to be read.
        /// </param>
        /// <returns>
        /// A byte array containing the specified key content.
        /// </returns>
        byte[] GetKeyData(int startPosition, int length);
        /// <summary>
        /// Gets the key data as a base-64 encoded string.
        /// </summary>
        /// <returns>
        /// A base-64 encoded string containing the key content.
        /// </returns>
        string GetKeyDataAsBase64String();
        /// <summary>
        /// Sets the key data from the byte array.
        /// </summary>
        /// <param name="keyData">
        /// A byte array containing the key data.
        /// </param>
        void SetKeyData(byte[] keyData);
        /// <summary>
        /// Sets the key data from the specified string.
        /// </summary>
        /// <param name="keyData">
        /// A base-64 encoded string representing a byte array containing the key data.
        /// </param>
        void SetKeyDataFromBase64String(string keyData);
        #endregion
    }
}
