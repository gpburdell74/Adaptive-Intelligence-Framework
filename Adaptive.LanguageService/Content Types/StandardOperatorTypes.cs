namespace Adaptive.LanguageService;

/// <summary>
/// Lists the standard operator types the language service supports.
/// </summary>
public enum StandardOperatorTypes
{
    /// <summary>
    /// Indicates an unknown operator type.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Indicates an arithmetic operator type (such as + - * / ).
    /// </summary>
    Arithmetic,
    /// <summary>
    /// Indicates an assignment operator type (such as = or :=)
    /// </summary>
    Assignment,
    /// <summary>
    /// Indicates an bitwise operator type (such as & or |).
    /// </summary>
    Bitwise,
    /// <summary>
    /// Indicates a comparison operator type (such as &gt; &lt; == etc.) 
    /// </summary>
    Comparison,
    /// <summary>
    /// Indicates an decrement operator type (such as --).
    /// </summary>
    Decrement,
    /// <summary>
    /// Indicates an increment operator type (such as ++).
    /// </summary>
    Increment,
    /// <summary>
    /// Indicates an logical operator type (such as AND, OR, NOT, &&, ||, !)
    /// </summary>
    Logical,
    /// <summary>
    /// Indicates another type of operator.
    /// </summary>
    Other
}
