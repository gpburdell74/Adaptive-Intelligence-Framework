namespace Adaptive.Bible
{
	/// <summary>
	/// Contains a list of <see cref="Version"/> instances.
	/// </summary>
	/// <seealso cref="List{T}" />
	/// <seealso cref="IIORecord"/> 
	public sealed class VersionCollection : IOCollection<Version>
	{
		#region Constructor(s)		
		/// <summary>
		/// Initializes a new instance of the <see cref="VersionCollection"/> class.
		/// </summary>
		public VersionCollection()
		{
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Adds the version with the specified name.
		/// </summary>
		/// <param name="versionName">
		/// A string containing the name of the version.</param>
		public void Add(string versionName)
		{
			Version newVersion = new Version();
			newVersion.Name = versionName;
			Add(newVersion);
		}
		/// <summary>
		/// Finds the <see cref="Version"/> instance with the specified name.
		/// </summary>
		/// <param name="name">
		/// A string containing the name to search for.
		/// </param>
		/// <returns>
		/// The <see cref="Version"/> instance, or <b>null</b>.
		/// </returns>
		public Version? FindByName(string name)
		{
			return this.Find(x => x.Name == name);
		}
		/// <summary>
		/// Updates the relational index values of the child objects.
		/// </summary>
		public void IndexRelations()
		{
			for(int index = 0; index < Count; index++)
			{
				this[index].VersionIndex = index;
				this[index].IndexRelations();
			}
		}
		#endregion
	}
}
