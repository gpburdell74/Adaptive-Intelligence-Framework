namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for a custom code parsing engine.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ISourceCodeParser : IDisposable
{
    /// <summary>
    /// Gets the reference to the logging instance.
    /// </summary>
    /// <value>
    /// The <see cref="IParserOutputLogger"/> instance used during the parsing process.
    /// </value>
    IParserOutputLogger? Logger { get; }

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="rawText">
    /// A string containing the complete list of source code to be parsed.
    /// </param>
    /// <returns>
    /// 
    /// </returns>
    List<object> ParseCodeContent(string rawText);

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="rawText">
    /// An <see cref="IEnumerable{T}"/> of strings containing the complete list of source code to be parsed.
    /// </param>
    /// <returns>
    /// 
    /// </returns>
    List<object> ParseCodeContent(IEnumerable<string> rawText);

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="sourceStream">
    /// An open <see cref="Stream"/> to read the complete list of source code to be parsed 
    /// </param>
    /// <returns>
    /// 
    /// </returns>
    List<object> ParseCodeContent(Stream sourceStream);

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="tokenizedCodeLines">
    /// An <see cref="List{T}"/> of <see cref="ITokenizedCodeLine"/> instances containing the tokenized list of code items.
    /// </param>
    /// <returns>
    /// 
    /// </returns>

    List<object> ParseCodeContent(List<ITokenizedCodeLine> tokenizedCodeLines);
}
