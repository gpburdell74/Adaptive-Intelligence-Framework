namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Lists the types of code/text tokens that may be parsed from input text.
/// </summary>
public enum TokenType
{
    /// <summary>
    /// Indicates the token type is unknown or invalid.
    /// </summary>
    NoneOrUnknown = 0,
    /// <summary>
    /// Indicates the token representing a reserved word.
    /// </summary>
    ReservedWord = 1,
    /// <summary>
    /// Indicates the token representing a built-in function.
    /// </summary>
    ReservedFunction,
    /// <summary>
    /// Indicates the token representing a data type name.
    /// </summary>
    DataTypeName,
    /// <summary>
    /// Indicates the token representing a user-defined procedure name.
    /// </summary>
    ProcedureName,
    /// <summary>
    /// Indicates the token representing a user-defined function name.
    /// </summary>
    FunctionName,
    /// <summary>
    /// Indicates the token representing a user-defined variable name.
    /// </summary>
    VariableName,
    /// <summary>
    /// Indicates the token representing a user-defined item.
    /// </summary>
    UserDefinedItem,
    /// <summary>
    /// Indicates the token representing an integer.
    /// </summary>
    Integer,
    /// <summary>
    /// Indicates the token representing a floating-point number.
    /// </summary>
    FloatingPoint,
    /// <summary>
    /// Indicates the token representing an arithmetic operator.
    /// </summary>
    ArithmeticOperator,
    /// <summary>
    /// Indicates the token representing a bitwise operator.
    /// </summary>
    BitwiseOperator,
    /// <summary>
    /// Indicates the token representing a comparison operator.
    /// </summary>
    ComparisonOperator,
    /// <summary>
    /// Indicates the token representing a logical operator.
    /// </summary>
    LogicalOperator,
    /// <summary>
    /// Indicates the token representing an increment operator.
    /// </summary>
    IncrementOperator,
    /// <summary>
    /// Indicates the token representing a decrement operator.
    /// </summary>
    DecrementOperator,
    /// <summary>
    /// Indicates the token representing an assignment operator.
    /// </summary>
    AssignmentOperator,
    /// <summary>
    /// Indicates the token representing a general separation delimiter.
    /// </summary>
    SeparatorDelimiter,
    /// <summary>
    /// Indicates the token representing character literal start/end delimiter.
    /// </summary>
    CharacterDelimiter,
    /// <summary>
    /// Indicates the token representing a string literal start/end delimiter.
    /// </summary>
    StringDelimiter,
    /// <summary>
    /// Indicates the token representing an expression start delimiter.
    /// </summary>
    ExpressionStartDelimiter,
    /// <summary>
    /// Indicates the token representing an expression end delimiter.
    /// </summary>
    ExpressionEndDelimiter,
    /// <summary>
    /// Indicates the token representing sizing expression start delimiter.
    /// </summary>
    SizingStartDelimiter,
    /// <summary>
    /// Indicates the token representing sizing expression end delimiter.
    /// </summary>
    SizingEndDelimiter,
    /// <summary>
    /// Indicates the token representing code block start delimiter.
    /// </summary>
    BlockStartDelimiter,
    /// <summary>
    /// Indicates the token representing a code block end delimiter.
    /// </summary>
    BlockEndDelimiter,
    /// <summary>
    /// Indicates the token representing an error in the parsing process.
    /// </summary>
    Error
}
