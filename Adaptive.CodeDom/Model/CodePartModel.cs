using Adaptive.Intelligence;
using Adaptive.Intelligence.Shared;

namespace Adaptive.CodeDom.Model
{
	/// <summary>
	/// Represents and manages the information and objects for specific parts of code that exist within a code section.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CodePartModel : DisposableObjectBase
	{
		#region Private Member Declarations		
		/// <summary>
		/// The content containing the code generation objects / information.
		/// </summary>
		private object? _content;
		/// <summary>
		/// The name for the item.
		/// </summary>
		private string? _name;
		/// <summary>
		/// The parameter XML comments list.
		/// </summary>
		private List<NameValuePair<string>>? _parameterComments;
		/// <summary>
		/// The XML remarks comment text.
		/// </summary>
		private string? _xmlRemarks;
		/// <summary>
		/// The XML returns comment text.
		/// </summary>
		private string? _xmlReturns;
		/// <summary>
		/// The XML summary comment text.
		/// </summary>
		private string? _xmlSummary;
		/// <summary>
		/// The XML value comment text.
		/// </summary>
		private string? _xmlValue;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="CodePartModel"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public CodePartModel()
		{
			_parameterComments = new List<NameValuePair<string>>();
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
				_parameterComments?.Clear();
			}

			_parameterComments = null;
			_name = null;
			_content = null;
			_xmlSummary = null;
			_xmlRemarks = null;
			_xmlReturns = null;
			_xmlValue = null;

			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets or sets the reference to the content used to generate the code.
		/// </summary>
		/// <value>
		/// An object containing the code generation content.
		/// </value>
		public object? Content
		{
			get => _content;
			set => _content = value;
		}
		/// <summary>
		/// Gets or sets the name.
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
		/// Gets the reference to the list of parameter comments.
		/// </summary>
		/// <value>
		/// A <see cref="List{T}"/> of <see cref="NameValuePair{string}"/> values.
		/// </value>
		public List<NameValuePair<string>>? ParameterComments => _parameterComments;
		/// <summary>
		/// Gets or sets the XML remarks comment content.
		/// </summary>
		/// <value>
		/// A string containing the text for the XML remarks comment, or <b>null</b> if not used/generated.
		/// </value>
		public string? Remarks
		{
			get => _xmlRemarks;
			set => _xmlRemarks = value;
		}
		/// <summary>
		/// Gets or sets the XML returns comment content.
		/// </summary>
		/// <value>
		/// A string containing the text for the XML returns comment, or <b>null</b> if not used/generated.
		/// </value>
		public string? Returns
		{
			get => _xmlReturns;
			set => _xmlReturns = value;
		}
		/// <summary>
		/// Gets or sets the XML summary comment content.
		/// </summary>
		/// <value>
		/// A string containing the text for the XML summary comment, or <b>null</b> if not used/generated.
		/// </value>
		public string? Summary
		{
			get => _xmlSummary;
			set => _xmlSummary = value;
		}
		/// <summary>
		/// Gets or sets the XML value comment content.
		/// </summary>
		/// <value>
		/// A string containing the text for the XML value comment, or <b>null</b> if not used/generated.
		/// </value>
		public string? Value
		{
			get => _xmlValue;
			set => _xmlValue = value;
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Adds the parameter comment.
		/// </summary>
		/// <param name="name">
		/// A string containing the name of the parameter.
		/// </param>
		/// <param name="text">
		/// A string containing the XML comment text
		/// .</param>
		public void AddParameterComment(string name, string text)
		{
			if (_parameterComments != null)
			{
				_parameterComments.Add(new NameValuePair<string>(name, text));
			}
		}
		#endregion
	}
}
