// Ignore Spelling: Sql

using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer
{
    /// <summary>
    /// Contains a list of <see cref="DatabaseInfo"/> instances.
    /// </summary>
    /// <seealso cref="NameIndexCollection{T}" />
    public sealed class DatabaseInfoCollection : NameIndexCollection<DatabaseInfo>
    {
        /// <summary>
        /// Gets the name / key value of the specified instance.
        /// </summary>
        /// <param name="item">
        /// The <see cref="DatabaseInfo"/> item to be stored in the collection.
        /// </param>
        /// <returns>
        /// The name or key value of the specified instance.
        /// </returns>
        /// <remarks>
        /// This is called from several methods, including the Add() method, to identify the instance
        /// being added.
        /// </remarks>
        protected override string GetName(DatabaseInfo item)
        {
            return item.DatabaseName!;
        }
    }
}
