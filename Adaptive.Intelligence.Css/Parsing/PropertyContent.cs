using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Contains the text content for a CSS property definition.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class PropertyContent : DisposableObjectBase
    {
        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyContent"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public PropertyContent() : this(null, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyContent"/> class.
        /// </summary>
        /// <param name="name">
        /// A string containing the CSS property name, or <b>null</b>.
        /// </param>
        public PropertyContent(string? name) : this(name, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyContent"/> class.
        /// </summary>
        /// <param name="name">
        /// A string containing the CSS property name, or <b>null</b>.
        /// </param>
        /// <param name="value">
        /// A string containing the CSS property value, or <b>null</b>.
        /// </param>
        public PropertyContent(string? name, string? value)
        {
            Name = name;
            Value = value;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            Name = null;
            Value = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets a value indicating whether the instance is considered empty.
        /// </summary>
        /// <remarks>
        /// This returns <b>true</b> if either the <see cref="Name"/> or <see cref="Value"/> is not populated.
        /// </remarks>
        /// <value>
        ///   <c>true</c> if the instance is considered empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Value);   
            }
        }
        /// <summary>
        /// Gets or sets the raw property name.
        /// </summary>
        /// <value>
        /// A string containing the raw property name value.
        /// </value>
        public string? Name { get; set; }
        /// <summary>
        /// Gets or sets the raw property value.
        /// </summary>
        /// <value>
        /// A string containing the raw property value.
        /// </value>
        public string? Value { get; set; }
        #endregion
    }
}
