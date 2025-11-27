using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Represents a SQL query execution error.
    /// </summary>
    /// <remarks>
    /// This creates a "business" object representing the SqlError content.
    /// </remarks>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class SqlQueryError : DisposableObjectBase
    {
        #region Constructor / Dispose Methods        
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            Source = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the severity level of the error returned from SQL Server.
        /// </summary>
        /// <value>
        /// A byte value from 1 to 25 that indicates the severity level of the error. The default 
        /// is zero (0).
        /// </value>
        public byte Class { get; set; }
        /// <summary>
        /// Gets or sets the number indicating the column.
        /// </summary>
        /// <value>
        /// An integer value specifying the column.
        /// </value>
        public int Column { get; set; }
        /// <summary>
        /// Gets a value indicating whether this error message is informational.
        /// </summary>
        /// <value>
        ///   <b>true</b> if this instance is an informational message; otherwise, <b>false</b>.
        /// </value>
        public bool IsInformationalMessage => Class < 10;
        /// <summary>
        /// Gets a value indicating whether this instance represents a user error.
        /// </summary>
        /// <value>
        ///   <b>true</b> if this instance represents a user error; otherwise, <b>false</b>.
        /// </value>
        public bool IsUserError => Class >= 11 && Class <= 16;
        /// <summary>
        /// Gets a value indicating whether this instance represents a software or hardware failure.
        /// </summary>
        /// <value>
        ///   <b>true</b> if this instance represents a  software or hardware failure; otherwise, <b>false</b>.
        /// </value>
        public bool IsSoftwareHardwareError => Class >= 17 && Class <= 25;
        /// <summary>
        /// Gets a value indicating whether the error caused the SQL Server connection to close.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the error caused the SQL Server connection to close; otherwise, <b>false</b>.
        /// </value>
        public bool CausedConnectionToClose => Class >= 20;
        /// <summary>
        /// Gets or sets the line number within the Transact-SQL command batch or stored procedure
        /// that contains the error.
        /// </summary>
        /// <value>
        /// An integer specifying the line number within the Transact-SQL command batch or stored procedure that
        /// contains the error.
        /// </value>
        public int LineNumber { get; set; }
        /// <summary>
        /// Gets or sets the text describing the error.
        /// </summary>
        /// <value>
        /// A string specifying the text describing the error.
        /// </value>
        public string? Message { get; set; }
        /// <summary>
        /// Gets or sets a number that identifies the type of error. 
        /// </summary>
        /// <value>
        /// A string specifying a number that identifies the type of error. 
        /// </value>
        public int Number { get; set; }
        /// <summary>
        /// Gets or sets 
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public int Offset { get; set; }
        /// <summary>
        /// Gets or sets the name of the stored procedure or remote procedure call (RPC) that generated the error. 
        /// </summary>
        /// <value>
        /// A string specifying the name of the stored procedure or RPC.
        /// </value>
        public string? Procedure { get; set; }
        /// <summary>
        /// Gets or sets the name of the instance of SQL Server that generated the error.
        /// </summary>
        /// <value>
        /// A string specifying the name of the instance of SQL Server that generated the error.
        /// </value>
        public string? Server { get; set; }
        /// <summary>
        /// Gets or sets the name of the provider that generated the error.
        /// </summary>
        /// <value>
        /// A string specifying the name of the provider that generated the error.
        /// </value>
        public string? Source { get; set; }
        /// <summary>
        /// Gets or sets the state code value.
        /// </summary>
        /// <remarks>
        /// Some error messages can be raised at multiple points in the code for the Database 
        /// Engine. For example, an 1105 error can be raised for several different conditions.
        /// Each specific condition that raises an error assigns a unique state code.
        /// </remarks>
        /// <value>
        /// A byte value containing the state code.
        /// </value>
        public byte State { get; set; }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (Message == null)
            {
                return nameof(SqlQueryError);
            }

            return Message;
        }
        #endregion
    }
}
