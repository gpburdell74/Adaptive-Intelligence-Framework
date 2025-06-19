using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides a token to represent a delimiter that marks the start or end of a literal string value.
/// </summary>
/// <seealso cref="TokenBase" />
public class StringDelimiterToken : TokenBase, IDelimiterToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringDelimiterToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public StringDelimiterToken(string? text) : base(TokenType.StringDelimiter, text)
    {
    }
}
