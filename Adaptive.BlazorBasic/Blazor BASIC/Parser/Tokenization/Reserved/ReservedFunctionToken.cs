using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides a token to represent a known (built-in) function name.
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IReservedWordToken" />
public class ReservedFunctionToken : TokenBase, IReservedWordToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReservedFunctionToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public ReservedFunctionToken(string text) : base(TokenType.ReservedFunction, text)
    {
    }
}
