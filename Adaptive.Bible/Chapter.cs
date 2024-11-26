using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Bible
{
	/// <summary>
	/// Represents a chapter in a book of the Bible.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	/// <seealso cref="IIORecord"/> 
	public sealed class Chapter : DisposableObjectBase, IChapterIndexed, IIORecord
	{
		#region Private Member Declarations		
		/// <summary>
		/// The list of verses.
		/// </summary>
		private VerseCollection? _verses;
		private int _bookIndex;
		private int _chapterNumber;
		private int _versionIndex;

		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="Chapter"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public Chapter()
		{
			_verses = new VerseCollection();
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
				_verses?.Clear();

			_verses = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets or sets the index of the related book record.
		/// </summary>
		/// <value>
		/// An integer containing the record index value.
		/// </value>
		public int BookIndex { get => _bookIndex; set => _bookIndex = value; }
		/// <summary>
		/// Gets or sets the number of the chapter.
		/// </summary>
		/// <value>
		/// An integer specifying the chapter number.
		/// </value>
		public int ChapterNumber { get => _chapterNumber; set => _chapterNumber = value; }
		/// <summary>
		/// Gets the reference to the list of verses.
		/// </summary>
		/// <value>
		/// A <see cref="VerseCollection"/> containing the list of verses.
		/// </value>
		public VerseCollection Verses
		{
			get
			{
				if (_verses != null)
					return _verses;
				else
					return new VerseCollection();
			}
		}
		/// <summary>
		/// Gets or sets the index of the related version record.
		/// </summary>
		/// <value>
		/// An integer containing the record index value.
		/// </value>
		public int VersionIndex { get => _versionIndex; set => _versionIndex = value; }

		#endregion

		#region Public Methods / Functions				
		/// <summary>
		/// Returns all the text in the chapter.
		/// </summary>
		/// <returns>
		/// A string containing the text of all verses in the chapter.
		/// </returns>
		public string AllText()
		{
			StringBuilder builder = new StringBuilder();
			foreach (Verse item in _verses)
			{
				builder.Append("<b>" + item.Number + "</b> ");
				builder.Append(item.Text + " ");
			}

			builder.AppendLine();
			return builder.ToString();
		}
		/// <summary>
		/// Finds the verse by number or creates and adds one if not already part of the verse collection.
		/// </summary>
		/// <param name="verseNo">
		/// An integer specifying the verse number.
		/// </param>
		/// <returns>
		/// The <see cref="Verse"/> instance.
		/// </returns>
		public Verse FindOrAddVerse(int verseNo)
		{
			Verse? instance;
			if (_verses != null)
			{
				instance = _verses.FindVerse(verseNo);
				if (instance == null)
				{
					instance = new Verse { Number = verseNo };
					_verses.Add(instance);
				}
			}
			else
				instance = new Verse();

			return instance;
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
			_versionIndex = versionIndex;
			_bookIndex = bookIndex;

			if (_verses != null)
				_verses.IndexRelations(versionIndex, bookIndex, _chapterNumber);
		}
		/// <summary>
		/// Loads the content for the record from the specified binary reader instance.
		/// </summary>
		/// <param name="reader">
		/// The open <see cref="BinaryReader" /> instance used to read the content.
		/// </param>
		public void Load(BinaryReader reader)
		{
			_bookIndex = reader.ReadInt32();
			_chapterNumber = reader.ReadInt32();
			_versionIndex = reader.ReadInt32();
			_verses ??= new VerseCollection();
			_verses.Load(reader);
		}
		/// <summary>
		/// Writes the content for the record to the specified binary writer instance.
		/// </summary>
		/// <param name="writer">
		/// The open <see cref="BinaryWriter" /> instance used to write the content.
		/// </param>
		public void Save(BinaryWriter writer)
		{
			writer.Write(_bookIndex);
			writer.Write(_chapterNumber);
			writer.Write(_versionIndex);
			_verses?.Save(writer);
		}
		/// <summary>
		/// Converts the numeric value of the chapter number to its equivalent string representation.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return _chapterNumber.ToString();
		}
		#endregion
	}
}