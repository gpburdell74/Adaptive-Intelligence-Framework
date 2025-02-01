using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.DynamicManagement
{
    /// <summary>
    /// Represents a row from the sys.dm_exec_connections dynamic management view in SQL Server,
    /// which provides information about the connections established to this instance of SQL Server
    /// and the details of each connection.
    /// </summary>
    public sealed class DmExecConnectionsEntity : DisposableObjectBase
    {
        #region Dispose Method
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            AuthScheme = null;
            ClientNetAddress = null;
            ClientTcpPort = 0;
            ConnectionId = null;
            ConnectTime = DateTime.MinValue;
            EncryptOption = null;
            EndpointId = null;
            LastRead = null;
            LastWrite = null;
            LocalNetAddress = null;
            LocalTcpPort = 0;
            MostRecentSessionId = null;
            NetPacketSize = null;
            NetTransport = null;
            NodeAffinity = null;
            NumReads = 0;
            NumWrites = 0;
            ParentConnectionId = null;
            ProtocolType = null;
            ProtocolVersion = null;
            SessionId = 0;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the authentication scheme.
        /// </summary>
        /// <value>
        /// A string containing the SQL Server/Windows Authentication scheme used with this connection.
        /// </value>
        public string? AuthScheme { get; set; }
        /// <summary>
        /// Gets or sets the network address of the client.
        /// </summary>
        /// <value>
        /// A string containing the host address of the client connecting to this server. 
        /// </value>
        public string? ClientNetAddress { get; set; }
        /// <summary>
        /// Gets or sets the TCP port of the client.
        /// </summary>
        /// <value>
        /// An integer containing the TCP port number of the client connecting to this server.
        /// </value>
        public int ClientTcpPort { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier for this connection.
        /// </summary>
        /// <value>
        /// A <see cref="Guid"/> specifying the unique identifier for this connection.
        /// </value>
        public Guid? ConnectionId { get; set; }
        /// <summary>
        /// Gets or sets the date/time when the connection was established.
        /// </summary>
        /// <value>
        /// A <see cref="DateTime"/> containing the time stamp for when connection was established.
        /// </value>
        public DateTime ConnectTime { get; set; }
        /// <summary>
        /// Gets or sets the ID of the database in use by the session.
        /// </summary>
        /// <value>
        /// An integer specifying the ID of the current database for each session,
        /// For Azure SQL Databases, the values are unique within a single database or an elastic pool, 
        /// but not within a logical server.
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
        /// Gets or sets the encryption true or false option.
        /// </summary>
        /// <value>
        /// A string containing "TRUE" OR "FALSE".
        /// </value>
        public string? EncryptOption { get; set; }
        /// <summary>
        /// Gets or sets the endpoint ID value.
        /// </summary>
        /// <value>
        /// An integer containing the identifier that describes what type of connection it is. 
        /// This <b>EndpointId</b>> can be used to query the sys.endpoints view.
        /// </value>
        public int? EndpointId { get; set; }
        /// <summary>
        /// Gets or sets the time of the last read operation.
        /// </summary>
        /// <value>
        /// A <see cref="DateTime"/> containing the time stamp for when the last read occurred over this connection.
        /// </value>
        public DateTime? LastRead { get; set; }
        /// <summary>
        /// Gets or sets the time of the last write operation.
        /// </summary>
        /// <value>
        /// A <see cref="DateTime"/> containing the time stamp for when the last write occurred over this connection.
        /// </value>
        public DateTime? LastWrite { get; set; }
        /// <summary>
        /// Gets or sets the local network address.
        /// </summary>
        /// <remarks>
        /// In Azure SQL Databases, this column always returns NULL.
        /// </remarks>
        /// <value>
        /// A string containing the IP address on the server that this connection targeted. Available only for 
        /// connections using the TCP transport provider.
        /// </value>
        public string? LocalNetAddress { get; set; }
        /// <summary>
        /// Gets or sets the local TCP port value.
        /// </summary>
        /// <value>
        /// An integer containing the server TCP port that this connection targeted if it were a connection 
        /// using the TCP transport.
        /// </value>
        public int? LocalTcpPort { get; set; }
        /// <summary>
        /// Gets or sets the session ID that is most recently associated with this connection.
        /// </summary>
        /// <value>
        /// A string containing the session ID for the most recent request associated with this connection. 
        /// (SOAP connections can be reused by another session.) 
        /// </value>
        public int? MostRecentSessionId { get; set; }
        /// <summary>
        /// Gets or sets the network packet size.
        /// </summary>
        /// <value>
        /// An integer containing Network packet size used for information and data transfer.
        /// </value>
        public int? NetPacketSize { get; set; }
        /// <summary>
        /// Gets or sets the network transport library used by this connection.
        /// </summary>
        /// <remarks>
        /// Note: Describes the physical transport protocol that is used by this connection.
        /// </remarks>
        /// <value>
        ///  When MARS is used, a string containing returns Session for each additional connection associated 
        ///  with a MARS logical session.
        /// </value>
        public string? NetTransport { get; set; }
        /// <summary>
        /// Gets or sets the node affinity.
        /// </summary>
        /// <value>
        /// A short integer identifying the memory node to which this connection has affinity.
        /// </value>
        public short? NodeAffinity { get; set; }
        /// <summary>
        /// Gets or sets the number of reads performed on this connection.
        /// </summary>
        /// <value>
        /// An integer specifying the number of byte reads that have occurred over this connection.
        /// </value>
        public int? NumReads { get; set; }
        /// <summary>
        /// Gets or sets the number of writes performed on this connection.
        /// </summary>
        /// <value>
        /// An integer specifying the number of byte writes that have occurred over this connection.
        /// </value>
        public int? NumWrites { get; set; }
        /// <summary>
        /// Gets or sets the parent connection ID, if any.
        /// </summary>
        /// <value>
        /// A <see cref="Guid"/> Identifying the primary connection that the MARS session is using.
        /// </value>
        public Guid? ParentConnectionId { get; set; }
        /// <summary>
        /// Gets or sets the type of protocol being used.
        /// </summary>
        /// <value>
        /// A string specifying the protocol type of the payload. It currently distinguishes between TDS ("TSQL"), 
        /// "SOAP", and "Database Mirroring".
        /// </value>
        public string? ProtocolType { get; set; }
        /// <summary>
        /// Gets or sets the version of the protocol being used.
        /// </summary>
        /// <value>
        /// An integer specifying the version of the data access protocol associated with this connection
        /// </value>
        public int? ProtocolVersion { get; set; }
        /// <summary>
        /// Gets or sets the ID of the session associated with this connection.
        /// </summary>
        /// <value>
        /// A string containing 
        /// </value>
        public int SessionId { get; set; }
        #endregion
    }
}
