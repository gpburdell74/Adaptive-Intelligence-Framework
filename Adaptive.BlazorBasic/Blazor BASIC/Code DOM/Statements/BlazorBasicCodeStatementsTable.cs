using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.CodeDom.Statements;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the methods and functions for containing and querying a list of <see cref="BasicCodeStatement"/> instances.
/// </summary>
/// <seealso cref="List{T}" />
/// <see cref="BasicCodeStatement"/>
public class BlazorBasicCodeStatementsTable : List<BasicCodeStatement>, ICodeStatementsTable
{
    public BlazorBasicCodeStatementsTable() : base()
    {

    }
    public BlazorBasicCodeStatementsTable(int capacity) : base(capacity)
    {

    }
    public BlazorBasicCodeStatementsTable(IEnumerable<BasicCodeStatement> statementList)
    {
        AddRange(statementList);
    }

    public void Add(ICodeStatement statement)
    {
        Add((BasicCodeStatement)statement);
    }
    public void Add(BasicCodeStatement statement)
    {
        base.Add(statement);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public BasicProcedureStartStatement? FindProcedureDefinition(string name)
    {
        BasicProcedureStartStatement? definition = null;

        foreach (BasicCodeStatement codeStatement in this)
        {
            if (codeStatement is BasicProcedureStartStatement procStartStatement)
            {
                if (procStartStatement.ProcedureName == name)
                    definition = procStartStatement;
            }
        }
        return definition;
    }
}
