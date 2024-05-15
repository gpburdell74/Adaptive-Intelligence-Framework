using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Contains a definition of a border side (such as border-left, border-top, etc.).
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class CssBorderSide : DisposableObjectBase, ICssProperty
    {
        #region Private Member Declarations
        /// <summary>
        /// The border color specification.
        /// </summary>
        private string? _color;
        /// <summary>
        /// The side being represented.
        /// </summary>
        private ElementSide? _side = ElementSide.AllSides;
        /// <summary>
        /// The style.
        /// </summary>
        private BorderStyle? _style = BorderStyle.NotSpecified;
        /// <summary>
        /// The width of the border.
        /// </summary>
        private FloatWithUnit? _width;
        #endregion

        #region Constructor / Dispose Methods                
        /// <summary>
        /// Initializes a new instance of the <see cref="CssBorderSide"/> class.
        /// </summary>
        /// <param name="side">
        /// The <see cref="ElementSide"/> enumerated value indicating which border is being represented.
        /// </param>
        public CssBorderSide(ElementSide side)
        {
            _side = side;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CssBorderSide"/> class.
        /// </summary>
        /// <param name="cssSpecification">
        /// A string containing the CSS specification code to be parsed.
        /// </param>
        public CssBorderSide(string cssSpecification)
        {
            ParseCss(cssSpecification);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CssBorderSide"/> class.
        /// </summary>
        /// <param name="side">
        /// The <see cref="ElementSide"/> enumerated value indicating which border is being represented.
        /// </param>
        /// <param name="color">
        /// A string containing the color specification.
        /// </param>
        public CssBorderSide(ElementSide side, string color) : this(side)
        {
            _color = color;
        }
        /// <param name="side">
        /// The <see cref="ElementSide"/> enumerated value indicating which border is being represented.
        /// </param>
        /// <param name="width">
        /// A <see cref="float"/> containing the width value.
        /// </param>
        /// <param name="unit">
        /// A <see cref="CssUnit"/> enumerated value indicating the unit of measurement.
        /// </param>
        public CssBorderSide(ElementSide side, float width, CssUnit unit) : this(side)
        {
            if (_width == null)
                _width = new FloatWithUnit();

            _width.Value = width;
            _width.Unit = unit;
        }
        /// <param name="side">
        /// The <see cref="ElementSide"/> enumerated value indicating which border is being represented.
        /// </param>
        /// <param name="color">
        /// A string containing the color specification.
        /// </param>
        /// <param name="width">
        /// A <see cref="float"/> containing the width value.
        /// </param>
        /// <param name="unit">
        /// A <see cref="CssUnit"/> enumerated value indicating the unit of measurement.
        /// </param>
        public CssBorderSide(ElementSide side, string color, float width, CssUnit unit) : this(side, color)
        {
            if (_width != null)
            {
                _width.Value = width;
                _width.Unit = unit;
            }
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        /// <returns></returns>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _width?.Dispose();
            }
            _color = null;
            _width = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
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
                return (_side != ElementSide.NotSpecifed) &&
                    (
                        !string.IsNullOrEmpty(_color) ||
                        _width != null ||
                        _style != BorderStyle.NotSpecified
                        );
            }
        }
        /// <summary>
        /// Gets or sets the border color specification.
        /// </summary>
        /// <value>
        /// A string containing the color value.
        /// </value>
        public string? Color
        {
            get => _color;
            set => _color = value;
        }
        /// <summary>
        /// Gets or sets the side of the border being represented.
        /// </summary>
        /// <value>
        /// A <see cref="ElementSide"/> enumerated value indicating the individual side,
        /// or all sides.
        /// </value>
        public ElementSide? Side
        {
            get => _side;
            set => _side = value;
        }
        /// <summary>
        /// Gets or sets the border style.
        /// </summary>
        /// <value>
        /// A <see cref="BorderStyle"/> enumerated value.
        /// </value>
        public BorderStyle? Style
        {
            get => _style;
            set => _style = value;
        }
        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> instance describing the width.
        /// </value>
        public FloatWithUnit? Width
        {
            get => _width;
            set => _width = value;
        }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Resets the content of the property and sets it to an "un-set" state.
        /// </summary>
        public void Clear()
        {
            _width?.Dispose();

            _width = null;
            _color = null;
            _side = ElementSide.NotSpecifed;
            _style = BorderStyle.NotSpecified;
        }
        /// <summary>
        /// Parses the CSS content for the instance.
        /// </summary>
        /// <param name="cssDefinition">A string containing the raw CSS definition / code defining the item.</param>
        public void ParseCss(string cssDefinition)
        {
            if (!string.IsNullOrEmpty(cssDefinition))
            {
                PropertyContent propertyDef = CssParsing.ParseProperty(cssDefinition);
                if (propertyDef != null && !propertyDef.IsEmpty)
                {
                    SetPropertyName(propertyDef.Name);
                    SetValues(propertyDef.Value);
                }
                else
                {
                    if (!string.IsNullOrEmpty(cssDefinition))
                        SetValues(cssDefinition);
				}
            }
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
            if (CanRender)
            {
                RenderName(builder);
                RenderValues(builder);
            }
			builder.Append(CssLiterals.CssTerminator);
			return builder.ToString();
            
        }
        #endregion

        #region Private Methods / Functions        
        /// <summary>
        /// Renders the appropriate property name.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="StringBuilder"/> instance to append the text to.
        /// </param>
        private void RenderName(StringBuilder builder)
        {
            switch (_side)
            {
                case ElementSide.AllSides:
                    builder.Append(CssPropertyNames.Border + CssLiterals.CssSeparator);
                    break;

                case ElementSide.Top:
                    builder.Append(CssPropertyNames.BorderTop + CssLiterals.CssSeparator);
                    break;

                case ElementSide.Left:
                    builder.Append(CssPropertyNames.BorderLeft + CssLiterals.CssSeparator);
                    break;

                case ElementSide.Right:
                    builder.Append(CssPropertyNames.BorderRight + CssLiterals.CssSeparator);
                    break;

                case ElementSide.Bottom:
                    builder.Append(CssPropertyNames.BorderBottom + CssLiterals.CssSeparator);
                    break;
            }
        }
        /// <summary>
        /// Renders the appropriate property value.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="StringBuilder"/> instance to append the text to.
        /// </param>
        private void RenderValues(StringBuilder builder)
        {
            int vcount = 0;

            // Render the width, if specified.
            if (_width != null)
            {
                string widthValue = _width.ToString();
                if (!string.IsNullOrEmpty(widthValue))
                {
                    builder.Append(_width.ToString());
                    vcount++;
                }
            }

            // If more than one value is specified, ensure spaces between the items.
            if (vcount > 0)
                builder.Append(' ');

            // Append the style - (required in CSS if the property is specified at all).
            builder.Append(BorderStyleConverter.ToText(_style));
            vcount++;

            // If more than one value is specified, ensure spaces between the items.
            if (vcount > 0)
                builder.Append(' ');

            // Append the color if specified.
            if (_color != null)
                builder.Append(_color);
        }
        /// <summary>
        /// Sets the state of the instance based on the provided property name.
        /// </summary>
        /// <param name="propName">
        /// A string containing the name of the CSS property being set.
        /// </param>
        private void SetPropertyName(string? propName)
        {
            switch (propName.ToLower().Trim())
            {
                case CssPropertyNames.Border:
                    _side = ElementSide.AllSides;
                    break;

                case CssPropertyNames.BorderTop:
                    _side = ElementSide.Top;
                    break;

                case CssPropertyNames.BorderLeft:
                    _side = ElementSide.Left;
                    break;

                case CssPropertyNames.BorderRight:
                    _side = ElementSide.Right;
                    break;

                case CssPropertyNames.BorderBottom:
                    _side = ElementSide.Bottom;
                    break;
            }

        }
        /// <summary>
        /// Sets the property value(s) from the provided string.
        /// </summary>
        /// <param name="propValues">
        /// A string containing one or more CSS property values to set.
        /// </param>
        private void SetValues(string? propValues)
        {
            List<string> subItems = CssParsing.ParsePropertyValue(propValues);
            foreach(string subItem in subItems)
            {
                // If this is a unit or number + unit value, like "23px"...
                if (CssParsing.IsUnit(subItem))
                {
                    _width?.Dispose();
                    _width = FloatWithUnit.Parse(subItem);
                }
                // otherwise, if this is a border style spec...
                else if (CssParsing.IsBorderStyle(subItem))
                {
                    _style = BorderStyleConverter.FromText(subItem);
                }
                // otherwise, assume it is a color.
                else
                {
                    _color = subItem;
                } 
            }
            subItems.Clear();

        }
        #endregion
    }
}
   