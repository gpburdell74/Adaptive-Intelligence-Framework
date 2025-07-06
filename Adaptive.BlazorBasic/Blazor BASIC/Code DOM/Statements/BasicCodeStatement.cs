using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;

/// <summary>
/// Represents a line of code to be executed in Blazor BASIC.
/// </summary>
/// <remarks>
/// This represents a single line of code, which must start with a command / keyword expression of some kind.
/// </remarks>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeStatement" />
public abstract class BasicCodeStatement : DisposableObjectBase, ILanguageCodeStatement
{
    #region Private Member Declarations
    /// <summary>
    /// The reference to the language service.
    /// </summary>
    private BlazorBasicLanguageService?  _service;
    /// <summary>
    /// The command expression.
    /// </summary>
    private ILanguageKeywordExpression? _commandExpression;
    /// <summary>
    /// The expressions list containing everything after the first command/keyword value.
    /// </summary>
    private List<ICodeExpression>? _expressionsList;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCodeStatement"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    public BasicCodeStatement(BlazorBasicLanguageService service)
    {
        _service = service;
        _expressionsList = new List<ICodeExpression>();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCodeStatement"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> to use to find and compare
    /// text to language reserved words and operators and other items.
    /// </param>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> containing the code line to be parsed.
    /// </param>
    public BasicCodeStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine)
    {
        _service = service;
        _expressionsList = new List<ICodeExpression>();
        LineNumber = codeLine.LineNumber;
        ParseIntoExpressions(codeLine);
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed)
        {
            _expressionsList?.Clear();
            _commandExpression?.Dispose();
        }

        _service = null;
        _expressionsList = null;
        _commandExpression = null;
        OriginalCode = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the reference to the command expression.
    /// </summary>
    /// <value>
    /// An <see cref="ILanguageKeywordExpression"/> instance specifying the command to be invoked.
    /// </value>
    public ILanguageKeywordExpression? CommandExpression
    {
        get => _commandExpression;
        protected set => _commandExpression = value;
    }
    /// <summary>
    /// Gets the reference to the list of expressions that make up the remainder of the statement.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}" /> of <see cref="ICodeExpression" /> instances containing the remainder
    /// of the expressions in the remaining statement code.
    /// </value>
    public List<ICodeExpression> Expressions => _expressionsList;

    /// <summary>
    /// Gets or sets the line number.
    /// </summary>
    /// <value>
    /// An integer specifying the original line number for debugging purposes.
    /// </value>
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is a no-operation statement.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is a statement that does not get executed; otherwise, <c>false</c>.
    /// </value>
    public bool IsNoOp { get; set; } = false;

    /// <summary>
    /// Gets or sets the original command text.
    /// </summary>
    /// <value>
    /// A string containing the command text that was parsed.
    /// </value>
    public string? OriginalCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets the value of how the current number of tabs being printed is to be modified.
    /// </summary>
    /// <value>
    /// A <see cref="RenderTabState"/> enumerated value indicating the tab state.
    /// </value>
    public abstract RenderTabState TabModification { get; }
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

    #region Protected Methods / Functions    
    /// <summary>
    /// Parses the specified code content.
    /// </summary>
    /// <param name="codeLine">
    /// A string containing the code to be parsed.
    /// </param>
    protected abstract void ParseIntoExpressions(ITokenizedCodeLine codeLine);
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public abstract string? Render();
    /// <summary>
    /// Returns a string representation of the current instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return Render() ?? string.Empty;
    }
    #endregion

}