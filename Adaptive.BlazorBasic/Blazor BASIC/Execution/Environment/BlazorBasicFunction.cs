using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

/// <summary>
/// 
/// </summary>
/// <seealso cref="Adaptive.Intelligence.Shared.DisposableObjectBase" />
/// <seealso cref="IFunction" />
public class BlazorBasicFunction : DisposableObjectBase, IFunction
{
    private List<ILanguageCodeStatement> _codeList;
    private string? _name;
    private int _id;

    private BlazorBasicVariableTable? _variables;

    public BlazorBasicFunction(int newId)
    {
        _id = newId;
        _variables = new BlazorBasicVariableTable(this);
    }
    public BlazorBasicFunction(int newId, List<ILanguageCodeStatement> code)
    {
        _codeList = new List<ILanguageCodeStatement>();
        _codeList.AddRange(code);

        _variables = new BlazorBasicVariableTable(this);

        BasicFunctionStartStatement start = _codeList[0] as BasicFunctionStartStatement;
        _name = start.FunctionName;
        _id = newId;
    }

    public List<ILanguageCodeStatement> Code => _codeList;
    public List<ICodeExpression> Parameters { get; }
    public string? Name => _name;
    public IVariableTable Variables { get; }
    public int Id => _id;

    public Type ReturnType { get; }

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

    public override string ToString()
    {
        return "FUNCTION " + _name;
    }

    public bool VariableExists(string? variableName)
    {
        return _variables.Exists(variableName);

    }
}
