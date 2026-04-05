using Adaptive.Intelligence.SecureApi.Cryptography;

namespace Adaptive.Intelligence.SecureApi.Sessions;

/// <summary>
/// Provides the signature definition for the client session data for the Secure API client.
/// </summary>
/// <remarks>
/// A Secure API session requires an exchange of three (3) assymetric public keys, and three
/// private symmetric key values.  Generally the implementation will be the key data for 
/// RSA in the assymetric implementation, and AES for the symmetric implementation.  
/// The session identifier is a <see cref="Guid"/> value that is used to correlate the session data 
/// on the client and server side, and is not used for any cryptographic purposes.
/// </remarks>
/// <seealso cref="IDisposable" />
public interface IClientSession : ISecureApiSession
{
}
