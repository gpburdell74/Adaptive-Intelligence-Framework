using Adaptive.LanguageService.Tokenization;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides a token for an element of text that could not be parsed or understood.
/// </summary>
/// <seealso cref="TokenBase" />
public class ErrorToken : TokenBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorToken"/> class.
    /// </summary>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    public ErrorToken(string? text) : base(TokenType.Error, text)
    {
    }
}
