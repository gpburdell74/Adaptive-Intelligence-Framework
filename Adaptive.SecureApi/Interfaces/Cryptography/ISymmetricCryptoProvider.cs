namespace Adaptive.SecureApi.Cryptography;

/// <summary>
/// Provides the signature definition for implementing a symmetric cryptographic provider.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ISymmetricCryptoProvider : IDisposable
{
    /// <summary>
    /// Decrypts the specified encrypted data, using the key in the specified key sequence.
    /// </summary>
    /// <param name="encryptedData">
    /// A byte array containing the encrypted data.
    /// </param>
    /// <param name="keySequence">
    /// An integer specifying the ordinal index of the key value to use.
    /// </param>
    /// <returns>
    /// A byte array containing the decrypted content if successful, or <b>null</b>.
    /// </returns>
    byte[]? Decrypt(byte[]? encryptedData, int keySequence);

    /// <summary>
    /// Decrypts the specified encrypted data, using all the keys in the list of known keys.
    /// </summary>
    /// <param name="encryptedData">
    /// A byte array containing the encrypted data.
    /// </param>
    /// <returns>
    /// A byte array containing the decrypted content if successful, or <b>null</b>.
    /// </returns>
    byte[]? DecryptAll(byte[]? encryptedData);

    /// <summary>
    /// Encrypts the specified clear data, using the key in the specified key sequence.
    /// </summary>
    /// <param name="clearData">
    /// A byte array containing the clear data to be encrypted.
    /// </param>
    /// <param name="keySequence">
    /// An integer specifying the ordinal index of the key value to use.
    /// </param>
    /// <returns>
    /// A byte array containing the encrypted content if successful, or <b>null</b>.
    /// </returns>
    byte[]? Encrypt(byte[]? clearData, int keySequence);

    /// <summary>
    /// Encrypts the specified clear data, using all the keys in the list of known keys.
    /// </summary>
    /// <param name="clearData">
    /// A byte array containing the clear data.
    /// </param>
    /// <returns>
    /// A byte array containing the encrypted content if successful, or <b>null</b>.
    /// </returns>
    byte[]? EncryptAll(byte[]? clearData);

    /// <summary>
    /// Sets the cryptographic key content for the provider.
    /// </summary>
    /// <param name="keyData">
    /// An <see cref="IEnumerable{T}"/> list of <see cref="byte"/> arrays containing the key data.
    /// The key data.</param>
    void SetKeyContent(IEnumerable<byte[]> keyData);
}