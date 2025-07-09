using Adaptive.Intelligence.LanguageService.Providers;

namespace Adaptive.Intelligence.LanguageService.Dictionaries;

/// <summary>
/// Provides the signature definition for implementations for keyword dictionaries.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface IKeywordDictionary<KeywordType> : ICodeDictionary 
    where KeywordType : Enum
{
    /// <summary>
    /// Gets the keyword as specified by the provided text.
    /// </summary>
    /// <param name="keyword">
    /// A string containing the name of the built-in keyword.
    /// </param>
    /// <returns>
    /// A <typeparamref name="KeywordType"/> enumerated value containing the unique ID value identifying the keyword.
    /// </returns>
    KeywordType GetKeywordType(string? keyword);

    /// <summary>
    /// Gets the text/name for the built-in keyword.
    /// </summary>
    /// <param name="keyword">
    /// A <typeparamref name="KeywordType"/> enumerated value containing the unique ID value identifying the keyword.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified keyword in the language being implemented,
    /// or <b>null</b> if an invalid keyword name is specified.
    /// </returns>
    string? GetKeywordText(KeywordType keyword);

    /// <summary>
    /// Populates the dictionary with the keywords from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IKeywordProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IKeywordProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in keyword in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known keyword; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    bool IsKeyword(string? code);
}
