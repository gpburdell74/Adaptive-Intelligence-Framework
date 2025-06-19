using System.Text;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for the parsing operation(s) output logging system.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IParserOutputLogger : IDisposable
{
    /// <summary>
    /// Logs the exception.
    /// </summary>
    /// <param name="ex">
    /// The <see cref="Exception"/> instance to be logged.
    /// </param>
    void LogException(Exception ex);
    /// <summary>
    /// Logs the exception.
    /// </summary>
    /// <param name="ex">The <see cref="Exception" /> instance to be logged.</param>
    /// <param name="member">
    /// The member in which the exception occurred.
    /// </param>
    /// <param name="file">
    /// The name of the source file, if any.
    /// </param>
    /// <param name="line">
    /// The line number.
    /// </param>
    void LogException(Exception ex, string? member, string? file, int line);

    /// <summary>
    /// Logs the exception.
    /// </summary>
    /// <param name="ex">The <see cref="Exception" /> instance to be logged.</param>
    /// <param name="currentCallStack">
    /// A string containing the current call stack information.
    /// </param>
    void LogException(Exception ex, string currentCallStack);

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
    StringBuilder FormatException(Exception ex, string? currentCallStack);

    /// <summary>
    /// Suspends writing to the log.
    /// </summary>
    void Suspend();
    /// <summary>
    /// Suspends writing to the stream instance.
    /// </summary>
    void InstanceSuspend();
    /// <summary>
    /// Resumes writing to the log.
    /// </summary>
    void Resume();
    /// <summary>
    /// Re-opens the stream for writing.
    /// </summary>
    public void InstanceResume();
    /// <summary>
    /// Attempts to write the text to the output stream.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="StringBuilder"/> containing the text to be written.
    /// </param>
    void WriteLines(StringBuilder builder);
    /// <summary>
    /// Writes the line to the log.
    /// </summary>
    /// <param name="content">
    /// A string containing the content to be written.
    /// </param>
    void WriteLine(string content);
}