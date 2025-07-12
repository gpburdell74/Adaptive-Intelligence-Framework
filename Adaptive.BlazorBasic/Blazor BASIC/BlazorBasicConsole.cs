using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Console;
using System.Diagnostics;

namespace Adaptive.Intelligence.BlazorBasic;

public class BlazorBasicConsole : DisposableObjectBase, IStandardOutput
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

    public void Locate(int x, int y)
    {
        QBConsole.Locate(x, y);
    }

    public void Print(string text)
    {
        System.Console.Write(text);
    }
}
