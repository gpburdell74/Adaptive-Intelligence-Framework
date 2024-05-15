using Adaptive.Intelligence.Shared;
using System.Text;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Provides static methods / functions for helping parse CSS content.
    /// </summary>
    public static class CssParsing
    {
        #region Private Static Members        
        /// <summary>
        /// The units list.
        /// </summary>
        private readonly static List<string> _unitsList;
        /// <summary>
        /// The border style names list.
        /// </summary>
        private readonly static List<string> _borderStyleList;
        /// <summary>
        /// The font style names list.
        /// </summary>
        private readonly static List<string> _fontStyleList;
        /// <summary>
        /// The font weight list.
        /// </summary>
        private readonly static List<string> _fontWeightList;
        /// <summary>
        /// The font variant list.
        /// </summary>
        private readonly static List<string> _fontVariantList;
        #endregion

        #region Static Constructor
        /// <summary>
        /// Statically initializes the <see cref="CssParsing"/> class.
        /// </summary>
        static CssParsing()
        {
            _unitsList = new List<string>
            {
                CssUnitConstants.UnitCh,
                CssUnitConstants.UnitCm,
                CssUnitConstants.UnitEm,
                CssUnitConstants.UnitEx,
                CssUnitConstants.UnitIn,
                CssUnitConstants.UnitMm,
                CssUnitConstants.UnitPercent,
                CssUnitConstants.UnitPi,
                CssUnitConstants.UnitPt,
                CssUnitConstants.UnitPx,
                CssUnitConstants.UnitRem,
                CssUnitConstants.UnitVh,
                CssUnitConstants.UnitVMax,
                CssUnitConstants.UnitVMin,
                CssUnitConstants.UnitVw
            };

            _borderStyleList = new List<string>()
            {
                CssBorderStyleNames.StyleDotted,
                CssBorderStyleNames.StyleDashed,
                CssBorderStyleNames.StyleSolid,
                CssBorderStyleNames.StyleDouble,
                CssBorderStyleNames.StyleGroove,
                CssBorderStyleNames.StyleRidge,
                CssBorderStyleNames.StyleInset,
                CssBorderStyleNames.StyleOutset,
                CssBorderStyleNames.StyleNone,
                CssBorderStyleNames.StyleHidden
            };

            _fontStyleList = new List<string>()
            {
                CssLiterals.ValueNormal,
                CssLiterals.ValueItalic,
                CssLiterals.ValueOblique
            };

            _fontWeightList = new List<string>()
            {
                CssLiterals.ValueNormal,
                CssLiterals.ValueBold,
                CssLiterals.ValueBolder,
                CssLiterals.ValueLighter,
                CssLiterals.ValueInitial,
                CssLiterals.ValueInherit,
                CssLiterals.Value100,
                CssLiterals.Value200,
                CssLiterals.Value300,
                CssLiterals.Value400,
                CssLiterals.Value500,
                CssLiterals.Value600,
                CssLiterals.Value700,
                CssLiterals.Value800,
                CssLiterals.Value900
            };

            _fontVariantList = new List<string>()
            {
                CssLiterals.ValueNormal,
                CssLiterals.ValueSmallCaps,
                CssLiterals.ValueInitial,
                CssLiterals.ValueInherit
            };

        }
        #endregion

        #region Public Methods / Functions        
        /// <summary>
        /// Determines whether the specified style name is an actual CSS border style name.
        /// </summary>
        /// <param name="styleName">
        /// A string containing the name of the style to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified value is a valid CSS border style; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBorderStyle(string styleName)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(styleName))
                isValid  = _borderStyleList.Contains(styleName);

            return isValid;
        }
        /// <summary>
        /// Determines whether the specified style name is an actual CSS font style name.
        /// </summary>
        /// <param name="styleName">
        /// A string containing the name of the style to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified value is a valid CSS font style; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFontStyle(string styleName)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(styleName))
                isValid = _fontStyleList.Contains(styleName);

            return isValid;
        }
        /// <summary>
        /// Determines whether the specified style name is an actual CSS font style name.
        /// </summary>
        /// <param name="styleName">
        /// A string containing the name of the style to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified value is a valid CSS font style; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFontVariant(string styleName)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(styleName))
                isValid = _fontVariantList.Contains(styleName);

            return isValid;
        }
        /// <summary>
        /// Determines whether the specified style name is an actual CSS font weight name or value.
        /// </summary>
        /// <param name="styleName">
        /// A string containing the text of the weight specification to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified value is a valid CSS font weight value; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFontWeight(string styleName)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(styleName))
                isValid = _fontWeightList.Contains(styleName);

            return isValid;
        }
        /// <summary>
        /// Determines whether the specified text represents a unit and/or unit and value specification.
        /// </summary>
        /// <remarks>
        /// This is used to recognize text like "px" or "12pt" or "1.2em".
        /// </remarks>
        /// <param name="text">
        /// A string containing the text to be examined.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified text represents a value and unit or just a unit specificatio9n; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUnit(string text)
        {
            bool isUnit = false;

            int index = text.FindFirstNonNumericCharacter(true);
            if (index > -1)
            {
                // Ensure the text matches a known unit, and that it comes at the end of the item.
                string subText = text.Substring(index, text.Length - index);
                isUnit = _unitsList.Contains(subText) && text.EndsWith(subText);
            }
            return isUnit;
        }
        /// <summary>
        /// Parses the CSS property definition text into its name and value parts.
        /// </summary>
        /// <param name="cssPropertyDefinition">
        /// A string containing the CSS property definition.
        /// </param>
        /// <returns>
        /// A <see cref="PropertyContent"/> instance containing the results.
        /// </returns>
        public static PropertyContent ParseProperty(string cssPropertyDefinition)
        {
            PropertyContent propDef = new PropertyContent();

            // Find the seperator character.
            int index = cssPropertyDefinition.IndexOf(CssLiterals.CssSeparator);
            if (index > -1)
            {
                // The name is on the left side.
                propDef.Name = cssPropertyDefinition.Substring(0, index).Trim();
                propDef.Value = string.Empty;

                index++;
                if (index < cssPropertyDefinition.Length)
                {
                    propDef.Value = cssPropertyDefinition.Substring(index, cssPropertyDefinition.Length - (index)).Trim();
                }
            }

            return propDef;
        }
        /// <summary>
        /// Parses the property value into its component parts.
        /// </summary>
        /// <param name="propValues">
        /// A string containing the property value(s) definitions.
        /// </param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="string"/> values to be returned as multiple property values.
        /// </returns>
        public static List<string> ParsePropertyValue(string propValues)
        {
            List<string> itemList = new List<string>();

            if (!string.IsNullOrEmpty(propValues))
            {
                char[] letters = propValues.ToCharArray();
                StringBuilder builder = new StringBuilder();
                int length = propValues.Length;
                int pos = 0;
                bool inParens = false;
                do
                {
                    char charToExamine = letters[pos];

                    // Space as seperator...
                    if (charToExamine == ' ')
                    {
                        string lastValue = builder.ToString();
                        itemList.Add(lastValue);
                        builder.Clear();
                    }
                    // In case quotes are used...
                    else if (charToExamine == '\'')
                    {
                        inParens = !inParens;
                        if (!inParens)
                        {
                            string lastValue = builder.ToString();
                            itemList.Add(lastValue);
                            builder.Clear();
                        }
                    }
                    else
                    {
                        builder.Append(charToExamine);
                    }
                    pos++;
                } while (pos < length);

                if (builder.Length > 0)
                    itemList.Add(builder.ToString());
            }
            return itemList;
        }
        #endregion
    }
}
