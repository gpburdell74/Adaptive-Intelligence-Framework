namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and contains a SQL statement that is one or more lines of comment text.
    /// </summary>
    /// <seealso cref="SqlCodeStatement" />
    public sealed class SqlCodeCommentStatement : SqlCodeStatement
    {
        #region Private Member Declarations        
        /// <summary>
        /// The comment text lines.
        /// </summary>
        private SqlCodeCommentExpressionCollection? _commentList;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeCommentStatement"/> class.
        /// </summary>
        public SqlCodeCommentStatement() : base(SqlStatementType.Comment)
        {
            _commentList = new SqlCodeCommentExpressionCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeCommentStatement"/> class.
        /// </summary>
        /// <param name="text">
        /// A string containing a comment line.
        /// </param>
        public SqlCodeCommentStatement(string? text) : base(SqlStatementType.Comment)
        {
            _commentList = new SqlCodeCommentExpressionCollection();
            _commentList.Add(new SqlCodeCommentExpression(text));
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
                _commentList?.Clear();

            _commentList = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the list of comment lines.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeCommentExpressionCollection"/> instance containing the list of items.
        /// </value>
        public SqlCodeCommentExpressionCollection? Comments => _commentList;
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeCommentStatement Clone()
        {
            SqlCodeCommentStatement statement = new SqlCodeCommentStatement();
            if (_commentList != null)
            {
                foreach (SqlCodeCommentExpression expression in _commentList)
                {
                    statement.Comments.Add(expression.Clone());
                }
            }
            return statement;
        }
        #endregion
    }
}