namespace Adaptive.Intelligence.LanguageService;

/// <summary>
/// Lists the types of content that may be parsed from a language code file.
/// </summary>
/// <remarks>
/// This is a master list of all the types of items, content, or other elements that may be seen and 
/// understood in a code file.
/// </remarks>
public enum LanguageContentType
{
    /// <summary>
    /// Indicates the element does not exist or is of an unknown type.
    /// </summary>
    NoneOrUnknown = 0,
    /// <summary>
    /// Indicates a boolean data type.
    /// </summary>
    DataTypeBoolean,
    /// <summary>
    /// Indicates a byte data type.
    /// </summary>
    DataTypeByte,
    /// <summary>
    /// Indicates a character data type.
    /// </summary>
    DataTypeChar,
    /// <summary>
    /// Indicates a 16-bit integer data type.
    /// </summary>
    DataTypeShortInteger,
    /// <summary>
    /// Indicates a 32-bit integer data type.
    /// </summary>
    DataTypeInteger,
    /// <summary>
    /// Indicates a 64-bit integer data type.
    /// </summary>
    DataTypeLongInteger,
    /// <summary>
    /// Indicates a single-precision floating-point data type.
    /// </summary>
    DataTypeFloat,
    /// <summary>
    /// Indicates a double-precision floating-point data type.
    /// </summary>
    DataTypeDouble,
    /// <summary>
    /// Indicates a date data type.
    /// </summary>
    DataTypeDate,
    /// <summary>
    /// Indicates a date and time data type.
    /// </summary>
    DataTypeDateTime,
    /// <summary>
    /// Indicates a time data type.
    /// </summary>
    DataTypeTime,
    /// <summary>
    /// Indicates a string data type.
    /// </summary>
    DataTypeString,
    /// <summary>
    /// Indicates an object data type.
    /// </summary>
    DataTypeObject,

    /// <summary>
    /// Indicates a delimiter used to (generally) separate tokens when parsing.
    /// </summary>
    DelimiterSeparator,
    /// <summary>
    /// Indicates a delimiter used to mark the start or end of a character literal.
    /// </summary>
    DelimiterCharacterLiteral,
    /// <summary>
    /// Indicates a delimiter used to mark the start or end of a string literal.
    /// </summary>
    DelimiterStringLiteral,
    /// <summary>
    /// Indicates a delimiter used to mark the start of a code block.
    /// </summary>
    DelimiterBlockStart,
    /// <summary>
    /// Indicates a delimiter used to mark the end of a code block.
    /// </summary>
    DelimiterBlockEnd,
    /// <summary>
    /// Indicates a delimiter used to mark the start of an expression (or sub-expression).
    /// </summary>
    DelimiterExpressionStart,
    /// <summary>
    /// Indicates a delimiter used to mark the end of an expression (or sub-expression).
    /// </summary>
    DelimiterExpressionEnd,
    /// <summary>
    /// Indicates a delimiter used to mark the start of a sizing expression (such as for an array).
    /// </summary>
    DelimiterSizingStart,
    /// <summary>
    /// Indicates a delimiter used to mark the end of a sizing expression (such as for an array).
    /// </summary>
    DelimiterSizingEnd,
    /// <summary>
    /// Indicates another delimiter type.
    /// </summary>
    DelimiterOther,

    /// <summary>
    /// Indicates an arithmetic operator type (such as + - * / ).
    /// </summary>
    OperatorArithmetic,
    /// <summary>
    /// Indicates an assignment operator type (such as = or :=)
    /// </summary>
    OperatorAssignment,
    /// <summary>
    /// Indicates an bitwise operator type (such as &amp; or |).
    /// </summary>
    OperatorBitwise,
    /// <summary>
    /// Indicates a comparison operator type (such as &gt; &lt; == etc.) 
    /// </summary>
    OperatorComparison,
    /// <summary>
    /// Indicates an decrement operator type (such as --) or an increment operator type (such as ++),
    /// or some other operator of this kind.
    /// </summary>
    OperatorOperational,
    /// <summary>
    /// Indicates an logical operator type (such as AND, OR, NOT, &amp;&amp;, ||, !)
    /// </summary>
    OperatorLogical,
    /// <summary>
    /// Indicates another type of operator.
    /// </summary>
    OperatorOther,
    /// <summary>
    /// Indicates a reserved keyword.
    /// </summary>
    ReservedKeyword,
    /// <summary>
    /// Indicates a reserved function name.
    /// </summary>
    ReservedFunctionName,
    /// <summary>
    /// Indicates a reserved language element.
    /// </summary>
    ReservedLanguageElement,
    /// <summary>
    /// Indicates a reserved data element of some other kind.
    /// </summary>
    ReservedOther
}