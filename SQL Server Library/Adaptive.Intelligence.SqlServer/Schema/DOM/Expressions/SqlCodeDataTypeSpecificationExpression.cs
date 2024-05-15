using Adaptive.Intelligence.SqlServer.Schema;

namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a SQL data type specification expression.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeDataTypeSpecificationExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The data type indicator.
        /// </summary>
        private SqlDataTypes _dataType = SqlDataTypes.Int;
        /// <summary>
        /// The maximum length for the data contained in the type.
        /// </summary>
        private int _maxLength;
        /// <summary>
        /// The maximum precision of the type if it is numeric-based; otherwise, 0.
        /// </summary>
        private byte _precision;
        /// <summary>
        /// The maximum scale of the type if it is numeric-based; otherwise, 0.
        /// </summary>
        private byte _scale;
        /// <summary>
        /// A value indicating whether the data type is nullable.
        /// </summary>
        private bool _isNullable;
        /// <summary>
        /// A value indicating whether the data type is ANSI padded.
        /// </summary>
        private bool _isPadded;
        #endregion

        #region Constructors         
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeDataTypeSpecificationExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeDataTypeSpecificationExpression()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeDataTypeSpecificationExpression"/> class.
        /// </summary>
        /// <param name="dataType">
        /// A <see cref="SqlDataTypes"/> enumerated value indicating the data type.
        /// </param>
        /// <param name="maxLength">
        /// An integer specifying the maximum length of the data type for a column instance.
        /// </param>
        /// <param name="isNullable">
        /// A value indicating whether the parent column or parameter is nullable.
        /// </param>
        public SqlCodeDataTypeSpecificationExpression(SqlDataTypes dataType, int maxLength, bool isNullable) : 
            this(dataType, maxLength, isNullable, 0, 0, false)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeDataTypeSpecificationExpression"/> class.
        /// </summary>
        /// <param name="dataType">
        /// A <see cref="SqlDataTypes"/> enumerated value indicating the data type.
        /// </param>
        /// <param name="maxLength">
        /// An integer specifying the maximum length of the data type for a column instance.
        /// </param>
        /// <param name="isNullable">
        /// A value indicating whether the parent column or parameter is nullable.
        /// </param>
        /// <param name="precision">
        /// The maximum precision of the type if it is numeric-based; otherwise, 0.
        /// </param>
        /// <param name="scale">
        /// The maximum scale of the type if it is numeric-based; otherwise, 0.
        /// </param>
        /// <param name="isPadded">
        /// A value indicating whether the data type is ANSI padded.
        /// </param>
        public SqlCodeDataTypeSpecificationExpression(SqlDataTypes dataType, int maxLength, bool isNullable, byte precision, byte scale, bool isPadded)
        {
            _dataType = dataType;
            _maxLength = maxLength;
            _precision = precision;
            _scale = scale;
            _isNullable = isNullable;
            _isPadded = isPadded;
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>
        /// A <see cref="SqlDataTypes"/> enumerated value indicating the data type.
        /// </value>
        public SqlDataTypes DataType
        {
            get => _dataType;
            set => _dataType = value;
        }
        /// <summary>
        /// Gets the maximum length for the data contained in the type.
        /// </summary>
        /// <value>
        /// A short integer specifying the maximum data length of the type. A
        /// value of -1 indicates the Column data type is varchar(max), nvarchar(max), 
        /// varbinary(max), or xml.  For text columns, the max_length value will be 16.
        /// </value>
        public int MaxLength
        {
            get => _maxLength;
            set => _maxLength = value;
        }
        /// <summary>
        /// Gets the maximum precision of the type if it is numeric-based; otherwise, 0.
        /// </summary>
        /// <value>
        /// The maximum precision  of the type, if applicable.
        /// </value>
        public byte Precision
        {
            get => _precision;
            set => _precision = value;
        }
        /// <summary>
        /// Gets the maximum scale of the type if it is numeric-based; otherwise, 0.
        /// </summary>
        /// <value>
        /// The maximum scale  of the type, if applicable.
        /// </value>
        public byte Scale
        {
            get => _scale;
            set => _scale = value;
        }
        /// <summary>
        /// Gets a value indicating whether the data type is nullable.
        /// </summary>
        /// <value>
        /// <c>true</c> if the data type is nullable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNullable
        {
            get => _isNullable;
            set => _isNullable = value;
        }
        /// <summary>
        /// Gets a value indicating whether the data type is ANSI padded.
        /// </summary>
        /// <value>
        /// <c>true</c> if the data type is ANSI padded; otherwise, <c>false</c>.
        /// </value>
        public bool IsPadded
        {
            get => _isPadded;
            set => _isPadded = value;
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new <see cref="SqlCodeDataTypeSpecificationExpression"/> that is a copy of this instance.
        /// </returns>
        public override SqlCodeDataTypeSpecificationExpression Clone()
        {
            return new SqlCodeDataTypeSpecificationExpression(_dataType, _maxLength, _isNullable, _precision, 
                _scale, _isPadded); 
        }
        #endregion
    }
}