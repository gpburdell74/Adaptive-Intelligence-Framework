namespace Adaptive.BlazorBasic;

/// <summary>
/// Lists the types of bitwise operators that are supported.
/// </summary>
public enum BlazorBasicBitwiseOperators
{
    /// <summary>
    /// Indicates an unknown operator.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Indicates the single-character bitwise AND operator ( &amp; ).
    /// </summary>
    ShortAnd,
    /// <summary>
    /// Indicates the standard bitwise AND operator ( AND ).
    /// </summary>
    LongAnd,
    /// <summary>
    /// Indicates the single-character bitwise OR operator ( | ).
    /// </summary>
    ShortOr,
    /// <summary>
    /// Indicates the standard bitwise OR operator ( OR ).
    /// </summary>
    LongOr

}
