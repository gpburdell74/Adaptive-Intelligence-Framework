using Adaptive.Intelligence.Shared;
using Adaptive.SecureApi.Cryptography;
using Adaptive.SecureApi.Sessions;

namespace Adaptive.SecureApi;

/// <summary>
/// Contains the data for the secure session for the server.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public class SecureSession : DisposableObjectBase, ISecureSession
{
    #region Private Member Declarations    
    /// <summary>
    /// The asymmetric cryptographic provider instance.
    /// </summary>
    private IAsymmetricCryptoProvider? _asymmetricProvider;
    /// <summary>
    /// The symmetric cryptographic provider instance.
    /// </summary>
    private ISymmetricCryptoProvider? _provider;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="SecureSession"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public SecureSession()
    {

    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            SessionId = Guid.Empty;

            ByteArrayUtil.Clear(PrimaryAesKey);
            ByteArrayUtil.Clear(SecondaryAesKey);
            ByteArrayUtil.Clear(TertiaryAesKey);
        }

        PrimaryRsaPublicKey = null;
        SecondaryRsaPublicKey = null;
        TertiaryRsaPublicKey = null;
        PrimaryAesKey = null;
        SecondaryAesKey = null;
        TertiaryAesKey = null;

        _provider = null;
        _asymmetricProvider = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the session identifier.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> specifying the session ID value.
    /// </value>
    public Guid SessionId { get; set; }

    #region RSA Keys
    /// <summary>
    /// Gets or sets the primary RSA public key.
    /// </summary>
    /// <value>
    /// A string containing the primary RSA public key.
    /// </value>
    public string? PrimaryRsaPublicKey { get; set; }
    /// <summary>
    /// Gets or sets the secondary RSA public key.
    /// </summary>
    /// <value>
    /// A string containing the primary RSA public key.
    /// </value>
    public string? SecondaryRsaPublicKey { get; set; }
    /// <summary>
    /// Gets or sets the tertiary RSA public key.
    /// </summary>
    /// <value>
    /// A string containing the primary RSA public key.
    /// </value>
    public string? TertiaryRsaPublicKey { get; set; }
    #endregion

    #region AES Keys    
    /// <summary>
    /// Gets or sets the primary AES key value.
    /// </summary>
    /// <value>
    /// A byte array containing the primary AES key data, or <b>null</b>.
    /// </value>
    public byte[]? PrimaryAesKey { get; set; }
    /// <summary>
    /// Gets or sets the secondary AES key value.
    /// </summary>
    /// <value>
    /// A byte array containing the primary AES key data, or <b>null</b>.
    /// </value>
    public byte[]? SecondaryAesKey { get; set; }
    /// <summary>
    /// Gets or sets the tertiary AES key value.
    /// </summary>
    /// <value>
    /// A byte array containing the primary AES key data, or <b>null</b>.
    /// </value>
    public byte[]? TertiaryAesKey { get; set; }
    Guid? ISecureSession.SessionId { get; set; }
    #endregion

    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Gets the asymmetric crypto provider instance attached to this session.
    /// </summary>
    /// <returns>
    /// An <see cref="IAsymmetricCryptoProvider" /> instance populated with the key values
    /// as defined on the session instance.
    /// </returns>
    public virtual IAsymmetricCryptoProvider? GetAsymmetricCryptoProvider()
    {
        if (_asymmetricProvider == null && 
            PrimaryRsaPublicKey != null &&
            SecondaryRsaPublicKey != null &&
            TertiaryRsaPublicKey != null)
        {
            _asymmetricProvider = new AsymmetricCryptoProvider(
                new List<byte[]>
                {
                        Convert.FromBase64String(PrimaryRsaPublicKey),
                        Convert.FromBase64String(SecondaryRsaPublicKey),
                        Convert.FromBase64String(TertiaryRsaPublicKey)
                });
        }
        return _asymmetricProvider;
    }
    /// <summary>
    /// Gets the crypto provider instance attached to this session.
    /// </summary>
    /// <returns>
    /// An <see cref="ISymmetricCryptoProvider"/> instance populated with the key values
    /// as defined on the session instance.
    /// </returns>
    public virtual ISymmetricCryptoProvider? GetSymmetricCryptoProvider()
    {
        if (_provider == null && PrimaryAesKey != null && SecondaryAesKey != null && TertiaryAesKey != null)
        {
            _provider = new SymmetricCryptoProvider(
                new List<byte[]>
                {
                        PrimaryAesKey,
                        SecondaryAesKey,
                        TertiaryAesKey
                });
        }
        return _provider;
    }

   #endregion
}
