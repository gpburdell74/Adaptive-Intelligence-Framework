using Adaptive.Intelligence.LanguageService.Dictionaries;

namespace Adaptive.Intelligence.LanguageService.Services;

/// <summary>
/// Provides the signature definition for a comprehensive language service.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ILanguageService<DelimiterType, ErrorType, FunctionType, KeywordType, OperatorType> : ILanguageService
    where DelimiterType : Enum
    where ErrorType : Enum
    where FunctionType : Enum
    where KeywordType : Enum
    where OperatorType : Enum
{
    #region Properties

    #region Dictionaries
    /// <summary>
    /// Gets the reference to the delimiters dictionary.
    /// </summary>
    /// <value>
    /// The <see cref="IDelimiterDictionary"/> containing the list of valid delimiter definitions.
    /// </value>
    IDelimiterDictionary<DelimiterType> Delimiters { get; }

    /// <summary>
    /// Gets the reference to the error types dictionary.
    /// </summary>
    /// <value>
    /// The <see cref="IErrorDictionary"/> containing the list of valid error types.
    /// </value>
    IErrorDictionary<ErrorType> Errors { get; }

    /// <summary>
    /// Gets the reference to the built-in functions dictionary.
    /// </summary>
    /// <value>
    /// The <see cref="IBuiltInFunctionDictionary"/> containing the list of built-in functions.
    /// </value>
    IBuiltInFunctionDictionary<FunctionType> Functions { get; }

    /// <summary>
    /// Gets the reference to the keywords dictionary.
    /// </summary>
    /// <value>
    /// The <see cref="IKeywordDictionary"/> containing the list of language keywords.
    /// </value>
    IKeywordDictionary<KeywordType> Keywords { get; }

    /// <summary>
    /// Gets the reference to the operators dictionary.
    /// </summary>
    /// <value>
    /// The <see cref="IOperatorDictionary{T}"/> containing the list of operators.
    /// </value>
    IOperatorDictionary<OperatorType> Operators { get; }
    #endregion

    #endregion

    #region Methods / Functions
    /// <summary>
    /// Initializes the language service with the code providers supplied from the provider service.
    /// </summary>
    /// <param name="providerService">
    /// The <see cref="ILanguageProviderService{D,E,F,K,O}"/> provider service implementation.
    /// </param>
    void Initialize(ILanguageProviderService<DelimiterType, ErrorType, FunctionType, KeywordType, OperatorType> providerService);
    #endregion
}