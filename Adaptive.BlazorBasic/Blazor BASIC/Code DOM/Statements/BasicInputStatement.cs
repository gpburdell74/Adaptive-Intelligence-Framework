using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents the INPUT command.
/// </summary>
/// <example>
/// Expected Formats:
///     INPUT [variableName]
///     INPUT [prompt string],[variableName]
///     INPUT #[fileNumber], [variableName]
///     
///     INPUT dataVariable
///     INPUT "Enter your Name: ", dataVariable
///     INPUT #1, dataVariable
///     
/// </example>
/// <seealso cref="BasicCodeStatement" />
public class BasicInputStatement : BasicCodeStatement 
{
    #region Private Member Declarations    
    /// <summary>
    /// The file number expression.
    /// </summary>
    private BasicFileNumberExpression? _fileNumberExpression;
    /// <summary>
    /// The variable reference expression.
    /// </summary>
    private BasicVariableReferenceExpression? _variableReferenceExpression;
    /// <summary>
    /// The prompt expression.
    /// </summary>
    private ILanguageCodeExpression? _promptExpression;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInputStatement"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicInputStatement(BlazorBasicLanguageService service) : base(service)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInputStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicInputStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {

    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _fileNumberExpression?.Dispose();
            _promptExpression?.Dispose();
            _variableReferenceExpression?.Dispose();
        }

        _fileNumberExpression = null;
        _promptExpression = null;
        _variableReferenceExpression = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the reference to the file number expression.
    /// </summary>
    /// <value>
    /// A <see cref="BasicFileNumberExpression"/> instance, or <b>null</b> if not used.
    /// </value>
    public BasicFileNumberExpression? FileNumberExpression => _fileNumberExpression;
    /// <summary>
    /// Gets the reference to the prompt expression.
    /// </summary>
    /// <value>
    /// An <see cref="ILanguageCodeExpression"/> instance, or <b>null</b> if not used.
    /// </value>
    public ILanguageCodeExpression? PromptExpression => _promptExpression;
    /// <summary>
    /// Gets the reference to the file number expression.
    /// </summary>
    /// <value>
    /// A <see cref="BasicVariableReferenceExpression"/> instance, or <b>null</b> if not used.
    /// </value>
    public BasicVariableReferenceExpression? VariableReferenceExpression => _variableReferenceExpression;

    /// <summary>
    /// Gets the value of how the current number of tabs being printed is to be modified.
    /// </summary>
    /// <value>
    /// The tab modification.
    /// </value>
    public override RenderTabState TabModification => RenderTabState.None;
    #endregion

    #region Protected Method Overrides    
    /// <summary>
    /// Parses the specified code content.
    /// </summary>
    /// <param name="codeLine">
    /// A string containing the code to be parsed.
    /// </param>
    protected override void ParseIntoExpressions(ITokenizedCodeLine codeLine)
    {
        // Expected formats:
        //
        //     INPUT [variableName]
        //     INPUT [prompt string],[variableName] or 
        //     INPUT [variableName],[variableName]
        //     INPUT #[fileNumber], [variableName]

        if (codeLine.Count < 3)
            throw new Exception("?SYNTAX ERROR");

        if (codeLine.Count == 3)
        {
            // e.g. INPUT myData
            ParseOnlyVariableName(codeLine);
        }
        else if (codeLine.Count > 3)
        {
            string? secondTokenText = codeLine[2]?.Text;

            if (secondTokenText == null)
                throw new Exception("?SYNTAX ERROR");

            if (secondTokenText.StartsWith(ParseConstants.NumberSign))
            {
                // e.g. INPUT #1, myData
                ParseWithFileNumber(codeLine);
            }
            else if (secondTokenText.StartsWith(ParseConstants.DoubleQuote))
            {
                // e.g.  INPUT "Enter something":, myData
                ParseWithStringLiteralPrompt(codeLine);
            }
            else
            {
                throw new Exception("?SYNTAX ERROR");
            }
        }
    }

    #endregion

    #region Private Methods / Functions
    private void ParseOnlyVariableName(ITokenizedCodeLine codeLine)
    {
        string variableName = codeLine[2]!.Text!;
        _variableReferenceExpression = new BasicVariableReferenceExpression(Service, variableName);

    }
    private void ParseWithFileNumber(ITokenizedCodeLine codeLine)
    {
        string fileNumber = codeLine[2]!.Text!;
        _fileNumberExpression = new BasicFileNumberExpression(Service, fileNumber);

        int index = 2;
        int nextIndex = -1;
        do
        {
            IToken token = codeLine[index];
            if (token.TokenType != TokenType.SeparatorDelimiter)
                nextIndex = index;
            index++;
        } while (index < codeLine.Count && nextIndex == -1);

        string? variableName = codeLine[nextIndex]!.Text!;
        if (variableName == null)
            throw new Exception("?SYNTAX ERROR");
        _variableReferenceExpression = new BasicVariableReferenceExpression(Service, variableName);
    }
    private void ParseWithStringLiteralPrompt(ITokenizedCodeLine codeLine)
    {
        int startIndex = codeLine.IndexOf(TokenType.StringDelimiter);
        int endIndex = codeLine.IndexOf(startIndex+1, TokenType.StringDelimiter);

        _promptExpression = new BlazorBasicLiteralStringExpression(Service,
            codeLine.CombineValues(startIndex + 1, endIndex - 1));


        int nextIndex = -1;
        int index = endIndex + 1;
        do
        {
            IToken token = codeLine[index];
            if (token.TokenType != TokenType.SeparatorDelimiter)
                nextIndex = index;
            index++;
        } while (index < codeLine.Count && nextIndex == -1);


        string? variableName = codeLine[nextIndex]!.Text!;
        if (variableName == null)
            throw new Exception("?SYNTAX ERROR");
        _variableReferenceExpression = new BasicVariableReferenceExpression(Service, variableName);
    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    public override string? Render()
    {
        StringBuilder builder = new StringBuilder();

        // INPUT [variableName] OR
        // INPUT #<fileHandle>, [VariableName] OR
        // INPUT [prompt string],[variableName]
        builder.Append(KeywordNames.CommandInput);
        builder.Append(ParseConstants.Space);

        if (_fileNumberExpression != null)
        {
            builder.Append(_fileNumberExpression.Render());
            builder.Append(ParseConstants.Comma);
            builder.Append(ParseConstants.Space);
        }
        else if (_promptExpression != null)
        {
            builder.Append(_promptExpression.Render());
            builder.Append(ParseConstants.Comma);
            builder.Append(ParseConstants.Space);
        }

        builder.Append(_variableReferenceExpression.Render());
        return builder.ToString();
    }
    #endregion
}
