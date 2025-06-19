namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for language service implementations.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ILanguageService : IDisposable 
{
    #region Properties
    /// <summary>
    /// Gets the reference to the data types dictionary for the language.
    /// </summary>
    /// <value>
    /// An <see cref="IDataTypeDictionary"/> specifying the names for the data types in the language.
    /// </value>
    IDataTypeDictionary DataTypes { get; }
    /// <summary>
    /// Gets the reference to the delimiters dictionary for the language.
    /// </summary>
    /// <value>
    /// An <see cref="IDelimiterDictionary"/> specifying the text representations for the delimiters in the language.
    /// </value>
    IDelimiterDictionary Delimiters { get; }
    /// <summary>
    /// Gets the reference to the operators dictionary for the language.
    /// </summary>
    /// <value>
    /// An <see cref="IOperatorDictionary"/> specifying the code representations for the operators in the language.
    /// </value>
    IOperatorDictionary Operators { get; }
    #endregion

    #region Methods / Functions
    /// <summary>
    /// Creates the parsing log instance.
    /// </summary>
    /// <returns>
    /// The <see cref="IParserOutputLogger"/> instance to supply to the parser mechanism.
    /// </returns>
    IParserOutputLogger CreateParsingLog();

    /// <summary>
    /// Creates the code parser instance for the language.
    /// </summary>
    /// <returns>
    /// The <see cref="ISourceCodeParser"/> instance to use to parse the source code.
    /// </returns>
    ISourceCodeParser GetParser();

    /// <summary>
    /// Gets the list of single character token values with their mapping from the populated dictionaries.
    /// </summary>
    /// <returns>
    /// A <see cref="Dictionary{TKey, TValue}"/> containing the mapped list.
    /// </returns>
    Dictionary<string, TokenType> GetSingleCharacterTokenValuesFromDictionaries();

    /// <summary>
    /// Initializes the dictionaries and the language service from the specified provider.
    /// </summary>
    void InitializeDictionaries();

    /// <summary>
    /// Determines whether the specified text refers to an item in the <see cref="DataTypes"/> list.
    /// </summary>
    /// <param name="code">
    /// A string containing the code to evaluate.
    /// </param>
    /// <returns>
    ///   <c>true</c> the specified text refers to a specified data type name; otherwise, <c>false</c>.
    /// </returns>
    bool IsDataType(string code);

    /// <summary>
    /// Determines whether the specified text refers to an item in the <see cref="Delimiters"/> list.
    /// </summary>
    /// <param name="code">
    /// A string containing the code to evaluate.
    /// </param>
    /// <returns>
    ///   <c>true</c> the specified text refers to a specified delimiter; otherwise, <c>false</c>.
    /// </returns>
    bool IsDelimiter(string code);

    /// <summary>
    /// Determines whether the specified text refers to an item in the <see cref="Operators"/> list.
    /// </summary>
    /// <param name="code">
    /// A string containing the code to evaluate.
    /// </param>
    /// <returns>
    ///   <c>true</c> the specified text refers to a specified operator; otherwise, <c>false</c>.
    /// </returns>
    bool IsOperator(string code);

    /// <summary>
    /// Maps the delimiter token text to the appropriate <see cref="TokenType"/> enumerated value.
    /// </summary>
    /// <param name="delimiterText">
    /// A string containing the delimiter text to be evaluated.
    /// </param>
    /// <returns>
    /// A <see cref="TokenType"/> enumerated value.
    /// </returns>
    TokenType MapDelimiterToken(string delimiterText);

    /// <summary>
    /// Maps the operator token text to the appropriate <see cref="TokenType"/> enumerated value.
    /// </summary>
    /// <param name="operatorText">
    /// A string containing the operator text to be evaluated.
    /// </param>
    /// <returns>
    /// A <see cref="TokenType"/> enumerated value.
    /// </returns>
    TokenType MapOperatorToken(string operatorText);

    /// <summary>
    /// Maps the data type name to the matching enumeration value.
    /// </summary>
    /// <remarks>
    /// This is used when initializing the data type dictionary.
    /// </remarks>
    /// <param name="dataTypeName">
    /// A string containing the data type name.</param>
    /// <returns>
    /// A <see cref="StandardDataTypes"/> enumerated value for the data type.
    /// </returns>
    public StandardDataTypes MapDataType(string dataTypeName);

    /// <summary>
    /// Maps the delimiter text to the matching enumeration value.
    /// </summary>
    /// <remarks>
    /// This is used when initializing the delimiter dictionary.
    /// </remarks>
    /// <param name="delimiter">
    /// A string containing the delimiter text.</param>
    /// <returns>
    /// A <see cref="StandardDelimiterTypes"/> enumerated value for the data type.
    /// </returns>
    public StandardDelimiterTypes MapDelimiter(string delimiter);

    /// <summary>
    /// Maps the operator code to the matching enumeration value.
    /// </summary>
    /// <remarks>
    /// This is used when initializing the operator dictionary.
    /// </remarks>
    /// <param name="operatorCode">
    /// A string containing the text/code for the operator.
    /// </param>
    /// <returns>
    /// A <see cref="StandardOperatorTypes"/> enumerated value for the data type.
    /// </returns>
    public StandardOperatorTypes MapOperator(string operatorCode);

    /// <summary>
    /// Returns the list of known built-in functions.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> containing the reserved words/text that represent
    /// each of the built-in functions for the language.
    /// </returns>
    List<string> RenderBuiltInFunctions();
    /// <summary>
    /// Returns the list of known built-in data types.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> containing the reserved words/text that represent
    /// each of the built-in data types for the language.
    /// </returns>
    List<string> RenderDataTypes();
    /// <summary>
    /// Returns the list of known built-in delimiters for parsing.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> containing the reserved words/text that represent
    /// each of the built-in delimiters for the language (when being parsed).
    /// </returns>
    List<string> RenderDelimiters();
    /// <summary>
    /// Returns the list of known built-in keywords.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> containing the reserved words/text that represent
    /// each of the built-in keywords for the language.
    /// </returns>
    List<string> RenderKeywordNames();
    /// <summary>
    /// Returns the list of known built-in operators.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> containing the reserved words/text that represent
    /// each of the built-in operators for the language.
    /// </returns>
    List<string> RenderOperators();
    #endregion

}
