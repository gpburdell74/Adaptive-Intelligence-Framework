using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.BlazorBasic.Parser;
using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.CodeDom.Statements;
using Adaptive.Intelligence.LanguageService.Dictionaries;
using Adaptive.Intelligence.LanguageService.Parsing;
using Adaptive.Intelligence.LanguageService.Services;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.Services;

/// <summary>
/// Provides the code parsing service for the Blazor BASIC language.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IParsingService{D,E,F,K,O}" />
public sealed class BlazorBasicParsingService : DisposableObjectBase,
    IParsingService<
            BlazorBasicDelimiters,
            BlazorBasicErrorCodes,
            BlazorBasicFunctions,
            BlazorBasicKeywords,
            StandardOperators>
{
    #region Private Member Declarations    
    /// <summary>
    /// The language service instance.
    /// </summary>
    private BlazorBasicLanguageService? _service;

    /// <summary>
    /// The parser worker / helper instance.
    /// </summary>
    private BlazorBasicParserWorker? _worker;

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
    /// Initializes a new instance of the <see cref="BlazorBasicParsingService"/> class.
    /// </summary>
    /// <param name="service">
    /// The reference to the <see cref="ILanguageService"/> service instance.
    /// </param>
    /// <param name="worker">
    /// The reference to the <see cref="ICodeParserWorker"/> worker instance.
    /// </param>
    /// <param name="logger">
    /// The reference to the <see cref="IParserOutputLogger"/> logger instance.
    /// </param>
    public BlazorBasicParsingService(BlazorBasicLanguageService service,
                             BlazorBasicParserWorker worker,
                             IParserOutputLogger logger)
    {
        _service = service;
        _worker = worker;
        _log = logger;
        _log.WriteLine(nameof(ILanguageService) + " Created.");
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _log?.WriteLine(nameof(BlazorBasicParsingService) + " Disposed.");

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

    /// <summary>
    /// Gets the reference to the language service used during the parsing operations.
    /// </summary>
    /// <value>
    /// The <see cref="ILanguageService{D,E,F,K,O}" /> instance.
    /// </value>
    public ILanguageService<
        BlazorBasicDelimiters,
        BlazorBasicErrorCodes,
        BlazorBasicFunctions,
        BlazorBasicKeywords,
        StandardOperators>? Service => _service;
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="rawText">
    /// A string containing the entire raw text to be parsed.  Each line must be separated by a carriage-return/newline pair.
    /// </param>
    /// <returns></returns>
    public ICodeStatementsTable ParseCodeContent(string rawText)
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
    public ICodeStatementsTable ParseCodeContent(IEnumerable<string> rawText)
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
        List<ITokenizedCodeLine>? tokensList = _service.TokenFactory.TokenizeCodeLines(preProcessedList);
        preProcessedList.Clear();

        return (BlazorBasicCodeStatementsTable)ParseCodeContent(tokensList);
    }

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="sourceStream">An open <see cref="Stream" /> to read the complete list of source code to be parsed</param>
    /// <returns></returns>
    public ICodeInterpreterUnit? ParseCodeContent(Stream sourceStream)
    {
        BlazorBasicCodeStatementsTable? parsedContentList = null;

        _log?.WriteLine(nameof(ParseCodeContent) + "(Stream)");
        if (_worker == null)
            throw new InvalidOperationException();

        List<string>? preProcessed = _worker.PreProcessStream(sourceStream);
        if (preProcessed != null)
        {
            List<ITokenizedCodeLine>? tokensList = _service.TokenFactory.TokenizeCodeLines(preProcessed);
            preProcessed.Clear();
            if (tokensList != null)
                parsedContentList = (BlazorBasicCodeStatementsTable)ParseCodeContent(tokensList);
            else
                parsedContentList = new BlazorBasicCodeStatementsTable();
        }
        else
        {
            parsedContentList = new BlazorBasicCodeStatementsTable();
        }

        return new BlazorBasicExecutionUnit(parsedContentList);
    }

    /// <summary>
    /// Parses the content of the code.
    /// </summary>
    /// <param name="tokenizedCodeLines">An <see cref="List{T}" /> of <see cref="ITokenizedCodeLine" /> instances containing the tokenized list of code items.</param>
    /// <returns></returns>
    public ICodeStatementsTable ParseCodeContent(List<ITokenizedCodeLine> tokenizedCodeLines)
    {
        _log?.WriteLine(nameof(ParseCodeContent) + "(List<ITokenizedCodeLines>)");

        if (_worker == null)
            throw new InvalidOperationException();

        // Find the user-defined names for procedures, functions, and variables.
        _userRefTable = _worker.FindUserDeclarations(tokenizedCodeLines);

        // Generate the CodeDOM for each line of code.
        BlazorBasicCodeStatementsTable statementList = _worker.CreateCodeStatements((UserReferenceTable)_userRefTable, tokenizedCodeLines);

        // Ensure for all function and procedure calls, the parameter values can be expressed and thus
        // assigned correctly.
        AdjustParameterValueDefinitions(statementList);

        return statementList;
    }
    #endregion

    private void AdjustParameterValueDefinitions(BlazorBasicCodeStatementsTable statementList)
    {
        foreach (ICodeStatement statement in statementList)
        {
            if (statement is BasicProcedureCallStatement procCallStatement)
            {
                CorrelateParametersAndVariableExpressions(statementList, procCallStatement);
            }
        }
    }

    private void CorrelateParametersAndVariableExpressions(BlazorBasicCodeStatementsTable statementList, BasicProcedureCallStatement procCallStatement)
    {
        BasicProcedureStartStatement? definition = statementList.FindProcedureDefinition(procCallStatement.ProcedureName);
        if (definition == null)
        {

        }
        else
        {
            if (definition.ParameterCount != procCallStatement.Parameters.Count)
            {
                // Invalid argument list.
            }
            else
            {
                int index = 0;
                int length = definition.ParameterCount;

                for (index = 0; index < length; index++)
                {
                    BlazorBasicParameterValueExpression valueExp =
                        procCallStatement.Parameters[index];

                    BasicParameterDefinitionExpression? paramDef =
                        definition.Parameters.ParameterList[index];

                    valueExp.ParameterName = paramDef.ParameterName;
                    valueExp.DataType = paramDef.DataType;

                    if (valueExp.OriginalData is string nameTest)
                    {
                        if (_userRefTable.IsVariable(nameTest))
                        {
                        }
                        else if (_userRefTable.IsFunction(nameTest))
                        {

                        }
                        else if (_userRefTable.IsFunction(nameTest))
                        {
                        }
                    }
                }
            }
        }
    }
}