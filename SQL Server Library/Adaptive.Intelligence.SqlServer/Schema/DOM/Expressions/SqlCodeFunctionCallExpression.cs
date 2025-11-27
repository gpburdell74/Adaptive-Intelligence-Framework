namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a functional within SQL code.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeFunctionCallExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The function name.
        /// </summary>
        private string? _functionName;
        /// <summary>
        /// The function parameters
        /// </summary>
        private SqlCodeExpressionCollection? _functionParameters;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeFunctionCallExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constrictor.
        /// </remarks>
        public SqlCodeFunctionCallExpression()
        {
            _functionParameters = new SqlCodeExpressionCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeFunctionCallExpression"/> class.
        /// </summary>
        /// <param name="functionName">
        /// A string containing the SQL name of the function to call.
        /// </param>
        public SqlCodeFunctionCallExpression(string? functionName) : this(functionName, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeFunctionCallExpression"/> class.
        /// </summary>
        /// <param name="functionName">
        /// A string containing the SQL name of the function to call.
        /// </param>
        /// <param name="parameterExpressions">
        /// An <see cref="IEnumerable{T}"/> list of <see cref="SqlCodeExpression"/> instances defining
        /// the parameters to pass to the function.
        /// </param>
        public SqlCodeFunctionCallExpression(string? functionName, IEnumerable<SqlCodeExpression>? parameterExpressions)
        {
            _functionName = functionName;
            _functionParameters = new SqlCodeExpressionCollection();
            if (parameterExpressions != null)
            {
                foreach (SqlCodeExpression item in parameterExpressions)
                {
                    _functionParameters.Add(item.Clone());
                }
            }
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _functionParameters?.Clear();
            _functionParameters = null;
            _functionName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the name of the function to be called.
        /// </summary>
        /// <value>
        /// A string containing the name of the function to be called.
        /// </value>
        public string? FunctionName
        {
            get => _functionName;
            set => _functionName = value;
        }
        /// <summary>
        /// Gets the reference to the function parameter value list.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeExpressionCollection"/> containing the <see cref="SqlCodeExpression"/> instances
        /// defining the parameter values, or <b>null</b> if no parameters are used.
        /// </value>
        public SqlCodeExpressionCollection? ParameterValueList => _functionParameters;
        #endregion


        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeFunctionCallExpression Clone()
        {
            return new SqlCodeFunctionCallExpression(_functionName, _functionParameters);
        }
        #endregion
    }
}