using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Contains a list of <see cref="SqlDatabase"/> instances.
    /// </summary>
    /// <seealso cref="NameIndexCollection{T}"/>
    public sealed class SqlDatabaseCollection : NameIndexCollection<SqlDatabase>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDatabaseCollection"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlDatabaseCollection()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDatabaseCollection"/> class.
        /// </summary>
        /// <param name="sourceList">
        /// An <see cref="IEnumerable{T}"/> of <see cref="SqlDatabase"/> list used to populate the collection.
        /// </param>
        public SqlDatabaseCollection(IEnumerable<SqlDatabase> sourceList)
        {
            AddRange(sourceList);
        }
        #endregion

        #region Protected Method Overrides		
        /// <summary>
        /// Gets the name / key value of the specified instance.
        /// </summary>
        /// <param name="item">The <typeparamref name="T" /> item top be stored in the collection.</param>
        /// <returns>
        /// The name / key value of the specified instance.
        /// </returns>
        /// <remarks>
        /// This is called from several methods, including the Add() method, to identify the instance
        /// being added.
        /// </remarks>
        protected override string GetName(SqlDatabase? item)
        {
            if (item == null)
                return string.Empty;
            else
                return item.Name;
        }
        #endregion
    }
}