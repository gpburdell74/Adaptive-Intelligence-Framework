namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents and contains the literal text to use as a SQL statement.
    /// </summary>
    /// <seealso cref="SqlCodeStatement" />
    public sealed class SqlCodeLiteralStatement : SqlCodeStatement
    {
        #region Private Member Declarations        
        /// <summary>
        /// The text.
        /// </summary>
        private string? _text;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeLiteralStatement"/> class.
        /// </summary>
        /// <param name="text">
        /// The literal text content.
        /// </param>
        public SqlCodeLiteralStatement(string? text) : base(SqlStatementType.Literal)
        {
            _text = text;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _text = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the literal SQL text.
        /// </summary>
        /// <value>
        /// A string specifying the text of the SQL statement.
        /// </value>
        public string? Text
        {
            get => _text;
            set => _text = value;
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeLiteralStatement Clone()
        {
            return new SqlCodeLiteralStatement(_text);
        }
        #endregion
    }
}