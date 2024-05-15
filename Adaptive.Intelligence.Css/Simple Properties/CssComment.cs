using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Represents a comment in CSS.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class CssComment : DisposableObjectBase
    {
        #region Private Member Declarations        
        /// <summary>
        /// The comment text/
        /// </summary>
        private string _commentText;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="CssComment"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public CssComment()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CssComment"/> class.
        /// </summary>
        /// <param name="commentText">
        /// A string containing the comment text.
        /// </param>
        public CssComment(string commentText)
        {

        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _commentText = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        /// <value>
        /// A string containing the comment text.
        /// </value>
        public string CommentText
        {
            get => _commentText;
            set => _commentText = value;
        }
        #endregion

        #region
        #endregion

        #region
        #endregion

    }
}
