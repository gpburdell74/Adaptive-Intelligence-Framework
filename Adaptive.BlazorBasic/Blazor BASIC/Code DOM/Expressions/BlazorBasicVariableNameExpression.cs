using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents and manages a procedure call expression.
/// </summary>
/// <seealso cref="BasicExpression" />
public sealed class BlazorBasicVariableNameExpression : BasicExpression
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicVariableNameExpression"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicVariableNameExpression(BlazorBasicLanguageService service) : base(service)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    public BlazorBasicVariableNameExpression(BlazorBasicLanguageService service, string expression) : base(service)
    {
        ParseContent(expression);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="codeLine">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances from the parent code line instance 
    /// containing the data to be parsed.
    /// </param>
    public BlazorBasicVariableNameExpression(BlazorBasicLanguageService service, List<IToken> codeLine) : base(service)
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
        VariableName = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the name/text of the reserved word being represented.
    /// </summary>
    /// <value>
    /// A string containing the name of the reserved word.
    /// </value>
    public string? VariableName { get; set; }
    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">A string containing the expression to be parsed.</param>
    protected override void ParseLiteralContent(string? expression)
    {
        VariableName = expression;
    }
    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">A <see cref="List{T}" /> of <see cref="IToken" /> instances containing the expression to be parsed.</param>
    protected void ParseCodeLine(List<IToken> codeLine)
    {
        VariableName = codeLine[0].Text;
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    /// <exception cref="Adaptive.Intelligence.Shared.ExceptionEventArgs.Exception"></exception>
    private void ParseContent(string expression)
    {
        VariableName = expression;
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Evaluates the expression.
    /// </summary>
    /// <param name="engine">The execution engine instance.</param>
    /// <param name="environment">The execution environment instance.</param>
    /// <param name="scope">The <see cref="T:Adaptive.Intelligence.LanguageService.Execution.IScopeContainer" /> instance, such as a procedure or function, in which scoped
    /// variables are declared.</param>
    /// <returns>
    /// The result of the object evaluation.
    /// </returns>
    public override object Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope)
    {
        return scope.GetVariable(VariableName).GetValue();
    }
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        return VariableName;
    }

    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
        throw new NotImplementedException();
    }

    #endregion


}
