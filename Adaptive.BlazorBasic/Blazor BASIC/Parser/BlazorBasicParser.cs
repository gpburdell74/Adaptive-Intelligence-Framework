using Adaptive.BlazorBasic.LanguageService;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides the parsing engine for the Blazor Basic language.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ISourceCodeParser" />
/// <seealso cref="ISourceCodeParser" />
public class BlazorBasicParser : DisposableObjectBase, ISourceCodeParser
{
    #region Private Member Declarations    
    /// <summary>
    /// The language service instance.
    /// </summary>
    private ILanguageService<BlazorBasicFunctions, BlazorBasicKeywords>? _service;

    /// <summary>
    /// The worker instance.
    /// </summary>
    private ICodeParserWorker? _worker;

    /// <summary>
    /// The user-defined-items reference table.
    /// </summary>
    private IUserReferenceTable? _userRefTable;

    /// <summary>
    /// The logger instance.
    /// </summary>
    private IParserOutputLogger? _log;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParser"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="ILanguageService{FunctionsEnum, KeywordsEnum}"/> service instance.
    /// </param>
    /// <param name="worker">
    /// The reference to the <see cref="ICodeParserWorker"/> worker instance.
    /// </param>
    /// <param name="logger">
    /// The reference to the <see cref="IParserOutputLogger"/> logger instance.
    /// </param>
    public BlazorBasicParser(ILanguageService<BlazorBasicFunctions, BlazorBasicKeywords> service,
                             ICodeParserWorker worker,
                             IParserOutputLogger logger)
    {
        _service = service;
        _worker = worker;
        _log = logger;
        _log.WriteLine(nameof(BlazorBasicParser) + " Created.");
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _log?.WriteLine(nameof(BlazorBasicParser) + " Disposed.");

        if (!IsDisposed && disposing)
            _userRefTable?.Dispose();

        _log = null;
        _userRefTable = null;
        _service = null;
        _worker = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the reference to the logging instance.
    /// </summary>
    /// <value>
    /// The <see cref="IParserOutputLogger" /> instance used during the parsing process.
    /// </value>
    public IParserOutputLogger? Logger => _log;
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="rawText">
    /// A string containing the entire raw text to be parsed.  Each line must be separated by a carriage-return/newline pair.
    /// </param>
    /// <returns></returns>
    public List<object> ParseCodeContent(string rawText)
    {
        _log?.WriteLine(nameof(ParseCodeContent) + "(string)");

        string[] lines = rawText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        return ParseCodeContent(lines);
    }

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="rawText">An <see cref="IEnumerable{T}" /> of strings containing the complete list of source code to be parsed.</param>
    /// <returns></returns>
    public List<object> ParseCodeContent(IEnumerable<string> rawText)
    {
        _log?.WriteLine(nameof(ParseCodeContent) + "(IEnumerable<string>)");

        if (_worker == null)
            throw new InvalidOperationException();

        // Pre-process each line of text to remove any invalid whitespace.
        List<string> preProcessedList = new List<string>();
        foreach (string rawCodeLine in rawText)
        {
            string? preProcessedLine = _worker.PreProcess(rawCodeLine);
            if (preProcessedLine != null)
                preProcessedList.Add(preProcessedLine);
        }

        // Translate the code lines into tokens.
        List<ITokenizedCodeLine>? tokensList = _worker.TokenizeCodeLines(preProcessedList);
        preProcessedList.Clear();


        return ParseCodeContent(tokensList);
    }

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="sourceStream">An open <see cref="Stream" /> to read the complete list of source code to be parsed</param>
    /// <returns></returns>
    public List<object> ParseCodeContent(Stream sourceStream)
    {
        List<object>? parsedContentList = null;

        _log?.WriteLine(nameof(ParseCodeContent) + "(Stream)");
        if (_worker == null)
            throw new InvalidOperationException();

        List<string>? preProcessed = _worker.PreProcessStream(sourceStream);
        if (preProcessed != null)
        {
            List<ITokenizedCodeLine>? tokensList = _worker.TokenizeCodeLines(preProcessed);
            preProcessed.Clear();
            if (tokensList != null)
                parsedContentList = ParseCodeContent(tokensList);
            else
                parsedContentList = new List<object>();
        }
        else
        {
            parsedContentList = new List<object>();
        }

        return parsedContentList;
    }

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="tokenizedCodeLines">An <see cref="List{T}" /> of <see cref="ITokenizedCodeLine" /> instances containing the tokenized list of code items.</param>
    /// <returns></returns>
    public List<object> ParseCodeContent(List<ITokenizedCodeLine> tokenizedCodeLines)
    {
        _log?.WriteLine(nameof(ParseCodeContent) + "(List<ITokenizedCodeLines>)");

        if (_worker == null)
            throw new InvalidOperationException();

        // Find the user-defined names for procedures, functions, and variables.
        _userRefTable = _worker.FindUserDeclarations(tokenizedCodeLines);

        // Generate the CodeDOM for each line of code.
        _worker.CreateCodeStatements(tokenizedCodeLines);

        // Return the results.
        return new List<object>();
    }
    #endregion

}
