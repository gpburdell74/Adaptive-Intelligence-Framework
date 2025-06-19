using Adaptive.BlazorBasic.LanguageService.CodeDom;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the signature definition for the internal worker object used to support a code/language parser instance.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ICodeParserWorker : IDisposable
{
    /// <summary>
    /// Determines whether the specified character is a delimiter.
    /// </summary>
    /// <param name="c">
    /// The character to be examined.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the specified character is a delimiter; otherwise, <c>false</c>.
    /// </returns>
    bool IsDelimiter(char c);

    /// <summary>
    /// Creates the code statements and expressions from the provided tokenized code lines.
    /// </summary>
    /// <param name="tokenizedCodeLines">
    /// A <see cref="List{T}"/> of <see cref="ITokenizedCodeLine"/> instances containing the items to be 
    /// translated into code statements and expressions.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="ILanguageCodeStatement"/> instances containing the Code DOM for the line.
    /// </returns>
    List<ILanguageCodeStatement> CreateCodeStatements(List<ITokenizedCodeLine> tokenizedCodeLines);

    /// <summary>
    /// Iterates through the tokenized code lines to find user declarations of procedures, functions, variables, and 
    /// any other necessary user-defined items for reference.
    /// </summary>
    /// <param name="tokenizedCodeLines">
    /// A <see cref="List{T}"/> of <see cref="ITokenizedCodeLine"/> instances.
    /// </param>
    /// <returns>
    /// An <see cref="IUserReferenceTable"/> containing the list of user-defined elements.
    /// </returns>
    IUserReferenceTable FindUserDeclarations(List<ITokenizedCodeLine> tokenizedCodeLines);

    /// <summary>
    /// Performs any necessary pre-processing on the code line.
    /// </summary>
    /// <param name="codeLine">
    /// A string containing the original line of code.
    /// </param>
    /// <returns>
    /// A string containing the modified value.
    /// </returns>
    string? PreProcess(string? codeLine);

    /// <summary>
    /// Pre-processes all the code lines in the stream.
    /// </summary>
    /// <param name="sourceStream">
    /// The source <see cref="Stream"/> to be read from.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of strings containing the code lines, or <b>null</b> if the operation fails.
    /// </returns>
    List<string>? PreProcessStream(Stream sourceStream);

    /// <summary>
    /// Pre-processes all the code lines in the stream.
    /// </summary>
    /// <param name="sourceStream">
    /// The source <see cref="Stream"/> to be read from.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of strings containing the code lines, or <b>null</b> if the operation fails.
    /// </returns>
    Task<List<string>?> PreProcessStreamAsync(Stream sourceStream);

    /// <summary>
    /// Parses the provided line of code into a list of tokens.
    /// </summary>
    /// <param name="codeLine">
    /// A string containing the code line to be parsed.
    /// </param>
    /// <returns>
    /// An <see cref="ITokenizedCodeLine"/> instance of successful; otherwise, 
    /// returns <b>null</b>.
    /// </returns>
    ITokenizedCodeLine? TokenizeLine(string? codeLine);

    /// <summary>
    /// Parses the provided list of lines of code into a list of tokens for each line.
    /// </summary>
    /// <param name="codeLines">
    /// A <see cref="List{T}"/> of strings each containing the code line to be parsed.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="ITokenizedCodeLine"/> instances of successful; otherwise, 
    /// returns <b>null</b>.
    /// </returns>
    List<ITokenizedCodeLine>? TokenizeCodeLines(List<string>? codeLines);
}