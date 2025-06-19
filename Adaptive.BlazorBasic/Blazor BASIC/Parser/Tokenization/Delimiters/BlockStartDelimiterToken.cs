using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides a token to represent a delimiter marking the start of a code block definition.
/// </summary>
/// <seealso cref="TokenBase" />
public class BlockStartDelimiterToken : TokenBase, IDelimiterToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockStartDelimiterToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public BlockStartDelimiterToken(string? text) : base(TokenType.BlockStartDelimiter, text)
    {
    }
}
