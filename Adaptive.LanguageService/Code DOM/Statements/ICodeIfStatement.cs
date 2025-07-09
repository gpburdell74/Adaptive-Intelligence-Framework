using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for statements that start code blocks for conditional checking
/// and branching - such as IF statements.
/// </summary>
/// <seealso cref="ICodeStatement" />
public interface ICodeIfStatement : ICodeStatement
{
    /// <summary>
    /// Gets the reference to the expression that defines the initial comparison.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeConditionalExpression"/> defining the logical comparison operation.
    /// </value>
    ICodeConditionalExpression? ConditionExpression { get; }

    /// <summary>
    /// Gets the reference to the expression that defines the next comparison operation (else if).
    /// </summary>
    /// <value>
    /// An <see cref="ICodeConditionalExpression"/> defining the next comparison operation if the first
    /// operation fails.
    /// </value>
    ICodeConditionalExpression? ElseIfConditionExpression { get; }
}
