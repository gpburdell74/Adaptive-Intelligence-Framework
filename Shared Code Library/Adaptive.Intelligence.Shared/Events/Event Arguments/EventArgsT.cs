namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides a generic version of <see cref="EventArgs"/>.
	/// </summary>
	/// <typeparam name="T">
	/// The data type of the <see cref="Data"/> property.
	/// </typeparam>
	/// <seealso cref="EventArgs" />
	public class EventArgs<T> : EventArgs
	{
		#region Constructor / Destructor
		/// <summary>
		/// Initializes a new instance of the <see cref="EventArgs{T}"/> class.
		/// </summary>
		public EventArgs()
		{
			Data = default;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="EventArgs{T}"/> class.
		/// </summary>
		/// <param name="data">
		/// The event data instance.
		/// </param>
		public EventArgs(T? data)
		{
			Data = data;
		}
		/// <summary>
		/// Finalizes an instance of the <see cref="EventArgs{T}"/> class.
		/// </summary>
		~EventArgs()
		{
			Data = default;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the reference to the event data.
		/// </summary>
		/// <value>
		/// The instance of <typeparamref name="T"/> that is being used
		/// as the event data.
		/// </value>
		public T? Data { get; set; }
		#endregion
	}
}