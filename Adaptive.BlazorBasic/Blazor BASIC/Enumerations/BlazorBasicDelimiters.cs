namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Lists the types of raw text delimiters used in parsing Blazor BASIC.
/// </summary>
public enum BlazorBasicDelimiters
{
    /// <summary>
    /// Indicates the delimiter does not exist or is not known.
    /// </summary>
    NoneOrUnknown = 0,
    /// <summary>
    /// The space character.
    /// </summary>
    Space,
    /// <summary>
    /// The CR character.
    /// </summary>
    Cr,
    /// <summary>
    /// The LF character.
    /// </summary>
    Lf,
    /// <summary>
    /// The ` character.
    /// </summary>
    Char,
    /// <summary>
    /// The double-quote character.
    /// </summary>
    String,
    /// <summary>
    /// The open parenthesis character.
    /// </summary>
    OpenParens,
    /// <summary>
    /// The close parenthesis character.
    /// </summary>
    CloseParens,
    /// <summary>
    /// The open square bracket character.
    /// </summary>
    OpenBracket,
    /// <summary>
    /// The close square bracket character.
    /// </summary>
    CloseBracket,
    /// <summary>
    /// The open curly bracket character.
    /// </summary>
    OpenBlockBracket,
    /// <summary>
    /// The close curly bracket  character.
    /// </summary>
    CloseBlockBracket,
    /// <summary>
    /// The comma character.
    /// </summary>
    ListSeparator
}
