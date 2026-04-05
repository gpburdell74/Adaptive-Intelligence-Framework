namespace Adaptive.Intelligence.SecureApi.Sessions;

/// <summary>
/// Provides the signature definition for implementing a session repository.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ISessionRepository : IDisposable
{
    /// <summary>
    /// Creates and stores a new session data object.
    /// </summary>
    /// <returns>
    /// A new <see cref="ISecureSession"/> instance with a unique ID value.
    /// </returns>
    Task<ISecureApiSession> CreateNewSessionAsync();

    /// <summary>
    /// Deletes the session.
    /// </summary>
    /// <param name="sessionId">
    /// A <see cref="Guid"/> containing the session identifier.
    /// </param>
    /// <returns>
    /// <b>true</b> if the session was deleted successfully; otherwise, returns <b>false</b>.
    /// </returns>
    Task<bool> DeleteSessionAsync(Guid sessionId);
    
    /// <summary>
    /// Gets the session by the specified ID value.
    /// </summary>
    /// <param name="sessionId">
    /// A <see cref="Guid"/> containing the session identifier.
    /// </param>
    /// <returns>
    /// The <see cref="SecureSession"/> for the matching ID value, or <b>null</b>
    /// if the operation fails.
    /// </returns>
    Task<ISecureApiSession?> GetSessionAsync(Guid sessionId);

    /// <summary>
    /// Loads the list of current sessions from the repository.
    /// </summary>
    /// <remarks>
    /// This method is intended to be used during the initialization of the session management system, 
    /// to load any existing sessions that may have been stored in a persistent storage medium (e.g., 
    /// database, file system) before the application was restarted. 
    /// 
    /// The implementation should ensure that the loaded sessions are valid and can be used for subsequent 
    /// operations.
    /// </remarks>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> list of <see cref="IServerSession"/> instances containing the current
    /// server-side session definitions if successful; otherwise, 
    /// returns <b>null</b>.
    /// </returns>
    Task<IEnumerable<IServerSession>?> LoadSessionsAsync();

    /// <summary>
    /// Updates the session data record.
    /// </summary>
    /// <param name="session">
    /// The <see cref="ISecureSession"/> instance to be updated.
    /// </param>
    /// <returns>
    /// <b>true</b> if the session was updated successfully; otherwise, returns <b>false</b>.
    /// </returns>
    Task<bool> UpdateSessionAsync(ISecureApiSession session);

}