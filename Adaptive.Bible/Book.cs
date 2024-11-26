using Adaptive.Intelligence.Shared;

namespace Adaptive.Bible
{
	/// <summary>
	/// Represents a book of the Bible.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	/// <seealso cref="IBookIndexed" />
	/// <seealso cref="IIORecord"/> 
	public sealed class Book : DisposableObjectBase, IBookIndexed, IIORecord
	{
		#region Private Member Declarations		
		/// <summary>
		/// The name of the book.
		/// </summary>
		private string? _name;
		/// <summary>
		/// The search name - lowercase and spaces removed for faster searching.
		/// </summary>
		private string? _searchName;
		/// <summary>
		/// The ordinal index of the book.
		/// </summary>
		private int _index;
		/// <summary>
		/// The old testament flag.
		/// </summary>
		private bool _oldTestament;
		/// <summary>
		/// The chapters list.
		/// </summary>
		private ChapterCollection? _chapters;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="Book"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public Book()
		{
			_chapters = new ChapterCollection();
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
				_chapters?.Clear();
			_chapters = null;
			_name = null;
			_searchName = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets the reference to the list of chapters.
		/// </summary>
		/// <value>
		/// A <see cref="ChapterCollection"/> instance containing the list.
		/// </value>
		public ChapterCollection Chapters
		{
			get
			{
				if (_chapters == null)
					return new ChapterCollection();
				else
					return _chapters;
			}
		}
		/// <summary>
		/// Gets or sets the ordinal index of the book in the Bible.
		/// </summary>
		/// <value>
		/// An integer value indicating the ordinal index.
		/// </value>
		public int BookIndex
		{
			get => _index;
			set
			{
				_index = value;
				if (_index < 0)
					_index = 0;
				else if (_index > 65)
					_index = 65;
				_oldTestament = (_index < 39);
			}
		}
		/// <summary>
		/// Gets or sets the name of the book.
		/// </summary>
		/// <value>
		/// A string containing the name of the book.
		/// </value>
		public string Name
		{
			get
			{
				if (_name == null)
					return string.Empty;
				else
					return
						_name;
			}
			set
			{
				_name = value;
				_searchName = value.Replace(" ", "").ToLower();
			}
		}
		/// <summary>
		/// Gets a value indicating whether the book is in the Old Testament.
		/// </summary>
		/// <remarks>
		/// This value is based on the value of <see cref="BookIndex"/>.
		/// </remarks>
		/// <value>
		///   <c>true</c> if the book is in the Old Testament; otherwise, <c>false</c>.
		/// </value>
		public bool OldTestament { get => _oldTestament; }
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
		/// Adds the content of a chapter and verse to the book.
		/// </summary>
		/// <param name="chapterNo">
		/// A value indicating the chapter number.
		/// </param>
		/// <param name="verseNo">
		/// A value indicating the verse number.
		/// </param>
		/// <param name="text">
		/// A value containing the text of the verse.
		/// </param>
		public void AddChapterAndVerse(int chapterNo, int verseNo, string text)
		{
			if (_chapters != null)
			{
				// Create the chapter and verse if they do not exist.
				Chapter chapter = _chapters.FindOrAdd(chapterNo);
				Verse verse = chapter.FindOrAddVerse(verseNo);

				// Set the text.
				verse.Number = verseNo;
				verse.Text = text;
			}
		}
		/// <summary>
		/// Updates the relational index values of the child objects.
		/// </summary>
		public void IndexRelations()
		{
			Chapters.IndexRelations(VersionIndex, _index);
		}
		/// <summary>
		/// Loads the content for the record from the specified binary reader instance.
		/// </summary>
		/// <param name="reader">
		/// The open <see cref="BinaryReader" /> instance used to read the content.
		/// </param>
		public void Load(BinaryReader reader)
		{
			_name = reader.ReadString();
			_searchName = reader.ReadString();
			_index = reader.ReadInt32();
			_oldTestament = reader.ReadBoolean();
			_chapters ??= new ChapterCollection();
			_chapters.Load(reader);
		}
		/// <summary>
		/// Writes the content for the record to the specified binary writer instance.
		/// </summary>
		/// <param name="writer">
		/// The open <see cref="BinaryWriter" /> instance used to write the content.
		/// </param>
		public void Save(BinaryWriter writer)
		{
			if (_name == null)
				writer.Write(string.Empty);
			else
				writer.Write(_name);

			if (_searchName == null)
				writer.Write(string.Empty);
			else
				writer.Write(_searchName);

			writer.Write(_index);
			writer.Write(_oldTestament);
			_chapters?.Save(writer);
		}
	/// <summary>
	/// Returns a string representation of the book.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
		{
			if (OldTestament)
				return "[OT] - " + Name;
			else
				return "[NT] - " + Name;
		}
		#endregion
	}
}
