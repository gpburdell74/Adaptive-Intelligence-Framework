namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a SQL Database Owner Object name.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeDatabaseNameOwnerNameExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The database owner object name.
        /// </summary>
        private string? _name;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeDatabaseNameOwnerNameExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeDatabaseNameOwnerNameExpression()
        {
            _name = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeDatabaseNameOwnerNameExpression"/> class.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the database owner object.
        /// </param>
        public SqlCodeDatabaseNameOwnerNameExpression(string? name)
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
        /// Gets or sets the name of the database owner object.
        /// </summary>
        /// <value>
        /// A string containing the name of the database owner object.
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
        public override SqlCodeDatabaseNameOwnerNameExpression Clone()
        {
            return new SqlCodeDatabaseNameOwnerNameExpression(_name);
        }
        #endregion
    }
}
