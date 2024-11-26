namespace Adaptive.Bible
{
	/// <summary>
	/// Provides the signature definition for items that are indexed to a specific translation / version.
	/// </summary>
	public interface IVersionIndexed
	{
		/// <summary>
		/// Gets or sets the index of the related version record.
		/// </summary>
		/// <value>
		/// An integer specifying the index of the version record.
		/// </value>
		int VersionIndex { get; set; }
	}
}
