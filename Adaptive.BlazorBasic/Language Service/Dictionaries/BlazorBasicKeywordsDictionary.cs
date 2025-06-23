using Adaptive.Intelligence.LanguageService.Dictionaries;
using Adaptive.Intelligence.LanguageService.Providers;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides the built-in keywords dictionary for the Blazor BASIC language.
/// </summary>
/// <seealso cref="TwoWayDictionaryBase{K, V}" />
/// <seealso cref="IKeywordDictionary{T}" />
/// <seealso cref="BlazorBasicKeywords"/>
public sealed class BlazorBasicKeywordsDictionary : TwoWayDictionaryBase<string, BlazorBasicKeywords>, IKeywordDictionary<BlazorBasicKeywords>
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicKeywordsDictionary"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicKeywordsDictionary()
    {

    }
    #endregion

    #region Public Methods / Keywords    
    /// <summary>
    /// Gets the keyword as specified by the provided text.
    /// </summary>
    /// <param name="keyword">
    /// A string containing the name of the built-in keyword.
    /// </param>
    /// <returns>
    /// A <see cref="BlazorBasicKeywords"/>  enumerated value indicating the keyword.
    /// </returns>
    public BlazorBasicKeywords GetKeywordType(string? keyword)
    {
        return Get(NormalizeString(keyword));
    }

    /// <summary>
    /// Gets the text/name for the built-in keyword.
    /// </summary>
    /// <param name="keyword">
    /// A <see cref="BlazorBasicKeywords"/> enumerated value indicating the keyword.
    /// </param>
    /// <returns>
    /// A string containing the standard code/text for the specified keyword in the language being implemented,
    /// or <b>null</b> if an invalid keyword name is specified.
    /// </returns>
    public string? GetKeywordText(BlazorBasicKeywords keyword)
    {
        return ReverseGet(keyword);
    }

    /// <summary>
    /// Populates the dictionary with the keywords from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IKeywordProvider" /> provider instance used to provide the list.
    /// </param>
    public void InitializeDictionary(IKeywordProvider provider)
    {
        // Get the list of delimiters.
        List<string> names = provider.RenderKeywordNames();

        // Get the list of ID values and translate to the appropriate list of enumerations.
        List<int> idValues = provider.RenderKeywordIds();
        List<BlazorBasicKeywords> typeList = IdsToEnum<BlazorBasicKeywords>(idValues);
        idValues.Clear();

        BaseInitialize(names, typeList);

        typeList.Clear();
        names.Clear();
    }

    /// <summary>
    /// Gets a value indicating whether the specified code is a built-in keyword in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known keyword; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool IsKeyword(string? code)
    {
        return IsInDictionary(NormalizeString(code));
    }
    /// <summary>
    /// Normalizes the specified value for use in a dictionary as a key value.
    /// </summary>
    /// <param name="value">A string variable containing the value.</param>
    /// <returns>
    /// The normalized version of the value for use in a dictionary as a key.
    /// </returns>
    protected override string NormalizeKeyValue(string value)
    {
        return value.ToLower().Trim();
    }
    #endregion
}