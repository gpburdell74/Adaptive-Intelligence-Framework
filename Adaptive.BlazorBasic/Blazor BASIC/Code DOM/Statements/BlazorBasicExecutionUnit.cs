using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a single set of code lines, aka. a "program".
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public class BlazorBasicExecutionUnit : DisposableObjectBase, IExecutionUnit
{
    private List<ILanguageCodeStatement> _statements;

    public BlazorBasicExecutionUnit()
    {
        _statements = new List<ILanguageCodeStatement>();
    }
    public BlazorBasicExecutionUnit(List<ILanguageCodeStatement> statements)
    {
        _statements = new List<ILanguageCodeStatement>();
        _statements.AddRange(statements);
    }
    protected override void Dispose(bool disposing)
    {
        _statements?.Clear();

        _statements = null;
        base.Dispose(disposing);
    }
    public List<ILanguageCodeStatement> Statements => _statements;
}
