using Adaptive.Intelligence.LanguageService.Providers;

namespace Adaptive.Intelligence.LanguageService.Dictionaries;

/// <summary>
/// Provides the signature definition for implementations for miscellaneous error dictionaries.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface IErrorDictionary<ErrorCodeType> : ICodeDictionary
    where ErrorCodeType : Enum
{
    /// <summary>
    /// Gets the error as specified by the provided text.
    /// </summary>
    /// <param name="errorText">
    /// A string containing the name of the built-in error.
    /// </param>
    /// <returns>
    /// A <typeparamref name="ErrorCodeType"/> enumerated value the unique ID value identifying the error.
    /// </returns>
    ErrorCodeType GetErrorType(string? errorText);

    /// <summary>
    /// Gets the text/name for the built-in error.
    /// </summary>
    /// <param name="errorCode">
    /// A <typeparamref name="ErrorCodeType"/> enumerated value containing the unique ID value identifying the error.  This can double as the error code.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified error in the language being implemented,
    /// or <b>null</b> if an invalid error name is specified.
    /// </returns>
    string? GetErrorText(ErrorCodeType errorCode);

    /// <summary>
    /// Populates the dictionary with the errors from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IErrorProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IErrorProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in error in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known error; otherwise, <c>false</c>.
    /// </returns>
    bool IsError(string? code);
}
