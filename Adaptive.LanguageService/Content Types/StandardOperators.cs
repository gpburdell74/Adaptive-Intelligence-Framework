namespace Adaptive.LanguageService;

/// <summary>
/// Lists the standard operators the language service supports.
/// </summary>
public enum StandardOperators
{
    /// <summary>
    /// Indicates none or an unknown operator.
    /// </summary>
    NoneOrUnknown = 0,
    /// <summary>
    /// Indicates the equals sign ( = ).
    /// </summary>
    AssignmentEquals = 1,
    /// <summary>
    /// Indicates the single-character bitwise AND operator ( & ).
    /// </summary>
    BitwiseShortAnd,
    /// <summary>
    /// Indicates the standard bitwise AND operator ( AND ).
    /// </summary>
    BitwiseLongAnd,
    /// <summary>
    /// Indicates the single-character bitwise OR operator ( | ).
    /// </summary>
    BitwiseShortOr,
    /// <summary>
    /// Indicates the standard bitwise OR operator ( OR ).
    /// </summary>
    BitwiseLongOr,
    /// <summary>
    /// Indicates the equal to operator (==)
    /// </summary>
    ComparisonEqualTo,
    /// <summary>
    /// Indicates the greater than operator (&gt;).
    /// </summary>
    ComparisonGreaterThan,
    /// <summary>
    /// Indicates the greater than or equal to operator (&gt;=).
    /// </summary>
    ComparisonGreaterThanOrEqualTo,
    /// <summary>
    /// Indicates the less than operator (&lt;).
    /// </summary>
    ComparisonLessThan,
    /// <summary>
    /// Indicates the less than or equal to operator (&lt;=).
    /// </summary>
    ComparisonLessThanOrEqualTo,
    /// <summary>
    /// Indicates the not equal to operator (!=).
    /// </summary>
    ComparisonNotEqualTo,
    /// <summary>
    /// Indicates the AND operator (AND)
    /// </summary>
    LogicalAnd,
    /// <summary>
    /// Indicates the AND operator (&&)
    /// </summary>
    LogicalAndShort,
    /// <summary>
    /// Indicates the OR operator (OR)
    /// </summary>
    LogicalOr,
    /// <summary>
    /// Indicates the OR operator (||)
    /// </summary>
    LogicalOrShort,
    /// <summary>
    /// Indicates the NOT operator (NOT)
    /// </summary>
    LogicalNot,
    /// <summary>
    /// Indicates the NOT operator (!)
    /// </summary>
    LogicalNotShort,
    /// <summary>
    /// Indicates the addition operator (+)
    /// </summary>
    MathAdd,
    /// <summary>
    /// Indicates the subtraction operator (-)
    /// </summary>
    MathSubtract,
    /// <summary>
    /// Indicates the multiplication operator (*)
    /// </summary>
    MathMultiply,
    /// <summary>
    /// Indicates the division operator (/)
    /// </summary>
    MathDivide,
    /// <summary>
    /// Indicates the Exponent operator (^)
    /// </summary>
    MathExponent,
    /// <summary>
    /// Indicates the modulus operator (%)
    /// </summary>
    MathModulus,
    /// <summary>
    /// Indicates the increment operator (++).
    /// </summary>
    OpsIncrement,
    /// <summary>
    /// Indicates the decrement operator (--).
    /// </summary>
    OpsDecrement

}
