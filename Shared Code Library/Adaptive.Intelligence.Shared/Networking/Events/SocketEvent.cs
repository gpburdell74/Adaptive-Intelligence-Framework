using System.Net.Sockets;

namespace Adaptive.Intelligence.Shared.Networking;

/// <summary>
/// Provides a delegate definition for <see cref="Socket"/> related events.
/// </summary>
/// <param name="sender">The sender.</param>
/// <param name="e">The <see cref="SocketEventArgs"/> instance containing the event data.</param>
public delegate void SocketEventHandler(object? sender, SocketEventArgs e);

/// <summary>
/// Provides an event arguments instane for <see cref="Socket"/> related events.
/// </summary>
/// <seealso cref="EventArgs" />
public class SocketEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SocketEventArgs"/> class.
    /// </summary>
    /// <param name="socket">
    /// The <see cref="Socket"/> used by the event.
    /// </param>
    public SocketEventArgs(Socket socket)
    {
        Socket = socket;
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="SocketEventArgs"/> class.
    /// </summary>
    ~SocketEventArgs()
    {
        Socket = null;
    }

    /// <summary>
    /// Gets or sets the reference to the socket being used by the event.
    /// </summary>
    /// <value>
    /// The <see cref="Socket"/> instance, or <b>null</b>.
    /// </value>
    public Socket? Socket { get; set; }
}
