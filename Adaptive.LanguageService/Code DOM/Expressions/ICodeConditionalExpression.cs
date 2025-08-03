namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that evaluate a logical condition
/// within the code.
/// </summary>
/// <remarks>
/// This is  the expression for IF (condition) or WHILE (condition) or LOOP UNTIL (condition)
/// statements.
/// </remarks>
/// <seealso cref="ICodeExpression" />
public interface ICodeConditionalExpression : ICodeExpression
{
    #region Public Properties
    /// <summary>
    /// Gets or sets the reference to the left-side expression.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeExpression"/> to be evaluated as the left-side of the conditional.
    /// </value>
    ICodeExpression? LeftExpression { get; set; }

    /// <summary>
    /// Gets or sets the comparison operator.
    /// </summary>
    /// <value>
    /// A <see cref="StandardOperators"/> enumerated value indicating the operator to use.
    /// </value>
    StandardOperators Operator { get; set; }

    /// <summary>
    /// Gets or sets the reference to the right-side expression.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeExpression"/> to be evaluated as the right-side of the conditional.
    /// </value>
    ICodeExpression? RightExpression { get; set; }
    #endregion
}