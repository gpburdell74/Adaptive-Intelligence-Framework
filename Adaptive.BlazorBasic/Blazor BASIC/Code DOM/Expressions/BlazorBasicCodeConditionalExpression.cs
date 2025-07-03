using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a conditional expression in Blazor BASIC.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BlazorBasicCodeConditionalExpression : BlazorBasicExpression, ILanguageCodeExpression
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BlazorBasicCodeConditionalExpression(BlazorBasicLanguageService service) : base(service)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    public BlazorBasicCodeConditionalExpression(BlazorBasicLanguageService service, string expression) : base(service)
    {
        ParseContent(expression);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicCodeConditionalExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance.
    /// </param>
    /// <param name="codeLine">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances from the parent code line instance 
    /// containing the data to be parsed.
    /// </param>
    public BlazorBasicCodeConditionalExpression(BlazorBasicLanguageService service, List<IToken> codeLine) : base(service)
    {
        ParseCodeLine(codeLine);
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
            LeftExpression?.Dispose();
            RightExpression?.Dispose();
        }

        LeftExpression = null;
        RightExpression = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the reference to the left expression.
    /// </summary>
    /// <value>
    /// An <see cref="ILanguageCodeExpression"/> to be evaluated as the left-side of the conditional.
    /// </value>
    public ILanguageCodeExpression? LeftExpression { get; set; }

    /// <summary>
    /// Gets or sets the comparison operator.
    /// </summary>
    /// <value>
    /// A <see cref="StandardOperators"/> enumerated value indicating the operator to use.
    /// </value>
    public StandardOperators Operator { get; set; } = StandardOperators.NoneOrUnknown;

    /// <summary>
    /// Gets or sets the reference to the right expression.
    /// </summary>
    /// <value>
    /// An <see cref="ILanguageCodeExpression"/> to be evaluated as the right-side of the conditional.
    /// </value>
    public ILanguageCodeExpression? RightExpression { get; set; }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    /// <exception cref="Adaptive.Intelligence.Shared.ExceptionEventArgs.Exception"></exception>
    private void ParseContent(string expression)
    {
        expression = expression.Trim();

        // Expected (abstract) format:
        // <left-side expression>[space]<operator>[space]<right-side expression>
    }
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">A string containing the expression to be parsed.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    protected override void ParseLiteralContent(string? expression)
    {
    }
    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">A <see cref="List{T}" /> of <see cref="IToken" /> instances containing the expression to be parsed.</param>
    protected override void ParseCodeLine(List<IToken> codeLine)
    {
        int lineNumber = -1;

        int last = codeLine.Count - 1;
        int startIndex = FindTrueStart(codeLine);
        int endIndex = FindTrueEnd(codeLine);

        IToken? startToken = codeLine[startIndex];
        IToken? endToken = codeLine[endIndex];

        // Left side expressions.
        ManagedTokenList leftSide = new ManagedTokenList();
        int index = startIndex;
        bool done = false;
        do
        {
            IToken current = codeLine[index];
            if (current.TokenType != TokenType.SeparatorDelimiter &&
                current.TokenType != TokenType.ExpressionStartDelimiter)
                leftSide.Add(current);
            else
                done = true;
            index++;
        } while (!done && index <= last);


        // Operator expressions.
        List<IToken> operatorList = new List<IToken>();
        done = false;
        do
        {
            IToken current = codeLine[index];
            if (current.TokenType != TokenType.SeparatorDelimiter &&
                current.TokenType != TokenType.ExpressionStartDelimiter)
                operatorList.Add(current);
            else
                done = true;
            index++;
        } while (!done && index <= last);

        // Right-side expressions.
        ManagedTokenList rightSide = new ManagedTokenList();
        done = false;
        do
        {
            IToken current = codeLine[index];
            if (current.TokenType != TokenType.SeparatorDelimiter &&
                current.TokenType != TokenType.ExpressionStartDelimiter)
                rightSide.Add(current);
            else
                done = true;
            index++;
        } while (!done && index <= last);

        LeftExpression = BlazorBasicExpressionFactory.CreateFromTokens(Service, lineNumber, leftSide);
        Operator = Service.Operators.GetOperator(operatorList[0].Text);
        RightExpression = BlazorBasicExpressionFactory.CreateFromTokens(Service, lineNumber, rightSide);
    }
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(LeftExpression.Render());
        builder.Append(DelimiterNames.DelimiterSpace);
        builder.Append(Service.Operators.GetOperatorText(Operator));
        builder.Append(DelimiterNames.DelimiterSpace);
        builder.Append(RightExpression.Render());

        return builder.ToString();
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Finds the true start of the conditional expression in the provided items list.
    /// </summary>
    /// <param name="codeLine">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances to be examined.
    /// </param>
    /// <returns>
    /// An integer indicating the ordinal index of the first token that is not a start delimiter or separator delimiter.
    /// </returns>
    private int FindTrueStart(List<IToken> codeLine)
    {
        int last = codeLine.Count - 1;
        int startIndex = -1;

        // Find the actual start of the expression.
        int index = 0;
        do
        {
            IToken token = codeLine[index];
            if (token.TokenType != TokenType.ExpressionStartDelimiter &&
                token.TokenType != TokenType.SeparatorDelimiter)
                startIndex = index;
            index++;
        } while (index < last && startIndex == -1);
        return startIndex;
    }

    /// <summary>
    /// Finds the true end of the conditional expression in the provided items list.
    /// </summary>
    /// <param name="codeLine">
    /// The <see cref="List{T}"/> of <see cref="IToken"/> instances to be examined.
    /// </param>
    /// <returns>
    /// An integer indicating the ordinal index of the last token that is not a start delimiter or separator delimiter.
    /// </returns>
    private int FindTrueEnd(List<IToken> codeLine)
    {
        int last = codeLine.Count - 1;
        int endIndex = -1;

        // Find the end o of the expression.
        int index = last;
        do
        {
            IToken token = codeLine[index];
            if (token.TokenType != TokenType.ExpressionStartDelimiter &&
                token.TokenType != TokenType.SeparatorDelimiter)
                endIndex = index;
            index--;
        } while (index < last && endIndex == -1);

        return endIndex;
    }
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
    public bool? Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope)
    {
        bool evalResult = false;

        object leftResult = LeftExpression.Evaluate<object>(engine, environment, scope);
        object rightResult = RightExpression.Evaluate<object>(engine, environment, scope);

        switch(Operator)
        {
            case StandardOperators.AssignmentEquals:
            case StandardOperators.ComparisonEqualTo:
                evalResult = DynamicTypeComparer.EqualTo(leftResult, rightResult);
                break;

            case StandardOperators.ComparisonGreaterThan:
                evalResult = DynamicTypeComparer.GreaterThan(leftResult, rightResult);
                break;

            case StandardOperators.ComparisonGreaterThanOrEqualTo:
                evalResult = DynamicTypeComparer.GreaterThanOrEqualTo(leftResult, rightResult);
                break;

            case StandardOperators.ComparisonLessThan:
                evalResult = DynamicTypeComparer.LessThan(leftResult, rightResult);
                break;

            case StandardOperators.ComparisonLessThanOrEqualTo:
                evalResult = DynamicTypeComparer.LessThanOrEqualTo(leftResult, rightResult);
                break;

            case StandardOperators.ComparisonNotEqualTo:
                evalResult = DynamicTypeComparer.NotEqualTo(leftResult, rightResult);
                break;
        }
        return evalResult;
    }

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
        return (T?)(object?)Evaluate(engine, environment, scope);
    }
    #endregion

}