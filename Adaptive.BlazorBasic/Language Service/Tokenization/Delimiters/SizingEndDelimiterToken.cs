using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent a delimiter marking the end of a sizing definition.
/// </summary>
/// <seealso cref="TokenBase" />
public class SizingEndDelimiterToken : TokenBase, IDelimiterToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SizingEndDelimiterToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public SizingEndDelimiterToken(string? text) : base(TokenType.SizingEndDelimiter, text)
    {
    }
}
