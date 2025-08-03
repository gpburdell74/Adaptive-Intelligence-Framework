using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definitions for code statements that start loop code blocks
/// where the exit condition is evaluated at the start of the loop.
/// </summary>
/// <remarks>
/// This is for the end of loops like 
/// <code>
///     while (condition) {};
/// </code> or 
/// <code>
///     WHILE (condition) ... WEND
/// </code>
/// </remarks>
/// <seealso cref="ICodeLoopStartStatement"/>
/// <seealso cref="ICodeStatement"/>
public interface ICodeLoopStartConditionalStatement : ICodeLoopStartStatement
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
