namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents the expression of a data table name.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeTableNameExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The data table name.
        /// </summary>
        private string? _tableName;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeTableNameExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeTableNameExpression()
        {
            _tableName = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeTableNameExpression"/> class.
        /// </summary>
        /// <param name="tableName">
        /// A string containing the table name.
        /// </param>
        public SqlCodeTableNameExpression(string? tableName)
        {
            _tableName = tableName;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _tableName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the table being referenced
        /// </summary>
        /// <value>
        /// A string containing the table name.
        /// </value>
        public string? TableName
        {
            get => _tableName;
            set => _tableName = value;
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeTableNameExpression Clone()
        {
            return new SqlCodeTableNameExpression(_tableName);
        }
        #endregion

    }
}
