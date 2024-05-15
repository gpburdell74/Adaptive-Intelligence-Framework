namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides event arguments for UI progress update events.
	/// </summary>
	public sealed class ProgressUpdateEventArgs : EventArgs
	{
		#region Constructor / Destructor Methods
		/// <summary>
		/// Initializes a new instance of the <see cref="ProgressUpdateEventArgs"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public ProgressUpdateEventArgs() : this(string.Empty, 0)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ProgressUpdateEventArgs"/> class.
		/// </summary>
		/// <param name="status">
		/// The current status of the operation.
		/// </param>
		public ProgressUpdateEventArgs(string status) : this(status, 0)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ProgressUpdateEventArgs"/> class.
		/// </summary>
		/// <param name="percentDone">
		/// An integer value indicating the current completion percentage.
		/// </param>
		public ProgressUpdateEventArgs(int percentDone) : this(string.Empty, percentDone)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ProgressUpdateEventArgs"/> class.
		/// </summary>
		/// <param name="status">
		/// The current status of the operation.
		/// </param>
		/// <param name="percentDone">
		/// An integer value indicating the current completion percentage.
		/// </param>
		public ProgressUpdateEventArgs(string? status, int percentDone)
		{
			Status = status;
			PercentDone = percentDone;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ProgressUpdateEventArgs"/> class.
		/// </summary>
		/// <param name="status">
		/// The current status of the operation.
		/// </param>
		/// <param name="subStatus">
		/// The current sub-status of the operation.
		/// </param>
		/// <param name="percentDone">
		/// An integer value indicating the current completion percentage.
		/// </param>
		public ProgressUpdateEventArgs(string? status, string? subStatus, int percentDone)
		{
			Status = status;
			SubStatus = subStatus;
			PercentDone = percentDone;
		}
		/// <summary>
		/// Finalizes an instance of the <see cref="ProgressUpdateEventArgs"/> class.
		/// </summary>
		~ProgressUpdateEventArgs()
		{
			SubStatus = null;
			Status = null;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the current operation status.
		/// </summary>
		/// <value>
		/// A string specifying the current status of the operation.
		/// </value>
		public string? Status { get; set; }
		/// <summary>
		/// Gets or sets the current operation sub-status.
		/// </summary>
		/// <value>
		/// A string specifying the current sub-status of the operation.
		/// </value>
		public string? SubStatus { get; set; }
		/// <summary>
		/// Gets or sets the operation completion percentage value.
		/// </summary>
		/// <value>
		/// An integer value indicating the current completion percentage.
		/// </value>
		public int PercentDone { get; set; }
		#endregion
	}
}
