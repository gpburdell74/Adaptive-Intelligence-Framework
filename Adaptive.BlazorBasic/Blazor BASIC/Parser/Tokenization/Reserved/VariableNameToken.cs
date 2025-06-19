using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides a token to represent a user-defined variable name.
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IReservedWordToken" />
public class VariableNameToken : TokenBase, IReservedWordToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VariableNameToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public VariableNameToken(string? text) : base(TokenType.VariableName, text)
    {
    }
}