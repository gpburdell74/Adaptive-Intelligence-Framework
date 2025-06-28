using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents an integer literal.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public sealed class BlazorBasicLiteralIntegerExpression : BlazorBasicLiteralExpression<long>
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralIntegerExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// A reference to the <see cref="BlazorBasicLanguageService"/> being used.
    /// </param>
    public BlazorBasicLiteralIntegerExpression(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralIntegerExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// A reference to the <see cref="BlazorBasicLanguageService"/> being used.
    /// </param>
    /// <param name="value">
    /// An integer containing the data value.
    /// </param>
    public BlazorBasicLiteralIntegerExpression(BlazorBasicLanguageService service, long value) : base(service)
    {
        Value = value;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralIntegerExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="codeLine">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances from the parent code line instance 
    /// containing the data to be parsed.
    /// </param>
    public BlazorBasicLiteralIntegerExpression(BlazorBasicLanguageService service, List<IToken> codeLine) : base(service)
    {
        ParseCodeLine(codeLine);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        Value = 0;
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
        try
        {
            Value = long.Parse(expression ?? "0");
        }
        catch(Exception ex)
        {
            throw new SyntaxErrorException(0);
        }
    }
    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">A <see cref="List{T}" /> of <see cref="IToken" /> instances containing the expression to be parsed.</param>
    protected override void ParseCodeLine(List<IToken> codeLine)
    {
        ParseLiteralContent(codeLine[0].Text);
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
        return Value.ToString();
    }
    #endregion

}
