using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.Execution;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;

/// <summary>
/// Represents a literal value expression.
/// </summary>
/// <seealso cref="BasicExpression" />
/// <seealso cref="ICodeExpression" />
/// <typeparam name="T">
/// The data type of the literal value.
/// </typeparam>
public abstract class BasicLiteralExpression<T> : BasicExpression, ICodeLiteralExpression<T>
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicLiteralExpression{T}"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    public BasicLiteralExpression(BlazorBasicLanguageService service) : base(service)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicLiteralExpression{T}"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the language service instance being injected.
    /// </param>
    /// <param name="literalValue">
    /// The literal value being represented.
    /// </param>
    public BasicLiteralExpression(BlazorBasicLanguageService service, T literalValue) : base(service)
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the literal value.
    /// </summary>
    /// <value>
    /// A <typeparamref name="T"/> containing the literal value being represented by this expression.
    /// </value>
    public T? Value { get; set; } = default;
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Evaluates the expression during execution.
    /// </summary>
    /// <param name="engine">The execution engine instance.</param>
    /// <param name="environment">The execution environment instance.</param>
    /// <param name="scope">The <see cref="T:Adaptive.Intelligence.LanguageService.Execution.IScopeContainer" /> instance, such as a procedure or function, in which scoped
    /// variables are declared.</param>
    /// <returns>
    /// The result of the expression evaluation.
    /// </returns>
    T? ICodeLiteralExpression<T>.Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope)
    {
        return Value;
    }
    #endregion

}
