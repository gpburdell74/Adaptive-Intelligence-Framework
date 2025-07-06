using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom;

/// <summary>
/// Provides the signature definition for instances that represent single-line code statements and may contain code expressions.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ILanguageCodeStatement : ICodeObject
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
    /// A <see cref="List{T}"/> of <see cref="ICodeExpression"/> instances containing the remainder
    /// of the expressions in the remaining statement code.
    /// </value>
    List<ICodeExpression>? Expressions { get; }

    /// <summary>
    /// Gets the state of the tab / indentions when rendering.
    /// </summary>
    /// <value>
    /// A <see cref="RenderTabState"/> indicating how to modify the indentation when rendering.
    /// </value>
    RenderTabState TabModification { get; }

    /// <summary>
    /// Renders the content of the expression into a string.
    /// </summary>
    /// <returns>
    /// A string containing the expression rendered into Blazor BASIC code.
    /// </returns>
    string? Render();

}
