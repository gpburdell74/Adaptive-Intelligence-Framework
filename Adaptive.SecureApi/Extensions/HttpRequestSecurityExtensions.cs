using Adaptive.Intelligence.SecureApi.Cryptography;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Adaptive.SecureApi.Server;

/// <summary>
/// Provides extension methods for the HttpRequest object.
/// </summary>
public static class HttpRequestSecurityExtensions
{
    /// <summary>
    /// Reads the encrypted JSON data content from the request.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="request">The request.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public static async Task<TValue?> ReadFromSecureJsonAsync<TValue>(this HttpRequest request, ISymmetricCryptoProvider provider, CancellationToken cancellationToken = default)
    {
        TValue? returnData = default(TValue);

        // Create a copy of the original body content.
        MemoryStream? sourceStream = await CopyBodyStreamAsync(request).ConfigureAwait(false);
        if (sourceStream != null)
        {
            // Attempt to read all the bytes.
            byte[]? encrypted = ReadStream(sourceStream);

            // If successful, decrypt and translate.
            if (encrypted != null)
            {
                returnData = DecryptAndInstantiate<TValue>(encrypted, provider);
                ByteArrayUtil.Clear(encrypted);
            }
            sourceStream?.Dispose();
        }
        return returnData;
    }

    #region Private Methods / Functions    
    /// <summary>
    /// Creates a copy of the request's body stream.
    /// </summary>
    /// <param name="request">
    /// The <see cref="HttpRequest"/> instance to read from.
    /// </param>
    /// <returns>
    /// A new <see cref="MemoryStream"/> instance if successful; otherwise,
    /// returns <b>null</b>.
    /// </returns>
    private static async Task<MemoryStream?> CopyBodyStreamAsync(HttpRequest request)
    {
        // Create a copy of the original body content.
        MemoryStream? sourceStream = null;
        try
        {
            sourceStream = new MemoryStream((int)request.ContentLength);
            await request.Body.CopyToAsync(sourceStream).ConfigureAwait(false);
            sourceStream.Seek(0, SeekOrigin.Begin);
        }
        catch (Exception ex)
        {
            sourceStream = null;
            ExceptionLog.LogException(ex);
        }

        return sourceStream;
    }
    /// <summary>
    /// Reads the stream.
    /// </summary>
    /// <param name="sourceStream">The source stream.</param>
    /// <returns>
    /// A byte array containing the content from the stream, or <b>null</b>.
    /// </returns>
    private static byte[]? ReadStream(Stream sourceStream)
    {
        // Attempt to read all the bytes.
        byte[]? encrypted = null;
        BinaryReader? reader = null;
        try
        {
            long? length = sourceStream.Length;
            reader = new BinaryReader(sourceStream);
            encrypted = reader.ReadBytes((int)length);
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
        }
        finally
        {
            reader?.Dispose();
        }
        return encrypted;
    }
    /// <summary>
    /// Decrypts the provided array and instantiates the object instance.
    /// </summary>
    /// <typeparam name="TValue">
    /// The data type of the value to return.
    /// </typeparam>
    /// <param name="encrypted">
    /// A byte array containing the encrypted data.
    /// </param>
    /// <param name="provider">
    /// The <see cref="ISymmetricCryptoProvider"/> instance used for decryption.
    /// </param>
    /// <returns>
    /// The new <typeparamref name="TValue"/> instance, or <b>null</b>.
    /// </returns>
    private static TValue? DecryptAndInstantiate<TValue>(byte[]? encrypted, ISymmetricCryptoProvider provider)
    {
        TValue? returnData = default(TValue);

        // If successful, decrypt and translate.
        if (encrypted != null)
        {
            byte[]? clearData = provider.DecryptAll(encrypted);
            if (clearData != null)
            {
                string json = System.Text.Encoding.ASCII.GetString(clearData!);
                returnData = JsonSerializer.Deserialize<TValue>(json);
                ByteArrayUtil.Clear(clearData);
            }
        }
        return returnData;
    }
    #endregion
}