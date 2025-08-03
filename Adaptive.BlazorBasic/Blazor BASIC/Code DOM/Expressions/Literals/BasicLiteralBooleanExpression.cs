using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;

/// <summary>
/// Represents a character literal.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="BasicLiteralExpression{T}" />
/// <seealso cref="ICodeLiteralBooleanExpression"/>
public sealed class BasicLiteralBooleanExpression : BasicLiteralExpression<bool>, ICodeLiteralBooleanExpression
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicLiteralBooleanExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// A reference to the <see cref="BlazorBasicLanguageService"/> being used.
    /// </param>
    public BasicLiteralBooleanExpression(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicLiteralBooleanExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// A reference to the <see cref="BlazorBasicLanguageService"/> being used.
    /// </param>
    /// <param name="value">
    /// The <see cref="char"/> value being represented.
    /// </param>
    public BasicLiteralBooleanExpression(BlazorBasicLanguageService service, bool value) : base(service)
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
        Value = false;
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
            Value = false;
        else
            Value = bool.Parse(expression);
    }
    #endregion

    #region Protected Method Overrides
    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.</param>
    /// <param name="startIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.</param>
    /// <param name="endIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.</param>
    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
        
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
        return Value;
    }

    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        if (Value == true)
            return "True";
        else
            return "False";
    }
    #endregion
}
