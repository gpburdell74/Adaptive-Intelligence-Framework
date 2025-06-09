using Adaptive.Intelligence.SecureApi.Cryptography;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;
using Adaptive.Intelligence.Shared.Security;
using System.Text;
using System.Text.Json;

namespace Adaptive.Intelligence.SecureApi.Client;

/// <summary>
/// Provides the client implementation for utilizing the Secure API.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public class SecureApiClient : DisposableObjectBase, ISecureApiClient
{
    #region Private Member Declarations

    #region Private Constants
    /// <summary>
    /// The API path string.
    /// </summary>
    private const string Api = "/api/";
    /// <summary>
    /// The start session API name.
    /// </summary>
    private const string StartSessionApi = "StartSession";
    /// <summary>
    /// The primary key exchange API name.
    /// </summary>
    private const string PrimaryKexApi = "PrimaryKex";
    /// <summary>
    /// The secondary key exchange API name.
    /// </summary>
    private const string SecondaryKexApi = "SecondaryKex";
    /// <summary>
    /// The tertiary key exchange API name.
    /// </summary>
    private const string TertiaryKexApi = "TertiaryKex";
    /// <summary>
    /// The media type json
    /// </summary>
    private const string MediaTypeJson = "application/json";
    #endregion

    /// <summary>
    /// The session instance.
    /// </summary>
    private IClientSession? _session;
    /// <summary>
    /// The RSA / assymmetric cryptographic provider instance.
    /// </summary>
    private IAsymmetricCryptoProvider? _rsa;
    /// <summary>
    /// The AES / symmetric cryptographic provider instance.
    /// </summary>
    private ISymmetricCryptoProvider? _aes;
    /// <summary>
    /// The HTTP client instance.
    /// </summary>
    private static HttpClient? _client;
    /// <summary>
    /// The URI of the base address of the Secure API Server to connect to.
    /// </summary>
    private static Uri? _uri;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="SecureApiClient"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public SecureApiClient()
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="SecureApiClient"/> class.
    /// </summary>
    /// <param name="uri">
    /// The string containing the absolute URI to connect to.
    /// </param>
    public SecureApiClient(string uri)
    {
        _uri = new Uri(uri, UriKind.Absolute);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="SecureApiClient"/> class.
    /// </summary>
    /// <param name="uri">
    /// A string containing the URI to connect to.
    /// </param>
    /// <param name="uriKind">
    /// A <see cref="UriKind"/> enumerated value indicating the kind of URI to create.
    /// </param>
    public SecureApiClient(string uri, UriKind uriKind)
    {
        _uri = new Uri(uri, uriKind);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="SecureApiClient"/> class.
    /// </summary>
    /// <param name="uri">
    /// The <see cref="Uri"/> instance to connect to.
    /// </param>
    public SecureApiClient(Uri uri)
    {
        _uri = uri;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _session?.Dispose();
            _client?.Dispose();
            _rsa?.Dispose();
            _aes?.Dispose();
        }

        _session = null;
        _client = null;
        _rsa = null;
        _uri = null;
        _aes = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets a value indicating whether this instance is connected.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is connected; otherwise, <c>false</c>.
    /// </value>
    public bool IsConnected => _session != null;
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
    public async Task<IOperationalResult> ConnectAsync()
    {
        OperationalResult result = new OperationalResult();

        // Start the session
        IClientSession? session = await StartSessionAsync(result).ConfigureAwait(false);
        if (session == null)
        {
            result.Success = false;
            result.Message = "Failed to start session.";
        }
        else
        {
            _session = session;
            // Perform the key exchanges
            try
            {
                await PerformFirstKeyExchangeAsync(result, session).ConfigureAwait(false);
                if (result.Success)
                {
                    await PerformSecondaryKeyExchangeAsync(result, session).ConfigureAwait(false);
                    if (result.Success)
                    {
                        await PerformTertiaryKeyExchangeAsync(result, session).ConfigureAwait(false);
                        if (!result.Success)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error occurred during key exchange: {ex.Message}";
                ExceptionLog.LogServerException(ex);
                result.AddException(ex);
            }
        }
        result.Success = true;

        return result;
    }

    /// <summary>
    /// Disconnects from the server.
    /// </summary>
    /// <returns>
    /// An <see cref="IOperationalResult"/> indicating the success or failure of the 
    /// disconnection attempt.
    /// </returns>
    public Task<IOperationalResult> DisconnectAsync()
    {
        IOperationalResult result = new OperationalResult();

        try
        {
            // Perform any necessary cleanup or disconnection logic here
            _session?.Dispose();
            _client?.Dispose();
            _session = null;
            _client = null;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"An error occurred while disconnecting: {ex.Message}";
            ExceptionLog.LogServerException(ex);
            result.AddException(ex);
        }
        return Task.FromResult(result);
    }
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
    public async Task<IClientSession?> StartSessionAsync(IOperationalResult result)
    {
        await CreateClientAsync(result).ConfigureAwait(false);
        if (result.Success)
        {
            HttpResponseMessage? response = await SendClearRequestAsync(result, StartSessionApi, null).ConfigureAwait(false);
            if (response != null)
            {
                IClearDataEnvelope? envelope = await ReadClearResponseAsync(
                    result, response).ConfigureAwait(false);
                if (envelope != null && envelope.SessionId.HasValue)
                {
                    // Start a new session and store the ID locally.
                    _session = new ClientSession();
                    _session.SessionId = envelope.SessionId;

                    // Store the first RSA public key value.
                    _session.PrimaryRsaPublicKey = envelope.Data;
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Failed to retrieve session ID or data from the server response.";
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Failed to send the session start request.";
            }
        }

        return _session;
    }
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
    public async Task PerformFirstKeyExchangeAsync(IOperationalResult result, IClientSession session)
    {
        if (_session == null)
            result.SetFailureMessage("The session is not initialized. Cannot continue.");
        else
        {
            List<byte[]?> resultList = await PerformKeyExchangeRequestAsync(result, PrimaryKexApi, _session.PrimaryRsaPublicKey)
                .ConfigureAwait(false);

            if (resultList != null && resultList.Count == 2)
            {
                _session.PrimarySymmetricKey = resultList[0];
                _session.SecondaryRsaPublicKey = resultList[1];
            }
            else
            {
                result.SetFailureMessage("The primary key exchange process failed.");
            }
        }
    }
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
    public async Task PerformSecondaryKeyExchangeAsync(IOperationalResult result, IClientSession session)
    {
        if (_session == null)
        {
            result.Success = false;
            result.Message = "The session is not initialized.  Cannot continue.";
        }
        else if (_session.PrimaryRsaPublicKey == null)
        {
            result.Success = false;
            result.Message = "The primary RSA public key is not set. Cannot perform key exchange.";
        }
        else
        {
            // Create or reference the cryptographic objects.
            RsaProvider? rsa = AsymmetricCryptoProviderFactory.CreateRsaProvider(_session.SecondaryRsaPublicKey);
            AesProvider? aes = SymmetricCryptoProviderFactory.CreateSymmetricProvider();

            if (rsa == null)
            {
                result.Success = false;
                result.Message = "Failed to create RSA provider from the primary public key.";
            }
            else if (aes == null)
            {
                result.Success = false;
                result.Message = "Failed to create AES provider.";
            }
            else
            {

                // Create the first AES key to use, and encrypt it with the RSA public key.
                aes.GenerateNewKey();
                byte[]? keyIv = aes.GetKeyData();
                byte[]? encData = rsa.Encrypt(keyIv);

                // Store the value locally.
                _session.SecondarySymmetricKey = keyIv;

                ClearDataEnvelope entity = new ClearDataEnvelope();
                entity.SessionId = _session.SessionId;
                entity.Data = encData;

                HttpResponseMessage? response = await SendClearRequestAsync(result, SecondaryKexApi, entity).ConfigureAwait(false);

                if (response != null)
                {
                    IClearDataEnvelope? responseData = await ReadClearResponseAsync(result, response).ConfigureAwait(false);
                    if (responseData != null)
                    {
                        _session.TertiaryRsaPublicKey = responseData.Data;
                        responseData.Dispose();
                    }
                    response.Dispose();
                }
                entity.Dispose();
            }
        }
    }
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
    public async Task PerformTertiaryKeyExchangeAsync(IOperationalResult result, IClientSession session)
    {
        if (_session == null)
        {
            result.Success = false;
            result.Message = "The session is not initialized.  Cannot continue.";
        }
        else if (_session.TertiaryRsaPublicKey == null)
        {
            result.Success = false;
            result.Message = "The tertiary RSA public key is not set. Cannot perform key exchange.";
        }
        else
        {
            // Create or reference the cryptographic objects.
            RsaProvider? rsa = AsymmetricCryptoProviderFactory.CreateRsaProvider(_session.TertiaryRsaPublicKey);
            AesProvider? aes = SymmetricCryptoProviderFactory.CreateSymmetricProvider();

            if (rsa == null)
            {
                result.Success = false;
                result.Message = "Failed to create RSA provider from the primary public key.";
            }
            else if (aes == null)
            {
                result.Success = false;
                result.Message = "Failed to create AES provider.";
            }
            else
            {

                // Create the first AES key to use, and encrypt it with the RSA public key.
                aes.GenerateNewKey();
                byte[]? keyIv = aes.GetKeyData();
                byte[]? encData = rsa.Encrypt(keyIv);

                // Store the value locally.
                _session.TertiarySymmetricKey = keyIv;

                ClearDataEnvelope entity = new ClearDataEnvelope();
                entity.SessionId = _session.SessionId;
                entity.Data = encData;

                HttpResponseMessage? response = await SendClearRequestAsync(result, TertiaryKexApi, entity).ConfigureAwait(false);

                if (response != null)
                {
                    IClearDataEnvelope? responseData = await ReadClearResponseAsync(result, response).ConfigureAwait(false);
                    if (responseData != null)
                    {
                        byte[] appAuthorizationToken = responseData.Data;
                        responseData.Dispose();
                    }
                    response.Dispose();
                }
                entity.Dispose();
            }
        }

    }
    #endregion

    #region Http Operations
    /// <summary>
    /// Creates and returns the HTTP client instance to be used.
    /// </summary>
    /// <returns>
    /// An <see cref="IOperationalResult"/> containing the <see cref="HttpClient"/> instance if successful, 
    /// or <b>null</b> if the URI is not set or an error occurs.
    /// </returns>
    public Task<HttpClient?> CreateClientAsync(IOperationalResult result)
    {
        _client?.Dispose();
        _client = null;

        if (_uri != null)
        {
            _client = new HttpClient();
            _client.BaseAddress = _uri;
            result.Success = true;
        }
        else
        {
            result.Success = false;
            result.Message = "The URI is not set. Please provide a valid URI to connect to the Secure API server.";
            _client = null;
        }

        return Task.FromResult(_client);
    }
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
    public async Task<HttpResponseMessage?> SendClearRequestAsync(IOperationalResult result, string apiName, IClearDataEnvelope? entity)
    {
        HttpResponseMessage? response = null;

        if (_client != null)
        {
            HttpRequestMessage? message = RenderClearRequest(apiName, entity);
            if (message != null)
            {
                string? json = SerializeClearDataEnvelope(entity);
                if (json != null)
                {
                    message.Content = new StringContent(json, Encoding.UTF8, MediaTypeJson);
                    try
                    {
                        response = await _client.SendAsync(message).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = $"An error occurred while sending the request: {ex.Message}";
                        ExceptionLog.LogServerException(ex);
                        result.AddException(ex);
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "Failed to serialize the entity to JSON.";
                }
                message.Dispose();
            }
            else
            {
                result.Success = false;
                result.Message = "Failed to create the HTTP request message.";
            }
        }
        else
        {
            result.Success = false;
            result.Message = "HTTP client is not initialized. Please call CreateClient() first.";
        }
        return response;
    }
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
    public async Task<HttpResponseMessage?> SendSecureRequestAsync(IOperationalResult result, string apiName, ISecureDataEnvelope? entity)
    {
        return null;
    }
    /// <summary>
    /// Sends the HTTP message and gets the clear HTTP response.
    /// </summary>
    /// <param name="message">
    /// The <see cref="HttpResponseMessage"/> instance containing the request details.
    /// </param>
    /// <returns>
    /// A deserialized <see cref="IClearDataEnvelope"/> instance containing the response data, or <b>null</b> if an error occurs.
    /// </returns>
    public async Task<IClearDataEnvelope?> ReadClearResponseAsync(IOperationalResult result, HttpResponseMessage message)
    {
        IClearDataEnvelope? returnMessage = null;

        if (!message.IsSuccessStatusCode)
        {
            result.Success = false;
            result.Message = $"HTTP request failed with status code: {message.StatusCode}";
        }
        else
        {
            try
            {
                string? responseJson = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
                message.Dispose();
                if (responseJson != null)
                {
                    returnMessage = JsonSerializer.Deserialize<ClearDataEnvelope>(responseJson);
                }
                else
                {
                    result.Success = false;
                    result.Message = "The response content is null or empty.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions related to the HTTP request
                ExceptionLog.LogServerException(ex);
                result.Success = false;
                result.Message = $"An error occurred while reading the response: {ex.Message}";
                result.AddException(ex);
            }
        }

        return returnMessage;
    }
    /// <summary>
    /// Sends the HTTP message and gets the clear HTTP response.
    /// </summary>
    /// <param name="client">
    /// The <see cref="HttpClient"/> instance to use for sending the request.
    /// </param>
    /// <param name="message">
    /// The <see cref="HttpRequestMessage"/> instance containing the request details.
    /// </param>
    /// <returns>
    /// A deserialized <see cref="ClearDataEnvelope"/> instance containing the response data, or <b>null</b> if an error occurs.
    /// </returns>
    public async Task<ISecureDataEnvelope?> ReadSecureResponseAsync(IOperationalResult result, HttpRequestMessage message)
    {
        return null;
    }
    #endregion

    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Sends the HTTP message and gets the clear HTTP response.
    /// </summary>
    /// <param name="client">
    /// The <see cref="HttpClient"/> instance to use for sending the request.
    /// </param>
    /// <param name="message">
    /// The <see cref="HttpRequestMessage"/> instance containing the request details.
    /// </param>
    /// <returns>
    /// A deserialized <see cref="ClearDataEnvelope"/> instance containing the response data, or <b>null</b> if an error occurs.
    /// </returns>
    private async Task<ClearDataEnvelope?> GetClearResponseAsync(HttpClient client, HttpRequestMessage message)
    {
        ClearDataEnvelope? returnMessage = null;
        try
        {
            HttpResponseMessage response = await client.SendAsync(message).ConfigureAwait(false);
            string? responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            response.Dispose();

            if (responseJson != null)
            {
                returnMessage = JsonSerializer.Deserialize<ClearDataEnvelope>(responseJson);
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions related to the HTTP request
            ExceptionLog.LogServerException(ex);
        }
        return returnMessage;
    }
    /// <summary>
    /// Creates a clear data request message for the specified API name and entity.
    /// </summary>
    /// <param name="apiName">
    /// A string containbing the name of the API.
    /// </param>
    /// <param name="entity">
    /// The <see cref="IClearDataEnvelope"/> entity to be sent.
    /// </param>
    /// <returns>
    /// The <see cref="HttpRequestMessage"/> instance containing the request details, or <b>null</b> if serialization fails."/>
    /// </returns>
    private HttpRequestMessage? RenderClearRequest(string apiName, IClearDataEnvelope? entity)
    {
        HttpRequestMessage? message = null;

        if (entity != null)
        {
            string? json = SerializeClearDataEnvelope(entity);
            if (json != null)
            {
                message = new HttpRequestMessage(
                    HttpMethod.Post,
                    Api + apiName);

                message.Content = new StringContent(
                    json,
                    Encoding.UTF8,
                    MediaTypeJson);
            }
        }
        return message;
    }
    /// <summary>
    /// Serializes the clear data envelope object for sending as a string content in an HTTP request.
    /// </summary>
    /// <param name="entity">
    /// A <see cref="ClearDataEnvelope"/> instance representing the data to be serialized."/>
    /// </param>
    /// <returns>
    /// A string containing the serialized JSON representation of the <see cref="ClearDataEnvelope"/> object,   
    /// </returns>
    private string? SerializeClearDataEnvelope(IClearDataEnvelope entity)
    {
        string? json = null;
        try
        {
            json = JsonSerializer.Serialize(entity);
        }
        catch (Exception ex)
        {
            // Handle serialization exceptions if necessary
            Console.WriteLine($"Serialization error: {ex.Message}");
            ExceptionLog.LogServerException(ex);
        }
        return json;
    }
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
    public async Task<List<byte[]?>> PerformKeyExchangeRequestAsync(IOperationalResult result, string apiName, byte[]? rsaPublicKey)
    {
        List<byte[]?> returnList = new List<byte[]?>();

        // Validate.
        ValidateKekParameters(result, rsaPublicKey);
        if (result.Success)
        {
            // Create or reference the cryptographic objects.
            RsaProvider? rsa = AsymmetricCryptoProviderFactory.CreateRsaProvider(rsaPublicKey);
            AesProvider? aes = SymmetricCryptoProviderFactory.CreateSymmetricProvider();

            if (rsa == null)
            {
                result.SetFailureMessage("Failed to create RSA provider from the primary public key.");
            }
            else if (aes == null)
            {
                result.SetFailureMessage("Failed to create AES provider.");
            }
            else
            {

                // Create the first AES key to use, and encrypt it with the RSA public key.
                aes.GenerateNewKey();
                byte[]? newSymmetricKey = aes.GetKeyData();

                byte[]? encryptedSymmetricKey = rsa.Encrypt(newSymmetricKey);
                if (encryptedSymmetricKey != null)
                {
                    byte[]? nextSetOfData = await ExecuteKeyExchangeRequestAsync(result, apiName, encryptedSymmetricKey).ConfigureAwait(false);

                    // Ensure the new key data is returned,
                    returnList.Add(newSymmetricKey);
                    if (nextSetOfData != null)
                        returnList.Add(nextSetOfData);
                }

                SymmetricCryptoProviderFactory.ReleaseSymmetricProvider(aes);
                AsymmetricCryptoProviderFactory.ReleaseRsaProvider(rsa);

            }
        }
        return returnList;
    }
    /// <summary>
    /// Validates the Key Exhcnage call(s) parameters.
    /// </summary>
    /// <param name="result">
    /// An <see cref="IOperationalResult"/> containing the result of the operation.
    /// </param>
    /// <param name="rsaPublicKey">
    /// A byte array containing the RSA public key to use.
    /// </param>
    private void ValidateKekParameters(IOperationalResult result, byte[]? rsaPublicKey)
    {
        result.Success = true;

        if (_session == null)
        {
            result.Success = false;
            result.Message = "The session is not initialized.  Cannot continue.";
        }
        else if (rsaPublicKey == null || rsaPublicKey.Length == 0)
        {
            result.Success = false;
            result.Message = "The RSA public key is not set. Cannot perform key exchange.";
        }
    }
    /// <summary>
    /// Executes the key exchange request.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IOperationalResult"/> instance containing the result of the operation.
    /// </param>
    /// <param name="encryptedSymmetricKey">
    /// A byte array containing the encrypted symmetric key.
    /// </param>
    /// <returns>
    /// A byte array containing the data returned from the server, which can be a new RSA public key value
    /// or an application authorization token, or <b>null</b> if the operation fails.
    /// </returns>
    private async Task<byte[]?> ExecuteKeyExchangeRequestAsync(IOperationalResult result, string apiName, byte[] encryptedSymmetricKey)
    {
        byte[]? returnData = null;

        ClearDataEnvelope entity = new ClearDataEnvelope();
        entity.SessionId = _session.SessionId;
        entity.Data = encryptedSymmetricKey;

        HttpResponseMessage? response = await SendClearRequestAsync(result, apiName, entity).ConfigureAwait(false);

        if (response != null)
        {
            IClearDataEnvelope? responseData = await ReadClearResponseAsync(result, response).ConfigureAwait(false);
            if (responseData != null)
            {
                returnData = responseData.Data;
                responseData.Dispose();
            }
            response.Dispose();
        }
        entity.Dispose();

        return returnData;
    }
    #endregion
}

