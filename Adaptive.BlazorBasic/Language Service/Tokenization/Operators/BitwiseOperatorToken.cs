using Adaptive.LanguageService.Tokenization;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent a bitwise operator such as AND or OR (&amp; or |).
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IOperatorToken" />
public class BitwiseOperatorToken : TokenBase, IOperatorToken
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
