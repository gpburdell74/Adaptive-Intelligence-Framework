using Adaptive.Intelligence.Shared;
using Adaptive.SecureApi.Cryptography;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace Adaptive.SecureApi.Server;

/// <summary>
/// Provides a data formatter to ensure data content is encrypted for the <see cref="SecureOkObjectResult"/> class.
/// </summary>
/// <seealso cref="OutputFormatter" />
public sealed class SecureResultFormatter : OutputFormatter, IDisposable
{
    #region Private Member Declarations    
    /// <summary>
    /// The expected media type for this type of data (encrypted JSON content).
    /// </summary>
    private const string ExpectedMediaType = "application/json";

    /// <summary>
    /// The cryptographic provider instance.
    /// </summary>
    private ISymmetricCryptoProvider? _provider;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="SecureResultFormatter"/> class.
    /// </summary>
    /// <param name="provider">
    /// The reference to the cryptographic provider instance.
    /// </param>
    public SecureResultFormatter(ISymmetricCryptoProvider provider)
    {
        _provider = provider;
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ExpectedMediaType));
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        _provider = null;
        GC.SuppressFinalize(this);
    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Returns a value indicating whether or not the given type can be written by this serializer.
    /// </summary>
    /// <param name="type">The object type.</param>
    /// <returns>
    ///   <c>true</c> if the type can be written, otherwise <c>false</c>.
    /// </returns>
    protected override bool CanWriteType(Type? type)
    {
        if (type == null)
            return false;

        return true;
    }

    /// <summary>
    /// Writes the response body.
    /// </summary>
    /// <param name="context">
    /// The <see cref="OutputFormatterWriteContext"/> containing the formatter context associated with the call.
    /// </param>
    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
    {
        // Serialize the data being returned to JSON format.
        string json = JsonSerializer.Serialize(context.Object, new JsonSerializerOptions
        {
            WriteIndented = false
        });

        if (_provider != null)
        {
            // Encrypt the JSON Text content.
            byte[] data = System.Text.Encoding.ASCII.GetBytes(json);
            byte[]? encrypted = _provider.EncryptAll(data);

            // Write the bytes to the output stream.
            Stream outStream = context.HttpContext.Response.Body;
            await outStream.WriteAsync(encrypted).ConfigureAwait(false);
            await outStream.FlushAsync().ConfigureAwait(false);

            ByteArrayUtil.Clear(data);
            ByteArrayUtil.Clear(encrypted);
        }
    }
    #endregion
}
