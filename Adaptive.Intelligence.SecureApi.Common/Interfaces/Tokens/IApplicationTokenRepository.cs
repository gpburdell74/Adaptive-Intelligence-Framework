namespace Adaptive.Intelligence.SecureApi.Tokens;

/// <summary>
/// Provides the signature definition for implementing an application authorization repository.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IApplicationTokenRepository : IDisposable 
{
    /// <summary>
    /// Creates and stores the new application auth token.
    /// </summary>
    /// <param name="sessionId">
    /// A <see cref="Guid"/> containing the ID of the related session instance.
    /// </param>
    /// <returns>
    /// A new <see cref="IApplicationAuthorizationToken"/> instance ready for use.
    /// </returns>
    Task<IApplicationAuthorizationToken?> CreateAndStoreNewTokenAsync(Guid sessionId);

    /// <summary>
    /// Gets the application authorization token for the specified session.
    /// </summary>
    /// <param name="sessionId">
    /// A <see cref="Guid"/> containing the ID of the session.
    /// </param>
    /// <returns>
    /// The <see cref="IApplicationAuthorizationToken"/> instance for the session, or <b>null</b>.
    /// </returns>
    Task<IApplicationAuthorizationToken?> GetAppAuthorizationTokenAsync(Guid sessionId);

    /// <summary>
    /// Stores the application authorization token.
    /// </summary>
    /// <param name="sessionId">
    /// A <see cref="Guid"/> containing the ID of the session.
    /// </param>
    /// <param name="token">
    /// The <see cref="IApplicationAuthorizationToken"/> instance to be stored.
    /// </param>
    /// <returns>
    /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>;
    /// </returns>
    Task<bool> StoreAppAuthorizationTokenAsync(Guid sessionId, IApplicationAuthorizationToken token);
    /// <summary>
    /// Verifies the application authorization token.
    /// </summary>
    /// <param name="sessionId">
    /// A <see cref="Guid"/> containing the ID of the session.
    /// </param>
    /// <param name="tokenData">
    /// A byte array containing the token data to be verified.
    /// </param>
    /// <returns>
    /// <b>true</b> if the application authorization token is present and verified;
    /// otherwise, returns <b>false</b>.
    /// </returns>
    Task<bool> VerifyAppAuthorizationTokenAsync(Guid sessionId, byte[] tokenData);
}

