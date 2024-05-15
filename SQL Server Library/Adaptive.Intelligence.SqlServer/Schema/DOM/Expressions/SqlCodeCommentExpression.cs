namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a Comment SQL code expression.
    /// </summary>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeCommentExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The SQL comment text.
        /// </summary>
        private string? _comment;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeCommentExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeCommentExpression()
        {
            _comment = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeCommentExpression"/> class.
        /// </summary>
        /// <param name="sqlComment">
        /// A string containing the comment text.
        /// </param>
        public SqlCodeCommentExpression(string? sqlComment)
        {
            _comment = sqlComment;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _comment = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        /// <value>
        /// A string containing the comment text.
        /// </value>
        public string? Comment
        {
            get => _comment;
            set => _comment = value;
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeCommentExpression Clone()
        {
            return new SqlCodeCommentExpression(_comment);
        }
        #endregion
    }
}
