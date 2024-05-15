using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Converts the <see cref="BorderStyle"/> enumerated values to and from strings.
    /// </summary>
    /// <seealso cref="IValueConverter{FromType, ToType}" />
    public sealed class BorderStyleConverter : IValueConverter<BorderStyle, string>
    {
        /// <summary>
        /// Converts the original <see cref="BorderStyle"/> enumerated value to the string value used in CSS/HTML.
        /// </summary>
        /// <param name="originalValue">
        /// The <see cref="BorderStyle"/> enumerated value to be converted.
        /// </param>
        /// <returns>
        /// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
        /// <see cref="BorderStyle.NotSpecified"/> is specified.
        /// </returns>
        public string Convert(BorderStyle originalValue)
        {
            string value = string.Empty;

            switch (originalValue)
            {
                case BorderStyle.Dashed:
                    value = CssBorderStyleNames.StyleDashed;
                    break;

                case BorderStyle.Dotted:
                    value = CssBorderStyleNames.StyleDotted;
                    break;

                case BorderStyle.Double:
                    value = CssBorderStyleNames.StyleDouble;
                    break;

                case BorderStyle.Groove:
                    value = CssBorderStyleNames.StyleGroove;
                    break;

                case BorderStyle.Hidden:
                    value = CssBorderStyleNames.StyleHidden;
                    break;

                case BorderStyle.Inset:
                    value = CssBorderStyleNames.StyleInset;
                    break;

                case BorderStyle.None:
                    value = CssBorderStyleNames.StyleNone;
                    break;

                case BorderStyle.Outset:
                    value = CssBorderStyleNames.StyleOutset;
                    break;

                case BorderStyle.Ridge:
                    value = CssBorderStyleNames.StyleRidge;
                    break;

                case BorderStyle.Solid:
                    value = CssBorderStyleNames.StyleSolid;
                    break;
            }

            return value;
        }
        /// <summary>
        /// Converts the converted value to the original representation.
        /// </summary>
        /// <param name="convertedValue">
        /// A string containing the value to be converted.</param>
        /// <returns>
        /// The matching <see cref="BorderStyle"/> enumerated value.
        /// </returns>
        /// <remarks>
        /// The implementation of this method must be the inverse of the <see cref="Convert" /> method.
        /// </remarks>
        public BorderStyle ConvertBack(string convertedValue)
        {
            BorderStyle style = BorderStyle.NotSpecified;

            if (!string.IsNullOrEmpty(convertedValue))
            { 
                switch (convertedValue.ToLower().Trim())
                {
                    case CssBorderStyleNames.StyleDashed:
                        style = BorderStyle.Dashed;
                        break;

                    case CssBorderStyleNames.StyleDotted:
                        style = BorderStyle.Dotted;
                        break;

                    case CssBorderStyleNames.StyleDouble:
                        style = BorderStyle.Double;
                        break;

                    case CssBorderStyleNames.StyleGroove:
                        style = BorderStyle.Groove;
                        break;

                    case CssBorderStyleNames.StyleHidden:
                        style = BorderStyle.Hidden;
                        break;

                    case CssBorderStyleNames.StyleInset:
                        style = BorderStyle.Inset;
                        break;

                    case CssBorderStyleNames.StyleNone:
                        style = BorderStyle.None;
                        break;

                    case CssBorderStyleNames.StyleOutset:
                        style = BorderStyle.Outset;
                        break;

                    case CssBorderStyleNames.StyleRidge:
                        style = BorderStyle.Ridge;
                        break;

                    case CssBorderStyleNames.StyleSolid:
                        style = BorderStyle.Solid;
                        break;

                    default:
                        style = BorderStyle.NotSpecified;
                        break;

                }
            }
            return style;
        }
        /// <summary>
        /// Provides the static implementation to converts the original <see cref="BorderStyle"/> enumerated value to the string value used in CSS/HTML.
        /// </summary>
        /// <param name="originalValue">
        /// The <see cref="BorderStyle"/> enumerated value to be converted.
        /// </param>
        /// <returns>
        /// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
        /// <see cref="BorderStyle.NotSpecified"/> is specified.
        /// </returns>
        public static BorderStyle? FromText(string originalValue)
        {
            BorderStyle? value = null;

            if (originalValue != null)
            {
                BorderStyleConverter converter = new BorderStyleConverter();
                value = converter.ConvertBack(originalValue);
            }

            return value;
        }
        /// <summary>
        /// Provides the static implementation to converts the original <see cref="BorderStyle"/> enumerated value to the string value used in CSS/HTML.
        /// </summary>
        /// <param name="originalValue">
        /// The <see cref="BorderStyle"/> enumerated value to be converted.
        /// </param>
        /// <returns>
        /// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
        /// <see cref="BorderStyle.NotSpecified"/> is specified.
        /// </returns>
        public static string? ToText(BorderStyle? originalValue)
        {
            string? value = null;

            if (originalValue != null)
            {
                BorderStyleConverter converter = new BorderStyleConverter();
                value = converter.Convert(originalValue.Value);
            }

            return value;
        }
    }
}
