using Adaptive.Intelligence.Shared;

namespace Adaptive.Bible
{
	/// <summary>
	/// Represents a single verse entry.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	/// <seealso cref="IIORecord" />
	public sealed class Verse : DisposableObjectBase, IChapterIndexed, IIORecord
	{
		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="Verse"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public Verse()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Verse"/> class.
		/// </summary>
		/// <param name="versionIndex">
		/// An integer specifying the record index of the related version / translation record.
		/// </param>
		/// <param name="bookIndex">
		/// An integer specifying the record index of the book record.
		/// </param>
		/// <param name="chapterNumber">
		/// An integer specifying the chapter number.
		/// </param>
		/// <param name="verseNumber">
		/// An integer specifying the verse number.
		/// </param>
		/// <param name="text">
		/// A string containing the verse text.
		/// </param>
		public Verse(int versionIndex, int bookIndex, int chapterNumber, int verseNumber, string text)
		{
			VersionIndex = versionIndex;
			BookIndex = bookIndex;
			ChapterNumber = chapterNumber;
			Number = verseNumber;
			Text = text;
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			Text = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets or sets the index of the related book record.
		/// </summary>
		/// <value>
		/// An integer specifying the index of the related book record.
		/// </value>
		public int BookIndex { get; set; }
		/// <summary>
		/// Gets or sets the index of the related chapter record.
		/// </summary>
		/// <value>
		/// An integer specifying the index of the chapter record.
		/// </value>
		public int ChapterNumber { get; set; }
		/// <summary>
		/// Gets or sets the verse number.
		/// </summary>
		/// <value>
		/// An integer specifying the verse number.
		/// </value>
		public int Number { get; set; }
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// A string containing the text.
		/// </value>
		public string? Text { get; set; }
		/// <summary>
		/// Gets or sets the index of the related version record.
		/// </summary>
		/// <value>
		/// An integer specifying the index of the version record.
		/// </value>
		public int VersionIndex { get; set; }
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Loads the content for the record from the specified binary reader instance.
		/// </summary>
		/// <param name="reader">
		/// The open <see cref="BinaryReader" /> instance used to read the content.
		/// </param>
		public void Load(BinaryReader reader)
		{
			BookIndex = reader.ReadInt32();
			ChapterNumber = reader.ReadInt32();
			Number = reader.ReadInt32();
			Text = reader.ReadString();
			VersionIndex = reader.ReadInt32();
		}
		/// <summary>
		/// Writes the content for the record to the specified binary writer instance.
		/// </summary>
		/// <param name="writer">
		/// The open <see cref="BinaryWriter" /> instance used to write the content.
		/// </param>
		public void Save(BinaryWriter writer)
		{
			writer.Write(BookIndex);

			writer.Write(ChapterNumber);
			writer.Write(Number);
			if (Text == null)
				writer.Write(string.Empty);
			else
				writer.Write(Text);
			writer.Write(VersionIndex);
			writer.Flush();
		}
		/// <summary>
		/// Returns a string representation of the verse.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return ChapterNumber + ":" + Number + ": " + Text;
		}
		#endregion
	}
}

