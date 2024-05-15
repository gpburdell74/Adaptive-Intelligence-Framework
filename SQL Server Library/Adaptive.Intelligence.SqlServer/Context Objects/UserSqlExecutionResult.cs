using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Contains the complete results from executing a SQL query from an editor.
    /// </summary>
    /// <seealso cref="EventArgs" />
    public sealed class UserSqlExecutionResult : DisposableObjectBase
    {
        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSqlExecutionResult"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public UserSqlExecutionResult()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSqlExecutionResult"/> class.
        /// </summary>
        /// <param name="sqlQuery">
        /// A string containing the SQL Query text.
        /// </param>
        public UserSqlExecutionResult(string sqlQuery)
        {
        }
        /// <summary>
        /// Finalizes an instance of the <see cref="UserSqlExecutionResult"/> class.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
                Errors?.Clear();

            Errors = null;
            Message = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
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
        /// Gets or sets the message for the operation.
        /// </summary>
        /// <value>
        /// A string containing the message text.
        /// </value>
        public string? Message { get; set; }
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