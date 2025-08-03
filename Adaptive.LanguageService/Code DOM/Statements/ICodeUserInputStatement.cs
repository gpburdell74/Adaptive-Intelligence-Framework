using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for code statements that prompt the user and collect 
/// input from the user.
/// </summary>
/// <seealso cref="ICodeStatement" />
public interface ICodeUserInputStatement : ICodeStatement
{
    #region Public Properties
    /// <summary>
    /// Gets the reference to the prompt expression.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeExpression"/> instance, used to render a prompt to a user.
    /// </value>
    public ICodeExpression? PromptExpression { get; }

    /// <summary>
    /// Gets the reference to the expression defining the variable to store the result into.
    /// </summary>
    /// <value>
    /// A <see cref="ICodeVariableReferenceExpression"/> instance specifying the variable reference, or <b>null</b> if not used.
    /// </value>
    public ICodeVariableReferenceExpression? VariableReferenceExpression { get; }
    #endregion
}
