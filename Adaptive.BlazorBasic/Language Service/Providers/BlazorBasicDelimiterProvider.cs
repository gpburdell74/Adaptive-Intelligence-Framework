using Adaptive.Intelligence.LanguageService.Providers;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of built-in functions.
/// </summary>
public sealed class BlazorBasicDelimiterProvider : DisposableObjectBase, IDelimiterProvider
{
    /// <summary>
    /// Renders the list of delimiter ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique ID values used for mapping ID to text content.
    /// </returns>
    public List<int> RenderDelimiterIds()
    {
        return new List<int>()
        {
            (int)BlazorBasicDelimiters.Space,
            (int)BlazorBasicDelimiters.Cr,
            (int)BlazorBasicDelimiters.Lf,
            (int)BlazorBasicDelimiters.Char,
            (int)BlazorBasicDelimiters.String,
            (int)BlazorBasicDelimiters.OpenParens,
            (int)BlazorBasicDelimiters.CloseParens,
            (int)BlazorBasicDelimiters.OpenBracket,
            (int)BlazorBasicDelimiters.CloseBracket,
            (int)BlazorBasicDelimiters.OpenBlockBracket,
            (int)BlazorBasicDelimiters.CloseBlockBracket,
            (int)BlazorBasicDelimiters.ListSeparator
        };
    }

    /// <summary>
    /// Renders the list of delimiter names / text for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique name values used for mapping text to ID values.
    /// </returns>
    public List<string> RenderDelimiterNames()
    {
        return new List<string>()
        {
            DelimiterNames.DelimiterSpace,
            DelimiterNames.DelimiterCr,
            DelimiterNames.DelimiterLf,
            DelimiterNames.DelimiterChar,
            DelimiterNames.DelimiterString,
            DelimiterNames.DelimiterOpenParens,
            DelimiterNames.DelimiterCloseParens,
            DelimiterNames.DelimiterOpenBracket,
            DelimiterNames.DelimiterCloseBracket,
            DelimiterNames.DelimiterOpenBlockBracket,
            DelimiterNames.DelimiterCloseBlockBracket,
            DelimiterNames.DelimiterListSeparator
        };
    }
}
