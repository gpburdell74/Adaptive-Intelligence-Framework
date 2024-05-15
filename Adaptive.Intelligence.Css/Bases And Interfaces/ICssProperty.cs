namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Provides the signature definition for defining and interactive with a CSS property.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ICssProperty : IDisposable 
    {
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the contents of the property can/should be rendered.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the property can/should be rendered; otherwise, <c>false</c>.
        /// </value>
        public bool CanRender { get; }
        #endregion

        #region Methods / Functions        
        /// <summary>
        /// Resets the content of the property and sets it to an "un-set" state.
        /// </summary>
        void Clear();
        /// <summary>
        /// Parses the CSS content for the instance.
        /// </summary>
        /// <param name="cssDefinition">
        /// A string containing the raw CSS definition / code defining the item.
        /// </param>
        void ParseCss(string? cssDefinition);
        /// <summary>
        /// Converts the object definition to its CSS code.
        /// </summary>
        /// <returns>
        /// A string contianing the CSS code to be used for the item being represented.
        /// </returns>
        string ToCss();
        #endregion
    }
}
