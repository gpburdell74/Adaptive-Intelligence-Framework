using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token to represent a known (built-in) data type name.
/// </summary>
/// <seealso cref="TokenBase" />
/// <seealso cref="IReservedWordToken" />
public class DataTypeNameToken : TokenBase, IReservedWordToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataTypeNameToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public DataTypeNameToken(string? text) : base(TokenType.DataTypeName, text)
    {
    }
}