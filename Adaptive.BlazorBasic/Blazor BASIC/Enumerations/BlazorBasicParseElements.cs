namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Lists the parsing elements.
/// </summary>
public enum BlazorBasicParseElements
{
    /// <summary>
    /// Indicates an unknown element.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The character for carriage return (CR).
    /// </summary>
    CharCarriageReturn,
    /// <summary>
    /// The character for linefeed (LF)
    /// </summary>
    CharLinefeed,
    /// <summary>
    /// The character for SPACE.
    /// </summary>
    CharSpace,
    /// <summary>
    /// The character for TAB.
    /// </summary>
    CharTab,
    /// <summary>
    /// The carriage return (CR) character as as string.
    /// </summary>
    CarriageReturn,
    /// <summary>
    /// The double quote string.
    /// </summary>
    DoubleQuote,
    /// <summary>
    /// A double space as a string.
    /// </summary>
    DoubleSpace,
    /// <summary>
    /// The linefeed (LF) character as as string.
    /// </summary>
    Linefeed,
    /// <summary>
    /// The number/pound sign.
    /// </summary>
    NumberSign,
    /// <summary>
    /// The space character as as string.
    /// </summary>
    Space,
    /// <summary>
    /// The TAB character as as string.
    /// </summary>
    Tab
}
