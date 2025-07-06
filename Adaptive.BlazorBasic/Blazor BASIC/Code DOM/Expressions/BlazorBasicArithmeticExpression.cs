using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a math expression.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ICodeExpression" />
public class BlazorBasicBasicArithmeticExpression : BasicExpression, ICodeExpression
{
    #region Private Member Declarations    
    /// <summary>
    /// The left expression.
    /// </summary>
    private BasicExpression? _leftExpression;
    /// <summary>
    /// The operator.
    /// </summary>
    private BlazorBasicMathOperators _operator = BlazorBasicMathOperators.Add;
    /// <summary>
    /// The right expression.
    /// </summary>
    private BasicExpression? _rightExpression;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicBasicArithmeticExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> instance.
    /// </param>
    public BlazorBasicBasicArithmeticExpression(BlazorBasicLanguageService service) : base(service)
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicBasicArithmeticExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="BlazorBasicLanguageService"/> instance.
    /// </param>
    /// <param name="leftExpression">
    /// A <see cref="BasicExpression"/> containing the left side of the operation.
    /// </param>
    /// <param name="operatorIndicator">
    /// A <see cref="BlazorBasicMathOperators"/> enumerated value indicating the operator.
    /// </param>
    /// <param name="rightExpression">
    /// A <see cref = "BasicExpression" /> containing the right side of the operation.
    /// </param>
    public BlazorBasicBasicArithmeticExpression(
        BlazorBasicLanguageService service, 
        BasicExpression leftExpression,
        BlazorBasicMathOperators operatorIndicator,
        BasicExpression rightExpression)
         : base(service)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
        _operator = operatorIndicator;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _leftExpression = null;
        _rightExpression = null;
        base.Dispose(disposing);
    }

    protected override void ParseLiteralContent(string? expression)
    {
    }
    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
    
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the reference to the left-side expression to evaluate.
    /// </summary>
    /// <value>
    /// A <see cref="BasicExpression"/> instance representing the left side of the operation.
    /// </value>
    public BasicExpression? LeftExpression => _leftExpression;
    /// <summary>
    /// Gets the operator.
    /// </summary>
    /// <value>
    /// A <see cref="BlazorBasicMathOperators"/> enumerated value indicating the operator to use.
    /// </value>
    public BlazorBasicMathOperators Operator => _operator;
    /// <summary>
    /// Gets the reference to the right-side expression to evaluate.
    /// </summary>
    /// <value>
    /// A <see cref="BasicExpression"/> instance representing the right side of the operation.
    /// </value>
    public BasicExpression? RightExpression => _rightExpression;
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
    public override object? Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope) 
    {
        double a = 0;
        if (LeftExpression != null)
        {
            object leftResult = LeftExpression.Evaluate(engine, environment, scope);
            a = Convert.ToDouble(leftResult);
        }
        double b = 0;
        if (RightExpression != null)
        {
            object rightResult = RightExpression.Evaluate(engine, environment, scope);
            b = Convert.ToDouble(rightResult);
        }

        return PerformOp(a, b);
    }

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

        builder.Append(_leftExpression.Render());
        builder.Append(ParseConstants.Space);
        builder.Append(BlazorBasicOperatorFactory.GetOperatorText(_operator));
        builder.Append(ParseConstants.Space);
        builder.Append(_rightExpression.Render());

        return builder.ToString();
    }
    #endregion

    private double PerformOp(double a, double b)
    {
        double result = 0;

        switch(Operator)
        {
            case BlazorBasicMathOperators.Add:
                result = a + b;
                break;

            case BlazorBasicMathOperators.Subtract:
                result = a - b;
                break;

            case BlazorBasicMathOperators.Multiply:
                result = a * b;
                break;

            case BlazorBasicMathOperators.Divide:
                result = a / b;
                break;

            case BlazorBasicMathOperators.Exponent:
                result =(int)a ^(int)b;
                break;

            case BlazorBasicMathOperators.Modulus:
                result = a % b;
                break;

        }
        return result;
    }
}
