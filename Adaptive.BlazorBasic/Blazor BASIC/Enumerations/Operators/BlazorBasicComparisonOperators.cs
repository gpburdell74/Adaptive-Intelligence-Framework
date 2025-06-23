namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Lists the types of comparison operators that are supported.
/// </summary>
public enum BlazorBasicComparisonOperators
{
    /// <summary>
    /// Indicates an unknown operator.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Indicates the equal to operator (==)
    /// </summary>
    EqualTo,
    /// <summary>
    /// Indicates the greater than operator (&gt;).
    /// </summary>
    GreaterThan,
    /// <summary>
    /// Indicates the greater than or equal to operator (&gt;=).
    /// </summary>
    GreaterThanOrEqualTo,
    /// <summary>
    /// Indicates the less than operator (&lt;).
    /// </summary>
    LessThan,
    /// <summary>
    /// Indicates the less than or equal to operator (&lt;=).
    /// </summary>
    LessThanOrEqualTo,
    /// <summary>
    /// Indicates the not equal to operator (!=).
    /// </summary>
    NotEqualTo
}
