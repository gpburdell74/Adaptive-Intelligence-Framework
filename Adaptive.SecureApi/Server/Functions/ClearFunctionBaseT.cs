using Adaptive.Intelligence.Shared;
using Adaptive.SecureApi.Server;
using Microsoft.Extensions.Logging;

namespace Adaptive.Intelligence.SecureApi.Server;

/// <summary>
/// Provides a base definition for creating Azure Functions that execute in the non-secured area of the API.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public abstract class ClearFunctionBase<T> : DisposableObjectBase
{
    #region Private Member Declarations
    /// <summary>
    /// The logger instance.
    /// </summary>
    private ILogger<T>? _logger;

    /// <summary>
    /// The repository service instance.
    /// </summary>
    private ISessionRepositoryService? _sessionService;

    /// <summary>
    /// The repository service instance.
    /// </summary>
    private IApplicationTokenRepositoryService? _tokenService;

    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="ClearFunctionBase"/> class.
    /// </summary>
    /// <param name="logger">
    /// The <see cref="ILogger"/> instance to be used for logging.
    /// </param>
    /// <param name="sessionService">
    /// The <see cref="ISessionRepositoryService"/> service instance used to create session
    /// repository instances.
    /// </param>
    /// <param name="tokenService">
    /// The <see cref="IApplicationTokenRepositoryService"/> service instance used to create
    /// application authorization token repository instances.
    /// </param>
    protected ClearFunctionBase(ILogger<T> logger, 
        ISessionRepositoryService sessionService, 
        IApplicationTokenRepositoryService? tokenService)
    {
        _logger = logger;
        _sessionService = sessionService;
        _tokenService = tokenService;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _logger = null;
        _sessionService = null;
        _tokenService = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Protected Properties    
    /// <summary>
    /// Gets the reference to the logger instance.
    /// </summary>
    /// <value>
    /// The <see cref="ILogger{T}"/> provided by the system.
    /// </value>
    protected ILogger<T>? Logger => _logger;

    /// <summary>
    /// Gets the reference to the session repository instance.
    /// </summary>
    /// <value>
    /// The <see cref="ISessionRepositoryService"/> provided by the system.
    /// </value>
    protected ISessionRepositoryService? SessionService => _sessionService;

    /// <summary>
    /// Gets the reference to the session repository instance.
    /// </summary>
    /// <value>
    /// The <see cref="ISessionRepositoryService"/> provided by the system.
    /// </value>
    protected IApplicationTokenRepositoryService? TokenService => _tokenService;

    #endregion

    #region Protected Methods / Functions    
    /// <summary>
    /// Logs the information.
    /// </summary>
    /// <param name="data">
    /// A string containing the data to be logged.
    /// </param>
    protected virtual void LogInfo(string? data)
    {
        if (_logger != null && data != null)
            _logger.LogInformation(data);
    }
    #endregion
}