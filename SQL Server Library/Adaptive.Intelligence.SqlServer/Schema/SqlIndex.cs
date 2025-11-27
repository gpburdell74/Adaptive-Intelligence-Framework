using Adaptive.Intelligence.Shared;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents an index definition on a table.
    /// </summary>
    public sealed class SqlIndex : DisposableObjectBase
    {
        #region Private Constants
        private const string FieldTableObjectId = "tableObjectId";
        private const string FieldTableName = "tableName";
        private const string FieldIndexName = "indexName";
        private const string FieldIndexId = "indexId";
        private const string FieldIndexType = "indexType";
        private const string FieldIndexTypeDesc = "indexTypeDesc";
        private const string FieldIsUnique = "isUnique";
        private const string FieldIsPrimaryKey = "isPrimaryKey";
        private const string FieldIsUniqueConstraint = "isUniqueConstraint";
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// The list of columns defined on the index.
        /// </summary>
        private SqlIndexColumnCollection? _columns;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlIndex"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlIndex()
        {
            _columns = new SqlIndexColumnCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlIndex"/> class.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="ISafeSqlDataReader"/> instance used to read the content 
        /// from the data source.
        /// </param>
        public SqlIndex(ISafeSqlDataReader? reader)
        {
            _columns = new SqlIndexColumnCollection();
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
            TableName = null;
            IndexName = null;
            IndexTypeDesc = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the list of columns defined on the index.
        /// </summary>
        /// <value>
        /// A <see cref="SqlIndexColumnCollection"/> containing the column definitions.
        /// </value>
        public SqlIndexColumnCollection? Columns => _columns;
        /// <summary>
        /// Gets or sets the object ID for the parent table.
        /// </summary>
        /// <value>
        /// An integer specifying the object ID for the parent table.
        /// </value>
        public int TableObjectId
        {
            get; set;
        }
        /// <summary>
        /// Gets or sets the name of the parent table.
        /// </summary>
        /// <value>
        /// A string containing the name of the parent table.
        /// </value>
        public string? TableName
        {
            get; set;
        }
        /// <summary>
        /// Gets or sets the name of the index.
        /// </summary>
        /// <value>
        /// A string containing the name of the index.
        /// </value>
        public string? IndexName
        {
            get; set;
        }
        /// <summary>
        /// Gets the table-specific ID of the index.
        /// </summary>
        /// <value>
        /// An integer specifying the table-specific ID of the index.
        /// </value>
        public int IndexId
        {
            get; set;
        }
        /// <summary>
        /// Gets the type of the index.
        /// </summary>
        /// <value>
        /// A byte value specifying the type of the index.
        /// </value>
        public byte IndexType { get; set; }
        /// <summary>
        /// Gets or sets description of the type of index as specified by <see cref="IndexType"/>.
        /// </summary>
        /// <value>
        /// A string containing the SQL-specified description of the index type.
        /// </value>
        public string? IndexTypeDesc { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the index is unique.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the index is unique; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnique { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the index is the primary key.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the index is the primary key; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the index is part of a unique constraint.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the index is a part of a unique constraint; otherwise, <c>false</c>.
        /// </value>
        public bool IsUniqueConstraint { get; set; }
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
            if (IndexName == null)
            {
                return nameof(SqlIndex);
            }

            return IndexName;
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
        private void SetFromReader(ISafeSqlDataReader? reader)
        {
            if (reader != null)
            {
                TableObjectId = reader.GetInt32(FieldTableObjectId);
                TableName = reader.GetString(FieldTableName);
                IndexName = reader.GetString(FieldIndexName);
                IndexId = reader.GetInt32(FieldIndexId);
                IndexType = reader.GetByte(FieldIndexType);
                IndexTypeDesc = reader.GetString(FieldIndexTypeDesc);
                IsUnique = reader.GetBoolean(FieldIsUnique);
                IsPrimaryKey = reader.GetBoolean(FieldIsPrimaryKey);
                IsUniqueConstraint = reader.GetBoolean(FieldIsUniqueConstraint);
            }
        }
        #endregion
    }
}
