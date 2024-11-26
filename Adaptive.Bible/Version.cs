using Adaptive.Intelligence.Shared;

namespace Adaptive.Bible
{
	/// <summary>
	/// Represents and contains the objects for a specific translation or version of the Bible.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	/// <seealso cref="IVersionIndexed" />
	/// <seealso cref="IIORecord"/> 
	public sealed class Version : DisposableObjectBase, IVersionIndexed, IIORecord
	{
		#region Private Member Declarations		
		/// <summary>
		/// The name of the translation / version.
		/// </summary>
		private string? _name;
		/// <summary>
		/// The books list.
		/// </summary>
		private BookCollection? _books;
		/// <summary>
		/// The version index.
		/// </summary>
		private int _versionIndex;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="Version"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public Version()
		{
			_books = new BookCollection();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Version"/> class.
		/// </summary>
		/// <param name="name">
		/// A string containing the name of the translation.
		/// </param>
		public Version(string name) : this()
		{
			_name = name;
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
				_books?.Clear();

			_books = null;
			_name = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets the reference to the list of <see cref="Book"/> instances.
		/// </summary>
		/// <value>
		/// A <see cref="BookCollection"/> containing the list.
		/// </value>
		public BookCollection Books
		{
			get
			{
				if (_books == null)
					return new BookCollection();
				else
					return _books;
			}
		}
		/// <summary>
		/// Gets or sets the name of the version/translation.
		/// </summary>
		/// <value>
		/// A string containing the name value.
		/// </value>
		public string Name
		{
			get
			{
				if (_name == null)
					return string.Empty;
				else
					return _name;
			}
			set
			{
				_name = value;
			}
		}
		/// <summary>
		/// Gets or sets the index of the related version record.
		/// </summary>
		/// <value>
		/// An integer specifying the index of the version record.
		/// </value>
		public int VersionIndex
		{
			get => _versionIndex;
			set => _versionIndex = value;
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Finds the book by name, or adds it if it does not exist.
		/// </summary>
		/// <param name="bookName">
		/// A string containing the name of the book.</param>
		/// <returns>
		/// The <see cref="Book"/> instance.
		/// </returns>
		public Book? FindOrAdd(string bookName)
		{
			Book? record = _books?.Find(x => x.Name == bookName);
			if (record == null && _books !=null)
			{
				record = new Book();
				record.Name = bookName;
				record.BookIndex = _books.Count;
				_books.Add(record);
			}
			return record;
		}
		/// <summary>
		/// Updates the relational index values of the child objects.
		/// </summary>
		public void IndexRelations()
		{
			_books?.IndexRelations(_versionIndex);
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
			_versionIndex = reader.ReadInt32();
			_books ??= new BookCollection();
			_books.Clear();
			_books.Load(reader);
		}
		/// <summary>
		/// Writes the content for the record to the specified binary writer instance.
		/// </summary>
		/// <param name="writer">
		/// The open <see cref="BinaryWriter" /> instance used to write the content.
		/// </param>
		public void Save(BinaryWriter writer)
		{
			writer.Write(Name);
			writer.Write(_versionIndex);
			_books?.Save(writer);
			writer.Flush();
		}
		#endregion
	}
}

