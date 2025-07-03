using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;
using System.Linq.Expressions;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a variable referenced by name in BASIC.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicVariableReferenceExpression : BlazorBasicExpression, ILanguageCodeExpression
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
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicVariableReferenceExpression(BlazorBasicLanguageService service) : base(service)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariableReferenceExpression"/> class.
    /// </summary>
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

    protected override void ParseLiteralContent(string? expression)
    {
        throw new NotImplementedException();
    }

    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
        throw new NotImplementedException();
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
    public override T? Evaluate<T>(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope) where T : default
    {
        return ((BlazorBasicVariable<T>)scope.GetVariable(VariableName)).Value;
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
