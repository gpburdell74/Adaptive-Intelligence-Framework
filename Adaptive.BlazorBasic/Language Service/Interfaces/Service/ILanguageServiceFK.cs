namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for language service implementations.
/// </summary>
/// <seealso cref="IDisposable" />
/// <typeparam name="FunctionsEnum">
/// An enumerator definition containing the list of known built-in functions.
/// </typeparam>
/// <typeparam name="KeywordsEnum">
/// An enumerator definition containing the list of keywords.
/// </typeparam>
public interface ILanguageService<FunctionsEnum, KeywordsEnum> : ILanguageService
    where FunctionsEnum : Enum
    where KeywordsEnum : Enum
{
    #region Properties
    /// <summary>
    /// Gets the reference to the functions dictionary for the language.
    /// </summary>
    /// <value>
    /// An <see cref="IFunctionDictionary"/> specifying the names for the built-in functions in the language.
    /// </value>
    IFunctionDictionary<FunctionsEnum, KeywordsEnum> Functions { get; }
    /// <summary>
    /// Gets the reference to the keywords dictionary for the language.
    /// </summary>
    /// <value>
    /// An <see cref="IKeywordDictionary"/> specifying the names for the keywords in the language.
    /// </value>
    IKeywordDictionary<FunctionsEnum, KeywordsEnum> Keywords { get; }
    #endregion

    #region Methods / Functions
    /// <summary>
    /// Determines whether the specified text refers to an item in the <see cref="Functions"/> list.
    /// </summary>
    /// <param name="code">
    /// A string containing the code to evaluate.
    /// </param>
    /// <returns>
    ///   <c>true</c> the specified text refers to a specified built-in function name; otherwise, <c>false</c>.
    /// </returns>
    bool IsBuiltInFunction(string code);

    /// <summary>
    /// Determines whether the specified text refers to an item in the <see cref="Keywords"/> list.
    /// </summary>
    /// <param name="code">
    /// A string containing the code to evaluate.
    /// </param>
    /// <returns>
    ///   <c>true</c> the specified text refers to a specified keyword; otherwise, <c>false</c>.
    /// </returns>
    bool IsKeyWord(string code);

    /// <summary>
    /// Maps the function name text to the matching enumeration value.
    /// </summary>
    /// <remarks>
    /// This is used when initializing the functions dictionary.
    /// </remarks>
    /// <param name="functionName">
    /// A string containing the function name text.</param>
    /// <returns>
    /// A <typeparamref name="FunctionsEnum"/> enumerated value for the function.
    /// </returns>
    FunctionsEnum MapFunction(string functionName);

    /// <summary>
    /// Maps the keyword name text to the matching enumeration value.
    /// </summary>
    /// <remarks>
    /// This is used when initializing the keywords dictionary.
    /// </remarks>
    /// <param name="keyword">
    /// A string containing the keyword text.
    /// </param>
    /// <returns>
    /// A <typeparamref name="KeywordsEnum"/> enumerated value for the keyword.
    /// </returns>
    KeywordsEnum MapKeyword(string keyword);
    #endregion
}
