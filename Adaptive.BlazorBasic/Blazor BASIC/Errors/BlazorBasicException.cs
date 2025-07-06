using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.Errors;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents an error / exception in the Blazor BASIC language.
/// </summary>
/// <seealso cref="Exception" />
public class BlazorBasicException : Exception, ICodeException 
{
    #region Private Member Declarations
    /// <summary>
    /// The line number of the error.
    /// </summary>
    private int _lineNumber;

    /// <summary>
    /// The error code for the exception.
    /// </summary>
    private BlazorBasicErrorCodes _errorCode = BlazorBasicErrorCodes.Success;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BlazorBasicException(int lineNumber) : base()
    {
        _lineNumber = lineNumber;
        _errorCode = BlazorBasicErrorCodes.GeneralError;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicException"/> class.
    /// </summary>
    /// <param name="errorCode">
    /// A <see cref="BlazorBasicErrorCodes"/> enumerated value indicating the error being represented.
    /// </param>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    public BlazorBasicException(int lineNumber, BlazorBasicErrorCodes errorCode)
    {
        _lineNumber = lineNumber;
        _errorCode = errorCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicException"/> class.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number on which the error occurred.
    /// </param>
    /// <param name="errorCode">
    /// A <see cref="BlazorBasicErrorCodes"/> enumerated value indicating the error being represented.
    /// </param>
    /// <param name="message">
    /// A string containing additional error information.
    /// </param>
    public BlazorBasicException(int lineNumber, BlazorBasicErrorCodes errorCode, string message) : base(message)
    {
        _lineNumber = lineNumber;
        _errorCode = errorCode;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the error code for the exception.
    /// </summary>
    /// <value>
    /// A <see cref="BlazorBasicErrorCodes"/> enumerated value indicating the error code.
    /// </value>
    public BlazorBasicErrorCodes BasicErrorCode => _errorCode;

    /// <summary>
    /// Gets the error code for the exception.
    /// </summary>
    /// <value>
    /// An error code value.
    /// </value>
    public int ErrorCode => (int)_errorCode;

    /// <summary>
    /// Gets the error text.
    /// </summary>
    /// <value>
    /// A string containing the standard error name text.
    /// </value>
    public string? ErrorText => GetErrorText();

    /// <summary>
    /// Gets the line number on which the error occurred.
    /// </summary>
    /// <value>
    /// An integer indicating the line number value.
    /// </value>
    public int LineNumber => _lineNumber;
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Gets the error text for the current error.
    /// </summary>
    /// <returns>
    /// A string containing the error name text.
    /// </returns>
    public virtual string? GetErrorText()
    {
        return BlazorBasicLanguageService.Instance?.Errors?.GetErrorText(_errorCode);
    }
    #endregion
}
