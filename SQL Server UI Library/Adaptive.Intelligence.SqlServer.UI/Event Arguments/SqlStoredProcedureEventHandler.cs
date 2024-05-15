namespace Adaptive.Intelligence.SqlServer.UI
{
    /// <summary>
    /// Provides an event handler delegate for SQL Stored procedure events for the UI.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="SqlStoredProcedureEventArgs"/> instance containing the event data.</param>
    public delegate void SqlStoredProcedureEventHandler(object? sender, SqlStoredProcedureEventArgs e);
}
