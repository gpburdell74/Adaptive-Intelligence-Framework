namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides static methods and functions for managing ID values for the data objects in a file.
	/// </summary>
	public static class IdGenerator
	{
		#region Static Members
		/// <summary>
		/// The threading synchronize root instance.
		/// </summary>
		private static readonly object _syncRoot = new object();
		/// <summary>
		/// The last ID used value.
		/// </summary>
		private static int _lastId = 1000;
		#endregion

		#region Public Methods / Functions
		/// <summary>
		/// Gets the last ID value that was used.
		/// </summary>
		/// <returns>
		/// An <see cref="int"/> specifying the last used ID value.
		/// </returns>
		public static int GetLastId()
		{
			int rvalue;
			lock (_syncRoot)
			{
				rvalue = _lastId;
			}

			return rvalue;
		}
		/// <summary>
		/// Gets the next ID value to be used.
		/// </summary>
		/// <returns>
		/// An <see cref="int"/> specifying the next ID value.  The internal
		/// counter is incremented during this call.
		/// </returns>
		public static int Next()
		{
			int rvalue;
			lock (_syncRoot)
			{
				_lastId++;
				rvalue = _lastId;
			}

			return rvalue;
		}
		/// <summary>
		/// Sets the last ID value used.
		/// </summary>
		/// <remarks>
		/// This is used when a file is read to set the application to the last used ID
		/// value in the file.
		/// </remarks>
		/// <param name="lastId">
		/// An integer specifying the last ID value used.
		/// </param>
		public static void SetLastId(int lastId)
		{
			lock (_syncRoot)
			{
				_lastId = lastId;
			}
		}
		#endregion
	}
}
