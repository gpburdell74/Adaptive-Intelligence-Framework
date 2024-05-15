namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a conditional expression to be evaluated, with an optional operator specification to be used when
    /// contained with other <see cref="SqlCodeConditionListExpression"/> instances.
    /// </summary>
    /// <remarks>
    /// This is used to wrap <see cref="SqlCodeConditionExpression"/> instances with an additional operator, so that the
    /// entire expression may be used in a list of such expressions, as in a WHERE clause.
    /// </remarks>
    /// <example>
    /// WHERE
    ///     [A].[Deleted] = 0 AND   -- SqlCodeConditionExpression within a SqlCodeConditionListExpression
    ///     [B].[Value] != 1        -- SqlCodeConditionExpression within a SqlCodeConditionListExpression
    ///     
    /// is equivalent to:
    ///       SqlCodeConditionListExpression firstLine = new SqlCodeConditionListExpression(
    ///             new SqlCodeConditionExpression(
    ///                new SqlCodeTableColumnReferenceExpression(
    ///                    new SqlCodeTableNameExpression("A"),
    ///                    new SqlCodeColumnNameExpression("Deleted")),
    ///                new SqlCodeLiteralExpression(0),
    ///                SqlComparisonOperators.EqualTo),
    ///           SqlConditionOperator.And);
    ///
    ///        SqlCodeConditionListExpression secondLine = new SqlCodeConditionListExpression(
    ///            new SqlCodeConditionExpression(
    ///                new SqlCodeTableColumnReferenceExpression(
    ///                    new SqlCodeTableNameExpression("B"),
    ///                    new SqlCodeColumnNameExpression("Value")),
    ///                new SqlCodeLiteralExpression(1),
    ///                SqlComparisonOperators.NotEqualTo),
    ///           SqlConditionOperator.NotSpecified);
    /// </example>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeConditionListExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The condition expression.
        /// </summary>
        private SqlCodeConditionExpression? _expression;
        /// <summary>
        /// The operator to apply to the conditions for comparison.
        /// </summary>
        private SqlConditionOperator _operator = SqlConditionOperator.NotSpecified;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeConditionListExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeConditionListExpression()
        {
            _expression = new SqlCodeConditionExpression();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeConditionListExpression"/> class.
        /// </summary>
        /// <param name="expression">
        /// The reference to the <see cref="SqlCodeConditionExpression"/> inner comparison instance.
        /// </param>
        /// <param name="sqlOperator">
        /// A <see cref="SqlConditionOperator"/> enumerated value indicating the operator that is to be used.
        /// </param>
        public SqlCodeConditionListExpression(SqlCodeConditionExpression? expression, SqlConditionOperator sqlOperator)
        {
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
            _operator = sqlOperator;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _expression?.Dispose();
            _expression = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the reference to the left-side SQL code expression to be evaluated.
        /// </summary>
        /// <value>
        /// The <see cref="SqlCodeConditionExpression"/> instance to be evaluated.
        /// </value>
        public SqlCodeConditionExpression? Expression => _expression;
        /// <summary>
        /// Gets or sets the operator used to perform the comparison.
        /// </summary>
        /// <value>
        /// A <see cref="SqlConditionOperator"/> enumerated value indicating the operator that is to be used, usually
        /// <see cref="SqlConditionOperator.And"/> or <see cref="SqlConditionOperator.Or"/>, or for the last item in
        /// a list, <see cref="SqlConditionOperator.NotSpecified"/>
        /// </value>
        public SqlConditionOperator Operator
        {
            get => _operator;
            set => _operator = value;
        }
        #endregion


        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeConditionListExpression Clone()
        {
            return new SqlCodeConditionListExpression(_expression, _operator);
        }
        #endregion
    }
}
