using Adaptive.Intelligence.BlazorBasic;
using Adaptive.Intelligence.BlazorBasic.LanguageService;
using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using System.Text;

namespace Test_Console;

internal class Program
{
    static string file = @"C:\Temp\PriceDat.bas";

    static void Main(string[] args)
    {
        BlazorBasicProviderService providerService = new BlazorBasicProviderService(
             new BlazorBasicDataTypeProvider(),
        new BlazorBasicDelimiterProvider(),
        new BlazorBasicErrorProvider(),
        new BlazorBasicFunctionProvider(),
        new BlazorBasicKeywordProvider(),
        new BlazorBasicOperatorProvider());

        BlazorBasicLanguageService service = new BlazorBasicLanguageService(providerService);
        ParserOutputLogger logger = new ParserOutputLogger();
        BlazorBasicParsingService parsingService = new BlazorBasicParsingService(
            service,
            new BlazorBasicParserWorker(service, logger, new BlazorBasicTokenFactory(service)),
            logger);


        FileStream sourceStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        List<ILanguageCodeStatement> finalList = parsingService.ParseCodeContent(sourceStream);
        sourceStream.Close();
        sourceStream.Dispose();

        BlazorBasicStatementRenderer renderer = new BlazorBasicStatementRenderer();
        renderer.UseTabs = true;

        foreach (ILanguageCodeStatement statement in finalList)
        {
            Console.WriteLine(renderer.RenderStatement(statement));
        }

        Console.WriteLine("");
        Console.WriteLine("Press [Enter] To Exit");
        Console.ReadLine();
    }
}
