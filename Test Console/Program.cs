using Adaptive.BlazorBasic;
using Adaptive.BlazorBasic.LanguageService;

namespace Test_Console;

internal class Program
{
    static string file = @"C:\Temp\PriceDat.bas";

    static void Main(string[] args)
    {
        BlazorBasicLanguageService service = new BlazorBasicLanguageService();
        ISourceCodeParser parser = service.GetParser();

        FileStream sourceStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        List<object> finalList = parser.ParseCodeContent(sourceStream);
        sourceStream.Close();
        sourceStream.Dispose();

        Console.WriteLine("");
        Console.WriteLine("Press [Enter] To Exit");
        Console.ReadLine();
    }
}
