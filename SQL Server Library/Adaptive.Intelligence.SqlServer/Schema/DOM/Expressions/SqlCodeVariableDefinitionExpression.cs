namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a SQL variable definition.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeVariableDefinitionExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The variable name.
        /// </summary>
        private string? _name;
        /// <summary>
        /// The data type of the variable.
        /// </summary>
        private SqlCodeDataTypeSpecificationExpression? _dataTypeExpression;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeVariableDefinitionExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeVariableDefinitionExpression()
        {
            _dataTypeExpression = new SqlCodeDataTypeSpecificationExpression();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeVariableDefinitionExpression"/> class.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the variable.
        /// </param>
        /// <param name="dataTypeExpression">
        /// A <see cref="SqlCodeDataTypeSpecificationExpression"/> instance representing the data type for the 
        /// variable.
        /// </param>
        public SqlCodeVariableDefinitionExpression(string? name, SqlCodeDataTypeSpecificationExpression? dataTypeExpression)
        {
            _name = name;
            if (dataTypeExpression != null)
            {
                _dataTypeExpression = dataTypeExpression.Clone();
            }
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _dataTypeExpression?.Dispose();
            }

            _dataTypeExpression = null;
            _name = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the data type of the variable.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeDataTypeSpecificationExpression"/> specifying the data type of the variable.
        /// </value>
        public SqlCodeDataTypeSpecificationExpression? DataTypeExpression
        {
            get => _dataTypeExpression;
            set
            {
                _dataTypeExpression?.Dispose();
                if (value == null)
                {
                    _dataTypeExpression = null;
                }
                else
                {
                    _dataTypeExpression = value.Clone();
                }
            }
        }
        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        /// <value>
        /// A string containing the name of the variable.
        /// </value>
        public string? Name
        {
            get => _name;
            set => _name = value;
        }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Returns an instance that is a copy of this instance.
        /// </summary>
        /// <returns>
        /// A new <see cref="SqlCodeVariableDefinitionExpression"/> containing the same values.
        /// </returns>
        public override SqlCodeVariableDefinitionExpression Clone()
        {
            return new SqlCodeVariableDefinitionExpression(_name, _dataTypeExpression);
        }
        #endregion
    }
}
