using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides a token to represent the start of end of a character literal.
/// </summary>
/// <seealso cref="TokenBase" />
public class CharacterDelimiterToken : TokenBase, IDelimiterToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterDelimiterToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public CharacterDelimiterToken(string? text) : base(TokenType.CharacterDelimiter, text)
    {
    }
}
