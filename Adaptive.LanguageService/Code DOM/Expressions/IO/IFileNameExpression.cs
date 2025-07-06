namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for an expression defining or referencing a file name.
/// </summary>
/// <seealso cref="ICodeExpression" />
public interface IFileNameExpression : ICodeExpression
{
    /// <summary>
    /// Gets or sets the path and name of the file.
    /// </summary>
    /// <value>
    /// A string containing the fully-qualified path and name of the file, or <b>null</b> if not specified.
    /// </value>
    string? FileName { get; set; }
}
