using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents an arithmetic overflow exception.
/// </summary>
/// <seealso cref="BlazorBasicException" />
/// <seealso cref="ICodeOverflowException"/>
public sealed class BasicOverflowException : BlazorBasicException, ICodeOverflowException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicOverflowException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicOverflowException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.Overflow)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicOverflowException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicOverflowException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.Overflow, message)
    {
    }
    #endregion
}
