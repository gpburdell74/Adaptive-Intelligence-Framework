namespace Adaptive.Bible
{
	/// <summary>
	/// Contains and manges a list of <see cref="Chapter"/> instances.
	/// </summary>
	/// <seealso cref="List{T}" />
	/// <seealso cref="IIORecord"/> 
	public class ChapterCollection : IOCollection<Chapter>
	{
		#region Constructor(s)		
		/// <summary>
		/// Initializes a new instance of the <see cref="ChapterCollection"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public ChapterCollection()
		{
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Adds the Chapter to the collection.
		/// </summary>
		/// <param name="versionIndex">
		/// An integer specifying the index of the related version record.
		/// </param>
		/// <param name="bookIndex">
		/// An integer specifying the index of the related book record.
		/// </param>
		/// <param name="chapterNumber">
		/// An integer specifying the chapter number.
		/// </param>
		public void AddChapter(int versionIndex, int bookIndex, int chapterNumber)
		{
			Add(new Chapter
			{
				VersionIndex = versionIndex,
				BookIndex = bookIndex,
				ChapterNumber = chapterNumber
			});
		}
		/// <summary>
		/// Finds the specified chapter by number value.
		/// </summary>
		/// <param name="chapterNumber">
		/// An integer specifying the chapter number to find.
		/// </param>
		/// <returns>
		/// The <see cref="Chapter"/> instance, if found; otherwise, returns <b>null</b>.
		/// </returns>
		public Chapter? FindChapter(int chapterNumber)
		{
			return Find(x => x.ChapterNumber == chapterNumber);
		}
		/// <summary>
		/// Finds the chapter by number, or if it does not exist, adds a new chapter with that number.
		/// </summary>
		/// <param name="chapterNumber">
		/// An integer indicating the chapter number.
		/// </param>
		/// <returns>
		/// The <see cref="Chapter"/> instance.
		/// </returns>
		public Chapter FindOrAdd(int chapterNumber)
		{
			Chapter? chapter = FindChapter(chapterNumber);
			if (chapter == null)
			{
				chapter = new Chapter { ChapterNumber = chapterNumber };
				Add(chapter);
			}
			return chapter;
		}
		/// <summary>
		/// Sets the relation index values for the instance.
		/// </summary>
		/// <param name="versionIndex">
		/// An integer specifying the index of the version record.
		/// </param>
		/// <param name="bookIndex">
		/// An integer specifying the index of the book record.
		/// </param>
		public void IndexRelations(int versionIndex, int bookIndex)
		{
			foreach (Chapter item in this)
			{
				item.BookIndex = bookIndex;
				item.VersionIndex = versionIndex;
				item.IndexRelations(versionIndex, bookIndex);
			}
		}
		#endregion
	}
}
