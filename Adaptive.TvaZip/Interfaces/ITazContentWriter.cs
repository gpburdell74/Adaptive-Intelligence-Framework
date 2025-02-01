namespace Adaptive.Taz
{
    /// <summary>
    /// Provides the signature definition for classes that implement the process for writing clear or secure TAZ files.
    /// </summary>
    public interface ITazContentWriter : IDisposable, IAsyncDisposable
    {
        #region Properties		
        /// <summary>
        /// Gets a value indicating whether this instance can write to the file.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid and can write; otherwise, <c>false</c>.
        /// </value>
        bool CanWrite { get; }
        /// <summary>
        /// Gets the current position in the file.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying The current position.
        /// </value>
        public long CurrentPosition { get; }
        #endregion

        #region Methods / Functions		
        /// <summary>
        /// Closes the underlying file and disposes of all stream and writer instances.
        /// </summary>
        Task CloseFileAsync();
        /// <summary>
        /// Generates an SHA-512 hash of the provided data.
        /// </summary>
        /// <param name="data">
        /// A byte array containing the data to be hashed.
        /// </param>
        /// <returns>
        /// A byte array containing the hash value.
        /// </returns>
        byte[]? CreateHash(byte[] data);
        /// <summary>
        /// Ensures the content in the write buffers to written to the underlying stream.
        /// </summary>
        void Flush();
        /// <summary>
        /// Ensures the content in the write buffers to written to the underlying stream.
        /// </summary>
        Task FlushAsync();
        /// <summary>
        /// Performs the initialization and other operations to prepare to write an archive file.
        /// </summary>
        /// <returns>
        /// A value indicating whether the operation was successful.
        /// </returns>
        Task<bool> InitializeFileAsync();
        /// <summary>
        /// Writes the specified byte array to the underlying stream.
        /// </summary>
        /// <remarks>
        /// The data format is left to the specific implementation; the data may or may nit be preceded by a length
        /// or a null indicator, or any other meta data needed to read and understand the provided byte array.
        /// </remarks>
        /// <param name="data">
        /// A byte array containing the data to be written.
        /// </param>
        void WriteArray(byte[] data);
        /// <summary>
        /// Writes the directory to the underlying file.
        /// </summary>
        /// <param name="directory">
        /// The <see cref="TazDirectory"/> instance whose contents are to be written.
        /// </param>
        /// <returns>
        /// A <see cref="long"/> value indicating the position at which the directory begins.
        /// </returns>
        long WriteDirectory(TazDirectory directory);
        /// <summary>
        /// Writes the file header to the underlying file.
        /// </summary>
        /// <param name="directory">
        /// The <see cref="TazFileHeader"/> instance whose contents are to be written.
        /// </param>
        void WriteHeader(TazFileHeader header);
        #endregion
    }
}
