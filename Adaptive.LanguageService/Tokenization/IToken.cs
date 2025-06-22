namespace Adaptive.LanguageService.Tokenization;

/// <summary>
/// Provides the signature definition for code-based token instances.
/// </summary>
public interface IToken : IDisposable
{
    /// <summary>
    /// Gets or sets the original text the token was created from.
    /// </summary>
    /// <value>
    /// A string containing the original text.
    /// </value>
    string? Text { get; set; }
    /// <summary>
    /// Gets the type of the token being represented.
    /// </summary>
    /// <value>
    /// A <see cref="TokenType"/> enumerated value indicating the type of the token.
    /// </value>
    TokenType TokenType { get; }
}

