namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and defines a statement to create a stored procedure.
    /// </summary>
    /// <seealso cref="SqlCodeStatement" />
    public sealed class SqlCodeCreateStoredProcedureStatement : SqlCodeStatement
    {
        #region Private Member Declarations        
        /// <summary>
        /// The owner object name expression.
        /// </summary>
        private SqlCodeDatabaseNameOwnerNameExpression? _owner;
        /// <summary>
        /// The name of the stored procedure.
        /// </summary>
        private string? _name;
        /// <summary>
        /// The parameters list.
        /// </summary>
        private SqlCodeParameterDefinitionExpressionCollection? _parameters;
        /// <summary>
        /// The statements within the statement.
        /// </summary>
        private SqlCodeStatementCollection? _statements;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeCreateStoredProcedureStatement"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeCreateStoredProcedureStatement() : base(SqlStatementType.CreateStoredProcedure)
        {
            _parameters = new SqlCodeParameterDefinitionExpressionCollection();
            _statements = new SqlCodeStatementCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeCreateStoredProcedureStatement"/> class.
        /// </summary>
        /// <param name="name">
        /// A string containing the name of the stored procedure.
        /// </param>
        public SqlCodeCreateStoredProcedureStatement(string name) : base(SqlStatementType.CreateStoredProcedure)
        {
            _name = name;
            _parameters = new SqlCodeParameterDefinitionExpressionCollection();
            _statements = new SqlCodeStatementCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeCreateStoredProcedureStatement"/> class.
        /// </summary>
        /// <param name="ownerName">
        /// A <see cref="SqlCodeDatabaseNameOwnerNameExpression"/> instance indicating the database object owner
        /// of the stored procedure.
        /// </param>
        /// <param name="name">
        /// A string containing the name of the stored procedure.
        /// </param>
        public SqlCodeCreateStoredProcedureStatement(SqlCodeDatabaseNameOwnerNameExpression? ownerName, string? name) 
            : base(SqlStatementType.CreateStoredProcedure)
        {
            if (ownerName != null)
                _owner = ownerName.Clone();

            _name = name;
            _parameters = new SqlCodeParameterDefinitionExpressionCollection();
            _statements = new SqlCodeStatementCollection();
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
                _parameters?.Clear();
                _statements?.Clear();
            }

            _name = null;
            _owner = null;
            _parameters = null;
            _statements = null;
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
            get => _name;
            set => _name = value;
        }
        /// <summary>
        /// Gets or sets the reference to the owner object name expression.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeDatabaseNameOwnerNameExpression"/> instance, or <b>null</b>.
        /// </value>
        public SqlCodeDatabaseNameOwnerNameExpression? Owner
        {
            get => _owner;
            set
            {
                _owner?.Dispose();
                if (value == null)
                    _owner = null;
                else
                    _owner = value.Clone();
            }
        }
        /// <summary>
        /// Gets the reference to the list of parameters for the stored procedure.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeParameterDefinitionExpressionCollection"/> containing the parameter definitions.
        /// </value>
        public SqlCodeParameterDefinitionExpressionCollection? Parameters => _parameters;
        /// <summary>
        /// Gets the reference to the list of statements that define the stored procedure.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeStatementCollection"/> containing the statement definitions.
        /// </value>
        public SqlCodeStatementCollection? Statements => _statements;
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeCreateStoredProcedureStatement Clone()
        {
            SqlCodeCreateStoredProcedureStatement item = new SqlCodeCreateStoredProcedureStatement(_owner, _name);
            if (_parameters != null)
            {
                foreach (SqlCodeParameterDefinitionExpression expression in _parameters)
                    item.Parameters!.Add(expression.Clone());
            }

            if (_statements != null)
            {
                foreach (SqlCodeStatement statement in _statements)
                    item.Statements!.Add(statement.Clone());
            }

            return item;
        }
        #endregion

    }
}