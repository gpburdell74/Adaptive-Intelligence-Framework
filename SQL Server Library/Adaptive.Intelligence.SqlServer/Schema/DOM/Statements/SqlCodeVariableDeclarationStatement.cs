namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a SQL Statement that declares a variable.
    /// </summary>
    /// <example>
    /// DECLARE @Id NVARCHAR(128)
    /// [or]
    /// DECLARE @Id NVARCHAR(128) = NEWID()
    /// </example>
    /// <seealso cref="SqlCodeStatement" />
    public sealed class SqlCodeVariableDeclarationStatement : SqlCodeStatement
    {
        #region Private Member Declarations
        /// <summary>
        /// The variable definition.
        /// </summary>
        private SqlCodeVariableDefinitionExpression? _variableDefinition;
        /// <summary>
        /// The expression to assign to the new variable.
        /// </summary>
        private SqlCodeExpression? _valueExpression;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeVariableDeclarationStatement"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeVariableDeclarationStatement() : base(SqlStatementType.VariableDeclaration)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeVariableDeclarationStatement"/> class.
        /// </summary>
        /// <param name="variableDefinitionExpression">
        /// A <see cref="SqlCodeVariableDefinitionExpression"/> representing the variable declaration.
        /// </param>
        public SqlCodeVariableDeclarationStatement(SqlCodeVariableDefinitionExpression? variableDefinitionExpression) : base(SqlStatementType.VariableDeclaration)
        {
            _variableDefinition = variableDefinitionExpression ??
                                  throw new ArgumentNullException(nameof(variableDefinitionExpression));
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeVariableDeclarationStatement"/> class.
        /// </summary>
        /// <param name="variableDefinitionExpression">
        /// A <see cref="SqlCodeVariableDefinitionExpression"/> representing the variable declaration.
        /// </param>
        /// <param name="valueExpression">
        /// A <see cref="SqlCodeExpression"/>-derived instance representing the value being assigned to the variable
        /// as it is declared.
        /// </param>
        public SqlCodeVariableDeclarationStatement(SqlCodeVariableDefinitionExpression? variableDefinitionExpression,
            SqlCodeExpression? valueExpression) : base(SqlStatementType.VariableDeclaration)
        {
            _variableDefinition = variableDefinitionExpression ??
                                  throw new ArgumentNullException(nameof(variableDefinitionExpression));
            _valueExpression = valueExpression;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _variableDefinition?.Dispose();
                _valueExpression?.Dispose();    
            }

            _variableDefinition = null;
            _valueExpression = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the reference to the expression defining the variable.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeVariableDefinitionExpression"/> defining the variable.
        /// </value>
        public SqlCodeVariableDefinitionExpression? VariableDefinitionExpression
        {
            get => _variableDefinition;
            set
            {
                _variableDefinition?.Dispose();
                if (value == null)
                    _variableDefinition = null;
                else
                    _variableDefinition = value.Clone();
            }
        }
        /// <summary>
        /// Gets or sets the reference to the expression indicating the value being assigned.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeExpression"/> defining the value being assigned, or <b>null</b>
        /// </value>
        public SqlCodeExpression? ValueExpression
        {
            get => _valueExpression;
            set
            {
                _valueExpression?.Dispose();
                if (value == null)
                    _valueExpression = null;
                else
                    _valueExpression = value.Clone();
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
        public override SqlCodeVariableDeclarationStatement Clone()
        {
            return new SqlCodeVariableDeclarationStatement(_variableDefinition, _valueExpression);
        }
        #endregion
    }
}