namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and defines a SQL Server INSERT statement.
    /// </summary>
    /// <seealso cref="SqlCodeStatement" />
    public sealed class SqlCodeInsertStatement : SqlCodeStatement
    {
        #region Private Member Declarations        
        /// <summary>
        /// The table to insert into.
        /// </summary>
        private SqlCodeTableReferenceExpression? _tableToInsert;
        /// <summary>
        /// The list of columns to use.
        /// </summary>
        private SqlCodeColumnNameExpressionCollection? _columnsToUse;
        /// <summary>
        /// The list of values to use.
        /// </summary>
        private SqlCodeExpressionCollection? _values;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeInsertStatement"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeInsertStatement() : base(SqlStatementType.Insert)
        {
            _columnsToUse = new SqlCodeColumnNameExpressionCollection();
            _values = new SqlCodeExpressionCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeInsertStatement"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeInsertStatement(SqlCodeTableReferenceExpression? table) : base(SqlStatementType.Insert)
        {
            if (table != null)
                _tableToInsert = table.Clone();

            _columnsToUse = new SqlCodeColumnNameExpressionCollection();
            _values = new SqlCodeExpressionCollection();
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _tableToInsert?.Dispose();
                _columnsToUse?.Clear();
                _values?.Clear();
            }

            _tableToInsert = null;
            _columnsToUse = null;
            _values = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the reference to the list of columns to insert data into.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeColumnNameExpressionCollection"/> instance containing the column name list.
        /// </value>
        public SqlCodeColumnNameExpressionCollection InsertColumnList
        {
            get
            {
                if (_columnsToUse == null)
                    _columnsToUse = new SqlCodeColumnNameExpressionCollection();
                return _columnsToUse;
            }
        }

        /// <summary>
        /// Gets or sets the reference to the table expression indicating the table to insert data into.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeTableReferenceExpression"/> specifying the table.
        /// </value>
        public SqlCodeTableReferenceExpression? Table
        {
            get => _tableToInsert;
            set
            {
                _tableToInsert?.Dispose();
                if (value == null)
                    _tableToInsert = null;
                else
                    _tableToInsert = value.Clone();
            }
        }
        /// <summary>
        /// Gets the reference to the list of values to insert into the data.
        /// </summary>
        /// <remarks>
        /// These values should match the ordinal index of their respective columns in 
        /// the <see cref="InsertColumnList"/> property.
        /// </remarks>
        /// <value>
        /// A <see cref="SqlCodeExpressionCollection"/> instance expressions defining the values.
        /// </value>
        public SqlCodeExpressionCollection ValuesList
        {
            get
            {
                if (_values == null)
                    _values = new SqlCodeExpressionCollection();
                return _values;
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
        public override SqlCodeInsertStatement Clone()
        {
            return new SqlCodeInsertStatement(_tableToInsert);
        }
        #endregion

    }
}
