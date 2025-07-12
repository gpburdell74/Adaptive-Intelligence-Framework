using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the methods and functions for executing statements in the Blazor BASIC language.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ICodeDomStatementExecutor" />
public sealed class BasicStatementExecutor : DisposableObjectBase, ICodeDomStatementExecutor
{
    #region Private Member Declarations
    /// <summary>
    /// The execution engine instance.
    /// </summary>
    private IExecutionEngine? _engine;

    /// <summary>
    /// The execution environment instance.
    /// </summary>
    private IExecutionEnvironment? _environment;

    /// <summary>
    /// The current statement index value.
    /// </summary>
    private int _statementIndex = 0;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicStatementExecutor"/> class.
    /// </summary>
    /// <param name="engine">
    /// The reference to the <see cref="IExecutionEngine"/> engine instance to be used.
    /// </param>
    /// <param name="environment">
    /// The reference to the <see cref="IExecutionEnvironment"/> environment instance.
    /// </param>
    public BasicStatementExecutor(IExecutionEngine engine, IExecutionEnvironment environment)
    {
        _engine = engine;
        _environment = environment;
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _statementIndex = 0;
        _environment = null;
        _engine = null;
        base.Dispose(disposing);
    }
    #endregion

    #region  Public Properties
    /// <summary>
    /// Gets the reference to the execution engine instance.
    /// </summary>
    /// <value>
    /// An <see cref="IExecutionEngine" /> instance.
    /// </value>
    public IExecutionEngine? Engine => _engine;

    /// <summary>
    /// Gets the reference to the execution environment instance.
    /// </summary>
    /// <value>
    /// An <see cref="IExecutionEnvironment" /> instance.
    /// </value>
    public IExecutionEnvironment? Environment => _environment;

    public int StatementIndex
    {
        get => _statementIndex;
        set => _statementIndex = value;
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Assigns the a value to a variable instance.
    /// </summary>
    /// <param name="scopeContainer">
    /// The <see cref="IScopeContainer" /> container specifying the function or procedure being 
    /// executed.
    /// </param>
    /// <param name="assignStatement">
    /// A <see cref="ICodeVariableAssignmentStatement" /> instance describing the variable to populate, 
    /// and an expression defining the value to assign to the variable.
    /// </param>
    /// <exception cref="BasicVariableNotFoundException">
    /// Thrown if the specified variable is not found or is not defined.
    /// </exception>
    /// <exception cref="BasicTypeMismatchException">
    /// Thrown if the data types of the specified variable and the value do not match.
    /// </exception>
    public void AssignValueToVariable(IScopeContainer scopeContainer, ICodeVariableAssignmentStatement assignStatement)
    {
        string? variableToAssign = assignStatement.VariableReference?.VariableName;

        // Validate the variable exists.
        if (variableToAssign == null || !scopeContainer.VariableExists(variableToAssign))
            throw new BasicVariableNotFoundException(assignStatement.LineNumber, variableToAssign ?? "No variable name");

        IVariable? variable = scopeContainer.GetVariable(variableToAssign);
        if (variable == null)
            throw new BasicVariableNotFoundException(assignStatement.LineNumber, variableToAssign ?? "No variable name");

        try
        {
            variable.SetValue(_engine.EvaluateExpression(assignStatement.LineNumber, scopeContainer, assignStatement.Expression));
        }
        catch (Exception ex)
        {
            throw new BasicTypeMismatchException(assignStatement.LineNumber, ex.Message);

        }
    }

    /// <summary>
    /// Closes the specified file.
    /// </summary>
    /// <param name="scopeContainer">
    /// The <see cref="IScopeContainer" /> container specifying the function or procedure being executed.
    /// </param>
    /// <param name="closeStatement">
    /// The <see cref="ICodeCloseFileStatement" /> statement describing the file to be closed.
    /// </param>
    public void CloseFile(IScopeContainer scopeContainer, ICodeCloseFileStatement closeStatement)
    {
        if (_environment != null)
        {
            if (closeStatement.FileHandleExpression == null)
                throw new BasicSyntaxErrorException(closeStatement.LineNumber, "Invalid file handle specified.");

            int fileHandle = closeStatement.FileHandleExpression.Evaluate();
            _environment.CloseFile(closeStatement.LineNumber, fileHandle);
        }
    }

    /// <summary>
    /// Creates the parameters as variables inside the scope of the specified function.
    /// </summary>
    /// <param name="scopeContainer">
    /// The <see cref="IScopeContainer" /> container specifying the function being called.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeFunctionStartStatement" /> instance describing the function and its parameter
    /// and return value definitions.
    /// </param>
    /// <exception cref="BasicEngineExecutionException">
    /// Thrown when the parameter list is invalid or null.
    /// </exception>  
    /// <exception cref="BasicSyntaxErrorException">
    /// Thrown when the parameter definition cannot be parsed correctly.
    /// </exception>
    /// <remarks>
    /// The parameters provided to the function call are essentially additional variables within
    /// the scope of the function.  This call ensures that the requisite variable instances are
    /// created.
    /// </remarks>
    public void CreateParametersAsVariablesInFunctionScope(IScopeContainer scopeContainer, ICodeFunctionStartStatement statement)
    {
        int lineNumber = statement.LineNumber;

        if (statement.Parameters != null && statement.ParameterCount > 0)
        {
            List<ICodeParameterDefinitionExpression>? paramList = statement.Parameters.ParameterList;
            if (paramList == null)
                throw new BasicEngineExecutionException(lineNumber, "Invalid parameter list.");

            foreach(ICodeParameterDefinitionExpression paramDef in paramList)
            {
                if (paramDef.ParameterName == null || paramDef.DataType == null)
                    throw new BasicSyntaxErrorException(lineNumber, "Invalid parameter definition.");

                scopeContainer.CreateParameterVariable(lineNumber,
                    paramDef.ParameterName, paramDef.DataType.DataType, paramDef.IsArray,
                    paramDef.ArraySize);
            }
        }
    }

    /// <summary>
    /// Creates the parameters as variables inside the scope of the specified procedure.
    /// </summary>
    /// <param name="scopeContainer">
    /// The <see cref="IScopeContainer" /> container specifying the function being called.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeFunctionStartStatement" /> instance describing the function and its parameter
    /// and return value definitions.
    /// </param>
    /// <exception cref="BasicEngineExecutionException">
    /// Thrown when the parameter list is invalid or null.
    /// </exception>  
    /// <exception cref="BasicSyntaxErrorException">
    /// Thrown when the parameter definition cannot be parsed correctly.
    /// </exception>
    /// <remarks>
    /// The parameters provided to the procedure call are essentially additional variables within
    /// the scope of the procedure.  This call ensures that the requisite variable instances are
    /// created.
    /// </remarks>
    public void CreateParametersAsVariablesInProcedureScope(IScopeContainer scopeContainer, ICodeProcedureStartStatement statement)
    {
        int lineNumber = statement.LineNumber;

        if (statement.Parameters != null && statement.ParameterCount > 0)
        {
            List<ICodeParameterDefinitionExpression>? paramList = statement.Parameters.ParameterList;
            if (paramList == null)
                throw new BasicEngineExecutionException(lineNumber, "Invalid parameter list.");

            foreach (ICodeParameterDefinitionExpression paramDef in paramList)
            {
                if (paramDef.ParameterName == null || paramDef.DataType == null)
                    throw new BasicSyntaxErrorException(lineNumber, "Invalid parameter definition.");

                scopeContainer.CreateParameterVariable(lineNumber,
                    paramDef.ParameterName, paramDef.DataType.DataType, paramDef.IsArray,
                    paramDef.ArraySize);
            }
        }
    }

    /// <summary>
    /// Creates the parameters as variables withing the parent scope.
    /// </summary>
    /// <param name="scopeContainer">
    /// The <see cref="IScopeContainer" /> container specifying the function being called.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeFunctionStartStatement" /> instance being executed.
    /// </param>
    public void CreateParametersAsVariablesInScope(IScopeContainer scopeContainer, BasicProcedureStartStatement statement)
    {
        if (statement.Parameters != null && statement.Parameters.ParameterList != null)
        {
            foreach (BasicParameterDefinitionExpression paramExpression in statement.Parameters.ParameterList)
            {
                if (paramExpression.ParameterName == null || paramExpression.DataType == null)
                    throw new BasicSyntaxErrorException(statement.LineNumber, "Syntax error in parameter definitions.");

                scopeContainer.CreateParameterVariable(
                    statement.LineNumber,
                    paramExpression.ParameterName, 
                    paramExpression.DataType.DataType,
                    paramExpression.DataType.IsArray, 
                    paramExpression.DataType.Size);
            }
        }
    }

    /// <summary>
    /// Declares the variable instance within the specified scope container.
    /// </summary>
    /// <param name="scopeContainer">
    /// The reference to the <see cref="IScopeContainer" /> container instance such as a
    /// procedure or function.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeVariableDeclarationStatement" /> statement instance containing the variable
    /// definition.
    /// </param>
    /// <exception cref="BasicSyntaxErrorException">
    /// Thrown when there is a syntax error in the variable declaration.
    /// </exception>
    public void DeclareVariableInScope(IScopeContainer scopeContainer, ICodeVariableDeclarationStatement statement)
    {
        int lineNumber = statement.LineNumber;

        ICodeDataTypeExpression? dataExpression = statement.DataType;
        ICodeVariableNameExpression? varExpression = statement.VariableReference;

        if (dataExpression == null || varExpression == null || string.IsNullOrEmpty(varExpression.VariableName))
            throw new BasicSyntaxErrorException(lineNumber, "Syntax error in variable declaration.");

        if (dataExpression != null)
        {
            scopeContainer.CreateParameterVariable(
                    lineNumber,
                    varExpression.VariableName,
                    dataExpression.DataType,
                    dataExpression.IsArray,
                    dataExpression.ArraySize);
        }
    }

    /// <summary>
    /// Executes the clear screen command..
    /// </summary>
    /// <param name="statement">
    /// The statement being executed.
    /// </param>
    public void ExecuteCls(BasicClsStatement statement)
    {
        if (_environment == null || _environment.StandardOutput == null)
            throw new BasicEngineExecutionException(statement.LineNumber);

        _environment.StandardOutput.Cls();
    }

    /// <summary>
    /// Executes the operations for opening a file.
    /// </summary>
    /// <param name="statement">
    /// The <see cref="BasicOpenStatement"/> being executed.
    /// </param>
    public void ExecuteFileOpen(BasicOpenStatement statement)
    {
        int lineNumber = statement.LineNumber;
        if (statement.FileNumber == null)
            throw new BasicInvalidArgumentException(lineNumber, "No valid file handle was specified.");

        int handle = statement.FileNumber.FileNumber;

        // Open the file.
        IFileSystemProvider fileSystem = _environment.System.FileSystem;
        FileStream? stream = fileSystem.OpenFile(statement.FileName.FileName,
            statement.FileDirection.Mode, statement.FileDirection.Access,
            FileShare.None);

        if (stream != null)
        {
            _environment.RegisterFileHandle(lineNumber, handle, stream);
        }
    }

    /// <summary>
    /// Executes a FOR ... NEXT loop.
    /// </summary>
    /// <param name="scopeContainer">
    /// The reference to the <see cref="IScopeContainer" /> container instance such as a
    /// procedure or function.
    /// </param>
    /// <param name="statement">
    /// The <see cref="ICodeForLoopStatement" /> statement instance defining the boundaries of
    /// the for loop.
    /// </param>
    public void ExecuteForLoop(IScopeContainer scopeContainer, ICodeForLoopStatement statement)
    {
    }

    /// <summary>
    /// Executes the a function call.
    /// </summary>
    /// <param name="scopeContainer">
    /// The reference to the <see cref="IScopeContainer" /> container instance such as a
    /// procedure or function.
    /// </param>
    /// <param name="functionCallStatement">
    /// The <see cref="ICodeFunctionCallStatement" /> statement instance defining the function call, its
    /// parameter values, and expected return type.
    /// </param>
    public void ExecuteFunctionCall(IScopeContainer scopeContainer, ICodeFunctionCallStatement functionCallStatement)
    {
    }

    public void ExecuteGetInputFromUser(IScopeContainer scopeContainer, ICodeUserInputStatement statement)
    {
    }

    public void ExecuteIfElseBlock(IScopeContainer scopeContainer, ICodeIfStatement statement)
    {
    }

    public void ExecuteLoopWithEndStartCondition(IScopeContainer scopeContainer, ICodeLoopStartStatement statement)
    {
    }

    public void ExecuteLoopWithStartCondition(IScopeContainer scopeContainer, ICodeLoopStartConditionalStatement statement)
    {
    }

    public bool ExecuteNextStatement(IScopeContainer scopeContainer)
    {
        ExecuteStatement(scopeContainer, scopeContainer.Code![_statementIndex]);
        return _statementIndex >= scopeContainer.Code.Count;

    }

    public void ExecuteProcedureCall(IScopeContainer scopeContainer, ICodeProcedureCallStatement procCallStatement)
    {

    }

    public void ExecuteStatement(
        IScopeContainer scopeContainer,
        ICodeStatement statement)
    {
        switch (statement)
        {
            case BasicClsStatement clsStatement:
                ExecuteCls(  clsStatement);
                _statementIndex++;
                break;

            case BasicCloseStatement closeStatement:
                ExecuteCloseStatement(  scopeContainer, closeStatement);
                _statementIndex++;
                break;

            case BasicOpenStatement openStatement:
                ExecuteFileOpen(  openStatement);
                _statementIndex++;
                break;

            case BasicVariableDeclarationStatement dimStatement:
                DeclareVariableInScope(scopeContainer, dimStatement);
                _statementIndex++;
                break;

            case BasicProcedureStartStatement procStatement:
                CreateParametersAsVariablesInScope(  scopeContainer, procStatement);
                _statementIndex++;
                break;

            case BasicVariableAssignmentStatement assignStatement:
                AssignValueToVariable(scopeContainer, assignStatement);
                _statementIndex++;
                break;

            case BasicDoStatement doStatement:
                ExecuteDoLoop(scopeContainer, doStatement);
                break;

            case BasicInputStatement inputStatement:
                PerformInput(scopeContainer, inputStatement);
                _statementIndex++;
                break;

            case BasicIfStatement ifStatement:
                ExecuteIfElseBlock(  scopeContainer, ifStatement);
                break;

            case BasicWriteStatement writeStatement:
                ExecuteWriteStatement(  scopeContainer, writeStatement);
                _statementIndex++;
                break;

            case BasicProcedureCallStatement procCallStatement:
                ExecuteProcedureCall(  scopeContainer, procCallStatement);
                break;

            case BasicCommentStatement:
            case BlazorBasicEmptyStatement:
            case BasicEndStatement:
            case BasicLoopStatement:
            case BasicProcedureEndStatement:
            case BasicFunctionEndStatement:
                _statementIndex++;
                break;

            default:
                throw new NotSupportedException($"Statement type {statement.GetType().Name} is not supported yet.");
        }
    }


    private void ExecuteCloseStatement(
        IScopeContainer scopeContainer,
        BasicCloseStatement closeStatement)
    {
        _environment.CloseFile(0, closeStatement.FileNumberExpression.FileNumber);
    }

    private void ExecuteDoLoop(
        IScopeContainer scopeContainer,
        BasicDoStatement statement)
    {
        int loopIndex = ((BasicCodeStatementList)scopeContainer.Code).IndexOf(_statementIndex, typeof(BasicLoopStatement));
        bool done = false;
        _statementIndex++;
        int startIndex = _statementIndex;
        do
        {
            BasicCodeStatement nextStatement = (BasicCodeStatement)scopeContainer.Code[_statementIndex];
            if (_statementIndex < loopIndex)
            {
                ExecuteStatement( scopeContainer, nextStatement);
            }
            
            if (_statementIndex == loopIndex)
            {
                BasicLoopStatement loopStatement = ((BasicLoopStatement)scopeContainer.Code[_statementIndex]);
                object? result = _engine.EvaluateExpression(loopStatement.LineNumber,
                    scopeContainer, loopStatement.ConditionExpression);
                if (result != null && result is bool condition)
                {
                    done = condition;
                    if (done)
                        _statementIndex++;
                    else
                        _statementIndex = startIndex;
                }

            }
        } while (_statementIndex <= loopIndex && !done);

        _statementIndex = loopIndex + 1;

    }

    private void ExecuteIfElseBlock(IScopeContainer scopeContainer,
        BasicIfStatement ifStatement)
    {
        int currentIndex = scopeContainer.Code.IndexOf(ifStatement);
        int endIfIndex = ((BasicCodeStatementList)scopeContainer.Code).FindEndIf(currentIndex);
        bool done = false;

        object data = _engine.EvaluateExpression(ifStatement.LineNumber, scopeContainer,
            ifStatement.ConditionalExpression);
        bool conditionIsTrue = (bool)(data);
        if (conditionIsTrue)
        {
            for (int startIndex = currentIndex + 1; startIndex < endIfIndex; startIndex++)
            {
                ExecuteStatement(scopeContainer, scopeContainer.Code[startIndex]);
            }
        }
        else
        {

        }
        _statementIndex = endIfIndex + 1;
    }

    private void ExecuteProcedureCall(
        IScopeContainer scopeContainer,
        BasicProcedureCallStatement procCallStatement)
    {
        int lineNumber = procCallStatement.LineNumber;

        List<object> paramValueList = new List<object>();

        foreach (BlazorBasicParameterValueExpression exp in procCallStatement.Parameters)
        {
            object a = _engine.EvaluateExpression(procCallStatement.LineNumber, scopeContainer, exp);
            paramValueList.Add(a);
        }

        _engine.CallProcedure(lineNumber, scopeContainer, procCallStatement.ProcedureName,
             paramValueList);
    }

    private void ExecuteWriteStatement(
        IScopeContainer scopeContainer,
        BasicWriteStatement writeStatement)
    {
        int lineNumber = writeStatement.LineNumber;

        FileStream? fs = _environment.GetFileStream(lineNumber, writeStatement.FileNumber.FileNumber);
        bool isBinary = _environment.IsBinaryFile(lineNumber, writeStatement.FileNumber.FileNumber);

        SafeBinaryWriter? binWriter = null;
        StreamWriter? streamWriter = null;

        if (isBinary)
            binWriter = new SafeBinaryWriter(fs);
        else
            streamWriter = new StreamWriter(fs);

        foreach (BasicExpression expression in writeStatement.Expressions)
        {
            object data = _engine.EvaluateExpression(writeStatement.LineNumber,scopeContainer, expression);
            if (isBinary)
            {
                //                binWriter.Write(data);
            }
            else
            {
                streamWriter.Write(data);
            }
        }
        if (isBinary)
            binWriter.Flush();
        else
            streamWriter.Flush();
        fs.Flush();
        fs = null;
    }

    /// <summary>
    /// Performs the process of collecting input from the user.
    /// </summary>
    /// <param name="scopeContainer">The scope container.</param>
    /// <param name="inputStatement">The input statement.</param>
    /// <exception cref="Adaptive.Intelligence.BlazorBasic.BasicVariableNotFoundException"></exception>
    /// <exception cref="Adaptive.Intelligence.BlazorBasic.BasicTypeMismatchException"></exception>
    private void PerformInput(IScopeContainer scopeContainer, BasicInputStatement inputStatement)
    {
        if (inputStatement.PromptExpression != null)
        {
            object? data = _engine.EvaluateExpression(
                inputStatement.LineNumber,
                scopeContainer, 
                (BasicExpression)inputStatement.PromptExpression);

            if (data != null && data is string stringData)
            {
                Console.Write(data);
            }

            string userData = Console.ReadLine();

            if (inputStatement.VariableReferenceExpression != null)
            {
                string variableName = inputStatement.VariableReferenceExpression.VariableName;
                if (!scopeContainer.VariableExists(variableName))
                    throw new BasicVariableNotFoundException(inputStatement.LineNumber, variableName);
                IVariable variable = scopeContainer.GetVariable(variableName);
                try
                {
                    variable.SetValue(userData);
                }
                catch (Exception ex)
                {
                    throw new BasicTypeMismatchException(inputStatement.LineNumber);
                }
            }
        }
    }

    public void ExecuteStatement(IExecutionEngine engine, IExecutionEnvironment environment, IScopeContainer scope, ICodeStatement statement)
    {
        throw new NotImplementedException();
    }

    public void OpenFile(IScopeContainer scopeContainer, ICodeOpenFileStatement statement)
    {
        throw new NotImplementedException();
    }

    public void StandardOutput(IScopeContainer scopeContainer, ICodeStandardOutputStatement writeStatement)
    {
        throw new NotImplementedException();
    }
    #endregion
}
