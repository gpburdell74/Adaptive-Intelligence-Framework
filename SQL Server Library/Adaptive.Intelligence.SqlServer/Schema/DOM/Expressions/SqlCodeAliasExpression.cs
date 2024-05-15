namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a SQL alias value for a column or table.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeAliasExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The alias name.
        /// </summary>
        private string? _name;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeAliasExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeAliasExpression()
        {
            _name = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeAliasExpression"/> class.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the alias.
        /// </param>
        public SqlCodeAliasExpression(string? name)
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
        /// Gets or sets the name of the alias.
        /// </summary>
        /// <value>
        /// A string containing the name of the alias.
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
        public override SqlCodeAliasExpression Clone()
        {
            return new SqlCodeAliasExpression(_name);
        }
        #endregion

    }
}
