using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and defines a LEFT or INNER join clause.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class SqlCodeJoinClause : SqlCodeClause
    {
        #region Private Member Declarations
        /// <summary>
        /// The left-side table of the join.
        /// </summary>
        private SqlCodeTableColumnReferenceExpression? _leftColumn;
        /// <summary>
        /// The right-side table of the join.
        /// </summary>
        private SqlCodeTableColumnReferenceExpression? _rightColumn;
        /// <summary>
        /// The referenced table
        /// </summary>
        private SqlCodeTableReferenceExpression? _referencedTable;
        /// <summary>
        /// The left join flag.
        /// </summary>
        private bool _leftJoin;
        /// <summary>
        /// The no lock flag.
        /// </summary>
        private bool _noLock;
        /// <summary>
        /// The operator
        /// </summary>
        private SqlComparisonOperator _operator;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeJoinClause"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeJoinClause()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeSelectClause"/> class.
        /// </summary>
        /// <param name="referencedTable">
        /// The <see cref="SqlCodeTableReferenceExpression"/> indicating the table being joined to.
        /// </param>
        /// <param name="leftColumn">
        /// A <see cref="SqlCodeTableColumnReferenceExpression"/> representing the table and column on the left side
        /// of the join.
        /// </param>
        /// <param name="rightColumn">
        /// A <see cref="SqlCodeTableColumnReferenceExpression"/> representing the table and column on the right side
        /// of the join.
        /// </param>
        /// <param name="leftJoin">
        /// A value indicating whether this represents a LEFT JOIN.  If the value is <b>false</b>,
        /// it represents an INNER JOIN.
        /// </param>
        public SqlCodeJoinClause(SqlCodeTableReferenceExpression? referencedTable, 
            SqlCodeTableColumnReferenceExpression? leftColumn, 
            SqlCodeTableColumnReferenceExpression? rightColumn,
            bool leftJoin)
        {
            _referencedTable = referencedTable;
            _leftColumn = leftColumn;
            _rightColumn = rightColumn;
            _leftJoin = leftJoin;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (IsDisposed && disposing)
            {
                _referencedTable?.Dispose();
                _leftColumn?.Dispose();
                _rightColumn?.Dispose();
            }

            _referencedTable = null;
            _leftColumn = null;
            _rightColumn = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the reference to the left-side column expression for the JOIN.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeTableColumnReferenceExpression"/> instance containing the table and column reference expression.
        /// </value>
        public SqlCodeTableColumnReferenceExpression? LeftColumn
        {
            get => _leftColumn;
            set
            {
                _leftColumn?.Dispose();
                if (value == null)
                    _leftColumn = null;
                else
                    _leftColumn = value.Clone();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the current instance represents a LEFT JOIN.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance represents a LEFT JOIN; <b>false</b> if the instance represents
        /// an INNER JOIN.
        /// </value>
        public bool IsLeftJoin
        {
            get => _leftJoin;
            set => _leftJoin = value;
        }
        /// <summary>
        /// Gets or sets a value indicating whether to utilize the no lock query hint.
        /// </summary>
        /// <value>
        ///   <b>true</b> to utilize the no lock query hint; otherwise, <b>false</b>.
        /// </value>
        public bool NoLock
        {
            get => _noLock;
            set => _noLock = value;
        }
        /// <summary>
        /// Gets or sets the comparison operator to use on the join.
        /// </summary>
        /// <value>
        /// A <see cref="SqlComparisonOperator"/> enumerated value indicating the operator to use.  Some
        /// enumerated values are not usable in this context.
        /// </value>
        public SqlComparisonOperator Operator
        {
            get => _operator;
            set
            {
                switch (value)
                {
                    case SqlComparisonOperator.NotSpecified:
                    case SqlComparisonOperator.IsNotNull:
                    case SqlComparisonOperator.IsNull:
                    case SqlComparisonOperator.Not:
                    case SqlComparisonOperator.NotIn:
                        throw new ArgumentOutOfRangeException(
                            nameof(Operator),
                            @"This operator value is not currently supported in JOIN operations.");
                }
                _operator = value;
            }
        }
        /// <summary>
        /// Gets or sets the expression for the table being joined to.
        /// </summary>
        /// <value>
        /// The <see cref="SqlCodeTableReferenceExpression"/> describing the table.
        /// </value>
        public SqlCodeTableReferenceExpression? ReferencedTable
        {
            get => _referencedTable;
            set
            {
                _referencedTable?.Dispose();
                if (value == null)
                    _referencedTable = null;
                else
                    _referencedTable = value.Clone();
            }
        }
        /// <summary>
        /// Gets or sets the reference to the right-side column expression for the JOIN.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeTableColumnReferenceExpression"/> instance containing the table and column reference expression.
        /// </value>
        public SqlCodeTableColumnReferenceExpression? RightColumn
        {
            get => _rightColumn;
            set
            {
                _rightColumn?.Dispose();
                if (value == null)
                    _rightColumn = null;
                else
                    _rightColumn = value.Clone();
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
        public SqlCodeJoinClause Clone()
        {
            return new SqlCodeJoinClause(_referencedTable, _leftColumn, _rightColumn, _leftJoin);
        }
        #endregion

    }
}