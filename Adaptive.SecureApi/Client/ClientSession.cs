using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Security;
using Adaptive.SecureApi.Cryptography;

namespace Adaptive.SecureApi.Client;

/// <summary>
/// Represents and manages the client session data for the Secure API client.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public class ClientSession : DisposableObjectBase, IClientSession
{
    #region Private Member Declarations

    private Guid? _sessionId;
    private SecureByteArray? _appToken;
    private SecureByteArray? _primaryRsaPublicKey;
    private SecureByteArray? _secondaryRsaPublicKey;
    private SecureByteArray? _tertiaryRsaPublicKey;
    private SecureByteArray? _primaryAesKey;
    private SecureByteArray? _secondaryAesKey;
    private SecureByteArray? _tertiaryAesKey;

    private ISymmetricCryptoProvider? _cryptoProvider;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientSession"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public ClientSession()
    {        
        _sessionId = null;
        _appToken = null;
        
        _primaryRsaPublicKey = null;
        _secondaryRsaPublicKey = null;
        _tertiaryRsaPublicKey = null;

        _primaryAesKey = null;
        _secondaryAesKey = null;
        _tertiaryAesKey = null;
        _cryptoProvider = null;
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
            _appToken?.Dispose();
            _primaryRsaPublicKey?.Dispose();
            _secondaryRsaPublicKey?.Dispose();
            _tertiaryRsaPublicKey?.Dispose();
            _primaryAesKey?.Dispose();
            _secondaryAesKey?.Dispose();
            _tertiaryAesKey?.Dispose();
            _cryptoProvider?.Dispose();
        }

        _sessionId = null;
        _appToken = null;
        _primaryRsaPublicKey = null;
        _secondaryRsaPublicKey = null;
        _tertiaryRsaPublicKey = null;
        _primaryAesKey = null;
        _secondaryAesKey = null;
        _tertiaryAesKey = null;
        _cryptoProvider = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the primary symmetric cryptographic key.
    /// </summary>
    /// <value>
    /// A byte array containing the primary cryptographic key, or <b>null</b>.
    /// </value>
    public byte[]? PrimarySymmetricKey
    {
        get
        {
            return _primaryAesKey?.Value;
        }
        set
        {
            if (value == null)
            {
                _primaryAesKey?.Dispose();
                _primaryAesKey = null;
            }
            else
            {
                if (_primaryAesKey == null)
                    _primaryAesKey = new SecureByteArray(value);
                else
                    _primaryAesKey.Value = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the secondary symmetric cryptographic key.
    /// </summary>
    /// <value>
    /// A byte array containing the seconadary cryptographic key, or <b>null</b>.
    /// </value>
    public byte[]? SecondarySymmetricKey
    {
        get
        {
            return _secondaryAesKey?.Value;
        }
        set
        {
            if (value == null)
            {
                _secondaryAesKey?.Dispose();
                _secondaryAesKey = null;
            }
            else
            {
                if (_secondaryAesKey == null)
                    _secondaryAesKey = new SecureByteArray(value);
                else
                    _secondaryAesKey.Value = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the tertiary symmetric cryptographic key.
    /// </summary>
    /// <value>
    /// A byte array containing the tertiary cryptographic key, or <b>null</b>.
    /// </value>
    public byte[]? TertiarySymmetricKey
    {
        get
        {
            return _tertiaryAesKey?.Value;
        }
        set
        {
            if (value == null)
            {
                _tertiaryAesKey?.Dispose();
                _tertiaryAesKey = null;
            }
            else
            {
                if (_tertiaryAesKey == null)
                    _tertiaryAesKey = new SecureByteArray(value);
                else
                    _tertiaryAesKey.Value = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the primary RSA public key.
    /// </summary>
    /// <value>
    /// A byte array containbing the primary RSA public key, or <b>null</b>.
    /// </value>
    public byte[]? PrimaryRsaPublicKey
    {
        get
        {
            return _primaryRsaPublicKey?.Value;
        }
        set
        {
            if (value == null)
            {
                _primaryRsaPublicKey?.Dispose();
                _primaryRsaPublicKey = null;
            }
            else
            {
                if (_primaryRsaPublicKey == null)
                    _primaryRsaPublicKey = new SecureByteArray(value);
                else
                    _primaryRsaPublicKey.Value = value;
            }
        }
    }
    /// <summary>
    /// Gets or sets the primary RSA public key.
    /// </summary>
    /// <value>
    /// A byte array containbing the primary RSA public key, or <b>null</b>.
    /// </value>
    public byte[]? SecondaryRsaPublicKey
    {
        get
        {
            return _secondaryRsaPublicKey?.Value;
        }
        set
        {
            if (value == null)
            {
                _secondaryRsaPublicKey?.Dispose();
                _secondaryRsaPublicKey = null;
            }
            else
            {
                if (_secondaryRsaPublicKey == null)
                    _secondaryRsaPublicKey = new SecureByteArray(value);
                else
                    _secondaryRsaPublicKey.Value = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the primary RSA public key.
    /// </summary>
    /// <value>
    /// A byte array containbing the primary RSA public key, or <b>null</b>.
    /// </value>
    public byte[]? TertiaryRsaPublicKey
    {
        get
        {
            return _tertiaryRsaPublicKey?.Value;
        }
        set
        {
            if (value == null)
            {
                _tertiaryRsaPublicKey?.Dispose();
                _tertiaryRsaPublicKey = null;
            }
            else
            {
                if (_tertiaryRsaPublicKey == null)
                    _tertiaryRsaPublicKey = new SecureByteArray(value);
                else
                    _tertiaryRsaPublicKey.Value = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the session identifier.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the session identifier, or <b>null</b> if not set.
    /// </value>
    public Guid? SessionId
    {
        get => _sessionId;
        set => _sessionId = value;
    }
    
    #endregion
    #region
    #endregion

    #region
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Creates the symmetric cryptographic provider instance from the local key data.
    /// </summary>
    /// <returns>
    /// The <see cref="ISymmetricCryptoProvider"/> instance if successful, or <b>null</b> if the keys are not set.
    /// </returns>
    public ISymmetricCryptoProvider? CreateCryptoProvider()
    {
        if (_primaryAesKey == null || _secondaryAesKey == null || _tertiaryAesKey == null)
        {
            return null; // Keys are not set, cannot create provider.
        }

        return new SymmetricCryptoProvider(
            new List<byte[]>
                {
                    _primaryAesKey.Value!,
                    _secondaryAesKey.Value!,
                    _tertiaryAesKey.Value!
                });
    }
    #endregion

    #region
    #endregion

}
