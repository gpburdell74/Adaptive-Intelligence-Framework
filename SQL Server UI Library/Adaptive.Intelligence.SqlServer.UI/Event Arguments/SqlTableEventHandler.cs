namespace Adaptive.Intelligence.SqlServer.UI
{
    /// <summary>
    /// Provides an event handler delegate for SQL table events for the UI.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="SqlTableEventArgs"/> instance containing the event data.</param>
    public delegate void SqlTableEventHandler(object? sender, SqlTableEventArgs e);
}
