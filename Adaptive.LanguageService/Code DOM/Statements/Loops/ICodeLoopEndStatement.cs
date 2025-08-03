namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definitions for code statements that end loop code blocks
/// where the exit condition is evaluated at the start of the loop.
/// </summary>
/// <remarks>
/// This is for the end of loops like 
/// <code>
///     while (condition) {} ;
/// </code> or 
/// <code>
///     WHILE (condition) ... WEND
/// </code>
/// </remarks>
/// <seealso cref="ICodeStatement" />
public interface ICodeLoopEndStatement : ICodeStatement
{
}
