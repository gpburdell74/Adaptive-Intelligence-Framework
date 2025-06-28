namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

/// <summary>
/// Lists the types of code blocks that are currently supported.
/// </summary>
public enum BlockType
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
