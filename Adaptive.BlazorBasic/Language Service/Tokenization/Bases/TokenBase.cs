using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService.Tokenization;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the base definition for token types used in parsing code text.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public abstract class TokenBase : DisposableObjectBase, IToken
{
    #region Private Member Declarations
    /// <summary>
    /// The token type.
    /// </summary>
    private readonly TokenType _type;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="TokenBase"/> class.
    /// </summary>
    /// <param name="type">
    /// A <see cref="TokenType"/> enumerated value indicating the type of token.
    /// </param>
    /// <param name="text">
    /// A string containing the text the token was created from.
    /// </param>
    protected TokenBase(TokenType type, string? text)
    {
        _type = type;
        Text = text;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        Text = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the original text the token was created from.
    /// </summary>
    /// <value>
    /// A string containing the original text.
    /// </value>
    public string? Text { get; set; }

    /// <summary>
    /// Gets the type of the token being represented.
    /// </summary>
    /// <value>
    /// A <see cref="TokenType" /> enumerated value indicating the type of the token.
    /// </value>
    public TokenType TokenType => _type;
    #endregion
}
