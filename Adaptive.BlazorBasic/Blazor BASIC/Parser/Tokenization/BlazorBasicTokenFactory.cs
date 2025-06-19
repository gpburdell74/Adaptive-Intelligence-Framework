using Adaptive.BlazorBasic.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides methods / functions for creating <see cref="IToken"/> and <see cref="TokenBase"/> instances.
/// </summary>
public class BlazorBasicTokenFactory : DisposableObjectBase, ITokenFactory<BlazorBasicFunctions, BlazorBasicKeywords>
{
    #region Private Member Declarations
    /// <summary>
    /// The symbols that are one-character-long list.
    /// </summary>
    private Dictionary<string, TokenType>? _singleList;

    /// <summary>
    /// The language service instance.
    /// </summary>
    private ILanguageService<BlazorBasicFunctions, BlazorBasicKeywords>? _service;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes the <see cref="BlazorBasicTokenFactory"/> class.
    /// </summary>
    /// <param name="service">
    /// The <see cref="ILanguageService{FunctionsEnum, KeywordsEnum}"/> language service instance.
    /// </param>
    public BlazorBasicTokenFactory(ILanguageService<BlazorBasicFunctions, BlazorBasicKeywords> service)
    {
        // Populate the static dictionaries with the known token types.
        _service = service;
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
            _singleList?.Clear();
        }

        _singleList = null;
        _service = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates the token instance from the provided text..
    /// </summary>
    /// <param name="originalCode">
    /// A string containing the original code to be parsed into a token.
    /// </param>
    /// <returns>
    /// A <see cref="TokenBase"/> instance representing the token created from the provided text.
    /// </returns>
    public IToken CreateToken(string originalCode)
    {
        return CreateToken(originalCode, DetermineTokenType(originalCode));
    }

    /// <summary>
    /// Creates the token instance from the provided text..
    /// </summary>
    /// <param name="originalData">
    /// A string containing the original code to be parsed into a token.
    /// </param>
    /// <param name="tokenType">
    /// A <see cref="TokenType"/> enumerated value indicating the type of token to create.
    /// </param>
    /// <returns>
    /// A <see cref="TokenBase"/> instance representing the token created from the provided text.
    /// </returns>
    public static IToken CreateToken(string originalData, TokenType tokenType)
    {
        TokenBase newToken;

        switch (tokenType)
        {
            case TokenType.ArithmeticOperator:
                newToken = new ArithmeticOperatorToken(originalData);
                break;

            case TokenType.AssignmentOperator:
                newToken = new AssignmentOperatorToken(originalData);
                break;

            case TokenType.BitwiseOperator:
                newToken = new BitwiseOperatorToken(originalData);
                break;

            case TokenType.BlockEndDelimiter:
                newToken = new BlockEndDelimiterToken(originalData);
                break;

            case TokenType.BlockStartDelimiter:
                newToken = new BlockStartDelimiterToken(originalData);
                break;

            case TokenType.CharacterDelimiter:
                newToken = new CharacterDelimiterToken(originalData);
                break;

            case TokenType.ComparisonOperator:
                newToken = new ComparisonOperatorToken(originalData);
                break;

            case TokenType.ReservedWord:
                newToken = new ReservedWordToken(originalData);
                break;

            case TokenType.ReservedFunction:
                newToken = new ReservedFunctionToken(originalData);
                break;

            case TokenType.DataTypeName:
                newToken = new DataTypeNameToken(originalData);
                break;

            case TokenType.ProcedureName:
                newToken = new ProcedureNameToken(originalData);
                break;

            case TokenType.FunctionName:
                newToken = new FunctionNameToken(originalData);
                break;

            case TokenType.VariableName:
                newToken = new VariableNameToken(originalData);
                break;

            case TokenType.UserDefinedItem:
                newToken = new UserDefinedItemToken(originalData);
                break;

            case TokenType.Integer:
                newToken = new IntegerToken(originalData);
                break;

            case TokenType.FloatingPoint:
                newToken = new FloatingPointToken(originalData);
                break;

            case TokenType.LogicalOperator:
                newToken = new LogicalOperatorToken(originalData);
                break;

            case TokenType.IncrementOperator:
                newToken = new IncrementOperatorToken(originalData);
                break;

            case TokenType.DecrementOperator:
                newToken = new DecrementOperatorToken(originalData);
                break;

            case TokenType.SeparatorDelimiter:
                newToken = new SeparatorDelimiterToken(originalData);
                break;

            case TokenType.StringDelimiter:
                newToken = new StringDelimiterToken(originalData);
                break;

            case TokenType.ExpressionStartDelimiter:
                newToken = new ExpressionStartDelimiterToken(originalData);
                break;

            case TokenType.ExpressionEndDelimiter:
                newToken = new ExpressionEndDelimiterToken(originalData);
                break;

            case TokenType.SizingStartDelimiter:
                newToken = new SizingStartDelimiterToken(originalData);
                break;

            case TokenType.SizingEndDelimiter:
                newToken = new SizingEndDelimiterToken(originalData);
                break;

            default:
                newToken = new ErrorToken(originalData);
                break;
        }
        return newToken;
    }

    /// <summary>
    /// Determines the type of the token.
    /// </summary>
    /// <param name="originalCode">
    /// A string containing the original code to be parsed into a token.
    /// </param>
    /// <returns>
    /// A <see cref="TokenType"/> enumerated value indicating the type of token the text is attempting to create.
    /// </returns>
    public TokenType DetermineTokenType(string originalCode)
    {
        TokenType tokenType = TokenType.Error;

        if (_service != null && !string.IsNullOrEmpty(originalCode))
        {
            originalCode = originalCode.ToUpper();

            if (_service.IsDataType(originalCode))
                tokenType = TokenType.DataTypeName;

            else if (_service.IsDelimiter(originalCode))
                tokenType = _service.MapDelimiterToken(originalCode);

            else if (_service.IsBuiltInFunction(originalCode))
                tokenType = TokenType.ReservedFunction;

            else if (_service.IsKeyWord(originalCode))
                tokenType = TokenType.ReservedWord;

            else if (_service.IsOperator(originalCode))
                tokenType = _service.MapOperatorToken(originalCode);

            else
            {
                if (IsNumeric(originalCode))
                {
                    if (originalCode.Contains(MathConstants.Decimal))
                        tokenType = TokenType.FloatingPoint;
                    else
                        tokenType = TokenType.Integer;
                }
                else
                    tokenType = TokenType.UserDefinedItem;
            }
        }
        return tokenType;
    }
    /// <summary>
    /// Initializes the factory instance using the specified language provider reference.
    /// </summary>
    /// <param name="service">The <see cref="ILanguageService{F, K}" /> instance to use.</param>
    public void Initialize(ILanguageService<BlazorBasicFunctions, BlazorBasicKeywords> service)
    {
        _singleList = service.GetSingleCharacterTokenValuesFromDictionaries();
    }

    /// <summary>
    /// Determines whether the specified character matches one of the known single-character tokens.
    /// </summary>
    /// <remarks>
    /// This is needed since single-character tokens (such as operators like + = /, etc. may or may not be preceded or succeeded with
    /// a separator delimiter.
    /// </remarks>
    /// <param name="character">
    /// The character to be checked.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the specified character matches one of the known single-character tokens; otherwise, <c>false</c>.
    /// </returns>
    public bool IsSingleCharToken(string character)
    {
        if (_singleList == null)
            return false;

        return _singleList.ContainsKey(character.ToUpper());
    }
    /// <summary>
    /// Determines whether the specified text to check is a number.
    /// </summary>
    /// <remarks>
    /// This is more complicated than char.IsNumber() because leading signs (+ -) must be accounted for, and the 
    /// value may be an integer (with no decimal point) or a floating-point number (with a single decimal point).
    /// </remarks>
    /// <param name="originalCode">
    /// A string containing the text to be checked.</param>
    /// <returns>
    ///   <c>true</c> if the specified text represents a numeric value; otherwise, <c>false</c>.
    /// </returns>
    public bool IsNumeric(string originalCode)
    {
        bool dotFound = false;
        bool isNumber = false;

        int length = originalCode.Length;

        if (length > 0)
        {
            char first = originalCode[0];
            if (length == 1)
            {
                isNumber = char.IsNumber(first);
            }
            else
            {
                // The first character may be a number or a sign.
                if (char.IsNumber(first) || first == MathConstants.Positive || first == MathConstants.Negative)
                {
                    isNumber = true;
                    int pos = 1;
                    do
                    {
                        char next = originalCode[pos];
                        if (!char.IsNumber(next))
                        {
                            if (!dotFound && next == MathConstants.Decimal)
                                dotFound = true;
                            else if (next == MathConstants.Decimal)
                            {
                                isNumber = false;
                            }
                        }
                        pos++;
                    } while (pos < length && isNumber);
                }
            }
        }
        return isNumber;
    }

    #endregion
}