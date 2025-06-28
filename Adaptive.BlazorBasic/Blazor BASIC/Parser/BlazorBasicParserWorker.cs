using Adaptive.Intelligence.BlazorBasic.CodeDom;
using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Dictionaries;
using Adaptive.Intelligence.LanguageService.Parsing;
using Adaptive.Intelligence.LanguageService.Tokenization;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.Parser;

/// <summary>
/// Provides the parsing methods and functions for the Blazor BASIC language.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ICodeParserWorker" />
public class BlazorBasicParserWorker : DisposableObjectBase, ICodeParserWorker
{
    #region Private Member Declarations
    /// <summary>
    /// The language service instance.
    /// </summary>
    private BlazorBasicLanguageService? _service;

    /// <summary>
    /// The logger instance.
    /// </summary>
    private IParserOutputLogger? _log;

    /// <summary>
    /// The token factory instance.
    /// </summary>
    private BlazorBasicTokenFactory? _tokenFactory;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParserWorker"/> class.
    /// </summary>
    /// <param name="languageService">
    /// A reference to the <see cref="BlazorBasicLanguageService"/> language service instance.
    /// </param>
    /// <param name="logger">
    /// A reference to the <see cref="IParserOutputLogger"/> logging service instance.
    /// </param>
    /// <param name="factory">
    /// The reference to the <see cref="BlazorBasicTokenFactory"/> instance to use.
    /// </param>
    public BlazorBasicParserWorker(
            BlazorBasicLanguageService languageService, 
            IParserOutputLogger logger,
            BlazorBasicTokenFactory factory)
    {
        _service = languageService;
        _log = logger;
        _tokenFactory = factory;

        _log.WriteLine("BlazorBasicParserWorker initialized.");
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _log?.WriteLine("BlazorBasicParserWorker Disposed.");

        _log = null;
        _service = null;
        _tokenFactory = null;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates the code statements and expressions from the provided tokenized code lines.
    /// </summary>
    /// <param name="userReferences">
    /// A <see cref="UserReferenceTable"/> instance containing the user reference definitions.
    /// </param>
    /// <param name="tokenizedCodeLines">
    /// A <see cref="List{T}"/> of <see cref="ITokenizedCodeLine"/> instances containing the items to be 
    /// translated into code statements and expressions.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="ILanguageCodeStatement"/> instances containing the Code DOM for the line.
    /// </returns>
    public List<ILanguageCodeStatement> CreateCodeStatements(UserReferenceTable userReferences, List<ITokenizedCodeLine> tokenizedCodeLines)
    {
        List<ILanguageCodeStatement> list = new List<ILanguageCodeStatement>();

        if (_service != null)
        {
            int line = 0;
            int length = tokenizedCodeLines.Count;

            do
            {
                ITokenizedCodeLine codeLine = tokenizedCodeLines[line];
                PerformSubstitutions(userReferences, codeLine);
                ILanguageCodeStatement? statement = BasicStatementFactory.CreateStatementByCommand(_service, codeLine);

                if (statement != null)
                {
                    // Ensure we add the parameter definitions to the list of "variables".
                    if (statement is BasicFunctionStartStatement functionStart)
                    {
                        foreach (BlazorBasicParameterDefinitionExpression paramExpression in functionStart.Parameters.ParameterList)
                        {
                            userReferences.AddUserVariableDeclaration(functionStart.LineNumber,
                                paramExpression.ParameterName, null);
                        }
                    }
                    else if (statement is BasicProcedureStartStatement procedureStart)
                    {
                        if (procedureStart.Parameters != null)
                        {
                            foreach (BlazorBasicParameterDefinitionExpression paramExpression in procedureStart.Parameters.ParameterList)
                            {
                                userReferences.AddUserVariableDeclaration(procedureStart.LineNumber,
                                    paramExpression.ParameterName, null);
                            }
                        }
                    }
                    list.Add(statement);
                }

                line++;

            } while (line < length);
        }

        return list;
    }

    /// <summary>
    /// Performs any necessary pre-processing on the code line.
    /// </summary>
    /// <param name="codeLine">A string containing the original line of code.</param>
    /// <returns>
    /// A string containing the modified value.
    /// </returns>
    public string? PreProcess(string? codeLine)
    {
        string? modified = null;

        if (codeLine != null)
        {
            modified = codeLine
                      .Replace(ParseConstants.Tab , ParseConstants.Space)
                      .Replace("   ", ParseConstants.Space)
                      .Replace(ParseConstants.DoubleSpace, ParseConstants.Space)
                      .Trim();
        }
        return modified;
    }

    /// <summary>
    /// Pre-processes all the code lines in the stream.
    /// </summary>
    /// <param name="sourceStream">The source <see cref="Stream" /> to be read from.</param>
    /// <returns>
    /// A <see cref="List{T}" /> of strings containing the code lines, or <b>null</b> if the operation fails.
    /// </returns>
    public List<string>? PreProcessStream(Stream sourceStream)
    {
        List<string>? codeLines = null;

        StreamReader reader = new StreamReader(sourceStream);
        codeLines = new List<string>();
        string? rawLine = null;
        do
        {
            rawLine = reader.ReadLine();
            if (!string.IsNullOrEmpty(rawLine))
            {
                string? preProcessed = PreProcess(rawLine);
                if (preProcessed == null)
                    codeLines.Add(string.Empty);
                else
                    codeLines.Add(preProcessed);
            }
            else
            {
                codeLines.Add(string.Empty);
            }
        } while (rawLine != null);

        reader.Close();
        return codeLines;
    }

    /// <summary>
    /// Pre-processes all the code lines in the stream.
    /// </summary>
    /// <param name="sourceStream">The source <see cref="Stream" /> to be read from.</param>
    /// <returns>
    /// A <see cref="List{T}" /> of strings containing the code lines, or <b>null</b> if the operation fails.
    /// </returns>
    public async Task<List<string>?> PreProcessStreamAsync(Stream sourceStream)
    {
        List<string>? codeLines = null;

        StreamReader reader = new StreamReader(sourceStream);
        codeLines = new List<string>();
        string? rawLine = null;
        do
        {
            rawLine = await reader.ReadLineAsync().ConfigureAwait(false);
            if (!string.IsNullOrEmpty(rawLine))
            {
                string? preProcessed = PreProcess(rawLine);
                if (!string.IsNullOrEmpty(preProcessed))
                    codeLines.Add(preProcessed);
            }
        } while (rawLine != null);

        reader.Close();

        return codeLines;
    }

    /// <summary>
    /// Iterates through the tokenized code lines to find user declarations of procedures, functions, variables, and
    /// any other necessary user-defined items for reference.
    /// </summary>
    /// <param name="tokenizedCodeLines">
    /// A <see cref="List{T}" /> of <see cref="ITokenizedCodeLine" /> instances.
    /// </param>
    public IUserReferenceTable FindUserDeclarations(List<ITokenizedCodeLine> tokenizedCodeLines)
    {
        IUserReferenceTable table = new UserReferenceTable();

        if (tokenizedCodeLines.Count > 0)
        {
            int lineCount = 0;
            foreach (ITokenizedCodeLine codeLine in tokenizedCodeLines)
            {
                // The first token should always be a "command" or other such indicator so we know what the line is doing.
                if (codeLine.Count > 0)
                {
                    // Assign line number.
                    codeLine.LineNumber = lineCount;
                    IToken? commandToken = codeLine[0];
                    if (commandToken != null)
                    {
                        switch (commandToken.TokenType)
                        {
                            case TokenType.ReservedWord:
                                if (commandToken.Text == KeywordNames.CommandProcedure)
                                    ProcessProcedureDeclaration(table, lineCount, codeLine);

                                else if (commandToken.Text == KeywordNames.CommandFunction)
                                    ProcessFunctionDeclaration(table, lineCount, codeLine);

                                else if (commandToken.Text == KeywordNames.CommandDim)
                                    ProcessVariableDeclaration(table, lineCount, codeLine);

                                break;

                            default:
                                break;
                        }
                    }
                    lineCount++;
                }
            }
        }
        return table;
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Processes the procedure declaration.
    /// </summary>
    /// <param name="table">
    /// The reference to the user-defined items table.
    /// </param>
    /// <param name="lineNumber">
    /// An integer specifying the line number.
    /// </param>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> instance containing the line of code parsed into tokens.
    /// The code line.</param>
    private void ProcessProcedureDeclaration(IUserReferenceTable table, int lineNumber, ITokenizedCodeLine codeLine)
    {
        IToken? separator = codeLine[1];
        IToken? nameToken = codeLine[2];

        if ((separator == null) || 
            (separator.TokenType != TokenType.SeparatorDelimiter) || 
            (nameToken == null) ||
            (nameToken.Text == null))
        {
            // Throw SYNTAX ERROR.
            throw new SyntaxErrorException(lineNumber, "Invalid procedure declaration syntax.");
        }

        table.AddUserProcedureDeclaration(lineNumber, nameToken.Text, codeLine);
        codeLine.Substitute(2, BlazorBasicTokenFactory.CreateToken(nameToken.Text, TokenType.ProcedureName));

    }
    /// <summary>
    /// Processes the function declaration.
    /// </summary>
    /// <param name="table">
    /// The reference to the user-defined items table.
    /// </param>
    /// <param name="lineNumber">
    /// An integer specifying the line number.
    /// </param>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> instance containing the line of code parsed into tokens.
    /// The code line.</param>
    private void ProcessFunctionDeclaration(IUserReferenceTable table, int lineNumber, ITokenizedCodeLine codeLine)
    {
        IToken? separator = codeLine[1];
        IToken? nameToken = codeLine[2];

        if ((separator == null) ||
            (separator.TokenType != TokenType.SeparatorDelimiter) ||
            (nameToken == null) ||
            (nameToken.Text == null))
        {
            // Throw SYNTAX ERROR.
            throw new SyntaxErrorException(lineNumber, "Invalid function declaration syntax.");
        }

        table.AddUserFunctionDeclaration(lineNumber, nameToken.Text, codeLine);
        codeLine.Substitute(2, BlazorBasicTokenFactory.CreateToken(nameToken.Text, TokenType.FunctionName));
    }
    /// <summary>
    /// Processes the variable declaration.
    /// </summary>
    /// <param name="table">
    /// The reference to the user-defined items table.
    /// </param>
    /// <param name="lineNumber">
    /// An integer specifying the line number.
    /// </param>
    /// <param name="codeLine">
    /// An <see cref="ITokenizedCodeLine"/> instance containing the line of code parsed into tokens.
    /// The code line.</param>
    private void ProcessVariableDeclaration(IUserReferenceTable table, int lineNumber, ITokenizedCodeLine codeLine)
    {
        IToken? nameToken = codeLine[2];
        if (nameToken == null || nameToken.Text == null)
        {
            // Throw SYNTAX ERROR.
            throw new SyntaxErrorException(lineNumber, "Invalid variable declaration syntax.");
        }

        table.AddUserVariableDeclaration(lineNumber, nameToken.Text, codeLine);
        codeLine.Substitute(2, BlazorBasicTokenFactory.CreateToken(nameToken.Text, TokenType.VariableName));
    }

    private void PerformSubstitutions(UserReferenceTable userReferences, ITokenizedCodeLine line)
    {
        for (int index = 0; index < line.Count;index++)
        {
            IToken token = line[index];
            if (token.TokenType == TokenType.UserDefinedItem)
            {
                if (userReferences.IsFunction(token))
                {
                    line.Substitute(index,
                        BlazorBasicTokenFactory.CreateToken(token.Text, TokenType.FunctionName));
                }
                else if (userReferences.IsProcedure(token))
                {
                    line.Substitute(index,
                        BlazorBasicTokenFactory.CreateToken(token.Text, TokenType.ProcedureName));
                }
                else if (userReferences.IsVariable(line.LineNumber,token))
                {
                    line.Substitute(index,
                        BlazorBasicTokenFactory.CreateToken(token.Text, TokenType.VariableName));
                }
            }
        }
    }
    #endregion
}