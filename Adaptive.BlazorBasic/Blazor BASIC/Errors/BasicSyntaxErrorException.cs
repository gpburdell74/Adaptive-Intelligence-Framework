using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a syntax error.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicSyntaxErrorException : BlazorBasicException, ICodeSyntaxErrorException 
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicSyntaxErrorException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicSyntaxErrorException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.Syntax)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicSyntaxErrorException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicSyntaxErrorException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.Syntax, message)
    {
    }
    #endregion
}
