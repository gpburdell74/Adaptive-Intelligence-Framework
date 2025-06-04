using Adaptive.SecureApi.Cryptography;
using Adaptive.SecureApi.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Adaptive.SecureApi.Server;

/// <summary>
/// An <see cref="SecureOkObjectResult"/> that when executed performs content negotiation, formats the entity body, and
/// will produce a <see cref="StatusCodes.Status200OK"/> response if negotiation and formatting succeed.  This class
/// also ensures the data content of the body is encrypted using the provided keys from the key exchange process.
/// </summary>
[DefaultStatusCode(DefaultStatusCode)]
public sealed class SecureOkObjectResult : OkObjectResult, IDisposable
{
    #region Private Member Declarations
    /// <summary>
    /// The default status code
    /// </summary>
    private const int DefaultStatusCode = StatusCodes.Status200OK;
    /// <summary>
    /// The cryptographic  provider instance.
    /// </summary>
    private ISymmetricCryptoProvider? _provider;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="OkObjectResult"/> class.
    /// </summary>
    /// <param name="value">The content to format into the entity body.</param>
    public SecureOkObjectResult(ISymmetricCryptoProvider provider, object? value)
        : base(value)
    {
        _provider = provider;
        StatusCode = DefaultStatusCode;
        Formatters.Clear();
        Formatters.Add(new SecureResultFormatter(provider));
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public void Dispose()
    {
        _provider = null;
        GC.SuppressFinalize(this);
    }
    #endregion
}