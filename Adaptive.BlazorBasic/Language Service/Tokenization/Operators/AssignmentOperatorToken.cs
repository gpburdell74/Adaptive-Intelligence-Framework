using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent an assignment operator.
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IOperatorToken" />
public class AssignmentOperatorToken : TokenBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssignmentOperatorToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public AssignmentOperatorToken(string? text) : base(TokenType.AssignmentOperator, text)
    {
    }
}
