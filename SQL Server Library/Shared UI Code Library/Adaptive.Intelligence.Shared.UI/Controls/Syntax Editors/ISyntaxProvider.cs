namespace Adaptive.Intelligence.Shared.UI;

/// <summary>
/// Provides the signature definition for implementing a basic syntax-colorizer set of rules.
/// </summary>
public interface ISyntaxProvider : IDisposable 
{

    /// <summary>
    /// Gets the reference to the string to color dictionary.
    /// </summary>
    /// <value>
    /// A <see cref="Dictionary{TKey, TValue}"/> of string key values as listed in
    /// the <see cref="WordList"/> property along with the associated color.
    /// </value>
    Dictionary<string, Color> Colors { get; }

    /// <summary>
    /// Gets the comment block end delimiter.
    /// </summary>
    /// <value>
    /// A string containing the comment block end value, or <b>null</b> if comments are not used.
    /// </value>
    string? CommentBlockEnd { get; }

    /// <summary>
    /// Gets the comment block start delimiter.
    /// </summary>
    /// <value>
    /// A string containing the comment block start value, or <b>null</b> if comments are not used.
    /// </value>
    string? CommentBlockStart { get; }

    /// <summary>
    /// Gets the ending delimiter for a comment line.
    /// </summary>
    /// <value>
    /// A string containing the comment single line end value, or <b>null</b> if comments are not used.
    /// </value>
    string? CommentLineEndDelimiter { get; }

    /// <summary>
    /// Gets the starting delimiter for a comment line.
    /// </summary>
    /// <value>
    /// A string containing the comment single line start value, or <b>null</b> if comments are not used.
    /// </value>
    string? CommentLineStartDelimiter { get; }

    /// <summary>
    /// Gets a value indicating whether comments are supported.
    /// </summary>
    /// <value>
    ///   <c>true</c> if comments are supported; otherwise, <c>false</c>.
    /// </value>
    bool SupportsComments { get; }

    /// <summary>
    /// Gets the reference to the list of keywords and other values to highlight.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="string"/> containing the string values
    /// to be highlighted.
    /// </value>
    List<string> WordList { get; }

    /// <summary>
    /// Formats the code/text value.
    /// </summary>
    /// <param name="originalContent">
    /// A string containing the original content to be formatted.
    /// </param>
    /// <returns>
    /// An <see cref="IOperationalResult{T}"/> of <see cref="string"/> containing the formatted version of 
    /// the text if successful; otherwise, contains the list of exceptions.
    /// </returns>
    IOperationalResult<string?> FormatCode(string originalContent);
}
