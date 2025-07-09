using Adaptive.Intelligence.BlazorBasic.Execution;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

public class BlazorBasicFunctionTable : DisposableObjectBase, IFunctionTable
{
    private Dictionary<int, IFunction> _functionsById;
    private Dictionary<string, IFunction> _functionsByName;

    public BlazorBasicFunctionTable()
    {
        _functionsById = new Dictionary<int, IFunction>();
        _functionsByName = new Dictionary<string, IFunction>();
    }
    public int Count => _functionsById.Count;
    public void Add(BlazorBasicFunction function)
    {
        _functionsById.Add(function.Id, function);
        _functionsByName.Add(function.Name!, function);
    }
    public bool Exists(string name)
    {
        return _functionsByName.ContainsKey(name);
    }
    public IFunction? GetFunction(int id)
    {
        return _functionsById[id];
    }

    public IFunction? GetFunction(string? functionName)
    {
        throw new NotImplementedException();
    }

    public IFunction? GetFunctionByName(string functionName)
    {
        return _functionsByName[functionName];
    }
}
