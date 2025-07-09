using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for code statements that mark and define the start
/// of a procedure definition.
/// </summary>
/// <seealso cref="ICodeStatement" />
public interface ICodeProcedureStartStatement : ICodeStatement
{
    /// <summary>
    /// Gets the name of the procedure.
    /// </summary>
    /// <value>
    /// A string containing the name of the procedure.
    /// </value>
    string? Name { get; }

    /// <summary>
    /// Gets the reference to the expression containing the list of parameters.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeParameterDefinitionListExpression"/> instance containing the list.
    /// </value>
    public ICodeParameterDefinitionListExpression? Parameters { get; }

    /// <summary>
    /// Gets the parameter count.
    /// </summary>
    /// <value>
    /// An integer specifying the number of parameters for the procedure.
    /// </value>
    int ParameterCount { get; }
}