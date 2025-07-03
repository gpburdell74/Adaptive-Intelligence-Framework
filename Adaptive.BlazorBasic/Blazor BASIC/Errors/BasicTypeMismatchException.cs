namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a syntax error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicTypeMismatchException : BlazorBasicException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicTypeMismatchException"/> class.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number on which the error occurred.</param>
    public BasicTypeMismatchException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.TypeMismatch)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicTypeMismatchException"/> class.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="message">The message.</param>
    public BasicTypeMismatchException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.TypeMismatch, message)
    {
    }
    #endregion
}
