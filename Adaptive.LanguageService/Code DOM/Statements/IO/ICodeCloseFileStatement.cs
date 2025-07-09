using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for instances that represent code statements for closing a
/// file.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ICodeCloseFileStatement : ICodeStatement
{
    /// <summary>
    /// Gets the reference to the file handle expression.
    /// </summary>
    /// <value>
    /// A <see cref="ICodeFileHandleExpression"/> instance specifying the file handle.
    /// </value>
    ICodeFileHandleExpression? FileHandleExpression { get; }
}
