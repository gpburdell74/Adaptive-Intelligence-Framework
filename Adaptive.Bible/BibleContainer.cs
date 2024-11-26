using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Security.IO;
using System.Data;

namespace Adaptive.Bible
{
	/// <summary>
	/// Contains one or more versions of the Bible.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	/// <seealso cref="IIORecord" />
	public sealed class BibleContainer : DisposableObjectBase
	{
		#region Private Member Declarations		
		/// <summary>
		/// The list of versions.
		/// </summary>
		private VersionCollection? _versions;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="BibleContainer"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public BibleContainer()
		{
			_versions = new VersionCollection();
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
				_versions?.Clear();

			_versions = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets the reference to the list of versions / translations.
		/// </summary>
		/// <value>
		/// A <see cref="VersionCollection"/> instance containing the list of versions/translations.
		/// </value>
		public VersionCollection Versions
		{
			get
			{
				if (_versions == null)
					return new VersionCollection();
				else
					return _versions;
			}
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Loads the content from the specified source stream.
		/// </summary>
		/// <param name="sourceStream">
		/// The open, readable destination <see cref="Stream"/> to read from.
		/// </param>
		public void Load(Stream sourceStream)
		{
			BinaryReader reader = new BinaryReader(sourceStream);
			_versions?.Clear();
			_versions = new VersionCollection();
			_versions.Load(reader);
		}
		/// <summary>
		/// Saves the content to the specified destination stream.
		/// </summary>
		/// <param name="destinationStream">
		/// The open, writable destination <see cref="Stream"/> to write to.
		/// </param>
		public void Save(Stream destinationStream)
		{
			BinaryWriter writer = new BinaryWriter(destinationStream);
			_versions?.Save(writer);
			writer.Flush();
		}
		public void SecureLoad(string fileName, string userId, string passPhrase, int pin)
		{
			// Clear the current contents.
			_versions?.Clear();
			_versions = new VersionCollection();

			// Open the secure file and prepare the cryptographic engine.
			PsfFile secureFile = new PsfFile();
			secureFile.OpenForReading(fileName, userId, passPhrase, pin);

			// Decrypt the contents to a stream.
			MemoryStream? content = secureFile.Read();
			if (content != null)
			{
				// Read the data.
				Load(content);
				content.Dispose();
			}
			
			secureFile.Dispose();
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
		/// <summary>
		/// Saves the content to the specified destination file as encrypted content using the provided parameters
		/// to generate the encryption keys.
		/// </summary>
		/// <param name="fileName">
		/// A string containing the fully-qualified path and name of the file to be written.
		/// </param>
		/// <param name="userId">
		/// A string containing the user identifier value or other such passkey value to use.
		/// </param>
		/// <param name="passPhrase">
		/// A string containing the password or other pass code value to use.
		/// </param>
		/// <param name="pin">
		/// An integer containing the PIN value to use.
		/// </param>
		public void SecureSave(string fileName, string userId, string passPhrase, int pin)
		{
			// Create and open the secure file.
			PsfFile secureFile = new PsfFile();
			secureFile.OpenForWriting(fileName, userId, passPhrase, pin);

			// Store the content.
			MemoryStream savedByGrace = new MemoryStream(65536000);
			Save(savedByGrace);
			savedByGrace.Seek(0, SeekOrigin.Begin);

			secureFile.Write(savedByGrace);
			secureFile.Close();
			GC.Collect();
			savedByGrace.Dispose();
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
		#endregion
	}
}
