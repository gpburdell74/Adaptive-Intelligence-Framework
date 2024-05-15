using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Provides a base definition for types that reflect common data type-specification properties.
    /// </summary>
    public class SqlDataTypeSpecification : DisposableObjectBase
    {
        #region Public Properties
        /// <summary>
        /// Gets the maximum length for the data contained in the type.
        /// </summary>
        /// <value>
        /// A short integer specifying the maximum data length of the type. A
        /// value of -1 indicates the Column data type is varchar(max), nvarchar(max), 
        /// varbinary(max), or xml.  For text columns, the max_length value will be 16.
        /// </value>
        public short MaxLength { get; set; }
        /// <summary>
        /// Gets the maximum precision of the type if it is numeric-based; otherwise, 0.
        /// </summary>
        /// <value>
        /// The maximum precision  of the type, if applicable.
        /// </value>
        public byte Precision { get; set; }
        /// <summary>
        /// Gets the maximum scale of the type if it is numeric-based; otherwise, 0.
        /// </summary>
        /// <value>
        /// The maximum scale  of the type, if applicable.
        /// </value>
        public byte Scale { get; set; }
        /// <summary>
        /// Gets a value indicating whether the data type is nullable.
        /// </summary>
        /// <value>
        /// <c>true</c> if the data type is nullable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNullable { get; set; }
        #endregion
    }
}