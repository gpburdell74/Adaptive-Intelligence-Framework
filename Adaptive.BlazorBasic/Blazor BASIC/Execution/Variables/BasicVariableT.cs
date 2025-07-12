using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Execution;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the base implementation for variable definitions to contain specific types of data.
/// </summary>
/// <typeparam name="T">
/// The data type of the variable value.
/// </typeparam>
/// <seealso cref="BasicVariable" />
/// <seealso cref="IVariable{T}" />
public abstract class BasicVariable<T> : BasicVariable, IVariable<T>
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicVariable{T}"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="dataType">Type of the data.</param>
    /// <param name="isArray">if set to <c>true</c> [is array].</param>
    /// <param name="size">The size.</param>
    protected BasicVariable(string name, StandardDataTypes dataType, bool isArray, int size) : base(name,
        dataType, isArray, size)
    {
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        Value = default(T);
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the value of the variable.
    /// </summary>
    /// <value>
    /// A <typeparamref name="T" /> value for the variable.
    /// </value>
    public T? Value { get; set; }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Performs the appropriate conversion of the source value to the variable's type.
    /// </summary>
    /// <param name="sourceValue">The source value to be converted.</param>
    /// <returns>
    /// The value of <paramref name="sourceValue" /> converted to the variable's type, or null if the conversion is not possible.
    /// </returns>
    public abstract T? Convert(object? sourceValue);

    /// <summary>
    /// Gets the value of the variable.
    /// </summary>
    /// <returns>
    /// The value of the variable, or null if it is not set.
    /// </returns>
    public T? GetValueAsType()
    {
        return (T?)(object?)Value;
    }
    /// <summary>
    /// Gets the value stored in the variable.
    /// </summary>
    /// <returns>
    /// The content of the variable, or <c>null</c> if the variable is not set.
    /// </returns>
    public override object? GetValue()
    {
        return Value;
    }
    /// <summary>
    /// Sets the value of the variable instance. This method allows for updating the variable's content.
    /// </summary>
    /// <param name="value">The value to be stored in the variable.</param>
    public override void SetValue(object value)
    {
        Value = Convert(value);
    }
    /// <summary>
    /// Sets the value of the variable.
    /// </summary>
    /// <param name="value">The value to be stored in the variable.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void SetValueFromType(T? value)
    {
        Value = value;
    }
    #endregion
}
