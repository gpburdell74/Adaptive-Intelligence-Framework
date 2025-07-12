using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.Parser;

/// <summary>
/// Provides the methods and functions for parsing expressions into CodeDOM instances.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public class BlazorBasicExpressionParser : DisposableObjectBase
{
    #region Private Member Declarations
    /// <summary>
    /// The service.
    /// </summary>
    private BlazorBasicLanguageService? _service;

    /// <summary>
    /// The current parsing index value.
    /// </summary>
    private int _parsingIndex = -1;

    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicExpressionParser"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    public BlazorBasicExpressionParser(BlazorBasicLanguageService service)
    {
        _service = service;
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

    #region Public Methods / Functions
    /// <summary>
    /// Parses the list of tokens into the appropriate expression tree and list.
    /// </summary>
    /// <param name="list">
    /// The <see cref="ManagedTokenList"/> instance containing the tokens to be parsed.
    /// </param>
    public List<BasicExpression>? ParseExpression(ManagedTokenList list)
    {
        List<BasicExpression>? subExpressionList = null;
        _parsingIndex = 0;
        if (list.Count > 0)
        {
            subExpressionList = ParseContent(list);
        }

        return subExpressionList;
    }
    #endregion

    #region Private Methods / Functions    
    /// <summary>
    /// Parses the content of the list of tokens into expressions and sub-expressions.
    /// </summary>
    /// <param name="list">
    /// The reference to the <see cref="ManagedTokenList"/> containing the data to process.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="BasicExpression"/> instances.
    /// </returns>
    private List<BasicExpression>? ParseContent(ManagedTokenList list)
    {
        List<BasicExpression>? expressionList = new List<BasicExpression>();
        int length = list.Count;
        bool done = false;

        do
        {
            // Process based on the type of token encountered.
            IToken token = list[_parsingIndex];
            switch (token.TokenType)
            {
                case TokenType.ExpressionStartDelimiter:
                    // e.g. (
                    ProcessExpressionStartDelimiter(expressionList, list, token);
                    break;

                case TokenType.ExpressionEndDelimiter:
                    // e.g. )
                    _parsingIndex++;
                    done = true;
                    break;

                case TokenType.ArithmeticOperator:
                    // e.g. + - * / etc.
                    ProcessArithmeticOperator(expressionList, token.Text!);
                    break;

                case TokenType.VariableName:
                    ProcessVariableName(expressionList, token);
                    break;

                case TokenType.FloatingPoint:
                    ProcessFloatingPointLiteral(expressionList, token);
                    break;

                case TokenType.Integer:
                    ProcessIntegerLiteral(expressionList, token);
                    break;

                default:
                    _parsingIndex++;
                    break;
            }

        } while (_parsingIndex < length && !done);

        return expressionList;
    }

    /// <summary>
    /// Processes the arithmetic operator token.
    /// </summary>
    /// <param name="expressionList">
    /// The <see cref="List{T}"/> of <see cref="BasicExpression"/> to add expressions to.
    /// </param>
    /// <param name="operatorText">
    /// A string containing the operator text.
    /// </param>
    private void ProcessArithmeticOperator(List<BasicExpression> expressionList, string operatorText)
    {
        _parsingIndex++;
        expressionList.Add(
            new BlazorBasicArithmeticOperatorExpression(_service!, operatorText));
    }
    /// <summary>
    /// Processes the expression start delimiter token and related operations for processing sub-expressions.
    /// </summary>
    /// <param name="expressionList">
    /// The <see cref="List{T}"/> of <see cref="BasicExpression"/> to add expressions to.
    /// </param>
    /// <param name="tokenList">
    /// The reference to the original <see cref="ManagedTokenList"/> list.
    /// </param>
    /// <param name="originalToken">
    /// The original <see cref="IToken"/> instance.
    /// </param>
    private void ProcessExpressionStartDelimiter(List<BasicExpression> expressionList, ManagedTokenList tokenList, IToken originalToken)
    {
        _parsingIndex++;

        // Recursively parse the sub-expression.
        List<BasicExpression>? expList = ParseContent(tokenList);
        if (expList != null)
        {

            // Store the results in a compound / complex expression instance.
            BlazorBasicComplexExpression expression = new BlazorBasicComplexExpression(_service!);
            expression.Expressions = expList;

            // Add the new instance to the parent list.
            expressionList.Add(expression);
        }
    }
    /// <summary>
    /// Processes the literal floating point number token.
    /// </summary>
    /// <param name="expressionList">
    /// The <see cref="List{T}"/> of <see cref="BasicExpression"/> to add expressions to.
    /// </param>
    /// <param name="originalToken">
    /// The original <see cref="IToken"/> instance.
    /// </param>
    private void ProcessFloatingPointLiteral(List<BasicExpression> expressionList, IToken originalToken)
    {
        _parsingIndex++;
        expressionList.Add(
            new BlazorBasicLiteralFloatingPointExpression(_service!, new ManagedTokenList { originalToken }));
    }

    /// <summary>
    /// Processes the literal integer token.
    /// </summary>
    /// <param name="expressionList">
    /// The <see cref="List{T}"/> of <see cref="BasicExpression"/> to add expressions to.
    /// </param>
    /// <param name="originalToken">
    /// The original <see cref="IToken"/> instance.
    /// </param>
    private void ProcessIntegerLiteral(List<BasicExpression> expressionList, IToken originalToken)
    {
        _parsingIndex++;
        expressionList.Add(
            new BasicLiteralIntegerExpression(_service!, new ManagedTokenList { originalToken }));
    }

    /// <summary>
    /// Processes the variable name token.
    /// </summary>
    /// <param name="expressionList">
    /// The <see cref="List{T}"/> of <see cref="BasicExpression"/> to add expressions to.
    /// </param>
    /// <param name="originalToken">
    /// The original <see cref="IToken"/> instance.
    /// </param>
    private void ProcessVariableName(List<BasicExpression> expressionList, IToken originalToken)
    {
        _parsingIndex++;
        expressionList.Add(
            new BasicVariableNameExpression(_service!, new ManagedTokenList { originalToken }));
    }
    #endregion
}
