using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;
using System.CodeDom.Compiler;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

/// <summary>
/// Provides the Blazor BASIC interpreter's execution engine implementation.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IExecutionEngine" />
public sealed class BlazorBasicExecutionEngine : DisposableObjectBase, IExecutionEngine
{
    #region Private Member Declarations
    /// <summary>
    /// The execution environment instance that maintains state values and provides access
    /// to the underlying system.
    /// </summary>
    private IExecutionEnvironment? _environment;

    /// <summary>
    /// The execution manager for Blazor BASIC statements.
    /// </summary>
    private BasicStatementExecutor _executor;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicExecutionEngine"/> class.
    /// </summary>
    /// <param name="environment">
    /// The reference to the <see cref="IExecutionEnvironment"/> environment instance.
    /// </param>
    public BlazorBasicExecutionEngine(IExecutionEnvironment environment)
    {
        _environment = environment;
        _executor = new BasicStatementExecutor(this, environment);
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _executor?.Dispose();
        }

        _executor = null;
        _environment = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the reference to the statement executor.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeDomStatementExecutor"/> instance containing the implementation
    /// functions for executing code statements.
    /// </value>
    public ICodeDomStatementExecutor Executor => _executor;
    #endregion

    public int CurrentStatementIndex => _executor.StatementIndex;

    #region Public Methods / Functions
    /// <summary>
    /// Calls the specified function and returns the calculated value.
    /// </summary>
    /// <typeparam name="T">
    /// The data type of the function's return type.
    /// </typeparam>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.</param>
    /// <param name="scope">
    /// The reference to the parent <see cref="T:Adaptive.Intelligence.LanguageService.Execution.IScopeContainer" /> instance.</param>
    /// <param name="functionName">
    /// A string containing the name of the function.
    /// </param>
    /// <param name="parameterValues">
    /// A <see cref="List{T}" /> of parameter values to supply to the function.</param>
    /// <returns>
    /// The data result from executing the function.
    /// </returns>
    public T? CallFunction<T>(int lineNumber, IScopeContainer scope, string functionName, List<object> parameterValues)
    {
        if (_environment == null)
            throw new BasicEngineExecutionException(lineNumber, "The execution environment is not initialized.");

        IFunction? function = _environment.Functions!.GetFunction(functionName);
        if (function == null)
        {
            throw new BasicIllegalFunctionCallException(lineNumber, "No such function defined.");
        }

        return (T)function.Execute(lineNumber, this, _environment);
    }

    /// <summary>
    /// Calls the specified procedure.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="scope">
    /// The reference to the parent <see cref="IScopeContainer" /> instance.
    /// </param>
    /// <param name="procedureName">
    /// A string containing the name of the procedure.
    /// </param>
    /// <param name="parameterValues">
    /// A <see cref="List{T}" /> of parameter values to supply to the function.
    /// </param>
    /// <exception cref="BasicEngineExecutionException">
    /// Thrown when the execution environment is not initialized.
    /// </exception>
    /// <exception cref="BasicIllegalFunctionCallException">
    /// Thrown when no such procedure is defined.
    /// </exception>
    public void CallProcedure(int lineNumber, IScopeContainer scope, string procedureName, List<object> parameterValues)
    {
        if (_environment == null)
            throw new BasicEngineExecutionException(lineNumber, "The execution environment is not initialized.");

        IProcedure? procedure = _environment.Procedures!.GetProcedure(procedureName);
        if (procedure == null)
        {
            throw new BasicIllegalFunctionCallException(lineNumber, "No such procedure defined.");
        }

        procedure.Execute(lineNumber, this, _environment);
    }

    /// <summary>
    /// Evaluates the expression based on the current execution context and state.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="scope">
    /// The reference to the parent <see cref="IScopeContainer" /> instance.
    /// </param>
    /// <param name="expression">
    /// The <see cref="ICodeExpression" /> instance to evaluate.
    /// </param>
    /// <returns>
    /// The result of the expression evaluation, or <b>null</b> if there is no result.
    /// </returns>
    /// <exception cref="BasicEngineExecutionException">
    /// Thrown when the execution environment is not initialized.
    /// </exception>
    public object? EvaluateExpression(int lineNumber, IScopeContainer scope, ICodeExpression expression)
    {
        if (_environment == null)
            throw new BasicEngineExecutionException(lineNumber, "The execution environment is not initialized.");

        object? data = null;

        switch (expression)
        {
            case BlazorBasicCodeConditionalExpression booleanConditional:
                data = booleanConditional.Evaluate(this, _environment, scope);
                break;

            case BasicLiteralStringExpression literalString:
                data = (string)literalString.Evaluate(this, _environment, scope);
                break;

            case BlazorBasicLiteralCharacterExpression literalChar:
                data = literalChar.Value;
                break;

            case BasicLiteralIntegerExpression literalInt32:
                data = literalInt32.Value;
                break;

            case BlazorBasicLiteralFloatingPointExpression literalFloat:
                data = literalFloat.Value;
                break;

            case BasicVariableReferenceExpression variableReference:
                data = variableReference.VariableName;
                break;

            case BasicVariableNameExpression variableName:
                IVariable? variable = scope.GetVariable(variableName.VariableName);
                if (variable == null)
                    throw new BasicInvalidArgumentException(lineNumber, "Variable does not exist.");

                data = variable.GetValue();
                break;

            case BlazorBasicBasicArithmeticExpression mathExpression:
                data = mathExpression.Evaluate(this, _environment, scope);
                break;

            case BlazorBasicParameterValueExpression paramValueExpression:
                data = paramValueExpression.Evaluate(this, _environment, scope);
                break;

            default:
                throw new NotSupportedException($"Expression type {expression.GetType().Name} is not supported yet.");
        }
        return data;
    }

    public void Execute()
    {
        if (_environment.MainProcedure == null)
            throw new BasicEngineExecutionException(0, "No MAIN procedure defined.");

        _environment.MainProcedure.Execute(0, this, _environment);
    }
    #endregion
}
