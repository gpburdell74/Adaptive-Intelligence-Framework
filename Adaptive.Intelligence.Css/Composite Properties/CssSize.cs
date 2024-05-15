using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Represents and manages the properties used to size an HTML element (width, height).
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class CssSize : DisposableObjectBase, ICssProperty
    {
        #region Dispose Method		
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
        /// Gets or sets the height of the related HTML element.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> instance indicating the size with the unit value, 
        /// or <b>null</b> if not set.
        /// </value>
        public FloatWithUnit? Height { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether any of the sub-properties on the instance have been set.
        /// </summary>
        /// <value>
        ///   <c>true</c> if any of the properties on the instance have been explicitly set; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// In CSS, the absence of a value is a value in and of itself.
        /// </remarks>
        public bool CanRender => (
            Height != null ||
            Width != null);
        /// <summary>
        /// Gets or sets the width of the related HTML element.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> instance indicating the size with the unit value, 
        /// or <b>null</b> if not set.
        /// </value>
        public FloatWithUnit? Width { get; set; }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Clears this instance and sets the property values to the "unset" state.
        /// </summary>
        public void Clear()
        {
            Height?.Dispose();
            Width?.Dispose();

            Height = null;
            Width = null;
        }
        /// <summary>
        /// Sets the height value from the provided string.
        /// </summary>
        /// <param name="heightValue">
        /// A string containing the width value.
        /// </param>
        public void SetHeight(string? heightValue)
        {
            Height?.Dispose();
            if (heightValue == null)
                Height = null;
            else
                Height = FloatWithUnit.Parse(heightValue);
        }
        /// <summary>
        /// Sets the measurement units in the properties to use the specified value.
        /// </summary>
        /// <param name="unit">The <see cref="CssUnits" /> enumerated value indicating the measurement unit, or <b>null</b>
        /// to unset all the unit properties.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetUnits(CssUnit? unit)
        {
            if (Height != null)
                Height.Unit = unit;
            if (Width != null)
                Width.Unit = unit;
        }
        /// <summary>
        /// Sets the width value from the provided string.
        /// </summary>
        /// <param name="widthValue">
        /// A string containing the width value.
        /// </param>
        public void SetWidth(string? widthValue)
        {
            Width?.Dispose();
            if (widthValue == null)
                Width = null;
            else
                Width = FloatWithUnit.Parse(widthValue);
        }
        /// <summary>
        /// Parses the CSS content for the instance.
        /// </summary>
        /// <param name="cssDefinition">A string containing the raw CSS definition / code defining the item.</param>
        public void ParseCss(string cssDefinition)
        {
            PropertyContent propertyDef = CssParsing.ParseProperty(cssDefinition);
            switch (propertyDef.Name)
            {
                case CssPropertyNames.Height:
                    Height = FloatWithUnit.Parse(propertyDef.Value);
                    break;

                case CssPropertyNames.Width:
                    Width = FloatWithUnit.Parse(propertyDef.Value);
                    break;
            }
            propertyDef.Dispose();
        }
        /// <summary>
        /// Converts to css.
        /// </summary>
        /// <returns>
        /// A string contianing the CSS code to be used for the item being represented.
        /// </returns>
        public string ToCss()
        {
            StringBuilder builder = new StringBuilder();

            if (Height != null)
                builder.Append(CssPropertyNames.Height + CssLiterals.CssSeparator + CssLiterals.Space + Height.ToString()+ CssLiterals.CssTerminator);

            if (Width != null)
                builder.Append(CssPropertyNames.Width + CssLiterals.CssSeparator + CssLiterals.Space + Width.ToString() + CssLiterals.CssTerminator);

			return builder.ToString();
        }
        #endregion
    }
}