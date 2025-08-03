using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents an assignment to a variable.
/// </summary>
/// <example>
/// Expected Formats:
/// 
///     LET A = 1
///     B = 3
///     C$ = "X"
///     myVar = 123.3
///     myStr = "Hello World"
///     
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicVariableAssignmentStatement : BasicCodeStatement, ICodeVariableAssignmentStatement
{
    #region Private Member Declarations    
    /// <summary>
    /// The variable reference.
    /// </summary>
    private BasicVariableReferenceExpression? _variable;
    /// <summary>
    /// The expression containing the value to assign to the variable.
    /// </summary>
    private BasicExpression? _expression;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariableAssignmentStatement"/> class.
    /// </summary>
    /// <param name="service">The reference to the <see cref="BlazorBasicLanguageService" /> to use to find and compare
    /// text to language reserved words and operators and other items.</param>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicVariableAssignmentStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {
        CommandExpression = new BlazorBasicReservedWordExpression(service, KeywordNames.CommandLet);
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _variable?.Dispose();
            _expression?.Dispose();
        }

        _variable = null;
        _expression = null;
        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties 
    /// <summary>
    /// Gets the reference to the expression defining the value to be assigned.
    /// </summary>
    /// <value>
    /// A <see cref="BasicExpression"/> instance representing the value.
    /// </value>
    public BasicExpression? Expression => _expression;
    ICodeExpression? ICodeVariableAssignmentStatement.Expression => _expression;

    /// <summary>
    /// Gets the reference to the expression providing the variable name.
    /// </summary>
    /// <value>
    /// A <see cref="BasicVariableReferenceExpression"/> defining the variable name/reference.
    /// </value>
    public BasicVariableReferenceExpression? VariableReference => _variable;
    ICodeVariableReferenceExpression? ICodeVariableAssignmentStatement.VariableReference => _variable;

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
    /// <param name="codeLine">A string containing the code to be parsed.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    protected override void ParseIntoExpressions(ITokenizedCodeLine codeLine)
    {
        int index = 0;
        if (codeLine[0].TokenType == TokenType.ReservedWord && codeLine[0].Text == KeywordNames.CommandLet)
            index = 2;

        // Find the variable.
        IToken? variableToken = codeLine[index];
        _variable = new BasicVariableReferenceExpression(Service, variableToken.Text);

        // Skip the separators and = sign.
        do
        {
            index++;
        } while (index < codeLine.Count &&
                    ((codeLine[index].TokenType == TokenType.SeparatorDelimiter) ||
                     (codeLine[index].TokenType == TokenType.AssignmentOperator)));

        _expression = BlazorBasicExpressionFactory.CreateFromTokens(Service, codeLine.LineNumber, 
            new ManagedTokenList(           codeLine.TokenList), index);
        Expressions.Add(_variable);
        Expressions.Add(_expression);
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
        StringBuilder builder = new StringBuilder();
        if (_variable != null)
        {
            builder.Append(_variable.Render());
            builder.Append(ParseConstants.Space);
        }
        builder.Append(ParseConstants.Equals);
        builder.Append(ParseConstants.Space);
        if (_expression != null)
        {
            builder.Append(_expression.Render());
        }
        return builder.ToString();
    }
    /// <summary>
    /// Converts to string.
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
