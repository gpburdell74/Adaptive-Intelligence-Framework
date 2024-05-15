namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a literal SQL code expression.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeLiteralExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The SQL code.
        /// </summary>
        private string? _expression;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeLiteralExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeLiteralExpression()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeLiteralExpression"/> class.
        /// </summary>
        /// <param name="sqlLiteral">
        /// A string containing the literal SQL code.
        /// </param>
        public SqlCodeLiteralExpression(string? sqlLiteral)
        {
            _expression = sqlLiteral;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _expression = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the SQL code that constitutes the literal expression.
        /// </summary>
        /// <value>
        /// A string containing the SQL literal value.
        /// </value>
        public string? Expression
        {
            get => _expression;
            set => _expression = value;
        }
        #endregion


        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeLiteralExpression Clone()
        {
            return new SqlCodeLiteralExpression(_expression);
        }
        #endregion

    }
}
