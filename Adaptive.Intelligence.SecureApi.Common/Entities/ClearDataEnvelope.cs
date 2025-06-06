using Adaptive.Intelligence.Shared;
using System.Text.Json.Serialization;

namespace Adaptive.Intelligence.SecureApi;

/// <summary>
/// Provides the base definition for communications entities that are transferred to and from a client
/// and are not encrypted.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public class ClearDataEnvelope : DisposableObjectBase, IClearDataEnvelope
{
    #region Private Member Declarations
    /// <summary>
    /// The data content.
    /// </summary>
    private byte[]? _data;

    /// <summary>
    /// The session ID value.
    /// </summary>
    private Guid? _sessionId;
    #endregion

    #region Constructor / Dispose Methods        
    /// <summary>
    /// Initializes a new instance of the <see cref="ClearDataEnvelope"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public ClearDataEnvelope()
    {
        _data = default;
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
            ByteArrayUtil.Clear(_data);
        }

        _data = default;
        _sessionId = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the reference to the data content.
    /// </summary>
    /// <value>
    /// The data content in the type of <typeparamref name="T"/>.
    /// </value>
    [JsonPropertyName("data")]
    public byte[]? Data
    {
        get => _data;
        set => _data = ByteArrayUtil.CopyToNewArray(value);
    }

    /// <summary>
    /// Gets or sets the session ID value.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> containing the session ID value.
    /// </value>
    [JsonPropertyName("sessionId")]
    public Guid? SessionId
    {
        get => _sessionId;
        set => _sessionId = value;
    }
    #endregion
}