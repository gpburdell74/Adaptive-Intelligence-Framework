using Adaptive.LanguageService.Providers;

namespace Adaptive.LanguageService;

/// <summary>
/// Provides the signature definition for implementations for miscellaneous parsing element dictionaries.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface IParsingElementDictionary : ICodeDictionary
{

    /// <summary>
    /// Gets the parsing element as specified by the provided text.
    /// </summary>
    /// <param name="parsingElement">
    /// A string containing the name of the built-in parsing element.
    /// </param>
    /// <returns>
    /// An integer containing the unique ID value identifying the parsing element.
    /// </returns>
    int GetParsingElementType(string? parsingElement);

    /// <summary>
    /// Gets the text/name for the built-in parsing element.
    /// </summary>
    /// <param name="parsingElement">
    /// An integer containing the unique ID value identifying the parsing element.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified parsing element in the language being implemented,
    /// or <b>null</b> if an invalid parsing element name is specified.
    /// </returns>
    string? GetParsingElementText(int parsingElement);

    /// <summary>
    /// Populates the dictionary with the parsing elements from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IParsingElementsProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IParsingElementsProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in parsing element in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known parsing element; otherwise, <c>false</c>.
    /// </returns>
    bool IsParsingElement(string? code);
}
