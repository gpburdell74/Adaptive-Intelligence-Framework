using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Console;

namespace Adaptive.Intelligence.BlazorBasic;

public class BlazorBasicConsole : DisposableObjectBase
{
    private QBConsole _console;

    public BlazorBasicConsole()
    {
        _console = new QBConsole();
    }
    protected override void Dispose(bool disposing)
    {
        _console = null;
        base.Dispose(disposing);
    }

    public void Color(int foreground, int background)
    {
        _console.Color((ConsoleColor)foreground, (ConsoleColor)background);
    }

    public void Cls()
    {
        QBConsole.Cls();
    }

    public void PrintLine(string text)
    {
        QBConsole.Print(text);
        System.Console.WriteLine();

    }
}
