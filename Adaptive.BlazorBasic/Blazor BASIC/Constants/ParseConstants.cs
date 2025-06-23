using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the character and string constants used in parsing operations.
/// </summary>
internal static class ParseConstants
{
    #region Characters
    /// <summary>
    /// The character for carriage return (CR).
    /// </summary>
    public const char CharCarriageReturn = '\r';
    /// <summary>
    /// The character for linefeed (LF)
    /// </summary>
    public const char CharLinefeed = '\n';
    /// <summary>
    /// The character for SPACE.
    /// </summary>
    public const char CharSpace = Constants.SpaceChar;
    /// <summary>
    /// The character for TAB.
    /// </summary>
    public const char CharTab = Constants.TabChar;
    #endregion

    #region Character Strings
    /// <summary>
    /// The carriage return (CR) character as as string.
    /// </summary>
    public const string CarriageReturn = Constants.CarriageReturn;
    /// <summary>
    /// The double quote string.
    /// </summary>
    public const string DoubleQuote = "\"";
    /// <summary>
    /// A double space as a string.
    /// </summary>
    public const string DoubleSpace = Constants.TwoSpaces;
    /// <summary>
    /// The linefeed (LF) character as as string.
    /// </summary>
    public const string Linefeed = Constants.Linefeed;
    /// <summary>
    /// The number/pound sign.
    /// </summary>
    public const string NumberSign = "#";
    /// <summary>
    /// The space character as as string.
    /// </summary>
    public const string Space = Constants.Space;
    /// <summary>
    /// The TAB character as as string.
    /// </summary>
    public const string Tab = Constants.Tab;
    #endregion
}
