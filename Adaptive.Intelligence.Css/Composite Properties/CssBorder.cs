using Adaptive.Intelligence.Shared;
using System.Security.Principal;
using System.Text;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Represents and manages the CSS "border" property.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class CssBorder : DisposableObjectBase, ICssProperty
    {
        #region Private Member Declarations				
        /// <summary>
        /// The specification for all borders.
        /// </summary>
        private CssBorderSide? _borderAll;
        /// <summary>
        /// The bottom border specification.
        /// </summary>
        private CssBorderSide? _borderBottom;
        /// <summary>
        /// The left border specification.
        /// </summary>
        private CssBorderSide? _borderLeft;
        /// <summary>
        /// The right border specification.
        /// </summary>
        private CssBorderSide? _borderRight;
        /// <summary>
        /// The top border specification.
        /// </summary>
        private CssBorderSide? _borderTop; 
        /// <summary>
        /// The border radius specification.
        /// </summary>
        private CssBorderRadius? _borderRadius;
        #endregion

        #region Constructor / Dispose Methods		
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
        /// Gets or sets the specification for all borders.
        /// </summary>
        /// <value>
        /// A string containing the CSS specification, or <b>null</b> if not set.
        /// </value>
        public CssBorderSide? All
        {
            get => _borderAll;
            set 
            {
                _borderAll = value;
                if (value != null)
                {
                    _borderLeft = null;
                    _borderTop = null;
                    _borderBottom = null;
                    _borderRight = null;
                }
            }
        }
        /// <summary>
        /// Gets or sets the specification for the bottom border.
        /// </summary>
        /// <remarks>
        /// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
        /// </remarks>
        /// <value>
        /// A string containing the CSS specification, or <b>null</b> if not set.
        /// </value>
        public CssBorderSide? Bottom
        {
            get => _borderBottom;
            set
            {
                _borderBottom = value;
                if (value != null)
                    _borderAll = null;
            }
        }
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
                return ((_borderAll != null && _borderAll.CanRender) ||
                    ((_borderLeft != null && _borderLeft.CanRender) ||
                    (_borderRight != null && _borderRight.CanRender) ||
                    (_borderBottom != null && _borderBottom.CanRender) ||
                    (_borderTop != null && _borderTop.CanRender)));
            }
        }
        /// <summary>
        /// Gets or sets the specification for the left border.
        /// </summary>
        /// <remarks>
        /// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
        /// </remarks>
        /// <value>
        /// A string containing the CSS specification, or <b>null</b> if not set.
        /// </value>
        public CssBorderSide? Left
        {
            get => _borderLeft;
            set
            {
                _borderLeft = value;
                if (value != null)
                    _borderAll = null;
            }
        }
        /// <summary>
        /// Gets or sets the reference to the border radius specification.
        /// </summary>
        /// <value>
        /// A <see cref="CssBorderRadius"/> specifying how much the borders are rounded.
        /// </value>
        public CssBorderRadius? Radius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = value;
            }
        }
        /// <summary>
        /// Gets or sets the specification for the right border.
        /// </summary>
        /// <remarks>
        /// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
        /// </remarks>
        /// <value>
        /// A string containing the CSS specification, or <b>null</b> if not set.
        /// </value>
        public CssBorderSide? Right
        {
            get => _borderRight;
            set
            {
                _borderRight = value;
                if (value != null)
                    _borderAll = null;
            }
        }
        /// <summary>
        /// Gets or sets the specification for the top border.
        /// </summary>
        /// <remarks>
        /// If this value is set, the <see cref="All"/> property is set to <b>null</b>.
        /// </remarks>
        /// <value>
        /// A string containing the CSS specification, or <b>null</b> if not set.
        /// </value>
        public CssBorderSide? Top
        {
            get => _borderTop;
            set
            {
                _borderTop = value;
                if (value != null)
                    _borderAll = null;
            }
        }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Clears this instance and resets it to the "unused" or "unset" state.
        /// </summary>
        public void Clear()
        {
            _borderTop?.Dispose();
            _borderLeft?.Dispose();
            _borderRight?.Dispose();
            _borderBottom?.Dispose();
            _borderAll?.Dispose();
            _borderRadius?.Dispose();

            _borderTop = null;
            _borderLeft = null;
            _borderRight = null;
            _borderBottom = null;
            _borderAll = null;
            _borderRadius = null;
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
                int index = cssDefinition.IndexOf(":");
                if (index > -1)
                {
                    string propName = cssDefinition.Substring(0, index).Trim();
                    string propValue = cssDefinition.Substring(index + 1, cssDefinition.Length - (index + 1)).Trim();
                    switch (propName)
                    {
                        case CssPropertyNames.Border:
                            _borderAll = new CssBorderSide(propValue);
                            break;

                        case CssPropertyNames.BorderTop:
                            _borderTop = new CssBorderSide(propValue);
                            break;

                        case CssPropertyNames.BorderBottom:
                            _borderBottom = new CssBorderSide(propValue);
                            break;

                        case CssPropertyNames.BorderLeft:
                            _borderRight = new CssBorderSide(propValue);
                            break;

                        case CssPropertyNames.BorderRight:
                            _borderLeft = new CssBorderSide(propValue);
                            break;

                        case CssPropertyNames.BorderRadius:
                            _borderRadius = new CssBorderRadius(propValue);
                            break;
                    }
                }
            }
        }
		/// <summary>
		/// Sets the value of the CSS "border-bottom" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetBottom(string? cssDefinition)
		{
			_borderBottom?.Dispose();
			_borderBottom = CssPropertyFactory.CreateBorderBottom(cssDefinition);
			if (_borderBottom != null)
			{
				// Remove the "all" setting if the a specific border side is set/specified.
				_borderAll?.Dispose();
				_borderAll = null;
			}
		}
		/// <summary>
		/// Sets the value of the CSS "border-left" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetLeft(string? cssDefinition)
        {
			_borderLeft?.Dispose();
			_borderLeft = CssPropertyFactory.CreateBorderLeft(cssDefinition);
            if (_borderLeft != null)
            { 
                // Remove the "all" setting if the a specific border side is set/specified.
                _borderAll?.Dispose();
                _borderAll = null;
            }
        }
		/// <summary>
		/// Sets the value of the CSS "border-right" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetRight(string? cssDefinition)
		{
			_borderRight?.Dispose();
			_borderRight = CssPropertyFactory.CreateBorderRight(cssDefinition);
			if (_borderRight != null)
			{
				// Remove the "all" setting if the a specific border side is set/specified.
				_borderAll?.Dispose();
				_borderAll = null;
			}
		}
		/// <summary>
		/// Sets the value of the CSS "border-left" property.
		/// </summary>
		/// <param name="cssDefinition">
		/// A string containing the CSS definition text, or <b>null</b> to "un-set" the property.
		/// </param>
		public void SetTop(string? cssDefinition)
		{
			_borderTop?.Dispose();
			_borderTop = CssPropertyFactory.CreateBorderTop(cssDefinition);
			if (_borderTop != null)
			{
				// Remove the "all" setting if the a specific border side is set/specified.
				_borderAll?.Dispose();
				_borderAll = null;
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
            StringBuilder css = new StringBuilder();

            if (_borderAll != null)
            {
                css.Append(_borderAll.ToCss());
            }
            else
            {
                if (_borderLeft != null && _borderLeft.CanRender)
                    css.Append(_borderLeft.ToCss());

                if (_borderRight != null && _borderRight.CanRender)
                    css.Append(_borderRight.ToCss());

                if (_borderTop != null && _borderTop.CanRender)
                    css.Append(_borderTop.ToCss());

                if (_borderBottom != null && _borderBottom.CanRender)
                    css.Append(_borderBottom.ToCss());

            }
            if (_borderRadius != null && _borderRadius.CanRender)
            {
                css.Append(_borderRadius.ToCss());
            }

            return css.ToString();
        }
        #endregion
    }
}

