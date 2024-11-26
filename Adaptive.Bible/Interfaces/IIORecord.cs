namespace Adaptive.Bible
{
	/// <summary>
	/// Provides the interface for classes that can be used to read records from and write records to a binary stream.
	/// </summary>
	public interface IIORecord
	{
		/// <summary>
		/// Loads the content for the record from the specified binary reader instance.
		/// </summary>
		/// <param name="reader">
		/// The open <see cref="BinaryReader"/> instance used to read the content.
		/// </param>
		void Load(BinaryReader reader);
		/// <summary>
		/// Writes the content for the record to the specified binary writer instance.
		/// </summary>
		/// <param name="writer">
		/// The open <see cref="BinaryWriter"/> instance used to write the content.
		/// </param>
		void Save(BinaryWriter writer);
	}
}
