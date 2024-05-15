namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents the expression of a data column name.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeColumnNameExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The data column name.
        /// </summary>
        private string? _columnName;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeColumnNameExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeColumnNameExpression()
        {
            _columnName = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeColumnNameExpression"/> class.
        /// </summary>
        /// <param name="columnName">
        /// A string containing the Column name.
        /// </param>
        public SqlCodeColumnNameExpression(string? columnName)
        {
            _columnName = columnName;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _columnName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the column being referenced.
        /// </summary>
        /// <value>
        /// A string containing the column name.
        /// </value>
        public string? ColumnName
        {
            get => _columnName;
            set => _columnName = value;
        }
        #endregion


        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeColumnNameExpression Clone()
        {
            return new SqlCodeColumnNameExpression(_columnName);
        }
        #endregion

    }
}
