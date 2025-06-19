using Adaptive.BlazorBasic.LanguageService;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides a token to represent a user-defined procedure name.
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IReservedWordToken" />
public class ProcedureNameToken : TokenBase, IReservedWordToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProcedureNameToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public ProcedureNameToken(string? text) : base(TokenType.ProcedureName, text)
    {
    }
}