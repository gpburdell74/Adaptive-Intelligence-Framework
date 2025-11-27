using Adaptive.Intelligence.Shared;
using Adaptive.SqlServer.Client;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Represents a SQL table-data-type definition.
    /// </summary>
    public sealed class SqlTableType : DisposableObjectBase
    {
        #region Private Constants

        private const string FieldTypeName = "typeName";
        private const string FieldSystemTypeId = "systemTypeId";
        private const string FieldObjectId = "objectId";
        private const string FieldIsUserDefined = "isUserDefined";
        private const string FieldIsTableType = "isTableType";

        #endregion

        #region Private Member Declarations        
        /// <summary>
        /// The columns defined on the type.
        /// </summary>
        private SqlTableTypeColumnCollection? _columns;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableType"/> class.
        /// </summary>
        public SqlTableType()
        {
            _columns = new SqlTableTypeColumnCollection();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableType"/> class.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="ISafeSqlDataReader"/> to read the data content from.
        /// </param>
        public SqlTableType(ISafeSqlDataReader? reader)
        {
            _columns = new SqlTableTypeColumnCollection();
            FromReader(reader);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _columns?.Clear();
            }

            TypeName = null;
            _columns = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the reference to the list of columns defined in the table type.
        /// </summary>
        /// <value>
        /// The <see cref="SqlTableTypeColumnCollection"/> containing the column list.
        /// </value>
        public SqlTableTypeColumnCollection? Columns => _columns;
        /// <summary>
        /// Gets the name of the table type definition.
        /// </summary>
        /// <value>
        /// A string specifying the name of the table type definition.
        /// </value>
        public string? TypeName { get; private set; }
        /// <summary>
        /// Gets the system type ID value for the table type.
        /// </summary>
        /// <value>
        /// A byte specifying the system type ID value for the table type.
        /// </value>
        public byte SystemTypeId { get; private set; }
        /// <summary>
        /// Gets the object ID value for the table type.
        /// </summary>
        /// <value>
        /// An integer specifying the object ID value for the table type.
        /// </value>
        public int ObjectId { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the definition is used-defined.
        /// </summary>
        /// <value>
        /// This value should always be <b>true</b>.
        /// </value>
        public bool IsUserDefined { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the definition is a table type.
        /// </summary>
        /// <value>
        /// This value should always be <b>true</b>.
        /// </value>
        public bool IsTableType { get; private set; }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Populates the instance from the supplied reader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="ISafeSqlDataReader"/> used to read the data row.
        /// </param>
        public void FromReader(ISafeSqlDataReader? reader)
        {
            if (reader != null)
            {
                TypeName = reader.GetString(FieldTypeName);
                SystemTypeId = reader.GetByte(FieldSystemTypeId);
                ObjectId = reader.GetInt32(FieldObjectId);
                IsUserDefined = reader.GetBoolean(FieldIsUserDefined);
                IsTableType = reader.GetBoolean(FieldIsTableType);
            }
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return TypeName!;
        }
        #endregion
    }
}