using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a division by zero exception.
/// </summary>
/// <seealso cref="BlazorBasicException" />
/// <seealso cref="ICodeBadDataTypeException"/>
public sealed class BasicDivisionByZeroException : BlazorBasicException, ICodeBadDataTypeException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDivisionByZeroException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicDivisionByZeroException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.DivisionByZero)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDivisionByZeroException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicDivisionByZeroException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.DivisionByZero, message)
    {
    }
    #endregion
}
