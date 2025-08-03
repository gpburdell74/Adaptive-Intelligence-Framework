namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that represents a literal value.
/// </summary>
/// <typeparam name="T">
/// The data type of the value being stored in and expressed by the expression.
/// </typeparam>
public interface ICodeLiteralExpression<T> : ICodeExpression
{
    #region Public Properties
    /// <summary>
    /// Gets or sets the literal value.
    /// </summary>
    /// <value>
    /// A <typeparamref name="T"/> containing the literal value being represented by this expression.
    /// </value>
    T? Value { get; set; }
    #endregion
}