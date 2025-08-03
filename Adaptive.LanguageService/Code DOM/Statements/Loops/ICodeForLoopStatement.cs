using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definitions for code statements that start code blocks
/// as for loops.
/// </summary>
/// <remarks>
/// This is for the start of loops like 
/// <code>
///     for (;;) {}
/// </code> or 
/// <code>
///     FOR ... NEXT
/// </code>
/// </remarks>
/// <seealso cref="ICodeStatement" />
/// <seealso cref="ICodeLoopStartStatement"/>
public interface ICodeForLoopStatement : ICodeLoopStartStatement
{
    /// <summary>
    /// Gets the reference to the expression that defines the lower limit / starting position
    /// for the for loop.
    /// </summary>
    /// <remarks>
    /// This is the start value to be assigned in the for loop.  For Example, in 
    /// "FOR X = 1 TO 1000" this represents the 1 value.
    /// </remarks>
    /// <value>
    /// The <see cref="ICodeExpression"/> instance.
    /// </value>
    ICodeExpression? LowerLimitExpression { get; }

    /// <summary>
    /// Gets the reference to the expression that defines the upper limit / ending position
    /// for the for loop.
    /// </summary>
    /// <remarks>
    /// This is the start value to be assigned in the for loop.  For Example, in 
    /// "FOR X = 1 TO 1000" this represents the 1000 value.
    /// </remarks>
    /// <value>
    /// The <see cref="ICodeExpression"/> instance.
    /// </value>
    ICodeExpression? UpperLimitExpression { get; }

    /// <summary>
    /// Gets the expression that defines the variable reference for the loop;
    /// </summary>
    /// <remarks>
    /// This is the variable to be manipulated in the for loop.  For Example, in 
    /// "FOR X = 1 TO 1000" this represents the "X" variable reference.
    /// </remarks>
    /// <value>
    /// The <see cref="ICodeVariableReferenceExpression"/> instance.
    /// </value>
    ICodeVariableReferenceExpression? VariableReferenceExpression { get; }
}
