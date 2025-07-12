using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Contains and manages instantiated procedures within a scope.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IProcedureTable" />
public sealed class BlazorBasicProcedureTable : BasicContainerTable<IProcedure>, IProcedureTable
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicProcedureTable"/> class.
    /// </summary>
    /// <param name="parent">
    /// The reference to the <see cref="IScopeContainer"/> parent instance.
    /// </param>
    public BlazorBasicProcedureTable(IScopeContainer? parent) : base(parent)
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
    /// The <see cref="IProcedure"/> instance, or <b>null</b> if not found.
    /// </returns>
    public IProcedure? GetProcedure(string? name)
    {
        return (IProcedure?)base.GetItem(name);
    }
    #endregion
}
