namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and defines the WHERE portion of a SELECT statement.
    /// </summary>
    /// <seealso cref="SqlCodeClause" />
    public sealed class SqlCodeWhereClause : SqlCodeClause
    {
        #region Private Member Declarations        
        /// <summary>
        /// The conditions to render.
        /// </summary>
        private SqlCodeConditionListExpressionCollection? _conditions;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeWhereClause"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeWhereClause()
        {
            _conditions = new SqlCodeConditionListExpressionCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeWhereClause"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeWhereClause(SqlCodeConditionListExpressionCollection? conditionList)
        {
            _conditions = new SqlCodeConditionListExpressionCollection();
            if (conditionList != null)
            {
                foreach (SqlCodeConditionListExpression expression in conditionList)
                {
                    _conditions.Add(expression.Clone());
                }
            }
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
                _conditions?.Clear();
            }

            _conditions = null;
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
        public SqlCodeConditionListExpressionCollection Conditions
        {
            get
            {
                if (_conditions == null)
                {
                    _conditions = new SqlCodeConditionListExpressionCollection();
                }

                return _conditions;
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
        public SqlCodeWhereClause Clone()
        {
            return new SqlCodeWhereClause(_conditions);
        }
        #endregion
    }
}