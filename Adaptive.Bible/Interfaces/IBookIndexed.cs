namespace Adaptive.Bible
{
	/// <summary>
	/// Provides the signature definition for items that are indexed to a specific book.
	/// </summary>
	public interface IBookIndexed : IVersionIndexed
	{
		/// <summary>
		/// Gets or sets the index of the related book record.
		/// </summary>
		/// <value>
		/// An integer specifying the index of the related book record.
		/// </value>
		int BookIndex { get; set; }
	}
}
