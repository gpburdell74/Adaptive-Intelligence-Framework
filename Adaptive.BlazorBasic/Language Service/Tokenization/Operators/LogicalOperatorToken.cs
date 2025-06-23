using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent a logical operator such as AND, OR, or NOT (&amp;&amp; || and !).
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IOperatorToken" />
public class LogicalOperatorToken : TokenBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LogicalOperatorToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public LogicalOperatorToken(string? text) : base(TokenType.LogicalOperator, text)
    {
    }
}
