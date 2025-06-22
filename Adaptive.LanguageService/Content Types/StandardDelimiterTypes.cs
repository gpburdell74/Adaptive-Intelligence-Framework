namespace Adaptive.LanguageService;

/// <summary>
/// Lists the standard text delimiter types the language service supports.
/// </summary>
public enum StandardDelimiterTypes
{
    /// <summary>
    /// Indicates an unknown delimiter type.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Indicates a delimiter used to (generally) separate tokens when parsing.
    /// </summary>
    Separator = 1,
    /// <summary>
    /// Indicates a delimiter used to mark the start or end of a character literal.
    /// </summary>
    CharacterLiteral,
    /// <summary>
    /// Indicates a delimiter used to mark the start or end of a string literal.
    /// </summary>
    StringLiteral,
    /// <summary>
    /// Indicates a delimiter used to mark the start of a code block.
    /// </summary>
    BlockStart,
    /// <summary>
    /// Indicates a delimiter used to mark the end of a code block.
    /// </summary>
    BlockEnd,
    /// <summary>
    /// Indicates a delimiter used to mark the start of an expression (or sub-expression).
    /// </summary>
    ExpressionStart,
    /// <summary>
    /// Indicates a delimiter used to mark the end of an expression (or sub-expression).
    /// </summary>
    ExpressionEnd,
    /// <summary>
    /// Indicates a delimiter used to mark the start of a sizing expression (such as for an array).
    /// </summary>
    SizingStart,
    /// <summary>
    /// Indicates a delimiter used to mark the end of a sizing expression (such as for an array).
    /// </summary>
    SizingEnd,
    /// <summary>
    /// Indicates another delimiter type.
    /// </summary>
    Other

}
