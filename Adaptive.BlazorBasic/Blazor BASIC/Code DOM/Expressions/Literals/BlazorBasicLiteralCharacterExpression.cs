using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a character literal.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public sealed class BlazorBasicLiteralCharacterExpression : BlazorBasicLiteralExpression<char>
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralCharacterExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// A reference to the <see cref="BlazorBasicLanguageService"/> being used.
    /// </param>
    public BlazorBasicLiteralCharacterExpression(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralCharacterExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// A reference to the <see cref="BlazorBasicLanguageService"/> being used.
    /// </param>
    /// <param name="value">
    /// The <see cref="char"/> value being represented.
    /// </param>
    public BlazorBasicLiteralCharacterExpression(BlazorBasicLanguageService service, char value) : base(service)
    {
        Value = value;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        Value = '\0';
        base.Dispose(disposing);
    }
    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">A string containing the expression to be parsed.</param>
    protected override void ParseLiteralContent(string? expression)
    {
        if (string.IsNullOrEmpty(expression))
            Value = '\0';

        Value = expression![0];
    }
    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">A <see cref="List{T}" /> of <see cref="IToken" /> instances containing the expression to be parsed.</param>
    protected override void ParseCodeLine(List<IToken> codeLine)
    {
        ParseLiteralContent(codeLine[1].Text);
    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public override string? Render()
    {
        throw new NotImplementedException();
    }
    #endregion

}
