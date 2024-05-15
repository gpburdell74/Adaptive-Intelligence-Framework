namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a column definition on a table.
    /// </summary>
    public sealed class SqlColumn : SqlDataTypeSpecification
    {
        #region Private Constants
        private const string FieldColumnId = "columnId";
        private const string FieldColumnName = "columnName";
        private const string FieldTypeId = "typeId";
        private const string FieldMaxLength = "maxLength";
        private const string FieldPrecision = "precision";
        private const string FieldScale = "scale";
        private const string FieldIsNullable = "isNullable";
        private const string FieldIsIdentity = "isIdentity";
        private const string FieldIsComputed = "isComputed";
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlColumn"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlColumn()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlColumn"/> class.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the content
        /// from the data source.
        /// </param>
        public SqlColumn(SafeSqlDataReader reader)
        {
            SetFromReader(reader);
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
        /// Gets or sets the column ID value.
        /// </summary>
        /// <value>
        /// An integer specifying the column ID value.
        /// </value>
        public int ColumnId
        {
            get; set;
        }
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>
        /// A string containing the name of the column.
        /// </value>
        public string? ColumnName
        {
            get; set;
        }
        /// <summary>
        /// Gets or sets the user type ID in SQL Server.
        /// </summary>
        /// <value>
        /// An integer specifying the user type ID in SQL Server.
        /// </value>
        public int TypeId
        {
            get; set;
        }
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
        /// Gets a value indicating whether the column is ANSI padded.
        /// </summary>
        /// <remarks>
        /// For NVARCHAR fields, this reflects the 2 bytes per character representation.
        /// </remarks>
        /// <value>
        /// <c>true</c> if the column has a 2-character padding; otherwise, <c>false</c>.
        /// </value>
        public bool IsAnsiPadded { get; set; }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <b>true</b> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <b>false</b>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is SqlColumn rightColumn))
                return false;
            else
            {
                return (
                    (rightColumn.IsComputed == IsComputed) &&
                    (rightColumn.IsAnsiPadded == IsAnsiPadded) &&
                    (rightColumn.IsIdentity == IsIdentity) &&
                    (rightColumn.TypeId == TypeId) &&
                    (rightColumn.MaxLength == MaxLength) &&
                    (rightColumn.IsNullable == IsNullable) &&
                    (rightColumn.Precision == Precision) &&
                    (rightColumn.Scale == Scale));
            }
        }
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            if (ColumnName == null)
                return 0;

            return ColumnName.GetHashCode();
        }
        /// <summary>a
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (ColumnName == null)
                return nameof(SqlColumn);

            return ColumnName;
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Sets property values from the reader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the content
        /// from the data source.
        /// </param>
        private void SetFromReader(SafeSqlDataReader reader)
        {
            ColumnId = reader.GetInt32(FieldColumnId);
            ColumnName = reader.GetString(FieldColumnName);
            TypeId = reader.GetInt32(FieldTypeId);
            MaxLength = reader.GetInt16(FieldMaxLength);
            Precision = reader.GetByte(FieldPrecision);
            Scale = reader.GetByte(FieldScale);
            IsNullable = reader.GetBoolean(FieldIsNullable);
            IsIdentity = reader.GetBoolean(FieldIsIdentity);
            IsComputed = reader.GetBoolean(FieldIsComputed);
        }
        #endregion
    }
}
