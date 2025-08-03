using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a general not-supported exception.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public sealed class BasicNotSupportedException : BlazorBasicException, ICodeNotSupportedException 
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicNotSupportedException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicNotSupportedException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.NotSupported)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicNotSupportedException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicNotSupportedException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.NotSupported, message)
    {
    }
    #endregion
}
