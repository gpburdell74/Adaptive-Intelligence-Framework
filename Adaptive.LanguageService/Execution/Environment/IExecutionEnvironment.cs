using Adaptive.Intelligence.LanguageService.CodeDom;

namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for the global environment container in which the 
/// language code is executed.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IExecutionEnvironment : IDisposable
{
    #region Properties
    /// <summary>
    /// Gets the reference to the table containing the defined function instances.
    /// </summary>
    /// <value>
    /// An <see cref="IFunctionTable"/> instance containing the function instances.
    /// </value>
    IFunctionTable? Functions { get; }

    /// <summary>
    /// Gets the reference to the list of global varaibles.
    /// </summary>
    /// <value>
    /// An <see cref="IVariableTable"/> instance containing the global variables.
    /// </value>
    IVariableTable? GlobalVaraibles { get; }

    /// <summary>
    /// Gets the reference to the main procedure.
    /// </summary>
    /// <value>
    /// The <see cref="IProcedure"/> instance representing the main procedure.
    /// </value>
    IProcedure? MainProcedure { get; }

    /// <summary>
    /// Gets the reference to the table containing the defined procedure instances.
    /// </summary>
    /// <value>
    /// An <see cref="IProcedureTable"/> instance containing the procedure instances.
    /// </value>
    IProcedureTable? Procedures { get; }

    /// <summary>
    /// Gets the referemce to the standard output device or mechanism.
    /// </summary>
    /// <value>
    /// The <see cref="IStandardOutput"/> instance used for output operations.
    /// </value>
    IStandardOutput? StandardOutput { get; }

    /// <summary>
    /// Gets the reference to the system API implementation.
    /// </summary>
    /// <value>
    /// The <see cref="ISystem"/> instance providing access to system-level operations."/>
    /// </value>
    ISystem System { get; }
    #endregion

    #region Methods    
    /// <summary>
    /// Invokes and executes the specified function.
    /// </summary>
    /// <typeparam name="T">
    /// The data type of the return value from the function.
    /// </typeparam>
    /// <param name="currentLineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="scope">
    /// The reference to the parent <see cref="IScopeContainer"/> instance.
    /// </param>
    /// <param name="functionName">
    /// A string contianing the name of the function to call.
    /// </param>
    /// <param name="parameterValues">
    /// A <see cref="List{T}"/> of values to populate the function parameters with.
    /// </param>
    /// <returns>
    /// The return value from the function, or null if the function does not return a value.
    /// </returns>
    T? CallFunction<T>(int currentLineNumber, IScopeContainer? scope, string functionName, List<object> parameterValues);

    /// <summary>
    /// Invokes and executes the specified procedure.
    /// </summary>
    /// <param name="currentLineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="scope">
    /// The reference to the parent <see cref="IScopeContainer"/> instance.
    /// </param>
    /// <param name="procedureName">
    /// A string contianing the name of the procedure to call.
    /// </param>
    /// <param name="parameterValues">
    /// A <see cref="List{T}"/> of values to populate the function parameters with.
    /// </param>
    void CallProcedure(int currentLineNumber, IScopeContainer? scope, string procedureName, List<object> parameterValues);

    /// <summary>
    /// Executes the specified interpreter unit.
    /// </summary>
    /// <param name="interpreterUnit">
    /// The <see cref="IInterpreterUnit"/> instance to be executed.
    /// </param>
    void Execute(IInterpreterUnit interpreterUnit);

    /// <summary>
    /// Loads the source into memory as an interpreter unit and prepares the environment for execution.
    /// </summary>
    /// <param name="interpreterUnit">
    /// The <see cref="IInterpreterUnit"/> instance containing the loaded source code.
    /// </param>
    void LoadUnit(IInterpreterUnit interpreterUnit);
    #endregion
}
