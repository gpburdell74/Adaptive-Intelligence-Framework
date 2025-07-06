namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for a keyword expression.
/// </summary>
/// <seealso cref="ICodeExpression" />
public interface ILanguageKeywordExpression : ICodeExpression
{
    /// <summary>
    /// Gets or sets the keyword value of the expression.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the keyword value.
    /// </value>
    string Keyword { get; set; }
}
