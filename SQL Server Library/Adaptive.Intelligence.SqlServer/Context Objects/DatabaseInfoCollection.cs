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
		/// <param name="item">The <typeparamref name="T" /> item to be stored in the collection.</param>
		/// <returns>
		/// The name / key value of the specified instance.
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
