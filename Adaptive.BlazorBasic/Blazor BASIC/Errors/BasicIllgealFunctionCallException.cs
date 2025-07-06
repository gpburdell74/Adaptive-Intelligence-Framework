using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents an illegal function call exception.
/// </summary>
/// <seealso cref="BlazorBasicException" />
/// <seealso cref="ICodeBadDataTypeException"/>
public sealed class BasicIllegalFunctionCallException : BlazorBasicException, ICodeIllegalFunctionCallException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicIllegalFunctionCallException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicIllegalFunctionCallException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.IllegalFunctionCall)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicBadDataTypeException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicIllegalFunctionCallException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.IllegalFunctionCall, message)
    {
    }
    #endregion
}
