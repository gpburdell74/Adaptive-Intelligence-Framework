using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.DynamicManagement
{
    /// <summary>
    /// Represents a row from the sys.dm_exec_sessions dynamic management view in SQL Server,
    /// which provides information about all active user connections and internal tasks.
    /// </summary>
    public sealed class DmExecSessionsEntity : DisposableObjectBase
    {
        #region Dispose Method
        /// <summary>
        /// Releases all resources used by the <see cref="DmExecSessions"/> object.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            ClientInterfaceName = null;
            ClientVersion = null;
            CpuTime = 0;
            DatabaseId = 0;
            DatabaseName = null;
            EndpointId = 0;
            HostName = null;
            HostProcessId = null;
            IsUserProcess = false;
            LastRequestEndTime = null;
            LastRequestStartTime = null;
            LogicalReads = 0;
            LogicalWrites = 0;
            LoginName = null;
            LoginTime = DateTime.MinValue;
            MemoryUsage = 0;
            NtDomain = null;
            NtUserName = null;
            OpenTransactionCount = 0;
            OriginalLoginName = null;
            PrevError = 0;
            ProgramName = null;
            Reads = 0;
            RowCount = 0;
            SecurityId = null;
            SessionId = null;
            Status = null;
            TotalElapsedTime = 0;
            TotalScheduledTime = 0;
            TransactionIsolationLevel = 0;
            Writes = 0;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the client interface name.
        /// </summary>
        /// <value>
        /// A string containing the name of the library/driver being used by the client to communicate with the server. 
        /// The value is NULL for internal sessions.
        /// </value>
        public string? ClientInterfaceName { get; set; }
        /// <summary>
        /// Gets or sets the version of the TDS protocol being used.
        /// </summary>
        /// <value>
        /// An integer specifying the version of the data access protocol associated with this connection
        /// </value>
        public int? ClientVersion { get; set; }
        /// <summary>
        /// Gets or sets the CPU usage time.
        /// </summary>
        /// <value>
        /// An integer containing the CPU time, in milliseconds, used by this session.
        /// </value>
        public int CpuTime { get; set; }
        /// <summary>
        /// Gets or sets the database ID.
        /// </summary>
        /// <value>
        /// An integer containing the ID of the database.
        /// </value>
        public int DatabaseId { get; set; }
        /// <summary>
        /// Gets or sets the name of the database in use by the session.
        /// </summary>
        /// <value>
        /// A string containing the name of the database as specified by <see cref="DatabaseId"/>.
        /// </value>
        public string? DatabaseName { get; set; }
        /// <summary>
        /// Gets or sets the endpoint identifier.
        /// </summary>
        /// <value>
        /// An integer containing the ID of the Endpoint associated with the session.
        /// </value>
        public int EndpointId { get; set; }
        /// <summary>
        /// Gets or sets the name of the host computer.
        /// </summary>
        /// <remarks>
        /// The client application provides the workstation name and can provide inaccurate data. Don't rely on 
        /// HOST_NAME as a security feature.
        /// </remarks>
        /// <value>
        /// A string containing the name of the client workstation that is specific to a session. The value is NULL for 
        /// internal sessions.
        /// </value>
        public string? HostName { get; set; }
        /// <summary>
        /// Gets or sets the host process identifier.
        /// </summary>
        /// <value>
        /// An integer specifying the process ID of the client program that initiated the session. The value is NULL 
        /// for internal sessions. 
        /// </value>
        public int? HostProcessId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the session is a user session.
        /// </summary>
        /// <value>
        ///   <c>true</c> if whether the session is a user session; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserProcess { get; set; }
        /// <summary>
        /// Gets or sets the last request end date/time.
        /// </summary>
        /// <value>
        /// A <see cref="DateTime"/> specifying the date/time of the last completion of a request on the session.
        /// </value>
        public DateTime? LastRequestEndTime { get; set; }
        /// <summary>
        /// Gets or sets the last request start date/time.
        /// </summary>
        /// <value>
        /// A <see cref="DateTime"/> specifying the date/time at which the last request on the session began. 
        /// This includes the currently executing request. 
        /// </value>
        public DateTime? LastRequestStartTime { get; set; }
        /// <summary>
        /// Gets or sets the logical reads.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the number of logical reads performed, by requests in this session, 
        /// during this session.
        /// </value>
        public long LogicalReads { get; set; }
        /// <summary>
        /// Gets or sets the logical writes.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the number of logical writes performed, by requests in this session, 
        /// during this session.
        /// </value>
        public long LogicalWrites { get; set; }
        /// <summary>
        /// Gets or sets the login name associated with the session.
        /// </summary>
        /// <value>
        /// A string containing the SQL Server login name under which the session is currently executing. For the original 
        /// login name that created the session, see <see cref="OriginalLoginName"/>.  Can be a SQL Server authenticated 
        /// login name or a Windows authenticated domain user name.
        /// </value>
        public string? LoginName { get; set; }
        /// <summary>
        /// Gets or sets the date and time the session was established.
        /// </summary>
        /// <remarks>
        /// Sessions that haven't completed logging in, at the time this DMV is queried, are shown with a login time of 
        /// "1900-01-01".
        /// </remarks>
        /// <value>
        /// The <see cref="DateTime"/> when session was established.
        /// </value>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// Gets or sets the memory usage.
        /// </summary>
        /// <value>
        /// An integer specifying the number of 8-KB pages of memory used by this session
        /// </value>
        public int MemoryUsage { get; set; }
        /// <summary>
        /// Gets or sets the NT domain name value.
        /// </summary>
        /// <value>
        /// A string containing the Windows domain name for the client if the session is using Windows Authentication or a 
        /// trusted connection. This value is NULL for internal sessions and non-domain users.
        /// </value>
        public string? NtDomain { get; set; }
        /// <summary>
        /// Gets or sets the NT user name value.
        /// </summary>
        /// <value>
        /// A string containing the Windows user name for the client if the session is using Windows Authentication or a 
        /// trusted connection. This value is NULL for internal sessions and non-domain users.
        /// </value>
        public string? NtUserName { get; set; }
        /// <summary>
        /// Gets or sets the number of open transactions for the session.
        /// </summary>
        /// <value>
        /// An integer specifying the number of currently open transactions.
        /// </value>
        public int OpenTransactionCount { get; set; }
        /// <summary>
        /// Gets or sets the name of the SQL Server login credential.
        /// </summary>
        /// <value>
        /// A string containing the SQL Server login name that the client used to create this session. Can be a SQL 
        /// Server authenticated login name, a Windows authenticated domain user name, or a contained database user. 
        /// The session could have gone through many implicit or explicit context switches after the initial connection. 
        /// For example, if EXECUTE AS is used.
        /// </value>
        public string? OriginalLoginName { get; set; }
        /// <summary>
        /// Gets or sets the number or id for the previous error.
        /// </summary>
        /// <value>
        /// An integer specifying the ID of the last error returned on the session.
        /// </value>
        public int PrevError { get; set; }
        /// <summary>
        /// Gets or sets the program name.
        /// </summary>
        /// <value>
        /// A string containing the name of the client program that initiated the session. The value is NULL for internal 
        /// sessions.
        /// </value>
        public string? ProgramName { get; set; }
        /// <summary>
        /// Gets or sets the number of reads performed on this session.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the number of byte reads that have occurred over this session.
        /// </value>
        public long Reads { get; set; }
        /// <summary>
        /// Gets or sets the row count.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the number of rows returned on the session up to this point.
        /// </value>
        public long RowCount { get; set; }
        /// <summary>
        /// Gets or sets the session security identifier.
        /// </summary>
        /// <value>
        /// A string containing the Microsoft Windows security ID associated with the login.
        /// </value>
        public string? SecurityId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the session.
        /// </summary>
        /// <value>
        /// A <see cref="short"/> (smallint) that identifies the session associated with each active primary 
        /// connection. 
        /// </value>
        public short? SessionId { get; set; }
        /// <summary>
        /// Gets or sets the session status.
        /// </summary>
        /// <value>
        /// A string containing the status of the session. Possible values are "Running", 
        /// "Sleeping", "Dormant", "Background", or "Preconnect".
        /// </value>
        public string? Status { get; set; }
        /// <summary>
        /// Gets or sets the total elapsed time.
        /// </summary>
        /// <value>
        /// An integer specifying the time, in milliseconds, since the session was established. 
        /// </value>
        public int TotalElapsedTime { get; set; }
        /// <summary>
        /// Gets or sets the total scheduled time.
        /// </summary>
        /// <value>
        /// An integer specifying the total time, in milliseconds, for which the session (requests within) were 
        /// scheduled for execution.
        /// </value>
        public int TotalScheduledTime { get; set; }
        /// <summary>
        /// Gets or sets the transaction isolation level.
        /// </summary>
        /// <value>
        /// A <see cref="short"/> specifying the transaction isolation level of the session.
        /// Values are :
        ///		0 = Unspecified
        ///		1 = ReadUncommitted
        ///		2 = ReadCommitted
        ///		3 = RepeatableRead
        ///		4 = Serializable
        ///		5 = Snapshot
        /// </value>
        public short TransactionIsolationLevel { get; set; }
        /// <summary>
        /// Gets or sets the number of reads performed on this session.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the number of byte reads that have occurred over this session.
        /// </value>
        public long Writes { get; set; }
        #endregion
    }

}
