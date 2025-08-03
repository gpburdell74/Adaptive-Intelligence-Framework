using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.BlazorBasic.LanguageService;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Providers;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

/// <summary>
/// Provides the methods and functions for implementing a virtual execution environment for 
/// a Blazor BASIC program.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IExecutionEnvironment" />
public sealed class BasicExecutionEnvironment : DisposableObjectBase, IExecutionEnvironment
{
    #region Private Member Declarations
    /// <summary>
    /// The data type provider.
    /// </summary>
    private IDataTypeProvider? _dataTypeProvider;

    /// <summary>
    /// The standard output implementation to use.
    /// </summary>
    private IStandardOutput? _console;

    /// <summary>
    /// The code unit to execute.
    /// </summary>
    private ICodeInterpreterUnit? _codeUnit;

    /// <summary>
    /// The file table instance.
    /// </summary>
    private OpenFileTable? _fileTable;

    /// <summary>
    /// The function look-up table for the code being executed.
    /// </summary>
    private IFunctionTable? _functions;

    /// <summary>
    /// The global variables list.
    /// </summary>
    private IVariableTable? _globals;

    /// <summary>
    /// The ID generator for integer-based ID values.
    /// </summary>
    private IIdGenerator? _idGenerator;

    /// <summary>
    /// The reference to the main procedure to start the application.
    /// </summary>
    private IProcedure? _mainProc;

    /// <summary>
    /// The procedure look-up table for the code being executed.
    /// </summary>
    private IProcedureTable? _procedures;

    /// <summary>
    /// The system instance.
    /// </summary>
    private ISystem? _system;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicExecutionEnvironment"/> class.
    /// </summary>
    /// <param name="dataTypeProvider">
    /// The <see cref="IDataTypeProvider"/> instance to use.
    /// </param>
    /// <param name="idGenerator">
    /// The reference to the <see cref="IIdGenerator"/> instance to use.
    /// </param>
    /// <param name="functionTable">
    /// The reference to the <see cref="IFunctionTable"/> instance.
    /// </param>
    /// <param name="procedureTable">
    /// The reference to the <see cref="IProcedureTable"/> instance.
    /// </param>
    /// <param name="globalsTable">
    /// The reference to the <see cref="IVariableTable"/> instance used to store the global
    /// variables.
    /// </param>
    /// <param name="output">
    /// The reference to the <see cref="IStandardOutput"/> implementation instance.
    /// </param>
    /// <param name="system">
    /// The reference to the <see cref="ISystem"/> implementation instance representing the 
    /// APIs for the underlying virtual system (I/O, memory, network, etc.)
    /// </param>
    public BasicExecutionEnvironment(
        BasicDataTypeProvider dataTypeProvider,
        IIdGenerator idGenerator,
        IFunctionTable functionTable,
        IProcedureTable procedureTable,
        IVariableTable globalsTable,
        IStandardOutput output,
        ISystem system)
    {
        _dataTypeProvider = dataTypeProvider;
        _idGenerator = idGenerator;
        _functions = functionTable;
        _procedures = procedureTable;
        _globals = globalsTable;
        _console = output;
        _system = system;
        _fileTable = new OpenFileTable();
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        UnloadUnit();
        if (!IsDisposed && disposing)
        {
            _fileTable?.SafeShutdown();
            _fileTable?.Dispose();
        }

        _dataTypeProvider = null;
        _codeUnit = null;
        _idGenerator = null;
        _console = null;
        _functions = null;
        _procedures = null;
        _fileTable = null;
        _globals = null;
        _system = null;

        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets the reference to the list of CodeDOM statements to be executed.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}" /> of <see cref="ICodeStatement" /> instances.
    /// </value>
    public List<ICodeStatement>? Code { get; }

    /// <summary>
    /// Gets the reference to the table containing the defined function instances.
    /// </summary>
    /// <value>
    /// An <see cref="IFunctionTable" /> instance containing the function instances.
    /// </value>
    public IFunctionTable? Functions => _functions;

    /// <summary>
    /// Gets the reference to the list of global variables.
    /// </summary>
    /// <value>
    /// An <see cref="IVariableTable" /> instance containing the global variables.
    /// </value>
    public IVariableTable? GlobalVaraibles => _globals;

    /// <summary>
    /// Gets the reference to the identifier generator.
    /// </summary>
    /// <value>
    /// The <see cref="IIdGenerator"/> instance.
    /// </value>
    public IIdGenerator? IdGenerator => _idGenerator;

    /// <summary>
    /// Gets the reference to the <see cref="IProcedure"/> defined as the Main procedure.
    /// </summary>
    /// <value>
    /// The <see cref="IProcedure"/> instance used as the entry point to the application.
    /// </value>
    public IProcedure? MainProcedure => _mainProc;

    /// <summary>
    /// Gets the reference to the table containing the defined procedure instances.
    /// </summary>
    /// <value>
    /// An <see cref="IProcedureTable" /> instance containing the procedure instances.
    /// </value>
    public IProcedureTable? Procedures => _procedures;

    /// <summary>
    /// Gets the reference to the standard output device or mechanism.
    /// </summary>
    /// <value>
    /// The <see cref="IStandardOutput" /> instance used for output operations.
    /// </value>
    public IStandardOutput? StandardOutput => _console;

    /// <summary>
    /// Gets the reference to the system API implementation.
    /// </summary>
    /// <value>
    /// The <see cref="ISystem" /> instance providing access to system-level operations
    /// </value>
    public ISystem? System => _system;
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Closes the file with the specified file handle.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the current line number.
    /// </param>
    /// <param name="fileHandle">
    /// An integer indicating the file handle value.
    /// </param>
    public void CloseFile(int lineNumber, int fileHandle)
    {
        if (_fileTable != null && _fileTable.ContainsHandle(fileHandle))
        {
            FileStream? stream = _fileTable.GetFileStream(lineNumber, fileHandle);

            if (_system  != null && _system.FileSystem != null)
                _system.FileSystem.CloseFile(stream);

            _fileTable.UnRegister(lineNumber, fileHandle);
        }
    }
    /// <summary>
    /// Creates the variable within the global scope.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number of the code line being executed.
    /// </param>
    /// <param name="variableName">
    /// A string containing the name of the variable.
    /// </param>
    /// <param name="dataType">
    /// A <see cref="StandardDataTypes" /> enumerated value indicating the data type.
    /// </param>
    /// <param name="isArray">
    /// <b>true</b> if the data type is an array.
    /// </param>
    /// <param name="size">
    /// If <paramref name="isArray" /> is true, specifies the size of the array.
    /// </param>
    public void CreateVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size)
    {
        if (_globals == null)
            throw new BasicEngineExecutionException(lineNumber, "Global variable table is not initialized.");

        if (_globals.Exists(variableName))
            throw new BasicDuplicateDefinitionException(lineNumber, "Variable already defined: " + variableName);   

        IVariable variable = new BasicVariable(variableName, dataType, isArray, size);
        _globals.Add(variable);
    }

    /// <summary>
    /// Creates the variable within the scope from its specified parameter definition.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the line number of the code line being executed.
    /// </param>
    /// <param name="variableName">
    /// A string containing the name of the variable.
    /// </param>
    /// <param name="dataType">
    /// A <see cref="StandardDataTypes" /> enumerated value indicating the data type.
    /// </param>
    /// <param name="isArray">
    /// <b>true</b> if the data type is an array.
    /// </param>
    /// <param name="size">
    /// If <paramref name="isArray" /> is true, specifies the size of the array.
    /// </param>
    public void CreateParameterVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size)
    {
        // Not implemented because this is not applicable at this level.
    }

    /// <summary>
    /// Loads the source into memory as an interpreter unit and prepares the environment for execution.
    /// </summary>
    /// <param name="interpreterUnit">
    /// The <see cref="ICodeInterpreterUnit" /> instance containing the loaded source code.
    /// </param>
    public void LoadUnit(ICodeInterpreterUnit interpreterUnit)
    {
        _codeUnit = interpreterUnit;

        // Separate the procedures and functions code.
        SeparateCodeIntoParts();

    }

    /// <summary>
    /// Gets the file stream reference.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="fileHandle">An integer specifying the file handle.</param>
    /// <returns>
    /// The <see cref="FileStream" /> instance, or <b>null</b> if not found.
    /// </returns>
    public FileStream? GetFileStream(int lineNumber, int fileHandle)
    {
        if (_fileTable == null)
            throw new BasicEngineExecutionException(lineNumber, "File Manager not initialized.");

        FileStream? stream = _fileTable.GetFileStream(lineNumber, fileHandle);
        if (stream == null)
            throw new BasicInvalidArgumentException(lineNumber, "Invalid file handle.");

        return stream;
    }

    /// <summary>
    /// Determines whether the specified file is opened for binary (rather than text).
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="fileHandle">An integer specifying the file handle.</param>
    /// <returns>
    /// <c>true</c> if the specified file is opened for binary; otherwise, <c>false</c>.
    /// </returns>
    public bool IsBinaryFile(int lineNumber, int fileHandle)
    {
        return true;
    }

    /// <summary>
    /// Gets the reference to the variable instance with the specified name.
    /// </summary>
    /// <param name="variableName">A string containing the name of the variable to find.</param>
    /// <returns>
    /// The <see cref="T:Adaptive.Intelligence.LanguageService.Execution.IVariable" /> instance, if found.
    /// </returns>
    public IVariable? GetVariable(string variableName)
    {
        return _globals?.GetVariable(variableName);
    }

    /// <summary>
    /// Registers the file handle with the specified stream instance.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer indicating the current line number in use.
    /// </param>
    /// <param name="fileHandle">
    /// An integer containing the file handle value.
    /// </param>
    /// <param name="stream">
    /// The <see cref="FileStream"/> instance that was opened.
    /// </param>
    /// <exception cref="BasicEngineExecutionException">
    /// Thrown when the internal file manager / file  table instance is <b>null</b>.
    /// </exception>
    public void RegisterFileHandle(int lineNumber, int fileHandle, FileStream stream)
    {
        if (_fileTable == null)
            throw new BasicEngineExecutionException(lineNumber, "File Manager not initialized.");

        _fileTable.Register(lineNumber, fileHandle, stream);
    }
    /// <summary>
    /// Unloads the <see cref="ICodeInterpreterUnit" /> loaded into memory and clears all
    /// environmental variables, lists, and closes all files.
    /// </summary>
    public void UnloadUnit()
    {

        // Remove the function and procedure definitions, and global variables.
        _mainProc = null;
        _globals?.Clear();
        _functions?.Clear();
        _procedures?.Clear();

        // Unload the Code DOM graph.
        _codeUnit?.Dispose();
        _codeUnit = null;
        
        // Reset the state of the environment.
        _fileTable?.SafeShutdown();
        _idGenerator?.Reset();

        _system?.Reset();

    }

    /// <summary>
    /// Gets a value indicating whether a variable with the specified name exists within this
    /// scope.
    /// </summary>
    /// <param name="variableName">A string containing the name of the variable to check for.</param>
    /// <returns>
    /// <b>true</b> if the variable with the specified name exists; otherwise, <b>false</b>.
    /// </returns>
    public bool VariableExists(string? variableName)
    {
        if (_globals == null)
            return false;
        
        return _globals.Exists(variableName);
    }
    #endregion

    #region Private Methods / Functions    
    /// <summary>
    /// Separates the code into its scoped parts.
    /// </summary>
    /// <exception cref="BasicEngineExecutionException">0 - No code loaded.</exception>
    /// <exception cref="BasicBadDataTypeException">0 - Invalid or missing data type in function definition.</exception>
    private void SeparateCodeIntoParts()
    {
        if (_codeUnit == null)
            throw new BasicEngineExecutionException(0, "No code loaded.");
        
        BlazorBasicCodeStatementsTable statements = GetStatements()!;
        int length = statements.Count;
        int index = 0;

        do
        {
            ICodeStatement statement = statements[index];

            if (statement is BasicProcedureStartStatement procStart)
            {
                index = AddProcedure(statements, index);
            }
            else if (statement is BasicFunctionStartStatement functionStart)
            {
                // Extract the .NET data type for the return value of the function.
                if (functionStart == null || functionStart.ReturnExpression == null)
                    throw new BasicBadDataTypeException(0, "Invalid or missing data type in function definition.");

                Type? functionType = _dataTypeProvider!.MapToDotNetType(functionStart.ReturnExpression.Render()!);
                if (functionType == null)
                    throw new BasicBadDataTypeException(0, "Invalid or missing data type in function definition.");

                index = AddFunction(statements, index, functionType);
            }
            else if (statement is BasicVariableDeclarationStatement globalVarStart)
            {
                index = AddGlobalVariable(statements, index);
            }
            else
                index++;
        } while (index < length);

    }

    /// <summary>
    /// Gets the list of code statements.
    /// </summary>
    /// <returns>
    /// A <see cref="BlazorBasicCodeStatementsTable"/> instance containing the code statement list.
    /// </returns>
    /// <exception cref="BasicEngineExecutionException">
    /// Thrown when there is no code to read.
    /// </exception>
    private BlazorBasicCodeStatementsTable? GetStatements()
    {
        BlazorBasicCodeStatementsTable? statements = null;

        if (_codeUnit != null)
        {
            if (_codeUnit.Statements == null)
            {
                throw new BasicEngineExecutionException(0, "No code loaded.");
            }
            else
            {
                statements = (BlazorBasicCodeStatementsTable)_codeUnit.Statements;  
            }
        }
        return statements;
    }

    /// <summary>
    /// Adds the function to the list of functions.
    /// </summary>
    /// <param name="statements">
    /// The <see cref="BlazorBasicCodeStatementsTable"/> containing the list of code statements.
    /// </param>
    /// <param name="currentIndex">
    /// An integer specifying the ordinal index of the current code line.
    /// </param>
    /// <param name="returnType">
    /// The data <see cref="Type"/> of the return value of the function when executed.
    /// </param>
    /// <returns>
    /// An integer specifying the next index of the list of code lines to return to.
    /// </returns>

    private int AddFunction(BlazorBasicCodeStatementsTable statements, int currentIndex, Type returnType)
    {
        int returnIndex = -1;
        int length = statements.Count;
        int index = currentIndex;

        // Get the list of  code within the procedure to be executed.
        // (e.g. between the FUNCTION ... and END FUNCTION lines.
        List<ICodeStatement> functionCodeList = new List<ICodeStatement>();
        do
        {
            ICodeStatement statement = statements[index];
            functionCodeList.Add(statement);

            if (statement is BasicFunctionEndStatement endStatement)
            {
                returnIndex = index + 1;
            }
            index++;
        } while (index < length && returnIndex == -1);

        // Create and add the new instance.
        IFunction newFunction = new BasicFunction(functionCodeList, this, returnType);
        _functions?.Add(newFunction);

        return index;
    }
    
    /// <summary>
    /// Adds the procedure to the list of procedures.
    /// </summary>
    /// <param name="statements">
    /// The <see cref="BlazorBasicCodeStatementsTable"/> containing the list of code statements.
    /// </param>
    /// <param name="currentIndex">
    /// An integer specifying the ordinal index of the current code line.
    /// </param>
    /// <returns>
    /// An integer specifying the next index of the list of code lines to return to.
         /// </returns>
    private int AddProcedure(BlazorBasicCodeStatementsTable statements, int currentIndex)
    {
        int returnIndex = -1;
        int length = statements.Count;
        int index = currentIndex;

        // Get the list of  code within the procedure to be executed.
        // (e.g. between the PROCEDURE ... and END PROCEDURE lines.
        List<ICodeStatement> procCodeList = new List<ICodeStatement>();
        index++;
        do
        {
            ICodeStatement statement = statements[index];
            procCodeList.Add(statement);
            if (statement is BasicProcedureEndStatement endStatement)
            {
                returnIndex = index + 1;
            }
            index++;
        } while (index < length && returnIndex == -1);


        // Extract the name and parameter list from the first statement.
        BasicProcedureStartStatement startStatement = statements[currentIndex] as BasicProcedureStartStatement;

        // Create and add the new instance.
        BasicProcedure newProcedure = new BasicProcedure(procCodeList, this, startStatement.ProcedureName,
            startStatement?.Parameters?.ParameterList);
        _procedures!.Add(newProcedure);

        // If this is the MAIN procedure, set the pointer.
        if (newProcedure!.Name!.Trim().ToUpper() == KeywordNames.KeywordMain)
        {
            _mainProc = newProcedure;
        }
        return index;
    }

    /// <summary>
    /// Adds the global variable to the list of global variables.
    /// </summary>
    /// <param name="statements">
    /// The <see cref="BlazorBasicCodeStatementsTable"/> containing the list of code statements.
    /// </param>
    /// <param name="currentIndex">
    /// An integer specifying the ordinal index of the current code line.
    /// </param>
    /// <returns>
    /// An integer specifying the next index of the list of code lines to return to.
    /// </returns>
    private int AddGlobalVariable(BlazorBasicCodeStatementsTable statements, int currentIndex)
    {
        int returnIndex = currentIndex + 1;

        if (_globals == null)
            throw new BasicEngineExecutionException(0, "Global variable table not initialized.");
                
        BasicVariableDeclarationStatement? declareStatement = statements[currentIndex] as BasicVariableDeclarationStatement;
        if (declareStatement != null)
        {
            int lineNo = declareStatement.LineNumber;
            if (declareStatement.DataType == null)
            {
                throw new BasicBadDataTypeException(lineNo, "Invalid or missing data type in variable declaration.");
            }
            if (string.IsNullOrEmpty(declareStatement.VariableReference!.VariableName))
                throw new BasicSyntaxErrorException(lineNo, "Syntax error.");

            IVariable variable = new BasicVariable(
                declareStatement.VariableReference.VariableName,
                declareStatement.DataType.DataType,
                declareStatement.DataType.IsArray,
                declareStatement.DataType.Size);
            _globals.Add(variable);
        }

        return currentIndex + 1;
    }
    #endregion


}