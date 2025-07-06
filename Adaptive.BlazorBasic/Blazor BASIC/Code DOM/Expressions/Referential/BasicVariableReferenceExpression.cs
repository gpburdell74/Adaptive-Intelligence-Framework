using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a variable referenced by name in BASIC.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ICodeExpression" />
public class BasicVariableReferenceExpression : BasicExpression, ICodeVariableReferenceExpression
{
    #region Private Member Declarations
    /// <summary>
    /// The variable name.
    /// </summary>
    private string? _variableName;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariableReferenceExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> service instance.
    /// </param>
    public BasicVariableReferenceExpression(BlazorBasicLanguageService service) : base(service)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariableReferenceExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> service instance.
    /// </param>
    /// <param name="variableName">
    /// A string containing the name of the variable.
    /// </param>
    public BasicVariableReferenceExpression(BlazorBasicLanguageService service, string variableName) : base(service)
    {
        _variableName = variableName;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _variableName = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the name of the variable.
    /// </summary>
    /// <value>
    /// A string containing the name of the variable.
    /// </value>
    public string? VariableName => _variableName;
    ICodeVariableNameExpression? ICodeVariableReferenceExpression.VariableName { get; }
    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">A string containing the expression to be parsed.</param>
    protected override void ParseLiteralContent(string? expression)
    {
    }


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
    /// <param name="engine">
    /// The reference to the execution engine instance.
    /// </param>
    /// <param name="environment">
    /// The reference to the execution environment instance.
    /// </param>
    /// <param name="scope">
    /// The <see cref="IScopeContainer" /> instance, such as a procedure or function, in which scoped
    /// variables are declared.</param>
    /// <returns>
    /// A string containing the user-defined text value.
    /// </returns>
    public override object Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope)
    {
        return ((BlazorBasicVariable)scope.GetVariable(VariableName)).GetValue();
    }
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        return _variableName;
    }
    #endregion
}
