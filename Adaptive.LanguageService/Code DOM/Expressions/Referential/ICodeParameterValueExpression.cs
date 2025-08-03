namespace Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

/// <summary>
/// Provides the signature definition for expressions that contain the run-time values
/// for function or procedure parameters.
/// </summary>
/// <seealso cref="ICodeExpression" />
public interface ICodeParameterValueExpression : ICodeParameterDefinitionExpression
{
    /// <summary>
    /// Gets or sets the original data.
    /// </summary>
    /// <value>
    /// The original data that was parsed from the source file.
    /// </value>
    object? OriginalData { get; set; }

    /// <summary>
    /// Gets the reference to the expression that renders / contains the value to be passed
    /// to the parameter.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeExpression"/> instance.
    /// </value>
    ICodeExpression? ValueExpression { get; }

    /// <summary>
    /// Sets the internal values based on the provided parent parameter definition.
    /// </summary>
    /// <param name="definitionExpression">
    /// The <see cref="ICodeParameterDefinitionExpression"/> instance defining the data type
    /// and parameter the value of the current instance belongs to.
    /// </param>
    void SetFromParameterDefinition(ICodeParameterDefinitionExpression definitionExpression);

    /// <summary>
    /// Sets the value expression from the provided data.
    /// </summary>
    /// <param name="originalData">
    /// The original data that was parsed from the source file.
    /// </param>
    void SetValueExpression(object originalData);
}
