using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.Shared.Security;

namespace Adaptive.Intelligence.SecureApi.Cryptography;

/// <summary>
/// Provides factory methods / functions for creating instances of asymmetric cryptographic providers.
/// </summary>
public static class AsymmetricCryptoProviderFactory
{
    #region Private Member Declarations    
    /// <summary>
    /// The available instance pool.
    /// </summary>
    private static List<RsaProvider>? _available = null;
    /// <summary>
    /// The instances that are currently in use.
    /// </summary>
    private static List<RsaProvider>? _inUse = null;
    /// <summary>
    /// The threading lock object to ensure thread safety when accessing the pool.
    /// </summary>
    private static readonly object _lock = new object();
    #endregion

    /// <summary>
    /// Initializes the <see cref="AsymmetricCryptoProviderFactory"/> class.
    /// </summary>
    static AsymmetricCryptoProviderFactory()
    {
        _inUse = new List<RsaProvider>();
        _available = new List<RsaProvider>();

        // Create 100 instances for the pool.
        for(int count = 0; count < 100; count++)
        {
            _available.Add(new RsaProvider());
        }
    }

    /// <summary>
    /// Creates / initializes an <see cref="RsaProvider"/> instance.
    /// </summary>
    /// <param name="rsaPublicKeyToImport">
    /// A byte array containing the RSA public key to import.
    /// </param>
    /// <returns>
    /// The <see cref="RsaProvider"/> instance populated from the public key, or <b>null</b> if the 
    /// provider could not be created.
    /// </returns>
    public static RsaProvider? CreateRsaProvider(byte[] rsaPublicKeyToImport)
    {
        RsaProvider? newInstance = null;

        if (_available != null && _inUse != null)
        {
            lock (_lock)
            {
                if (_available.Count == 0)
                {
                    // No available providers, create a new one.
                    newInstance = ManufactureRsaProvider(rsaPublicKeyToImport);
                    if (newInstance != null)
                        _inUse.Add(newInstance);
                }
                else
                {
                    // Get the first available provider.
                    newInstance = _available[0];
                    _available.RemoveAt(0);
                    _inUse.Add(newInstance);
                    ImportPublicKeyData(newInstance, rsaPublicKeyToImport);
                }
            }
        }
        return newInstance;
    }
    /// <summary>
    /// Releases the RSA provider to the available pool.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="RsaProvider"/> instance.
    /// </param>
    public static void ReleaseRsaProvider(RsaProvider? provider)
    {
        if (provider == null)
            return;
        lock (_lock)
        {
            // Remove the provider from the in-use list and add it back to the available pool.
            if (_inUse != null && _available != null)
            {
                _inUse.Remove(provider);
                _available.Add(provider);
            }
        }
    }
    /// <summary>
    /// Imports the specified public key data into the provided <see cref="RsaProvider" /> instance.
    /// </summary>
    /// <param name="provider">The provider instance used to import the public key data.  This, obviously, must not be <b>null</b>.</param>
    /// <param name="publicKeyToImport">A 131-element byte array containing the public key data to import.  This must include the 128 byte modulus,
    /// and the 3 byte exponent, in that order.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="provider"/> is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the <paramref name="publicKeyToImport"/> is null or is not not 131 bytes long.
    /// </exception>
    private static void ImportPublicKeyData(RsaProvider? provider, byte[] publicKeyToImport)
    {
        // Do nothing if the byte array is bad or the provider is null.
        if (provider == null)
            throw new ArgumentNullException(nameof(provider));

        if (publicKeyToImport.Length != 131)
            throw new ArgumentOutOfRangeException(nameof(publicKeyToImport), "The public key data must be 131 bytes long.");

        // Copy the public key data into the appropriate modulus and exponent arrays.
        byte[] mod = new byte[128];
        byte[] exp = new byte[3];
        Array.Copy(publicKeyToImport, 0, mod, 0, 128);
        Array.Copy(publicKeyToImport, 128, exp, 0, 3);

        try
        {
            // Create the provider and perform the public key import.
            provider.ImportPublicKey(mod, exp);
        }
        catch
        {
            throw;
        }
        finally
        {
            // Clear and return.
            Array.Clear(mod);
            Array.Clear(exp);
        }
    }
    /// <summary>
    /// Manufactures a new <see cref="RsaProvider"/> instance using the provided public key data.
    /// </summary>
    /// <param name="publicKeyToImport">
    /// A 131-element byte array containing the public key data to import.  This must include the 128 byte modulus, 
    /// and the 3 byte exponent, in that order.
    /// </param>
    /// <returns></returns>
    private static RsaProvider? ManufactureRsaProvider(byte[] publicKeyToImport)
    {
        RsaProvider? newProvider = null;

        // Create the provider and perform the public key import.
        try
        {
            newProvider = new RsaProvider();
            ImportPublicKeyData(newProvider, publicKeyToImport);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during the import.
            ExceptionLog.LogException(ex);
            newProvider = null;
        }

        return newProvider;
    }
}
