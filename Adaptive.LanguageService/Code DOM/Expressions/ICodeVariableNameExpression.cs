namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that contain or render the name of a variable.
/// </summary>
/// <seealso cref="ICodeExpression" />
public interface ICodeVariableNameExpression : ICodeExpression 
{
    /// <summary>
    /// Gets the name/text of the variable being represented.
    /// </summary>
    /// <value>
    /// A string containing the name of the variable.
    /// </value>
    public string? VariableName { get; }
}
