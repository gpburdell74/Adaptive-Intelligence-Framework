namespace Adaptive.Intelligence.LanguageService;

/// <summary>
/// Lists the types of code blocks to be supported.
/// </summary>
public enum CodeBlockType
{
    /// <summary>
    /// Indicates the block type is unknown.
    /// </summary>
    NoneOrUnknown = 0,
    /// <summary>
    /// Indicates an IF THEN ELSE ENDIF block.
    /// </summary>
    If,
    /// <summary>
    /// Indicates a PROCEDURE ... END PROCEDURE block. 
    /// </summary>
    Procedure,
    /// <summary>
    /// Indicates a FUNCTION ... END FUNCTION block. 
    /// </summary>
    Function,
    /// <summary>
    /// Indicates a loop.
    /// </summary>
    Loop
}

