using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition and base implementation for Blazor BASIC code expressions.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ICodeExpression" />
public abstract class BasicExpression : DisposableObjectBase, ICodeExpression
{
    #region Private Member Declarations
    /// <summary>
    /// The reference to the language service.
    /// </summary>
    private BlazorBasicLanguageService? _service;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    protected BasicExpression(BlazorBasicLanguageService service)
    {
        _service = service;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    protected BasicExpression(BlazorBasicLanguageService service, string expression) : base()
    {
        _service = service;
        ParseLiteralContent(expression);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    /// <param name="codeLine">
    /// A <see cref="ITokenizedCodeLine"/> containing the code tokens for the entire line of code.
    /// </param>
    /// <param name="startIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine"/> to start parsing the expression.
    /// </param>
    /// <param name="endIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine"/> to end parsing the expression.
    /// </param>
    protected BasicExpression(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine, int startIndex, int endIndex) : base()
    {
        _service = service;
        ParseCodeLine(codeLine, startIndex, endIndex);
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _service = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Protected Properties
    /// <summary>
    /// Gets the reference to the language service.
    /// </summary>
    /// <value>
    /// The reference to the <see cref="BlazorBasicLanguageService"/> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </value>
    protected BlazorBasicLanguageService Service
    {
        get
        {
            if (_service == null)
                throw new Exception("Engine Error!");
            return _service;
        }
    }
    #endregion

    #region Protected Abstract Methods
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    protected abstract void ParseLiteralContent(string? expression);

    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">
    /// A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.
    /// </param>
    /// <param name="startIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.
    /// </param>
    /// <param name="endIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.
    /// </param>
    protected abstract void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex);
    #endregion

    #region Protected Methods / Functions    
    /// <summary>
    /// Normalizes the string for parsing.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    /// <returns>
    /// The modified string.
    /// </returns>
    protected string? NormalizeString(string? expression)
    {
        if (expression == null)
            return null;

        return expression.Trim();
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Evaluates the expression during execution.
    /// </summary>
    /// <param name="engine">The execution engine instance.</param>
    /// <param name="environment">The execution environment instance.</param>
    /// <param name="scope">
    /// The <see cref="IScopeContainer" /> instance, such as a procedure or function, in which scoped
    /// variables are declared.
    /// </param>
    /// <returns>
    /// The result of the expression evaluation.
    /// </returns>
    public abstract object Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope);

    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public abstract string? Render();
    #endregion
}