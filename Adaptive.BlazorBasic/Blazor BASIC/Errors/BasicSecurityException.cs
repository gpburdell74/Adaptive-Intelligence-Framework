using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a general security exception.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public sealed class BasicSecurityException : BlazorBasicException, ICodeSecurityException 
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicSecurityException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicSecurityException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.PermissionDenied)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicSecurityException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicSecurityException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.PermissionDenied, message)
    {
    }
    #endregion
}
