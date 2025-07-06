using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a general I/O exception.
/// </summary>
/// <seealso cref="BlazorBasicException" />
public sealed class BasicIOException : BlazorBasicException, ICodeIOException 
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicIOException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BasicIOException(int lineNumber) : base(lineNumber, BlazorBasicErrorCodes.IOError)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicIOException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="message">
    /// A string containing the additional error message.
    /// </param>
    public BasicIOException(int lineNumber, string message) : base(lineNumber, BlazorBasicErrorCodes.IOError, message)
    {
    }
    #endregion
}
