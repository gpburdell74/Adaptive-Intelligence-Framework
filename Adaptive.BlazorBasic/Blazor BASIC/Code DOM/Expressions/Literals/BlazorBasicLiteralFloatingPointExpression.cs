using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents an floating-point literal.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public sealed class BlazorBasicLiteralFloatingPointExpression : BlazorBasicLiteralExpression<float>
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralFloatingPointExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// A reference to the <see cref="BlazorBasicLanguageService"/> being used.
    /// </param>
    public BlazorBasicLiteralFloatingPointExpression(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralFloatingPointExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// A reference to the <see cref="BlazorBasicLanguageService"/> being used.
    /// </param>
    /// <param name="value">
    /// An integer containing the data value.
    /// </param>
    public BlazorBasicLiteralFloatingPointExpression(BlazorBasicLanguageService service, float value) : base(service)
    {
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicLiteralFloatingPointExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="codeLine">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances from the parent code line instance 
    /// containing the data to be parsed.
    /// </param>
    public BlazorBasicLiteralFloatingPointExpression(BlazorBasicLanguageService service, List<IToken> codeLine) : base(service)
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
            Value = float.Parse(expression ?? "0");
        }
        catch (Exception ex)
        {
            throw new BasicSyntaxErrorException(0);
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
    /// <exception cref="System.NotImplementedException"></exception>
    public override T? Evaluate<T>(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope) where T : default
    {
        return (T?)(object)Value;
    }

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
