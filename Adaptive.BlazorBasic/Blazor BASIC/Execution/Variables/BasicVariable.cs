using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the base implementation to represent a variable with a value of a specified data type.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IVariable" />
public class BasicVariable : DisposableObjectBase, IVariable
{
    #region Private Member Declarations
    /// <summary>
    /// The name.
    /// </summary>
    private string? _name;
    /// <summary>
    /// The data type.
    /// </summary>
    private StandardDataTypes _dataType = StandardDataTypes.Unknown;
    /// <summary>
    /// The is array flag.
    /// </summary>
    private bool _isArray;
    /// <summary>
    /// The array size indicator.
    /// </summary>
    private int _size;

    /// <summary>
    /// The value.
    /// </summary>
    private object? _value;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariable"/> class.
    /// </summary>
    /// <param name="name">
    /// A string containing the variable name.
    /// </param>
    /// <param name="dataType">
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the data type.
    /// </param>
    /// <param name="isArray">
    /// <b>true</b> if the variable is an array.
    /// </param>
    /// <param name="size">
    /// An integer specifying the size of the array.
    /// </param>
    public BasicVariable(string name, StandardDataTypes dataType, bool isArray, int size)
    {
        _name = name;
        _isArray = isArray;
        _dataType = dataType;
        _size = size;
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _name = null;
        _value = null;
        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the type of the data.
    /// </summary>
    /// <value>
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the data type of the variable.
    /// </value>
    public StandardDataTypes DataType => _dataType;
    /// <summary>
    /// Gets a value indicating whether this variable is defined and populated from a procedure or
    /// function parameter.
    /// </summary>
    /// <value>
    /// <c>true</c> if variable is defined and populated from a procedure or
    /// function parameter; otherwise, <c>false</c>.
    /// </value>
    public bool IsParameter { get; set; }
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
    public IScopeContainer? Parent { get; }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Gets the value stored in the variable.
    /// </summary>
    /// <returns>
    /// The content of the variable, or <c>null</c> if the variable is not set.
    /// </returns>
    public virtual  object? GetValue()
    {
        return _value;
    }

    /// <summary>
    /// Sets the value of the variable instance. This method allows for updating the variable's content.
    /// </summary>
    /// <param name="value">The value to be stored in the variable.</param>
    public virtual void SetValue(object value)
    {
        _value = value;
    }
    #endregion

}
