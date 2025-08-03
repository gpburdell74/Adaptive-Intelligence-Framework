using Adaptive.Intelligence.LanguageService;
using Adaptive.Intelligence.LanguageService.Providers;
using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of data types.
/// </summary>
public sealed class BasicDataTypeProvider : DisposableObjectBase, IDataTypeProvider
{
    #region Private Member Declarations
    /// <summary>
    /// The type list.
    /// </summary>
    private Dictionary<string, Type>? _typeList;
    #endregion

    #region Constructor / Dispose Method    
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDataTypeProvider"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicDataTypeProvider()
    {
        Initialize();
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _typeList?.Clear();
        }

        _typeList = null;
        base.Dispose(disposing);
    }
    #endregion

    /// <summary>
    /// Initializes the content of the provider.
    /// </summary>
    public void Initialize()
    {
        _typeList = new Dictionary<string, Type>(20);

        _typeList.Add(DataTypeNames.TypeBool, typeof(bool));
        _typeList.Add(DataTypeNames.TypeByte, typeof(byte));
        _typeList.Add(DataTypeNames.TypeChar, typeof(char));
        _typeList.Add(DataTypeNames.TypeShort, typeof(short));
        _typeList.Add(DataTypeNames.TypeInteger, typeof(int));
        _typeList.Add(DataTypeNames.TypeLong, typeof(long));
        _typeList.Add(DataTypeNames.TypeFloat, typeof(float));
        _typeList.Add(DataTypeNames.TypeDouble, typeof(double));
        _typeList.Add(DataTypeNames.TypeDate, typeof(DateTime));
        _typeList.Add(DataTypeNames.TypeDateTime, typeof(DateTime));
        _typeList.Add(DataTypeNames.TypeTime, typeof(Time));
        _typeList.Add(DataTypeNames.TypeString, typeof(string));
        _typeList.Add(DataTypeNames.TypeObject, typeof(object));
    }

    /// <summary>
    /// Maps the name of the data type to a .NET data type.
    /// </summary>
    /// <param name="typeName">A string containing the name of the type to be mapped.</param>
    /// <returns>
    /// A <see cref="T:System.Type" /> instance if successful;otherwise, returns <b>null</b>.
    /// </returns>
    public Type? MapToDotNetType(string typeName)
    {
        Type? netType = null;

        if (_typeList != null && _typeList.ContainsKey(typeName))
            netType = _typeList[typeName];

        return netType;
    }

    /// <summary>
    /// Renders the list of data type ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique ID values used for mapping ID to text content.
    /// </returns>
    public List<int> RenderDataTypeIds()
    {
        return new List<int>
        { 
            (int)StandardDataTypes.Boolean,
            (int)StandardDataTypes.Byte,
            (int)StandardDataTypes.Char,
            (int)StandardDataTypes.ShortInteger,
            (int)StandardDataTypes.Integer,
            (int)StandardDataTypes.LongInteger,
            (int)StandardDataTypes.Float,
            (int)StandardDataTypes.Double,
            (int)StandardDataTypes.Date,
            (int)StandardDataTypes.DateTime,
            (int)StandardDataTypes.Time,
            (int)StandardDataTypes.String,
            (int)StandardDataTypes.Object

        };
    }

    /// <summary>
    /// Renders the data type names.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of strings containing the data type names.
    /// </returns>
    public List<string> RenderDataTypeNames()
    {
        return new List<string>
        {
            DataTypeNames.TypeBool,
            DataTypeNames.TypeByte,
            DataTypeNames.TypeChar,
            DataTypeNames.TypeShort,
            DataTypeNames.TypeInteger,
            DataTypeNames.TypeLong ,
            DataTypeNames.TypeFloat ,
            DataTypeNames.TypeDouble,
            DataTypeNames.TypeDate,
            DataTypeNames.TypeDateTime,
            DataTypeNames.TypeTime,
            DataTypeNames.TypeString,
            DataTypeNames.TypeObject
        };
    }
}
