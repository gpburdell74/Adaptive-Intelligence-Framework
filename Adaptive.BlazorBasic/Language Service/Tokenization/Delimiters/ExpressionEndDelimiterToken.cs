using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent the delimiter marking the end of an expression.
/// </summary>
/// <seealso cref="TokenBase" />
public class ExpressionEndDelimiterToken : TokenBase, IDelimiterToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionEndDelimiterToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public ExpressionEndDelimiterToken(string? text) : base(TokenType.ExpressionEndDelimiter, text)
    {
    }
}
