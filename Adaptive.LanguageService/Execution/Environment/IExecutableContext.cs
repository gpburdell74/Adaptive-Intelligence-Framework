using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for executable language elements and
/// constructs such as functions, procedures, and other code blocks.
/// </summary>
/// <seealso cref="IScopeContainer" />
public interface IExecutableContext : IScopeContainer 
{
    /// <summary>
    /// Gets the reference to the list of procedure parameters.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="ICodeExpression"/> instances defining the 
    /// parameters for the procedure.
    /// </value>
    IParameterTable? Parameters { get; }

    /// <summary>
    /// Gets the name of the current instance.
    /// </summary>
    /// <value>
    /// A string containing the name identifying the instance.
    /// </value>
    string? Name { get; }

    /// <summary>
    /// Gets the reference to the list of variables defined within the procedure.
    /// </summary>
    /// <value>
    /// An <see cref="IVariableTable"/> implementation.
    /// </value>
    IVariableTable? Variables { get; }

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
    void Execute(int lineNumber, IExecutionEngine engine, IExecutionEnvironment environment);

    /// <summary>
    /// Instantiates the variable within the local scope.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="dataType">Type of the data.</param>
    /// <param name="isArray">if set to <c>true</c> [is array].</param>
    /// <param name="size">The size.</param>
    void InstantiateVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size);

    /// <summary>
    /// Instantiates a new variable within the local scope to represent a 
    /// parameter and its value.
    /// </summary>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="dataType">Type of the data.</param>
    /// <param name="isArray">if set to <c>true</c> [is array].</param>
    /// <param name="size">The size.</param>
    void InstantiateParameterVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size);

}
