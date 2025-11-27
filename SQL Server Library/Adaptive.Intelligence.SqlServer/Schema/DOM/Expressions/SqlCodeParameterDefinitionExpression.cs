namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a SQL parameter definition.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeParameterDefinitionExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The parameter name.
        /// </summary>
        private string? _name;
        /// <summary>
        /// The data type of the parameter.
        /// </summary>
        private SqlCodeDataTypeSpecificationExpression? _dataTypeExpression;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeParameterDefinitionExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeParameterDefinitionExpression()
        {
            _dataTypeExpression = new SqlCodeDataTypeSpecificationExpression();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeParameterDefinitionExpression"/> class.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the parameter.
        /// </param>
        /// <param name="dataTypeExpression">
        /// A <see cref="SqlCodeDataTypeSpecificationExpression"/> instance representing the data type for the 
        /// parameter.
        /// </param>
        public SqlCodeParameterDefinitionExpression(string? name, SqlCodeDataTypeSpecificationExpression? dataTypeExpression)
        {
            _name = name;
            _dataTypeExpression = dataTypeExpression?.Clone();
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _dataTypeExpression?.Dispose();
            _dataTypeExpression = null;
            _name = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the data type of the parameter.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeDataTypeSpecificationExpression"/> specifying the data type of the parameter.
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
        /// Gets or sets the name of the parameter.
        /// </summary>
        /// <value>
        /// A string containing the name of the parameter.
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
        public override SqlCodeParameterDefinitionExpression Clone()
        {
            return new SqlCodeParameterDefinitionExpression(_name, _dataTypeExpression);
        }
        #endregion

    }
}
