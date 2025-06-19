namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for implementing a data type dictionary for language service.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IDataTypeDictionary : ILanguageDefinitionDictionary
{
    /// <summary>
    /// Gets the data type as specified by the provided text.
    /// </summary>
    /// <param name="dataTypeName">
    /// A string containing the name of the data type.
    /// </param>
    /// <returns>
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the data type.
    /// </returns>
    StandardDataTypes GetDataType(string? dataTypeName);
    /// <summary>
    /// Gets the name of the data type.
    /// </summary>
    /// <param name="dataType">
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the data type.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified data type in the language being implemented,
    /// or <b>null</b> if an invalid data type is specified.
    /// </returns>
    string? GetDataTypeName(StandardDataTypes dataType);

    /// <summary>
    /// Populates the dictionary with the data types from the specified language service.
    /// </summary>
    /// <param name="service">
    /// The <see cref="ILanguageService"/> provider instance used to provide the data type list.
    /// </param>
    void InitializeDictionary(ILanguageService service);

    /// <summary>
    /// Gets a value indicating whether the specified code is a data type in the language being implemented.
    /// </summary>
    /// <param name="code">
    /// A string containing the code text to be evaluated.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the text can be matched to a known data type; otherwise, <c>false</c>.
    /// </returns>
    bool IsDataType(string? code);
}
