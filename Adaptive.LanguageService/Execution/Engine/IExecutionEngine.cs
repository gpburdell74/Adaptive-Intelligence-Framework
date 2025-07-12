using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for executing language-specific operations.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IExecutionEngine : IDisposable 
{
    /// <summary>
    /// Gets the reference to the statement executor.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeDomStatementExecutor"/> instance containing the implementation
    /// functions for executing code statements.
    /// </value>
    public ICodeDomStatementExecutor Executor { get; }


    /// <summary>
    /// Evaluates the expression based on the current execution context and state.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="scope">
    /// The <see cref="IScopeContainer"/> instance in which the expression is to be evaluated.
    /// </param>
    /// <param name="expression">
    /// The <see cref="ICodeExpression"/> instance to evaluate.
    /// </param>
    /// <returns>
    /// The result of the expression evaluation, or <b>null</b> if there is no result.
    /// </returns>
    object? EvaluateExpression(int lineNumber, IScopeContainer scope, ICodeExpression expression);

    /// <summary>
    /// Calls the function.
    /// </summary>
    /// <typeparam name="T">
    /// The data type of the function's return type.
    /// </typeparam>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="scope">
    /// The reference to the parent <see cref="IScopeContainer"/> instance.
    /// </param>
    /// <param name="functionName">
    /// A string containing the name of the function.
    /// </param>
    /// <param name="parameterValues">
    /// A <see cref="List{T}"/> of parameter values to supply to the function.
    /// </param>
    /// <returns>
    /// The data result from executing the function.
    /// </returns>
    T? CallFunction<T>(int lineNumber, IScopeContainer scope, string functionName, List<object> parameterValues);

    /// <summary>
    /// Calls the procedure.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="scope">
    /// The reference to the parent <see cref="IScopeContainer"/> instance.
    /// </param>
    /// <param name="procedureName">
    /// A string containing the name of the procedure.
    /// </param>
    /// <param name="parameterValues">
    /// A <see cref="List{T}"/> of parameter values to supply to the function.
    /// </param>
    void CallProcedure(int lineNumber, IScopeContainer scope, string procedureName, List<object> parameterValues);
}
