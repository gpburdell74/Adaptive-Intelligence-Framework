using Adaptive.Intelligence.Shared.Security;

namespace Adaptive.Intelligence.SecureApi.Cryptography;

/// <summary>
/// Provides factory methods / functions for creating instances of asymmetric cryptographic providers.
/// </summary>
public static class SymmetricCryptoProviderFactory
{
    #region Private Member Declarations    
    /// <summary>
    /// The available instance pool.
    /// </summary>
    private static List<AesProvider>? _available = null;
    /// <summary>
    /// The instances that are currently in use.
    /// </summary>
    private static List<AesProvider>? _inUse = null;
    /// <summary>
    /// The threading lock object to ensure thread safety when accessing the pool.
    /// </summary>
    private static readonly object _lock = new object();
    #endregion

    /// <summary>
    /// Initializes the <see cref="SymmetricCryptoProviderFactory"/> class.
    /// </summary>
    static SymmetricCryptoProviderFactory()
    {
        _inUse = new List<AesProvider>();
        _available = new List<AesProvider>();

        // Create 100 instances for the pool.
        for (int count = 0; count < 100; count++)
        {
            _available.Add(new AesProvider());
        }
    }

    /// <summary>
    /// Creates / initializes an <see cref="AesProvider"/> instance.
    /// </summary>
    /// <returns>
    /// The <see cref="AesProvider"/> instance populated from the public key, or <b>null</b> if the 
    /// provider could not be created.
    /// </returns>
    public static AesProvider? CreateSymmetricProvider()
    {
        AesProvider? newInstance = null;

        if (_available != null && _inUse != null)
        {
            lock (_lock)
            {
                if (_available.Count == 0)
                {
                    // No available providers, create a new one.
                    newInstance = new AesProvider();
                    if (newInstance != null)
                        _inUse.Add(newInstance);
                }
                else
                {
                    // Get the first available provider.
                    newInstance = _available[0];
                    _available.RemoveAt(0);
                    _inUse.Add(newInstance);
                }
            }
        }
        return newInstance;
    }
    /// <summary>
    /// Releases the AEs / symmetric provider to the available pool.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="AesProvider"/> instance.
    /// </param>
    public static void ReleaseSymmetricProvider(AesProvider? provider)
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
}