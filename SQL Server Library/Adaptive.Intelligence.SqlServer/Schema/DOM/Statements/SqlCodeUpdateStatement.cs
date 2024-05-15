namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and defines a SQL Server UPDATE statement.
    /// </summary>
    /// <seealso cref="SqlCodeStatement" />
    public sealed class SqlCodeUpdateStatement : SqlCodeStatement
    {
        #region Private Member Declarations        
        /// <summary>
        /// The table to be updated.
        /// </summary>
        private SqlCodeTableReferenceExpression? _table;
        /// <summary>
        /// The list of columns and values.
        /// </summary>
        private SqlCodeAssignmentExpressionCollection? _updateColumnList;
        /// <summary>
        /// The where clause for the statement.
        /// </summary>
        private SqlCodeWhereClause? _whereClause;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeUpdateStatement"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeUpdateStatement() : base(SqlStatementType.Update)
        {
            _updateColumnList = new SqlCodeAssignmentExpressionCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeUpdateStatement"/> class.
        /// </summary>
        /// <param name="table">
        /// A <see cref="SqlCodeTableReferenceExpression"/> instance indicating the table to be updated.
        /// </param>
        public SqlCodeUpdateStatement(SqlCodeTableReferenceExpression? table) : base(SqlStatementType.Update)
        {
            if (table != null)
                _table = table.Clone();

            _updateColumnList = new SqlCodeAssignmentExpressionCollection();
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed  && disposing)
            {
                _updateColumnList?.Clear();
                _table?.Dispose();
                _whereClause?.Dispose();
            }

            _whereClause = null;
            _updateColumnList = null;
            _table = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the reference to the expression for table being updated.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeTableReferenceExpression"/> instance indicating the table being updated.
        /// </value>
        public SqlCodeTableReferenceExpression? Table
        {
            get => _table;
            set
            {
                _table?.Dispose();
                if (value == null)
                    _table = null;
                else
                    _table = value.Clone();
            }
        }
        /// <summary>
        /// Gets the reference to the list of assignment expressions that comprise the update column list.
        /// </summary>
        /// <value>
        /// The <see cref="SqlCodeAssignmentExpressionCollection"/> instance containing the list.
        /// </value>
        public SqlCodeAssignmentExpressionCollection? UpdateColumnList => _updateColumnList;
        /// <summary>
        /// Gets the reference to the WHERE clause for the update statement.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeWhereClause"/> instance, or <b>null</b> if not used.
        /// </value>
        public SqlCodeWhereClause? WhereClause
        {
            get => _whereClause;
            set
            {
                _whereClause?.Dispose();
                if (value == null)
                    _whereClause = null;
                else
                    _whereClause = value.Clone();
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
        public override SqlCodeUpdateStatement Clone()
        {
            SqlCodeUpdateStatement item = new SqlCodeUpdateStatement(_table);

            if (_updateColumnList != null)
            {
                foreach (SqlCodeAssignmentExpression expression in _updateColumnList)
                    item.UpdateColumnList!.Add(expression.Clone());
            }

            if (_whereClause != null)
                item.WhereClause = _whereClause.Clone();

            return item;
        }
        #endregion

    }
}