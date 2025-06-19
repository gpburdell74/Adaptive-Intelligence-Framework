using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides a token to represent a delimiter marking the end of a code block definition.
/// </summary>
/// <seealso cref="TokenBase" />
public class BlockEndDelimiterToken : TokenBase, IDelimiterToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockEndDelimiterToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public BlockEndDelimiterToken(string? text) : base(TokenType.BlockEndDelimiter, text)
    {
    }
}
