using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Represents and manages the settings for a CSS background property.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    /// <seealso cref="ICssProperty" />
    public sealed class CssBackground : DisposableObjectBase, ICssProperty
    {
        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="CssBackground"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public CssBackground()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CssBackground"/> class.
        /// </summary>
        /// <param name="background">
        /// A string containing the CSS background property definition.
        /// </param>
        public CssBackground(string background)
        {
            ParseCss(background);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            Clear();
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties		
        /// <summary>
        /// Gets or sets the attachment mode for a background image.
        /// </summary>
        /// <value>
        /// A background-image that will scroll with the page: scroll
        /// A background-image that will not scroll with the page: fixed
        /// </value>
        public string? Attachment { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the contents of the property can/should be rendered.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the property can/should be rendered; otherwise, <c>false</c>.
        /// </value>
        public bool CanRender
        {
            get
            {
                return Attachment != null ||
                          Color != null ||
                          Clip != null ||
                          Image != null ||
                          Origin != null ||
                          Position != null ||
                          Repeat != null ||
                          Size != null;
            }
        }
        /// <summary>
        /// Gets or sets the background-clip value.
        /// </summary>
        /// <value>
        /// Specifies how far the background should extend within an element:
        ///		border-box
        ///		padding-box
        ///		content-box
        ///		initial
        ///		inherit
        /// </value>
        public string? Clip { get; set; }
        /// <summary>
        /// Gets or sets the background color value.
        /// </summary>
        /// <value>
        /// A string containing the color specification, or <b>null</b>.
        /// </value>
        public string? Color { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance has any.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has any; otherwise, <c>false</c>.
        /// </value>
        public bool HasAny => (
            Attachment != null ||
            Color != null ||
            Clip != null ||
            Image != null ||
            Origin != null ||
            Position != null ||
            Repeat != null ||
            Size != null);
        /// <summary>
        /// Gets or sets the background image.
        /// </summary>
        /// <value>
        /// A string containing the URL for the image, or <b>null</b>.
        /// </value>
        public string? Image { get; set; }
        /// <summary>
        /// Gets or sets the background image origin specification.
        /// </summary>
        /// <value>
        /// A string containing the background-image starting position, or <b>null</b>.
        /// </value>
        public string? Origin { get; set; }
        /// <summary>
        /// Gets or sets the background image position specification.
        /// </summary>
        /// <value>
        /// A string containing the background-image position, or <b>null</b>.
        /// </value>
        public string? Position { get; set; }
        /// <summary>
        /// Gets or sets the background repeat behavior specification.
        /// </summary>
        /// <value>
        /// A string containing the background-image repeat behavior, or <b>null</b>.
        /// </value>
        public string? Repeat { get; set; }
        /// <summary>
        /// Gets or sets the background image size specification.
        /// </summary>
        /// <value>
        /// A string containing the background-image size, or <b>null</b>.
        /// </value>
        public string? Size { get; set; }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Clears the property values and resets the instance to an "unset" state.
        /// </summary>
        public void Clear()
        {
            Attachment = null;
            Clip = null;
            Color = null;
            Image = null;
            Origin = null;
            Position = null;
            Repeat = null;
            Size = null;
        }
        /// <summary>
        /// Parses the CSS content for the instance.
        /// </summary>
        /// <param name="cssDefinition">
        /// A string containing the raw CSS definition / code defining the item.
        /// </param>
        public void ParseCss(string cssDefinition)
        {
            PropertyContent propertyDef = CssParsing.ParseProperty(cssDefinition);
            switch(propertyDef.Name)
            {
                case CssPropertyNames.BackgroundAttachment:
                    Attachment = propertyDef.Value;
                    break;

                case CssPropertyNames.BackgroundClip:
                    Clip = propertyDef.Value;
                    break;

                case CssPropertyNames.BackgroundColor:
                    Color = propertyDef.Value;
                    break;

                case CssPropertyNames.BackgroundImage:
                    Image = propertyDef.Value;
                    break;

                case CssPropertyNames.BackgroundPosition:
                    Position = propertyDef.Value;
                    break;

                case CssPropertyNames.BackgroundOrigin:
                    Origin = propertyDef.Value;
                    break;

                case CssPropertyNames.BackgroundRepeat:
                    Repeat = propertyDef.Value;
                    break;

                case CssPropertyNames.BackgroundSize:
                    Size = propertyDef.Value;
                    break;
            }
        }
        /// <summary>
        /// Converts the property definition to CSS.
        /// </summary>
        /// <returns>
        /// A string contianing the CSS code to be used for the item being represented.
        /// </returns>
        public string ToCss()
        {
            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(Attachment))
                builder.Append(CssPropertyNames.BackgroundAttachment + CssLiterals.CssSeparator + CssLiterals.Space + Attachment);

            if (!string.IsNullOrEmpty(Clip))
                builder.Append(CssPropertyNames.BackgroundClip + CssLiterals.CssSeparator + CssLiterals.Space + Clip);

            if (!string.IsNullOrEmpty(Color))
                builder.Append(CssPropertyNames.BackgroundColor + CssLiterals.CssSeparator + CssLiterals.Space + Color);

            if (!string.IsNullOrEmpty(Image))
                builder.Append(CssPropertyNames.BackgroundImage + CssLiterals.CssSeparator + CssLiterals.Space + Image);

            if (!string.IsNullOrEmpty(Origin))
                builder.Append(CssPropertyNames.BackgroundOrigin  + CssLiterals.CssSeparator + CssLiterals.Space + Origin);

            if (!string.IsNullOrEmpty(Position))
                builder.Append(CssPropertyNames.BackgroundPosition + CssLiterals.CssSeparator + CssLiterals.Space + Position);

            if (!string.IsNullOrEmpty(Repeat))
                builder.Append(CssPropertyNames.BackgroundRepeat + CssLiterals.CssSeparator + CssLiterals.Space + Position);

            if (!string.IsNullOrEmpty(Size))
                builder.Append(CssPropertyNames.BackgroundSize + CssLiterals.CssSeparator + CssLiterals.Space + Position);

			builder.Append(CssLiterals.CssTerminator);
			return builder.ToString();
        }
        #endregion
    }
}
