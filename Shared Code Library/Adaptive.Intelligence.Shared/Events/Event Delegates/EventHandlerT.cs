namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides an event delegate definition for events with generic type arguments.
	/// </summary>
	/// <param name="sender">
	/// The object raising the event.
	/// </param>
	/// <param name="e">
	/// The <see cref="EventHandler{T}"/> instance containing the event data.
	/// </param>
	/// <typeparam name="T">
	/// The data type of the data contained in <i>e</i>.
	/// </typeparam>
	public delegate void EventHandler<T>(object sender, EventArgs<T> e);
}