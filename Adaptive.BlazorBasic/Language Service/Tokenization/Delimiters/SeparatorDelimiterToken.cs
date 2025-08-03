using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent a generic item separator delimiter.
/// </summary>
/// <seealso cref="TokenBase" />
public class SeparatorDelimiterToken : TokenBase, IDelimiterToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SeparatorDelimiterToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public SeparatorDelimiterToken(string? text) : base(TokenType.SeparatorDelimiter, text)
    {
    }
}
