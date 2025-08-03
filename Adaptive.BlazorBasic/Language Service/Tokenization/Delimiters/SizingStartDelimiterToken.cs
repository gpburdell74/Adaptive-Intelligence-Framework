using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent a delimiter marking the start of a sizing definition.
/// </summary>
/// <seealso cref="TokenBase" />
public class SizingStartDelimiterToken : TokenBase, IDelimiterToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SizingStartDelimiterToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public SizingStartDelimiterToken(string? text) : base(TokenType.SizingStartDelimiter, text)
    {
    }
}
