namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for an expression defining or referencing a file 
/// handle value.
/// </summary>
/// <seealso cref="ICodeExpression" />
public interface ICodeFileHandleExpression : ICodeExpression
{
    /// <summary>
    /// Gets or sets the file number/handle value.
    /// </summary>
    /// <value>
    /// An integer containing the file number/handle value to use.
    /// </value>
    int FileHandle { get; set; }
}
