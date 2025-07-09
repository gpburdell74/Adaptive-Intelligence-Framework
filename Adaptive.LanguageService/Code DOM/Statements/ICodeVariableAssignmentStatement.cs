using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for code statements that assign values to variables.
/// </summary>
/// <seealso cref="ICodeStatement" />
public interface ICodeVariableAssignmentStatement : ICodeStatement
{
    #region Public Properties 
    /// <summary>
    /// Gets the reference to the expression defining the value to be assigned.
    /// </summary>
    /// <value>
    /// A <see cref="ICodeExpression"/> instance representing the value.
    /// </value>
    public ICodeExpression? Expression { get; }
    /// <summary>
    /// Gets the reference to the expression providing the reference to an instantiated variable.
    /// </summary>
    /// <value>
    /// A <see cref="ICodeVariableReferenceExpression"/> defining the variable name/reference.
    /// </value>
    public ICodeVariableReferenceExpression? VariableReference { get; }
    #endregion
}
