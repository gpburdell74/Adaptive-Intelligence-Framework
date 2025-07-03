using Adaptive.Intelligence.LanguageService.CodeDom;

namespace Adaptive.Intelligence.LanguageService.Execution;

/// <summary>
/// Provides the signature definition for procedure definitions and instances.
/// </summary>
/// <seealso cref="IScopeContainer" />
public interface IProcedure : IScopeContainer, ICodeItemInstance
{
    /// <summary>
    /// Gets the reference to the list of code statements.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="ILanguageCodeStatement"/> instances.
    /// </value>
    List<ILanguageCodeStatement> Code { get; }

    /// <summary>
    /// Gets the reference to the list of procedure parameters.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of <see cref="ILanguageCodeExpression"/> instances defining the 
    /// parameters for the procedure.
    /// </value>
    List<ILanguageCodeExpression> Parameters { get; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    string? Name { get; }

    /// <summary>
    /// Gets the reference to the list of variables defined within the procedure.
    /// </summary>
    /// <value>
    /// An <see cref="IVariableTable"/> implementation.
    /// </value>
    IVariableTable Variables { get; }
}
