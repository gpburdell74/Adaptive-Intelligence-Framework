using Adaptive.Intelligence.BlazorBasic.Services;
using Adaptive.Intelligence.LanguageService.CodeDom;
using Adaptive.Intelligence.LanguageService.Tokenization;
using System.Text;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Statements;
using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Represents a POKE statement.
/// </summary>
/// <example>
/// 
///     POKE ( [address value less than 65536], [byte value] )
///     
/// </example>
/// <seealso cref="BasicCodeStatement" />
public sealed class BasicPokeStatement : BasicCodeStatement
{
    private short _address = 0;
    private byte _value = 0;
    
    public BasicPokeStatement(BlazorBasicLanguageService service, ITokenizedCodeLine codeLine) : base(service, codeLine)
    {

    }
    
    public byte Value { get; set; }
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
        start = list.FindFirstToken(TokenType.Integer);
        int end = list.FindNextToken(start+1, TokenType.Integer);
        
        if (start == -1 || end == -1 || end <= start)
        {
            throw new BasicSyntaxErrorException(0, "Syntax error in PEEK statement.");
        }
        else
        {
            _address = Convert.ToInt16(list[start].Text);
            _value = Convert.ToByte(list[end].Text);
        }
    }

    public override string? Render()
    {
        return KeywordNames.CommandPoke + 
               ParseConstants.Space + 
               DelimiterNames.DelimiterOpenParens +
               _address.ToString() + 
               DelimiterNames.DelimiterListSeparator + 
               _value.ToString() + 
                DelimiterNames.DelimiterCloseParens;
        
    }
}