namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides an event delegate definition for operation progress update events.
	/// </summary>
	/// <param name="sender">
	/// The object raising the event.
	/// </param>
	/// <param name="e">
	/// The <see cref="ProgressUpdateEventArgs"/> instance containing the event data.
	/// </param>
	public delegate void ProgressUpdateEventHandler(object sender, ProgressUpdateEventArgs e);
}