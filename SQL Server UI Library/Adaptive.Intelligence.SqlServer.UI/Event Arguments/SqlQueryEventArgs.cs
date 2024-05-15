namespace Adaptive.Intelligence.SqlServer.UI
{
    /// <summary>
    /// Provides an event arguments definition for events that involved editing/executing SQL queries.
    /// </summary>
    /// <seealso cref="EventArgs" />
    public sealed class SqlQueryEventArgs : EventArgs
    {
        #region Private Member Declarations        
        /// <summary>
        /// The error list
        /// </summary>
        private SqlQueryErrorCollection? _errorList;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlQueryEventArgs"/> class.
        /// </summary>
        public SqlQueryEventArgs() : this(null, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlQueryEventArgs"/> class.
        /// </summary>
        /// <param name="sqlQuery">
        /// A string containing the SQL Query text.
        /// </param>
        public SqlQueryEventArgs(string? sqlQuery) : this(sqlQuery, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlQueryEventArgs"/> class.
        /// </summary>
        /// <param name="sqlQuery">
        /// A string containing the SQL Query text.
        /// </param>
        /// <param name="fileName">
        /// A string containing the name of the file the query was last saved to.
        /// </param>
        public SqlQueryEventArgs(string? sqlQuery, string? fileName)
        {
            _errorList = new SqlQueryErrorCollection();
            FileName = fileName;
            QueryText = sqlQuery;
        }
        /// <summary>
        /// Finalizes an instance of the <see cref="SqlQueryEventArgs"/> class.
        /// </summary>
        ~SqlQueryEventArgs()
        {
            _errorList?.Clear();
            _errorList = null;
            FileName = null;
            QueryText = null;
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets a value indicating whether the related query operation was canceled.
        /// </summary>
        /// <value>
        ///   <b>true</b> if  the related query operation was canceled; otherwise, <b>false</b>.
        /// </value>
        public bool Cancelled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the related query operation resulted in one or more errors.
        /// </summary>
        /// <value>
        ///   <b>true</b> if  the related query operation resulted in one or more errors; otherwise, <b>false</b>.
        /// </value>
        public bool Error { get; set; }
        /// <summary>
        /// Gets or sets the list of the related SQL Error content.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public SqlQueryErrorCollection? Errors { get; set; }
        /// <summary>
        /// Gets or sets the name of the file the query was last loaded from or saved to.
        /// </summary>
        /// <value>
        /// A string containing the fully-qualified path and name of the file, or <b>null</b>.
        /// </value>
        public string? FileName { get; set; }
        /// <summary>
        /// Gets or sets the message for the operation.
        /// </summary>
        /// <value>
        /// A string containing the message text.
        /// </value>
        public string? Message { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the query operation has completed.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the query operation has completed; otherwise, <b>false</b>.
        /// </value>
        public bool QueryOperationComplete { get; set; }
        /// <summary>
        /// Gets or sets the query text.
        /// </summary>
        /// <value>
        /// A string containing the SQL query text.
        /// </value>
        public string? QueryText { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the query operation was successful.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the query operation was successful; otherwise, <b>false</b>.
        /// </value>
        public bool Success { get; set; }
        #endregion
    }
}
