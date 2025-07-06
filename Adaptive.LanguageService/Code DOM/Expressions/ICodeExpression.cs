using Adaptive.Intelligence.LanguageService.Execution;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for instances that represent generic code expressions.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ICodeExpression : ICodeObject
{
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
    object Evaluate(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope);

    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into a string containing the code content.
    /// </returns>
    string? Render();
}
