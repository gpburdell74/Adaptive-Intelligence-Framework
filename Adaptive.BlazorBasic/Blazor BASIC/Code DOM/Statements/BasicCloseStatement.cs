using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a CLOSE statement.
/// </summary>
/// <example>
/// 
///     CLOSE #42
///     
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicCloseStatement : BasicCodeStatement
{
    #region Private Member Declarations    
    /// <summary>
    /// The expression for the file number/handle.
    /// </summary>
    private BasicFileNumberExpression? _expression;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCloseStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicCloseStatement(BlazorBasicLanguageService service) : base(service)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicCloseStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicCloseStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _expression?.Dispose();
        _expression = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets the reference to the file number expression.
    /// </summary>
    /// <value>
    /// A <see cref="BasicFileNumberExpression"/> instance specifying the file number/handle.
    /// </value>
    public BasicFileNumberExpression FileNumberExpression => _expression;

    /// <summary>
    /// Gets the value of how the current number of tabs being printed is to be modified.
    /// </summary>
    /// <value>
    /// The tab modification.
    /// </value>
    public override RenderTabState TabModification => RenderTabState.None;
    #endregion

    #region Protected Method Overrides
    /// <summary>
    /// Parses the specified code content.
    /// </summary>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> containing the code tokens to parse.
    /// </param>
    protected override void ParseIntoExpressions(ITokenizedCodeLine codeLine)
    {
        LineNumber = codeLine.LineNumber;
        _expression = new BasicFileNumberExpression(Service, codeLine[2].Text);

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
        StringBuilder builder = new StringBuilder();
        builder.Append(KeywordNames.CommandClose);
        builder.Append(ParseConstants.Space);
        builder.Append(ParseConstants.NumberSign);
        builder.Append(_expression.Render());

        return builder.ToString();

    }
    #endregion
}
