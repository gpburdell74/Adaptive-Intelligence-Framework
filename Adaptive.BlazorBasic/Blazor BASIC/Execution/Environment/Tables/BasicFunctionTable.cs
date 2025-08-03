using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Contains and manages instantiated variables within a scope.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IFunctionTable" />
public sealed class BasicFunctionTable : BasicContainerTable<IFunction>, IFunctionTable
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicFunctionTable"/> class.
    /// </summary>
    /// <param name="parent">
    /// The reference to the <see cref="IScopeContainer"/> parent instance.
    /// </param>
    public BasicFunctionTable(IScopeContainer? parent) : base(parent)
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
    /// The <see cref="IFunction"/> instance, or <b>null</b> if not found.
    /// </returns>
    public IFunction? GetFunction(string? name)
    {
        return (IFunction?)base.GetItem(name);
    }
    #endregion
}
