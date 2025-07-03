using Adaptive.Intelligence.LanguageService.Execution;

namespace Adaptive.Intelligence.LanguageService.CodeDom;

/// <summary>
/// Provides the signature definition for instances that represent generic code expressions.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ILanguageCodeExpression : ILanguageCodeObject
{
    /// <summary>
    /// Evaluates the expression.
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
    /// <typeparam name="T">
    /// The data type of the result produced by the evaluation of the expression.
    /// </typeparam>
    /// <returns>
    /// The result of the object evaluation.
    /// </returns>
    T? Evaluate<T>(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope);

    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    string? Render();
}
