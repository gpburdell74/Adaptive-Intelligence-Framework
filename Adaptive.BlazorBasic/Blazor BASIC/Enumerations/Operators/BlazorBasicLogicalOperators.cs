namespace Adaptive.BlazorBasic;

/// <summary>
/// Lists the types of logic operators that are supported.
/// </summary>
public enum BlazorBasicLogicalOperators
{
    /// <summary>
    /// Indicates an unknown operator.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Indicates the AND operator (AND)
    /// </summary>
    And,
    /// <summary>
    /// Indicates the AND operator (&amp;&amp;)
    /// </summary>
    AndShort,
    /// <summary>
    /// Indicates the OR operator (OR)
    /// </summary>
    Or,
    /// <summary>
    /// Indicates the OR operator (||)
    /// </summary>
    OrShort,
    /// <summary>
    /// Indicates the NOT operator (NOT)
    /// </summary>
    Not,
    /// <summary>
    /// Indicates the NOT operator (!)
    /// </summary>
    NotShort
}
