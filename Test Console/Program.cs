using Adaptive.BlazorBasic.LanguageService;
using Adaptive.BlazorBasic.Parser;
using Adaptive.BlazorBasic.Services;
using Adaptive.LanguageService.Providers;

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
        new BasicFunctionProvider(),
        new BlazorBasicKeywordProvider(),
        new OperatorProvider(
            new AssignmentOperatorProvider(),
            new BitwiseOperatorProvider(),
            new ComparisonOperatorProvider(),
            new LogicalOperatorProvider(),
            new MathOperatorProvider(),
            new OperationalOperatorProvider()));

        BlazorBasicLanguageService service = new BlazorBasicLanguageService(providerService);
        ParserOutputLogger logger = new ParserOutputLogger();
        BlazorBasicParsingService parsingService = new BlazorBasicParsingService(
            service,
            new BlazorBasicParserWorker(service, logger, new BlazorBasicTokenFactory(service)),
            logger);


        FileStream sourceStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        List<object> finalList = parsingService.ParseCodeContent(sourceStream);
        sourceStream.Close();
        sourceStream.Dispose();

        Console.WriteLine("");
        Console.WriteLine("Press [Enter] To Exit");
        Console.ReadLine();
    }
}
