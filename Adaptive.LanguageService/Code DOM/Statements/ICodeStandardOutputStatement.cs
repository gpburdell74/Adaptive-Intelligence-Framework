using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for code statements that write to the standard output mechanism.
/// </summary>
public interface ICodeStandardOutputStatement : ICodeStatement
{
    /// <summary>
    /// Gets the reference to the expression that, when evaluated, renders the content to be 
    /// written to standard output.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeExpression"/> instance defining the output.
    /// </value>
    ICodeExpression? WriteExpression { get; }
}
