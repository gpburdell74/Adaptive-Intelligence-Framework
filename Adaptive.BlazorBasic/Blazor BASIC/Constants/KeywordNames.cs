namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the static constants definitions for the keywords.
/// </summary>
internal static class KeywordNames
{
    #region Commands
    /// <summary>
    /// Indicates no command or a NOOP command.
    /// </summary>
    public const string CommandNoOp = "NOOP";
    /// <summary>
    /// Indicates a comment: ' or REM
    /// </summary>
    public const string CommandCommentShort = "'";
    /// <summary>
    /// Indicates a comment: ' or REM
    /// </summary>
    public const string CommandCommentRemark = "REM";
    /// <summary>
    /// Indicates a CLOSE (file) command.
    /// </summary>
    public const string CommandClose = "CLOSE";
    /// <summary>
    /// Indicates a CLS command.
    /// </summary>
    public const string CommandCls = "CLS";
    /// <summary>
    /// Indicates a DIM command.
    /// </summary>
    public const string CommandDim = "DIM";
    /// <summary>
    /// Indicates a DO loop start command.
    /// </summary>
    public const string CommandDo = "DO";
    /// <summary>
    /// Indicates a general END command ( for loops = ""; ifs = ""; etc.).
    /// </summary>
    public const string CommandEnd = "END";
    /// <summary>
    /// Indicates a the start of a FOR ... NEXT loop.
    /// </summary>
    public const string CommandFor = "FOR";
    /// <summary>
    /// Indicates a new FUNCTION declaration.
    /// </summary>
    public const string CommandFunction = "FUNCTION";
    /// <summary>
    /// Indicates an IF statement start command.
    /// </summary>
    public const string CommandIf = "IF";
    /// <summary>
    /// Indicates an INPUT command.
    /// </summary>
    public const string CommandInput = "INPUT";
    /// <summary>
    /// Indicates a LET command.
    /// </summary>
    public const string CommandLet = "LET";
    /// <summary>
    /// Indicates a LOOP command.
    /// </summary>
    public const string CommandLoop = "LOOP";
    /// <summary>
    /// Indicates an OPEN (file) command.
    /// </summary>
    public const string CommandOpen = "OPEN";
    /// <summary>
    /// Indicates a the end of a FOR ... NEXT loop.
    /// </summary>
    public const string CommandNext = "NEXT";
/// <summary>
/// Indicates a PEEK command.
/// </summary>
    public const string CommandPeek = "PEEK";
    /// <summary>
    /// Indicates a POKE command.
    /// </summary>
    public const string CommandPoke = "POKE";
    /// <summary>
    /// Indicates a PRINT command.
    /// </summary>
    public const string CommandPrint = "PRINT";
    /// <summary>
    /// Indicates a new PROCEDURE declaration.
    /// </summary>
    public const string CommandProcedure = "PROCEDURE";
    /// <summary>
    /// Indicates a READ command.
    /// </summary>
    public const string CommandRead = "READ";
    /// <summary>
    /// Indicates a RETURN command.
    /// </summary>
    public const string CommandReturn = "RETURN";
    /// <summary>
    /// Indicates a WRITE command.
    /// </summary>
    public const string CommandWrite = "WRITE";
    #endregion

    #region SUB MAIN
    /// <summary>
    /// The keyword "main" identifying the entry point into the application.
    /// </summary>
    public const string KeywordMain = "MAIN";
    #endregion

    #region Sub Command Reserved Words
    /// <summary>
    /// Indicates an "AS" keyword.
    /// </summary>
    public const string KeywordAs = "AS";
    /// <summary>
    /// Indicates an "END" keyword.
    /// </summary>
    public const string KeywordEnd = "END";
    /// <summary>
    /// Indicates a "THEN" keyword.
    /// </summary>
    public const string KeywordThen = "THEN";
    /// <summary>
    /// Indicates an "UNTIL" keyword.
    /// </summary>
    public const string KeywordUntil = "UNTIL";

    #endregion

    #region I/O Reserved Words
    /// <summary>
    /// The I/O append keyword.
    /// </summary>
    public const string IOAppend = "APPEND";
    /// <summary>
    /// The I/O input keyword.
    /// </summary>
    public const string IOInput = "INPUT";
    /// <summary>
    /// The I/O output keyword.
    /// </summary>
    public const string IOOutput = "OUTPUT";
    /// <summary>
    /// The I/O random keyword.
    /// </summary>
    public const string IORandom = "RANDOM";
    #endregion
}
