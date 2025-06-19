using Adaptive.BlazorBasic.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic;

/// <summary>
/// Provides the delimiter dictionary for the Blazor BASIC language.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IDelimiterDictionary" />
public class BlazorBasicDelimiterDictionary : DisposableObjectBase, IDelimiterDictionary
{
    #region Private Member Declarations    
    /// <summary>
    /// The list.
    /// </summary>
    private Dictionary<string, StandardDelimiterTypes>? _list;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicDelimiterDictionary"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicDelimiterDictionary()
    {
        _list = new Dictionary<string, StandardDelimiterTypes>();
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
            _list?.Clear();

        _list = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Gets the delimiter as specified by the provided text.
    /// </summary>
    /// <param name="delimiter">A string containing the name of the delimiter.</param>
    /// <returns>
    /// A <see cref="StandardDataTypes" /> enumerated value indicating the delimiter.
    /// </returns>
    public StandardDelimiterTypes GetDelimiterType(string? delimiter)
    {
        if (delimiter == null || _list == null || !_list.ContainsKey(delimiter))
            return StandardDelimiterTypes.Unknown;
        else
            return _list[delimiter];
    }
    /// <summary>
    /// Gets the text for the delimiter.
    /// </summary>
    /// <param name="delimiter">A <see cref="T:Adaptive.Intelligence.LanguageService.StandardDelimiterTypes" /> enumerated value indicating the delimiter.</param>
    /// <returns>
    /// A string containing the standard code/text for the specified delimiter in the language being implemented,
    /// or <b>null</b> if an invalid delimiter is specified.
    /// </returns>
    public string? GetDelimiterText(StandardDelimiterTypes delimiter)
    {
        return string.Empty;
    }

    /// <summary>
    /// Populates the dictionary with the delimiters from the specified language provider.
    /// </summary>
    /// <param name="service">
    /// The <see cref="ILanguageService" /> provider instance used to provide the list.
    /// </param>
    public void InitializeDictionary(ILanguageService service)
    {
        List<string> delimiters = service.RenderDelimiters();

        foreach (string delimiter in delimiters)
        {
            _list!.Add(delimiter, service.MapDelimiter(delimiter));
        }
        delimiters.Clear();
    }
    /// <summary>
    /// Gets a value indicating whether the specified code is a delimiter in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known delimiter; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDelimiter(string? code)
    {
        if (code == null)
            return false;

        return _list!.ContainsKey(code);
    }

    /// <summary>
    /// Renders a list of the unique keys in the dictionary.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of <see cref="string" /> values containing the unique list of
    /// text items stored in the dictionary.
    /// </returns>
    public List<string> RenderUniqueKeys()
    {
        List<string> keyList = new List<string>(30);
        if (_list != null)
            keyList.AddRange(_list.Keys);
        return keyList;
    }
    #endregion
}
