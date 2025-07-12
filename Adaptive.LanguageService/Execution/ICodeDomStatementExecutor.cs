using Adaptive.Intelligence.LanguageService.CodeDom.Statements;

namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for executing code statement represented by 
/// <see cref="ICodeStatement"/> instances.
/// 
/// </summary>
/// <seealso cref="IDisposable" />
public interface ICodeDomStatementExecutor : IDisposable
{
    #region Properties
    /// <summary>
    /// Gets the reference to the execution engine instance.
    /// </summary>
    /// <value>
    /// An <see cref="IExecutionEngine"/> instance.
    /// </value>
    IExecutionEngine? Engine { get; }

    /// <summary>
    /// Gets the reference to the execution environment instance.
    /// </summary>
    /// <value>
    /// An <see cref="IExecutionEnvironment"/> instance.
    /// </value>
    IExecutionEnvironment? Environment { get; }
    #endregion

    #region General Use Methods / Functions
    /// <summary>
    /// Assigns the a value to a variable instance.
    /// </summary>
    /// <param name="scopeContainer">
    /// The <see cref="IScopeContainer"/> container specifying the function or procedure being executed.
    /// </param>
    /// <param name="assignStatement">
    /// A <see cref="ICodeVariableAssignmentStatement"/> instance describing the variable to populate, and 
    /// an expression defining the value to assign to the variable.
    /// </param>
    void AssignValueToVariable(IScopeContainer scopeContainer, ICodeVariableAssignmentStatement assignStatement);

    /// <summary>
    /// Closes the specified file.
    /// </summary>
    /// <param name="scopeContainer">
    /// The <see cref="IScopeContainer"/> container specifying the function or procedure being executed.
    /// </param>
    /// <param name="closeStatement">
    /// The <see cref="ICodeCloseFileStatement"/> statement describing the file to be closed.
    /// </param>
    void CloseFile(IScopeContainer scopeContainer, ICodeCloseFileStatement closeStatement);

    /// <summary>
    /// Creates the parameters as variables inside the scope of the specified function.
    /// </summary>
    /// <remarks>
    /// The parameters provided to the function call are essentially additional variables within
    /// the scope of the function.  This call ensures that the requisite variable instances are 
    /// created.
    /// </remarks>
    /// <param name="scopeContainer">
    /// The <see cref="IScopeContainer"/> container specifying the function being called.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeFunctionStartStatement"/> instance describing the function and its parameter
    /// and return value definitions.
    /// </param>
    void CreateParametersAsVariablesInFunctionScope(
        IScopeContainer scopeContainer,
        ICodeFunctionStartStatement statement);

    /// <summary>
    /// Creates the parameters as variables inside the scope of the specified procedure.
    /// </summary>
    /// <remarks>
    /// The parameters provided to the procedure call are essentially additional variables within
    /// the scope of the procedure.  This call ensures that the requisite variable instances are 
    /// created.
    /// </remarks>
    /// <param name="scopeContainer">
    /// The <see cref="IScopeContainer"/> container specifying the function being called.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeProcedureStartStatement"/> instance describing the procedure and its parameter
    /// definitions.
    /// </param>
    void CreateParametersAsVariablesInProcedureScope(
        IScopeContainer scopeContainer,
        ICodeProcedureStartStatement statement);

    /// <summary>
    /// Declares the variable instance within the specified scope container.
    /// </summary>
    /// <param name="scopeContainer">
    /// The reference to the <see cref="IScopeContainer"/> container instance such as a 
    /// procedure or function.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeVariableDeclarationStatement"/> statement instance containing the variable
    /// definition.</param>
    void DeclareVariableInScope(IScopeContainer scopeContainer, ICodeVariableDeclarationStatement statement);

    /// <summary>
    /// Executes a for loop.
    /// </summary>
    /// <param name="scopeContainer">
    /// The reference to the <see cref="IScopeContainer"/> container instance such as a 
    /// procedure or function.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeForLoopStatement"/> statement instance defining the boundaries of
    /// the for loop.
    /// </param>
    void ExecuteForLoop(IScopeContainer scopeContainer, ICodeForLoopStatement statement);

    /// <summary>
    /// Executes the a function call.
    /// </summary>
    /// <param name="scopeContainer">
    /// The reference to the <see cref="IScopeContainer"/> container instance such as a 
    /// procedure or function.
    /// </param>
    /// <param name="functionCallStatement">
    /// The <see cref="ICodeFunctionCallStatement"/> statement instance defining the function call, its 
    /// parameter values, and expected return type.
    /// </param>
    void ExecuteFunctionCall(IScopeContainer scopeContainer, ICodeFunctionCallStatement functionCallStatement);

    /// <summary>
    /// Executes a statement to optionally prompt and then gather input from a user.
    /// </summary>
    /// <param name="scopeContainer">
    /// The reference to the <see cref="IScopeContainer"/> container instance such as a 
    /// procedure or function.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeUserInputStatement"/> statement instance defining the input prompt (Optional)
    /// and the variable the user input is stored in.
    /// </param>
    void ExecuteGetInputFromUser(IScopeContainer scopeContainer, ICodeUserInputStatement statement);

    /// <summary>
    /// Executes an if condition ... else if .... end code block.
    /// </summary>
    /// <param name="scopeContainer">
    /// The reference to the <see cref="IScopeContainer"/> container instance such as a 
    /// procedure or function.
    /// </param>
    /// <example>
    /// if (condition)
    /// {
    /// }
    /// else if (condition)
    /// {
    /// }
    /// else
    /// {
    /// }
    /// 
    /// OR 
    /// 
    /// IF (condition) THEN
    /// ...
    /// ELSE IF (condition) THEN
    /// ...
    /// END IF
    /// 
    /// </example>
    /// <param name="statement">
    /// The <see cref="ICodeIfStatement"/> statement instance defining the initial comparison 
    /// condition for execution of a code block with an optional condition for execution of a 
    /// second code block. 
    /// </param>
    void ExecuteIfElseBlock(IScopeContainer scopeContainer, ICodeIfStatement statement);

    /// <summary>
    /// Executes a loop where the exit condition is not evaluated until after the first pass through.
    /// </summary>
    /// <param name="scopeContainer">
    /// The reference to the <see cref="IScopeContainer"/> container instance such as a 
    /// procedure or function.
    /// </param>
    /// <example>
    /// do { ... } while (condition);
    /// 
    /// OR
    /// 
    /// DO ... LOOP UNTIL (condition)
    /// 
    /// </example>
    /// <param name="statement">
    /// The <see cref="ICodeLoopStartStatement"/> code statement that defines the start of the loop.
    /// </param>
    void ExecuteLoopWithEndStartCondition(IScopeContainer scopeContainer, ICodeLoopStartStatement statement);

    /// <summary>
    /// Executes a loop that begins with a start condition.
    /// </summary>
    /// <param name="scopeContainer">
    /// The reference to the <see cref="IScopeContainer"/> container instance such as a 
    /// procedure or function.
    /// </param>
    /// <example>
    /// while (condition) { ... }
    /// 
    /// OR
    /// 
    /// WHILE (condition) ... WEND
    /// 
    /// </example>
    /// <param name="statement">
    /// The <see cref="ICodeLoopStartConditionalStatement"/> code statement that defines the start of the loop.
    /// </param>
    void ExecuteLoopWithStartCondition(IScopeContainer scopeContainer, ICodeLoopStartConditionalStatement statement);

    /// <summary>
    /// Executes the a procedure call.
    /// </summary>
    /// <param name="scopeContainer">
    /// The reference to the <see cref="IScopeContainer"/> container instance such as a 
    /// procedure or function.
    /// </param>
    /// <param name="procCallStatement">
    /// The <see cref="ICodeProcedureCallStatement"/> statement instance defining the procedure call, 
    /// and its parameter values.
    /// </param>
    void ExecuteProcedureCall(IScopeContainer scopeContainer, ICodeProcedureCallStatement procCallStatement);

    /// <summary>
    /// Executes the code statement.
    /// </summary>
    /// <param name="scope">
    /// The <see cref="IScopeContainer"/> scope instance inside of which the statement is
    /// being executed.  This is the source of variable and parameter definitions.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeStatement"/> statement to be executed.
    /// </param>
    void ExecuteStatement(
        IScopeContainer scope,
        ICodeStatement statement);

    /// <summary>
    /// Executes the code statement.
    /// </summary>
    /// <param name="engine">
    /// The <see cref="IExecutionEngine"/> engine used to perform specific tasks.
    /// </param>
    /// <param name="environment">
    /// The <see cref="IExecutionEnvironment"/> environment instance containing the global variables,
    /// lists of procedures and functions, and state variables for file system, memory, and other
    /// components.
    /// </param>
    /// <param name="scope">
    /// The <see cref="IScopeContainer"/> scope instance inside of which the statement is
    /// being executed.  This is the source of variable and parameter definitions.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeStatement"/> statement to be executed.
    /// </param>
    void ExecuteStatement(
        IExecutionEngine engine,
        IExecutionEnvironment environment,
        IScopeContainer scope,
        ICodeStatement statement);

    /// <summary>
    /// Attempts to open the specified file.
    /// </summary>
    /// <param name="scopeContainer">
    /// The <see cref="IScopeContainer"/> container specifying the function or procedure being executed.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeOpenFileStatement"/> statement describing the file to be opened and how
    /// to open the file.
    /// </param>
    void OpenFile(IScopeContainer scopeContainer, ICodeOpenFileStatement statement);

    /// <summary>
    /// Attempts to write content to the standard output device/mechanism.
    /// </summary>
    /// <param name="scopeContainer">
    /// The <see cref="IScopeContainer"/> container specifying the function or procedure being executed.
    /// </param>
    /// <param name="writeStatement">
    /// The <see cref="ICodeStandardOutputStatement"/> statement containing the content to be 
    /// written to standard output.
    /// </param>
    void StandardOutput(IScopeContainer scopeContainer, ICodeStandardOutputStatement writeStatement);
    #endregion
}