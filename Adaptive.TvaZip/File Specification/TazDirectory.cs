using Adaptive.Intelligence.Shared;
using Adaptive.Taz.Interfaces;

namespace Adaptive.Taz
{
    /// <summary>
    /// Represents and manages the directory for the TAZ file.
    /// </summary>
    /// <seealso cref="ExceptionTrackingBase" />
    public sealed class TazDirectory : ExceptionTrackingBase, IBinarySerializable
    {
        #region Private Member Declarations		
        /// <summary>
        /// The list of directory entries.
        /// </summary>
        private TazDirectoryEntryList? _entries;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="TazDirectory"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public TazDirectory()
        {
            _entries = new TazDirectoryEntryList();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TazDirectory"/> class.
        /// </summary>
        /// <param name="data">
        /// A byte array containing the entries for the directory.
        /// </param>
        public TazDirectory(byte[] data)
        {
            _entries = new TazDirectoryEntryList();
            _entries.FromBytes(data);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _entries?.Clear();
            }

            _entries = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties		
        /// <summary>
        /// Gets the refernece to the list of directory entries.
        /// </summary>
        /// <value>
        /// The <see cref="TazDirectoryEntryList"/> containing the list of entries.
        /// </value>
        public TazDirectoryEntryList? Entries => _entries;
        /// <summary>
        /// Gets the entry count.
        /// </summary>
        /// <value>
        /// An integer specifying the entry count.
        /// </value>
        public int EntryCount => _entries?.Count ?? 0;
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Adds the file information to the end of the directory listing.
        /// </summary>
        /// <param name="operation">
        /// The <see cref="FileCompressionOperation"/> instance containing the meta data for the file.
        /// </param>
        /// <param name="currentPosition">
        /// A <see cref="long"/> specifying the current position in the archive file at which the file's
        /// compressed data is written.
        /// </param>
        public void AddNewFile(FileCompressionOperation operation, long currentPosition)
        {
            _entries?.Add(new TazDirectoryEntry()
            {
                Hash = ByteArrayUtil.CopyToNewArray(operation.Hash),
                OrigName = operation.OrigName,
                OrigPath = operation.OrigPath,
                OrigSize = operation.OrigSize,
                CompressedSize = operation.CompressedSize,
                Position = currentPosition
            });
        }
        /// <summary>
        /// Populates the current instance from the provided byte array.
        /// </summary>
        /// <param name="data">
        /// A byte array containing the data for the instance, usually provided by <see cref="ToBytes" />.
        /// </param>
        public void FromBytes(byte[] data)
        {
            _entries?.Clear();
            _entries = null;
            _entries = new TazDirectoryEntryList();
            _entries.FromBytes(data);
        }
        /// <summary>
        /// Converts the content of the current instance to a byte array.
        /// </summary>
        /// <returns>
        /// A byte array containing the data for this instance.
        /// </returns>
        public byte[] ToBytes()
        {
            if (_entries == null)
                return new byte[0];
            else
                return _entries.ToBytes();
        }
        #endregion
    }
}
