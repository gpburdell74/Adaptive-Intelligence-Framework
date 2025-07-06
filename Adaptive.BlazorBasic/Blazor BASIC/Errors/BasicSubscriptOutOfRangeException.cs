using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a subscript out of range exception.
/// </summary>
/// <seealso cref="BlazorBasicException" />
/// <seealso cref="ICodeSubscriptOutOfRangeException"/>
public sealed class BasicSubscriptOutOfRangeException : BlazorBasicException, ICodeSubscriptOutOfRangeException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicSubscriptOutOfRangeException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicSubscriptOutOfRangeException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.SubscriptOutOfRange)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicSubscriptOutOfRangeException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicSubscriptOutOfRangeException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.SubscriptOutOfRange, message)
    {
    }
    #endregion
}
