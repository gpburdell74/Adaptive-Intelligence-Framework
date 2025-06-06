using Adaptive.Intelligence.SecureApi.Tokens;

namespace Adaptive.Intelligence.SecureApi;

/// <summary>
/// Provides the signature definition for implementing an Application Token Repository Service for 
/// saving, loading, and managing application authorization tokens for  the secure API calls.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IApplicationTokenRepositoryService : IDisposable
{
    /// <summary>
    /// Creates and returns a new, configured <see cref="IApplicationTokenRepository"/> instance 
    /// ready for use.
    /// </summary>
    /// <returns>
    /// A new <see cref="IApplicationTokenRepository"/> implementation instance.
    /// </returns>
    Task<IApplicationTokenRepository> CreateNewRepositoryInstanceAsync();

    /// <summary>
    /// Takes care of disposing and deallocating the repository instance.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IApplicationTokenRepository"/> instance to be disposed of.
    /// </param>
    Task DeleteRepositoryInstanceAsync(IApplicationTokenRepository repository);

}
