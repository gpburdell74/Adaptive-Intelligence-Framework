namespace Adaptive.Intelligence.LanguageService.Errors;

/// <summary>
/// Provides the signature definition for language-specific exceptions and errors.
/// </summary>
public interface ICodeException
{
    #region Public Properties
    /// <summary>
    /// Gets the error code for the exception.
    /// </summary>
    /// <value>
    /// An error code value.
    /// </value>
    int ErrorCode { get; }

    /// <summary>
    /// Gets the error text.
    /// </summary>
    /// <value>
    /// A string containing the standard error name text.
    /// </value>
    string? ErrorText { get; }

    /// <summary>
    /// Gets the line number on which the error occurred.
    /// </summary>
    /// <value>
    /// An integer indicating the line number value.
    /// </value>
    public int LineNumber { get; }
    #endregion

    #region Methods / Functions
    /// <summary>
    /// Gets the error text for the current error.
    /// </summary>
    /// <returns>
    /// A string containing the error name text.
    /// </returns>
    string? GetErrorText();
    #endregion
}