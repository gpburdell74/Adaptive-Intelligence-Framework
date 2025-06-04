namespace Adaptive.SecureApi.Tokens;

/// <summary>
/// Provides the signature definition for an application authorization token definition
/// for the Secure API Server and Client.
/// </summary>
/// <remarks>
/// This token is used to validate and ensure a client application has invoked the APIs correctly and
/// has performed the initial key exchange process successfully.
/// </remarks>
/// <seealso cref="IDisposable" />
public interface IApplicationAuthorizationToken : IDisposable
{
    #region Public Properties
    /// <summary>
    /// Gets or sets the reference to the hash data.
    /// </summary>
    /// <value>
    /// A byte array containing the SHA-512 hash for the instance, or <b>null</b>.
    /// </value>
    byte[]? HashData { get; set; }

    /// <summary>
    /// Gets or sets the name of the machine on which the token is created.
    /// </summary>
    /// <value>
    /// A string containing the name of the machine, or <b>null</b>.
    /// </value>
    string? MachineName { get; set; }

    /// <summary>
    /// Gets or sets the reference to the original randomized data.
    /// </summary>
    /// <value>
    /// A byte array of <see cref="TOKEN_SIZE"/> containing the random data.
    /// </value>
    byte[]? OriginalData { get; set; }

    /// <summary>
    /// Gets or sets the ID of the session the token is related to.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> containing the session ID, or <b>null</b>.
    /// </value>
    Guid? SessionId { get; set; }

    /// <summary>
    /// Gets or sets the token creation date.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> containing the token creation date, or <b>null</b>.
    /// </value>
    DateTime? TokenCreationDate { get; set; }

    /// <summary>
    /// Gets the token data.
    /// </summary>
    /// <value>
    /// A byte array containing the token data, or <b>null</b>.
    /// </value>
    byte[]? TokenData { get; }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates the data content for the new token.
    /// </summary>
    void CreateData();
    /// <summary>
    /// Creates the byte array to represent the token instance.
    /// </summary>
    /// <returns>
    /// A byte array representing this token.
    /// </returns>
    byte[]? CreateTokenData();
    /// <summary>
    /// Sets the token data from the provided byte array.
    /// </summary>
    /// <param name="originalData">
    /// A byte array containing the original data used to constitute the token.
    /// </param>
    void SetTokenData(byte[]? originalData);
    #endregion
}
