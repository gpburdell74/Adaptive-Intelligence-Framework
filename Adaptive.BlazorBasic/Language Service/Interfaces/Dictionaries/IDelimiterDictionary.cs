namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for implementing a delimiter dictionary for a language service.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IDelimiterDictionary : ILanguageDefinitionDictionary
{
    /// <summary>
    /// Gets the delimiter as specified by the provided text.
    /// </summary>
    /// <param name="delimiter">
    /// A string containing the name of the delimiter.
    /// </param>
    /// <returns>
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the delimiter.
    /// </returns>
    StandardDelimiterTypes GetDelimiterType(string? delimiter);

    /// <summary>
    /// Gets the text for the delimiter.
    /// </summary>
    /// <param name="delimiter">
    /// A <see cref="StandardDelimiterTypes"/> enumerated value indicating the delimiter.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified delimiter in the language being implemented,
    /// or <b>null</b> if an invalid delimiter is specified.
    /// </returns>
    string? GetDelimiterText(StandardDelimiterTypes delimiter);

    /// <summary>
    /// Populates the dictionary with the delimiters from the specified language provider.
    /// </summary>
    /// <param name="service">
    /// The <see cref="ILanguageService"/> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(ILanguageService service);

    /// <summary>
    /// Gets a value indicating whether the specified code is a delimiter in the language being implemented.
    /// </summary>
    /// <param name="code">
    /// A string containing the code text to be evaluated.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the text can be matched to a known delimiter; otherwise, <c>false</c>.
    /// </returns>
    bool IsDelimiter(string? code);
}
