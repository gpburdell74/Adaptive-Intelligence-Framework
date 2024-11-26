namespace Adaptive.Bible
{
	/// <summary>
	/// Contains a list of <see cref="Book"/> instances.
	/// </summary>
	/// <seealso cref="IOCollection{T}" />
	/// <seealso cref="Book"/>
	/// <seealso cref="IIORecord"/> 
	public sealed class BookCollection : IOCollection<Book>
	{
		#region Constructor(s)		
		/// <summary>
		/// Initializes a new instance of the <see cref="BookCollection"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public BookCollection()
		{
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Updates the relational index values of the child objects.
		/// </summary>
		/// <param name="versionIndex">
		/// An integer indicating the index of the related version record.
		/// </param>
		public void IndexRelations(int versionIndex)
		{
			for (int count = 0; count < Count; count++)
			{
				this[count].VersionIndex = versionIndex;
				this[count].IndexRelations();
			}
		}
		#endregion
	}
}
