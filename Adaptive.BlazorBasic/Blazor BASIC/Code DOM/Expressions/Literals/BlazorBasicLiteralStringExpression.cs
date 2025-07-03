using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a string literal.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BlazorBasicLiteralStringExpression : BlazorBasicLiteralExpression<string>
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralStringExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    public BlazorBasicLiteralStringExpression(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralStringExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="stringContent">
    /// A string containing the data type name.
    /// </param>
    public BlazorBasicLiteralStringExpression(BlazorBasicLanguageService service, string stringContent) : base(service)
    {
        Value = stringContent;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        Value = string.Empty;
        base.Dispose(disposing);
    }
    #endregion

    #region Protected Method Overrides
    /// <summary>
    /// Evaluates the expression.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="engine">The execution engine instance.</param>
    /// <param name="environment">The execution environment instance.</param>
    /// <param name="scope">The <see cref="T:Adaptive.Intelligence.LanguageService.Execution.IScopeContainer" /> instance, such as a procedure or function, in which scoped
    /// variables are declared.</param>
    /// <returns>
    /// The result of the object evaluation.
    /// </returns>
    public override T? Evaluate<T>(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope) where T : default
    {
        return (T?)(object)Value;
    }


    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">A string containing the expression to be parsed.</param>
    protected override void ParseLiteralContent(string? expression)
    {
        Value = expression;
    }
    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">A <see cref="List{T}" /> of <see cref="IToken" /> instances containing the expression to be parsed.</param>
    protected override void ParseCodeLine(List<IToken> codeLine)
    {
        Value = codeLine[1].Text;
    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        return ParseConstants.DoubleQuote + Value + ParseConstants.DoubleQuote;
    }
    #endregion


}
