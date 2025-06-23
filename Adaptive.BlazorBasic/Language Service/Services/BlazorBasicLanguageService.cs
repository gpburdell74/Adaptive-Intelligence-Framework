using Adaptive.Intelligence.BlazorBasic.LanguageService;
using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Dictionaries;
using Adaptive.Intelligence.LanguageService.Parsing;
using Adaptive.Intelligence.LanguageService.Services;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.Services;

/// <summary>
/// Provides the implementation of the language service for the Blazor BASIC Language.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public class BlazorBasicLanguageService : 
    DisposableObjectBase, 
    ILanguageService<
        BlazorBasicDelimiters,
        BlazorBasicErrorCodes,
        BlazorBasicFunctions,
        BlazorBasicKeywords,
        StandardOperators>
{
    #region Private Member Declarations

    #region Dependency-Injected Items
    /// <summary>
    /// The language provider service.
    /// </summary>
    private ILanguageProviderService<BlazorBasicDelimiters,
        BlazorBasicErrorCodes,
        BlazorBasicFunctions,
        BlazorBasicKeywords,
        StandardOperators>? _providerService;

    /// <summary>
    /// The data types dictionary.
    /// </summary>
    private BlazorBasicDataTypeDictionary? _dataTypes;

    /// <summary>
    /// The delimiter dictionary.
    /// </summary>
    private BlazorBasicDelimiterDictionary? _delimiters;

    /// <summary>
    /// The functions dictionary.
    /// </summary>
    private BlazorBasicFunctionDictionary? _functions;

    /// <summary>
    /// The keywords dictionary.
    /// </summary>
    private BlazorBasicKeywordsDictionary? _keywords;

    /// <summary>
    /// The errors dictionary.
    /// </summary>
    private BlazorBasicErrorDictionary? _errors;

    /// <summary>
    /// The operators dictionary.
    /// </summary>
    private BlazorBasicOperatorDictionary? _operators;

    /// <summary>
    /// The parsing elements dictionary.
    /// </summary>
    private IParsingElementDictionary? _parsingElements;
    #endregion

    /// <summary>
    /// The dictionary of reserved keywords, functions , operators, delimiters, etc. that are one character long.
    /// </summary>
    private Dictionary<string, TokenType>? _singleCharacterList;

    /// <summary>
    /// The token factory.
    /// </summary>
    private BlazorBasicTokenFactory? _tokenFactory;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLanguageService"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicLanguageService(BlazorBasicProviderService providerService)
    {
        _providerService = providerService;
        Initialize(providerService);
        if (Instance == null)
            Instance = this;
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
            _tokenFactory?.Dispose();
            _singleCharacterList?.Clear();
        }

        _providerService = null;
        _dataTypes = null;
        _delimiters = null;
        _functions = null;
        _keywords = null;
        _operators = null;
        _errors = null;
        _tokenFactory = null;
        _singleCharacterList = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Static Properties
    /// <summary>
    /// Gets the static instance reference.
    /// </summary>
    /// <value>
    /// A <see cref="BlazorBasicLanguageService"/> instance.
    /// </value>
    public static BlazorBasicLanguageService? Instance { get; private set; }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the reference to the data types dictionary for the language.
    /// </summary>
    /// <value>
    /// An <see cref="IDataTypeDictionary" /> specifying the names for the data types in the language.
    /// </value>
    public IDataTypeDictionary DataTypes
    {
        get
        {
            if (_dataTypes == null)
                throw new Exception();
            return _dataTypes;
        }
    }

    /// <summary>
    /// Gets the reference to the delimiters dictionary for the language.
    /// </summary>
    /// <value>
    /// An <see cref="BlazorBasicDelimiterDictionary" /> specifying the text representations for the delimiters in the language.
    /// </value>
    public BlazorBasicDelimiterDictionary Delimiters
    {
        get
        {
            if (_delimiters == null)
                throw new Exception();
            return _delimiters;
        }
    }

    /// <summary>
    /// Gets the reference to the error types dictionary.
    /// </summary>
    /// <value>
    /// The <see cref="BlazorBasicErrorDictionary" /> containing the list of valid error types.
    /// </value>
    public BlazorBasicErrorDictionary Errors
    {
        get
        {
            if (_errors == null)
                throw new Exception();
            return _errors;
        }
    }

    /// <summary>
    /// Gets the reference to the functions dictionary for the language.
    /// </summary>
    /// <value>
    /// An <see cref="BlazorBasicFunctionDictionary" /> specifying the names for the built-in functions in the language.
    /// </value>
    public BlazorBasicFunctionDictionary Functions
    {
        get
        {
            if (_functions == null)
                throw new Exception();
            return _functions;
        }
    }

    /// <summary>
    /// Gets the reference to the keywords dictionary for the language.
    /// </summary>
    /// <value>
    /// An <see cref="BlazorBasicKeywordsDictionary" /> specifying the names for the keywords in the language.
    /// </value>
    public BlazorBasicKeywordsDictionary? Keywords
    {
        get
        {
            if (_keywords == null)
                throw new Exception();
            return _keywords;
        }
    }

    /// <summary>
    /// Gets the reference to the operators dictionary for the language.
    /// </summary>
    /// <value>
    /// An <see cref="T:Adaptive.Intelligence.LanguageService.IOperatorDictionary" /> specifying the code representations for the operators in the language.
    /// </value>
    public BlazorBasicOperatorDictionary Operators
    {
        get
        {
            if (_operators == null)
                throw new Exception();
            return _operators;
        }
    }

    /// <summary>
    /// Gets the reference to the parsing elements dictionary.
    /// </summary>
    /// <value>
    /// The <see cref="IParsingElementDictionary" /> containing the list of valid parsing elements.
    /// </value>
    public IParsingElementDictionary ParsingElements
    {
        get
        {
            if (_parsingElements == null)
                throw new Exception();
            return _parsingElements;
        }
    }
    #endregion

    #region Interface Implementation Properties    
    /// <summary>
    /// Gets the reference to the delimiters dictionary.
    /// </summary>
    /// <value>
    /// The <see cref="!:IDelimiterDictionary" /> containing the list of valid delimiter definitions.
    /// </value>
    IDelimiterDictionary<BlazorBasicDelimiters>
        ILanguageService<
            BlazorBasicDelimiters,
            BlazorBasicErrorCodes,
            BlazorBasicFunctions,
            BlazorBasicKeywords,
            StandardOperators>
        .Delimiters => _delimiters;

    /// <summary>
    /// Gets the reference to the error types dictionary.
    /// </summary>
    /// <value>
    /// The <see cref="IErrorDictionary{T}" /> containing the list of valid error types.
    /// </value>
    IErrorDictionary<BlazorBasicErrorCodes>
        ILanguageService<
            BlazorBasicDelimiters,
            BlazorBasicErrorCodes,
            BlazorBasicFunctions,
            BlazorBasicKeywords,
            StandardOperators>.Errors => _errors;

    IBuiltInFunctionDictionary<BlazorBasicFunctions>
        ILanguageService<
            BlazorBasicDelimiters,
            BlazorBasicErrorCodes,
            BlazorBasicFunctions,
            BlazorBasicKeywords,
            StandardOperators>.Functions { get; }
    IKeywordDictionary<BlazorBasicKeywords>
        ILanguageService<
            BlazorBasicDelimiters,
            BlazorBasicErrorCodes,
            BlazorBasicFunctions,
            BlazorBasicKeywords,
            StandardOperators>.Keywords => _keywords;
        
    IOperatorDictionary<StandardOperators>
        ILanguageService<
            BlazorBasicDelimiters,
            BlazorBasicErrorCodes,
            BlazorBasicFunctions,
            BlazorBasicKeywords,
            StandardOperators>.Operators => _operators;

    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates the parsing log instance.
    /// </summary>
    /// <returns>
    /// The <see cref="IParserOutputLogger" /> instance to supply to the parser mechanism.
    /// </returns>
    public IParserOutputLogger CreateParsingLog()
    {
        return (IParserOutputLogger) new ParserOutputLogger();
    }

    /// <summary>
    /// Gets the list of single character token values with their mapping from the populated dictionaries.
    /// </summary>
    /// <returns>
    /// A <see cref="Dictionary{TKey, TValue}" /> of <see cref="string"/> and <see cref="TokenType"/> containing the mapped list.
    /// </returns>
    public Dictionary<string, TokenType> GetSingleCharacterTokenValuesFromDictionaries()
    {
        if (_singleCharacterList == null)
        {
            _singleCharacterList = new Dictionary<string, TokenType>(200);

            // Data Type names.
            List<string> dataTypeNames = RenderDataTypes();
            foreach (string dataType in dataTypeNames)
            {
                if (dataType.Length == 1)
                    _singleCharacterList.Add(dataType, TokenType.DataTypeName);
            }
            dataTypeNames.Clear();

            // Built-in Functions
            List<string> functionNames = RenderBuiltInFunctions();
            foreach (string functionName in functionNames)
            {
                if (functionName.Length == 1)
                    _singleCharacterList.Add(functionName, TokenType.ReservedFunction);
            }
            functionNames.Clear();

            // Keywords.
            List<string> keywords = RenderKeywordNames();
            foreach (string keyword in dataTypeNames)
            {
                if (keyword.Length == 1)
                    _singleCharacterList.Add(keyword, TokenType.ReservedWord);
            }
            keywords.Clear();

            // Operators.
            List<string> operators = RenderOperators();
            foreach (string operatorText in operators)
            {
                if (operatorText.Length == 1)
                {
                    _singleCharacterList.Add(operatorText, _tokenFactory!.DetermineTokenType(operatorText));
                }
            }
            dataTypeNames.Clear();

            // Delimiters
            List<string> delimiters = RenderDelimiters();
            foreach (string delimiter in delimiters)
            {
                if (delimiter.Length == 1)
                    _singleCharacterList.Add(delimiter, _tokenFactory!.DetermineTokenType(delimiter));
            }
            dataTypeNames.Clear();
        }
        return _singleCharacterList;
    }

    /// <summary>
    /// Initializes the language service with the code providers supplied from the provider service.
    /// </summary>
    /// <param name="providerService">
    /// The <see cref="BlazorBasicProviderService" /> provider service implementation.
    /// </param>
    public void Initialize(BlazorBasicProviderService providerService)
    {
        // Dictionaries.
        _dataTypes = providerService.CreateDataTypeDictionary();
        _delimiters = providerService.CreateDelimiterDictionary();
        _functions = providerService.CreateFunctionsDictionary();
        _keywords = providerService.CreateKeywordsDictionary();
        _operators = providerService.CreateOperatorDictionary();
        _errors = providerService.CreateErrorDictionary();

        // Token factory.
        _tokenFactory = new BlazorBasicTokenFactory(this);
    }
    /// <summary>
    /// Initializes the language service with the code providers supplied from the provider service.
    /// </summary>
    /// <remarks>
    /// This is the explicit interface implementation.
    /// </remarks>
    /// <param name="providerService">
    /// The <see cref="ILanguageProviderService{D,E,F,K,O}" /> provider service implementation.</param>
    void ILanguageService<
        BlazorBasicDelimiters, 
        BlazorBasicErrorCodes, 
        BlazorBasicFunctions, 
        BlazorBasicKeywords, 
        StandardOperators>
        .Initialize(ILanguageProviderService<BlazorBasicDelimiters, BlazorBasicErrorCodes, BlazorBasicFunctions, BlazorBasicKeywords, StandardOperators> providerService)
    {
        Initialize((BlazorBasicProviderService)providerService);
    }

    /// <summary>
    /// Determines whether the specified text refers to an item in the <see cref="P:Adaptive.BlazorBasic.LanguageService.ILanguageService`2.Functions" /> list.
    /// </summary>
    /// <param name="code">A string containing the code to evaluate.</param>
    /// <returns>
    /// <c>true</c> the specified text refers to a specified built-in function name; otherwise, <c>false</c>.
    /// </returns>
    public bool IsBuiltInFunction(string code)
    {
        return _functions!.IsBuiltInFunction(code);
    }

    /// <summary>
    /// Determines whether the specified text refers to an item in the <see cref="DataTypes" /> list.
    /// </summary>
    /// <param name="code">A string containing the code to evaluate.</param>
    /// <returns>
    ///   <c>true</c> the specified text refers to a specified data type name; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDataType(string code)
    {
        return _dataTypes!.IsDataType(code);
    }

    /// <summary>
    /// Determines whether the specified text refers to an item in the <see cref="Delimiters" /> list.
    /// </summary>
    /// <param name="code">A string containing the code to evaluate.</param>
    /// <returns>
    ///   <c>true</c> the specified text refers to a specified delimiter; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDelimiter(string code)
    {
        return _delimiters!.IsDelimiter(code);
    }

    /// <summary>
    /// Determines whether the specified text refers to an item in the <see cref="P:Adaptive.BlazorBasic.LanguageService.ILanguageService`2.Keywords" /> list.
    /// </summary>
    /// <param name="code">A string containing the code to evaluate.</param>
    /// <returns>
    /// <c>true</c> the specified text refers to a specified keyword; otherwise, <c>false</c>.
    /// </returns>
    public bool IsKeyWord(string code)
    {
        return _keywords!.IsKeyword(code);
    }

    /// <summary>
    /// Determines whether the specified text refers to an item in the <see cref="Operators" /> list.
    /// </summary>
    /// <param name="code">A string containing the code to evaluate.</param>
    /// <returns>
    ///   <c>true</c> the specified text refers to a specified operator; otherwise, <c>false</c>.
    /// </returns>
    public bool IsOperator(string code)
    {
        return _operators!.IsOperator(code);
    }

    /// <summary>
    /// Maps the data type name to the matching enumeration value.
    /// </summary>
    /// <param name="dataTypeName">A string containing the data type name.</param>
    /// <returns>
    /// A <see cref="StandardDataTypes" /> enumerated value for the data type.
    /// </returns>
    /// <remarks>
    /// This is used when initializing the data type dictionary.
    /// </remarks>
    public StandardDataTypes MapDataType(string dataTypeName)
    {
        return dataTypeName switch
        {
            DataTypeNames.TypeBool => StandardDataTypes.Boolean,
            DataTypeNames.TypeByte => StandardDataTypes.Byte,
            DataTypeNames.TypeChar => StandardDataTypes.Char,
            DataTypeNames.TypeShort => StandardDataTypes.ShortInteger,
            DataTypeNames.TypeInteger => StandardDataTypes.Integer,
            DataTypeNames.TypeLong => StandardDataTypes.LongInteger,
            DataTypeNames.TypeFloat => StandardDataTypes.Float,
            DataTypeNames.TypeDouble => StandardDataTypes.Double,
            DataTypeNames.TypeDate => StandardDataTypes.Date,
            DataTypeNames.TypeDateTime => StandardDataTypes.DateTime,
            DataTypeNames.TypeTime => StandardDataTypes.Time,
            DataTypeNames.TypeString => StandardDataTypes.String,
            DataTypeNames.TypeObject => StandardDataTypes.Object,
            _ => StandardDataTypes.Unknown
        };
    }

    /// <summary>
    /// Maps the delimiter text to the matching enumeration value.
    /// </summary>
    /// <param name="delimiter">A string containing the delimiter text.</param>
    /// <returns>
    /// A <see cref="StandardDelimiterTypes" /> enumerated value for the data type.
    /// </returns>
    /// <remarks>
    /// This is used when initializing the delimiter dictionary.
    /// </remarks>
    public StandardDelimiterTypes MapDelimiter(string delimiter)
    {
        return delimiter switch
        {
            DelimiterNames.DelimiterSpace => StandardDelimiterTypes.Separator,
            DelimiterNames.DelimiterCr => StandardDelimiterTypes.Separator,
            DelimiterNames.DelimiterLf => StandardDelimiterTypes.Separator,
            DelimiterNames.DelimiterChar => StandardDelimiterTypes.CharacterLiteral,
            DelimiterNames.DelimiterString => StandardDelimiterTypes.StringLiteral,
            DelimiterNames.DelimiterOpenParens => StandardDelimiterTypes.ExpressionStart,
            DelimiterNames.DelimiterCloseParens => StandardDelimiterTypes.ExpressionEnd,
            DelimiterNames.DelimiterOpenBracket => StandardDelimiterTypes.SizingStart,
            DelimiterNames.DelimiterCloseBracket => StandardDelimiterTypes.SizingEnd,
            DelimiterNames.DelimiterOpenBlockBracket => StandardDelimiterTypes.BlockStart,
            DelimiterNames.DelimiterCloseBlockBracket => StandardDelimiterTypes.BlockEnd,
            DelimiterNames.DelimiterListSeparator => StandardDelimiterTypes.Separator,
            _ => StandardDelimiterTypes.Unknown
        };
    }

    /// <summary>
    /// Maps the delimiter token text to the appropriate <see cref="TokenType" /> enumerated value.
    /// </summary>
    /// <param name="delimiterText">A string containing the delimiter text to be evaluated.</param>
    /// <returns>
    /// A <see cref="TokenType" /> enumerated value.
    /// </returns>
    public TokenType MapDelimiterToken(string delimiterText)
    {
        return Delimiters.GetTokenType(delimiterText);
    }

    /// <summary>
    /// Maps the function name text to the matching enumeration value.
    /// </summary>
    /// <param name="functionName">A string containing the function name text.</param>
    /// <returns>
    /// A <see cref="BlazorBasicFunctions"/>  enumerated value for the function.
    /// </returns>
    /// <remarks>
    /// This is used when initializing the functions dictionary.
    /// </remarks>
    public BlazorBasicFunctions MapFunction(string functionName)
    {
        return functionName switch
        {
            FunctionNames.FunctionAbs => BlazorBasicFunctions.Abs,
            FunctionNames.FunctionAsc => BlazorBasicFunctions.Asc,
            FunctionNames.FunctionChr => BlazorBasicFunctions.Chr,
            FunctionNames.FunctionCos => BlazorBasicFunctions.Cos,
            FunctionNames.FunctionLTrim => BlazorBasicFunctions.LTrim,
            FunctionNames.FunctionRTrim => BlazorBasicFunctions.RTrim,
            FunctionNames.FunctionSin => BlazorBasicFunctions.Sin,
            FunctionNames.FunctionTrim => BlazorBasicFunctions.Trim,
            FunctionNames.FunctionVer => BlazorBasicFunctions.Ver,
            _ => BlazorBasicFunctions.Unknown
        };
    }

    /// <summary>
    /// Maps the keyword name text to the matching enumeration value.
    /// </summary>
    /// <param name="keyword">A string containing the keyword text.</param>
    /// <returns>
    /// A <see cref="BlazorBasicKeywords" /> enumerated value for the keyword.
    /// </returns>
    /// <remarks>
    /// This is used when initializing the keywords dictionary.
    /// </remarks>
    public BlazorBasicKeywords MapKeyword(string keyword)
    {
        return keyword switch
        {
            KeywordNames.CommandNoOp => BlazorBasicKeywords.NoOp,
            KeywordNames.CommandCommentShort => BlazorBasicKeywords.Comment,
            KeywordNames.CommandCommentRemark => BlazorBasicKeywords.Comment,
            KeywordNames.CommandClose => BlazorBasicKeywords.Close,
            KeywordNames.CommandCls => BlazorBasicKeywords.Cls,
            KeywordNames.CommandDim => BlazorBasicKeywords.Dim,
            KeywordNames.CommandDo => BlazorBasicKeywords.Do,
            KeywordNames.CommandEnd => BlazorBasicKeywords.End,
            KeywordNames.CommandFor => BlazorBasicKeywords.For,
            KeywordNames.CommandFunction => BlazorBasicKeywords.Function,
            KeywordNames.CommandIf => BlazorBasicKeywords.If,
            KeywordNames.CommandInput => BlazorBasicKeywords.Input,
            KeywordNames.CommandLet => BlazorBasicKeywords.Let,
            KeywordNames.CommandLoop => BlazorBasicKeywords.Loop,
            KeywordNames.CommandOpen => BlazorBasicKeywords.Open,
            KeywordNames.CommandNext => BlazorBasicKeywords.Next,
            KeywordNames.CommandPrint => BlazorBasicKeywords.Print,
            KeywordNames.CommandProcedure => BlazorBasicKeywords.Procedure,
            KeywordNames.CommandRead => BlazorBasicKeywords.Read,
            KeywordNames.CommandWrite => BlazorBasicKeywords.Write,
            KeywordNames.IOAppend => BlazorBasicKeywords.Append,
            KeywordNames.IOOutput => BlazorBasicKeywords.Output,
            KeywordNames.IORandom => BlazorBasicKeywords.Random,
            KeywordNames.KeywordAs => BlazorBasicKeywords.As,
            KeywordNames.KeywordThen => BlazorBasicKeywords.Then,
            _ => BlazorBasicKeywords.Unknown
        };
    }

    /// <summary>
    /// Maps the operator code to the matching enumeration value.
    /// </summary>
    /// <param name="operatorCode">A string containing the text/code for the operator.</param>
    /// <returns>
    /// A <see cref="StandardOperatorTypes" /> enumerated value for the data type.
    /// </returns>
    /// <remarks>
    /// This is used when initializing the operator dictionary.
    /// </remarks>
    public StandardOperatorTypes MapOperator(string operatorCode)
    {
        return operatorCode switch
        {
            OperatorNames.OperatorAdd => StandardOperatorTypes.Arithmetic,
            OperatorNames.OperatorSubtract => StandardOperatorTypes.Arithmetic,
            OperatorNames.OperatorMultiply => StandardOperatorTypes.Arithmetic,
            OperatorNames.OperatorDivide => StandardOperatorTypes.Arithmetic,
            OperatorNames.OperatorModulus => StandardOperatorTypes.Arithmetic,
            OperatorNames.OperatorExponent => StandardOperatorTypes.Arithmetic,
            OperatorNames.OperatorAssignment => StandardOperatorTypes.Assignment,
            OperatorNames.OperatorBitwiseAnd => StandardOperatorTypes.Bitwise,
            OperatorNames.OperatorBitwiseOr => StandardOperatorTypes.Bitwise,
            OperatorNames.OperatorGreaterThan => StandardOperatorTypes.Comparison,
            OperatorNames.OperatorGreaterThanOrEqualTo => StandardOperatorTypes.Comparison,
            OperatorNames.OperatorLessThan => StandardOperatorTypes.Comparison,
            OperatorNames.OperatorLessThanOrEqualTo => StandardOperatorTypes.Comparison,
            OperatorNames.OperatorNotEqualTo => StandardOperatorTypes.Comparison,
            OperatorNames.OperatorEqualTo => StandardOperatorTypes.Comparison,
            OperatorNames.OperatorLogicalAnd => StandardOperatorTypes.Logical,
            OperatorNames.OperatorLogicalOr => StandardOperatorTypes.Logical,
            OperatorNames.OperatorLogicalNot => StandardOperatorTypes.Logical,
            OperatorNames.OperatorLogicalAndShort => StandardOperatorTypes.Logical,
            OperatorNames.OperatorLogicalOrShort => StandardOperatorTypes.Logical,
            OperatorNames.OperatorLogicalNotShort => StandardOperatorTypes.Logical,
            OperatorNames.OperatorIncrement => StandardOperatorTypes.Increment,
            OperatorNames.OperatorDecrement => StandardOperatorTypes.Decrement,
            _ => StandardOperatorTypes.Unknown
        };

    }

    /// <summary>
    /// Maps the operator token text to the appropriate <see cref="TokenType" /> enumerated value.
    /// </summary>
    /// <param name="operatorText">A string containing the operator text to be evaluated.</param>
    /// <returns>
    /// A <see cref="TokenType" /> enumerated value.
    /// </returns>
    public TokenType MapOperatorToken(string operatorText)
    {
        return Operators.GetTokenType(operatorText);
    }

    /// <summary>
    /// Returns the list of known built-in functions.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of <see cref="string" /> containing the reserved words/text that represent
    /// each of the built-in functions for the language.
    /// </returns>
    public List<string> RenderBuiltInFunctions()
    {
        return new List<string>(9)
        {
            FunctionNames.FunctionAbs,
            FunctionNames.FunctionAsc,
            FunctionNames.FunctionChr,
            FunctionNames.FunctionCos,
            FunctionNames.FunctionLTrim,
            FunctionNames.FunctionRTrim,
            FunctionNames.FunctionSin,
            FunctionNames.FunctionTrim,
            FunctionNames.FunctionVer
        };
    }

    /// <summary>
    /// Returns the list of known built-in data types.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of <see cref="string" /> containing the reserved words/text that represent
    /// each of the built-in data types for the language.
    /// </returns>
    public List<string> RenderDataTypes()
    {
        return new List<string>(13)
        {
            DataTypeNames.TypeBool,
            DataTypeNames.TypeByte,
            DataTypeNames.TypeChar,
            DataTypeNames.TypeShort,
            DataTypeNames.TypeInteger,
            DataTypeNames.TypeLong,
            DataTypeNames.TypeFloat,
            DataTypeNames.TypeDouble,
            DataTypeNames.TypeDate,
            DataTypeNames.TypeDateTime,
            DataTypeNames.TypeTime,
            DataTypeNames.TypeString,
            DataTypeNames.TypeObject
        };
    }

    /// <summary>
    /// Returns the list of known built-in delimiters for parsing.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of <see cref="string" /> containing the reserved words/text that represent
    /// each of the built-in delimiters for the language (when being parsed).
    /// </returns>
    public List<string> RenderDelimiters()
    {
        return new List<string>(12)
        {
            DelimiterNames.DelimiterSpace,
            DelimiterNames.DelimiterCr,
            DelimiterNames.DelimiterLf,
            DelimiterNames.DelimiterChar,
            DelimiterNames.DelimiterString,
            DelimiterNames.DelimiterOpenParens,
            DelimiterNames.DelimiterCloseParens,
            DelimiterNames.DelimiterOpenBracket,
            DelimiterNames.DelimiterCloseBracket,
            DelimiterNames.DelimiterOpenBlockBracket,
            DelimiterNames.DelimiterCloseBlockBracket,
            DelimiterNames.DelimiterListSeparator
        };
    }

    /// <summary>
    /// Returns the list of known built-in keywords.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of <see cref="string" /> containing the reserved words/text that represent
    /// each of the built-in keywords for the language.
    /// </returns>
    public List<string> RenderKeywordNames()
    {
        return new List<string>(24)
        {
            KeywordNames.CommandNoOp,
            KeywordNames.CommandCommentShort,
            KeywordNames.CommandCommentRemark,
            KeywordNames.CommandClose,
            KeywordNames.CommandCls,
            KeywordNames.CommandDim,
            KeywordNames.CommandDo,
            KeywordNames.CommandEnd,
            KeywordNames.CommandFor,
            KeywordNames.CommandFunction,
            KeywordNames.CommandIf,
            KeywordNames.CommandInput,
            KeywordNames.CommandLet,
            KeywordNames.CommandLoop,
            KeywordNames.CommandOpen,
            KeywordNames.CommandNext,
            KeywordNames.CommandPrint,
            KeywordNames.CommandProcedure,
            KeywordNames.CommandRead,
            KeywordNames.CommandWrite,
            KeywordNames.IOAppend,
            KeywordNames.IOOutput,
            KeywordNames.IORandom,
            KeywordNames.KeywordAs,
            KeywordNames.KeywordThen
        };
    }

    /// <summary>
    /// Returns the list of known built-in operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of <see cref="string" /> containing the reserved words/text that represent
    /// each of the built-in operators for the language.
    /// </returns>
    public List<string> RenderOperators()
    {
        return new List<string>(20)
        {
            OperatorNames.OperatorAdd,
            OperatorNames.OperatorSubtract,
            OperatorNames.OperatorMultiply,
            OperatorNames.OperatorDivide,
            OperatorNames.OperatorModulus,
            OperatorNames.OperatorExponent,
            OperatorNames.OperatorAssignment,
            OperatorNames.OperatorBitwiseAnd,
            OperatorNames.OperatorBitwiseOr,
            OperatorNames.OperatorGreaterThan,
            OperatorNames.OperatorGreaterThanOrEqualTo,
            OperatorNames.OperatorLessThan,
            OperatorNames.OperatorLessThanOrEqualTo,
            OperatorNames.OperatorNotEqualTo,
            OperatorNames.OperatorEqualTo,
            OperatorNames.OperatorLogicalAnd,
            OperatorNames.OperatorLogicalOr,
            OperatorNames.OperatorLogicalNot,
            OperatorNames.OperatorLogicalAndShort,
            OperatorNames.OperatorLogicalOrShort,
            OperatorNames.OperatorLogicalNotShort,
            OperatorNames.OperatorIncrement,
            OperatorNames.OperatorDecrement
        };
    }

    #endregion
}