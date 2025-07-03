namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a syntax error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicNotSupportedException : BlazorBasicException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicNotSupportedException"/> class.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number on which the error occurred.</param>
    public BasicNotSupportedException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.NotSupported)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicNotSupportedException"/> class.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="message">The message.</param>
    public BasicNotSupportedException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.NotSupported, message)
    {
    }
    #endregion
}
