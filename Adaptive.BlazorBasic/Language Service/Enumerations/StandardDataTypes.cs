namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Lists the standard data types the language service supports.
/// </summary>
public enum StandardDataTypes
{
    /// <summary>
    /// Indicates an unknown data type.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Indicates a boolean data type.
    /// </summary>
    Boolean,
    /// <summary>
    /// Indicates a byte data type.
    /// </summary>
    Byte,
    /// <summary>
    /// Indicates a character data type.
    /// </summary>
    Char,
    /// <summary>
    /// Indicates a 16-bit integer data type.
    /// </summary>
    ShortInteger,
    /// <summary>
    /// Indicates a 32-bit integer data type.
    /// </summary>
    Integer,
    /// <summary>
    /// Indicates a 64-bit integer data type.
    /// </summary>
    LongInteger,
    /// <summary>
    /// Indicates a single-precision floating-point data type.
    /// </summary>
    Float,
    /// <summary>
    /// Indicates a double-precision floating-point data type.
    /// </summary>
    Double,
    /// <summary>
    /// Indicates a date data type.
    /// </summary>
    Date,
    /// <summary>
    /// Indicates a date and time data type.
    /// </summary>
    DateTime,
    /// <summary>
    /// Indicates a time data type.
    /// </summary>
    Time,
    /// <summary>
    /// Indicates a string data type.
    /// </summary>
    String,
    /// <summary>
    /// Indicates an object data type.
    /// </summary>
    Object

}
