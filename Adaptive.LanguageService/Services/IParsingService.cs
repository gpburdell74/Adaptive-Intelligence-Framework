using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Parsing;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.LanguageService.Services;

/// <summary>
/// Provides the signature definition for a language parsing service implementation.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IParsingService<DelimiterType, ErrorType, FunctionType, KeywordType, OperatorType> : IDisposable
    where DelimiterType : Enum
    where ErrorType : Enum
    where FunctionType : Enum
    where KeywordType : Enum
    where OperatorType : Enum
{
    #region Properties
    /// <summary>
    /// Gets the reference to the logging instance.
    /// </summary>
    /// <value>
    /// The <see cref="IParserOutputLogger"/> instance used during the parsing process.
    /// </value>
    IParserOutputLogger? Logger { get; }

    /// <summary>
    /// Gets the reference to the language service used during the parsing operations.
    /// </summary>
    /// <value>
    /// The <see cref="ILanguageService{D,E,F,K,O}"/> instance.
    /// </value>
    ILanguageService<DelimiterType, ErrorType, FunctionType, KeywordType, OperatorType>? Service { get; }
    #endregion

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="rawText">
    /// A string containing the complete list of source code to be parsed.
    /// </param>
    /// <returns>
    /// 
    /// </returns>
    ILanguageCodeStatementsTable ParseCodeContent(string rawText);

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="rawText">
    /// An <see cref="IEnumerable{T}"/> of strings containing the complete list of source code to be parsed.
    /// </param>
    /// <returns>
    /// 
    /// </returns>
    ILanguageCodeStatementsTable ParseCodeContent(IEnumerable<string> rawText);

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="sourceStream">
    /// An open <see cref="Stream"/> to read the complete list of source code to be parsed 
    /// </param>
    /// <returns>
    /// 
    /// </returns>
    IExecutionUnit? ParseCodeContent(Stream sourceStream);

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="tokenizedCodeLines">
    /// An <see cref="List{T}"/> of <see cref="ITokenizedCodeLine"/> instances containing the tokenized list of code items.
    /// </param>
    /// <returns>
    /// 
    /// </returns>
    ILanguageCodeStatementsTable? ParseCodeContent(List<ITokenizedCodeLine> tokenizedCodeLines);
}
