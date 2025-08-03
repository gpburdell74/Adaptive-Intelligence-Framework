namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition to represent a function definition and its code.
/// </summary>
/// <seealso cref="IScopeContainer" />
public interface IFunction : IScopedElement, IScopeContainer, IExecutableContext
{
    /// <summary>
    /// Gets the data type of the return value for the function instance.
    /// </summary>
    /// <value>
    /// The data <see cref="Type"/> of the expected return value.
    /// </value>
    Type ReturnType { get; }

    /// <summary>
    /// Executes the code element within the context of its parent scope.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number value.
    /// </param>
    /// <param name="engine">
    /// The current <see cref="IExecutionEngine"/> instance.
    /// </param>
    /// <param name="environment">
    /// The current <see cref="IExecutionEnvironment"/> instance.
    /// </param>
    new object Execute(int lineNumber, IExecutionEngine engine, IExecutionEnvironment environment);
}
