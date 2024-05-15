namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a reference to a SQL parameter.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeParameterReferenceExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The parameter name.
        /// </summary>
        private SqlCodeParameterNameExpression? _parameterName;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeParameterReferenceExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeParameterReferenceExpression()
        {
            _parameterName = new SqlCodeParameterNameExpression();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeParameterReferenceExpression"/> class.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the parameter.
        /// </param>
        public SqlCodeParameterReferenceExpression(string name)
        {
            _parameterName = new SqlCodeParameterNameExpression(name);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeParameterReferenceExpression"/> class.
        /// </summary>
        /// <param name="parameterReference">
        /// A <see cref="SqlCodeParameterNameExpression"/> containing the name of the parameter.
        /// </param>
        public SqlCodeParameterReferenceExpression(SqlCodeParameterNameExpression? parameterReference)
        {
            _parameterName = parameterReference?.Clone();
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _parameterName?.Dispose();
            _parameterName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeParameterNameExpression"/> instance containing the name of the parameter.
        /// </value>
        public SqlCodeParameterNameExpression? Name
        {
            get => _parameterName;
            set
            {
                _parameterName?.Dispose();
                if (value == null)
                    _parameterName = null;
                else
                    _parameterName = value.Clone();
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
        public override SqlCodeParameterReferenceExpression Clone()
        {
            return new SqlCodeParameterReferenceExpression(_parameterName);
        }
        #endregion

    }
}
