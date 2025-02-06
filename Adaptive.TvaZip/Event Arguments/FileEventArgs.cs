namespace Adaptive.Taz
{
    /// <summary>
    /// Provides the event arguments definition for file-related events.
    /// </summary>
    /// <seealso cref="EventArgs" />
    public sealed class FileEventArgs : EventArgs
    {
        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="FileEventArgs"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public FileEventArgs()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileEventArgs"/> class.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the name of the file.
        /// </param>
        public FileEventArgs(string fileName)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileEventArgs"/> class.
        /// </summary>
        /// <param name="fileName">
        /// A string containing the name of the file.
        /// </param>
        /// <param name="ex">
        /// The <see cref="Exception"/> being reported.
        /// </param>
        public FileEventArgs(string fileName, Exception? ex)
        {

        }
        /// <summary>
        /// Finalizes an instance of the <see cref="FileEventArgs"/> class.
        /// </summary>
        ~FileEventArgs()
        {
            FileName = null;
            Exception = null;
        }
        #endregion

        #region Public Properties		
        /// <summary>
        /// Gets or sets the size of the compressed file, in bytes.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the size of the file after being compressed.
        /// </value>
        public long CompressedSize { get; set; } = -1;
        /// <summary>
        /// Gets or sets the reference to the exception being reported.
        /// </summary>
        /// <value>
        /// The <see cref="Exception"/> instance, or <b>null</b>.
        /// </value>
        public Exception? Exception { get; set; }
        /// <summary>
        /// Gets or sets the ordinal index of the file being processed.
        /// </summary>
        /// <value>
        /// An integer indicating the position of the file in the list of files being processed.
        /// </value>
        public int FileIndex { get; set; } = -1;
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// A string containing the name of the file, or <b>null</b>
        /// </value>
        public string? FileName { get; set; }
        /// <summary>
        /// Gets or sets the size of the file, in bytes.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the size of the file.
        /// </value>
        public long FileSize { get; set; }
        #endregion
    }
}
