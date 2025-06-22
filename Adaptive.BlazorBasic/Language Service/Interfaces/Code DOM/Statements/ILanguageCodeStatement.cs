namespace Adaptive.BlazorBasic.LanguageService.CodeDom;

/// <summary>
/// Provides the signature definition for instances that represent single-line code statements and may contain code expressions.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ILanguageCodeStatement : ILanguageCodeObject
{
    /// <summary>
    /// Gets or sets the reference to the expression defining the command at the start of the line.
    /// </summary>
    /// <value>
    /// An <see cref="ILanguageKeywordExpression"/> containing the expression instance defining the command.
    /// </value>
    ILanguageKeywordExpression? CommandExpression { get; }

    /// <summary>
    /// Gets the reference to the list of expressions.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="ILanguageCodeExpression"/> instances containing the remainder
    /// of the expressions in the remaining statement code.
    /// </value>
    List<ILanguageCodeExpression>? Expressions { get; }
}
