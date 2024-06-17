using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides a base definition for standard data access methods for Mobile Service objects where
	/// only one record is involved.
	/// </summary>
	/// <remarks>
	/// This is generally for the settings objects, where there is just one entry per customer.
	/// </remarks>
	public abstract class DataAccessBase : ExceptionTrackingBase
	{
		#region Public Events         
		/// <summary>
		/// Occurs when an asynchronous query is started.
		/// </summary>
		public static event StringEventHandler? AsyncQueryStarted;
		/// <summary>
		/// Occurs when an asynchronous query is completed.
		/// </summary>
		public static event StringEventHandler? AsyncQueryCompleted;
		#endregion

		#region Private Member Declarations        
		/// <summary>
		/// The thread synchronization instance.
		/// </summary>
		private static readonly object _syncRoot = new();
		/// <summary>
		/// Keeps track of the number of queries executing at one time.
		/// </summary>
		private static int _queriesRunning;
		#endregion

		#region Static Event Methods
		/// <summary>
		/// Called when a remote query starts to raise the <see cref="AsyncQueryStarted"/> event.
		/// </summary>
		protected static void OnAsyncQueryStarted(string methodName)
		{
			lock (_syncRoot)
			{
				_queriesRunning++;
			}

			// Don't let the subscriber do anything goofy.
			try
			{
				AsyncQueryStarted?.Invoke(null!, new StringEventArgs { Content = methodName });
			}
			catch (Exception ex)
			{
				ExceptionLog.LogException(ex);
			}
		}
		/// <summary>
		/// Called when a remote query ends to raise the <see cref="AsyncQueryCompleted"/> event.
		/// </summary>
		protected static void OnAsyncQueryCompleted(string methodName)
		{
			lock (_syncRoot)
			{
				_queriesRunning--;
			}

			if (_queriesRunning < 0)
				_queriesRunning = 0;

			// Don't let the subscriber do anything goofy.
			try
			{
				AsyncQueryCompleted?.Invoke(null!, new StringEventArgs { Content = methodName });
			}
			catch (Exception ex)
			{
				ExceptionLog.LogException(ex);
			}
		}
		#endregion

		#region Dispose Method		
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			LastOperationError = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets a value indicating whether the last data burst operation
		/// was successful.
		/// </summary>
		/// <value>
		///   <b>true</b> if the last data burst operation was successful; otherwise, <b>false</b>.
		/// </value>
		public bool LastOperationSuccess { get; set; }
		/// <summary>
		/// Gets or sets the text of the last operation error.
		/// </summary>
		/// <value>
		/// A string containing the text of the last operation error.
		/// </value>
		public string? LastOperationError { get; set; }
		#endregion

		#region Protected Abstract Methods		
		/// <summary>
		/// Records, logs, or otherwise stores the exception information when an exception is caught.
		/// </summary>
		/// <param name="ex">
		/// The <see cref="Exception"/> instance that was caught.
		/// </param>
		protected virtual void RecordException(Exception ex)
		{
			LastOperationError = ex.Message;
			ExceptionLog.LogException(ex);
		}
		#endregion
	}
}