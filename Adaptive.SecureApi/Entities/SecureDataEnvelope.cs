using Adaptive.Intelligence.Shared;
using Adaptive.SecureApi.Entities;
using System.Text.Json.Serialization;

namespace Adaptive.SecureApi;

/// <summary>
/// Provides the base definition for communications entities that are transferred to and from a client
/// and are encrypted.
/// </summary>
/// <seealso cref="ClearDataEnvelope" />
/// <seealso cref="ISecureDataEnvelope"/>
public class SecureDataEnvelope : ClearDataEnvelope, ISecureDataEnvelope
{
    #region Private Member Declarations
    /// <summary>
    /// The application authorization token.
    /// </summary>
    private byte[]? _appToken;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="SecureDataEnvelope"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public SecureDataEnvelope() : base()
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
            ByteArrayUtil.Clear(_appToken);

        _appToken = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the application token content.
    /// </summary>
    /// <value>
    /// A byte array containing the application token value, or <b>null</b>.
    /// </value>
    [JsonPropertyName("appToken")]
    public byte[]? AppToken
    {
        get => _appToken;
        set
        {
            ByteArrayUtil.Clear(_appToken);
            if (value != null)
                _appToken = ByteArrayUtil.CopyToNewArray(value);
            else
                _appToken = null;
        }
    }
    #endregion
}