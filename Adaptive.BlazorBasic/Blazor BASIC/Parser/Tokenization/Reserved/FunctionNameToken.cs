using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides a token to represent a user-defined procedure name.
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IReservedWordToken" />
public class FunctionNameToken : TokenBase, IReservedWordToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionNameToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public FunctionNameToken(string? text) : base(TokenType.FunctionName, text)
    {
    }
}