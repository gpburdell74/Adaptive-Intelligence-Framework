using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Dictionaries;
using Adaptive.Intelligence.LanguageService.Providers;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a two-way look-up dictionary for data types.
/// </summary>
/// <seealso cref="TwoWayDictionaryBase{PrimaryValue, SecondaryValue}" />
/// <seealso cref="IDataTypeDictionary" />
public sealed class BlazorBasicDataTypeDictionary : TwoWayDictionaryBase<string, StandardDataTypes>, IDataTypeDictionary
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

    #region Public Methods / DataTypes
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
    /// Populates the dictionary with the data types from the specified language provider.
    /// </summary>
    /// <param name="provider">The <see cref="IDataTypeProvider" /> provider instance used to provide the list.</param>
    public void InitializeDictionary(IDataTypeProvider provider)
    {
        // Get the list of data type names.
        List<string> names = provider.RenderDataTypeNames();

        // Get the list of ID values and translate to the appropriate list of enumerations.
        List<int> idValues = provider.RenderDataTypeIds();
        List<StandardDataTypes> typeList = IdsToEnum<StandardDataTypes>(idValues);
        idValues.Clear();

        BaseInitialize(names, typeList);

        typeList.Clear();
        names.Clear();
    }

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in data type in the language being implemented.
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

    #region Protected Method Overrides    
    /// <summary>
    /// Normalizes the specified value for use in a dictionary as a key value.
    /// </summary>
    /// <param name="value">
    /// A <see cref="string"/> variable containing the value.
    /// </param>
    /// <returns>
    /// The normalized version of the value for use in a dictionary as a key.
    /// </returns>
    protected override string NormalizeKeyValue(string value)
    {
        return value.ToLower().Trim();
    }
    #endregion


}
