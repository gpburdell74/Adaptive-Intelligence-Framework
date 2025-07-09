using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
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
public class BlazorBasicProcedure : DisposableObjectBase, IProcedure
{
    private BasicCodeStatementList _codeList;
    private string? _name;
    private int _id;

    private BlazorBasicVariableTable? _variables;

    public BlazorBasicProcedure(int newId)
    {
        _id = newId;
        _variables = new BlazorBasicVariableTable(this);
    }
    public BlazorBasicProcedure(int newId, List<ICodeStatement> code)
    {
        _codeList = new BasicCodeStatementList();
        _codeList.AddRange(code);

        _variables = new BlazorBasicVariableTable(this);

        BasicProcedureStartStatement start = _codeList[0] as BasicProcedureStartStatement;
        _name = start.ProcedureName;
        _id = newId;
    }

    public BasicCodeStatementList Code => _codeList;
    List<ICodeStatement> IScopeContainer.Code => _codeList;

    public List<ICodeExpression> Parameters { get; }
    public string? Name => _name;
    public IVariableTable Variables { get; }
    public int Id => _id;

    IParameterTable IExecutableContext.Parameters { get; }

    public void CreateVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size)
    {
        if (dataType == StandardDataTypes.Unknown)
            throw new BasicBadDataTypeException(lineNumber, variableName);

        BlazorBasicVariable variable = BasicVariableFactory.CreateByType(variableName, dataType, isArray, size);
        _variables.Add(variable);
    }
    public void CreateParameterVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size)
    {
        if (dataType == StandardDataTypes.Unknown)
            throw new BasicBadDataTypeException(lineNumber, variableName);

        BlazorBasicVariable variable = BasicVariableFactory.CreateByType(variableName, dataType, isArray, size);
        variable.IsParameter = true;
        _variables.Add(variable);
    }

    public override string ToString()
    {
        return "PROCEDURE " + _name;
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
    public IVariable GetVariable(string variableName)
    {
        return _variables.GetVariableByName(variableName);
    }
    public bool VariableExists(string? variableName)
    {
        return _variables.Exists(variableName);
    }

    public void Execute(ILanguageService service, IExecutionEngine engine, IExecutionEnvironment environment)
    {
        throw new NotImplementedException();
    }

    public void InstantiateVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size)
    {
        throw new NotImplementedException();
    }

    public void InstantiateParameterVariable(int lineNumber, string variableName, StandardDataTypes dataType, bool isArray, int size)
    {
        throw new NotImplementedException();
    }
}
