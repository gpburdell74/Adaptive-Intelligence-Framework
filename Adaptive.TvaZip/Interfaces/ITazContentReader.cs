using Adaptive.Intelligence.Shared;
using System.Reflection.PortableExecutable;

namespace Adaptive.Taz
{
    /// <summary>
    /// Provides the signature definition for classes that implment the process for writing clear or secure TAZ files.
    /// </summary>
    public interface ITazContentReader : IDisposable, IAsyncDisposable
    {
        #region Properties		
        /// <summary>
        /// Gets a value indicating whether the instance can read data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can read data; otherwise, <c>false</c>.
        /// </value>
        public bool CanRead { get; }
        #endregion

        #region Methods / Functions		
        /// <summary>
        /// Closes the file and all underlying readers and streams.
        /// </summary>
        Task CloseFileAsync();
        /// <summary>
        /// Performs the initialization and other operations to prepare to read an archive file.
        /// </summary>
        /// <returns>
        /// A value indicating whether the operation was successful.
        /// </returns>
        Task<bool> InitializeFileAsync();
        /// <summary>
        /// Reads the next byte array from the underlying stream.
        /// </summary>
        /// <remarks>
        /// This method assumes the array is preceded by an integer length indicator.
        /// </remarks>
        /// <returns>
        /// The byte array that was read, or <b>null</b> if the operation fails.
        /// </returns>
        byte[]? ReadArray();
        /// <summary>
        /// Reads the next byte array from the underlying stream.
        /// </summary>
        /// <param name="position">
        /// A <see cref="long"/> value indicating the position at which to begin reading.
        /// </param>
        /// <remarks>
        /// This method assumes the array is preceded by an integer length indicator.
        /// </remarks>
        /// <returns>
        /// The byte array that was read, or <b>null</b> if the operation fails.
        /// </returns>
        byte[]? ReadArray(long position);
        /// <summary>
        /// Reads the directory content into memory.
        /// </summary>
        /// <param name="directoryStart">
        /// A <see cref="long"/> specifying the location in the file at which to read the directory data.
        /// </param>
        /// <returns>
        /// A <see cref="TazDirectory"/> instance that was read from the file, or <b>null</b> if the operation failed.
        /// </returns>
        TazDirectory? ReadDirectory(long directoryStart);
        /// <summary>
        /// Reads the file header content into memory.
        /// </summary>
        /// <returns>
        /// A <see cref="TazFileHeader"/> instance that was read from the file, or <b>null</b> if the operation failed.
        /// </returns>
        TazFileHeader? ReadHeader();
        #endregion
    }
}
