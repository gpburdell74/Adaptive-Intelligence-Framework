using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using System.Diagnostics;

namespace System
{
	/// <summary>
	/// Provides a shortcut class for printing debug and trace messages.
	/// </summary>
	public static class AdaptiveDebug
	{
		#region Private Static Members
		/// <summary>
		/// An optional output file.
		/// </summary>
		private static TextFile? _outputFile;
		/// <summary>
		/// The last ticks value that was recorded.
		/// </summary>
		private static long _lastTicks;
		#endregion

		#region Public Static Properties				
		/// <summary>
		/// Gets or sets the file name date format.
		/// </summary>
		/// <value>
		/// A string specifying the format for the date value in the file name.
		/// </value>
		public static string FileNameDateFormat { get; set; } = "yyyyddMM-hhmmss";
		/// <summary>
		/// Gets or sets the file name prefix value.
		/// </summary>
		/// <value>
		/// A string containing the name prefix value.
		/// </value>
		public static string FileNamePrefix { get; set; } = @"\Trace Log - ";
		/// <summary>
		/// Gets or sets the file name extension value.
		/// </summary>
		/// <value>
		/// A string containing the name extension value.
		/// </value>
		public static string FileNameExtension { get; set; } = @"csv";

		/// <summary>
		/// Gets or sets the mode text.
		/// </summary>
		/// <value>
		/// A string describing the operation mode for the debugger, or <see cref="String.Empty"/>.
		/// </value>
		public static string Mode { get; set; } = string.Empty;
		#endregion

		#region Public Static Methods / Functions
		/// <summary>
		/// Signals a break point to an attached debugger.
		/// </summary>
		public static void Break()
		{
#if DEBUG
			Debugger.Break();
#endif
		}
		/// <summary>
		/// Closes the text output file, if open.
		/// </summary>
		public static void Close()
		{
			_outputFile?.Close();
			_outputFile?.Dispose();
			_outputFile = null;
		}
		/// <summary>
		/// Creates a logging output file name.
		/// </summary>
		/// <returns>
		/// A string containing the fully-qualified path and name of the file.
		/// </returns>
		public static string CreateOutputFileName()
		{
			// Format:
			//		<path>\<filename prefix><date time>.<filename extension>
			string path = 
				Directory.GetCurrentDirectory() +
				FileNamePrefix +
				DateTime.Now.ToString(FileNameDateFormat) + "." +
				FileNameExtension;

			return path;
		}
		/// <summary>
		/// Increases the current trace indention level by one.
		/// </summary>
		public static void Indent()
		{
			Trace.Indent();
		}
		/// <summary>
		/// Prints the specified content to the output window.
		/// </summary>
		/// <param name="content">
		/// A string containing the content to be printed..</param>
		public static void Print(string content)
		{
			string output = RenderOutputPrefix() + content;
			if (Debugger.IsAttached)
				Debug.WriteLine(output);
			FileOutput(content);
		}
		/// <summary>
		/// Writes the current thread ID value to the trace and output window.
		/// </summary>
		public static void ShowThread()
		{
			string id = Thread.CurrentThread.ManagedThreadId.ToString();
			if (Debugger.IsAttached)
				Debug.WriteLine($"TID: {id}");
			Trace.TraceInformation($"TID: {id}");
			_outputFile?.WriteLine($"TID: {id}");
		}
		/// <summary>
		/// Decreases the current trace indention level by one.
		/// </summary>
		public static void Unindent()
		{
			Trace.Unindent();
		}
		/// <summary>
		/// Initializes the use of an output file for writing the debug/trace content to
		/// a text file.
		/// </summary>
		/// <param name="fileName">
		/// A string containing the fully-qualified path and name of the file.
		/// </param>
		public static void UseOutputFile(string fileName)
		{
			_outputFile = new TextFile(fileName);
			_outputFile.Create();

			string line = "Mode,Date/Time,Thread Id,Elapsed Ticks,Content";
			_outputFile.WriteLine(line);
		}
		/// <summary>
		/// Writes the specified content to the trace and output window.
		/// </summary>
		/// <param name="content">A string containing the content to be written.
		/// </param>
		public static void Write(string content)
		{
			string output = RenderOutputPrefix() + content;
			if (Debugger.IsAttached)
				Debug.Write(output);
			FileOutput(content);
		}
		/// <summary>
		/// Writes the specified content to the trace and output window as a
		/// single line.
		/// </summary>
		/// <param name="content">
		/// A string containing the content to be written.
		/// </param>
		public static void WriteLine(string content)
		{
			string output = RenderOutputPrefix() + content;
			if (Debugger.IsAttached)
				Debug.WriteLine(output);
			FileOutput(content);
		}
		#endregion

		#region Private Methods / Functions
		/// <summary>
		/// Renders the output prefix string to print before the intended output.
		/// </summary>
		/// <returns>
		/// A string containing the current date/time value and thread ID value.
		/// </returns>
		private static string RenderOutputPrefix()
		{
			return Mode +
				DateTime.Now.ToString(Constants.USFullDateFormat) +
				"\tTID: " + Thread.CurrentThread.ManagedThreadId + ":\t";
		}
		/// <summary>
		/// Writes the content to an output file.
		/// </summary>
		/// <param name="content">
		/// A string containing the content to be written.</param>
		private static void FileOutput(string content)
		{
			long elapsed = 0;
			long current = Environment.TickCount;
			if (_lastTicks > 0)
				elapsed = current - _lastTicks;
			_lastTicks = current;

			if (_outputFile != null)
			{
				string line = 
					$"{Mode}, {DateTimeExtensions.NowAsString()}, {Thread.CurrentThread.ManagedThreadId}, " +
					$"{elapsed}, \"{content}\"";
				_outputFile.WriteLine(line);
			}
		}
		#endregion
	}
}
