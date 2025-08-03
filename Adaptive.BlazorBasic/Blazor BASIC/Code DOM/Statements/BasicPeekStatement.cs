using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a PEEK statement.
/// </summary>
/// <example>
/// 
///     PEEK ( [address value less than 65536] )
///     
/// </example>
/// <seealso cref="BasicCodeStatement" />
public sealed class BasicPeekStatement : BasicCodeStatement
{
    private short _address = 0;

    public BasicPeekStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {

    }
    
    public short Address { get; set; }

    public override RenderTabState TabModification { get; }

    /// <summary>
    /// Parses the tokenized code line into the PEEK statement.
    /// </summary>
    /// <param name="codeLine">
    /// The <see cref="ITokenizedCodeLine"/> instance to be parsed.
    /// </param>
    /// <exception cref="BasicSyntaxErrorException">
    /// Thrown if the PEEK statement is not formatted correctly.
    /// </exception>
    protected override void ParseIntoExpressions(ITokenizedCodeLine codeLine)
    {
        ManagedTokenList list = new ManagedTokenList(codeLine.TokenList).Trim();
        int start = list.FindFirstToken(TokenType.ExpressionStartDelimiter);
        int end = list.FindLastToken(TokenType.ExpressionEndDelimiter);

        if (start == -1 || end == -1 || end > start || end <= start + 1)
        {
            throw new BasicSyntaxErrorException(0, "Syntax error in PEEK statement.");
        }
        else
        {
            _address = Convert.ToInt16(list[start + 1]);
        }
    }

    public override string? Render()
    {
        return KeywordNames.CommandPeek + ParseConstants.Space + DelimiterNames.DelimiterOpenParens +
               _address.ToString()
               + DelimiterNames.DelimiterCloseParens;
        
    }
}