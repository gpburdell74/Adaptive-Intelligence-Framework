using Adaptive.Intelligence.LanguageService.Providers;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.LanguageService.Dictionaries;

/// <summary>
/// Provides the signature definition for delimiters in the language.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface IDelimiterDictionary<DelimiterListType> : ICodeDictionary
    where DelimiterListType : Enum
{
    /// <summary>
    /// Gets the type of delimiter as specified by the provided text.
    /// </summary>
    /// <param name="delimiter">A string containing the name of the delimiter.</param>
    /// <returns>
    /// A <see cref="StandardDataTypes" /> enumerated value indicating the standard delimiter type.
    /// </returns>
    StandardDelimiterTypes GetDelimiterType(string? delimiter);

    /// <summary>
    /// Gets the delimiter as specified by the provided text.
    /// </summary>
    /// <param name="delimiter">
    /// A string containing the delimiter.
    /// </param>
    /// <returns>
    /// A <see cref="DelimiterListType"/> enumerated value containing the unique ID value identifying the delimiter.
    /// </returns>
    DelimiterListType GetDelimiter(string? delimiter);

    /// <summary>
    /// Gets the text for the delimiter.
    /// </summary>
    /// <param name="delimiter">
    /// A <see cref="DelimiterListType" /> enumerated value indicating the delimiter.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified delimiter in the language being implemented,
    /// or <b>null</b> if an invalid delimiter is specified.
    /// </returns>
    string? GetDelimiterText(DelimiterListType delimiter);

    /// <summary>
    /// Gets the type of the token used to represent the specific delimiter when parsing.
    /// </summary>
    /// <param name="delimiter">A string containing the delimiter text.</param>
    /// <returns>
    /// A <see cref="TokenType" /> enumerated value indicating the token type.
    /// </returns>
    TokenType GetTokenType(string? delimiter);

    /// <summary>
    /// Gets the type of the token used to represent the specific delimiter when parsing.
    /// </summary>
    /// <param name="delimiterType">
    /// A <see cref="DelimiterListType"/> enumerated value indicating the delimiter.
    /// </param>
    /// <returns>
    /// A <see cref="TokenType" /> enumerated value indicating the token type.
    /// </returns>
    TokenType GetTokenType(DelimiterListType delimiterType);

    /// <summary>
    /// Populates the dictionary with the delimiters from the specified language provider.
    /// </summary>
    /// <param name="service">
    /// The <see cref="ILanguageService" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IDelimiterProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a delimiter in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known delimiter; otherwise, <c>false</c>.
    /// </returns>
    bool IsDelimiter(string? code);
}
