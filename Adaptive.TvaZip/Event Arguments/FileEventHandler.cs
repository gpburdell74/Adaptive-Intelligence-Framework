namespace Adaptive.Taz
{
    /// <summary>
    /// Provides an event delegate definition for <see cref="FileEventArgs"/>
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="FileEventArgs"/> instance containing the event data.</param>
    public delegate void FileEventHandler(object? sender, FileEventArgs e);
}
