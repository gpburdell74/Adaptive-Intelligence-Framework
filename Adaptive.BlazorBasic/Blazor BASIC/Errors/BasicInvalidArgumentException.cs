namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a syntax error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicInvalidArgumentException : BlazorBasicException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInvalidArgumentException"/> class.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number on which the error occurred.</param>
    public BasicInvalidArgumentException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.InvalidArgument)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInvalidArgumentException"/> class.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="message">The message.</param>
    public BasicInvalidArgumentException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.InvalidArgument, message)
    {
    }
    #endregion
}
