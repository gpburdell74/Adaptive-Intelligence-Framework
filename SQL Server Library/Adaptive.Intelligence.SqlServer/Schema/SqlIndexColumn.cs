using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a column specified in a table index.
    /// </summary>
    public sealed class SqlIndexColumn : DisposableObjectBase
    {
        #region Private Constants
        private const string FieldIndexColumnId = "indexColumnId";
        private const string FieldTableColumnId = "tableColumnId";
        private const string FieldKeyOrdinal = "keyOrdinal";
        private const string FieldIsDescending = "isDescending";
        private const string FieldIsIncluded = "isIncluded";
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlIndexColumn"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlIndexColumn()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlIndexColumn"/> class.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the content
        /// from the data source.
        /// </param>
        public SqlIndexColumn(SafeSqlDataReader reader)
        {
            SetFromReader(reader);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                TableColumn?.Dispose();

            TableColumn = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the index-specific column ID value.
        /// </summary>
        /// <value>
        /// An integer specifying the index-specific column ID value.
        /// </value>
        public int IndexColumnId
        {
            get; set;
        }
        /// <summary>
        /// Gets or sets the ID of the column on the parent table.
        /// </summary>
        /// <value>
        /// An integer specifying the ID of the column on the parent table.
        /// </value>
        public int TableColumnId
        {
            get; set;
        }
        /// <summary>
        /// Gets or sets the user key ordinal value.
        /// </summary>
        /// <value>
        /// A byte specifying the ordinal position of the column in the index.
        /// </value>
        public byte KeyOrdinal
        {
            get; set;
        }
        /// <summary>
        /// Gets a value indicating whether the column is sorted in descending order in the index.
        /// </summary>
        /// <value>
        /// <c>true</c> if the column is sorted in descending order in the index; otherwise, <c>false</c>.
        /// </value>
        public bool IsDescending { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the column is specified as an "INCLUDED" column in the SQL definition.
        /// </summary>
        /// <value>
        /// <c>true</c> if the column is specified as an "INCLUDED" column in the SQL definition; otherwise, <c>false</c>.
        /// </value>
        public bool IsIncluded { get; private set; }
        /// <summary>
        /// Gets or sets the reference to the table column.
        /// </summary>
        /// <value>
        /// The <see cref="SqlColumn"/> instance defined on the related <see cref="SqlTable"/>.
        /// </value>
        public SqlColumn? TableColumn { get; set; }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return TableColumnId + "-" + IndexColumnId;
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
            IndexColumnId = reader.GetInt32(FieldIndexColumnId);
            TableColumnId = reader.GetInt32(FieldTableColumnId);
            KeyOrdinal = reader.GetByte(FieldKeyOrdinal);
            IsDescending = reader.GetBoolean(FieldIsDescending);
            IsIncluded = reader.GetBoolean(FieldIsIncluded);
        }
        #endregion
    }
}
