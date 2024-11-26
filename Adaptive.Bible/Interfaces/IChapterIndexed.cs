namespace Adaptive.Bible
{
	/// <summary>
	/// Provides the signature definition for items that are indexed to a specific chapter in a book.
	/// </summary>
	public interface IChapterIndexed : IBookIndexed
	{
		/// <summary>
		/// Gets or sets the index of the related chapter record.
		/// </summary>
		/// <value>
		/// An integer specifying the index of the chapter record.
		/// </value>
		int ChapterNumber { get; set; }
	}
}
