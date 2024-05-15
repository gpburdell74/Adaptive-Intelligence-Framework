namespace Adaptive.Intelligence.Shared.UI
{
	/// <summary>
	/// Provides an override of <see cref="Control"/> that captures the ID of the main UI thread.
	/// </summary>
	/// <seealso cref="Control" />
	public class ThreadAwareControl : Control
	{
		/// <summary>
		/// The managed ID of the main UI thread.
		/// </summary>
		private readonly int _id;

		/// <summary>
		/// Initializes a new instance of the <see cref="ThreadAwareControl"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public ThreadAwareControl() 
		{
			_id = Environment.CurrentManagedThreadId;
		}
		/// <summary>
		/// Gets the ID of the thread the control was created on.
		/// </summary>
		/// <value>
		/// An integer containing the managed thread ID value.
		/// </value>
		public int ThreadId => _id;
	}
}
