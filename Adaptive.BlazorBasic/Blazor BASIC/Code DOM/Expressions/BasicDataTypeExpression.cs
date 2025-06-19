using Adaptive.BlazorBasic.LanguageService;
using Adaptive.BlazorBasic.LanguageService.CodeDom;
using Adaptive.Intelligence.Shared;

namespace Adaptive.BlazorBasic.CodeDom;

/// <summary>
/// Represents a data type expression.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="ILanguageCodeExpression" />
public class BasicDataTypeExpression : DisposableObjectBase, ILanguageCodeExpression
{
    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDataTypeExpression"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public BasicDataTypeExpression()
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicDataTypeExpression"/> class.
    /// </summary>
    /// <param name="dataTypeSpec">
    /// A string containing the data type name.
    /// </param>
    public BasicDataTypeExpression(string dataTypeSpec) : base()
    {
        ParseDataTypeExpression(dataTypeSpec.Trim());
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets or sets the type of the data.
    /// </summary>
    /// <value>
    /// A <see cref="StandardDataTypes"/> enumerated value indicating the data type.
    /// </value>
    public StandardDataTypes DataType { get; set; } = StandardDataTypes.Unknown;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is array.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is an array; otherwise, <c>false</c>.
    /// </value>
    public bool IsArray { get; set; }

    /// <summary>
    /// Gets or sets the size of the array, if <see cref="IsArray"/> is <b>true</b>.
    /// </summary>
    /// <remarks>
    /// If this is not an array, this property is ignored.
    /// </remarks>
    /// <value>
    /// An integer indicating the array size.
    /// </value>
    public int Size { get; set; }
    #endregion

    #region Private Methods/Functions    
    /// <summary>
    /// Parses the data type expression.
    /// </summary>
    /// <param name="dataTypeSpec">
    /// A string containing the data type specification in the format:
    ///    "AS [datatypename]"
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the data type specification does no start with the AS keyword.
    /// </exception>
    private void ParseDataTypeExpression(string dataTypeSpec)
    {
        if (dataTypeSpec.Trim().ToLower().Substring(0, 3) != "as ")
        {
            // Throw an error here.
            throw new ArgumentException("Data type specification must start with 'as '.");
        }

        string dataType = dataTypeSpec.Substring(3).Trim();

        // TODO:
        //DataType = BasicDataTypeDictionary.Standard.GetDataType(dataType);
    }
    #endregion
}
