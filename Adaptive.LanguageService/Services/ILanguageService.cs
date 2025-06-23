using Adaptive.Intelligence.LanguageService.Dictionaries;

namespace Adaptive.Intelligence.LanguageService.Services;

/// <summary>
/// Provides the signature definition for a basic language service.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ILanguageService: IDisposable
{
    #region Properties
    /// <summary>
    /// Gets the reference to the data types dictionary.
    /// </summary>
    /// <value>
    /// The <see cref="IDataTypeDictionary"/> containing the list of valid data type definitions.
    /// </value>
    IDataTypeDictionary DataTypes { get; }

    /// <summary>
    /// Gets the reference to the parsing elements dictionary.
    /// </summary>
    /// <value>
    /// The <see cref="IParsingElementDictionary"/> containing the list of valid parsing elements.
    /// </value>
    IParsingElementDictionary ParsingElements { get; }
    #endregion
}