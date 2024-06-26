using Adaptive.Intelligence.Shared;

namespace Adaptive.CodeDom.Model
{
	/// <summary>
	/// Contains and manages the information of a code section.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CodeSectionModel : DisposableObjectBase
	{
		#region Private Member Declarations		
		/// <summary>
		/// The associated class name
		/// </summary>
		private string? _className;
		/// <summary>
		/// The section name / display text.
		/// </summary>
		private string? _name;
		/// <summary>
		/// The ordinal index of the collection.
		/// </summary>
		private int _ordinalIndex;
		/// <summary>
		/// The section type.
		/// </summary>
		private CodeSectionType _sectionType;
		/// <summary>
		/// The parts list.
		/// </summary>
		private List<CodePartModel>? _parts;
		#endregion

		#region Constructor / Dispose Methods
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeSectionModel"/> class.
		/// </summary>
		/// <param name="name">
		/// A string containing the name/display text for the section.
		/// </param>
		/// <param name="sectionType">
		/// A <see cref="CodeSectionType"/> enumerated value indicating the type of the section.
		/// Type of the section.</param>
		public CodeSectionModel(string name, CodeSectionType sectionType)
		{
			_name = name;
			_sectionType = sectionType;
			_parts = new List<CodePartModel>();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeSectionModel"/> class.
		/// </summary>
		/// <param name="name">
		/// A string containing the name/display text for the section.
		/// </param>
		/// <param name="sectionType">
		/// A <see cref="CodeSectionType"/> enumerated value indicating the type of the section.
		/// </param>
		/// <param name="ordinalIndex">
		/// An integer specifying the ordinal index of the section.
		/// </param>
		public CodeSectionModel(string name, CodeSectionType sectionType, int ordinalIndex)
		{
			_name = name;
			_sectionType = sectionType;
			_parts = new List<CodePartModel>();
			_ordinalIndex = ordinalIndex;
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
			{
				_parts?.Clear();
			}

			_parts = null;
			_className = null;
			_name = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the name of the associated class being generated.
		/// </summary>
		/// <value>
		/// A string containing the class name.
		/// </value>
		public string? ClassName
		{
			get => _className;
			set => _className = value;
		}
		/// <summary>
		/// Gets or sets the ordinal index of the section.
		/// </summary>
		/// <remarks>
		/// This value is used when generating sections in a specific order.
		/// </remarks>
		/// <value>
		/// An integer specifying the index value.
		/// </value>
		public int OrdinalIndex
		{
			get => _ordinalIndex;
			set => _ordinalIndex = value;
		}
		/// <summary>
		/// Gets or sets the section name / display text.
		/// </summary>
		/// <value>
		/// A string containing the name value.
		/// </value>
		public string? Name
		{
			get => _name;
			set => _name = value;
		}
		/// <summary>
		/// Gets or sets the code section type.
		/// </summary>
		/// <value>
		/// A <see cref="CodeSectionType"/> enumerated value indicating the type.
		/// </value>
		public CodeSectionType SectionType
		{
			get => _sectionType;
			set => _sectionType = value;
		}
		/// <summary>
		/// Gets the reference to the code parts list.
		/// </summary>
		/// <value>
		/// A <see cref="List{T}"/> of <see cref="CodePartModel"/> instances.
		/// </value>
		public List<CodePartModel>? Parts
		{
			get => _parts;
		}
		#endregion
	}
}
 