namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a reference to a specific column in a specific table.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeTableColumnReferenceExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The table name expression.
        /// </summary>
        private SqlCodeTableNameExpression? _tableName;
        /// <summary>
        /// The column name expression.
        /// </summary>
        private SqlCodeColumnNameExpression? _columnName;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeTableColumnReferenceExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeTableColumnReferenceExpression()
        {
            _tableName = new SqlCodeTableNameExpression();
            _columnName = new SqlCodeColumnNameExpression();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeTableColumnReferenceExpression"/> class.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the name of the table.
        /// </param>
        /// <param name="columnName">
        /// A string containing the column name.
        /// </param>
        public SqlCodeTableColumnReferenceExpression(string? tableName, string columnName) :
            this(new SqlCodeTableNameExpression(tableName), new SqlCodeColumnNameExpression(columnName))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeTableColumnReferenceExpression"/> class.
        /// </summary>
        /// <param name="tableName">
        /// A <see cref="SqlCodeTableNameExpression"/> instance representing the name of the table.
        /// </param>
        /// <param name="columnName">
        /// A <see cref="SqlCodeColumnNameExpression"/> instance representing the name of the column.
        /// </param>
        public SqlCodeTableColumnReferenceExpression(
            SqlCodeTableNameExpression? tableName, 
            SqlCodeColumnNameExpression? columnName)
        {
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            _columnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _tableName?.Dispose();
                _columnName?.Dispose();
            }

            _tableName = null;
            _columnName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the column being referenced.
        /// </summary>
        /// <value>
        /// A reference to the <see cref="SqlCodeColumnNameExpression"/> instance.
        /// </value>
        public SqlCodeColumnNameExpression? ColumnName
        {
            get => _columnName;
            set
            {
                _columnName?.Dispose();
                if (value == null)
                    _columnName = null;
                else
                    _columnName = value.Clone();
            }
        }
        /// <summary>
        /// Gets or sets the name of the table being referenced.
        /// </summary>
        /// <value>
        /// A reference to the <see cref="SqlCodeTableNameExpression"/> instance.
        /// </value>
        public SqlCodeTableNameExpression? TableName
        {
            get => _tableName;
            set
            {
                _tableName?.Dispose();
                if (value == null)
                    _tableName = null;
                else
                    _tableName = value.Clone();
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeTableColumnReferenceExpression Clone()
        {
            return new SqlCodeTableColumnReferenceExpression(_tableName, _columnName);
        }
        #endregion

    }
}