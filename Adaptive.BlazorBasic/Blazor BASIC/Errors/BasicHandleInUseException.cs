using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a handle already in use exception.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public sealed class BasicHandleInUseException : BlazorBasicException, ICodeHandleInUseException
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicHandleInUseException"/> class.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number on which the error occurred.</param>
    public BasicHandleInUseException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.FileHandleInUse)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicHandleInUseException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicHandleInUseException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.FileHandleInUse, message)
    {
    }
    #endregion
}