using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a sub-expression of expressions.
/// </summary>
/// <seealso cref="BlazorBasicExpression" />
/// <seealso cref="ILanguageCodeExpression" />
public class BlazorBasicComplexExpression : BlazorBasicExpression, ILanguageCodeExpression
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicComplexExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    public BlazorBasicComplexExpression(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicComplexExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="dataTypeSpec">
    /// A string containing the data type name.
    /// </param>
    public BlazorBasicComplexExpression(BlazorBasicLanguageService service, string dataTypeSpec) : base(service, dataTypeSpec)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicComplexExpression"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="codeLine">A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.</param>
    /// <param name="startIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.</param>
    /// <param name="endIndex">An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.</param>
    public BlazorBasicComplexExpression(
        BlazorBasicLanguageService service,
        ITokenizedCodeLine codeLine,
        int startIndex,
        int endIndex) : base(service, codeLine, startIndex, endIndex)
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the type of the data.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="BlazorBasicExpression"/> enumerated value indicating the data type.
    /// </value>
    public List<BlazorBasicExpression> Expressions { get; set; } = new List<BlazorBasicExpression>();
    #endregion

    #region Protected Methods    
    /// <summary>
    /// Parses the content expression into a parameter definition.
    /// </summary>
    /// <param name="expression">
    /// A string containing the expression to be parsed.
    /// </param>
    protected override void ParseLiteralContent(string expression)
    {
    }

    /// <summary>
    /// Parses the code line.
    /// </summary>
    /// <param name="codeLine">
    /// A <see cref="ITokenizedCodeLine" /> containing the code tokens for the entire line of code.
    /// </param>
    /// <param name="startIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine" /> to start parsing the expression.
    /// </param>
    /// <param name="endIndex">
    /// An integer indicating the ordinal position in <paramref name="codeLine" /> to end parsing the expression.
    /// </param>
    protected override void ParseCodeLine(ITokenizedCodeLine codeLine, int startIndex, int endIndex)
    {
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
    public object? Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope)
    {
        int index = 0;
        object initEval = Expressions[index].Evaluate<object>(engine, environment, scope);

        double current = Convert.ToDouble(initEval);

        do
        {
            index++;
            BlazorBasicArithmeticOperatorExpression opExpression = (BlazorBasicArithmeticOperatorExpression)Expressions[index];
            index++;
            object abc = Expressions[index].Evaluate<object>(engine, environment, scope);
            double nextValue = Convert.ToDouble(abc);

            switch (opExpression.Operator)
            {
                case BlazorBasicMathOperators.Add:
                    current += nextValue;
                    break;

                case BlazorBasicMathOperators.Subtract:
                    current -= nextValue;
                    break;

                case BlazorBasicMathOperators.Modulus:
                    current %= nextValue;
                    break;

                case BlazorBasicMathOperators.Multiply:
                    current *= nextValue;
                    break;

                case BlazorBasicMathOperators.Divide:
                    current /= nextValue;
                    break;

                case BlazorBasicMathOperators.Exponent:
                    current = (double)((int)current ^ (int)nextValue);
                    break;
            }
        } while (index < Expressions.Count-1);
        


        return current;
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
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("(");
        foreach(BlazorBasicExpression expression in Expressions)
        {
            builder.Append(expression.Render());
        }
        builder.Append(") ");
        return builder.ToString();
    }
    #endregion
}
