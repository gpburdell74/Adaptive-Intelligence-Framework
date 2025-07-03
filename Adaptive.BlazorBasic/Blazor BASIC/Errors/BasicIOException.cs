namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a syntax error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicIOException : BlazorBasicException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicIOException"/> class.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number on which the error occurred.</param>
    public BasicIOException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.IOError)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicIOException"/> class.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="message">The message.</param>
    public BasicIOException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.IOError, message)
    {
    }
    #endregion
}
