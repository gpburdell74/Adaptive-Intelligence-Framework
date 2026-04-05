using Adaptive.Intelligence.SecureApi.Cryptography;

namespace Adaptive.Intelligence.SecureApi.Sessions;

/// <summary>
/// Provides the signature definition for the basic implementations of session data for the Secure API 
/// server and client.
/// </summary>
/// <remarks>
/// A Secure API session requires an exchange of three (3) assymetric public keys, and three
/// private symmetric key values.  Generally the implementation will be the key data for 
/// RSA in the assymetric implementation, and AES for the symmetric implementation.  
/// The session identifier is a <see cref="Guid"/> value that is used to correlate the session data 
/// on the client and server side, and is not used for any cryptographic purposes.
/// </remarks>
/// <seealso cref="IDisposable" />
public interface ISecureApiSession : IDisposable
{
    #region Public Properties
    /// <summary>
    /// Gets or sets the primary assymetric public key.
    /// </summary>
    /// <value>
    /// A byte array containing the primary assymetric public key, or <b>null</b>.
    /// </value>
    byte[]? PrimaryAsymmetricPublicKey { get; set; }

    /// <summary>
    /// Gets or sets the secondary assymetric public key.
    /// </summary>
    /// <value>
    /// A byte array containing the secondary assymetric public key, or <b>null</b>.
    /// </value>
    byte[]? SecondaryAsymmetricPublicKey { get; set; }

    /// <summary>
    /// Gets or sets the tertiary assymetric public key.
    /// </summary>
    /// <value>
    /// A byte array containing the tertiary assymetric public key, or <b>null</b>.
    /// </value>
    byte[]? TertiaryAsymmetricPublicKey { get; set; }

    /// <summary>
    /// Gets or sets the primary symmetric cryptographic key.
    /// </summary>
    /// <value>
    /// A byte array containing the primary cryptographic key, or <b>null</b>.
    /// </value>
    byte[]? PrimarySymmetricKey { get; set; }

    /// <summary>
    /// Gets or sets the secondary symmetric cryptographic key.
    /// </summary>
    /// <value>
    /// A byte array containing the seconadary cryptographic key, or <b>null</b>.
    /// </value>
    byte[]? SecondarySymmetricKey { get; set; }

    /// <summary>
    /// Gets or sets the tertiary symmetric cryptographic key.
    /// </summary>
    /// <value>
    /// A byte array containing the tertiary cryptographic key, or <b>null</b>.
    /// </value>
    byte[]? TertiarySymmetricKey { get; set; }

    /// <summary>
    /// Gets or sets the session identifier.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the session identifier, or <b>null</b> if not set.
    /// </value>
    Guid? SessionId { get; set; }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates and/or gets the reference to the asymmetric crypto provider instance attached to this session.
    /// </summary>
    /// <returns>
    /// An <see cref="IAsymmetricCryptoProvider"/> instance populated with the key values
    /// as defined on the session instance.
    /// </returns>
    IAsymmetricCryptoProvider? GetAsymmetricCryptoProvider();

    /// <summary>
    /// Creates and/or gets the symmetric crypto provider instance attached to this session.
    /// </summary>
    /// <returns>
    /// An <see cref="ISymmetricCryptoProvider"/> instance populated with the key values
    /// as defined on the session instance.
    /// </returns>
    ISymmetricCryptoProvider? GetSymmetricCryptoProvider();
}
#endregion

