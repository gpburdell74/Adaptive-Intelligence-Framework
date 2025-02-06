namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a reference to a SQL variable.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeVariableReferenceExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The variable name.
        /// </summary>
        private SqlCodeVariableNameExpression? _variableName;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeVariableReferenceExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeVariableReferenceExpression()
        {
            _variableName = new SqlCodeVariableNameExpression();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeVariableReferenceExpression"/> class.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the variable.
        /// </param>
        public SqlCodeVariableReferenceExpression(string name)
        {
            _variableName = new SqlCodeVariableNameExpression(name);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeVariableReferenceExpression"/> class.
        /// </summary>
        /// <param name="variableReference">
        /// A <see cref="SqlCodeVariableNameExpression"/> containing the name of the variable.
        /// </param>
        public SqlCodeVariableReferenceExpression(SqlCodeVariableNameExpression? variableReference)
        {
            _variableName = variableReference;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _variableName?.Dispose();
            _variableName = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeVariableNameExpression"/> instance containing the name of the variable.
        /// </value>
        public SqlCodeVariableNameExpression? Name
        {
            get => _variableName;
            set
            {
                _variableName?.Dispose();
                if (value == null)
                    _variableName = null;
                else
                    _variableName = value.Clone();
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
        public override SqlCodeVariableReferenceExpression Clone()
        {
            return new SqlCodeVariableReferenceExpression(_variableName);
        }
        #endregion

    }
}
