namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for an expression that contains and manages a list
/// of expressions that define the values to assign to the parameters for a procedure or function.
/// </summary>
/// <seealso cref="ICodeExpression" />
public interface ICodeParameterValueListExpression : ICodeExpression
{
    #region Public Properties
    /// <summary>
    /// Gets the number of parameter expressions.
    /// </summary>
    /// <value>
    /// An integer containing the number of expressions.
    /// </value>
    int Count { get; }

    /// <summary>
    /// Gets the reference to the list of parameter definitions.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="ICodeParameterDefinitionExpression"/> instances.
    /// </value>
    public List<ICodeParameterValueExpression>? ParameterList { get; }
    #endregion
}