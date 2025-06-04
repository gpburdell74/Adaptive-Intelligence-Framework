using Adaptive.Intelligence.Shared;
using Adaptive.SecureApi.Sessions;

namespace Adaptive.SecureApi.Server;

/// <summary>
/// Provides a simple in-memory only storage / repository for session data.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ISessionRepository" />
public sealed class InMemorySessionRepository : DisposableObjectBase, ISessionRepository
{
    #region Private Member Declarations
    /// <summary>
    /// The in-memory sessions table.
    /// </summary>
    private static Dictionary<Guid, ISecureSession> _sessions = new Dictionary<Guid, ISecureSession>();
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Creates and stores a new session data object.
    /// </summary>
    /// <returns>
    /// A new <see cref="SecureSession" /> instance with a unique ID value.
    /// </returns>
    public Task<ISecureSession> CreateNewSessionAsync()
    {
        ISecureSession session = new SecureSession
        {
            SessionId = Guid.NewGuid()
        };
        _sessions.Add(session.SessionId.Value, session);

        return Task.FromResult(session);
    }
    /// <summary>
    /// Gets the session by the specified ID value.
    /// </summary>
    /// <param name="sessionId">A <see cref="Guid" /> containing the session identifier.</param>
    /// <returns>
    /// The <see cref="SecureSession" /> for the matching ID value, or <b>null</b>
    /// if the operation fails.
    /// </returns>
    public async Task<ISecureSession?> GetSessionAsync(Guid sessionId)
    {
        if (_sessions.ContainsKey(sessionId))
            return _sessions[sessionId];
        else
            return null;
    }
    /// <summary>
    /// Deletes the session.
    /// </summary>
    /// <param name="sessionId">A <see cref="Guid" /> containing the session identifier.</param>
    /// <returns>
    ///   <b>true</b> if the session was deleted successfully; otherwise, returns <b>false</b>.
    /// </returns>
    public Task<bool> DeleteSessionAsync(Guid sessionId)
    {
        if (_sessions.ContainsKey(sessionId))
            _sessions.Remove(sessionId);
        return Task.FromResult(true);
    }
    /// <summary>
    /// Updates the session data record.
    /// </summary>
    /// <param name="session">The <see cref="ISecureSession" /> instance to be updated.</param>
    /// <returns>
    ///   <b>true</b> if the session was updated successfully; otherwise, returns <b>false</b>.
    /// </returns>
    public Task<bool> UpdateSessionAsync(ISecureSession session)
    {
        _sessions[session.SessionId.Value] = session;
        return Task.FromResult(true);
    }
    #endregion
}