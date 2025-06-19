namespace Adaptive.BlazorBasic.LanguageService.CodeDom;

/// <summary>
/// Provides the signature definition for a keyword expression.
/// </summary>
/// <seealso cref="ILanguageCodeExpression" />
public interface ILanguageKeywordExpression : ILanguageCodeExpression
{
    /// <summary>
    /// Gets or sets the keyword value of the expression.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the keyword value.
    /// </value>
    string Keyword { get; set; }
}
