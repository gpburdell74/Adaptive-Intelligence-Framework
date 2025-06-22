using Adaptive.LanguageService;
using Adaptive.LanguageService.Providers;
using Adaptive.LanguageService.Tokenization;

namespace Adaptive.BlazorBasic.LanguageService;


/// <summary>
/// Provides the delimiter dictionary for the Blazor BASIC language.
/// </summary>
/// <seealso cref="TwoWayDictionaryBase{K, V}" />
/// <seealso cref="IDelimiterDictionary{T}" />
public sealed class BlazorBasicDelimiterDictionary : TwoWayDictionaryBase<string, BlazorBasicDelimiters>, IDelimiterDictionary<BlazorBasicDelimiters>
{
    #region Private Member Declarations
    /// <summary>
    /// The token map.
    /// </summary>
    private Dictionary<BlazorBasicDelimiters, TokenType>? _tokenMap;
    /// <summary>
    /// The delimiter type list.
    /// </summary>
    private Dictionary<BlazorBasicDelimiters, StandardDelimiterTypes>? _typeList;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicDelimiterDictionary"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicDelimiterDictionary()
    {
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _tokenMap?.Clear();
            _typeList?.Clear();
        }

        _typeList = null;
        _tokenMap = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Gets the delimiter as specified by the provided text.
    /// </summary>
    /// <param name="delimiter">A string containing the delimiter.</param>
    /// <returns>
    /// An integer containing the unique ID value identifying the delimiter.
    /// </returns>
    public BlazorBasicDelimiters GetDelimiter(string? delimiter)
    {
        if (string.IsNullOrEmpty(delimiter))
            throw new Exception();

        return Get(delimiter);
    }
    /// <summary>
    /// Gets the text for the delimiter.
    /// </summary>
    /// <param name="delimiter">A <see cref="!:DelimiterListType" /> enumerated value indicating the delimiter.</param>
    /// <returns>
    /// A string containing the standard code/text for the specified delimiter in the language being implemented,
    /// or <b>null</b> if an invalid delimiter is specified.
    /// </returns>
    public string? GetDelimiterText(BlazorBasicDelimiters delimiter)
    {
        return ReverseGet(delimiter);
    }
    /// <summary>
    /// Gets the type of delimiter as specified by the provided text.
    /// </summary>
    /// <param name="delimiter">A string containing the name of the delimiter.</param>
    /// <returns>
    /// A <see cref="StandardDelimiterTypes" /> enumerated value indicating the standard delimiter type.
    /// </returns>
    public StandardDelimiterTypes GetDelimiterType(string? delimiter)
    {
        if (_typeList == null)
            throw new Exception();

        return _typeList[GetDelimiter(delimiter)];
    }

    /// <summary>
    /// Gets the type of the token used to represent the specific delimiter when parsing.
    /// </summary>
    /// <param name="delimiter">A string containing the delimiter text.</param>
    /// <returns>
    /// A <see cref="TokenType" /> enumerated value indicating the token type.
    /// </returns>
    public TokenType GetTokenType(string? delimiter)
    {
        if (string.IsNullOrEmpty(delimiter))
            return TokenType.NoneOrUnknown;

        return GetTokenType(GetDelimiter(delimiter));
    }

    /// <summary>
    /// Gets the type of the token used to represent the specific delimiter when parsing.
    /// </summary>
    /// <param name="delimiterType"></param>
    /// <returns>
    /// A <see cref="TokenType" /> enumerated value indicating the token type.
    /// </returns>
    public TokenType GetTokenType(BlazorBasicDelimiters delimiterType)
    {
        if (_tokenMap == null || !_tokenMap.ContainsKey(delimiterType))
            throw new Exception();

        return _tokenMap[delimiterType];
    }

    /// <summary>
    /// Populates the dictionary with the delimiters from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IDelimiterProvider" /> provider instance used to provide the list.
    /// </param>
    public void InitializeDictionary(IDelimiterProvider provider)
    {
        // Get the list of delimiters.
        List<string> names = provider.RenderDelimiterNames();

        // Get the list of ID values and translate to the appropriate list of enumerations.
        List<int> idValues = provider.RenderDelimiterIds();
        List<BlazorBasicDelimiters> typeList = IdsToEnum<BlazorBasicDelimiters>(idValues);
        idValues.Clear();

        BaseInitialize(names, typeList);

        typeList.Clear();
        names.Clear();

        // Initialize the token list.
        _tokenMap = new Dictionary<BlazorBasicDelimiters, TokenType>(12)
        {
            { BlazorBasicDelimiters.Space, TokenType.SeparatorDelimiter },
            { BlazorBasicDelimiters.Cr, TokenType.SeparatorDelimiter },
            { BlazorBasicDelimiters.Lf, TokenType.SeparatorDelimiter },
            { BlazorBasicDelimiters.Char, TokenType.CharacterDelimiter },
            { BlazorBasicDelimiters.String, TokenType.StringDelimiter },
            { BlazorBasicDelimiters.OpenParens, TokenType.ExpressionStartDelimiter },
            { BlazorBasicDelimiters.CloseParens, TokenType.ExpressionEndDelimiter },
            { BlazorBasicDelimiters.OpenBracket, TokenType.SizingStartDelimiter },
            { BlazorBasicDelimiters.CloseBracket, TokenType.SizingEndDelimiter },
            { BlazorBasicDelimiters.OpenBlockBracket, TokenType.BlockStartDelimiter },
            { BlazorBasicDelimiters.CloseBlockBracket, TokenType.BlockEndDelimiter },
            { BlazorBasicDelimiters.ListSeparator, TokenType.SeparatorDelimiter }
        };

        // Initialize the type list.
        _typeList = new Dictionary<BlazorBasicDelimiters, StandardDelimiterTypes>(12)
        {
            { BlazorBasicDelimiters.Space, StandardDelimiterTypes.Separator },
            { BlazorBasicDelimiters.Cr, StandardDelimiterTypes.Separator },
            { BlazorBasicDelimiters.Lf, StandardDelimiterTypes.Separator },
            { BlazorBasicDelimiters.Char, StandardDelimiterTypes.CharacterLiteral },
            { BlazorBasicDelimiters.String, StandardDelimiterTypes.StringLiteral },
            { BlazorBasicDelimiters.OpenParens, StandardDelimiterTypes.ExpressionStart },
            { BlazorBasicDelimiters.CloseParens, StandardDelimiterTypes.ExpressionEnd },
            { BlazorBasicDelimiters.OpenBracket, StandardDelimiterTypes.SizingStart },
            { BlazorBasicDelimiters.CloseBracket, StandardDelimiterTypes.SizingEnd },
            { BlazorBasicDelimiters.OpenBlockBracket, StandardDelimiterTypes.BlockStart },
            { BlazorBasicDelimiters.CloseBlockBracket, StandardDelimiterTypes.BlockEnd },
            { BlazorBasicDelimiters.ListSeparator, StandardDelimiterTypes.Separator }
        };
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
        return IsInDictionary(code);
    }
    /// <summary>
    /// Normalizes the specified value for use in a dictionary as a key value.
    /// </summary>
    /// <param name="value">A  variable containing the value.</param>
    /// <returns>
    /// The normalized version of the value for use in a dictionary as a key.
    /// </returns>
    protected override string NormalizeKeyValue(string value)
    {
        return value;
    }
    #endregion
}
