namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and defines a SQL Server SELECT statement.
    /// </summary>
    /// <seealso cref="SqlCodeStatement" />
    public sealed class SqlCodeSelectStatement : SqlCodeStatement
    {
        #region Private Member Declarations        
        /// <summary>
        /// The select clause containing the item(s) to be selected.
        /// </summary>
        private SqlCodeSelectClause? _selectClause;
        /// <summary>
        /// The From clause.
        /// </summary>
        private SqlCodeFromClause? _fromClause;
        /// <summary>
        /// The where clause.
        /// </summary>
        private SqlCodeWhereClause? _whereClause;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeSelectStatement"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeSelectStatement() : base(SqlStatementType.Select)
        {
            _selectClause = new SqlCodeSelectClause();
            _fromClause = new SqlCodeFromClause();
            _whereClause = new SqlCodeWhereClause();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeSelectStatement"/> class.
        /// </summary>
        /// <param name="selectClause">The select clause.</param>
        /// <param name="fromClause">From clause.</param>
        /// <param name="whereClause">The where clause.</param>
        public SqlCodeSelectStatement(SqlCodeSelectClause? selectClause, SqlCodeFromClause? fromClause, SqlCodeWhereClause? whereClause) : base(SqlStatementType.Select)
        {
            if (selectClause != null)
                _selectClause = selectClause.Clone();

            if (fromClause != null)
                _fromClause = fromClause.Clone();

            if (whereClause != null)
                _whereClause = whereClause.Clone();
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
                _fromClause?.Dispose();
                _selectClause?.Dispose();
                _whereClause?.Dispose();
            }

            _fromClause = null;
            _selectClause = null;
            _whereClause = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the reference to the FROM clause of the statement.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeFromClause"/> defining the tables to query on.
        /// </value>
        public SqlCodeFromClause FromClause
        {
            get
            {
                if (_fromClause == null)
                    _fromClause = new SqlCodeFromClause();
                return _fromClause;
            }
        }
        /// <summary>
        /// Gets or sets the reference to the select list clause of the statement.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeSelectClause"/> defining the items to be selected.
        /// </value>
        public SqlCodeSelectClause SelectClause
        {
            get
            {
                if (_selectClause == null)
                    _selectClause = new SqlCodeSelectClause();
                return _selectClause;
            }
        }
        /// <summary>
        /// Gets or sets the reference to the WHERE clause of the statement.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeWhereClause"/> defining the conditions for the query.
        /// </value>
        public SqlCodeWhereClause WhereClause
        {
            get
            {
                if (_whereClause == null)
                    _whereClause = new SqlCodeWhereClause();
                return _whereClause;
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
        public override SqlCodeSelectStatement Clone()
        {
            return new SqlCodeSelectStatement(_selectClause, _fromClause, _whereClause);
        }
        #endregion
    }
}