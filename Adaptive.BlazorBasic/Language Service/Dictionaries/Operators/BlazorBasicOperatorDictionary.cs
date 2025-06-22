using Adaptive.LanguageService;
using Adaptive.LanguageService.Providers;
using Adaptive.LanguageService.Tokenization;

namespace Adaptive.BlazorBasic.LanguageService;


/// <summary>
/// Provides the operator dictionary for the Blazor BASIC language.
/// </summary>
/// <seealso cref="TwoWayDictionaryBase{K, V}" />
/// <seealso cref="IOperatorDictionary{A, B, C, D, E, F, G}" />
public class BlazorBasicOperatorDictionary : TwoWayDictionaryBase<string, StandardOperators>,
        IOperatorDictionary<StandardOperators, BlazorBasicAssignmentOperators, BlazorBasicBitwiseOperators,
        BlazorBasicComparisonOperators, BlazorBasicLogicalOperators, BlazorBasicMathOperators,
        BlazorBasicOperationalOperators>
{
    #region Private Member Declarations
    /// <summary>
    /// The dictionary for assignment operators.
    /// </summary>
    private IAssignmentOperatorDictionary<BlazorBasicAssignmentOperators>? _assignment;

    /// <summary>
    /// The dictionary for bitwise operators.
    /// </summary>
    private IBitwiseOperatorDictionary<BlazorBasicBitwiseOperators>? _bitwise;

    /// <summary>
    /// The dictionary for comparison operators.
    /// </summary>
    private IComparisonOperatorDictionary<BlazorBasicComparisonOperators>? _comparison;

    /// <summary>
    /// The dictionary for logical operators.
    /// </summary>
    private ILogicalOperatorDictionary<BlazorBasicLogicalOperators>? _logical;

    /// <summary>
    /// The dictionary for arithmetic operators.
    /// </summary>
    private IMathOperatorDictionary<BlazorBasicMathOperators>? _math;

    /// <summary>
    /// The dictionary for operational/functional operators.
    /// </summary>
    private IOperationalOperatorDictionary<BlazorBasicOperationalOperators>? _ops;

    /// <summary>
    /// The delimiter type to token type map.
    /// </summary>
    private Dictionary<StandardOperators, TokenType>? _tokenMap;
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
            _assignment?.Dispose();
            _bitwise?.Dispose();
            _comparison?.Dispose();
            _logical?.Dispose();
            _math?.Dispose();
            _ops?.Dispose();
            _tokenMap?.Clear();

        }
        _assignment = null;
        _bitwise = null;
        _comparison = null;
        _logical = null;
        _math = null;
        _ops = null;
        _tokenMap = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the reference to the dictionary for assignment operators.
    /// </summary>
    /// <value>
    /// The <see cref="IAssignmentOperatorDictionary{T}"/> of <see cref="BlazorBasicAssignmentOperators"/> instance.
    /// </value>
    public IAssignmentOperatorDictionary<BlazorBasicAssignmentOperators> AssignmentOperators
    {
        get
        {
            if (_assignment == null)
                throw new Exception();
            return _assignment;
        }
    }

    /// <summary>
    /// Gets the reference to the dictionary for bitwise operators.
    /// </summary>
    /// <value>
    /// The <see cref="IBitwiseOperatorDictionary{T}"/> of <see cref="BlazorBasicBitwiseOperators"/> instance.
    /// </value>
    public IBitwiseOperatorDictionary<BlazorBasicBitwiseOperators> BitwiseOperators
    {
        get
        {
            if (_bitwise == null)
                throw new Exception();
            return _bitwise;
        }
    }

    /// <summary>
    /// Gets the reference to the dictionary for comparison operators.
    /// </summary>
    /// <value>
    /// The <see cref="IComparisonOperatorDictionary{T}"/> of <see cref="BlazorBasicComparisonOperators"/> instance.
    /// </value>
    public IComparisonOperatorDictionary<BlazorBasicComparisonOperators> ComparisonOperators
    {
        get
        {
            if (_comparison == null)
                throw new Exception();
            return _comparison;
        }
    }

    /// <summary>
    /// Gets the reference to the dictionary for logical operators.
    /// </summary>
    /// <value>
    /// The <see cref="ILogicalOperatorDictionary{T}"/> of <see cref="BlazorBasicLogicalOperators"/> instance.
    /// </value>
    public ILogicalOperatorDictionary<BlazorBasicLogicalOperators> LogicalOperators
    {
        get
        {
            if (_logical == null)
                throw new Exception();
            return _logical;
        }
    }

    /// <summary>
    /// Gets the reference to the dictionary for arithmetic operators.
    /// </summary>
    /// <value>
    /// The <see cref="IMathOperatorDictionary{T}"/> of <see cref="BlazorBasicMathOperators"/> instance.
    /// </value>
    public IMathOperatorDictionary<BlazorBasicMathOperators> MathOperators
    {
        get
        {
            if (_math == null)
                throw new Exception();
            return _math;
        }
    }

    /// <summary>
    /// Gets the reference to the dictionary for operational/functional operators.
    /// </summary>
    /// <value>
    /// The <see cref="IOperationalOperatorDictionary{T}"/> of <see cref="BlazorBasicOperationalOperators"/> instance.
    /// </value>
    public IOperationalOperatorDictionary<BlazorBasicOperationalOperators> OperationalOperators
    {
        get
        {
            if (_ops == null)
                throw new Exception();
            return _ops;
        }
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
    /// A <see cref="StandardOperators" /> enumerated value indicating the operator.
    /// </returns>
    public StandardOperators GetOperator(string? operatorText)
    {
        if (operatorText == null)
            return StandardOperators.NoneOrUnknown;

        return Get(NormalizeKeyValue(operatorText));
    }

    /// <summary>
    /// Gets the name / text representation of the operator.
    /// </summary>
    /// <param name="operatorType">
    /// A <see cref="StandardOperators" /> enumerated value indicating the operator.</param>
    /// <returns>
    /// A string containing the standard code/text for the specified operator in the language being implemented.
    /// </returns>
    public string? GetOperatorText(StandardOperators operatorType)
    {
        return ReverseGet(operatorType);
    }

    /// <summary>
    /// Populates the dictionary with the operators from the specified language provider.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IOperatorProvider" /> provider instance used to provide the list.
    /// </param>
    public void InitializeDictionary(IOperatorProvider provider)
    {
        // Get the list of delimiters.
        List<string> names = provider.RenderOperatorNames();

        // Get the list of ID values and translate to the appropriate list of enumerations.
        List<int> idValues = provider.RenderOperatorIds();
        List<StandardOperators> typeList = IdsToEnum<StandardOperators>(idValues);
        idValues.Clear();

        BaseInitialize(names, typeList);

        typeList.Clear();
        names.Clear();

        InitializeSubDictionaries(provider);

        InitializeTokenMap();
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
        return GetTokenType(Get(operatorType));
    }

    /// <summary>
    /// Gets the type of the token used to represent the specific operator when parsing.
    /// </summary>
    /// <param name="operatorType"></param>
    /// <returns>
    /// A <see cref="TokenType" /> enumerated value indicating the token type.
    /// </returns>
    public TokenType GetTokenType(StandardOperators operatorType)
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
        return value.ToUpper().Trim();
    }
    #endregion

    #region Private Methods / Functions    
    /// <summary>
    /// Initializes the sub dictionaries.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IOperatorProvider"/> provider instance to use.
    /// </param>
    private void InitializeSubDictionaries(IOperatorProvider provider)
    {

        _assignment = new BlazorBasicAssignmentOperatorDictionary();
        _bitwise = new BlazorBasicBitwiseOperatorDictionary();
        _comparison = new BlazorBasicComparisonOperatorDictionary();
        _logical = new BlazorBasicLogicalOperatorDictionary();
        _math = new BlazorBasicMathOperatorDictionary();
        _ops = new BlazorBasicOperationalOperatorDictionary();

        _assignment.InitializeDictionary(provider.AssignmentOperators);
        _bitwise.InitializeDictionary(provider.BitwiseOperators);
        _comparison.InitializeDictionary(provider.ComparisonOperators);
        _logical.InitializeDictionary(provider.LogicalOperators);
        _math.InitializeDictionary(provider.MathOperators);
        _ops.InitializeDictionary(provider.OperationOperators);
    }
    /// <summary>
    /// Initializes the token map.
    /// </summary>
    private void InitializeTokenMap()
    {
        _tokenMap = new Dictionary<StandardOperators, TokenType>
        {
            { StandardOperators.AssignmentEquals, TokenType.AssignmentOperator },
            { StandardOperators.BitwiseShortAnd, TokenType.BitwiseOperator },
            { StandardOperators.BitwiseLongAnd, TokenType.BitwiseOperator },
            { StandardOperators.BitwiseShortOr, TokenType.BitwiseOperator },
            { StandardOperators.BitwiseLongOr, TokenType.BitwiseOperator },
            { StandardOperators.ComparisonEqualTo, TokenType.ComparisonOperator },
            { StandardOperators.ComparisonGreaterThan, TokenType.ComparisonOperator },
            { StandardOperators.ComparisonGreaterThanOrEqualTo, TokenType.ComparisonOperator },
            { StandardOperators.ComparisonLessThan, TokenType.ComparisonOperator },
            { StandardOperators.ComparisonLessThanOrEqualTo, TokenType.ComparisonOperator },
            { StandardOperators.ComparisonNotEqualTo, TokenType.ComparisonOperator },
            { StandardOperators.LogicalAnd, TokenType.LogicalOperator },
            { StandardOperators.LogicalAndShort, TokenType.LogicalOperator },
            { StandardOperators.LogicalOr, TokenType.LogicalOperator },
            { StandardOperators.LogicalOrShort, TokenType.LogicalOperator },
            { StandardOperators.LogicalNot, TokenType.LogicalOperator },
            { StandardOperators.LogicalNotShort, TokenType.LogicalOperator },
            { StandardOperators.MathAdd, TokenType.ArithmeticOperator },
            { StandardOperators.MathSubtract, TokenType.ArithmeticOperator },
            { StandardOperators.MathMultiply, TokenType.ArithmeticOperator },
            { StandardOperators.MathDivide, TokenType.ArithmeticOperator },
            { StandardOperators.MathExponent, TokenType.ArithmeticOperator },
            { StandardOperators.MathModulus, TokenType.ArithmeticOperator },
            { StandardOperators.OpsIncrement, TokenType.IncrementOperator },
            { StandardOperators.OpsDecrement, TokenType.DecrementOperator }
        };
    }
    #endregion
}