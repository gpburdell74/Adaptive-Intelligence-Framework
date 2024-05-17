namespace Adaptive.Taz.Interfaces
{
	/// <summary>
	/// Provides the marker interface and signature for instances that may be serialized to a byte array.
	/// </summary>
	public interface IBinarySerializable
	{
		/// <summary>
		/// Populates the current instance from the provided byte array.
		/// </summary>
		/// <param name="data">
		/// A byte array containing the data for the instance, usually provided by <see cref="ToBytes"/>.
		/// </param>
		void FromBytes(byte[] data);
		/// <summary>
		/// Converts the content of the current instance to a byte array.
		/// </summary>
		/// <returns>
		/// A byte array containing the data for this instance.
		/// </returns>
		byte[] ToBytes();
	}
}
