namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definitions for code statements that start loop code blocks
/// where the exit condition is evaluated at the start of the loop.
/// </summary>
/// <remarks>
/// This is for the start of loops (the "DO" part) as in:
/// <code>
///     do {} while() ;
/// </code> or 
/// <code>
/// DO ... LOOP UNTIL.
/// </code>
/// </remarks>
/// <seealso cref="ICodeStatement" />
public interface ICodeLoopStartStatement : ICodeStatement 
{
}
