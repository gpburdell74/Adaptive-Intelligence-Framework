using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;
using Newtonsoft.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Adaptive.Intelligence.BlazorBasic;

/// <summary>
/// Provides the base implementation for variable definitions to contain specific types of data.
/// </summary>
/// <typeparam name="T">
/// The data type of the variable value.
/// </typeparam>
/// <seealso cref="BlazorBasicVariable" />
/// <seealso cref="IVariable{T}" />
public class BlazorBasicVariable<T> : BlazorBasicVariable, IVariable<T>
{
    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="BlazorBasicVariable{T}"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="dataType">Type of the data.</param>
    /// <param name="isArray">if set to <c>true</c> [is array].</param>
    /// <param name="size">The size.</param>
    protected BlazorBasicVariable(string name, StandardDataTypes dataType, bool isArray, int size) : base(name,
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

    public void SetValue(T value)
    {
        Value = value;
    }
    public override object? GetValue()
    {
        return Value;
    }
    public override void SetValue(object value)
    {
        if (value is string stringData)
        {
            switch (DataType)
            {
                case StandardDataTypes.Boolean:
                    Value = (T)(object)bool.Parse(stringData);
                    break;

                case StandardDataTypes.Byte:
                    Value = (T)(object)byte.Parse(stringData);
                    break;

                case StandardDataTypes.Char:
                    Value = (T)(object)char.Parse(stringData);
                    break;

                case StandardDataTypes.ShortInteger:
                    Value = (T)(object)short.Parse(stringData);
                    break;

                case StandardDataTypes.Integer:
                    Value = (T)(object)int.Parse(stringData);
                    break;

                case StandardDataTypes.LongInteger:
                    Value = (T)(object)long.Parse(stringData);
                    break;

                case StandardDataTypes.Float:
                    Value = (T)(object)float.Parse(stringData);
                    break;

                case StandardDataTypes.Double:
                    Value = (T)(object)double.Parse(stringData);
                    break;

                case StandardDataTypes.Date:
                    Value = (T)(object)DateTime.Parse(stringData).Date;
                    break;

                case StandardDataTypes.DateTime:
                    Value = (T)(object)DateTime.Parse(stringData);
                    break;

                case StandardDataTypes.Time:
                    Value = (T)(object)Time.Parse(stringData);
                    break;

                case StandardDataTypes.String:
                    Value = (T)(object)stringData;
                    break;

                case StandardDataTypes.Object:
                    break;
            }
        }
        else
            Value = (T)(object)value;
    }
}
