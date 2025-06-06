using System.Text.Json.Serialization;

namespace Adaptive.Intelligence.SecureApi;

/// <summary>
/// Provides the signature definition for the outer data entity that contains the JSON content
/// to be sent in the clear between the Secure API Server and Client.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IClearDataEnvelope : IDisposable
{
    /// <summary>
    /// Gets or sets the reference to the data content.
    /// </summary>
    /// <value>
    /// A byte array containing the actual or serialized data content.
    /// </value>
    [JsonPropertyName("data")]
    byte[]? Data { get; set; }

    /// <summary>
    /// Gets or sets the session ID value.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> containing the session ID value.
    /// </value>
    [JsonPropertyName("sessionId")]
    Guid? SessionId { get; set; }
}
