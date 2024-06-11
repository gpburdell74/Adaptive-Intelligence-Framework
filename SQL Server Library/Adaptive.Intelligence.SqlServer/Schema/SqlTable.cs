using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.CodeDom;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a SQL Server table.
    /// </summary>
    public sealed class SqlTable : DisposableObjectBase
    {
        #region Private Constants

        private const string FieldTableObjectId = "tableObjectId";
        private const string FieldTableName = "tableName";

        #endregion

        #region Private Member Declarations
        /// <summary>
        /// The list of columns defined on the table.
        /// </summary>
        private SqlColumnCollection? _columns;
        /// <summary>
        /// The list of indexes defined on the table. 
        /// </summary>
        private SqlIndexCollection? _indexes;
        /// <summary>
        /// The list of foreign keys defined on the table. 
        /// </summary>
        private SqlForeignKeyCollection? _foreignKeys;
        /// <summary>
        /// The reference to the table's primary key.
        /// </summary>
        private SqlIndex? _primaryKey;

        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTable"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlTable()
        {
            _columns = new SqlColumnCollection();
            _indexes = new SqlIndexCollection();
            _foreignKeys = new SqlForeignKeyCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTable"/> class.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the content from the data source.
        /// </param>
        public SqlTable(SafeSqlDataReader reader)
        {
            _columns = new SqlColumnCollection();
            _indexes = new SqlIndexCollection();
            _foreignKeys = new SqlForeignKeyCollection();
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
                _foreignKeys?.Clear();
                _indexes?.Clear();
                _columns?.Clear();
            }

            _primaryKey = null;
            _indexes = null;
            _foreignKeys = null;
            _columns = null;
            TableName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the list of columns defined on the table.
        /// </summary>
        /// <value>
        /// A <see cref="SqlColumnCollection"/> containing the column definitions.
        /// </value>
        public SqlColumnCollection Columns
        {
            get
            {
                if (_columns == null)
                    _columns = new SqlColumnCollection();
                return _columns;
            }
        }
        /// <summary>
        /// Gets the reference to the list of foreign keys defined on the table.
        /// </summary>
        /// <value>
        /// A <see cref="SqlColumnCollection"/> containing the column definitions.
        /// </value>
        public SqlForeignKeyCollection ForeignKeys
        {
            get
            {
                if (_foreignKeys == null)
                    _foreignKeys = new SqlForeignKeyCollection();
                return _foreignKeys;
            }
        }
        /// <summary>
        /// Gets the reference to the list of indexes defined on the table.
        /// </summary>
        /// <value>
        /// A <see cref="SqlColumnCollection"/> containing the column definitions.
        /// </value>
        public SqlIndexCollection Indexes
        {
            get
            {
                if (_indexes == null)
                    _indexes = new SqlIndexCollection();
                return _indexes;
            }
        }
        /// <summary>
        /// Gets the reference to the index that is the primary key.
        /// </summary>
        /// <value>
        /// A <see cref="SqlIndex"/> that is the primary key, or <b>null</b>.
        /// </value>
        public SqlIndex? PrimaryKey => _primaryKey;
        /// <summary>
        /// Gets or sets the SQL Server table object ID value.
        /// </summary>
        /// <value>
        /// An integer specifying the SQL Server table object ID value.
        /// </value>
        public int TableObjectId { get; set; }
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// A string containing the name of the table.
        /// </value>
        public string? TableName { get; set; }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Generates the T-SQL script for creating the table.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="SqlDataProvider"/> used to communicate with the server.
        /// </param>
        /// <returns>
        /// A string containing the SQL script, if successful; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<string?> GenerateCreateScriptAsync(SqlDataProvider provider)
        {
            if (TableName != null)
            {
                await Task.Yield();
                return NativeTSqlCodeDom.GenerateCreateTableScript(provider, TableName);
            }
            else
                return string.Empty;
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (TableName == null)
                return nameof(SqlTable);

            return TableName;
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Sets the property values from the supplied data reader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the content from the data source.
        /// </param>
        private void SetFromReader(SafeSqlDataReader reader)
        {
            TableObjectId = reader.GetInt32(FieldTableObjectId);
            TableName = reader.GetString(FieldTableName);
        }
        #endregion
    }
}
