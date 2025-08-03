using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents an invalid argument specified or supplied exception.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public class BasicInvalidArgumentException : BlazorBasicException, ICodeInvalidArgumentException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInvalidArgumentException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicInvalidArgumentException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.InvalidArgument)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInvalidArgumentException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicInvalidArgumentException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.InvalidArgument, message)
    {
    }
    #endregion
}
