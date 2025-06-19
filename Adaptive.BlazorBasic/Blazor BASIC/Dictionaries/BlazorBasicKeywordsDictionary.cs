using Adaptive.BlazorBasic.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic;

/// <summary>
/// Provides the built-in keywords dictionary for the Blazor BASIC language.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IKeywordDictionary{F, K}" />
/// <seealso cref="BlazorBasicKeywords"/>
public class BlazorBasicKeywordsDictionary : TwoWayDictionaryBase<string, BlazorBasicKeywords>, IKeywordDictionary<BlazorBasicFunctions, BlazorBasicKeywords>
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicKeywordsDictionary"/> class.
    /// </summary>
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
    /// <param name="service">
    /// The <see cref="ILanguageService{F, K}" /> provider instance used to provide the list.
    /// </param>
    public void InitializeDictionary(ILanguageService<BlazorBasicFunctions, BlazorBasicKeywords> service)
    {
        List<string> keywordList = service.RenderKeywordNames();

        foreach (string keywordName in keywordList)
        {
            AddEntry(keywordName, NormalizeString(keywordName), service.MapKeyword(keywordName));
        }
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
    #endregion
}