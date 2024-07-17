using Adaptive.Intelligence.Shared;

namespace Adaptive.CodeDom.Model
{
    /// <summary>
    /// Contains the meta-data information for defining a property based on a SQL column or
    /// a derived (table/Info) object/field reference.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class PropertyProfile : DisposableObjectBase
    {
        #region Private Member Declarations
        private string? _fieldName;
        private string? _typeName;
        private string? _propertyName;
        private string? _summaryText;
        private string? _valueText;
        private string? _remarksText;
        private bool _isList;
        private bool _isDisposable;
        private bool _isNullable;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyProfile"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public PropertyProfile()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyProfile"/> class.
        /// </summary>
        /// <param name="fieldName">
        /// A string containing the name of the backing field for the property.
        /// </param>
        /// <param name="propertyName">
        /// A string containing the name of the property.
        /// </param>
        /// <param name="dataTypeName">
        /// A string containing the name of the data type.
        /// </param>
        /// <param name="summary">
        /// A string containing the XML Comment summary text.
        /// </param>
        /// <param name="remarks">
        /// A string containing the XML Comment remarks text.
        /// </param>
        /// <param name="valueText">
        /// A string containing the XML Comment value text.
        /// </param>
        /// <param name="isList">
        /// <b>true</b> if the data type is a list or collection; otherwise, <b>false</b>.
        /// </param>
        /// <param name="isDisposable">
        /// <b>true</b> if the data type implements <see cref="IDisposable"/>; otherwise, <b>false</b>.
        /// </param>
        /// <param name="isNullable">
        /// <b>true</b> if the data type is nullable; otherwise, <b>false</b>.
        /// </param>
        public PropertyProfile(string fieldName, string propertyName, string dataTypeName, string summary, string remarks, string valueText,
            bool isList, bool isDisposable, bool isNullable)
        {
            _fieldName = fieldName;
            _propertyName = propertyName;
            _typeName = dataTypeName;
            _summaryText = summary;
            _remarksText = remarks;
            _valueText = valueText;
            _isList = isList;
            _isDisposable = isDisposable;
            _isNullable = isNullable;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _fieldName = null;
            _typeName = null;
            _propertyName = null;
            _summaryText = null;
            _remarksText = null;
            _valueText = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the backing field for the property.
        /// </summary>
        /// <value>
        /// A string containing the name of the backing field for the property.
        /// </value>
        public string? FieldName
        {
            get => _fieldName;
            set => _fieldName = value;
        }
        /// <summary>
        /// Gets or sets a value indicating whether the data type for the property is disposable.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the data type for the property is disposable; otherwise, <b>false</b>.
        /// </value>
        public bool IsDisposable
        {
            get => _isDisposable;
            set => _isDisposable = value;
        }
        /// <summary>
        /// Gets or sets a value indicating whether the data type for the property is a list or collection.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the data type for the property is a list or collection; otherwise, <b>false</b>.
        /// </value>
        public bool IsList
        {
            get => _isList;
            set => _isList = value;
        }
        /// <summary>
        /// Gets or sets a value indicating whether the data type is nullable.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the data type is nullable; otherwise, <b>false</b>.
        /// </value>
        public bool IsNullable
        {
            get => _isNullable;
            set => _isNullable = value;
        }
        /// <summary>
        /// Gets or sets the name of the new property.
        /// </summary>
        /// <value>
        /// A string containing the name of the new property.
        /// </value>
        public string? PropertyName
        {
            get => _propertyName;
            set => _propertyName = value;
        }
        /// <summary>
        /// Gets or sets the XML Comment Remarks text.
        /// </summary>
        /// <value>
        /// A string containing the remarks, or <b>null</b>.
        /// </value>
        public string? RemarksText
        {
            get => _remarksText;
            set => _remarksText = value;
        }
        /// <summary>
        /// Gets or sets the XML Comment Summary text.
        /// </summary>
        /// <value>
        /// A string containing the summary, or <b>null</b>.
        /// </value>
        public string? SummaryText
        {
            get => _summaryText;
            set => _summaryText = value;
        }
        /// <summary>
        /// Gets or sets the name of the data type for the property.
        /// </summary>
        /// <value>
        /// A string containing the name of the data type for the property.
        /// </value>
        public string? TypeName
        {
            get => _typeName;
            set => _typeName = value;
        }
        /// <summary>
        /// Gets or sets the XML Value Remarks text.
        /// </summary>
        /// <value>
        /// A string containing the value text, or <b>null</b>.
        /// </value>
        public string? ValueText
        {
            get => _valueText;
            set => _valueText = value;
        }

        #endregion
    }
}
