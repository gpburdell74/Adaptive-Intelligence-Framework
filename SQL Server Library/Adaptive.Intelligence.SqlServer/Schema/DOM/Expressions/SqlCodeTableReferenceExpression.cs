namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a data table from which data is to be queried in a SELECT statement's FROM clause or
    /// JOIN clause.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeTableReferenceExpression : SqlCodeExpression
    {
        #region Private Member Declarations
        /// <summary>
        /// The table to start querying from.
        /// </summary>
        private SqlCodeTableNameExpression? _tableName;
        /// <summary>
        /// The owner object name.
        /// </summary>
        private SqlCodeDatabaseNameOwnerNameExpression? _ownerName;
        /// <summary>
        /// The alias name to use in the query.
        /// </summary>
        private SqlCodeAliasExpression? _alias;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeTableReferenceExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeTableReferenceExpression()
        {
            _tableName = new SqlCodeTableNameExpression();
            _ownerName = new SqlCodeDatabaseNameOwnerNameExpression();
            _alias = new SqlCodeAliasExpression();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeTableReferenceExpression"/> class.
        /// </summary>
        /// <param name="tableName">
        /// A string specifying the name of the table.
        /// </param>
        public SqlCodeTableReferenceExpression(string tableName) : this(TSqlConstants.DefaultDatabaseOwner, tableName, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeTableReferenceExpression"/> class.
        /// </summary>
        /// <param name="tableName">
        /// A string specifying the name of the table.
        /// </param>
        /// <param name="aliasName">
        /// A string specifying the alias to use for this table.
        /// </param>
        public SqlCodeTableReferenceExpression(string tableName, string? aliasName) : this(TSqlConstants.DefaultDatabaseOwner, tableName, aliasName)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeTableReferenceExpression"/> class.
        /// </summary>
        /// <param name="ownerName">
        /// A string specifying the database owner object name.
        /// </param>
        /// <param name="tableName">
        /// A string specifying the name of the table.
        /// </param>
        /// <param name="aliasName">
        /// A string specifying the alias to use for this table.
        /// </param>
        public SqlCodeTableReferenceExpression(string? ownerName, string tableName, string? aliasName)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            _tableName = new SqlCodeTableNameExpression(tableName);
            if (!string.IsNullOrEmpty(ownerName))
                _ownerName = new SqlCodeDatabaseNameOwnerNameExpression(ownerName);

            if (!string.IsNullOrEmpty(aliasName))
                _alias = new SqlCodeAliasExpression(aliasName);

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeTableReferenceExpression"/> class.
        /// </summary>
        /// <param name="ownerName">
        /// A <see cref="SqlCodeDatabaseNameOwnerNameExpression"/> instance specifying the database owner object name, or
        /// <b>null</b>.
        /// </param>
        /// <param name="tableName">
        /// A <see cref="SqlCodeTableNameExpression"/> specifying the name of the table.
        /// </param>
        /// <param name="aliasName">
        /// A <see cref="SqlCodeAliasExpression"/> instance specifying the alias to use for this table, or <b>null</b>.
        /// </param>
        public SqlCodeTableReferenceExpression(SqlCodeDatabaseNameOwnerNameExpression? ownerName, 
            SqlCodeTableNameExpression? tableName,
            SqlCodeAliasExpression? aliasName)
        {
            if (ownerName != null)
                _ownerName = ownerName.Clone();

            if (tableName != null)
                _tableName = tableName.Clone();

            if (aliasName != null)
                _alias = aliasName.Clone();
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
                _tableName?.Dispose();
                _ownerName?.Dispose();
                _alias?.Dispose();
            }
            _tableName = null;
            _ownerName = null;
            _alias = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the reference to the expression for the alias for this table in the query.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeAliasExpression"/> instance specifying a unique table alias name to use,
        /// or <b>null</b> if not specified.
        /// </value>
        public SqlCodeAliasExpression? Alias
        {
            get => _alias;
            set
            {
                _alias?.Dispose();
                if (value == null)
                    _alias = null;
                else
                    _alias = value.Clone();
            }
        }
        /// <summary>
        /// Gets or sets the reference to the expression for the database owner object name.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeDatabaseNameOwnerNameExpression"/> instance containing the object owner name.  If not specified this value
        /// defaults to "dbo".
        /// </value>
        public SqlCodeDatabaseNameOwnerNameExpression? OwnerName
        {
            get => _ownerName;
            set
            {
                _ownerName?.Dispose();
                if (value == null)
                    _ownerName = null;
                else
                    _ownerName = value.Clone();
            }
        }
        /// <summary>
        /// Gets or sets the reference to the expression for the name of the table to start querying from.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeTableNameExpression"/> containing the table name.
        /// </value>
        public SqlCodeTableNameExpression? TableName
        {
            get => _tableName;
            set
            {
                _tableName?.Dispose();
                if (value == null)
                    _tableName = null;
                else
                    _tableName = value.Clone();
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
        public override SqlCodeTableReferenceExpression Clone()
        {
            return new SqlCodeTableReferenceExpression(_ownerName, _tableName, _alias);
        }
        #endregion
    }
}