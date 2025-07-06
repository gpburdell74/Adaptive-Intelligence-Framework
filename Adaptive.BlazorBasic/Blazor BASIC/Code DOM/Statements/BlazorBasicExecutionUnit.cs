using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a single set of code lines, aka. a "program".
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public class BlazorBasicExecutionUnit : DisposableObjectBase, IExecutionUnit
{
    private BlazorBasicCodeStatementsTable _statements;

    public BlazorBasicExecutionUnit()
    {
        _statements = new BlazorBasicCodeStatementsTable();
    }
    public BlazorBasicExecutionUnit(BlazorBasicCodeStatementsTable statements)
    {
        _statements = new BlazorBasicCodeStatementsTable();
        _statements.AddRange(statements);
    }
    protected override void Dispose(bool disposing)
    {
        _statements?.Clear();

        _statements = null;
        base.Dispose(disposing);
    }
    public BlazorBasicCodeStatementsTable Statements => _statements;
    ILanguageCodeStatementsTable? IExecutionUnit.Statements => _statements;
}
