namespace Adaptive.Intelligence.Shared.Networking;

/// <summary>
/// Provides a delegate definition for socket error-related events.
/// </summary>
/// <param name="sender">The sender.</param>
/// <param name="e">The <see cref="SocketErrorEventArgs"/> instance containing the event data.</param>
public delegate void SocketErrorEventHandler (object sender, SocketErrorEventArgs e);


/// <summary>
/// Provides an event arguments instnace for socket-error related events.
/// </summary>
/// <seealso cref="EventArgs" />
public class SocketErrorEventArgs : EventArgs
{
    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    /// <value>
    /// An integer specifying the socket error code.
    /// </value>
    public int Error { get; set; }
}
