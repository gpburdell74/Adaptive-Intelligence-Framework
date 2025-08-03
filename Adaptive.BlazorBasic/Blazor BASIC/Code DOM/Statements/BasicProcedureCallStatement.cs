using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Text;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents the invocation of a user-defined procedure.
/// </summary>
/// <seealso cref="BasicCodeStatement" />
public class BasicProcedureCallStatement : BasicCodeStatement
{
    #region Private Member Declarations
    /// <summary>
    /// The procedure name.
    /// </summary>
    private string? _procedureName;

    /// <summary>
    /// The parameter expressions list.
    /// </summary>
    private List<BlazorBasicParameterValueExpression>? _parameterValues;

    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedureStartStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicProcedureCallStatement(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedureStartStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicProcedureCallStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _parameterValues?.Clear();
        _procedureName = null;
        _parameterValues = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets the name of the procedure.
    /// </summary>
    /// <value>
    /// A string containing the name of the procedure.
    /// </value>
    public string ProcedureName => _procedureName;

    /// <summary>
    /// Gets the reference to the expression containing the list of parameters.
    /// </summary>
    /// <value>
    /// A <see cref="BlazorBasicParameterDefinitionListExpression"/> instance containing the list.
    /// </value>
    public List<BlazorBasicParameterValueExpression>? Parameters => _parameterValues;

    /// <summary>
    /// Gets the value of how the current number of tabs being printed is to be modified.
    /// </summary>
    /// <value>
    /// The tab modification.
    /// </value>
    public override RenderTabState TabModification => RenderTabState.NoTabAndIndentAfter;

    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Parses the specified code content.
    /// </summary>
    /// <param name="codeLine">
    /// The <see cref="ITokenizedCodeLine"/> instance to be parsed into its component expressions.
    /// </param>
    protected override void ParseIntoExpressions(ITokenizedCodeLine codeLine)
    {
        // Expected format:
        //
        // <procedureName>[<space>](<[parameter list values])

        if (_parameterValues == null)
            _parameterValues = new List<BlazorBasicParameterValueExpression>();

        // Remove all the separator tokens for simplicity.
        ManagedTokenList managedList = new ManagedTokenList(codeLine.TokenList);
        ManagedTokenList trimmedList = managedList.RemoveSeparators();

        _procedureName = codeLine[0].Text;

        int index = 2;
        int length = trimmedList.Count;
        while (index < length)
        {
            // If no parameters are passed, we are done.
            if (trimmedList[index].TokenType != TokenType.ExpressionEndDelimiter)
            {
                if (trimmedList[index].TokenType == TokenType.StringDelimiter)
                {
                    string data = trimmedList.GetString(index);
                    _parameterValues.Add(
                        new BlazorBasicParameterValueExpression(Service, data));
                    index = trimmedList.FindNextToken(index + 1, TokenType.StringDelimiter);
                }
                                    
                else
                {
                    _parameterValues.Add(
                new BlazorBasicParameterValueExpression(Service,
                    codeLine[index].Text));
                }
            }
            index++;
        }
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

        // PROCEDURE<space><name>([parameter list expression])
        builder.Append(KeywordNames.CommandProcedure);
        builder.Append(ParseConstants.Space);
        builder.Append(_procedureName);
        builder.Append(ParseConstants.Space);
        builder.Append(DelimiterNames.DelimiterOpenParens);

        if (_parameterValues != null && _parameterValues.Count > 0)
        {
            foreach (BlazorBasicParameterValueExpression exp in _parameterValues)
                builder.Append(exp.Render() + ",");
        }
        builder.Append(DelimiterNames.DelimiterCloseParens);

        return builder.ToString();
    }
    #endregion

}
