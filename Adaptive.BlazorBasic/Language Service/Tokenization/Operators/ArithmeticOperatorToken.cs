using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent an arithmetic operator such as + - / * ^ and %.
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IOperatorToken" />
public class ArithmeticOperatorToken : TokenBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArithmeticOperatorToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public ArithmeticOperatorToken(string? text) : base(TokenType.ArithmeticOperator, text)
    {
    }
}