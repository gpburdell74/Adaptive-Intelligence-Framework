namespace Adaptive.LanguageService.Tokenization;

/// <summary>
/// Represents a single line of code that has been tokenized, and manages the tokens for that line.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ITokenizedCodeLine : IDisposable
{
    #region Properties
    /// <summary>
    /// Gets the number of tokens in the code line.
    /// </summary>
    /// <value>
    /// An integer indicating the number of tokens.
    /// </value>
    int Count { get; }

    /// <summary>
    /// Gets or sets the line number.
    /// </summary>
    /// <value>
    /// The line number specified when parsing.
    /// </value>
    int LineNumber { get; set; }

    /// <summary>
    /// Gets the <see cref="IToken"/> at the specified index.
    /// </summary>
    /// <value>
    /// The <see cref="IToken"/>.
    /// </value>
    /// <param name="index">
    /// An integer specifying the ordinal index of the token.
    /// </param>
    IToken? this[int index] { get; }

    /// <summary>
    /// Gets the reference to the list of tokens for a line of code.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="IToken"/> instances.
    /// </value>
    List<IToken>? TokenList { get; }
    #endregion

    #region Methods
    /// <summary>
    /// Combines the values of each of the tokens into a single string.
    /// </summary>
    /// <param name="startIndex">The ordinal index of the first token.</param>
    /// <param name="endIndex">
    /// The ordinal index of the last token.
    /// </param>
    /// <returns>
    /// A string containing the combined text values.
    /// </returns>
    string CombineValues(int startIndex, int endIndex);

    /// <summary>
    /// Substitutes the new token for the token at the specified index.
    /// </summary>
    /// <param name="index">
    /// An integer containing the ordinal index.
    /// </param>
    /// <param name="newToken">
    /// An <see cref="IToken"/> containing the new token instance.
    /// </param>
    /// <returns>
    /// The reference to the <see cref="IToken"/> that was removed from the list.
    /// </returns>
    public IToken? Substitute(int index, IToken newToken);
    #endregion
}
