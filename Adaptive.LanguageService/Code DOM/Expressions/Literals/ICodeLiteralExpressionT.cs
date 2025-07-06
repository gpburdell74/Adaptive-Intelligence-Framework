using Adaptive.Intelligence.LanguageService.Execution;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that represents a literal value.
/// </summary>
/// <typeparam name="T">
/// The data type of the value being stored in and expressed by the expression.
/// </typeparam>
public interface ICodeLiteralExpression<T> : ICodeExpression
{
    #region Public Properties
    /// <summary>
    /// Gets or sets the literal value.
    /// </summary>
    /// <value>
    /// A <typeparamref name="T"/> containing the literal value being represented by this expression.
    /// </value>
    T? Value { get; set; }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Evaluates the expression during execution.
    /// </summary>
    /// <param name="engine">
    /// The execution engine instance.
    /// </param>
    /// <param name="environment">
    /// The execution environment instance.
    /// </param>
    /// <param name="scope">
    /// The <see cref="IScopeContainer"/> instance, such as a procedure or function, in which scoped
    /// variables are declared.
    /// </param>
    /// <returns>
    /// The result of the expression evaluation.
    /// </returns>
    T? Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope);
    #endregion
}