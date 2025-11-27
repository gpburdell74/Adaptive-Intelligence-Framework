using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a foreign key definition on a table.
    /// </summary>
    public sealed class SqlForeignKey : SqlDataTypeSpecification
    {
        #region Private Constants
        private const string FieldParentTableName = "parentTableName";
        private const string FieldForeignKeyName = "foreignKeyName";
        private const string FieldForeignKeyObjectId = "foreignKeyObjectId";
        private const string FieldParentTableObjectId = "parentTableObjectId";
        private const string FieldReferencedTableName = "referencedTableName";
        private const string FieldReferencedTableObjectId = "referencedTableObjectId";
        private const string FieldKeyIndexId = "keyIndexId";
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// The list of columns defined on the foreign key.
        /// </summary>
        private SqlForeignKeyColumnCollection? _columns;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlForeignKey"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlForeignKey()
        {
            _columns = new SqlForeignKeyColumnCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlForeignKey"/> class.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="ISafeSqlDataReader"/> instance used to read the content 
        /// from the data source.
        /// </param>
        public SqlForeignKey(ISafeSqlDataReader reader)
        {
            _columns = new SqlForeignKeyColumnCollection();
            SetFromReader(reader);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _columns?.Clear();
            }

            _columns = null;
            ParentTableName = null;
            ReferencedTableName = null;
            ForeignKeyName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the list of columns defined on the table.
        /// </summary>
        /// <value>
        /// A <see cref="SqlForeignKeyColumnCollection"/> containing the column definitions.
        /// </value>
        public SqlForeignKeyColumnCollection? Columns => _columns;
        /// <summary>
        /// Gets or sets the name of the parent table.
        /// </summary>
        /// <value>
        /// A string containing the name of the parent table.
        /// </value>
        public string? ParentTableName { get; set; }
        /// <summary>
        /// Gets or sets the name of the foreign key.
        /// </summary>
        /// <value>
        /// A string containing the name of the foreign key.
        /// </value>
        public string? ForeignKeyName { get; set; }
        /// <summary>
        /// Gets or sets the ID of the foreign key.
        /// </summary>
        /// <value>
        /// An integer containing the ID of the foreign key.
        /// </value>
        public int ForeignKeyObjectId { get; set; }
        /// <summary>
        /// Gets or sets the ID of the parent table.
        /// </summary>
        /// <value>
        /// An integer containing the ID of the parent table.
        /// </value>
        public int ParentTableObjectId { get; set; }
        /// <summary>
        /// Gets or sets the name of the referenced table.
        /// </summary>
        /// <value>
        /// A string containing the name of the referenced table.
        /// </value>
        public string? ReferencedTableName { get; set; }
        /// <summary>
        /// Gets or sets the ID of the referenced table.
        /// </summary>
        /// <value>
        /// An integer containing the ID of the referenced table.
        /// </value>
        public int ReferencedTableObjectId { get; set; }
        /// <summary>
        /// Gets or sets the ID of the key index within the referenced object.
        /// </summary>
        /// <value>
        /// An integer containing the ID of the key index within the referenced object.
        /// </value>
        public int KeyIndexId { get; set; }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (ForeignKeyName == null)
            {
                return nameof(SqlForeignKey);
            }

            return ForeignKeyName;
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Sets property values from the reader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="ISafeSqlDataReader"/> instance used to read the content 
        /// from the data source.
        /// </param>
        private void SetFromReader(ISafeSqlDataReader reader)
        {
            ParentTableName = reader.GetString(FieldParentTableName);
            ForeignKeyName = reader.GetString(FieldForeignKeyName);
            ForeignKeyObjectId = reader.GetInt32(FieldForeignKeyObjectId);
            ParentTableObjectId = reader.GetInt32(FieldParentTableObjectId);
            ReferencedTableName = reader.GetString(FieldReferencedTableName);
            ReferencedTableObjectId = reader.GetInt32(FieldReferencedTableObjectId);
            KeyIndexId = reader.GetInt32(FieldKeyIndexId);
        }
        #endregion
    }
}
