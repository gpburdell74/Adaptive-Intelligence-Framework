using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents the declaration of a procedure (PROCEDURE [Name](...) ).
/// </summary>
/// <seealso cref="BasicCodeStatement" />
public class BasicFunctionStartStatement : BasicCodeStatement
{
    #region Private Member Declarations
    /// <summary>
    /// The function name.
    /// </summary>
    private string? _functionName;

    /// <summary>
    /// The parameter expressions list.
    /// </summary>
    private BlazorBasicParameterDefinitionListExpression? _parameterExpressions;

    /// <summary>
    /// The return type expression.
    /// </summary>
    private BlazorBasicDataTypeExpression? _returnExpression;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFunctionStartStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicFunctionStartStatement(BlazorBasicLanguageService service) : base(service)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFunctionStartStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicFunctionStartStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        //_parameterExpressions?.Dispose();

        _functionName = null;
        _parameterExpressions = null;
        _returnExpression = null;
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
    public string? FunctionName => _functionName;

    /// <summary>
    /// Gets the reference to the expression containing the list of parameters.
    /// </summary>
    /// <value>
    /// A <see cref="BlazorBasicParameterDefinitionListExpression"/> instance containing the list.
    /// </value>
    public BlazorBasicParameterDefinitionListExpression? Parameters => _parameterExpressions;

    /// <summary>
    /// Gets the reference to the return expression.
    /// </summary>
    /// <value>
    /// A <see cref="BlazorBasicDataTypeExpression"/> expression specifying the return data type for the function.
    /// </value>
    public BlazorBasicDataTypeExpression? ReturnExpression => _returnExpression;

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
        // FUNCTION <name> ([parameters]) AS [dataType]
        if (codeLine[1].TokenType == TokenType.SeparatorDelimiter)
        {
            // Function name should be token 2:
            if (codeLine.Count < 3 || codeLine[2].TokenType != TokenType.FunctionName)
            {
                throw new Exception("Function name is missing");
            }
            else
            {
                // Determine the locations of the parts of the declarations.
                ManagedTokenList managedList = new ManagedTokenList(codeLine.TokenList);
                ManagedTokenList trimmedList = managedList.RemoveSeparators();

                int parameterStartIndex = trimmedList.FindFirstToken(TokenType.ExpressionStartDelimiter);
                int parameterEndIndex = trimmedList.FindLastToken(TokenType.ExpressionEndDelimiter);
                int dataTypeStartIndex = parameterEndIndex + 2;

                // Expected;
                //      FUNCTION<FunctionName>([<parameter list>])AS<dataTypeName> or 
                //      FUNCTION<FunctionName>([<parameter list>])AS<dataTypeName>[]

                // Determine if the return type is an array.  This will be specified by the last
                // two tokens being: []
                int lastIndex = trimmedList.Count - 1;
                int nextToLastIndex = lastIndex - 1;
                bool isArray = (
                                (trimmedList[nextToLastIndex].TokenType == TokenType.SizingStartDelimiter) &&
                                (trimmedList[lastIndex].TokenType == TokenType.SizingEndDelimiter));

                _functionName = trimmedList[1].Text;

                // This indicates a not-empty parameter list: e.g. ()
                if (parameterEndIndex != parameterStartIndex + 1)
                {
                    parameterStartIndex = managedList.FindFirstToken(TokenType.ExpressionStartDelimiter);
                    parameterEndIndex = managedList.FindLastToken(TokenType.ExpressionEndDelimiter);

                    _parameterExpressions = new BlazorBasicParameterDefinitionListExpression(
                        Service, managedList, parameterStartIndex, parameterEndIndex);
                }

                // Return type.
                string dataTypeName = trimmedList[dataTypeStartIndex].Text;
                _returnExpression = new BlazorBasicDataTypeExpression(
                    Service,
                    dataTypeName,
                    isArray);
            }
        }
        else
        {
            throw new Exception("Error in declaring procedure");
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

        // FUNCTION<space><name>([parameter list expression])<space>AS<space><datatypename>
        builder.Append(KeywordNames.CommandFunction);
        builder.Append(ParseConstants.Space);
        builder.Append(_functionName);
        builder.Append(ParseConstants.Space);
        builder.Append(DelimiterNames.DelimiterOpenParens);
        if (_parameterExpressions != null && _parameterExpressions.Count > 0)
        {
            builder.Append(_parameterExpressions.Render());
        }

        builder.Append(DelimiterNames.DelimiterCloseParens);
        builder.Append(ParseConstants.Space);
        builder.Append(KeywordNames.KeywordAs);
        builder.Append(ParseConstants.Space);
        builder.Append(_returnExpression.Render());

        return builder.ToString();
    }
    #endregion

    private List<BasicParameterDefinitionExpression> SplitIntoParameters(ManagedTokenList tokenList)
    {
        int startIndex = tokenList.FindFirstToken(TokenType.ExpressionStartDelimiter);
        int endIndex = tokenList.FindLastToken(TokenType.ExpressionEndDelimiter);

        ManagedTokenList tokenSubList = tokenList.CreateCopy(startIndex, endIndex).RemoveSeparators();

        List<BasicParameterDefinitionExpression> parameterList = new List<BasicParameterDefinitionExpression>();

        int pos = 0;
        do
        {
            bool isArray = false;
            IToken nameToken = tokenSubList[pos];
            IToken dataTypeToken;
            pos++;

            if (tokenSubList[pos].TokenType == TokenType.SizingStartDelimiter &&
                tokenSubList[pos + 1].TokenType == TokenType.SizingStartDelimiter)
            {
                isArray = true;
                pos += 2;

            }
            dataTypeToken = tokenSubList[pos];
            BasicParameterDefinitionExpression expression =
                new BasicParameterDefinitionExpression(Service, nameToken, dataTypeToken, isArray);
            pos++;

        } while (pos < tokenSubList.Count);

        return parameterList;
    }
}
