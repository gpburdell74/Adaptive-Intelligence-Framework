using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Represents and manages the properties used to position an HTML element (left, top, bottom, right).
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class CssLocation : DisposableObjectBase, ICssProperty
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
        /// Gets or sets the co-ordinate for the bottom position/location setting.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> instance indicating the position of the bottom of the element, 
        /// or <b>null</b> if not set.
        /// </value>
        public FloatWithUnit? Bottom { get; set; }
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
            Left != null ||
            Top != null ||
            Bottom != null ||
            Right != null);
        /// <summary>
        /// Gets or sets the co-ordinate for the left position/location setting.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> instance indicating the position of the left of the element, or <b>null</b> if not set.
        /// </value>
        public FloatWithUnit? Left { get; set; }
        /// <summary>
        /// Gets or sets the co-ordinate for the right position/location setting.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> indicating the position of the right of the element, or <b>null</b> if not set.
        /// </value>
        public FloatWithUnit? Right { get; set; }
        /// <summary>
        /// Gets or sets the co-ordinate for the top position/location setting.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> indicating the position of the top of the element, or <b>null</b> if not set.
        /// </value>
        public FloatWithUnit? Top { get; set; }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Clears this instance and sets the property values to the "unset" state.
        /// </summary>
        public void Clear()
        {
            Left?.Dispose();
            Top?.Dispose();
            Bottom?.Dispose();
            Right?.Dispose();

            Left = null;
            Top = null;
            Bottom = null;
            Right = null;
        }
        /// <summary>
        /// Parses the CSS content for the instance.
        /// </summary>
        /// <param name="cssDefinition">
        /// A string containing the raw CSS definition / code defining the item.
        /// </param>
        public void ParseCss(string? cssDefinition)
        {
            Clear();

            if (!string.IsNullOrEmpty(cssDefinition))
            {
                int index = cssDefinition.IndexOf(CssLiterals.CssSeparator);
                if (index > -1)
                {
                    string propName = cssDefinition.Substring(0, index).Trim();
                    string propValue = cssDefinition.Substring(index + 1, cssDefinition.Length - (index + 1)).Trim();
                    switch (propName)
                    {
                        case CssPropertyNames.Bottom:
                            Bottom =  FloatWithUnit.Parse(propValue); 
                            break;

                        case CssPropertyNames.Left:
                            Left = FloatWithUnit.Parse(propValue);
                            break;

                        case CssPropertyNames.Right:
                            Right = FloatWithUnit.Parse(propValue);
                            break;

                        case CssPropertyNames.Top:
                            Top = FloatWithUnit.Parse(propValue);
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Converts the instance to a CSS definition.
        /// </summary>
        /// <returns>
        /// A string contianing the CSS code to be used for the item being represented.
        /// </returns>
        public string ToCss()
        {
            StringBuilder builder = new StringBuilder();

            if (Bottom != null)
                builder.Append(CssPropertyNames.Bottom + CssLiterals.CssSeparator + CssLiterals.Space + Bottom.ToString() + CssLiterals.CssTerminator);

			if (Left != null)
                builder.Append(CssPropertyNames.Left + CssLiterals.CssSeparator + CssLiterals.Space + Left.ToString() + CssLiterals.CssTerminator);

			if (Right != null)
                builder.Append(CssPropertyNames.Right + CssLiterals.CssSeparator + CssLiterals.Space + Right.ToString() + CssLiterals.CssTerminator);

			if (Top != null)
                builder.Append(CssPropertyNames.Top + CssLiterals.CssSeparator + CssLiterals.Space + Top.ToString() + CssLiterals.CssTerminator);

			return builder.ToString();
        }
        /// <summary>
        /// Sets the measurement units in the properties to use the specified value.
        /// </summary>
        /// <param name="unit">The <see cref="CssUnits" /> enumerated value indicating the measurement unit, or <b>null</b>
        /// to unset all the unit properties.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetUnits(CssUnit? unit)
        {
            if (Left != null)
                Left.Unit = unit;

            if (Top != null)
                Top.Unit = unit;

            if (Bottom != null)
                Bottom.Unit = unit;

            if (Right != null)
                Right.Unit = unit;
        }
        #endregion
    }
}
