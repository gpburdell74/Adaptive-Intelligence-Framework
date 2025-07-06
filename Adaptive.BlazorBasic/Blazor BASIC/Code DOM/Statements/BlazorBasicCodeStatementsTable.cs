using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the methods and functions for containing and querying a list of <see cref="BasicCodeStatement"/> instances.
/// </summary>
/// <seealso cref="List{T}" />
/// <see cref="BasicCodeStatement"/>
public class BlazorBasicCodeStatementsTable : List<BasicCodeStatement>, ILanguageCodeStatementsTable
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

    public void Add(ILanguageCodeStatement statement)
    {
        Add((BasicCodeStatement)statement);
    }
    public void Add(BasicCodeStatement statement)
    {
        base.Add(statement);
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
