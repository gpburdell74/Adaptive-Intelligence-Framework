using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent an increment operator, such as: ++
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IOperatorToken" />
public class IncrementOperatorToken : TokenBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IncrementOperatorToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public IncrementOperatorToken(string text) : base(TokenType.IncrementOperator, text)
    {
    }
}
