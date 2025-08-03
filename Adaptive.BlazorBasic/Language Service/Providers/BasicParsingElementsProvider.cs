using Adaptive.Intelligence.LanguageService.Providers;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of built-in functions.
/// </summary>
public sealed class BasicParsingElementsProvider : DisposableObjectBase, IParsingElementsProvider
{
    /// <summary>
    /// Renders the list of parsing element ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique ID values used for mapping ID to text content.
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public List<int> RenderParsingElementIds()
    {
        return new List<int>()
        {
            (int)BlazorBasicParseElements.CharCarriageReturn,
            (int)BlazorBasicParseElements.CharLinefeed,
            (int)BlazorBasicParseElements.CharSpace,
            (int)BlazorBasicParseElements.CharTab,
            (int)BlazorBasicParseElements.CarriageReturn,
            (int)BlazorBasicParseElements.DoubleQuote,
            (int)BlazorBasicParseElements.DoubleSpace,
            (int)BlazorBasicParseElements.Linefeed,
            (int)BlazorBasicParseElements.NumberSign,
            (int)BlazorBasicParseElements.Space,
            (int)BlazorBasicParseElements.Tab
        };
    }

    /// <summary>
    /// Renders the list of parsing elements for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique name values used for mapping text to ID values.
    /// </returns>
    public List<string> RenderParsingElements()
    {
        return new List<string>()
        {
            ParseConstants.CharCarriageReturn.ToString(),
            ParseConstants.CharLinefeed.ToString(),
            ParseConstants.CharSpace.ToString(),
            ParseConstants.CharTab.ToString(),
            ParseConstants.CarriageReturn,
            ParseConstants.DoubleQuote,
            ParseConstants.DoubleSpace,
            ParseConstants.Linefeed,
            ParseConstants.NumberSign,
            ParseConstants.Space,
            ParseConstants.Tab
        };
    }
}
