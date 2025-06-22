using Adaptive.LanguageService.Tokenization;

namespace Adaptive.LanguageService.Services;

/// <summary>
/// Provides the signature definition for a service that creates and manages <see cref="ICodeProvider"/> instances.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ILanguageProviderService<DelimiterType, ErrorType, FunctionType, KeywordType, OperatorType,
    AssignmentType, BitwiseType, ComparisonType, LogicalType, MathType, OperationalType> : IDisposable 
    where DelimiterType : Enum
    where ErrorType : Enum
    where FunctionType : Enum
    where KeywordType : Enum
    where OperatorType : Enum
    where AssignmentType : Enum
    where BitwiseType : Enum
    where ComparisonType : Enum
    where LogicalType : Enum
    where MathType : Enum
    where OperationalType : Enum
{
    /// <summary>
    /// Creates the data type dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IDataTypeDictionary"/> that contains the data types used in the language.
    /// </returns>
    IDataTypeDictionary CreateDataTypeDictionary();
    /// <summary>
    /// Creates the delimiter dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IDataTypeDictionary{T}"/> that contains the delimiters used in the language.
    /// </returns>
    IDelimiterDictionary<DelimiterType> CreateDelimiterDictionary();
    /// <summary>
    /// Creates the errors dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IErrorDictionary"/> that contains the error types used in the language.
    /// </returns>
    IErrorDictionary<ErrorType> CreateErrorDictionary();
    /// <summary>
    /// Creates the built-in functions dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IBuiltInFunctionDictionary{F}"/> that contains the built in functions used in the language.
    /// </returns>
    IBuiltInFunctionDictionary<FunctionType> CreateFunctionsDictionary();
    /// <summary>
    /// Creates the keyword / reserved word dictionary.
    /// </summary>
    /// <returns>
    /// An <see cref="IKeywordDictionary{T}"/> that contains the keywords used in the language.
    /// </returns>
    IKeywordDictionary<KeywordType> CreateKeywordsDictionary();
    /// <summary>
    /// Creates the operators dictionary and sub-dictionaries.
    /// </summary>
    /// <returns>
    /// An <see cref="IOperatorDictionary{T}"/> that contains the operators used in the language.
    /// </returns>
    IOperatorDictionary<OperatorType, AssignmentType, BitwiseType, ComparisonType, LogicalType, MathType, OperationalType> 
        CreateOperatorDictionary();
    /// <summary>
    /// Creates the list of single-character delimiters.
    /// </summary>
    /// <remarks>
    /// This is used when separator delimiters are optional around the particular 1-character token, such as 
    /// parsing: 1+1 or myVariable=3.
    /// </remarks>
    /// <returns>
    /// An <see cref="IDataTypeDictionary"/> that contains the list of delimiters that are single-character length.
    /// </returns>
    List<string> GetSingleCharacterDelimiters();
}
