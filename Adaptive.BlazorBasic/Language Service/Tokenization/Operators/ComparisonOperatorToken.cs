using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent a comparison operator.
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IOperatorToken" />
public class ComparisonOperatorToken : TokenBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ComparisonOperatorToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public ComparisonOperatorToken(string? text) : base(TokenType.ComparisonOperator, text)
    {
    }

}