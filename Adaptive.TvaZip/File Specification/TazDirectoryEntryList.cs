using Adaptive.Intelligence.Shared;
using Adaptive.Taz.Interfaces;

namespace Adaptive.Taz
{
	/// <summary>
	/// Contains a list of <see cref="TazDirectoryEntry"/> instances.
	/// </summary>
	/// <seealso cref="S />
	public sealed class TazDirectoryEntryList : NameIndexCollection<TazDirectoryEntry>, IBinarySerializable
	{
		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="TazDirectoryEntryList"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public TazDirectoryEntryList()
		{
		}
		#endregion

		#region Protected Method Overrides
		/// <summary>
		/// Gets the name / key value of the specified instance.
		/// </summary>
		/// <param name="item">The <typeparamref name="T" /> item to be stored in the collection.</param>
		/// <returns>
		/// The name / key value of the specified instance.
		/// </returns>
		/// <remarks>
		/// This is called from several methods, including the Add() method, to identify the instance
		/// being added.
		/// </remarks>
		protected override string GetName(TazDirectoryEntry item)
		{
			return item.OrigPath + "\\" + item.OrigName;
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Populates the current instance from the provided byte array.
		/// </summary>
		/// <param name="data">A byte array containing the data for the instance, usually provided by <see cref="ToBytes" />.</param>
		public void FromBytes(byte[] data)
		{
			Clear();
			MemoryStream ms = new MemoryStream(data);
			BinaryReader reader = new BinaryReader(ms);

			int entryCount = reader.ReadInt32();
			for(int count = 0; count < entryCount; count++)
			{
				int length = reader.ReadInt32();
				byte[] entryContent = reader.ReadBytes(length);
				TazDirectoryEntry entry = new TazDirectoryEntry(entryContent);
				Add(entry);
				ByteArrayUtil.Clear(entryContent);
			}

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
			MemoryStream ms = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(ms);

			writer.Write(Count);
			foreach (TazDirectoryEntry entry in this)
			{
				byte[] entryBytes = entry.ToBytes();
				writer.Write(entryBytes.Length);
				writer.Write(entryBytes);
				ByteArrayUtil.Clear(entryBytes);
			}
			writer.Flush();

			ms.Seek(0,SeekOrigin.Begin);
			byte[] data = ms.ToArray();
			writer.Dispose();
			ms.Dispose();

			return data;
		}
		#endregion
	}
}
