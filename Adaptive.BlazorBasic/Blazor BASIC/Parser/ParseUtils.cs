namespace Adaptive.BlazorBasic.Parser;

/// <summary>
/// Provides common utility methods and functions for parsing operations.
/// </summary>
public static class ParseUtils
{
    /// <summary>
    /// Removes unnecessary whitespace from the provided line of code.
    /// </summary>
    /// <param name="original">
    /// A string containing the original line of code.
    /// </param>
    /// <returns>
    /// A string containing the trimmed content, or <b>null</b>.
    /// </returns>
    public static string? TrimCodeLine(string? original)
    {
        string? trimmed = null;

        if (!string.IsNullOrEmpty(original))
        {
            trimmed = original
                       .Replace(ParseConstants.Tab, ParseConstants.Space)
                       .Replace(ParseConstants.CarriageReturn, ParseConstants.Space)
                       .Replace(ParseConstants.Linefeed, ParseConstants.Space)
                       .Replace(ParseConstants.DoubleSpace, ParseConstants.Space)
                       .Replace(ParseConstants.DoubleSpace, ParseConstants.Space)
                       .Trim();
        }

        return trimmed;
    }
    /// <summary>
    /// Normalizes the provided string to be used as a key value in a dictionary.
    /// </summary>
    /// <param name="original">
    /// A string containing the original value.
    /// </param>
    /// <returns>
    /// The original string trimmed and made all lowercase.
    /// </returns>
    public static string NormalizeAsKey(string original)
    {
        return original.Trim().ToLower();
    }
}
