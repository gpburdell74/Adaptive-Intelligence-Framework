using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using Adaptive.Taz.Interfaces;

namespace Adaptive.Taz
{
    /// <summary>
    /// Represents an entry in the directory of a TAZ file.
    /// </summary>
    /// <seealso cref="ExceptionTrackingBase" />
    public sealed class TazDirectoryEntry : ExceptionTrackingBase, IBinarySerializable
    {
        #region Private Member Declarations		
        /// <summary>
        /// The hash value for the original data.
        /// </summary>
        private byte[]? _hash;
        /// <summary>
        /// The name of the file.
        /// </summary>
        private string? _name;
        /// <summary>
        /// The original path of the file.
        /// </summary>
        private string? _path;
        /// <summary>
        /// The original size of the file, in bytes.
        /// </summary>
        private long _size;
        /// <summary>
        /// The original size of the file, in bytes.
        /// </summary>
        private long _compressedSize;
        /// <summary>
        /// The position in the file that the compressed data content is written.
        /// </summary>
        private long _position;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="TazDirectoryEntry"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public TazDirectoryEntry()
        {
        }
        public TazDirectoryEntry(byte[] data)
        {
            FromBytes(data);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TazDirectoryEntry"/> class.
        /// </summary>
        /// <param name="originalName">
        /// A string containing the name of the original file.
        /// </param>
        /// <param name="originalPath">
        /// A string containing the original path for the file.
        /// </param>
        /// <param name="originalSize">
        /// A <see cref="long"/> specifying the size of the original file.
        /// </param>
        /// <param name="compressedSize">
        /// A <see cref="long"/> specifying the size of the compressed data.
        /// </param>
        /// <param name="position">
        /// A <see cref="long"/> specifying the position in the archive file at which the compressed data starts.
        /// </param>
        /// <param name="hashValue">
        /// A byte array containing the SHA-512 hash value for the original  data.
        /// </param>
        private TazDirectoryEntry(string originalName, string? originalPath, long originalSize, long compressedSize,
            long position, byte[] hashValue)
        {
            _name = originalName;
            _path = originalPath;
            _size = originalSize;
            _compressedSize = compressedSize;
            _position = position;
            _hash = ByteArrayUtil.CopyToNewArray(hashValue);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            ByteArrayUtil.Clear(_hash);

            _hash = null;
            _name = null;
            _path = null;
            _size = 0;
            _compressedSize = 0;
            _position = 0;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties		
        /// <summary>
        /// Gets or sets the size of the compressed data
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the size of the compressed data, in bytes.
        /// </value>
        public long CompressedSize
        {
            get => _compressedSize;
            set => _compressedSize = value;
        }
        /// <summary>
        /// Gets the compression percentage value.
        /// </summary>
        /// <value>
        /// A <see cref="float"/> indicating the amount of compression achieved from the original data.
        /// </value>
        public float CompressionPercent
        {
            get
            {
                if (_compressedSize == 0 || _size == 0)
                    return 0;
                else
                    return (((float)_compressedSize / _size) * 100);
            }
        }
        /// <summary>
        /// Gets or sets the hash value for the original data.
        /// </summary>
        /// <value>
        /// A 64-element byte array containing the SHA512 hash of the original data.
        /// </value>
        public byte[]? Hash
        {
            get => ByteArrayUtil.CopyToNewArray(_hash);
            set
            {
                ByteArrayUtil.Clear(_hash);
                _hash = ByteArrayUtil.CopyToNewArray(value);
            }
        }
        /// <summary>
        /// Gets or sets the name of the original file.
        /// </summary>
        /// <value>
        /// A string containing the name of the file, or <b>null</b>.
        /// </value>
        public string? OrigName
        {
            get => _name;
            set => _name = value;
        }
        /// <summary>
        /// Gets or sets the path of the original file.
        /// </summary>
        /// <value>
        /// A string containing the original path of the file, or <b>null</b>.
        /// </value>
        public string? OrigPath
        {
            get => _path;
            set => _path = value;
        }
        /// <summary>
        /// Gets or sets the size of the compressed data.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the size of the compressed data, in bytes.
        /// </value>
        public long OrigSize
        {
            get => _size;
            set => _size = value;
        }
        /// <summary>
        /// Gets or sets the location in the file at which the compressed data starts.
        /// </summary>
        /// <value>
        /// A <see cref="long"/> specifying the location of the compressed data.
        /// </value>
        public long Position
        {
            get => _position;
            set => _position = value;
        }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Populates the current instance from the provided byte array.
        /// </summary>
        /// <param name="data">
        /// A byte array containing the data for the instance, usually provided by <see cref="ToBytes"/>.
        /// </param>
        public void FromBytes(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(ms);
            ms.Seek(0, SeekOrigin.Begin);

            _name = reader.ReadString();
            _path = reader.ReadString();
            _size = reader.ReadInt64();
            _compressedSize = reader.ReadInt64();
            _position = reader.ReadInt64();
            int hashLength = reader.ReadInt32();
            _hash = reader.ReadBytes(hashLength);

            reader.Dispose();
            ms.Dispose();
        }
        /// <summary>
        /// Converts the content of the current instance to a byte array.
        /// </summary>
        /// <returns>
        /// A byte array containing the data for this instance.
        /// </returns>
        public byte[] ToBytes()
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);

            writer.Write(_name!);
            writer.Write(_path!);
            writer.Write(_size);
            writer.Write(_compressedSize);
            writer.Write(_position);
            writer.Write(_hash!.Length);
            writer.Write(_hash);
            writer.Flush();

            stream.Seek(0, SeekOrigin.Begin);

            byte[] data = stream.ToArray();
            writer.Dispose();
            stream.Dispose();

            return data;
        }
        /// <summary>
        /// Returns a string value that represents the instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{_name} {AdaptiveFormat.FormatByteString(_size)} ({CompressionPercent}%)";
        }
        #endregion

        #region Public Static Methods / Functions
        /// <summary>
        /// Attempts to read the TAZ directory entry from the current position in the file.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="ISafeBinaryReader"/> instance used to perform the data read.
        /// </param>
        /// <remarks>
        /// The <see cref="ISafeBinaryReader"/> implementation may be for a (clear) binary reader,
        /// or a secure (cryptographic) binary reader.
        /// </remarks>
        /// <returns>
        /// If successful, returns a <see cref="TazDirectoryEntry"/> instance containing the header data;
        /// otherwise, returns <b>null</b>.
        /// </returns>
        public static TazDirectoryEntry? Read(ISafeBinaryReader reader)
        {
            TazDirectoryEntry? entry = null;

            if (reader != null)
            {

                // Read the standard property values.
                string? name = reader.ReadString();
                string? path = reader.ReadString();
                long size = reader.ReadInt64();
                long compressedSize = reader.ReadInt64();
                long position = reader.ReadInt64();

                // Read the hash array.
                byte[]? hash = reader.ReadByteArray();

                // Create the instance if everything is valid.
                if (!reader.HasExceptions && !string.IsNullOrEmpty(name) && hash != null)
                {
                    entry = new TazDirectoryEntry(name, path, size, compressedSize, position, hash);
                }
                ByteArrayUtil.Clear(hash);
            }
            return entry;
        }
        /// <summary>
        /// Writes the directory entry to the open file.
        /// </summary>
        /// <param name="header">
        /// The <see cref="TazFileHeader"/> instance whose contents are to be written.
        /// </param>
        /// <param name="writer">
        /// The <see cref="ISafeBinaryWriter"/> implementation to use to write the content.
        /// </param>
        public static void Write(TazDirectoryEntry entry, ISafeBinaryWriter writer)
        {
            // Write the standard properties.
            writer.Write(entry.OrigName);
            writer.Write(entry.OrigPath);
            writer.Write(entry.OrigSize);
            writer.Write(entry.CompressedSize);
            writer.Write(entry.Position);

            // Write the hash array.
            writer.WriteByteArray(entry.Hash);
            writer.Flush();
        }
        #endregion
    }
}
