#define WINDOWS 
//#define MACOS 

using Adaptive.Intelligence.BlazorBasic;
using Adaptive.Intelligence.BlazorBasic.Execution;
using Adaptive.Intelligence.BlazorBasic.LanguageService;
using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;

namespace Test_Console;

internal class Program
{
#if WINDOWS
    static string file = @"C:\Temp\PriceDat.bas";
#elif MACOS
    static string file = @"/users/samjones/Documents/PriceDat.bas";
#endif

    static void Main(string[] args)
    {
        BlazorBasicProviderService providerService = new BlazorBasicProviderService(
             new BasicDataTypeProvider(),
        new BasicDelimiterProvider(),
        new BasicErrorProvider(),
        new BasicFunctionProvider(),
        new BasicKeywordProvider(),
        new BasicOperatorProvider());

        BlazorBasicLanguageService service = new BlazorBasicLanguageService(providerService);
        ParserOutputLogger logger = new ParserOutputLogger();
        BlazorBasicParsingService parsingService = new BlazorBasicParsingService(
            service,
            new BlazorBasicParserWorker(service, logger, new BlazorBasicTokenFactory(service)),
            logger);

        BlazorBasicConsole console = new BlazorBasicConsole();

        FileStream sourceStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        ICodeInterpreterUnit execUnit = parsingService.ParseCodeContent(sourceStream);
        sourceStream.Close();
        sourceStream.Dispose();

        BasicVirtualSystem system = new BasicVirtualSystem();


        BasicExecutionEnvironment env = new BasicExecutionEnvironment(
            new BasicDataTypeProvider(),
            new BasicIdGenerator(),
            new BasicFunctionTable(null),
            new BlazorBasicProcedureTable(null),
            new BasicVariableTable(null),
            console,
            system);

        BlazorBasicExecutionEngine engine = new BlazorBasicExecutionEngine(env);

        env.LoadUnit(execUnit);

        engine.Execute();
        
        Console.WriteLine("");
        Console.WriteLine("Press [Enter] To Exit");
        Console.ReadLine();
    }
}
