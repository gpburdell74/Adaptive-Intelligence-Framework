namespace Adaptive.Bible
{
	/// <summary>
	/// Contains and manges a list of <see cref="IIORecord"/> implementation instances.
	/// </summary>
	/// <seealso cref="List{T}" />
	/// <seealso cref="IIORecord"/> 
	public class IOCollection<T> : List<T>, IIORecord
		where T:IIORecord
	{
		#region Constructor(s)		
		/// <summary>
		/// Initializes a new instance of the <see cref="IOCollection{T}"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public IOCollection()
		{
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Loads the content for the record from the specified binary reader instance.
		/// </summary>
		/// <param name="reader">
		/// The open <see cref="BinaryReader" /> instance used to read the content.
		/// </param>
		public void Load(BinaryReader reader)
		{
			Clear();

			int length = reader.ReadInt32();
			for(int count = 0; count < length; count++)
			{
				T instance = Activator.CreateInstance<T>();
				instance.Load(reader);
				Add(instance);
			}
		}
		/// <summary>
		/// Writes the content for the record to the specified binary writer instance.
		/// </summary>
		/// <param name="writer">
		/// The open <see cref="BinaryWriter" /> instance used to write the content.
		/// </param>
		public void Save(BinaryWriter writer)
		{
			writer.Write(Count);
			for (int count = 0; count < Count; count++)
				this[count].Save(writer);
			writer.Flush();
		}
		#endregion
	}
}
