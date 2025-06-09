using Adaptive.Intelligence.SecureApi.Cryptography;

namespace Adaptive.Intelligence.SecureApi.Sessions;

/// <summary>
/// Provides the signature definition for class that contain the data for the secure 
/// session for the Secure API Server.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ISecureSession : IDisposable 
{
    #region Public Properties 
    /// <summary>
    /// Gets or sets the session identifier.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> specifying the session ID value.
    /// </value>
    Guid? SessionId { get; set; }

    #region RSA Keys
    /// <summary>
    /// Gets or sets the primary RSA public key.
    /// </summary>
    /// <value>
    /// A string containing the primary RSA public key.
    /// </value>
    string? PrimaryRsaPublicKey { get; set; }
    /// <summary>
    /// Gets or sets the secondary RSA public key.
    /// </summary>
    /// <value>
    /// A string containing the primary RSA public key.
    /// </value>
    string? SecondaryRsaPublicKey { get; set; }
    /// <summary>
    /// Gets or sets the tertiary RSA public key.
    /// </summary>
    /// <value>
    /// A string containing the primary RSA public key.
    /// </value>
    string? TertiaryRsaPublicKey { get; set; }
    #endregion

    #region AES Keys
    /// <summary>
    /// Gets or sets the primary AES key value.
    /// </summary>
    /// <value>
    /// A byte array containing the primary AES key data, or <b>null</b>.
    /// </value>
    byte[]? PrimaryAesKey { get; set; }
    /// <summary>
    /// Gets or sets the secondary AES key value.
    /// </summary>
    /// <value>
    /// A byte array containing the primary AES key data, or <b>null</b>.
    /// </value>
    byte[]? SecondaryAesKey { get; set; }
    /// <summary>
    /// Gets or sets the tertiary AES key value.
    /// </summary>
    /// <value>
    /// A byte array containing the primary AES key data, or <b>null</b>.
    /// </value>
    byte[]? TertiaryAesKey { get; set; }
    #endregion

    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Gets the asymmetric crypto provider instance attached to this session.
    /// </summary>
    /// <returns>
    /// An <see cref="IAsymmetricCryptoProvider"/> instance populated with the key values
    /// as defined on the session instance.
    /// </returns>
    IAsymmetricCryptoProvider? GetAsymmetricCryptoProvider();
    /// <summary>
    /// Gets the symmetric crypto provider instance attached to this session.
    /// </summary>
    /// <returns>
    /// An <see cref="ISymmetricCryptoProvider"/> instance populated with the key values
    /// as defined on the session instance.
    /// </returns>
    ISymmetricCryptoProvider? GetSymmetricCryptoProvider();
    #endregion
}
