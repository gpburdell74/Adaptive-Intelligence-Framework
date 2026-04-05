using Adaptive.Intelligence.SecureApi.Cryptography;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Security;

namespace Adaptive.Intelligence.SecureApi.Common.Cryptography.Asymmetric;

/// <summary>
/// Provides the standard RSA asymmetric cryptographic provider for the secure API system.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IAsymmetricCryptoProvider" />
public class RsaCryptoProvider : DisposableObjectBase, IAsymmetricCryptoProvider
{
    #region Private Member Declarations
    /// <summary>
    /// The key store list.
    /// </summary>
    private List<RsaKeyStore>? _keyStore;

    /// <summary>
    /// The RSA engine.
    /// </summary>
    private RsaProvider? _rsa;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="RsaCryptoProvider"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public RsaCryptoProvider()
    {
        // Create the engine.
        _rsa = new RsaProvider();

        // Create the key store list.
        _keyStore = new List<RsaKeyStore>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RsaCryptoProvider"/> class.
    /// </summary>
    /// <param name="keyData">
    /// An <see cref="IEnumerable{T}"/> list of <see cref="byte"/> arrays containing The key data.
    /// </param>
    public RsaCryptoProvider(IEnumerable<byte[]> keyData) : this()
    {
        SetKeyContent(keyData);
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
            ClearKeyData();
            _rsa?.Dispose();
        }

        _rsa = null;
        _keyStore = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Decrypts the specified encrypted data, using all the keys in the list of known keys.
    /// </summary>
    /// <param name="encryptedData">A byte array containing the encrypted data.</param>
    /// <returns>
    /// A byte array containing the decrypted content if successful, or <b>null</b>.
    /// </returns>
    public byte[]? DecryptAll(byte[]? encryptedData)
    {
        byte[]? operationData = null;

        if (_keyStore != null && _rsa != null)
        {
            operationData = ByteArrayUtil.CopyToNewArray(encryptedData);
            int length = _keyStore.Count;

            // Iterate over each key value in reverse and decrypt the provided contents,
            // and then use the result as the input for the next decrypted value.
            for (int count = length - 1; count >= 0; count--)
            {
                byte[]? result = Encrypt(operationData, count);
                operationData = ByteArrayUtil.CopyToNewArray(result);
                ByteArrayUtil.Clear(result);
            }
        }
        return operationData;
    }

    /// <summary>
    /// Decrypts the specified encrypted data, using the key in the specified key sequence.
    /// </summary>
    /// <param name="encryptedData">A byte array containing the encrypted data.</param>
    /// <param name="keySequence">An integer specifying the ordinal index of the key value to use.</param>
    /// <returns>
    /// A byte array containing the decrypted content if successful, or <b>null</b>.
    /// </returns>
    public byte[]? Decrypt(byte[]? encryptedData, int keySequence)
    {
        if (encryptedData == null || _keyStore == null || _rsa == null)
            return null;

        if (keySequence < 0 || keySequence >= _keyStore.Count)
            return null;

        // Set the key data and then perform the operation.
        SetRsaPublicKeyFromKeyStore(_keyStore[keySequence]);
        return _rsa.Decrypt(encryptedData);
    }

    /// <summary>
    /// Encrypts the specified clear data, using all the keys in the list of known keys.
    /// </summary>
    /// <param name="clearData">A byte array containing the clear data.</param>
    /// <returns>
    /// A byte array containing the encrypted content if successful, or <b>null</b>.
    /// </returns>
    public byte[]? EncryptAll(byte[]? clearData)
    {
        byte[]? operationData = null;

        if (_keyStore != null && _rsa != null)
        {
            operationData = ByteArrayUtil.CopyToNewArray(clearData);
            int length = _keyStore.Count;

            // Iterate over each key value and encrypt the provided contents,
            // and then use the result as the input for the next encrypted value.
            for (int count = 0; count < length; count++)
            {
                byte[]? result = Encrypt(operationData, count);
                operationData = ByteArrayUtil.CopyToNewArray(result);
                ByteArrayUtil.Clear(result);
            }
        }
        return operationData;
    }

    /// <summary>
    /// Encrypts the specified clear data, using the key in the specified key sequence.
    /// </summary>
    /// <param name="clearData">A byte array containing the clear data to be encrypted.</param>
    /// <param name="keySequence">An integer specifying the ordinal index of the key value to use.</param>
    /// <returns>
    /// A byte array containing the encrypted content if successful, or <b>null</b>.
    /// </returns>
    public byte[]? Encrypt(byte[]? clearData, int keySequence)
    {
        if (clearData == null || _keyStore == null || _rsa == null)
            return null;

        if (keySequence < 0 || keySequence > _keyStore.Count)
            return null;

        // Set the key data and then perform the operation.
        SetRsaPublicKeyFromKeyStore(_keyStore[keySequence]);
        return _rsa.Encrypt(clearData);
    }

    /// <summary>
    /// Sets the cryptographic key content for the provider.
    /// </summary>
    /// <param name="keyData">An <see cref="IEnumerable{T}" /> list of <see cref="byte" /> arrays containing the key data.
    /// The key data.</param>
    public void SetKeyContent(IEnumerable<byte[]> keyData)
    {
        ClearKeyData();

        // Create the list of keys.
        List<byte[]> list = keyData.ToList();
        _keyStore = new List<RsaKeyStore>();
        int length = keyData.Count();

        for (int count = 0; count < length; count++)
        {
            _keyStore.Add(new RsaKeyStore(list[count]));
        }
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Removes any key data from memory.
    /// </summary>
    private void ClearKeyData()
    {
        // Clear memory.
        if (_keyStore != null)
        {
            _keyStore = new List<RsaKeyStore>();
            int length = _keyStore.Count;

            for (int count = length; count >= 0; count--)
            {
                _keyStore[count].ClearKeyMemory();
                _keyStore[count].Dispose();
            }

            _keyStore.Clear();
        }
    }

    /// <summary>
    /// Sets the RSA public key data from the provided key store instance.
    /// </summary>
    /// <param name="store">
    /// The <see cref="RsaKeyStore"/> instance containing the key data.
    /// </param>
    private void SetRsaPublicKeyFromKeyStore(RsaKeyStore? store)
    {
        // Set the key data and then perform the operation.
        if (store != null && _rsa != null)
        {
            byte[]? original = store.GetKeyData();
            if (original != null)
            {
                int length = original.Length;

                // Extract the exponent.
                byte[] exponent = new byte[3];
                Array.Copy(original, length - 3, exponent, 0, 3);

                // Extract the modulus.
                byte[] modulus = new byte[original.Length - 3];
                Array.Copy(original, 0, modulus, 0, length - 3);

                _rsa.ImportPublicKey(modulus, exponent);
            }
        }
    }
    #endregion
}