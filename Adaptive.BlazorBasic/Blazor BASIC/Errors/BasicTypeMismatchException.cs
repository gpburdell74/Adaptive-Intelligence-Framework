using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a data type mis-match exception.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public sealed class BasicTypeMismatchException : BlazorBasicException, ICodeTypeMismatchException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicTypeMismatchException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicTypeMismatchException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.TypeMismatch)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicTypeMismatchException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicTypeMismatchException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.TypeMismatch, message)
    {
    }
    #endregion
}
