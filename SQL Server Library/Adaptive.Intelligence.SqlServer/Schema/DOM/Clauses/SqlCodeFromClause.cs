namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and defines the FROM portion of a SELECT statement.
    /// </summary>
    /// <seealso cref="SqlCodeClause" />
    public sealed class SqlCodeFromClause : SqlCodeClause
    {
        #region Private Member Declarations
        /// <summary>
        /// The table to start querying from.
        /// </summary>
        private SqlCodeTableReferenceExpression? _table;
        /// <summary>
        /// The list of joins to apply to the table.
        /// </summary>
        private SqlCodeJoinClauseCollection? _joins;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeFromClause"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeFromClause()
        {
            _joins = new SqlCodeJoinClauseCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeFromClause"/> class.
        /// </summary>
        /// <param name="sourceTable">
        /// A <see cref="SqlCodeTableReferenceExpression"/> instance defining the first table source in the
        /// FROM clause.
        /// </param>
        public SqlCodeFromClause(SqlCodeTableReferenceExpression? sourceTable)
        {
            _table = sourceTable;
            _joins = new SqlCodeJoinClauseCollection();
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
                _table?.Dispose();
                _joins?.Clear();
            }

            _table = null;
            _joins = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the reference to the list of join clauses in the FROM clause.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeJoinClauseCollection"/> containing the join clause instances.
        /// </value>
        public SqlCodeJoinClauseCollection? Joins
        {
            get => _joins;
            set
            {
                _joins?.Clear();
                _joins = new SqlCodeJoinClauseCollection();
                if (value != null)
                {
                    _joins.AddRange(value);
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether to utilize the no lock query hint.
        /// </summary>
        /// <value>
        ///   <b>true</b> to utilize the no lock query hint; otherwise, <b>false</b>.
        /// </value>
        public bool NoLock { get; set; }
        /// <summary>
        /// Gets or sets the reference to the source table for the FROM clause.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeTableReferenceExpression"/> specifying the source table.
        /// </value>
        public SqlCodeTableReferenceExpression? SourceTable
        {
            get => _table;
            set
            {
                _table?.Dispose();
                if (value == null)
                {
                    _table = null;
                }
                else
                {
                    _table = value.Clone();
                }
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
        public SqlCodeFromClause Clone()
        {
            SqlCodeFromClause clause = new SqlCodeFromClause(_table);
            if (_joins != null)
            {
                if (clause.Joins != null)
                {
                    foreach (SqlCodeJoinClause join in _joins)
                    {
                        clause.Joins.Add(join.Clone());
                    }
                }
            }
            return clause;
        }
        #endregion

    }
}