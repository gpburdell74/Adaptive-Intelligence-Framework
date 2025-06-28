namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Lists the commands that are currently supported.
/// </summary>
public enum BlazorBasicKeywords
{
    /// <summary>
    /// Indicates an unknown value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Indicates a NOOP command.
    /// </summary>
    NoOp,
    /// <summary>
    /// Indicates an APPEND keyword.
    /// </summary>
    Append,
    /// <summary>
    /// Indicates an AS keyword.
    /// </summary>
    As,
    /// <summary>
    /// Indicates a comment: ' or REM
    /// </summary>
    Comment,
    /// <summary>
    /// Indicates a CLOSE (file) command.
    /// </summary>
    Close,
    /// <summary>
    /// Indicates a CLS command.
    /// </summary>
    Cls,
    /// <summary>
    /// Indicates a DO loop start command.
    /// </summary>
    Do,
    /// <summary>
    /// Indicates a DIM command.
    /// </summary>
    Dim,
    /// <summary>
    /// Indicates a general END command ( procedures, functions, loops, ifs, etc.).
    /// </summary>
    End,
    /// <summary>
    /// The command to start a FOR loop.
    /// </summary>
    For,
    /// <summary>
    /// The command to start a FUNCTION definition.
    /// </summary>
    Function,
    /// <summary>
    /// Indicates an IF statement start command.
    /// </summary>
    If,
    /// <summary>
    /// Indicates an INPUT command.
    /// </summary>
    Input,
    /// <summary>
    /// Indicates a LET command.
    /// </summary>
    Let,
    /// <summary>
    /// Indicates a LOOP command.
    /// </summary>
    Loop,
    /// <summary>
    /// Indicates the end of a FOR loop.
    /// </summary>
    Next,
    /// <summary>
    /// Indicates an OPEN (file) command.
    /// </summary>
    Open,
    /// <summary>
    /// Indicates an OUTPUT keyword.
    /// </summary>
    Output,
    /// <summary>
    /// Indicates a PRINT command.
    /// </summary>
    Print,
    /// <summary>
    /// The command to start a PROCEDURE definition.
    /// </summary>
    Procedure,
    /// <summary>
    /// Indicates a file is to be opened for random access.
    /// </summary>
    Random,
    /// <summary>
    /// Indicates a READ command.
    /// </summary>
    Read,
    /// <summary>
    /// Indicates a RETURN command.
    /// </summary>
    Return,
    /// <summary>
    /// Indicates a THEN keyword.
    /// </summary>
    Then,
    /// <summary>
    /// Indicates an UNTIL keyword.
    /// </summary>
    Until,
    /// <summary>
    /// Indicates a WRITE command.
    /// </summary>
    Write
}
