using Adaptive.Intelligence.LanguageService.Dictionaries;
using Adaptive.Intelligence.LanguageService.Providers;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a two-way look-up dictionary for built-in functions.
/// </summary>
/// <seealso cref="TwoWayDictionaryBase{PrimaryValue, SecondaryValue}" />
/// <seealso cref="IBuiltInFunctionDictionary{T}" />
public sealed class BlazorBasicFunctionDictionary : TwoWayDictionaryBase<string, BlazorBasicFunctions>, IBuiltInFunctionDictionary<BlazorBasicFunctions>
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicFunctionDictionary"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicFunctionDictionary()
    {
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Gets the text/name for the built-in function.
    /// </summary>
    /// <param name="functionId">An integer containing the unique ID value identifying the function.</param>
    /// <returns>
    /// A string containing the standard code/text for the specified function in the language being implemented,
    /// or <b>null</b> if an invalid function name is specified.
    /// </returns>
    public string? GetBuiltInFunctionText(BlazorBasicFunctions functionId)
    {
        return ReverseGet(functionId);
    }

    /// <summary>
    /// Gets the function as specified by the provided text.
    /// </summary>
    /// <param name="functionText">A string containing the name of the built-in function.</param>
    /// <returns>
    /// An integer containing the unique ID value identifying the function.
    /// </returns>
    public BlazorBasicFunctions GetBuiltInFunctionType(string? functionText)
    {
        return Get(functionText);
    }

    /// <summary>
    /// Populates the dictionary with the functions from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IBuiltInFunctionProvider" /> provider instance used to provide the list.
    /// </param>
    public void InitializeDictionary(IBuiltInFunctionProvider provider)
    {
        // Get the list of delimiters.
        List<string> names = provider.RenderFunctionNames();

        // Get the list of ID values and translate to the appropriate list of enumerations.
        List<int> idValues = provider.RenderFunctionIds();
        List<BlazorBasicFunctions> typeList = IdsToEnum<BlazorBasicFunctions>(idValues);
        idValues.Clear();

        BaseInitialize(names, typeList);

        typeList.Clear();
        names.Clear();
    }

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in function in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known function; otherwise, <c>false</c>.
    /// </returns>
    public bool IsBuiltInFunction(string? code)
    {
        return IsInDictionary(code);
    }
    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Normalizes the specified value for use in a dictionary as a key value.
    /// </summary>
    /// <param name="value">A string variable containing the value.</param>
    /// <returns>
    /// The normalized version of the value for use in a dictionary as a key.
    /// </returns>
    protected override string NormalizeKeyValue(string value)
    {
        string? normalized = NormalizeString(value);
        if (normalized == null)
            return string.Empty;
        else
            return normalized;
    }
    #endregion
}
