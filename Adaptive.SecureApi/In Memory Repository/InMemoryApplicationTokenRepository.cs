using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SecureApi.Tokens;

/// <summary>
/// Provides a simple in-memory repository for app authorization tokens.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IApplicationTokenRepository" />
public sealed class InMemoryApplicationTokenRepository : DisposableObjectBase, IApplicationTokenRepository
{
    #region Private Member Declarations
    /// <summary>
    /// The in-memory tokens table.
    /// </summary>
    private static Dictionary<Guid, IApplicationAuthorizationToken> _appTokens = new Dictionary<Guid, IApplicationAuthorizationToken>();
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates and stores the new application auth token.
    /// </summary>
    /// <param name="sessionId">
    /// A <see cref="Guid"/> containing the ID of the related session instance.
    /// </param>
    /// <returns>
    /// A new <see cref="IApplicationAuthorizationToken"/> instance ready for use.
    /// </returns>
    public Task<IApplicationAuthorizationToken?> CreateAndStoreNewTokenAsync(Guid sessionId)
    {
        IApplicationAuthorizationToken? newToken = null;

        if (!_appTokens.ContainsKey(sessionId))
        {

            newToken = new ApplicationAuthorizationToken();
            _appTokens.Add(sessionId, newToken);
        }
        return Task.FromResult(newToken);
    }

    /// <summary>
    /// Gets the application authorization token for the specified session.
    /// </summary>
    /// <param name="sessionId">A <see cref="Guid" /> containing the ID of the session.</param>
    /// <returns>
    /// The <see cref="ApplicationAuthorizationToken" /> instance for the session, or <b>null</b>.
    /// </returns>
    public Task<IApplicationAuthorizationToken?> GetAppAuthorizationTokenAsync(Guid sessionId)
    {
        IApplicationAuthorizationToken? token = null;

        if (_appTokens.ContainsKey(sessionId))
            token = _appTokens[sessionId];

        return Task.FromResult(token);
    }
    /// <summary>
    /// Stores the application authorization token.
    /// </summary>
    /// <param name="sessionId">A <see cref="Guid" /> containing the ID of the session.</param>
    /// <param name="token">The <see cref="ApplicationAuthorizationToken" /> instance to be stored.</param>
    /// <returns>
    ///   <b>true</b> if the operation is successful; otherwise, returns <b>false</b>;
    /// </returns>
    public Task<bool> StoreAppAuthorizationTokenAsync(Guid sessionId, IApplicationAuthorizationToken token)
    {
        if (_appTokens.ContainsKey(sessionId))
            _appTokens[sessionId] = token;
        else
            _appTokens.Add(sessionId, token);

        return Task.FromResult(true);
    }
    /// <summary>
    /// Verifies the application authorization token.
    /// </summary>
    /// <param name="sessionId">A <see cref="Guid" /> containing the ID of the session.</param>
    /// <param name="tokenData">A byte array containing the token data to be verified.</param>
    /// <returns>
    /// <b>true</b> if the application authorization token is present and verified;
    /// otherwise, returns <b>false</b>.
    /// </returns>
    public Task<bool> VerifyAppAuthorizationTokenAsync(Guid sessionId, byte[] tokenData)
    {
        bool isValid = false;
        if (_appTokens.ContainsKey(sessionId))
        {
            IApplicationAuthorizationToken token = _appTokens[sessionId];
            int result = token.TokenData.Compare(tokenData);

            isValid = (result == 0);
        }
        return Task.FromResult(isValid);
    }
    #endregion
}
