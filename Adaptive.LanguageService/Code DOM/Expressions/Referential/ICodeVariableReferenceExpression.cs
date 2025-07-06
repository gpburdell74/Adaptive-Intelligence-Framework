namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that reference defined variables.
/// </summary>
/// <seealso cref="ICodeExpression" />
public interface ICodeVariableReferenceExpression : ICodeExpression
{
    /// <summary>
    /// Gets the expression defining the name of the variable.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeVariableNameExpression"/> instance containing the name of the variable.
    /// </value>
    ICodeVariableNameExpression? VariableName { get; }
}
