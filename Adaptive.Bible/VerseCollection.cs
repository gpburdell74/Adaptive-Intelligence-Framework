namespace Adaptive.Bible
{
	/// <summary>
	/// Contains and manges a list of <see cref="Verse"/> instances.
	/// </summary>
	/// <seealso cref="List{T}" />
	/// <seealso cref="IIORecord"/> 
	public class VerseCollection : IOCollection<Verse>
	{
		#region Constructor(s)		
		/// <summary>
		/// Initializes a new instance of the <see cref="VerseCollection"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public VerseCollection()
		{
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Adds the verse to the collection.
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
		/// <param name="verseNumber">
		/// An integer specifying the verse number.
		/// </param>
		/// <param name="text">
		/// A string containing the text of the verse.
		/// </param>
		public void AddVerse(int versionIndex, int bookIndex, int chapterNumber, int verseNumber, string text)
		{
			Verse verse = new Verse();
			verse.VersionIndex = versionIndex;
			verse.BookIndex = bookIndex;
			verse.ChapterNumber = chapterNumber;
			verse.Number = verseNumber;
			verse.Text = text;

			Add(verse);
		}
		/// <summary>
		/// Finds the or add.
		/// </summary>
		/// <param name="verseNumber">The verse number.</param>
		/// <returns></returns>
		public Verse? FindVerse(int verseNumber)
		{
			return Find(x => x.Number == verseNumber);
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
		/// <param name="chapterNumber">
		/// An integer specifying the number of the containing chapter.
		/// </param>
		public void IndexRelations(int versionIndex, int bookIndex, int chapterNumber)
		{
			foreach (Verse item in this)
			{
				item.ChapterNumber = chapterNumber;
				item.BookIndex = bookIndex;
				item.VersionIndex = versionIndex;
			}
		}
		#endregion
	}
}
