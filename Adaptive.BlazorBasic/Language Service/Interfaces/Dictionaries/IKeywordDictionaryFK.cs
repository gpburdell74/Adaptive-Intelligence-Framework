namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for implementing a keyword dictionary for language service.
/// </summary>
/// <seealso cref="IDisposable" />
/// <typeparam name="FunctionsEnum">
/// The data type of the numeration defining the list of supported functions.
/// </typeparam>
/// <typeparam name="KeywordsEnum">
/// The data type of the numeration defining the list of supported keywords.
/// </typeparam>
public interface IKeywordDictionary<FunctionsEnum, KeywordsEnum> : ILanguageDefinitionDictionary
    where FunctionsEnum : Enum
    where KeywordsEnum : Enum
{
    /// <summary>
    /// Gets the keyword as specified by the provided text.
    /// </summary>
    /// <param name="keyword">
    /// A string containing the name of the keyword.
    /// </param>
    /// <returns>
    /// A <typeparamref name="T"/> enumerated value indicating the keyword.
    /// </returns>
    KeywordsEnum GetKeywordType(string? keyword);

    /// <summary>
    /// Gets the text/name for the built-in keyword.
    /// </summary>
    /// <param name="keyword">
    /// A <typeparamref name="T"/> enumerated value indicating the keyword.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified keyword in the language being implemented,
    /// or <b>null</b> if an invalid keyword name is specified.
    /// </returns>
    string? GetKeywordText(KeywordsEnum keyword);

    /// <summary>
    /// Populates the dictionary with the keywords from the specified language service.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="ILanguageService{F, K}"/> service instance used to provide the list of keywords.
    /// </param>
    void InitializeDictionary(ILanguageService<FunctionsEnum, KeywordsEnum> provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a keyword in the language being implemented.
    /// </summary>
    /// <param name="code">
    /// A string containing the code text to be evaluated.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the text can be matched to a known keyword; otherwise, <c>false</c>.
    /// </returns>
    bool IsKeyword(string? code);
}
