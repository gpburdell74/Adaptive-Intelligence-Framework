namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a SQL table-data-type column definition.
    /// </summary>
    public sealed class SqlTableTypeColumn : SqlDataTypeSpecification
    {
        #region Private Constants

        private const string FieldColumnName = "columnName";
        private const string FieldColumnId = "columnId";
        private const string FieldSystemTypeId = "systemTypeId";
        private const string FieldMaxLength = "maxLength";
        private const string FieldPrecision = "precision";
        private const string FieldScale = "scale";
        private const string FieldIsNullable = "isNullable";
        private const string FieldIsIdentity = "isIdentity";
        private const string FieldIsComputed = "isComputed";

        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableTypeColumn"/> class.
        /// </summary>
        public SqlTableTypeColumn()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableTypeColumn"/> class.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> to read the data content from.
        /// </param>
        public SqlTableTypeColumn(SafeSqlDataReader reader)
        {
            FromReader(reader);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            ColumnName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the name of the column,
        /// </summary>
        /// <value>
        /// A string specifying the name of the column.
        /// </value>
        public string? ColumnName { get; private set; }
        /// <summary>
        /// Gets the ID value for the table column.
        /// </summary>
        /// <value>
        /// An integer specifying the ID value for the table column.
        /// </value>
        public int ColumnId { get; private set; }
        /// <summary>
        /// Gets the ID of the system data type.
        /// </summary>
        /// <value>
        /// A byte specifying the system data type.
        /// </value>
        public byte SystemTypeId { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the column is an identity column.
        /// </summary>
        /// <value>
        /// <c>true</c> if the column is an identity column; otherwise, <c>false</c>.
        /// </value>
        public bool IsIdentity { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the column is a computed column.
        /// </summary>
        /// <value>
        /// <c>true</c> if the column is a computed column; otherwise, <c>false</c>.
        /// </value>
        public bool IsComputed { get; private set; }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Populates the instance from the supplied reader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> used to read the data row.
        /// </param>
        public void FromReader(SafeSqlDataReader reader)
        {
            ColumnName = reader.GetString(FieldColumnName);
            ColumnId = reader.GetInt32(FieldColumnId);
            SystemTypeId = reader.GetByte(FieldSystemTypeId);
            MaxLength = reader.GetInt16(FieldMaxLength);
            Precision = reader.GetByte(FieldPrecision);
            Scale = reader.GetByte(FieldScale);
            IsNullable = reader.GetBoolean(FieldIsNullable);
            IsIdentity = reader.GetBoolean(FieldIsIdentity);
            IsComputed = reader.GetBoolean(FieldIsComputed);
        }
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ColumnName!;
        }
        #endregion
    }
}