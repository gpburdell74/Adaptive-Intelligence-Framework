using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Adaptive.Intelligence.Shared.Networking.Tcp;

/// <summary>
/// Provides a class for communicating across a TCP connection.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public sealed class TcpClient : LoggableBase
{
    #region Public Events
    /// <summary>
    /// Occurs when an erorr occurs when binding to a local address.
    /// </summary>
    public event ExceptionEventHandler? BindingFailed;

    /// <summary>
    /// Occurs when the connection is closed.
    /// </summary>
    public event EventHandler? Closed;

    /// <summary>
    /// Occurs when an erorr occurs when closing the connection.
    /// </summary>
    public event ExceptionEventHandler? CloseError;

    /// <summary>
    /// Occurs when a connection is established.
    /// </summary>
    public event EventHandler? Connected;

    /// <summary>
    /// Occurs when an erorr occurs when creating a connection.
    /// </summary>
    public event ExceptionEventHandler? ConnectionFailure;

    /// <summary>
    /// Occurs when an erorr occurs when creating the socket.
    /// </summary>
    public event ExceptionEventHandler? CreateSocketFailure;

    /// <summary>
    /// Occurs when an exception is encountered when reading data.
    /// </summary>
    public event ExceptionEventHandler? DataReadFailure;

    /// <summary>
    /// Occurs when new data is received and read from the socket.
    /// </summary>
    public event DataReceievedEventHandler? DataReceived;

    /// <summary>
    /// Occurs when an erorr occurs when disconnecting from a remote host.
    /// </summary>
    public event ExceptionEventHandler? DisconnectError;

    /// <summary>
    /// Occurs when an erorr occurs when A DNS query fails.
    /// </summary>
    public event ExceptionEventHandler? DnsFailure;

    /// <summary>
    /// Occurs when an erorr occurs when attempting to use the instance without a valid socket.
    /// </summary>
    public event EventHandler? InvalidSocket;

    /// <summary>
    /// Occurs when an exception is encountered when polling the socket.
    /// </summary>
    public event ExceptionEventHandler? PollFailure;

    /// <summary>
    /// Occurs when an exception is encountered when starting the polling thread.
    /// </summary>
    public event ExceptionEventHandler? PollThreadStartFailure;

    /// <summary>
    /// Occurs when an exception is encountered when transmitting data.
    /// </summary>
    public event ExceptionEventHandler? SendFailure;

    /// <summary>
    /// Occurs when an erorr occurs when the setsockopt() call fails.
    /// </summary>
    public event ExceptionEventHandler? SetSocketOptionsFailure;

    /// <summary>
    /// Occurs when an erorr occurs when shutting down communications on the socket.
    /// </summary>
    public event ExceptionEventHandler? ShutdownError;

    /// <summary>
    /// Occurs when a socket error is detected.
    /// </summary>
    public event SocketErrorEventHandler? SocketError;

    /// <summary>
    /// Occurs when the socket is unexpectedly closed.  Usually closed by the remote host.
    /// </summary>
    public event EventHandler? UnexpectedClose;
    #endregion

    #region Private Member Declarations
    /// <summary>
    /// The threading synchronization instance.
    /// </summary>
    private static readonly object _syncRoot = new object();

    /// <summary>
    /// The client socket instance - may be provided from a listner instance or created locally.
    /// </summary>
    private Socket? _clientSocket;

    /// <summary>
    /// The local binding, if used.
    /// </summary>
    private IPEndPoint? _localBinding;

    /// <summary>
    /// The data stream to write to and read from (for external use).
    /// </summary>
    private NetworkStream? _dataStream;

    /// <summary>
    /// The socket and connection options.
    /// </summary>
    private TcpClientOptions? _options;

    /// <summary>
    /// The polling thread instance.
    /// </summary>
    private Thread? _pollingThread;

    /// <summary>
    /// The polling thread execution flag.
    /// </summary>
    private bool _executePollingThread;

    /// <summary>
    /// The polling thread is running indicator.
    /// </summary>
    private bool _pollingThreadRunning;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="TcpClient"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public TcpClient() : this(TcpClientOptions.Default)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TcpClient"/> class.
    /// </summary>
    /// <param name="options">
    /// The <see cref="TcpClientOptions"/> options instance used for connecting to remote clients.
    /// </param>
    public TcpClient(TcpClientOptions options)
    {
        WriteStatus("TcpClient Created.");
        _options = options;
        CreateSocket();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TcpClient"/> class.
    /// </summary>
    /// <param name="localEndPoint">
    /// The local <see cref="IPEndPoint"/> to bind to.
    /// </param>
    public TcpClient(IPEndPoint localEndPoint)
    {
        WriteStatus("TcpClient Created - Using default options.");
        WriteStatus($"LocalEP Binding: {localEndPoint}");
        _options = TcpClientOptions.Default;

        CreateSocket();
        CreateBinding(localEndPoint);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TcpClient"/> class.
    /// </summary>
    /// <param name="hostname">
    /// A string containing the host name or IP address of the remote system to connect to.
    /// </param>
    /// <param name="port">
    /// An integer indicating the port number to connect to.
    /// </param>
    public TcpClient(string hostname, int port) : this()
    {
        WriteStatus("TcpClient Created.");
        WriteStatus($"\t{hostname}:{port}");

        _options = TcpClientOptions.Default;
        _options.RemoteHostNameOrAddress = hostname;
        _options.Port = port;
        CreateSocket();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TcpClient"/> class.
    /// </summary>
    /// <param name="acceptedSocket">
    /// A <see cref="Socket"/> provided from <see cref="TcpServer"/> or another listener instance that
    /// is the result of accepting a connection.
    /// </param>
    public TcpClient(Socket acceptedSocket)
    {
        WriteStatus("TcpClient Created From Listener (TcpServer).");
        _options = TcpClientOptions.Default;
        _clientSocket = acceptedSocket;
        SetSocketOptions();
        _dataStream = GetStream();
        StartPollingThread();
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        WriteStatus("Disposing Start.");
        if (!IsDisposed && disposing)
        {
            WriteStatus("Close and release network stream.");
            _dataStream?.Dispose();

            WriteStatus("Closing instance.");
            Close();

            _options?.Dispose();
        }
        _options = null;
        WriteStatus("Disposing End.");
        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Closes all connections and disposes of the underlying socket.
    /// </summary>
    public void Close()
    {
        WriteStatus("Closing...");

        // Stop the polling thread.
        WriteStatus("Waiting for polling to terminate...");
        _executePollingThread = false;
        while (_pollingThreadRunning)
        {
            Thread.Sleep(50);
        }

        WriteStatus("... Polling terminated.");
        if (_clientSocket != null)
        {
            if (_clientSocket.Connected)
            {
                try
                {
                    WriteStatus("Shutting down socket...");
                    _clientSocket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception ex)
                {
                    WriteException(ex);
                    AddException(ex);
                    OnShutdownError(ex);
                }

                try
                {
                    WriteStatus("Disconnecting...");
                    _clientSocket.Disconnect(false);
                }
                catch (Exception ex)
                {
                    WriteException(ex);
                    AddException(ex);
                    OnDisconnectError(ex);
                }

                try
                {
                    WriteStatus("Closing Socket...");
                    _clientSocket.Close();
                    _clientSocket.Dispose();
                }
                catch (Exception ex)
                {
                    WriteException(ex);
                    AddException(ex);
                    OnCloseError(ex);
                }
            }
            WriteStatus("Clearing memory...");
            _clientSocket = null;
            _dataStream = null;
            _pollingThread = null;
            _localBinding = null;
            _options = null;
            _executePollingThread = false;
            _pollingThreadRunning = false;

            OnClosed(EventArgs.Empty);
            GC.Collect();
        }
    }

    /// <summary>
    /// Gets the reference to the network stream.
    /// </summary>
    /// <returns>
    /// The <see cref="NetworkStream"/> instance, if present.
    /// </returns>
    public NetworkStream? GetStream()
    {
        NetworkStream? _stream = null;

        if (_clientSocket != null && _clientSocket.Connected)
        {
            _stream = new NetworkStream(_clientSocket);
        }
        return _stream;
    }

    #region Send Data Methods
    /// <summary>
    /// Sends the specified text to the remote host.
    /// </summary>
    /// <param name="text">
    /// A string containing the text to be send.  The encoding is assumed to be ASCII.
    /// </param>
    public void Send(string text)
    {
        Send(text, Encoding.ASCII);
    }

    /// <summary>
    /// Sends the specified text to the remote host.
    /// </summary>
    /// <param name="text">
    /// A string containing the text to be send.
    /// </param>
    /// <param name="encoding">
    /// The text <see cref="Encoding"/> used to translate the content to a byte array.
    /// </param>
    public void Send(string text, Encoding encoding)
    {
        byte[] data = encoding.GetBytes(text);
        Send(data);
    }

    /// <summary>
    /// Sends the specified data to the remote host.
    /// </summary>
    /// <param name="data">
    /// A byte array containing the data to be sent.
    /// The data.</param>
    public int Send(byte[] data)
    {
        int sentBytes = 0;
        if (_clientSocket != null && _clientSocket.Connected)
        {
            try
            {
                sentBytes = _clientSocket.Send(data);
            }
            catch (Exception ex)
            {
                WriteException(ex);
                AddException(ex);
                OnSendFailure(ex);
            }
        }
        return sentBytes;
    }
    
    /// <summary>
    /// Sends the specified text to the remote host.
    /// </summary>
    /// <param name="text">
    /// A string containing the text to be send.  The encoding is assumed to be ASCII.
    /// </param>
    public Task SendAsync(string text)
    {
        return SendAsync(text, Encoding.ASCII);
    }

    /// <summary>
    /// Sends the specified text to the remote host.
    /// </summary>
    /// <param name="text">
    /// A string containing the text to be send.
    /// </param>
    /// <param name="encoding">
    /// The text <see cref="Encoding"/> used to translate the content to a byte array.
    /// </param>
    public async Task SendAsync(string text, Encoding encoding)
    {
        byte[] data = encoding.GetBytes(text);
        await SendAsync(data);
    }

    /// <summary>
    /// Sends the specified data to the remote host.
    /// </summary>
    /// <param name="data">
    /// A byte array containing the data to be sent.
    /// The data.</param>
    public async Task SendAsync(byte[] data)
    {
        if (_clientSocket != null && _clientSocket.Connected)
        {
            try
            {
                int sent = await _clientSocket.SendAsync(data).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                WriteException(ex);
                AddException(ex);
                OnSendFailure(ex);
            }
        }
    }

    #endregion

    #endregion

    #region Private Event Methods
    /// <summary>
    /// Raises the <see cref="BindingFailed"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnBindingFailed(Exception ex)
    {
        WriteException("OnBindingFailed", ex);
        lock (_syncRoot)
        {
            BindingFailed?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="Closed" /> event.
    /// </summary>
    /// <param name="e">
    /// The <see cref="EventArgs"/> instance containing the event data.
    /// </param>
    private void OnClosed(EventArgs e)
    {
        WriteStatus("OnClosed");
        lock (_syncRoot)
        {
            Closed?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the <see cref="BindingFailed"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnCloseError(Exception ex)
    {
        WriteException("OnCloseError", ex);
        lock (_syncRoot)
        {
            CloseError?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="E:Connected" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void OnConnected(EventArgs e)
    {
        WriteStatus("OnConnected");
        lock (_syncRoot)
        {
            Connected?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the <see cref="ConnectionFailure"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnConnectionFailure(Exception ex)
    {
        WriteException("OnConnectionFailure", ex);
        lock (_syncRoot)
        {
            ConnectionFailure?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="CreateSocketFailure"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnCreateSocketFailure(Exception ex)
    {
        WriteException("OnCreateSocketFailure", ex);
        lock (_syncRoot)
        {
            CreateSocketFailure?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="DataReadFailure"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnDataReadFailure(Exception ex)
    {
        WriteException("OnDataReadFailure", ex);
        lock (_syncRoot)
        {
            DataReadFailure?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="DataReceived" /> event.
    /// </summary>
    /// <param name="e">The <see cref="DataReceivedEventArgs"/> instance containing the event data.
    /// </param>
    private void OnDataReceived(DataReceivedEventArgs e)
    {
        WriteStatus("OnDataReceived");
        lock (_syncRoot)
        {
            DataReceived?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the <see cref="DisconnectError"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnDisconnectError(Exception ex)
    {
        WriteException("OnDisconnectError", ex);
        lock (_syncRoot)
        {
            DisconnectError?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="DnsFailure"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnDnsFailure(Exception ex)
    {
        WriteException("OnDnsFailure", ex);
        lock (_syncRoot)
        {
            DnsFailure?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="E:InvalidSocket" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void OnInvalidSocket(EventArgs e)
    {
        WriteStatus("OnInvalidSocket");
        lock (_syncRoot)
        {
            InvalidSocket?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the <see cref="PollFailure"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnPollFailure(Exception ex)
    {
        WriteException("OnPollFailure", ex);
        lock (_syncRoot)
        {
            PollFailure?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="PollThreadStartFailure"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnPollThreadStartFailure(Exception ex)
    {
        WriteException("OnPollThreadStartFailure", ex);
        lock (_syncRoot)
        {
            PollThreadStartFailure?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="SendFailure"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnSendFailure(Exception ex)
    {
        WriteException(nameof(OnSendFailure), ex);
        lock (_syncRoot)
        {
            SendFailure?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="SetSocketOptionsFailure"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnSetSocketOptionsFailure(Exception ex)
    {
        WriteException(nameof(OnSetSocketOptionsFailure), ex);
        lock (_syncRoot)
        {
            SetSocketOptionsFailure?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="ShutdownError"/> event.
    /// </summary>
    /// <param name="ex">
    /// The reference to the <see cref="Exception"/> causing the event.
    /// </param>
    private void OnShutdownError(Exception ex)
    {
        WriteException("OnShutdownError", ex);
        lock (_syncRoot)
        {
            ShutdownError?.Invoke(this, new ExceptionEventArgs(ex));
        }
    }

    /// <summary>
    /// Raises the <see cref="SocketError" /> event.
    /// </summary>
    /// <param name="e">The <see cref="SocketErrorEventArgs"/> instance containing the event data.</param>
    private void OnSocketError(SocketErrorEventArgs e)
    {
        WriteStatus(nameof(OnSocketError));
        lock (_syncRoot)
        {
            SocketError?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Raises the <see cref="UnexpectedClose" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void OnUnexpectedClose(EventArgs e)
    {
        WriteStatus(nameof(OnUnexpectedClose));
        lock (_syncRoot)
        {
            UnexpectedClose?.Invoke(this, e);
        }
    }

    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Attempts to create the local binding.
    /// </summary>
    /// <param name="localEndPoint">
    /// The local <see cref="IPEndPoint"/> to bind to.
    /// </param>
    private void CreateBinding(IPEndPoint localEndPoint)
    {
        if (_clientSocket != null)
        {
            try
            {
                WriteStatus($"Creating Binding: {localEndPoint}");
                _clientSocket.Bind(localEndPoint);
                _localBinding = localEndPoint;
            }
            catch (Exception ex)
            {
                WriteException(ex);
                _localBinding = null;
                AddException(ex);
                OnBindingFailed(ex);
                
            }
        }
    }
    /// <summary>
    /// Attempts to create the socket to use.
    /// </summary>
    private void CreateSocket()
    {
        if (_clientSocket != null)
        {
            WriteStatus("Socket already created.");
            OnInvalidSocket(EventArgs.Empty);
        }
        else
        {
            try
            {
                WriteStatus("Creating Socket...");
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                WriteStatus("...Success.");
                SetSocketOptions();
            }
            catch (Exception ex)
            {
                WriteException(ex);
                AddException(ex);
                _clientSocket = null;
                OnCreateSocketFailure(ex);
            }
        }
    }

    /// <summary>
    /// Attempts to set the socket options.
    /// </summary>
    private void SetSocketOptions()
    {
        if (_clientSocket == null)
        {
            WriteStatus("No socket was created.");
            OnInvalidSocket(EventArgs.Empty);
        }
        else if (_options == null)
        {
            WriteStatus("CRITICAL: Options instance is missing.");
        }
        else
        { 
            try
            {
                WriteStatus($"\tReceiveBufferSize = {_options.ReceiveBufferSize}");
                _clientSocket.ReceiveBufferSize = _options.ReceiveBufferSize;

                WriteStatus($"\tSendBufferSize = {_options.SendBufferSize}");
                _clientSocket.SendBufferSize = _options.SendBufferSize;

                WriteStatus($"\tReceiveTimeout = {_options.ReceiveTimeout}");
                _clientSocket.ReceiveTimeout = _options.ReceiveTimeout;

                WriteStatus($"\tSendTimeout = {_options.SendTimeout}");
                _clientSocket.SendTimeout = _options.SendTimeout;

                WriteStatus($"\tNoDelay = {_options.NoDelay}");
                _clientSocket.NoDelay = _options.NoDelay;

                WriteStatus($"\tDontFragment = true");
                _clientSocket.DontFragment = true;

                if (_options.LingerState != null)
                {
                    WriteStatus($"\tLingerState = {_options.LingerState.ToString()}");
                    _clientSocket.LingerState = _options.LingerState;
                }

                WriteStatus("ExclusiveAddressUse = false");
                _clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, false);

                WriteStatus("KeepAlive = true");
                _clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                WriteStatus("ReuseAddress = true");
                _clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                WriteStatus("DontLinger = true");
                _clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
            }
            catch (Exception ex)
            {
                WriteException(ex);
                AddException(ex);
                OnSetSocketOptionsFailure(ex);
            }
        }
    }

    /// <summary>
    /// Starts the data reception polling thread.
    /// </summary>
    private void StartPollingThread()
    {
        if (!_pollingThreadRunning)
        {
            lock (_syncRoot)
            {
                WriteStatus($"Starting Data and Status Polling Thread... {Thread.CurrentThread.ManagedThreadId}");
                if (_pollingThread == null)
                {
                    try
                    {
                        WriteStatus("\tCreating Background Thread... AboveNormal priority...");
                        _executePollingThread = true;
                        _pollingThread = new Thread(ExecutePollingThread);
                        _pollingThread.IsBackground = true;
                        _pollingThread.Priority = ThreadPriority.AboveNormal;
                        _pollingThread.Start();
                    }
                    catch (Exception ex)
                    {
                        _pollingThreadRunning = false;
                        _executePollingThread = false;
                        _pollingThread = null;

                        WriteException(ex);
                        AddException(ex);
                        OnPollThreadStartFailure(ex);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Executes the thread to poll the socket for its status and data.
    /// </summary>
    private void ExecutePollingThread()
    {
        const int ReadBufferSize = 512000;

        if (_executePollingThread)
        {
            lock (_syncRoot)
            {
                _pollingThreadRunning = true;
                WriteStatus($"Data And Status Polling Thread Start: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            }

            // Use one allocated buffer for all the data reads.
            byte[] dataReceptionBuffer = new byte[ReadBufferSize];

            // While everything is valid...
            while (_executePollingThread && _pollingThreadRunning && _clientSocket != null)
            {
                // Poll for errors.
                bool hasError = CheckForError();
                if (hasError)
                {
                    // Quit Polling on error.
                    _executePollingThread = false;
                }
                else
                {

                    bool hasData = CheckForData();
                    if (hasData && _executePollingThread)
                    {
                        // Clear the allocated buffer before every read.
                        Array.Clear(dataReceptionBuffer, 0, ReadBufferSize);

                        // Read the data and raise an event.
                        ReadData(ref dataReceptionBuffer);
                    }
                }
            }

            // Clear and exit the thread.
            lock (_syncRoot)
            {
                Array.Clear(dataReceptionBuffer, 0, ReadBufferSize);
                _pollingThreadRunning = false;
            }
        }
        _pollingThreadRunning = false;
    }

    /// <summary>
    /// Polls the socket for errors.
    /// </summary>
    /// <returns>
    /// <b>true</b> if the socket has one or more errors; otherwise, return <b>false</b>.
    /// </returns>
    private bool CheckForError()
    {
        bool hasError = false;

        if (_clientSocket != null)
        {
            try
            {
                hasError = _clientSocket.Poll(100, SelectMode.SelectError);
                if (hasError)
                {
                    int? error = (int?)_clientSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error);
                    if (error != null)
                    {
                        WriteStatus("--SOCKET ERROR: " + error);
                        OnSocketError(new SocketErrorEventArgs { Error = (int)error });

                    }
                }
            }
            catch (Exception ex)
            {
                AddException(ex);
                OnPollFailure(ex);
            }
        }
        return hasError;
    }

    /// <summary>
    /// Polls the socket for data to be read.
    /// </summary>
    /// <returns>
    /// <b>true</b> if the socket has data to be read; otherwise, return <b>false</b>.
    /// </returns>
    private bool CheckForData()
    {
        bool hasData = false;

        if (_clientSocket != null)
        {
            try
            {
                hasData = _clientSocket.Poll(100, SelectMode.SelectRead);
                if (!_clientSocket.Connected)
                {
                    WriteStatus("Socket Unexpectedly Closed.");
                    OnUnexpectedClose(EventArgs.Empty);
                    _executePollingThread = false;
                }
            }
            catch (Exception ex)
            {
                WriteException(ex);
                AddException(ex);
                OnPollFailure(ex);
            }
        }
        return hasData;
    }

    /// <summary>
    /// Reads the data from the socket.
    /// </summary>
    /// <param name="buffer">
    /// The buffer that was allocated to hold the data.
    /// </param>
    /// <returns></returns>
    private void ReadData(ref byte[] buffer)
    {
        if (_clientSocket != null)
        {
            try
            {
                // Read the data from the socket.
                int numberOfBytesRead = _clientSocket.Receive(buffer, buffer.Length, SocketFlags.OutOfBand);
                if (numberOfBytesRead > 0)
                {
                    // Copy the content into the event arguments instance and raise the event.
                    DataReceivedEventArgs evArgs = new DataReceivedEventArgs(buffer, numberOfBytesRead);
                    OnDataReceived(evArgs);
                }
            }
            catch (Exception ex)
            {
                WriteException(ex);
                AddException(ex);
                OnDataReadFailure(ex);
            }
        }
    }
    #endregion
}