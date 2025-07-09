using Adaptive.Intelligence.LanguageService.CodeDom.Expressions;

namespace Adaptive.Intelligence.LanguageService.CodeDom.Statements;

/// <summary>
/// Provides the signature definition for code statements that invoke the execution of a procedure.
/// </summary>
/// <seealso cref="ICodeStatement" />
public interface ICodeProcedureCallStatement : ICodeStatement
{
    /// <summary>
    /// Gets the name of the procedure.
    /// </summary>
    /// <value>
    /// A string containing the name of the procedure.
    /// </value>
    string? Name { get; }

    /// <summary>
    /// Gets the reference to the expression containing the list of parameter values to be 
    /// passed in to the procedure.
    /// </summary>
    /// <value>
    /// An <see cref="ICodeParameterValueListExpression"/> instance containing the list.
    /// </value>
    public ICodeParameterValueListExpression? Parameters { get; }

    /// <summary>
    /// Gets the parameter count.
    /// </summary>
    /// <value>
    /// An integer specifying the number of parameters for the procedure.
    /// </value>
    int ParameterCount { get; }
}