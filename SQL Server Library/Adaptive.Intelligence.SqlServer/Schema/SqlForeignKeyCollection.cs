namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Contains a list of <see cref="SqlForeignKey"/> instances.
    /// </summary>
    public sealed class SqlForeignKeyCollection : List<SqlForeignKey>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlForeignKeyCollection"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlForeignKeyCollection()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlForeignKeyCollection"/> class.
        /// </summary>
        /// <param name="sourceList">
        /// An <see cref="IEnumerable{T}"/> of <see cref="SqlForeignKey"/> list used to populate the collection.
        /// </param>
        public SqlForeignKeyCollection(IEnumerable<SqlForeignKey> sourceList)
        {
            AddRange(sourceList);
        }
        #endregion
    }
}