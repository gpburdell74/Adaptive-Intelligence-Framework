using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent the delimiter marking the start of an expression.
/// </summary>
/// <seealso cref="TokenBase" />
public class ExpressionStartDelimiterToken : TokenBase, IDelimiterToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionStartDelimiterToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public ExpressionStartDelimiterToken(string? text) : base(TokenType.ExpressionStartDelimiter, text)
    {
    }

}
