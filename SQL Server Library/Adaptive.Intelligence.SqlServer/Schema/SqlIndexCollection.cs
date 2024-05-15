namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Contains a list of <see cref="SqlIndex"/> instances.
    /// </summary>
    public sealed class SqlIndexCollection : List<SqlIndex>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlIndexCollection"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlIndexCollection()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlIndexCollection"/> class.
        /// </summary>
        /// <param name="sourceList">
        /// An <see cref="IEnumerable{T}"/> of <see cref="SqlIndex"/> list used to populate the collection.
        /// </param>
        public SqlIndexCollection(IEnumerable<SqlIndex> sourceList)
        {
            AddRange(sourceList);
        }
        #endregion
    }
}