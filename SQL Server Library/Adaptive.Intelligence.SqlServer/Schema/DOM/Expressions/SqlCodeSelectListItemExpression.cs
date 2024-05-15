namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents the expression for an item in the selection list portion of a SELECT statement.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeSelectListItemExpression : SqlCodeExpression 
    {
        #region Private Member Declarations        
        /// <summary>
        /// The expression representing the item being selected.
        /// </summary>
        private SqlCodeExpression? _expression;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeSelectListItemExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeSelectListItemExpression()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeSelectListItemExpression"/> class.
        /// </summary>
        /// <param name="expression">
        /// A <see cref="SqlCodeExpression"/> implementation representing the item to be selected.
        /// </param>
        public SqlCodeSelectListItemExpression(SqlCodeExpression? expression)
        {
            _expression = expression?.Clone();
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _expression?.Dispose();
            _expression = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the reference to the expression representing the item to be selected.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeExpression"/> defining the expression.
        /// </value>
        public SqlCodeExpression? Expression
        {
            get => _expression;
            set
            {
                _expression?.Dispose();
                if (value == null)
                    _expression = null;
                else
                    _expression = value.Clone();
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
        public override SqlCodeSelectListItemExpression Clone()
        {
            return new SqlCodeSelectListItemExpression(_expression);
        }
        #endregion
    }
}
