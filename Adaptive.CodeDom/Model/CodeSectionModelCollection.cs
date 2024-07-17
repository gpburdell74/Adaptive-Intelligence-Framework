using Adaptive.Intelligence.Shared;

namespace Adaptive.CodeDom.Model
{
	/// <summary>
	/// Contains a list of <see cref="CodeSectionModel"/> instances, stored by name.
	/// </summary>
	/// <seealso cref="NameIndexCollection" />
	public sealed class CodeSectionModelCollection : NameIndexCollection<CodeSectionModel>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeSectionModelCollection"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public CodeSectionModelCollection()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeSectionModelCollection"/> class.
		/// </summary>
		/// <param name="createStandardList">
		/// A value indicating whether to populate the collection with the standard list of sections.
		/// </param>
		public CodeSectionModelCollection(bool createStandardList)
		{
			if (createStandardList)
				CreateStandardList();
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
		protected override string GetName(CodeSectionModel item)
		{
			if (item.Name == null)
				return string.Empty;
			return item.Name;
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Gets the code section of the specified type.
		/// </summary>
		/// <param name="sectionType">
		/// A <see cref="CodeSectionType"/> enumerated value indicating the section type to find.
		/// </param>
		/// <returns>
		/// A <see cref="CodeSectionModel"/> instance if found; otherwise, returns <b>null</b>.
		/// </returns>
		public CodeSectionModel? GetSectionByType(CodeSectionType sectionType)
		{
			CodeSectionModel? model = null;

			foreach (CodeSectionModel section in this)
			{
				if (section.SectionType == sectionType)
					model = section;
			}
			return model;
		}
		#endregion

		#region Private Methods / Functions		
		/// <summary>
		/// Creates the standard list of code sections and appends them to the collection.
		/// </summary>
		private void CreateStandardList()
		{
			int index = 0;
			Add(new CodeSectionModel(CodeDomConstants.RegionPublicEvents, CodeSectionType.PublicEvents, index++));
			Add(new CodeSectionModel(CodeDomConstants.RegionPrivateConstants, CodeSectionType.PrivateConstants, index++));
			Add(new CodeSectionModel(CodeDomConstants.RegionPrivateMembers, CodeSectionType.PrivateMembers, index++));
			Add(new CodeSectionModel(CodeDomConstants.RegionConstructorDispose, CodeSectionType.ConstructorDispose, index++));
			Add(new CodeSectionModel(CodeDomConstants.RegionPublicProperties, CodeSectionType.PublicProperties, index++));
			Add(new CodeSectionModel(CodeDomConstants.RegionProtectedProperties, CodeSectionType.ProtectedProperties, index++));
			Add(new CodeSectionModel(CodeDomConstants.RegionAbstractMethods, CodeSectionType.AbstractMethods, index++));
			Add(new CodeSectionModel(CodeDomConstants.RegionProtectedMethods, CodeSectionType.ProtectedMethods, index++));
			Add(new CodeSectionModel(CodeDomConstants.RegionPublicMethods, CodeSectionType.PublicMethods, index++));
			Add(new CodeSectionModel(CodeDomConstants.RegionPrivateMethods, CodeSectionType.PrivateMethods, index++));
			Add(new CodeSectionModel(CodeDomConstants.RegionEventHandlers, CodeSectionType.EventHandlers, index++));
			Add(new CodeSectionModel(CodeDomConstants.RegionEventMethods, CodeSectionType.EventMethods, index));
		}
		#endregion
	}
}
