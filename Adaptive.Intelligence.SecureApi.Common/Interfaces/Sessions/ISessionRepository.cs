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
    Task<ISecureSession> CreateNewSessionAsync();
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
    Task<ISecureSession?> GetSessionAsync(Guid sessionId);

    /// <summary>
    /// Updates the session data record..
    /// </summary>
    /// <param name="session">
    /// The <see cref="ISecureSession"/> instance to be updated.
    /// </param>
    /// <returns>
    /// <b>true</b> if the session was updated successfully; otherwise, returns <b>false</b>.
    /// </returns>
    Task<bool> UpdateSessionAsync(ISecureSession session);
}