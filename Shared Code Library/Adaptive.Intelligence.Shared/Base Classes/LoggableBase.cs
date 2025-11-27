using Adaptive.Intelligence.Shared.IO;
using System.Text;

namespace Adaptive.Intelligence.Shared;

/// <summary>
/// Provides a base definition for any disposable class that tracks its exceptions and provides logging to the console,
/// or a local file.
/// </summary>
/// <seealso cref="ExceptionTrackingBase" />
public class LoggableBase : ExceptionTrackingBase
{
    #region Private Logging Members
    /// <summary>
    /// The enable logging flag.
    /// </summary>
    private bool _enableLogging = true;
    /// <summary>
    /// The log to console flag.
    /// </summary>
    private bool _logToConsole = true;
    /// <summary>
    /// The log file name.
    /// </summary>
    private string? _logFileName;
    /// <summary>
    /// The log stream.
    /// </summary>
    private FileStream? _logStream;
    /// <summary>
    /// The writer for hte log file.
    /// </summary>
    private StreamWriter? _writer;
    #endregion

    #region Private Member Declarations
    /// <summary>
    /// The threading synchronization instance.
    /// </summary>
    private static readonly object _syncLogRoot = new object();
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="LoggableBase"/> class.
    /// </summary>
    /// <param name="enableLogging">
    /// An optional parameter indicating whether or not logging is enabled.
    /// </param>
    /// <param name="logToConsole">
    /// An optional parameter indicating whether to log to the console instead of a local file.
    /// </param>
    /// <param name="specificFileName">
    /// An optional parameter indicating a specific log file name.  If not specified, one is generated.
    /// Name of the specific file.</param>
    public LoggableBase(bool enableLogging = false, bool logToConsole = false, string? specificFileName = null)
    {
        _enableLogging = enableLogging;
        _logToConsole = logToConsole;
        _logFileName = specificFileName;
        if (_enableLogging)
        {
            if (!_logToConsole)
            {
                _logFileName = GenerateLogFileName();
                OpenLogFile();
            }
        }
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (_enableLogging)
            CloseLog();

        base.Dispose(disposing);
    }
    #endregion

    #region Private Logging Methods    
    /// <summary>
    /// Closes the log.
    /// </summary>
    private void CloseLog()
    {
        if (_enableLogging)
        {
            if (_logToConsole)
            {
                System.Console.WriteLine("Press [Enter] To Exit...");
                System.Console.ReadLine();
            }
            else
            {
                try
                {
                    _writer?.Close();
                    _logStream?.Close();
                }
                catch
                { }
            }
        }
        _writer = null;
        _logStream = null;
        GC.Collect();
    }
    /// <summary>
    /// Generates a local file name for the new log file.
    /// </summary>
    /// <returns>
    /// A string containing the data type name and tickcount as a text file name.
    /// </returns>
    private string? GenerateLogFileName()
    {
        return GetType().Name + "Log-" + System.Environment.TickCount.ToString() + ".txt";
    }
    /// <summary>
    /// Attempts to open the local log file for writing.
    /// </summary>
    private void OpenLogFile()
    {
        if (_enableLogging && !_logToConsole && !string.IsNullOrEmpty(_logFileName))
        {
            try
            {
                if (SafeIO.FileExists(_logFileName))
                    SafeIO.DeleteFile(_logFileName);

                _logStream = new FileStream(_logFileName, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            }
            catch
            { }
            if (_logStream != null)
            {
                _writer = new StreamWriter(_logStream);
                _writer.AutoFlush = true;
            }
        }
    }

    /// <summary>
    /// Writes the status message to the log.
    /// </summary>
    /// <param name="message">
    /// A string containing the message to write.
    /// </param>
    protected void WriteStatus(string message)
    {
        if (_enableLogging)
            WriteLogData(message);
    }

    /// <summary>
    /// Writes the Exception message to the log.
    /// </summary>
    /// <param name="ex">
    /// The <see cref="Exception"/> to be written.
    /// </param>
    protected void WriteException(Exception ex)
    {
        if (_enableLogging)
        {
            WriteLogData(ex.Message);
            if (ex.InnerException != null)
                WriteException(ex.InnerException);
        }
    }

    /// <summary>
    /// Writes the status and exception messages to the log.
    /// </summary>
    /// <param name="message">
    /// A string containing the message to be written.
    /// </param>
    /// <param name="ex">
    /// The <see cref="Exception"/> to be written.
    /// </param>
    protected void WriteException(string message, Exception ex)
    {
        WriteStatus(message);
        WriteException(ex);
    }

    /// <summary>
    /// Writes the log data to the chosen output.
    /// </summary>
    /// <param name="content">
    /// A string containing the content to be written.
    /// </param>
    private void WriteLogData(string content)
    {
        if (_enableLogging)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(DateTime.Now.ToString("o") + "\t");
            builder.Append("Thread: " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + "\t");
            builder.Append(content);

            // One thread at a time, please...
            lock (_syncLogRoot)
            {
                // Console
                if (_logToConsole)
                {
                    System.Console.WriteLine(builder.ToString());
                }
                else
                {
                    // Or file.
                    try
                    {
                        _writer?.WriteLine(builder.ToString());
                        _writer?.Flush();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
    }
    #endregion
}
