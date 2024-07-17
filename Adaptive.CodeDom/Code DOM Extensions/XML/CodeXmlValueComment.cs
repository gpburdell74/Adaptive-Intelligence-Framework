using System.CodeDom;

namespace Adaptive.CodeDom
{
	/// <summary>
	/// Represents an XML value comment.
	/// </summary>
	public sealed class CodeXmlValueComment : CodeCommentStatement
	{
		#region Private Member Declarations		
		/// <summary>
		/// The comment text.
		/// </summary>
		private string? _commentText;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeXmlValueComment"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public CodeXmlValueComment() : base(null, true)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeXmlValueComment"/> class.
		/// </summary>
		/// <param name="commentText">
		/// A string containing the comment text.
		/// </param>
		public CodeXmlValueComment(string commentText) : base(commentText, true)
		{
			_commentText = commentText;
		}
		#endregion

		#region Public Properties		
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