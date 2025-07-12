using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.LanguageService.Services;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

/// <summary>
/// Represents a Blazor BASIC procedure definition and instance.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IProcedure" />
public class BasicProcedure : DisposableObjectBase, IProcedure
{
    #region Private Member Declarations
    /// <summary>
    /// The code list.
    /// </summary>
    private BasicCodeStatementList? _codeList;

    /// <summary>
    /// The procedure name.
    /// </summary>
    private string? _name;

    /// <summary>
    /// The parameters table.
    /// </summary>
    private IParameterTable? _parameters;

    /// <summary>
    /// The parent scope reference.
    /// </summary>
    private IScopeContainer? _parent;

    /// <summary>
    /// The variables table.
    /// </summary>
    private IVariableTable? _variables;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedure"/> class.
    /// </summary>
    /// <param name="parent">
    /// The reference to the <see cref="IScopeContainer"/> instance acting as the parent
    /// scope container.
    /// </param>
    public BasicProcedure(IScopeContainer parent)
    {
        _variables = new BasicVariableTable(this);
        _codeList = new BasicCodeStatementList();
        _parameters = new BlazorBasicParameterTable(parent);
        _parent = parent;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedure"/> class.
    /// </summary>
    /// <param name="code">
    /// A <see cref="List{T}"/> of <see cref="ICodeStatement"/> instances containing the 
    /// code for the procedure definition.
    /// The code.</param>
    /// <param name="parent">
    /// The reference to the <see cref="IScopeContainer"/> instance acting as the parent
    /// scope container.
    /// </param>
    public BasicProcedure(List<ICodeStatement> code, IScopeContainer parent)
    {
        _codeList = new BasicCodeStatementList();
        _codeList.AddRange(code);

        _variables = new BasicVariableTable(this);

        // The first statement in the code list will be the procedure definition.
        BasicProcedureStartStatement? start = _codeList[0] as BasicProcedureStartStatement;
        if (start != null)
            _name = start.ProcedureName;

        _parent = parent;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicProcedure"/> class.
    /// </summary>
    /// <param name="code">
    /// A <see cref="List{T}"/> of <see cref="ICodeStatement"/> instances containing the 
    /// code for the procedure definition.
    /// The code.</param>
    /// <param name="parent">
    /// The reference to the <see cref="IScopeContainer"/> instance acting as the parent
    /// scope container.
    /// </param>
    /// <param name="procedureName">
    /// A string containing the name of the procedure.
    /// </param>
    /// <param name="parameters">
    /// A <see cref="List{T}"/> of <see cref="BasicParameterDefinitionExpression"/> defining the list of expressions.
    /// </param>
    public BasicProcedure(List<ICodeStatement> code, IScopeContainer parent, string procedureName,
        List<BasicParameterDefinitionExpression>? parameters)
    {
        _codeList = new BasicCodeStatementList();
        _codeList.AddRange(code);

        _variables = new BasicVariableTable(this);

        // The first statement in the code list will be the procedure definition.
        BasicProcedureStartStatement? start = _codeList[0] as BasicProcedureStartStatement;
        _name = procedureName;
        _parameters = new BlazorBasicParameterTable(parent);

        if (parameters != null)
        {
            foreach (BasicParameterDefinitionExpression paramExpression in parameters)
            {
                if (paramExpression.ParameterName == null || paramExpression.DataType == null)
                    throw new BasicSyntaxErrorException(start.LineNumber, "Syntax error in procedure definition.");

                _parameters.Add(this, paramExpression.ParameterName, paramExpression.DataType.DataType);
            }
        }

        _parent = parent;
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
            _codeList?.Clear();
            _variables?.Dispose();
        }

        _codeList = null;
        _variables = null;
        _name = null;
        _parent = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the reference to the list of CodeDOM statements to be executed.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="ICodeStatement"/> instances.
    /// </value>
    public List<ICodeStatement>? Code => _codeList;

    /// <summary>
    /// Gets the name of the procedure.
    /// </summary>
    /// <value>
    /// A string containing the procedure name.
    /// </value>
    public string? Name => _name;

    /// <summary>
    /// Gets the reference to the list of parameters.
    /// </summary>
    /// <value>
    /// The <see cref="IParameterTable"/> containing the parameter 
    /// definitions.
    /// </value>
    public IParameterTable? Parameters => _parameters;

    /// <summary>
    /// Gets the reference to the parent scope container.
    /// </summary>
    /// <value>
    /// The <see cref="IScopeContainer"/> defining the scope in which the procedure is contained.
    /// /// </value>
    public IScopeContainer? Parent => _parent;

    /// <summary>
    /// Gets the reference to the list of instantiated variables in the procedure.
    /// </summary>
    /// <value>
    /// An <see cref="IVariableTable"/> containing the list of variables.
    /// </value>
    public IVariableTable? Variables => _variables;
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Creates the variable within the scope from its specified parameter definition.
    /// </summary>
    /// <param name="lineNumber">An integer indicating the line number of the code line being executed.</param>
    /// <param name="variableName">A string containing the name of the variable.</param>
    /// <param name="dataType">A <see cref="StandardDataTypes" /> enumerated value indicating the data type.
    /// Type of the data.</param>
    /// <param name="isArray"><b>true</b> if the data type is an array.</param>
    /// <param name="size">If <paramref name="isArray" /> is true, specifies the size of the array.</param>
    public void CreateParameterVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size)
    {
        if (_variables == null)
            throw new BasicEngineExecutionException(lineNumber, "Internal variable table not initialized for: " + _name);

        if (dataType == StandardDataTypes.Unknown)
            throw new BasicBadDataTypeException(lineNumber, variableName);

        BasicVariable? variable = BasicVariableFactory.CreateByType(variableName, dataType, isArray, size);
        if (variable != null)
        {
            variable.IsParameter = true;
            _variables.Add(variable);
        }
    }

    /// <summary>
    /// Creates the variable within the scope.
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
    /// <exception cref="BasicEngineExecutionException">
    /// Occurs when an internal C# exception state is entered.
    /// </exception>
    /// <exception cref="BasicBadDataTypeException">
    /// Thrown when the data type of the variable is unknown or invalid.
    /// </exception>
    public void CreateVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size)
    {
        if (_variables == null)
            throw new BasicEngineExecutionException(lineNumber, "Internal variable table not initialized for: " + _name);

        if (dataType == StandardDataTypes.Unknown)
            throw new BasicBadDataTypeException(lineNumber, variableName);

        BasicVariable? variable = BasicVariableFactory.CreateByType(variableName, dataType, isArray, size);
        if (variable != null)
            _variables.Add(variable);
    }

    /// <summary>
    /// Executes the code elements within the context of the procedure.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="engine">
    /// The reference to the <see cref="IExecutionEngine"/>.
    /// </param>
    /// <param name="environment">
    /// The reference to the <see cref="IExecutionEnvironment"/> instance.
    /// </param>
    public void Execute(int lineNumber, IExecutionEngine engine, IExecutionEnvironment environment)
    {
        int length = _codeList.Count;
        
        for(int index = 0; index < length; index++)
        {
            ICodeStatement statement = _codeList[index];
            engine.Executor.ExecuteStatement(this, statement);
        }
    }

    /// <summary>
    /// Gets the reference to the variable instance with the specified name.
    /// </summary>
    /// <param name="variableName">
    /// A string containing the name of the variable to find.
    /// </param>
    /// <returns>
    /// The <see cref="IVariable"/> instance, if found.
    /// </returns>
    public IVariable? GetVariable(string variableName)
    {
        if (_variables == null)
            return null;

        return _variables.GetVariable(variableName);
    }

    /// <summary>
    /// Instantiates a new variable within the local scope to represent a
    /// parameter and its value.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="variableName">
    /// A string containing the name of the variable.
    /// </param>
    /// <param name="dataType">
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the type of the data.
    /// </param>
    /// <param name="isArray">
    /// <b>true</b> if the variable represents an array.
    /// </param>
    /// <param name="size">
    /// An integer specifying the size of the array.
    /// </param>
    public void InstantiateParameterVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size)
    {
    }


    /// <summary>
    /// Instantiates a new variable within the local scope.
    /// </summary>
    /// <param name="lineNumber">
    /// An integer specifying the current line number.
    /// </param>
    /// <param name="variableName">
    /// A string containing the name of the variable.
    /// </param>
    /// <param name="dataType">
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the type of the data.
    /// </param>
    /// <param name="isArray">
    /// <b>true</b> if the variable represents an array.
    /// </param>
    /// <param name="size">
    /// An integer specifying the size of the array.
    /// </param>
    public void InstantiateVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size)
    {
    }

    /// <summary>
    /// Returns the string representation of the current instance.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return "PROCEDURE " + _name;
    }

    /// <summary>
    /// Gets a value indicating whether a variable with the specified name exists within this
    /// scope.
    /// </summary>
    /// <param name="variableName">A string containing the name of the variable to check for.</param>
    /// <returns>
    ///   <b>true</b> if the variable with the specified name exists; otherwise, <b>false</b>.
    /// </returns>
    public bool VariableExists(string? variableName)
    {
        if (_variables == null)
            return false;

        return _variables.Exists(variableName);
    }

    #endregion
}