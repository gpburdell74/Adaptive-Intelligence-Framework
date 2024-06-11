using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and defines the selection list portion of a SELECT statement.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class SqlCodeSelectClause : DisposableObjectBase
    {
        #region Private Member Declarations        
        /// <summary>
        /// The select items list.
        /// </summary>
        private SqlCodeSelectListItemExpressionCollection? _selectItemsList;
        /// <summary>
        /// The distinct flag.
        /// </summary>
        private bool _distinct;
        /// <summary>
        /// The TOP row count value, if specified.
        /// </summary>
        private int _topValue;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeSelectClause"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeSelectClause()
        {
            _selectItemsList = new SqlCodeSelectListItemExpressionCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeSelectClause"/> class.
        /// </summary>
        /// <param name="distinct">
        /// A value indicating whether to return only distinct rows.
        /// </param>
        public SqlCodeSelectClause(bool distinct)
        {
            _distinct = distinct;
            _selectItemsList = new SqlCodeSelectListItemExpressionCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeSelectClause"/> class.
        /// </summary>
        /// <param name="distinct">
        /// A value indicating whether to return only distinct rows.
        /// </param>
        /// <param name="topRows">
        /// An integer indicating the top number of rows to return.  A zero (0) value is treated
        /// as if a value was not specified.
        /// </param>
        public SqlCodeSelectClause(bool distinct, int topRows)
        {
            _distinct = distinct;
            _topValue = topRows;
            _selectItemsList = new SqlCodeSelectListItemExpressionCollection();
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
                _selectItemsList?.Clear();

            _selectItemsList = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets a value indicating whether to return only DISTINCT rows.
        /// </summary>
        /// <value>
        ///   <b>true</b> to render DISTINCT; otherwise, <b>false</b>.
        /// </value>
        public bool Distinct
        {
            get => _distinct;
            set => _distinct = value;
        }
        /// <summary>
        /// Gets the list of items to be selected in the statement.
        /// </summary>
        /// <example>
        /// The items in this list may include:
        /// SELECT *
        /// $ROWGUID
        /// [table].[column]
        /// -- comment line
        /// blank line
        /// </example>
        /// <value>
        /// A <see cref="SqlCodeSelectListItemExpressionCollection"/> instance containing the list of items to be selected.
        /// </value>
        public SqlCodeSelectListItemExpressionCollection SelectItemsList
        {
            get
            {
                if (_selectItemsList == null)
                    _selectItemsList = new SqlCodeSelectListItemExpressionCollection();
                return _selectItemsList;
            }
        }
        /// <summary>
        /// Gets or sets the top number of rows to return.
        /// </summary>
        /// <value>
        /// An integer indicating the top number of rows to return.  A zero (0) value is treated
        /// as if a value was not specified.
        /// </value>
        public int TopValue
        {
            get => _topValue;
            set => _topValue = value;
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public SqlCodeSelectClause Clone()
        {
            return new SqlCodeSelectClause(_distinct, _topValue);
        }
        #endregion

    }
}