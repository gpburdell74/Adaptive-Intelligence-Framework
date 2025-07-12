using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Contains and manages instantiated variables within a scope.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IParameterTable" />
public sealed class BlazorBasicParameterTable : BasicContainerTable<IParameter>, IParameterTable
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicParameterTable"/> class.
    /// </summary>
    /// <param name="parent">
    /// The reference to the <see cref="IScopeContainer"/> parent instance.
    /// </param>
    public BlazorBasicParameterTable(IScopeContainer? parent) : base(parent)
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the name of the instance.
    /// </summary>
    /// <value>
    /// A string containing the name of the instance.
    /// </value>
    public string? Name { get; }
    #endregion

    #region Public Methods / Parameters
    /// <summary>
    /// Adds the specified parameter.
    /// </summary>
    /// <param name="parameter">
    /// The <see cref="BasicParameter"/> instance to add to the list.
    /// </param>
    public void Add(BasicParameter parameter)
    {
        base.Add(parameter);
    }

    /// <summary>
    /// Creates the parameter object and adds to the table with the specified name and data type.
    /// </summary>
    /// <param name="scope">
    /// The <see cref="IScopeContainer"/> instance the parameter is defined for.
    /// </param>
    /// <param name="parameterName">
    /// A string containing the name of the parameter.
    /// </param>
    /// <param name="dataType">
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the type of the data.
    /// </param>
    public void Add(IScopeContainer scope, string parameterName, StandardDataTypes dataType)
    {
        BasicParameter parameter = new BasicParameter(scope, parameterName, dataType);
        base.Add(parameter);
    }

    /// <summary>
    /// Gets the reference to the variable instance with the specified name.
    /// </summary>
    /// <param name="name">
    /// A string containing the variable name to check for.
    /// </param>
    /// <returns>
    /// The <see cref="IParameter"/> instance, or <b>null</b> if not found.
    /// </returns>
    public IParameter? GetParameter(string? name)
    {
        return (IParameter?)base.GetItem(name);
    }
    #endregion
}
