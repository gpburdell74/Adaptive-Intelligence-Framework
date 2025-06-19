using Adaptive.BlazorBasic.LanguageService;
using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic.CodeDom;

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
    /// The command expression.
    /// </summary>
    private ILanguageKeywordExpression? _commandExpression;
    /// <summary>
    /// The expressions list containing everything after the first command/keyword value.
    /// </summary>
    private List<ILanguageCodeExpression> _expressionsList;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCodeStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicCodeStatement()
    {
        _expressionsList = new List<ILanguageCodeExpression>();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCodeStatement"/> class.
    /// </summary>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> containing the code line to be parsed.
    /// </param>
    public BasicCodeStatement(ITokenizedCodeLine codeLine)
    {
        _expressionsList = new List<ILanguageCodeExpression>();
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
    /// An <see cref="ILanguageKeywordExpression"> instance specifying the command to be invoked.
    /// </value>
    public ILanguageKeywordExpression? CommandExpression => _commandExpression;

    /// <summary>
    /// Gets the reference to the list of expressions that make up the remainder of the statement.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}" /> of <see cref="ILanguageCodeExpression" /> instances containing the remainder
    /// of the expressions in the remaining statement code.
    /// </value>
    public List<ILanguageCodeExpression> Expressions => _expressionsList;

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
}