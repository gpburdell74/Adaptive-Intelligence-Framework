namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for an expression defining or referencing a file 
/// operational mode value or values.
/// </summary>
/// <seealso cref="ICodeExpression" />
public interface IFileModeExpression : ICodeExpression
{
    /// <summary>
    /// Gets the file access setting.
    /// </summary>
    /// <value>
    /// A <see cref="FileAccess"/> enumerated value.
    /// </value>
    FileAccess Access { get; }
    /// <summary>
    /// Gets the file mode setting.
    /// </summary>
    /// <value>
    /// A <see cref="FileMode"/> enumerated value.
    /// </value>
    FileMode Mode { get; }
}