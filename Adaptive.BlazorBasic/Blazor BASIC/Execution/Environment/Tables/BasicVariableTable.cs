using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Contains and manages instantiated variables within a scope.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IVariableTable" />
public sealed class BasicVariableTable : BasicContainerTable<IVariable>, IVariableTable
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariableTable"/> class.
    /// </summary>
    /// <param name="parent">
    /// The reference to the <see cref="IScopeContainer"/> parent instance.
    /// </param>
    public BasicVariableTable(IScopeContainer? parent) : base(parent)
    {
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Gets the reference to the variable instance with the specified name.
    /// </summary>
    /// <param name="name">
    /// A string containing the variable name to check for.
    /// </param>
    /// <returns>
    /// The <see cref="IVariable"/> instance, or <b>null</b> if not found.
    /// </returns>
    public IVariable? GetVariable(string? name)
    {
        return (IVariable?)base.GetItem(name);
    }
    #endregion
}
