using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definitions for code statements that end loop code blocks
/// where the exit condition is evaluated at the end of the loop.
/// </summary>
/// <remarks>
/// This is for the end of loops like 
/// <code>
///     do {} while(condition) ;
/// </code> or 
/// <code>
/// DO ... LOOP UNTIL (condition)
/// </code>
/// </remarks>
/// <seealso cref="ICodeStatement" />
public interface ICodeLoopEndConditionalStatement : ICodeStatement
{
    /// <summary>
    /// Gets the reference for the test condition for exiting the loop.
    /// </summary>
    /// <value>
    /// The <see cref="ICodeConditionalExpression"/> expression to be evaluated to determine
    /// if the loop is to be executed.
    /// </value>
    ICodeConditionalExpression? Condition { get; }
}
