using Adaptive.BlazorBasic.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic;

/// <summary>
/// Provides the data type dictionary for the Blazor BASIC language.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IDataTypeDictionary" />
public class BlazorBasicDataTypeDictionary : TwoWayDictionaryBase<string, StandardDataTypes>, IDataTypeDictionary
{
    #region Constructor 
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicDataTypeDictionary"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicDataTypeDictionary()
    {
    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Gets the data type as specified by the provided text.
    /// </summary>
    /// <param name="dataTypeName">A string containing the name of the data type.</param>
    /// <returns>
    /// A <see cref="StandardDataTypes" /> enumerated value indicating the data type.
    /// </returns>
    public StandardDataTypes GetDataType(string? dataTypeName)
    {
        return Get(NormalizeString(dataTypeName));
    }
    /// <summary>
    /// Gets the name of the data type.
    /// </summary>
    /// <param name="dataType">A <see cref="StandardDataTypes" /> enumerated value indicating the data type.</param>
    /// <returns>
    /// A string containing the standard code/text for the specified data type in the language being implemented,
    /// or <b>null</b> if an invalid data type is specified.
    /// </returns>
    public string? GetDataTypeName(StandardDataTypes dataType)
    {
        return ReverseGet(dataType);
    }

    /// <summary>
    /// Populates the dictionary with the data types from the specified language service.
    /// </summary>
    /// <param name="service">
    /// The <see cref="ILanguageService" /> service instance used to provide the list.
    /// </param>
    public void InitializeDictionary(ILanguageService service)
    {
        List<string> dataTypeNames = service.RenderDataTypes();

        foreach (string dataTypeName in dataTypeNames)
        {
            AddEntry(dataTypeName, NormalizeString(dataTypeName), service.MapDataType(dataTypeName));
        }
    }
    /// <summary>
    /// Gets a value indicating whether the specified code is a data type in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known data type; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDataType(string? code)
    {
        return IsInDictionary(code);
    }
    #endregion
}