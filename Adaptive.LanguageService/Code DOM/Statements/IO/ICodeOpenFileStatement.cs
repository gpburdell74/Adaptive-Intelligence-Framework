using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for instances that represent code statements for opening a
/// file.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ICodeOpenFileStatement : ICodeStatement 
{
    /// <summary>
    /// Gets the reference to the expression defining how to open and access the file.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeFileModeExpression"/> instance.
    /// </value>
    ICodeFileModeExpression? FileDirection { get; }

    /// <summary>
    /// Gets the reference to the expression defining the file handle.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeFileHandleExpression"/> instance.
    /// </value>
    ICodeFileHandleExpression? FileHandle { get; }

    /// <summary>
    /// Gets the reference to the expression defining the file path and name.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeFileNameExpression"/> instance.
    /// </value>
    ICodeFileNameExpression? FileName { get; }
}
