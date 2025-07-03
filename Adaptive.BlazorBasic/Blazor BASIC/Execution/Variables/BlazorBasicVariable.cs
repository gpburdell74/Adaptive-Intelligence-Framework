using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the base implementation to represent a variable with a value of a specified data type.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IVariable" />
public class BlazorBasicVariable : DisposableObjectBase, IVariable
{
    #region Private Member Declarations

    private string? _name;
    private int _id;
    private StandardDataTypes _dataType = StandardDataTypes.Unknown;
    private bool _isArray;
    private int _size;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicVariable"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="dataType">Type of the data.</param>
    /// <param name="isArray">if set to <c>true</c> [is array].</param>
    /// <param name="size">The size.</param>
    public BlazorBasicVariable(string name, StandardDataTypes dataType, bool isArray, int size)
    {
        _name = name;
        _id = IdGenerator.Next();
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
        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets a unique Id value for the variable within the scope.
    /// </summary>
    /// <value>
    /// An integer indicating the ID of the instance.
    /// </value>
    public int Id => _id;
    /// <summary>
    /// Gets or sets a unique Id value for the variable within the scope.
    /// </summary>
    /// <value>
    /// An integer indicating the ID of the instance.
    /// </value>
    int IVariable.Id { get; set; }
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

    public StandardDataTypes DataType => _dataType;

    #endregion

    public virtual object? GetValue()
    {
        return null;
    }
    public virtual void SetValue(object value)
    {
    }


}
