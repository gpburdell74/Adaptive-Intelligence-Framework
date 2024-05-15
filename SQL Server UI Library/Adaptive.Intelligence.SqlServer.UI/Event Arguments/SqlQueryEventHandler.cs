namespace Adaptive.Intelligence.SqlServer.UI
{
    /// <summary>
    /// Provides an event handler delegate for the SQL Query events for the UI.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="SqlQueryEventArgs"/> instance containing the event data.</param>
    public delegate void SqlQueryEventHandler(object? sender, SqlQueryEventArgs e); 
}
