namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a SQL Database name.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeDatabaseNameExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The Database name.
        /// </summary>
        private string? _name;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeDatabaseNameExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeDatabaseNameExpression()
        {
            _name = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeDatabaseNameExpression"/> class.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the database.
        /// </param>
        public SqlCodeDatabaseNameExpression(string? name)
        {
            _name = name;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _name = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// A string containing the name of the database.
        /// </value>
        public string? Name
        {
            get => _name;
            set => _name = value;
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeDatabaseNameExpression Clone()
        {
            return new SqlCodeDatabaseNameExpression(_name);
        }
        #endregion
    }
}
