using System.CodeDom;

namespace Adaptive.CodeDom
{
	/// <summary>
	/// Represents an XML Parameter comment.
	/// </summary>
	public sealed class CodeXmlParameterComment : CodeCommentStatement
	{
		#region Private Member Declarations		
		/// <summary>
		/// The parameter name.
		/// </summary>
		private string? _parameterName;
		/// <summary>
		/// The comment text.
		/// </summary>
		private string? _commentText;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeXmlParameterComment"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public CodeXmlParameterComment() : base(null, true)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeXmlParameterComment"/> class.
		/// </summary>
		/// <param name="parameterName">
		/// A string containing the name of the parameter.</param>
		/// <param name="commentText">
		/// A string containing the comment text.
		/// </param>
		public CodeXmlParameterComment(string parameterName, string commentText) : base(commentText, true)
		{
			_parameterName = parameterName;
			_commentText = commentText;
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets or sets the name of the parameter.
		/// </summary>
		/// <value>
		/// A string specifying the name of the parameter being commented on.
		/// </value>
		public string? ParameterName
		{
			get
			{
				if (_parameterName != null)
					return _parameterName;
				else
					return string.Empty;
			}
			set => _parameterName = value;
		}
		/// <summary>
		/// Gets or sets the comment text.
		/// </summary>
		/// <value>
		/// A string specifying the text of the comment.
		/// </value>
		public string? Text 
		{
			get
			{
				if (_commentText == null)
					return _commentText;
				else
					return string.Empty;
			}
			set => _commentText = value; 
		}
		#endregion
	}
}