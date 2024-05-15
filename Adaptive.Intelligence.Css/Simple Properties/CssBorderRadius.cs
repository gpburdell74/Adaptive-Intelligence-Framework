using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Represents the border-radius setting for creating cornered-borders.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    /// <seealso cref="ICssProperty" />
    public sealed class CssBorderRadius : DisposableObjectBase, ICssProperty
    {
        #region Private Member Declarations        
        /// <summary>
        /// The setting for all borders.
        /// </summary>
        private FloatWithUnit? _all;
        /// <summary>
        /// The setting for the top border.
        /// </summary>
        private FloatWithUnit? _top;
        /// <summary>
        /// The setting for the left border.
        /// </summary>
        private FloatWithUnit? _left;
        /// <summary>
        /// The setting for the right border.
        /// </summary>
        private FloatWithUnit? _right;
        /// <summary>
        /// The setting for the bottom border.
        /// </summary>
        private FloatWithUnit? _bottom;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="CssBorderRadius"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public CssBorderRadius()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CssBorderRadius"/> class.
        /// </summary>
        /// <param name="allBorders">
        /// A string containing the definition for all borders.
        /// </param>
        public CssBorderRadius(string allBorders)
        {
            _all = FloatWithUnit.Parse(allBorders); 
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CssBorderRadius"/> class.
        /// </summary>
        /// <param name="value">
        /// The value setting for all borders.</param>
        /// <param name="unit">
        /// The <see cref="CssUnit"/> to use for all borders.
        /// </param>
        public CssBorderRadius(float value, CssUnit unit)
        {
            _all = new FloatWithUnit();
            _all.Value = value;
            _all.Unit = unit;
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
        /// Gets or sets reference to the value and unit specification for all borders.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> instance specifying the setting for all borders.
        /// </value>
        public FloatWithUnit? AllBorders
        {
            get => _all;
            set
            {
                _all = value;
                if (value != null)
                {
                    _left = null;
                    _right = null;
                    _top = null;
                    _bottom = null;
                }
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
                return ((_all != null) ||
                   (_left != null ||
                   _right != null ||
                   _top != null ||
                   _bottom != null));
            }
        }
        /// <summary>
        /// Gets or sets reference to the value and unit specification for the bottom border.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> instance specifying the setting for the bottom border.
        /// </value>
        public FloatWithUnit? Bottom
        {
            get => _bottom;
            set
            {
                _bottom = value;
                if (value != null)
                {
                    _all?.Dispose();
                    _all = null;
                }
            }
        }
        /// <summary>
        /// Gets or sets reference to the value and unit specification for the left border.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> instance specifying the setting for the left border.
        /// </value>
        public FloatWithUnit? Left
        {
            get => _left;
            set
            {
                _left = value;
                if (value != null)
                {
                    _all?.Dispose();
                    _all = null;
                }
            }
        }
        /// <summary>
        /// Gets or sets reference to the value and unit specification for the right border.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> instance specifying the setting for the right border.
        /// </value>
        public FloatWithUnit? Right
        {
            get => _right;
            set
            {
                _right = value;
                if (value != null)
                {
                    _all?.Dispose();
                    _all = null;
                }
            }
        }
        /// <summary>
        /// Gets or sets reference to the value and unit specification for the top border.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> instance specifying the setting for the top border.
        /// </value>
        public FloatWithUnit? Top
        {
            get => _top;
            set
            {
                _top = value;
                if (value != null)
                {
                    _all?.Dispose();
                    _all = null;
                }
            }
        }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Resets the content of the property and sets it to an "un-set" state.
        /// </summary>
        public void Clear()
        {
            _all?.Dispose();
            _left?.Dispose();
            _top?.Dispose();
            _right?.Dispose();
            _bottom?.Dispose();
         
            _all = null;
            _left = null;
            _top = null;
            _right = null;
            _bottom = null;
        }
        /// <summary>
        /// Parses the CSS content for the instance.
        /// </summary>
        /// <param name="cssDefinition">A string containing the raw CSS definition / code defining the item.</param>
        public void ParseCss(string cssDefinition)
        {
            Clear();
            int index = cssDefinition.IndexOf(":");
            if (index > -1)
            {
                string propName = cssDefinition.Substring(0, index).Trim();
                string propValue = cssDefinition.Substring(index + 1, cssDefinition.Length - (index + 1)).Trim();
                switch (propName)
                {
                    case CssPropertyNames.BorderRadius:
                        _all = FloatWithUnit.Parse(propValue);
                        break;

                    case CssPropertyNames.BorderRadiusBottom:
                        _bottom = FloatWithUnit.Parse(propValue);
                        break;

                    case CssPropertyNames.BorderRadiusLeft:
                        _left = FloatWithUnit.Parse(propValue);
                        break;

                    case CssPropertyNames.BorderRadiusRight:
                        _right = FloatWithUnit.Parse(propValue);
                        break;

                    case CssPropertyNames.BorderRadiusTop:
                        _top = FloatWithUnit.Parse(propValue);
                        break;
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
            StringBuilder css = new StringBuilder();

            if (_all != null)
            {
                css.Append(CssPropertyNames.BorderRadius + CssLiterals.CssSeparator + CssLiterals.Space +
                    _all.ToString());
            }
            else
            {
                if (_left != null)
                    css.Append(CssPropertyNames.BorderRadiusLeft + CssLiterals.CssSeparator + CssLiterals.Space +
                        _left.ToString());

                if (_right != null)
                    css.Append(CssPropertyNames.BorderRadiusRight + CssLiterals.CssSeparator + CssLiterals.Space +
                        _right.ToString());

                if (_bottom != null)
                    css.Append(CssPropertyNames.BorderRadiusBottom + CssLiterals.CssSeparator + CssLiterals.Space +
                        _bottom.ToString());

                if (_top != null)
                    css.Append(CssPropertyNames.BorderRadiusTop + CssLiterals.CssSeparator + CssLiterals.Space +
                        _top.ToString());
            }
            css.Append(CssLiterals.CssTerminator);
            return css.ToString();
        }
        #endregion
    }
}