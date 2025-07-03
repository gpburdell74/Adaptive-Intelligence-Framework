namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a syntax error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicSecurityException : BlazorBasicException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicSecurityException"/> class.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number on which the error occurred.</param>
    public BasicSecurityException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.PermissionDenied)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicSecurityException"/> class.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="message">The message.</param>
    public BasicSecurityException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.PermissionDenied, message)
    {
    }
    #endregion
}
