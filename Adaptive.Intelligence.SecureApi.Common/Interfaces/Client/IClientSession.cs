using Adaptive.Intelligence.SecureApi.Cryptography;

namespace Adaptive.Intelligence.SecureApi.Client;

/// <summary>
/// Provides the signature definition for the client session data for the Secure API client.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IClientSession : IDisposable
{
    #region Public Properties
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
    /// Gets or sets the primary RSA public key.
    /// </summary>
    /// <value>
    /// A byte array containbing the primary RSA public key, or <b>null</b>.
    /// </value>
    byte[]? PrimaryRsaPublicKey { get; set; }
    /// <summary>
    /// Gets or sets the primary RSA public key.
    /// </summary>
    /// <value>
    /// A byte array containbing the primary RSA public key, or <b>null</b>.
    /// </value>
    byte[]? SecondaryRsaPublicKey { get; set; }
    /// <summary>
    /// Gets or sets the primary RSA public key.
    /// </summary>
    /// <value>
    /// A byte array containbing the primary RSA public key, or <b>null</b>.
    /// </value>
    byte[]? TertiaryRsaPublicKey { get; set; }

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
    /// Creates the symmetric cryptographic provider instance from the local key data.
    /// </summary>
    /// <returns>
    /// The <see cref="ISymmetricCryptoProvider"/> instance if successful, or <b>null</b> if the keys are not set.
    /// </returns>
    ISymmetricCryptoProvider? CreateCryptoProvider();
}
#endregion

