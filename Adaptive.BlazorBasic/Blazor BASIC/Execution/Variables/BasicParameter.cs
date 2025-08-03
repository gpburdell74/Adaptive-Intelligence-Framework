using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Represents a parameter instance within the scope of a procedure or function.
/// </summary>
/// <seealso cref="IScopedElement" />
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IParameter" />
public sealed class BasicParameter : DisposableObjectBase, IParameter
{
    #region Private Member Declarations
    /// <summary>
    /// The parameter name.
    /// </summary>
    private string _name;

    /// <summary>
    /// The parent container.
    /// </summary>
    private IScopeContainer? _parent;

    /// <summary>
    /// The data type.
    /// </summary>
    private StandardDataTypes _dataType;

    /// <summary>
    /// The value assigned to the parameter.
    /// </summary>
    private object? _value;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicParameter"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the name of the parameter.
    /// </param>
    /// <param name="dataType">
    /// The data type of the value stored in the parameter.
    /// </param>
    public BasicParameter(string name, StandardDataTypes dataType)
    {
        _name = name;
        _dataType = dataType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicParameter"/> class.
    /// </summary>
    /// <param name="scope">
    /// The <see cref="IScopeContainer"/> instance pointing to the procedure or
    /// function the parameter is defined for.
    /// </param>
    /// <param name="name">
    /// A string containing the name of the parameter.
    /// </param>
    /// <param name="dataType">
    /// The data type of the value stored in the parameter.
    /// </param>
    public BasicParameter(IScopeContainer scope, string name, StandardDataTypes dataType) :
        this(name, dataType)
    {
        _parent = scope;
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _name = null;
        _parent = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the data type of the value in the parameter.
    /// </summary>
    /// <value>
    /// A <see cref="StandardDataTypes" /> enumerated value indicating the data type.
    /// </value>
    public StandardDataTypes DataType => _dataType;

    /// <summary>
    /// Gets the name of the variable instance.
    /// </summary>
    /// <value>
    /// A string containing the name of the variable.
    /// </value>
    public string? Name => _name;

    /// <summary>
    /// Gets the reference to the parent scope container.
    /// </summary>
    /// <value>
    /// A reference to the parent <see cref="IScopeContainer" /> instance.
    /// </value>
    public IScopeContainer? Parent => _parent;
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Gets the value stored in the variable.
    /// </summary>
    /// <returns>
    /// The content of the variable, or <c>null</c> if the variable is not set.
    /// </returns>
    public object? GetValue()
    {
        return _value;
    }
    /// <summary>
    /// Sets the value of the variable instance. This method allows for updating the variable's content.
    /// </summary>
    /// <param name="value">The value to be stored in the variable.</param>
    public void SetValue(object value)
    {
        _value = value;
    }
    #endregion
}
