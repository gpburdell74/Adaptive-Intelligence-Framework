using Adaptive.Intelligence.LanguageService.Providers;

namespace Adaptive.Intelligence.LanguageService.Dictionaries;

/// <summary>
/// Provides the signature definition for a data type dictionary.
/// </summary>
/// <seealso cref="ICodeDictionary" />
public interface IDataTypeDictionary : ICodeDictionary
{
    /// <summary>
    /// Gets the data type as specified by the provided text.
    /// </summary>
    /// <param name="dataTypeName">A string containing the name of the data type.</param>
    /// <returns>
    /// A <see cref="StandardDataTypes" /> enumerated value indicating the data type.
    /// </returns>
    StandardDataTypes GetDataType(string? dataTypeName);

    /// <summary>
    /// Gets the name of the data type.
    /// </summary>
    /// <param name="dataType">A <see cref="StandardDataTypes" /> enumerated value indicating the data type.</param>
    /// <returns>
    /// A string containing the standard code/text for the specified data type in the language being implemented,
    /// or <b>null</b> if an invalid data type is specified.
    /// </returns>
    string? GetDataTypeName(StandardDataTypes dataType);

    /// <summary>
    /// Populates the dictionary with the data types from the specified language service.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IDataTypeProvider" /> provider instance used to provide the list.
    /// </param>
    void InitializeDictionary(IDataTypeProvider provider);

    /// <summary>
    /// Gets a value indicating whether the specified code is a data type in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known data type; otherwise, <c>false</c>.
    /// </returns>
    bool IsDataType(string? code);
}
