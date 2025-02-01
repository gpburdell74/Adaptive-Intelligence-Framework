// Ignore Spelling: Sql

using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents the raw data read from the Get Tables And Columns SQL Query.
    /// </summary>
    internal sealed class SqlTableColumnRecord : DisposableObjectBase
    {
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            SchemaName = null;
            TableName = null;
            ColumnName = null;
            base.Dispose(disposing);
        }
        /// <summary>
        /// Gets or sets the SQL ID of the table column object.
        /// </summary>
        /// <value>
        /// An integer specifying the SQL-assigned ID for the object instance.
        /// </value>
        public int ColumnId { get; set; }
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>
        /// A string containing the name of the column.
        /// </value>
        public string? ColumnName { get; set; }
        /// <summary>
        /// Gets a value indicating whether the column is a computed column.
        /// </summary>
        /// <value>
        /// <c>true</c> if the column is a computed column; otherwise, <c>false</c>.
        /// </value>
        public bool IsComputed { get; set; }
        /// <summary>
        /// Gets a value indicating whether the column is an identity column.
        /// </summary>
        /// <value>
        /// <c>true</c> if the column is an identity column; otherwise, <c>false</c>.
        /// </value>
        public bool IsIdentity { get; set; }
        /// <summary>
        /// Gets a value indicating whether the data type is nullable.
        /// </summary>
        /// <value>
        /// <c>true</c> if the data type is nullable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNullable { get; set; }
        /// <summary>
        /// Gets a value indicating whether the column is ANSI padded.
        /// </summary>
        /// <remarks>
        /// For NVARCHAR fields, this reflects the 2 bytes per character representation.
        /// </remarks>
        /// <value>
        /// <c>true</c> if the column has a 2-character padding; otherwise, <c>false</c>.
        /// </value>
        public bool IsPadded { get; set; }
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
        /// Gets or sets the name of the schema/owner.
        /// </summary>
        /// <value>
        /// A string containing the name of the schema.
        /// </value>
        public string? SchemaName { get; set; }
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// A string containing the name of the table.
        /// </value>
        public string? TableName { get; set; }
        /// <summary>
        /// Gets or sets the SQL ID of the table object.
        /// </summary>
        /// <value>
        /// An integer specifying the SQL-assigned ID for the object instance.
        /// </value>
        public int TableObjectId { get; set; }
        /// <summary>
        /// Gets or sets the SQL-assigned ID of the data type for the column.
        /// </summary>
        /// <value>
        /// An integer specifying the 
        /// </value>
        public int TypeId { get; set; }
    }
}
