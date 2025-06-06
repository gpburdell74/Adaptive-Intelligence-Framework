using Adaptive.Intelligence.SecureApi;
using Adaptive.Intelligence.SecureApi.Server;
using Adaptive.Intelligence.SecureApi.Sessions;
using Adaptive.Intelligence.SecureApi.Tokens;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Adaptive.SecureApi.Server;

/// <summary>
/// Provides the wrapper class for the Clear API functions for session creation and key exchange.
/// </summary>
public class ClearApiFunctions : ClearFunctionBase<ClearApiFunctions>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StartSession"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="service">The service.</param>
    public ClearApiFunctions(ILogger<ClearApiFunctions> logger, 
        ISessionRepositoryService sessionService,
        IApplicationTokenRepositoryService? service)
        : base(logger, sessionService, service)
    {
    }

    /// <summary>
    /// Provides the function to start a new client session.
    /// </summary>
    /// <param name="req">
    /// The <see cref="HttpRequest"/> instance containing the client request.
    /// </param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the result of the operation.  If successful, this returns
    /// an <see cref="OkObjectResult"/> instance containing a <see cref="ClearDataEntity"/> which itself
    /// contains the new session ID and the primary RSA public key value.
    /// </returns>
    [Function("StartSession")]
    public async Task<IActionResult> StartSessionFunction([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        LogInfo("StartSession Function Started.");

        // Create the new session record.
        ISessionRepository repository = await SessionService.CreatNewRepositoryInstanceAsync().ConfigureAwait(false);
        ISecureSession newSession = await repository.CreateNewSessionAsync().ConfigureAwait(false);

        // Generate the RSA key data and store it locally.
        RsaProvider provider = new RsaProvider();
        newSession.PrimaryRsaPublicKey = provider.GetPrivateKeyValueForStorage();

        // Prepare the response message.
        ClearDataEnvelope entity = new ClearDataEnvelope
        {
            Data = provider.GetKeyValueForExportAsByteArray(),
            SessionId = newSession.SessionId
        };

        // Save the private key to the server session, and the public key to the client session.
        await repository.UpdateSessionAsync(newSession).ConfigureAwait(false);

        // Clear.
        provider.Dispose();
        await SessionService.DeleteRepositoryInstanceAsync(repository).ConfigureAwait(false);

        // Return the client data.
        return new OkObjectResult(entity);
    }

    /// <summary>
    /// Provides the function to perform the first key exchange in the process.
    /// </summary>
    /// <param name="req">
    /// The <see cref="HttpRequest"/> instance containing the client request.
    /// </param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the result of the operation.  If successful, this returns
    /// an <see cref="OkObjectResult"/> instance containing a <see cref="ClearDataEntity"/> which itself
    /// contains the new session ID and the primary RSA public key value.
    /// </returns>
    [Function("PrimaryKex")]
    public async Task<IActionResult> PrimaryKexFunction([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        LogInfo("Primary Key Exchange Started.");

        // Read the user-specified data.
        ClearDataEnvelope? clientData = await req.ReadFromJsonAsync<ClearDataEnvelope>().ConfigureAwait(false);
        if (clientData == null)
            return new BadRequestObjectResult("No client data was specified.");

        // Read the stored session data.
        ISessionRepository repository = await SessionService!.CreatNewRepositoryInstanceAsync().ConfigureAwait(false);
        ISecureSession? session = await repository.GetSessionAsync(clientData.SessionId.Value).ConfigureAwait(false);
        if (session == null)
            return new SecurityErrorResult();

        // Create the RSA provider from the stored private key.
        RsaProvider provider = new RsaProvider();
        provider.SetPrivateKeyFromBase64String(session.PrimaryRsaPublicKey);

        // Decrypt the symmetric key sent by the client.
        byte[] primarySymmetricKey = provider.Decrypt(clientData.Data);
        provider.Dispose();

        // Store the data.
        session.PrimaryAesKey = primarySymmetricKey;

        // Create a new RSA Key exchange.
        provider = new RsaProvider();
        session.SecondaryRsaPublicKey = provider.GetPrivateKeyValueForStorage();
        byte[]? export = provider.GetKeyValueForExportAsByteArray();

        await repository.UpdateSessionAsync(session).ConfigureAwait(false);
        await SessionService.DeleteRepositoryInstanceAsync(repository).ConfigureAwait(false);
        provider.Dispose();

        ClearDataEnvelope response = new ClearDataEnvelope
        {
            SessionId = session.SessionId,
            Data = ByteArrayUtil.CopyToNewArray(export)
        };

        ByteArrayUtil.Clear(export);

        return new OkObjectResult(response);
    }

    /// <summary>
    /// Provides the function to perform the first key exchange in the process.
    /// </summary>
    /// <param name="req">
    /// The <see cref="HttpRequest"/> instance containing the client request.
    /// </param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the result of the operation.  If successful, this returns
    /// an <see cref="OkObjectResult"/> instance containing a <see cref="ClearDataEntity"/> which itself
    /// contains the new session ID and the primary RSA public key value.
    /// </returns>
    [Function("SecondaryKex")]
    public async Task<IActionResult> SecondaryKexFunction([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        LogInfo("Secondary Primary Key Started.");

        // Read the user-specified data.
        ClearDataEnvelope? clientData = await req.ReadFromJsonAsync<ClearDataEnvelope>().ConfigureAwait(false);
        if (clientData == null)
            return new BadRequestResult();

        // Read the stored session data.
        ISessionRepository repository = await SessionService.CreatNewRepositoryInstanceAsync().ConfigureAwait(false);
        ISecureSession? session = await repository.GetSessionAsync(clientData.SessionId.Value).ConfigureAwait(false);
        if (session == null)
            return new SecurityErrorResult();

        // Create the RSA provider from the stored private key.
        RsaProvider provider = new RsaProvider();
        provider.SetPrivateKeyFromBase64String(session.SecondaryRsaPublicKey);

        // Decrypt the symmetric key sent by the client.
        byte[] secondarySymmetricKey = provider.Decrypt(clientData.Data);
        provider.Dispose();

        // Store the data.
        session.SecondaryAesKey = secondarySymmetricKey;

        // Create a new RSA Key exchange.
        provider = new RsaProvider();
        session.TertiaryRsaPublicKey = provider.GetPrivateKeyValueForStorage();
        byte[]? export = provider.GetKeyValueForExportAsByteArray();
        await repository.UpdateSessionAsync(session).ConfigureAwait(false);

        provider.Dispose();

        ClearDataEnvelope response = new ClearDataEnvelope
        {
            SessionId = session.SessionId,
            Data = export
        };

        return new OkObjectResult(response);
    }

    /// <summary>
    /// Provides the function to perform the first key exchange in the process.
    /// </summary>
    /// <param name="req">
    /// The <see cref="HttpRequest"/> instance containing the client request.
    /// </param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the result of the operation.  If successful, this returns
    /// an <see cref="OkObjectResult"/> instance containing a <see cref="ClearDataEntity"/> which itself
    /// contains the new session ID and the primary RSA public key value.
    /// </returns>
    [Function("TertiaryKex")]
    public async Task<IActionResult> TertiaryKexFunction([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        LogInfo("Tertiary Key Exchange Started.");

        // Read the user-specified data.
        ClearDataEnvelope? clientData = await req.ReadFromJsonAsync<ClearDataEnvelope>().ConfigureAwait(false);
        if (clientData == null)
            return new BadRequestResult();

        // Read the stored session data.
        ISessionRepository repository = await SessionService.CreatNewRepositoryInstanceAsync().ConfigureAwait(false);   
        ISecureSession? session = await repository.GetSessionAsync(clientData.SessionId.Value).ConfigureAwait(false);
        if (session == null)
            return new SecurityErrorResult();

        // Create the RSA provider from the stored private key.
        RsaProvider provider = new RsaProvider();
        provider.SetPrivateKeyFromBase64String(session.TertiaryRsaPublicKey);

        // Decrypt the symmetric key sent by the client.
        byte[] tertiarySymmetricKey = provider.Decrypt(clientData.Data);
        provider.Dispose();

        // Store the data.
        session.TertiaryAesKey = tertiarySymmetricKey;
        await repository.UpdateSessionAsync(session).ConfigureAwait(false);

        // Create and store the authorization token.
        IApplicationTokenRepository? tokenRepository = await TokenService.CreateNewRepositoryInstanceAsync().ConfigureAwait(false);
        IApplicationAuthorizationToken? token = await tokenRepository.CreateAndStoreNewTokenAsync(
            session.SessionId.Value).ConfigureAwait(false);


        ClearDataEnvelope response = new ClearDataEnvelope
        {
            SessionId = session.SessionId,
            Data = token.CreateTokenData()
        };

        token?.Dispose();
        await TokenService.DeleteRepositoryInstanceAsync(tokenRepository).ConfigureAwait(false);
        await SessionService.DeleteRepositoryInstanceAsync(repository).ConfigureAwait(false);   

        return new OkObjectResult(response);
    }
}
