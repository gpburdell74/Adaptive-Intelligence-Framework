namespace Adaptive.Intelligence.LanguageService.CodeDom;

/// <summary>
/// Lists the tab states to use with statements when rendering code.
/// </summary>
public enum RenderTabState
{
    /// <summary>
    /// Indicates no modification or operation.
    /// </summary>
    None = 0,
    /// <summary>
    /// Indicates the related statement must start at the left, no indention.
    /// </summary>
    NoTab,
    /// <summary>
    /// Indicates the related statement must start at the left, no indention, but the indentation
    /// starts again after the statement.
    /// </summary>
    NoTabAndIndentAfter,
    /// <summary>
    /// Indicates the indention is to be increased for this statement.
    /// </summary>
    Indent,
    /// <summary>
    /// Indicates the indention is to be increased after this statement.
    /// </summary>
    IndentAfter,
    /// <summary>
    /// Indicates the indention is to be decreased for this statement.
    /// </summary>
    Exdent,
    /// <summary>
    /// Indicates the indention is to be decreased before this statement.
    /// </summary>
    ExdentBefore

}
