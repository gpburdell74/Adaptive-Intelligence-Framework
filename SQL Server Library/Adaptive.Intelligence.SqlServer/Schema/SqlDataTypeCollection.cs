// Ignore Spelling: Sql

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Contains a list of <see cref="SqlDataType"/> instances. 
    /// </summary>
    public sealed class SqlDataTypeCollection : List<SqlDataType>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataTypeCollection"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlDataTypeCollection()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataTypeCollection"/> class.
        /// </summary>
        /// <param name="sourceList">
        /// An <see cref="IEnumerable{T}"/> of <see cref="SqlDataType"/> list used to populate the collection.
        /// </param>
        public SqlDataTypeCollection(IEnumerable<SqlDataType> sourceList)
        {
            AddRange(sourceList);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataTypeCollection"/> class.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="SafeSqlDataReader"/> instance used to read the content 
        /// from SQL Server. 
        /// </param>
        public SqlDataTypeCollection(SafeSqlDataReader reader)
        {
            while (reader.Read())
            {
                Add(new SqlDataType(reader));
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Gets the data type by the SQL system ID value. 
        /// </summary>
        /// <param name="dataTypeId">The SQL data type id value.</param>
        /// <returns>
        /// The matching <see cref="SqlDataType"/> instance, or <b>null</b>.
        /// </returns>
        public SqlDataType GetTypeById(int dataTypeId)
        {
            return
                (from types in this
                 where types.TypeId == dataTypeId
                 select types).FirstOrDefault()!;
        }
        #endregion
    }
}
