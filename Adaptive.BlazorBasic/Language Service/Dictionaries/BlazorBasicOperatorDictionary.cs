using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Dictionaries;
using Adaptive.Intelligence.LanguageService.Providers;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides the operator dictionary for the Blazor BASIC language.
/// </summary>
/// <seealso cref="TwoWayDictionaryBase{K, V}" />
/// <seealso cref="IOperatorDictionary{T}" />
public class BlazorBasicOperatorDictionary : TwoWayDictionaryBase<string, StandardOperators>,
        IOperatorDictionary<StandardOperators>
{
    #region Private Member Declarations
    /// <summary>
    /// The dictionary for assignment operators.
    /// </summary>
    private IOperatorDictionary<StandardOperators>? _assignment;

    /// <summary>
    /// The dictionary for bitwise operators.
    /// </summary>
    private IOperatorDictionary<StandardOperators>? _bitwise;

    /// <summary>
    /// The dictionary for comparison operators.
    /// </summary>
    private IOperatorDictionary<StandardOperators>? _comparison;

    /// <summary>
    /// The dictionary for logical operators.
    /// </summary>
    private IOperatorDictionary<StandardOperators>? _logical;

    /// <summary>
    /// The dictionary for arithmetic operators.
    /// </summary>
    private IOperatorDictionary<StandardOperators>? _math;

    /// <summary>
    /// The dictionary for operational/functional operators.
    /// </summary>
    private IOperatorDictionary<StandardOperators>? _ops;

    /// <summary>
    /// The delimiter type to token type map.
    /// </summary>
    private Dictionary<StandardOperators, TokenType>? _tokenMap;

    /// <summary>
    /// The operator to operator type map.
    /// </summary>
    private Dictionary<StandardOperators, StandardOperatorTypes>? _typeMap;
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
            _typeMap?.Clear();

        }
        _assignment = null;
        _bitwise = null;
        _comparison = null;
        _logical = null;
        _math = null;
        _ops = null;
        _tokenMap = null;
        _typeMap = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the reference to the dictionary for assignment operators.
    /// </summary>
    /// <value>
    /// The <see cref="IOperatorDictionary{T}"/> of <see cref="StandardOperators"/> instance.
    /// </value>
    public IOperatorDictionary<StandardOperators> AssignmentOperators
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
    /// The <see cref="IOperatorDictionary{T}"/> of <see cref="StandardOperators"/> instance.
    /// </value>
    public IOperatorDictionary<StandardOperators> BitwiseOperators
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
    /// The <see cref="IOperatorDictionary{T}"/> of <see cref="StandardOperators"/> instance.
    /// </value>
    public IOperatorDictionary<StandardOperators> ComparisonOperators
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
    /// The <see cref="IOperatorDictionary{T}"/> of <see cref="StandardOperators"/> instance.
    /// </value>
    public IOperatorDictionary<StandardOperators> LogicalOperators
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
    /// The <see cref="IOperatorDictionary{T}"/> of <see cref="StandardOperators"/> instance.
    /// </value>
    public IOperatorDictionary<StandardOperators> MathOperators
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
    /// The <see cref="IOperatorDictionary{T}"/> of <see cref="StandardOperators"/> instance.
    /// </value>
    public IOperatorDictionary<StandardOperators> OperationalOperators
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
        InitializeTypeMap();
    }
    /// <summary>
    /// Gets the type of the operator.
    /// </summary>
    /// <param name="operatorItem"></param>
    /// <returns>
    /// A <see cref="StandardOperatorTypes" /> enumerated value indicating the type of operator.
    /// </returns>
    public StandardOperatorTypes GetOperatorType(StandardOperators operatorItem)
    {
        if (_typeMap == null)
            return StandardOperatorTypes.Unknown;

        return _typeMap[operatorItem];
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
        CreateAssignmentsDictionary();
        CreateBitwiseDictionary();
        CreateComparisonDictionary();
        CreateLogicalDictionary();
        CreateMathDictionary();
        CreateOpsDictionary();
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
    private void InitializeTypeMap()
    {
        _typeMap = new Dictionary<StandardOperators, StandardOperatorTypes>()
        {
            { StandardOperators.AssignmentEquals, StandardOperatorTypes.Assignment },
            { StandardOperators.BitwiseShortAnd, StandardOperatorTypes.Bitwise },
            { StandardOperators.BitwiseLongAnd, StandardOperatorTypes.Bitwise },
            { StandardOperators.BitwiseShortOr, StandardOperatorTypes.Bitwise },
            { StandardOperators.BitwiseLongOr, StandardOperatorTypes.Bitwise },
            { StandardOperators.ComparisonEqualTo, StandardOperatorTypes.Comparison },
            { StandardOperators.ComparisonGreaterThan, StandardOperatorTypes.Comparison },
            { StandardOperators.ComparisonGreaterThanOrEqualTo, StandardOperatorTypes.Comparison },
            { StandardOperators.ComparisonLessThan, StandardOperatorTypes.Comparison },
            { StandardOperators.ComparisonLessThanOrEqualTo, StandardOperatorTypes.Comparison },
            { StandardOperators.ComparisonNotEqualTo, StandardOperatorTypes.Comparison },
            { StandardOperators.LogicalAnd, StandardOperatorTypes.Logical },
            { StandardOperators.LogicalAndShort, StandardOperatorTypes.Logical },
            { StandardOperators.LogicalOr, StandardOperatorTypes.Logical },
            { StandardOperators.LogicalOrShort, StandardOperatorTypes.Logical },
            { StandardOperators.LogicalNot, StandardOperatorTypes.Logical },
            { StandardOperators.LogicalNotShort, StandardOperatorTypes.Logical },
            { StandardOperators.MathAdd, StandardOperatorTypes.Arithmetic },
            { StandardOperators.MathSubtract, StandardOperatorTypes.Arithmetic },
            { StandardOperators.MathMultiply, StandardOperatorTypes.Arithmetic },
            { StandardOperators.MathDivide, StandardOperatorTypes.Arithmetic },
            { StandardOperators.MathExponent, StandardOperatorTypes.Arithmetic },
            { StandardOperators.MathModulus, StandardOperatorTypes.Arithmetic },
            { StandardOperators.OpsIncrement, StandardOperatorTypes.Increment },
            { StandardOperators.OpsDecrement, StandardOperatorTypes.Decrement }
        };
    }
    /// <summary>
    /// Creates and adds the assignment operators to the dictionary.
    /// </summary>
    private void CreateAssignmentsDictionary()
    {
        _assignment?.Dispose();

        _assignment = new BlazorBasicOperatorDictionary();
        _assignment.AddEntry(OperatorNames.OperatorAssignment, StandardOperators.AssignmentEquals);
    }
    /// <summary>
    /// Creates and adds the bitwise operators to the dictionary.
    /// </summary>
    private void CreateBitwiseDictionary()
    {
        _bitwise?.Dispose();

        _bitwise = new BlazorBasicOperatorDictionary();
        _bitwise.AddEntry(OperatorNames.OperatorBitwiseAnd, StandardOperators.BitwiseShortAnd);
        _bitwise.AddEntry(OperatorNames.OperatorBitwiseLongAnd, StandardOperators.BitwiseLongAnd);
        _bitwise.AddEntry(OperatorNames.OperatorBitwiseOr, StandardOperators.BitwiseShortOr);
        _bitwise.AddEntry(OperatorNames.OperatorBitwiseLongOr, StandardOperators.BitwiseLongOr);
    }
    /// <summary>
    /// Creates and adds the comparison operators to the dictionary.
    /// </summary>
    private void CreateComparisonDictionary()
    {
        _comparison?.Dispose();

        _comparison = new BlazorBasicOperatorDictionary();
        _comparison.AddEntry(OperatorNames.OperatorGreaterThan, StandardOperators.ComparisonGreaterThan);
        _comparison.AddEntry(OperatorNames.OperatorGreaterThanOrEqualTo, StandardOperators.ComparisonGreaterThanOrEqualTo);
        _comparison.AddEntry(OperatorNames.OperatorLessThan, StandardOperators.ComparisonLessThan);
        _comparison.AddEntry(OperatorNames.OperatorLessThanOrEqualTo, StandardOperators.ComparisonLessThanOrEqualTo);
        _comparison.AddEntry(OperatorNames.OperatorEqualTo, StandardOperators.ComparisonEqualTo);
        _comparison.AddEntry(OperatorNames.OperatorNotEqualTo, StandardOperators.ComparisonNotEqualTo);
    }
    /// <summary>
    /// Creates and adds the logical operators to the dictionary.
    /// </summary>
    private void CreateLogicalDictionary()
    {
        _logical?.Dispose();

        _logical = new BlazorBasicOperatorDictionary();
        _logical.AddEntry(OperatorNames.OperatorLogicalAnd, StandardOperators.LogicalAnd);
        _logical.AddEntry(OperatorNames.OperatorLogicalAndShort, StandardOperators.LogicalAndShort);
        _logical.AddEntry(OperatorNames.OperatorLogicalOr, StandardOperators.LogicalOr);
        _logical.AddEntry(OperatorNames.OperatorLogicalOrShort, StandardOperators.LogicalOrShort);
        _logical.AddEntry(OperatorNames.OperatorLogicalNot, StandardOperators.LogicalNot);
        _logical.AddEntry(OperatorNames.OperatorLogicalNotShort, StandardOperators.LogicalNotShort);
    }
    /// <summary>
    /// Creates and adds the arithmetic operators to the dictionary.
    /// </summary>

    private void CreateMathDictionary()
    {
        _math?.Dispose();

        _math = new BlazorBasicOperatorDictionary();
        _math.AddEntry(OperatorNames.OperatorAdd, StandardOperators.MathAdd);
        _math.AddEntry(OperatorNames.OperatorSubtract, StandardOperators.MathSubtract);
        _math.AddEntry(OperatorNames.OperatorMultiply, StandardOperators.MathMultiply);
        _math.AddEntry(OperatorNames.OperatorDivide, StandardOperators.MathDivide);
        _math.AddEntry(OperatorNames.OperatorModulus, StandardOperators.MathModulus);
        _math.AddEntry(OperatorNames.OperatorExponent, StandardOperators.MathExponent);
    }
    /// <summary>
    /// Creates and adds the operational / functional operators to the dictionary.
    /// </summary>

    private void CreateOpsDictionary()
    {
        _ops?.Dispose();

        _ops = new BlazorBasicOperatorDictionary();
        _ops.AddEntry(OperatorNames.OperatorDecrement, StandardOperators.OpsDecrement);
        _ops.AddEntry(OperatorNames.OperatorIncrement, StandardOperators.OpsIncrement);

    }
    #endregion
}