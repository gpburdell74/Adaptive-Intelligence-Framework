namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a SQL stored procedure name.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeStoredProcedureNameExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The SQL code.
        /// </summary>
        private string? _spName;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeStoredProcedureNameExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeStoredProcedureNameExpression()
        {
            _spName = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeStoredProcedureNameExpression"/> class.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the stored procedure.
        /// </param>
        public SqlCodeStoredProcedureNameExpression(string? name)
        {
            _spName = name;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _spName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        /// <value>
        /// A string containing the name of the stored procedure.
        /// </value>
        public string? Name
        {
            get => _spName;
            set => _spName = value;
        }
        #endregion


        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeStoredProcedureNameExpression Clone()
        {
            return new SqlCodeStoredProcedureNameExpression(_spName);
        }
        #endregion

    }
}
