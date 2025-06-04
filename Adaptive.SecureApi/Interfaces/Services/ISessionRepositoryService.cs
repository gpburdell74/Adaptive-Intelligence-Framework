using Adaptive.SecureApi.Sessions;

namespace Adaptive.SecureApi.Server;

/// <summary>
/// Provides the signature definition for implementing a Session Repository Service for saving, loading,
/// and managing session data for  the secure API calls.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ISessionRepositoryService : IDisposable
{
    /// <summary>
    /// Creates and returns a new, configured <see cref="ISessionRepository"/> instance ready for use.
    /// </summary>
    /// <returns>
    /// A new <see cref="ISessionRepository"/> implementation instance.
    /// </returns>
    Task<ISessionRepository> CreatNewRepositoryInstanceAsync();

    /// <summary>
    /// Takes care of disposing and deallocating the repository instance.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="ISessionRepository"/> instance to be disposed of.
    /// </param>
    Task DeleteRepositoryInstanceAsync(ISessionRepository repository);
}
