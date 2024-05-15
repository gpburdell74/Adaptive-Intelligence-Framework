namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a conditional expression with a left-side expression, and right-side expression,
    /// and a specified operator.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeConditionExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The left-side expression for comparision.
        /// </summary>
        private SqlCodeExpression? _leftExpression;
        /// <summary>
        /// The right-side expression for comparision.
        /// </summary>
        private SqlCodeExpression? _rightExpression;
        /// <summary>
        /// The comparison operator.
        /// </summary>
        private SqlComparisonOperator _operator = SqlComparisonOperator.NotSpecified;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeConditionExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeConditionExpression()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeConditionExpression"/> class.
        /// </summary>
        /// <param name="leftExpression">
        /// The reference to the left-side <see cref="SqlCodeExpression"/> instance.f
        /// </param>
        /// <param name="rightExpression">
        /// The reference to the right-side <see cref="SqlCodeExpression"/> instance.f
        /// </param>
        /// <param name="sqlOperator">
        /// A <see cref="SqlComparisonOperator"/> enumerated value indicating the operator that is to be used.
        /// </param>
        public SqlCodeConditionExpression(SqlCodeExpression? leftExpression, SqlCodeExpression? rightExpression, SqlComparisonOperator sqlOperator)
        {
            if (sqlOperator ==  SqlComparisonOperator.NotSpecified)
                throw new ArgumentOutOfRangeException(nameof(sqlOperator));

            _leftExpression = leftExpression ?? throw new ArgumentNullException(nameof(leftExpression));
            _rightExpression = rightExpression ?? throw new ArgumentNullException(nameof(rightExpression));
            _operator = sqlOperator;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _leftExpression?.Dispose();
                _rightExpression?.Dispose();
            }

            _leftExpression = null;
            _rightExpression = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the reference to the left-side SQL code expression to be evaluated.
        /// </summary>
        /// <value>
        /// The <see cref="SqlCodeExpression"/> instance to be evaluated.
        /// </value>
        public SqlCodeExpression? LeftExpression
        {
            get => _leftExpression;
            set
            {
                _leftExpression?.Dispose();
                if (value == null)
                    _leftExpression = null;
                else
                    _leftExpression = value.Clone();
            }
        }
        /// <summary>
        /// Gets or sets the operator used to perform the comparison.
        /// </summary>
        /// <value>
        /// A <see cref="SqlComparisonOperator"/> enumerated value indicating the operator that is to be used.
        /// </value>
        public SqlComparisonOperator Operator
        {
            get => _operator;
            set => _operator = value;
        }
        /// <summary>
        /// Gets or sets the reference to the right-side SQL code expression to be evaluated.
        /// </summary>
        /// <value>
        /// The <see cref="SqlCodeExpression"/> instance to be evaluated.
        /// </value>
        public SqlCodeExpression? RightExpression
        {
            get => _rightExpression;
            set
            {
                _rightExpression?.Dispose();
                if (value == null)
                    _rightExpression = null;
                else
                    _rightExpression = value.Clone();
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
        public override SqlCodeConditionExpression Clone()
        {
            return new SqlCodeConditionExpression(_leftExpression, _rightExpression, _operator);
        }
        #endregion

    }
}
