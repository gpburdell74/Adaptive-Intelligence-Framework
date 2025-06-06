using Adaptive.Intelligence.SecureApi.Cryptography;
using Adaptive.Intelligence.SecureApi.Sessions;
using Adaptive.Intelligence.SecureApi.Tokens;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;
using Adaptive.SecureApi.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Adaptive.Intelligence.SecureApi.Server;

/// <summary>
/// Provides a base definition for creating Azure Functions that execute in the non-secured area of the API.
/// </summary>
/// <seealso cref="ClearFunctionBase{T}" />
public abstract class SecuredFunctionBase<T> : ClearFunctionBase<T>
{
    #region Private Member Declarations

    /// <summary>
    /// The x API session header identifier.
    /// </summary>
    private const string XApiSessionId = "x-AI-SessionId";

    /// <summary>
    /// The session instance.
    /// </summary>
    private ISecureSession? _session;

    /// <summary>
    /// The repository instance for sessions.
    /// </summary>
    private ISessionRepository _sessionRepository;

    /// <summary>
    /// The repository instance for app auth tokens.
    /// </summary>
    private IApplicationTokenRepository _tokenRepository;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="SecuredFunctionBase{T}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance provided by the system.
    /// </param>
    /// <param name="service">
    /// The session repository service provided by the system.
    /// </param>
    public SecuredFunctionBase(ILogger<T> logger,
                ISessionRepositoryService sessionService,
        IApplicationTokenRepositoryService? tokenService)
        : base(logger, sessionService, tokenService)

    {
        _sessionRepository = SessionService.CreatNewRepositoryInstanceAsync().Result;
        _tokenRepository = TokenService.CreateNewRepositoryInstanceAsync().Result;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        SessionService.DeleteRepositoryInstanceAsync(_sessionRepository);
        TokenService.DeleteRepositoryInstanceAsync(_tokenRepository);

        _sessionRepository = null;
        _tokenRepository = null;

        _session?.Dispose();
        _session = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Protected Properties    
    /// <summary>
    /// Gets the reference to the session object.
    /// </summary>
    /// <value>
    /// The <see cref="ISecureSession"/> instance.
    /// </value>
    protected ISecureSession? Session => _session;
    #endregion

    #region Protected Methods / Functions
    /// <summary>
    /// Attempts to load the session data, if any is present.
    /// </summary>
    /// <param name="request">
    /// The <see cref="HttpRequest"/> instance that was received.
    /// </param>
    /// <returns>
    /// An <see cref="IOperationalResult"/> instance containing the result of the operation.
    /// </returns>
    protected virtual async Task<IOperationalResult> LoadSessionAsync(HttpRequest request)
    {
        OperationalResult result = new OperationalResult();

        _session?.Dispose();
        _session = null;

        Guid? sessionId = ReadSessionIdFromHeaders(request);
        if (sessionId != null && _sessionRepository != null)
        {
            _session = await _sessionRepository.GetSessionAsync(sessionId.Value).ConfigureAwait(false);
            result.Success = (_session != null);
        }

        return result;

    }

    /// <summary>
    /// Attempts to execute the specified function.
    /// </summary>
    /// <typeparam name="T">
    /// The data type the specified <see cref="Func"/> will return.
    /// </typeparam>
    /// <param name="func">The function.</param>
    /// <returns>
    /// The <see cref="IActionResult"/> containing the result of the operation.
    /// </returns>
    protected virtual async Task<IActionResult> TryExecute<T>(Func<Task<T>> func)
    {
        IActionResult result;
        try
        {
            T operationResult = await func().ConfigureAwait(false);
            ISymmetricCryptoProvider? provider = _session!.GetSymmetricCryptoProvider();
            if (provider != null)
                result = new SecureOkObjectResult(provider, operationResult);
            else
                result = new SecurityErrorResult("Cryptographic Error.");
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
            result = new BadRequestResult();
        }
        return result;
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Attempts to read the session identifier from the request header(s).
    /// </summary>
    /// <param name="request">
    /// The <see cref="HttpRequest"/> instance that was received.
    /// </param>
    /// <returns>
    /// A <see cref="Guid"/> containing the session ID value, if present; otherwise,
    /// returns <b>null</b>.
    /// </returns>
    private static Guid? ReadSessionIdFromHeaders(HttpRequest request)
    {
        Guid? sessionId = null;

        if (request.Headers.ContainsKey(XApiSessionId))
        {
            string sessionIdAsString = request.Headers[XApiSessionId].ToString();
            if (Guid.TryParse(sessionIdAsString, out Guid id))
            {
                sessionId = id;
            }
        }
        return sessionId;
    }
    /// <summary>
    /// Attempts to read the secured content from the HTTP request body.
    /// </summary>
    /// <param name="request">
    /// The <see cref="HttpRequest"/> instance to read from.
    /// </param>
    /// <returns>
    /// An <see cref="ClearDataEntity"/> instance containing the decrypted data.
    /// </returns>
    protected virtual async Task<ISecureDataEnvelope?> ReadContentAsync(HttpRequest request)
    {
        ISecureDataEnvelope? dto = null;

        // Read the encrypted content.
        if (_session != null)
        {
            ISymmetricCryptoProvider? provider = _session.GetSymmetricCryptoProvider();
            if (provider != null)
            {
                try
                {
                    // Read the request body, decrypt the content, and instantiate the object.
                    dto = await request.ReadFromSecureJsonAsync<ISecureDataEnvelope>(provider).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }
        }
        return dto;
    }
    /// <summary>
    /// Verifies that the provided application token is valid.
    /// </summary>
    /// <param name="entity">
    /// The <see cref="ISecureDataEnvelope"/> containing the client's message.
    /// </param>
    /// <returns>
    /// An <see cref="IOperationalResult"/> instance containing the result of the operation.
    /// </returns>
    protected virtual async Task<IOperationalResult> VerifyAppTokenAsync(ISecureDataEnvelope? entity)
    {
        OperationalResult result = new OperationalResult();

        if (entity == null)
        {
            result.Success = false;
            result.Message = "Application Token is missing";
        }
        else if (_sessionRepository == null || _tokenRepository == null)
        {
            result.Success = false;
            result.Message = "Service is unavailable.";
        }
        else if (_session == null)
        {
            result.Success = false;
            result.Message = "No secure application session has been established.";
        }
        else
        {
            try
            {
                bool verified = await _tokenRepository.VerifyAppAuthorizationTokenAsync(_session.SessionId.Value, entity.AppToken).ConfigureAwait(false);
                if (!verified)
                {
                    result.Success = false;
                    result.Message = "Application Token is invalid.";
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
                result.Success = false;
                result.Message = ex.Message;
            }
        }
        return result;
    }
    #endregion
}