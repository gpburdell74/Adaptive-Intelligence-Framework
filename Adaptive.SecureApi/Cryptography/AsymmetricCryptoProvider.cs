using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Security;

namespace Adaptive.SecureApi.Cryptography;

/// <summary>
/// Provides the standard asymmetric cryptographic provider for the secure API system.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IAsymmetricCryptoProvider" />
public class AsymmetricCryptoProvider : DisposableObjectBase, IAsymmetricCryptoProvider
{

    #region Private Member Declarations    
    /// <summary>
    /// The key store list.
    /// </summary>
    private List<AesKeyStore>? _keyStore;

    /// <summary>
    /// The AES engine.
    /// </summary>
    private AesProvider? _aes;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="SymmetricCryptoProvider"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public AsymmetricCryptoProvider()
    {
        // Create the engine.
        _aes = new AesProvider();

        // Create a default key store and populate with one key.
        AesKeyStore newStore = new AesKeyStore(_aes.GetKeyData()!);
        _keyStore = new List<AesKeyStore>();
        _keyStore.Add(newStore);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SymmetricCryptoProvider"/> class.
    /// </summary>
    /// <param name="keyData">
    /// An <see cref="IEnumerable{T}"/> list of <see cref="byte"/> arrays containing The key data.
    /// </param>
    public AsymmetricCryptoProvider(IEnumerable<byte[]> keyData)
    {
        // Create the engine.
        _aes = new AesProvider();

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
            _aes?.Dispose();
        }

        _aes = null;
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

        if (_keyStore != null && _aes != null)
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
        if (encryptedData == null || _keyStore == null || _aes == null)
            return null;

        if (keySequence < 0 || keySequence >= _keyStore.Count)
            return null;

        // Set the key data and then perform the operation.
        _aes.SetKeyIV(_keyStore[keySequence].GetKeyIVData());
        return _aes.Decrypt(encryptedData);
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

        if (_keyStore != null && _aes != null)
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
        if (clearData == null || _keyStore == null || _aes == null)
            return null;

        if (keySequence < 0 || keySequence > _keyStore.Count)
            return null;

        // Set the key data and then perform the operation.
        _aes.SetKeyIV(_keyStore[keySequence].GetKeyIVData());
        return _aes.Encrypt(clearData);
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
        _keyStore = new List<AesKeyStore>();
        int length = keyData.Count();

        for (int count = 0; count < length; count++)
        {
            _keyStore.Add(new AesKeyStore(list[count]));
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
            _keyStore = new List<AesKeyStore>();
            int length = _keyStore.Count;

            for (int count = length; count >= 0; count--)
            {
                _keyStore[count].ClearKeyMemory();
                _keyStore[count].Dispose();
            }

            _keyStore.Clear();
        }
    }
    #endregion
}