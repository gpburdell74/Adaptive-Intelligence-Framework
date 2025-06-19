using Adaptive.BlazorBasic.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic;

/// <summary>
/// Provides the operator dictionary for the Blazor BASIC language.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IOperatorDictionary" />
public class BlazorBasicOperatorDictionary : TwoWayDictionaryBase<string, StandardOperatorTypes>, IOperatorDictionary
{
    #region Private Member Declarations
    /// <summary>
    /// The delimiter type to token type map.
    /// </summary>
    private Dictionary<StandardOperatorTypes, TokenType>? _tokenMap;
    #endregion

    #region Constructor 
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicOperatorDictionary"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicOperatorDictionary()
    {
        _tokenMap = new Dictionary<StandardOperatorTypes, TokenType>();
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Gets the operator as specified by the provided text.
    /// </summary>
    /// <param name="operatorText">
    /// A string containing the code representation of the operator.
    /// </param>
    /// <returns>
    /// A <see cref="StandardOperatorTypes" /> enumerated value indicating the operator.
    /// </returns>
    public StandardOperatorTypes GetOperator(string? operatorText)
    {
        return Get(NormalizeString(operatorText));
    }

    /// <summary>
    /// Gets the name of the operator.
    /// </summary>
    /// <param name="operatorType">A <see cref="!:StandardOperators" /> enumerated value indicating the operator.</param>
    /// <returns>
    /// A string containing the standard code/text for the specified operator in the language being implemented,
    /// or <b>null</b> if an invalid operator is specified.
    /// </returns>
    public string? GetOperatorName(StandardOperatorTypes operatorType)
    {
        return ReverseGet(operatorType);
    }

    /// <summary>
    /// Populates the dictionary with the operators from the specified language provider.
    /// </summary>
    /// <param name="provider">The <see cref="ILanguageService" /> provider instance used to provide the list.</param>
    public void InitializeDictionary(ILanguageService provider)
    {
        List<string> operatorList = provider.RenderOperators();

        foreach(string operatorCode in operatorList)
        {
            string? normalized = NormalizeString(operatorCode);
            if (normalized != null)
                AddEntry(operatorCode, normalized, provider.MapOperator(operatorCode));
        }
        operatorList.Clear();

        if (_tokenMap != null)
        {
            _tokenMap.Add(StandardOperatorTypes.Arithmetic, TokenType.ArithmeticOperator);
            _tokenMap.Add(StandardOperatorTypes.Assignment, TokenType.AssignmentOperator);
            _tokenMap.Add(StandardOperatorTypes.Bitwise, TokenType.BitwiseOperator);
            _tokenMap.Add(StandardOperatorTypes.Comparison, TokenType.ComparisonOperator);
            _tokenMap.Add(StandardOperatorTypes.Decrement, TokenType.DecrementOperator);
            _tokenMap.Add(StandardOperatorTypes.Increment, TokenType.IncrementOperator);
            _tokenMap.Add(StandardOperatorTypes.Logical, TokenType.LogicalOperator);
            _tokenMap.Add(StandardOperatorTypes.Other, TokenType.NoneOrUnknown);
            _tokenMap.Add(StandardOperatorTypes.Unknown, TokenType.NoneOrUnknown);
        }

    }

    /// <summary>
    /// Gets the type of the token used to represent the specific operator when parsing.
    /// </summary>
    /// <param name="operatorType"></param>
    /// <returns>
    /// A <see cref="TokenType" /> enumerated value indicating the token type.
    /// </returns>
    public TokenType GetTokenType(string operatorType)
    {
        return GetTokenType(GetOperator(operatorType));
    }

    /// <summary>
    /// Gets the type of the token used to represent the specific operator when parsing.
    /// </summary>
    /// <param name="operatorType"></param>
    /// <returns>
    /// A <see cref="TokenType" /> enumerated value indicating the token type.
    /// </returns>
    public TokenType GetTokenType(StandardOperatorTypes operatorType)
    {
        if (_tokenMap == null || !_tokenMap.ContainsKey(operatorType))
            return TokenType.NoneOrUnknown;

        return _tokenMap[operatorType];
    }

    /// <summary>
    /// Gets a value indicating whether the specified code is an operator in the language being implemented.
    /// </summary>
    /// <param name="code">A string containing the code text to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known operator; otherwise, <c>false</c>.
    /// </returns>
    public bool IsOperator(string? code)
    {
        return IsInDictionary(code);
    }
    #endregion
}

