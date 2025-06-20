using Adaptive.BlazorBasic.LanguageService;
using Adaptive.BlazorBasic.LanguageService.CodeDom;
using System;

namespace Adaptive.BlazorBasic.CodeDom;

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
    public BasicInputStatement()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicInputStatement"/> class.
    /// </summary>
    /// <param name="codeLine">An <see cref="ITokenizedCodeLine" /> containing the code line to be parsed.</param>
    public BasicInputStatement(ITokenizedCodeLine codeLine) : base(codeLine)
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
        _variableReferenceExpression = new BasicVariableReferenceExpression(variableName);

    }
    private void ParseWithFileNumber(ITokenizedCodeLine codeLine)
    {
        string fileNumber = codeLine[2]!.Text!;
        _fileNumberExpression = new BasicFileNumberExpression(fileNumber);

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
        _variableReferenceExpression = new BasicVariableReferenceExpression(variableName);
    }
    private void ParseWithStringLiteralPrompt(ITokenizedCodeLine codeLine)
    {
        int index = 3;
        string data = codeLine[2]!.Text!;

        int nextIndex = -1;
        do
        {
            IToken token = codeLine[index];
            if (token.TokenType == TokenType.StringDelimiter)
                nextIndex = index;
            index++;
        } while (index < codeLine.Count && nextIndex == -1);

        _promptExpression = new BasicLiteralStringExpression(codeLine.CombineValues(4, nextIndex - 1));

        nextIndex = -1;
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
        _variableReferenceExpression = new BasicVariableReferenceExpression(variableName);
    }
    #endregion
}
