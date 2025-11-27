using Adaptive.Intelligence.Shared;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a SQL Server stored procedure.
    /// </summary>
    public sealed class SqlStoredProcedure : DisposableObjectBase
    {
        #region Private Member Declarations
        /// <summary>
        /// The name of the stored procedure.
        /// </summary>
        private string? _name;
        /// <summary>
        /// The object identifier for the stored procedure.
        /// </summary>
        private int _objectId;
        /// <summary>
        /// The created date.
        /// </summary>
        private DateTime _createdDate;
        /// <summary>
        /// The modified date.
        /// </summary>
        private DateTime? _modifiedDate;
        /// <summary>
        /// The definition / text content of the procedure.
        /// </summary>
        private string? _definition;
        /// <summary>
        /// Recompiled flag.
        /// </summary>
        private bool _isRecompiled;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStoredProcedure"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlStoredProcedure()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStoredProcedure"/> class.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="ISafeSqlDataReader"/> instance used to read the content from the data source.
        /// </param>
        public SqlStoredProcedure(ISafeSqlDataReader reader)
        {
            SetFromReader(reader);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _name = null;
            _definition = null;
            _modifiedDate = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the date the stored procedure was originally created.
        /// </summary>
        /// <value>
        /// A <see cref="DateTime"/> specifying the date and time value.
        /// </value>
        public DateTime CreatedDate => _createdDate;
        /// <summary>
        /// Gets or sets the text definition of the stored procedure.
        /// </summary>
        /// <value>
        /// A string containing the SQL text.
        /// </value>
        public string? Definition
        {
            get => _definition;
            set => _definition = value;
        }
        /// <summary>
        /// Gets a value indicating whether the stored procedure has been recompiled on the server.
        /// </summary>
        /// <value>
        ///   <b>true</b> if the stored procedure has been recompiled on the server; otherwise, <b>false</b>.
        /// </value>
        public bool IsRecompiled => _isRecompiled;
        /// <summary>
        /// Gets the date the stored procedure was last modified.
        /// </summary>
        /// <value>
        /// A <see cref="DateTime"/> specifying the date and time value, or <b>null</b>.
        /// </value>
        public DateTime? ModifiedDate => _modifiedDate;
        /// <summary>
        /// Gets the name of the stored procedure.
        /// </summary>
        /// <value>
        /// A string containing the name of the stored procedure.
        /// </value>
        public string Name => _name ?? string.Empty;
        /// <summary>
        /// Gets the object ID value assigned to the stored procedure by SQL Server.
        /// </summary>
        /// <value>
        /// An integer containing the object ID value.
        /// </value>
        public int ObjectId => _objectId;
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
            if (_definition == null)
            {
                return nameof(SqlStoredProcedure);
            }

            return _definition;
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Sets the property values from the supplied data reader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the content from the data source.
        /// </param>
        private void SetFromReader(ISafeSqlDataReader reader)
        {
            int index = 0;
            _name = reader.GetString(index++);
            _objectId = reader.GetInt32(index++);
            _createdDate = reader.GetDateTime(index++);
            _modifiedDate = reader.GetDateTimeNullable(index++);
            _definition = reader.GetString(index++);
            _isRecompiled = reader.GetBoolean(index);
        }
        #endregion
    }
}