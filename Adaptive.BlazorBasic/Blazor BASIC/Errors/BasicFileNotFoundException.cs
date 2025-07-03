namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a syntax error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicFileNotFoundException : BlazorBasicException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileNotFoundException"/> class.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number on which the error occurred.</param>
    public BasicFileNotFoundException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.FileNotFound)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFileNotFoundException"/> class.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="message">The message.</param>
    public BasicFileNotFoundException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.FileNotFound, message)
    {
    }
    #endregion
}
