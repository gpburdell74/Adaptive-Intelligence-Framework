namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Contains a list of <see cref="SqlTableType"/> instances.
    /// </summary>
    public sealed class SqlTableTypeCollection : List<SqlTableType>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableTypeCollection"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlTableTypeCollection()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableTypeCollection"/> class.
        /// </summary>
        /// <param name="sourceList">
        /// An <see cref="IEnumerable{T}"/> of <see cref="SqlTableType"/> list used to populate the collection.
        /// </param>
        public SqlTableTypeCollection(IEnumerable<SqlTableType> sourceList)
        {
            AddRange(sourceList);
        }
        #endregion
    }
}
