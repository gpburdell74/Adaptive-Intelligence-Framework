namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and defines a SQL Server DELETE statement.
    /// </summary>
    /// <seealso cref="SqlCodeStatement" />
    public sealed class SqlCodeDeleteStatement : SqlCodeStatement
    {
        #region Private Member Declarations
        /// <summary>
        /// The FROM clause.
        /// </summary>
        private SqlCodeFromClause? _fromClause;
        /// <summary>
        /// The WHERE clause.
        /// </summary>
        private SqlCodeWhereClause? _whereClause;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeDeleteStatement"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeDeleteStatement() : base(SqlStatementType.Delete)
        {
            _fromClause = new SqlCodeFromClause();
            _whereClause = new SqlCodeWhereClause();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeDeleteStatement"/> class.
        /// </summary>
        /// <param name="fromClause">
        /// The reference to the <see cref="SqlCodeFromClause"/> specifying where to delete from.
        /// </param>
        /// <param name="whereClause">
        /// The reference to the <see cref="SqlCodeWhereClause"/> specifying the conditions, if any.
        /// </param>
        public SqlCodeDeleteStatement(SqlCodeFromClause? fromClause, SqlCodeWhereClause? whereClause) : base(SqlStatementType.Delete)
        {
            _fromClause = fromClause?.Clone();
            _whereClause = whereClause?.Clone();
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
                _whereClause?.Dispose();
            }

            _fromClause = null;
            _whereClause = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the FROM clause instance.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeFromClause"/> instance.
        /// </value>
        public SqlCodeFromClause FromClause
        {
            get
            {
                if (_fromClause == null)
                {
                    _fromClause = new SqlCodeFromClause();
                }

                return _fromClause;
            }
        }
        /// <summary>
        /// Gets the reference to the WHERE clause instance.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeWhereClause"/> instance.
        /// </value>
        public SqlCodeWhereClause WhereClause
        {
            get
            {
                if (_whereClause == null)
                {
                    _whereClause = new SqlCodeWhereClause();
                }

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
        public override SqlCodeDeleteStatement Clone()
        {
            return new SqlCodeDeleteStatement(_fromClause, _whereClause);
        }
        #endregion
    }
}
