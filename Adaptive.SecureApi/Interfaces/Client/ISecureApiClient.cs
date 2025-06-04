using Adaptive.Intelligence.Shared;

namespace Adaptive.SecureApi.Client;

/// <summary>
/// Provides the signature definition for classes that implement a secure API client interface.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ISecureApiClient : IDisposable
{
    #region Public Properties
    /// <summary>
    /// Gets a value indicating whether this instance is connected.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is connected; otherwise, <c>false</c>.
    /// </value>
    bool IsConnected { get; }
    #endregion

    #region Public Methods / Functions

    #region General Operations
    /// <summary>=
    /// Attempts to connect to the Secure API server, and performs the session initialization
    /// and key exchange process.
    /// </summary>
    /// <remarks>
    /// This method is resposible for calling the <see cref="StartSessionAsync(IOperationalResult)"/>,
    /// <see cref="PerformFirstKeyExchangeAsync(IOperationalResult, IClientSession)"/>,
    /// <see cref="PerformSecondaryKeyExchangeAsync(IOperationalResult, IClientSession)"/>, and
    /// <see cref="PerformTertiaryKeyExchangeAsync(IOperationalResult, IClientSession)"/> methods,
    /// providing an <see cref="IOperationalResult"/> instance to each method to contain the
    /// complete result of the operation.
    /// </remarks>
    /// <returns>
    /// An <see cref="IOperationalResult"/> indicating the success or failure of the 
    /// connection attempt.
    /// </returns>
    Task<IOperationalResult> ConnectAsync();

    /// <summary>
    /// Disconnects from the server.
    /// </summary>
    /// <returns>
    /// An <see cref="IOperationalResult"/> indicating the success or failure of the 
    /// disconnection attempt.
    /// </returns>
    Task<IOperationalResult> DisconnectAsync();
    #endregion

    #region Session and Key Exchange Operations 
    /// <summary>
    /// Starts the session by requesting a session start from the server.
    /// </summary>
    /// <param name="result">
    /// An <see cref="IOperationalResult"/> instance containing the result of
    /// the operation.
    /// </param>
    /// <returns>
    /// If successful, returns a new <see cref="IClientSession"/> instance that can be used for further operations;
    /// otherwise, returns <b>null</b> if the operation fails.
    /// </returns>
    Task<IClientSession?> StartSessionAsync(IOperationalResult result);

    /// <summary>
    /// Performs the first key exchange.
    /// </summary>
    /// <param name="result">
    /// An <see cref="IOperationalResult"/> instance containing the result of
    /// the operation.
    /// </param>
    /// <param name="session">
    /// The <see cref="IClientSession"/> session instance provided by <see cref="StartSessionAsync"/>.
    /// </param>
    Task PerformFirstKeyExchangeAsync(IOperationalResult result, IClientSession session);

    /// <summary>
    /// Performs the second key exchange.
    /// </summary>
    /// <param name="result">
    /// An <see cref="IOperationalResult"/> instance containing the result of
    /// the operation.
    /// </param>
    /// <remarks>
    /// This method must be invoked after <see cref="PerformFirstKeyExchangeAsync(IOperationalResult, IClientSession)"/>.
    /// </remarks>
    /// <param name="session">
    /// The <see cref="IClientSession"/> session instance provided by <see cref="StartSessionAsync"/>.
    /// </param>
    Task PerformSecondaryKeyExchangeAsync(IOperationalResult result, IClientSession session);

    /// <summary>
    /// Performs the third key exchange.
    /// </summary>
    /// <remarks>
    /// This method must be invoked after <see cref="PerformSecondaryKeyExchangeAsync(IOperationalResult, IClientSession)"/>.
    /// </remarks>
    /// <param name="result">
    /// An <see cref="IOperationalResult"/> instance containing the result of
    /// the operation.
    /// </param>
    /// <param name="session">
    /// The <see cref="IClientSession"/> session instance provided by <see cref="StartSessionAsync"/>.
    /// </param>
    Task PerformTertiaryKeyExchangeAsync(IOperationalResult result, IClientSession session);
    #endregion

    #region Http Operations
    /// <summary>
    /// Creates and returns the HTTP client instance to be used.
    /// </summary>
    /// <returns>
    /// An <see cref="IOperationalResult"/> containing the <see cref="HttpClient"/> instance if successful, 
    /// or <b>null</b> if the URI is not set or an error occurs.
    /// </returns>
    Task<HttpClient?> CreateClientAsync(IOperationalResult result);

    /// <summary>
    /// Sends the specified request.
    /// </summary>
    /// <param name="apiName">
    /// A string containing the name of the API.
    /// </param>
    /// <param name="entity">
    /// The <see cref="IClearDataEnvelope"/> entity to be sent.
    /// </param>
    /// <returns>
    /// The <see cref="HttpResponseMessage"/> instance containing the response data, or <b>null</b> if an error occurs."/>
    /// </returns>
    Task<HttpResponseMessage?> SendClearRequestAsync(IOperationalResult result, string apiName, IClearDataEnvelope entity);

    /// <summary>
    /// Performs the specified request.
    /// </summary>
    /// <param name="apiName">
    /// A string containing the name of the API.
    /// </param>
    /// <param name="entity">
    /// The <see cref="IClearDataEnvelope"/> entity to be sent, or <b>null</b> if there
    /// is no body to send.
    /// </param>
    /// <returns>
    /// The <see cref="HttpResponseMessage"/> instance containing the response data, or <b>null</b> if an error occurs."/>
    /// </returns>
    Task<HttpResponseMessage?> SendSecureRequestAsync(IOperationalResult result, string apiName, ISecureDataEnvelope? entity);

    /// <summary>
    /// Sends the HTTP message and gets the clear HTTP response.
    /// </summary>
    /// <param name="message">
    /// The <see cref="HttpResponseMessage"/> instance containing the request details.
    /// </param>
    /// <returns>
    /// A deserialized <see cref="ClearDataEnvelope"/> instance containing the response data, or <b>null</b> if an error occurs.
    /// </returns>
    Task<IClearDataEnvelope?> ReadClearResponseAsync(IOperationalResult result, HttpResponseMessage message);

    /// <summary>
    /// Sends the HTTP message and gets the clear HTTP response.
    /// </summary>
    /// <param name="message">
    /// The <see cref="HttpRequestMessage"/> instance containing the request details.
    /// </param>
    /// <returns>
    /// A deserialized <see cref="ClearDataEnvelope"/> instance containing the response data, or <b>null</b> if an error occurs.
    /// </returns>
    Task<ISecureDataEnvelope?> ReadSecureResponseAsync(IOperationalResult result, HttpRequestMessage message);
    #endregion

    #endregion
}
