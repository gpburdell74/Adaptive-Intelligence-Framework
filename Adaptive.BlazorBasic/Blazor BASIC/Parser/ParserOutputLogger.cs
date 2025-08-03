using Adaptive.Intelligence.LanguageService.Parsing;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;
using System.Diagnostics;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.Parser;

/// <summary>
/// Provides the logging mechanism for the parsing output.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IParserOutputLogger" />
public class ParserOutputLogger : DisposableObjectBase, IParserOutputLogger
{
    #region Private Member Declarations

    #region Private Constants
    private const string LocalFileName = "Blazor BASIC Parsing Log - ";
    private const string LocalFileExtension = ".log";
    private const string USDateFormat = "MMddyyyy-hhmmss";

    private const string DividerLineMain = "----------------------------------------------------------------------";
    private const string DividerLineInner = "	--------------------------------------------------------------";

    private const string ExceptionOccurrence = "Exception Occurrence: {0} - {1}";
    private const string ExceptionTypeOuter = "\tType:    {0}";
    private const string ExceptionMessageOuter = "\tMessage: {0}";
    private const string ApplicationStackTraceOuter = "\t\tApplication Stack Trace: ";
    private const string ExceptionStackTraceOuter = "\t\tException Stack Trace: ";
    private const string ExceptionOccurrenceEnd = "-- End Exception";

    private const string InnerException = "\tInner Exception:";
    private const string ExceptionTypeInner = "\t\tType:    {0}";
    private const string ExceptionMessageInner = "\t\tMessage: {0}";
    private const string ExceptionStackTraceInner = "\t\tStack Trace: ";

    private const string StackTraceValueOuter = "\t\t{0}";
    private const string StackTraceValueInner = "\t\t\t{0}";
    #endregion

    #region Private Static Definitions
    /// <summary>
    /// The thread synchronization object.
    /// </summary>
    private static readonly object _syncRoot = new object();
    /// <summary>
    /// The singleton log instance.
    /// </summary>
    private static ParserOutputLogger? _log;
    #endregion

    /// <summary>
    /// The file stream to write to.
    /// </summary>
    private FileStream? _stream;
    /// <summary>
    /// The text writer instance.
    /// </summary>
    private StreamWriter? _writer;
    /// <summary>
    /// The file name.
    /// </summary>
    private string? _fileName;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="ParserOutputLogger"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public ParserOutputLogger()
    {
        CreateStream();
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            CloseStream();
        }

        _fileName = null;
        _stream = null;
        _writer = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Formats the exception text output.
    /// </summary>
    /// <param name="ex">
    /// The <see cref="Exception"/> instance to be recorded.
    /// </param>
    /// <param name="currentCallStack">
    /// A string specifying the current call stack from which the Log Exception
    /// method was called.
    /// </param>
    /// <returns>
    /// A <see cref="StringBuilder"/> instance containing the text to be written.
    /// </returns>
    public StringBuilder FormatException(Exception ex, string? currentCallStack)
    {
        StringBuilder builder = new StringBuilder();

        // Header
        builder.AppendLine(DividerLineMain);
        builder.AppendLine(string.Format(ExceptionOccurrence,
            DateTime.Now.ToString(Constants.USDateFormat),
            Environment.TickCount));
        builder.AppendLine(DividerLineMain);

        // Type, Message
        builder.AppendLine(string.Format(ExceptionTypeOuter, ex.GetType().Name));
        if (!string.IsNullOrEmpty(ex.Message))
            builder.AppendLine(string.Format(ExceptionMessageOuter, ex.Message));

        // Application Stack Trace
        builder.AppendLine(ApplicationStackTraceOuter);
        builder.AppendLine(DividerLineInner);
        if (currentCallStack != null)
            builder.AppendLine(string.Format(StackTraceValueOuter, currentCallStack));
        builder.AppendLine(DividerLineInner);

        // Exception Stack Trace
        builder.AppendLine(ExceptionStackTraceOuter);
        builder.AppendLine(DividerLineInner);
        if (ex.StackTrace != null)
            builder.AppendLine(string.Format(StackTraceValueOuter, ex.StackTrace));
        builder.AppendLine(DividerLineInner);

        // Inner Exception
        if (ex.InnerException != null)
        {
            builder.AppendLine(InnerException);
            builder.AppendLine(DividerLineInner);
            builder.AppendLine(string.Format(ExceptionTypeInner, ex.InnerException.GetType().Name));
            if (!string.IsNullOrEmpty(ex.InnerException.Message))
                builder.AppendLine(string.Format(ExceptionMessageInner, ex.InnerException.Message));
            builder.AppendLine(ExceptionStackTraceInner);
            builder.AppendLine(DividerLineInner);
            builder.AppendLine(string.Format(StackTraceValueInner, ex.StackTrace));
            builder.AppendLine(DividerLineInner);
        }

        // End
        builder.AppendLine(DividerLineMain);
        builder.AppendLine(ExceptionOccurrenceEnd);
        builder.AppendLine(DividerLineMain);
        builder.AppendLine();
        builder.AppendLine();

        return builder;
    }
    /// <summary>
    /// Logs the exception.
    /// </summary>
    /// <param name="ex">
    /// The <see cref="Exception"/> instance to be logged.
    /// </param>
    public void LogException(Exception ex)
    {
        StackTrace st = new StackTrace();
        if (ex != null)
        {
            lock (_syncRoot)
            {
                if (_log == null)
                    _log = new ParserOutputLogger();
            }

            WriteException(ex, st.ToString());
        }
    }
    /// <summary>
    /// Logs the exception.
    /// </summary>
    /// <param name="currentCallStack">
    /// The <see cref="Exception"/> instance to be logged.
    /// </param>
    public void LogException(Exception ex, string currentCallStack)
    {
        StackTrace st = new StackTrace();
        if (ex != null)
        {
            lock (_syncRoot)
            {
                if (_log == null)
                    _log = new ParserOutputLogger();
            }

            WriteException(ex, currentCallStack);
        }
    }
    /// <summary>
    /// Logs the exception.
    /// </summary>
    /// <param name="ex">The <see cref="Exception" /> instance to be logged.</param>
    /// <param name="member">The member.</param>
    /// <param name="file">The file.</param>
    /// <param name="line">The line.</param>
    public void LogException(Exception ex, string? member, string? file, int line)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Member: " + member);
        builder.AppendLine("File: " + file);
        builder.AppendLine("Line No: " + line);

        lock (_syncRoot)
        {
            if (_log == null)
                _log = new ParserOutputLogger();
        }

        _log.WriteLines(builder);
        StackTrace st = new StackTrace();
        if (ex != null)
        {
            WriteException(ex, st.ToString());
        }
    }
    /// <summary>
    /// Writes the exception to the output stream.
    /// </summary>
    /// <param name="ex">
    /// The <see cref="Exception"/> instance to be recorded.
    /// </param>
    /// <param name="currentCallStack">
    /// A string specifying the current call stack from which the Log Exception
    /// method was called.
    /// </param>
    public void WriteException(Exception ex, string currentCallStack)
    {
        StringBuilder builder = FormatException(ex, currentCallStack);
        lock (_syncRoot)
        {
            _log?.WriteLines(builder);
        }
    }
    /// <summary>
    /// Suspends writing to the log.
    /// </summary>
    public void Suspend()
    {
        lock (_syncRoot)
        {
            _log?.InstanceSuspend();
        }
    }
    /// <summary>
    /// Suspends writing to the stream instance.
    /// </summary>
    public void InstanceSuspend()
    {
        CloseStream();
    }
    /// <summary>
    /// Resumes writing to the log.
    /// </summary>
    public void Resume()
    {
        lock (_syncRoot)
        {
            _log?.InstanceResume();
        }
    }
    /// <summary>
    /// Re-opens the stream for writing.
    /// </summary>
    public void InstanceResume()
    {
        if (!string.IsNullOrEmpty(_fileName))
            CreateStream(_fileName);
    }
    /// <summary>
    /// Attempts to write the text to the output stream.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="StringBuilder"/> containing the text to be written.
    /// </param>
    public void WriteLines(StringBuilder builder)
    {
        if ((_writer != null) && (builder != null))
        {
            try
            {
                _writer.Write(builder.ToString());
                _writer.Flush();
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }
    }
    /// <summary>
    /// Writes the line to the log.
    /// </summary>
    /// <param name="content">
    /// A string containing the content to be written.
    /// </param>
    public void WriteLine(string content)
    {
        if ((_writer != null) && (content != null))
        {
            try
            {
                _writer.Write(content);
                _writer.Flush();
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Attempts to create the output steam to write to.
    /// </summary>
    private void CreateStream()
    {
        lock (_syncRoot)
        {
            string fileName = LocalFileName +
                              DateTime.Now.ToString(USDateFormat) +
                              LocalFileExtension;
            _fileName = fileName;
            CreateStream(_fileName);
        }
    }
    /// <summary>
    /// Attempts to create the output steam to write to.
    /// </summary>
    private void CreateStream(string fileName)
    {
        _fileName = fileName;
        try
        {
            _stream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
        }
        catch
        {
            _stream = null;
        }
        if (_stream != null)
        {
            _writer = new StreamWriter(_stream);
        }
    }
    /// <summary>
    /// Closes the output stream.
    /// </summary>
    private void CloseStream()
    {
        _writer?.Dispose();
        _stream?.Dispose();

        _writer = null;
        _stream = null;
    }
    #endregion
}