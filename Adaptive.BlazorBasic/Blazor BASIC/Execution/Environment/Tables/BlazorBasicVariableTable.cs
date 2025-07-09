using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

public class BlazorBasicVariableTable : DisposableObjectBase, IVariableTable
{
    private Dictionary<int, IVariable> _variablesById;
    private Dictionary<string, IVariable> _variablesByName;

    public BlazorBasicVariableTable(IScopeContainer? parent)
    {
        Parent = parent;
        _variablesById = new Dictionary<int, IVariable>();
        _variablesByName = new Dictionary<string, IVariable>();
    }
    public int Count => _variablesById.Count;

    public IScopeContainer? Parent { get; }

    public void Add(BlazorBasicVariable function)
    {
        _variablesByName.Add(function.Name!, function);
    }
    public bool Exists(string name)
    {
        return _variablesByName.ContainsKey(name);
    }

    public IVariable? GetVariable(int id)
    {
        return _variablesById[id];
    }

    public IVariable? GetVariable(string? name)
    {
        throw new NotImplementedException();
    }

    public IVariable? GetVariableByName(string functionName)
    {
        return _variablesByName[functionName];
    }
}
