using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents the declaration of a procedure (PROCEDURE [Name](...) ).
/// </summary>
/// <seealso cref="BasicCodeStatement" />
public class BasicProcedureStartStatement : BasicCodeStatement
{
    #region Private Member Declarations
    /// <summary>
    /// The procedure name.
    /// </summary>
    private string? _procedureName;

    /// <summary>
    /// The parameter expressions list.
    /// </summary>
    private BlazorBasicParameterDefinitionListExpression? _parameterExpressions;

    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedureStartStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicProcedureStartStatement(BlazorBasicLanguageService service) : base(service)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedureStartStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicProcedureStartStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _parameterExpressions?.Dispose();
        _procedureName = null;
        _parameterExpressions = null;
    
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
    public BlazorBasicParameterDefinitionListExpression? Parameters => _parameterExpressions;

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
        // PROCEDURE <name> ([parameters])
        if (codeLine[1].TokenType == TokenType.SeparatorDelimiter)
        {
            // Procedure name should be token 2:
            if (codeLine.Count < 3 || codeLine[2].TokenType != TokenType.ProcedureName)
            {
                throw new Exception("Procedure name is missing");
            }
            else
            {
                // Determine the locations of the parts of the declarations.
                ManagedTokenList managedList = new ManagedTokenList(codeLine.TokenList);
                ManagedTokenList trimmedList = managedList.RemoveSeparators();

                int parameterStartIndex = trimmedList.FindFirstToken(TokenType.ExpressionStartDelimiter);
                int parameterEndIndex = trimmedList.FindLastToken(TokenType.ExpressionEndDelimiter);

                // Expected;
                //      PROCEDURE<FunctionName>([<parameter list>])

                // Determine if the return type is an array.  This will be specified by the last
                // two tokens being: []
                int lastIndex = trimmedList.Count - 1;
                int nextToLastIndex = lastIndex - 1;
                bool isArray = (
                                (trimmedList[nextToLastIndex].TokenType == TokenType.SizingStartDelimiter) &&
                                (trimmedList[lastIndex].TokenType == TokenType.SizingEndDelimiter));

                _procedureName = trimmedList[1].Text;

                // This indicates a not-empty parameter list: e.g. ()
                if (parameterEndIndex != parameterStartIndex + 1)
                {
                    parameterStartIndex = managedList.FindFirstToken(TokenType.ExpressionStartDelimiter);
                    parameterEndIndex = managedList.FindLastToken(TokenType.ExpressionEndDelimiter);

                    _parameterExpressions = new BlazorBasicParameterDefinitionListExpression(
                        Service, managedList, parameterStartIndex, parameterEndIndex);
                }
            }
        }
        else
        {
            throw new Exception("Error in declaring procedure.");
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

        if (_parameterExpressions != null && _parameterExpressions.Count > 0)
        {
            builder.Append(_parameterExpressions.Render());
        }
        builder.Append(DelimiterNames.DelimiterCloseParens);

        return builder.ToString();
    }
    #endregion

}
