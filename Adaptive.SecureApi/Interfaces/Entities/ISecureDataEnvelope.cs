using System.Text.Json.Serialization;

namespace Adaptive.SecureApi;

/// <summary>
/// Provides the signature definition for the outer data entity that contains the JSON content
/// to be sent after encryption has been applied between the Secure API Server and Client.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ISecureDataEnvelope : IClearDataEnvelope
{
    /// <summary>
    /// Gets or sets the application token content.
    /// </summary>
    /// <remarks>
    /// This property contains the byte array containing the application authorization token
    /// that is generate after all the key exchange and encryption handshakes are complete.
    /// </remarks>
    /// <value>
    /// A byte array containing the application token value, or <b>null</b>.
    /// </value>
    [JsonPropertyName("appToken")]
    byte[]? AppToken { get; set; }
}
