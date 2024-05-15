using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.Schema
{
    /// <summary>
    /// Contains a list of <see cref="SqlColumn"/> instances.
    /// </summary>
    public sealed class SqlColumnCollection : NameIndexCollection<SqlColumn>
    {
        /// <summary>
        /// Gets the name / key value of the specified instance.
        /// </summary>
        /// <param name="item">The <see cref="SqlColumn"/> item top be stored in the collection.</param>
        /// <returns>
        /// The name / key value of the specified instance.
        /// </returns>
        /// <remarks>
        /// This is called from several methods, including the Add() method, to identify the instance
        /// being added.
        /// </remarks>
        protected override string GetName(SqlColumn item)
        {
            return item.ColumnName!;
        }
    }
}