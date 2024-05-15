
using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Represents and manages the CSS "font" property.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class CssFont : DisposableObjectBase, ICssProperty
    {
        #region Private Member Declarations		
        /// <summary>
        /// The font bold flag.
        /// </summary>
        private bool? _fontBold;
        /// <summary>
        /// The font italic flag.
        /// </summary>
        private bool? _fontItalic;
        /// <summary>
        /// The font name/ font family setting.
        /// </summary>
        private string? _fontFamily;
        /// <summary>
        /// The font feature settings.
        /// </summary>
        private string? _fontFeatureSettings;
        /// <summary>
        /// The font kerning value.
        /// </summary>
        private bool? _fontKerning;
        /// <summary>
        /// The font size specification.
        /// </summary>
        private FloatWithUnit? _fontSize;
        /// <summary>
        /// The font size adjustment value.
        /// </summary>
        private float? _fontSizeAdjust;
        /// <summary>
        /// The font skewed flag.
        /// </summary>
        private bool? _fontSkewed;
        /// <summary>
        /// The font stretch option.
        /// </summary>
        private FontStretchOption? _fontStretch;
        /// <summary>
        /// The font variant specification.
        /// </summary>
        private string? _fontVariant;
        /// <summary>
        /// The font variant caps specification.
        /// </summary>
        private string? _fontVariantCaps;
        /// <summary>
        /// The font weight expressed as a value.
        /// </summary>
        private string? _fontWeightValue;
        #endregion

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
        /// Gets or sets the font is bold flag.
        /// </summary>
        /// <remarks>
        /// Setting this value to a non-null boolean will set the <see cref="Weight"/> property to <b>null</b>.
        /// </remarks>
        /// <value>
        /// <b>true</b> if the font weight is set to "bold"; <b>false</b> if the font weight is set to "normal",
        /// and <b>null</b> if the value is not set at all.
        /// </value>
        public bool? Bold
        {
            get => _fontBold;
            set
            {
                _fontBold = value;
                if (value != null)
                    _fontWeightValue = null;
            }
        }
        /// <summary>
        /// Gets or sets the font name/ font family setting.
        /// </summary>
        /// <value>
        /// A string containing the font family name, or <b>null</b> if not set.
        /// </value>
        public string? Family
        {
            get => _fontFamily;
            set => _fontFamily = value;
        }
        /// <summary>
        /// Gets a value indicating whether any of the properties have been set.
        /// </summary>
        /// <remarks>
        /// This excludes checking the <see cref="HasSizeUnit"/> because that property has no meaning
        /// without <see cref="Size"/> being set.
        /// </remarks>
        /// <value>
        ///   <c>true</c> if any properties have been set; otherwise, <c>false</c>.
        /// </value>
        public bool CanRender => (
            _fontBold != null ||
            _fontItalic != null ||
            _fontFamily != null ||
            _fontFeatureSettings != null ||
            _fontKerning != null ||
            _fontSize != null ||
            _fontSizeAdjust != null ||
            _fontSkewed != null ||
            _fontVariant != null ||
            _fontStretch != null ||
            _fontVariantCaps != null ||
            _fontWeightValue != null);
        /// <summary>
        /// Gets or sets the font is italicized flag.
        /// </summary>
        /// <remarks>
        /// Setting this value to a non-null boolean will set the <see cref="Skewed"/> property to <b>null</b>.
        /// </remarks>
        /// <value>
        /// <b>true</b> if the font style is set to "italic"; <b>false</b> if the font style is set to "normal",
        /// and <b>null</b> if the value is not set at all.
        /// </value>
        public bool? Italic
        {
            get => _fontItalic;
            set
            {
                _fontItalic = value;
                if (value != null)
                    _fontSkewed = null;
            }
        }
        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>
        /// A <see cref="FloatWithUnit"/> specifying the size of the font with the measurement unit,
        /// or <b>null</b> if not set.
        /// </value>
        public FloatWithUnit? Size
        {
            get => _fontSize;
            set => _fontSize = value;
        }
        /// <summary>
        /// Gets or sets the font stretch option.
        /// </summary>
        /// <value>
        /// The <see cref="FontStretchOption"/> enumerated value to use, or <b>null</b> if not set.
        /// </value>
        public FontStretchOption? Stretch
        {
            get => _fontStretch;
            set => _fontStretch = value;
        }
        /// <summary>
        /// Gets or sets the font is skewed / oblique flag.
        /// </summary>
        /// <remarks>
        /// Setting this value to a non-null boolean will set the <see cref="Italic"/> property to <b>null</b>.
        /// </remarks>
        /// <value>
        /// <b>true</b> if the font style is set to "oblique"; <b>false</b> if the font style is set to "normal",
        /// and <b>null</b> if the value is not set at all.
        /// </value>
        public bool? Skewed
        {
            get => _fontSkewed;
            set
            {
                _fontSkewed = value;
                if (value != null)
                    _fontItalic = null;
            }
        }
		/// <summary>
		/// Gets or sets the font style setting.
		/// </summary>
		/// <value>
		/// A string containing the font-style definition, or <b>null</b> if not set.
		/// </value>
		public string? Style
        {
            get
            {
                if ((_fontItalic == null) && (_fontSkewed == null))
                        return CssLiterals.ValueNone;

                else
                {
                    if (_fontItalic != null && _fontItalic.Value)
                        return CssLiterals.ValueItalic;

                    else if (_fontSkewed != null && _fontSkewed.Value)
                        return CssLiterals.ValueOblique;
                    else
                        return null;
                }
            }
            set
            {
                if (value == null)
                {
                    _fontItalic = null;
                    _fontSkewed = null;
                }
                else if (value.ToLower() == CssLiterals.ValueItalic)
                {
                    _fontItalic = true;
                    _fontSkewed = false;
                }
                else if (value.ToLower() == CssLiterals.ValueOblique)
                {
					_fontItalic = false;
                    _fontSkewed = true;
				}
                else
                {
					_fontItalic = false;
					_fontSkewed = false;
				}
			}
        }
        /// <summary>
        /// Gets or sets the font variant setting.
        /// </summary>
        /// <value>
        /// A string containing the font variant setting, or <b>null</b> if not set.
        /// </value>
        public string? Variant
        {
            get => _fontVariant;
            set => _fontVariant = value;
        }
        /// <summary>
        /// Gets or sets the font variant Caps setting.
        /// </summary>
        /// <value>
        /// A string containing the font variant Caps setting, or <b>null</b> if not set.
        /// </value>
        public string? VariantCaps
        {
            get => _fontVariantCaps;
            set => _fontVariantCaps = value;
        }
        /// <summary>
        /// Gets or sets the font weight as a specific value.
        /// </summary>
        /// <remarks>
        /// Setting this value to a non-null boolean will set the <see cref="Bold"/> property to <b>null</b>.
        /// </remarks>
        /// <value>
        /// A string describing the font weight setting.
        /// </value>
        public string? Weight
        {
            get => _fontWeightValue;
            set
            {
                _fontWeightValue = value;
                if (value != null)
                    _fontBold = null;
            }
        }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Clears this instance and sets the property values to the "unset" state.
        /// </summary>
        public void Clear()
        {
            _fontFamily = null;
            _fontSize = null;
            _fontBold = null;
            _fontWeightValue = null;
            _fontVariant = null;
            _fontVariantCaps = null;
            _fontItalic = null;
            _fontSkewed = null;
        }
        /// <summary>
        /// Parses the CSS content for the instance.
        /// </summary>
        /// <param name="cssDefinition">A string containing the raw CSS definition / code defining the item.</param>
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
                        case CssPropertyNames.Font:
                            List<string> valueList = CssParsing.ParsePropertyValue(propValue);
                            SetValuesByType(valueList);
                            break;

                        case CssPropertyNames.FontFamily:
                            _fontFamily = propValue;
                            break;

                        case CssPropertyNames.FontKerning:
                            if (propValue == CssLiterals.ValueNone)
                                _fontKerning = false;
                            else if (propValue == CssLiterals.ValueNormal)
                                _fontKerning = true;
                            else
                                _fontKerning = null;
                            break;

                        case CssPropertyNames.FontSize:
                            _fontSize = FloatWithUnit.Parse(propValue);
                            break;

                        case CssPropertyNames.FontSizeAdjust:
                            _fontSizeAdjust = float.Parse(propValue);
                            break;

                        case CssPropertyNames.FontStyle:
                            switch (propValue)
                            {
                                case CssLiterals.ValueItalic:
                                    _fontItalic = true;
                                    _fontSkewed = false;
                                    break;

                                case CssLiterals.ValueNormal:
                                    _fontItalic = false;
                                    _fontSkewed = false;
                                    break;

                                case CssLiterals.ValueOblique:
                                    _fontItalic = false;
                                    _fontSkewed = true;
                                    break;
                            }
                            break;

                        case CssPropertyNames.FontVariant:
                            _fontVariant = propValue;
                            break;

                        case CssPropertyNames.FontVariantCaps:
                            _fontVariantCaps = propValue;
                            break;

                        case CssPropertyNames.FontWeight:
                            _fontWeightValue = propValue;
                            break;

                        case CssPropertyNames.FontStretch:
                            _fontStretch = FontStretchOptionConverter.FromText(propValue);
                            break;


                    }
                }
            }
        }
        /// <summary>
        /// Sets the font size specification.
        /// </summary>
        /// <param name="fontSize">
        /// A <see cref="float"/> containing the size value.
        /// </param>
        /// <param name="unit">
        /// A <see cref="CssUnits"/> enumerated value specifying the measurement unit.
        /// </param>
        public void SetSize(float fontSize, CssUnit unit)
        {
            _fontSize ??= new FloatWithUnit();

            _fontSize.Value = fontSize;
            _fontSize.Unit = unit;
        }
        /// <summary>
        /// Sets the font size specification.
        /// </summary>
        /// <param name="fontSize">
        /// A <see cref="float"/> containing the size value.
        /// </param>
        /// <param name="unit">
        /// A <see cref="CssUnits"/> enumerated value specifying the measurement unit.
        /// </param>
        public void SetSize(string? cssDefinition)
        {
            _fontSize?.Dispose();
            _fontSize = null;

            if (!string.IsNullOrEmpty(cssDefinition))
                _fontSize = FloatWithUnit.Parse(cssDefinition);
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

            // Font Family
            if (_fontFamily != null)
                builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontFamily) +
                    CssLiterals.CssOpenQuote + _fontFamily + CssLiterals.CssCloseQuote + CssLiterals.CssTerminator);

            // Font Weight
            if (_fontBold != null && _fontBold.Value)
                builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontWeight) + CssLiterals.ValueBold + CssLiterals.CssTerminator);

			else if (_fontBold != null && !_fontBold.Value)
                builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontWeight) + CssLiterals.ValueNormal + CssLiterals.CssTerminator);

			else if (_fontBold == null && !string.IsNullOrEmpty(_fontWeightValue))
                builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontWeight) + _fontWeightValue + CssLiterals.CssTerminator);

			// Font Style
			if (_fontItalic != null && _fontItalic.Value)
				builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontStyle) + CssLiterals.ValueItalic + CssLiterals.CssTerminator);

			else if (_fontItalic != null && !_fontItalic.Value)
				builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontStyle) + CssLiterals.ValueNormal + CssLiterals.CssTerminator);

			else if (_fontSkewed != null && _fontSkewed.Value)
				builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontStyle) + CssLiterals.ValueOblique + CssLiterals.CssTerminator);

			else if (_fontSkewed != null && !_fontSkewed.Value)
				builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontStyle) + CssLiterals.ValueNormal + CssLiterals.CssTerminator);

			// Font Size
			if (_fontSize != null)
                builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontSize) + _fontSize.ToString() + CssLiterals.CssTerminator);

			// Feature Settings
			if (!string.IsNullOrEmpty(_fontFeatureSettings))
                builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontFeatureSettings) + _fontFeatureSettings + CssLiterals.CssTerminator);

			// Kerning
			if (_fontKerning != null && _fontKerning.Value)
                builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontKerning) + CssLiterals.ValueNormal + CssLiterals.CssTerminator);

			// Size Adjustment
			if (_fontSizeAdjust != null)
				builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontSizeAdjust) + _fontSizeAdjust.Value.ToString() + CssLiterals.CssTerminator);

			// Font Stretch
			if (_fontStretch != null)
                builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontStretch) +
                    FontStretchOptionConverter.ToText(_fontStretch) + CssLiterals.CssTerminator);

			// Variant
			if (!string.IsNullOrEmpty(_fontVariant))
				builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontVariant) + _fontVariant + CssLiterals.CssTerminator);


			// Variant Caps
			if (!string.IsNullOrEmpty(_fontVariantCaps))
				builder.Append(CssPropertyNames.FormatPropertyName(CssPropertyNames.FontVariantCaps) + _fontVariantCaps + CssLiterals.CssTerminator);

			return builder.ToString();
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Sets the font style properties based on the provided text.
        /// </summary>
        /// <param name="fontStyle">
        /// A string specifying the CSS font style setting.
        /// </param>
        private void SetFontStyle(string? fontStyle)
        {
            if (string.IsNullOrEmpty(fontStyle))
            {
                _fontItalic = false;
                _fontSkewed = false;
            }
            else
            {
                switch (fontStyle)
                {
                    case CssLiterals.ValueItalic:
                        _fontItalic = true;
                        _fontSkewed = false;
                        break;

                    case CssLiterals.ValueNormal:
                        _fontItalic = false;
                        _fontSkewed = false;
                        break;

                    case CssLiterals.ValueOblique:
                        _fontItalic = false;
                        _fontSkewed = true;
                        break;
                }
            }
        }
        /// <summary>
        /// Sets the property values based on the property type/content of each.
        /// </summary>
        /// <param name="valueList">
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the property values.
        /// </param>
        private void SetValuesByType(List<string> valueList)
        {
            foreach (string propValue in valueList)
            {
                if (CssParsing.IsFontStyle(propValue))
                    SetFontStyle(propValue);

                else if (CssParsing.IsFontVariant(propValue))
                    _fontVariant = propValue;

                else if (CssParsing.IsFontWeight(propValue))
                    _fontWeightValue = propValue;

                else if (CssParsing.IsUnit(propValue))
                    _fontSize = FloatWithUnit.Parse(propValue);
                else
                    _fontFamily = propValue;
            }
        }
#endregion
    }
}