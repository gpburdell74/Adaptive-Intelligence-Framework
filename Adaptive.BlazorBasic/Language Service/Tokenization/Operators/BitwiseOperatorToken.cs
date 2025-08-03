using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent a bitwise operator such as AND or OR (&amp; or |).
/// </summary>
/// <seealso cref="TokenBase" />
public class BitwiseOperatorToken : TokenBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BitwiseOperatorToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public BitwiseOperatorToken(string? text) : base(TokenType.BitwiseOperator, text)
    {
    }
}
