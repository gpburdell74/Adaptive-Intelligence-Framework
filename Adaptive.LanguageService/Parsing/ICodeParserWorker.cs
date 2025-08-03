using Adaptive.Intelligence.LanguageService.Dictionaries;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.LanguageService.Parsing;

/// <summary>
/// Provides the signature definition for the internal worker object used to support a code/language parser instance.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ICodeParserWorker : IDisposable
{
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
}