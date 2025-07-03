namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a syntax error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicHandleInUseException : BlazorBasicException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicHandleInUseException"/> class.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number on which the error occurred.</param>
    public BasicHandleInUseException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.FileHandleInUse)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicHandleInUseException"/> class.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="message">The message.</param>
    public BasicHandleInUseException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.FileHandleInUse, message)
    {
    }
    #endregion
}
