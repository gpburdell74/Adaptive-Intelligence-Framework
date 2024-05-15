namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Represents the results of a text character examination operation with resulting
    /// recommendations for text boxes that process key entry behavior.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class TextCharacterOpResult : DisposableObjectBase
    {
        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="TextCharacterOpResult"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public TextCharacterOpResult()
        {
            SetEventToHandled = true;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            NewText = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the new recommended cursor position.
        /// </summary>
        /// <value>
        /// An integer specifying the new cursor position.
        /// </value>
        public int CursorPosition { get; set; }
        /// <summary>
        /// Gets or sets the new recommended text value.
        /// </summary>
        /// <value>
        /// A string containing the new text value, or <b>null</b>.
        /// </value>
        public string? NewText { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to set the related event handled parameter, if present.
        /// </summary>
        /// <value>
        ///   <c>true</c> to mark the event has having been handled; otherwise, <c>false</c> to allow the
        /// base method to process the event.
        /// </value>
        public bool SetEventToHandled { get; set; }
        #endregion
    }
}
