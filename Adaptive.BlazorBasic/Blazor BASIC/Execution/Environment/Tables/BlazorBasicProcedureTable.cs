using Adaptive.Intelligence.BlazorBasic.Execution;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

public class BlazorBasicProcedureTable : DisposableObjectBase, IProcedureTable
{
    private Dictionary<int, IProcedure> _procsById;
    private Dictionary<string, IProcedure> _procsByName;

    public BlazorBasicProcedureTable()
    {
        _procsById = new Dictionary<int, IProcedure>();
        _procsByName = new Dictionary<string, IProcedure>();
    }
    public int Count => _procsById.Count;

    public void Add(BlazorBasicProcedure procedure)
    {
        _procsById.Add(procedure.Id, procedure);
        _procsByName.Add(procedure.Name!, procedure);
    }

    public bool Exists(string name)
    {
        throw new NotImplementedException();
    }

    public IProcedure? GetProcedure(int id)
    {
        return _procsById[id];
    }

    public IProcedure? GetProcedure(string? procedureName)
    {
        throw new NotImplementedException();
    }

    public IProcedure? GetProcedureByName(string functionName)
    {
        return _procsByName[functionName];
    }
}
