using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Converts the <see cref="FontStretchOption"/> enumerated values to and from strings.
    /// </summary>
    /// <seealso cref="IValueConverter{FromType, ToType}" />
    public sealed class FontStretchOptionConverter : IValueConverter<FontStretchOption, string>
    {
        /// <summary>
        /// Converts the original <see cref="FontStretchOption"/> enumerated value to the string value used in CSS/HTML.
        /// </summary>
        /// <param name="originalValue">
        /// The <see cref="FontStretchOption"/> enumerated value to be converted.
        /// </param>
        /// <returns>
        /// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
        /// <see cref="FontStretchOption.NotSpecified"/> is specified.
        /// </returns>
        public string Convert(FontStretchOption originalValue)
        {
            string value = string.Empty;

            switch (originalValue)
            {
                case FontStretchOption.Normal:
                    value = CssFontStretchConstants.Normal;
                    break;

                case FontStretchOption.Condensed:
                    value = CssFontStretchConstants.Condensed;
                    break;

                case FontStretchOption.SemiCondensed:
                    value = CssFontStretchConstants.SemiCondensed;
                    break;

                case FontStretchOption.ExtraCondensed:
                    value = CssFontStretchConstants.ExtraCondensed;
                    break;

                case FontStretchOption.UltraCondensed:
                    value = CssFontStretchConstants.UltraCondensed;
                    break;

                case FontStretchOption.Expanded:
                    value = CssFontStretchConstants.Expanded;
                    break;

                case FontStretchOption.SemiExpanded:
                    value = CssFontStretchConstants.SemiExpanded;
                    break;

                case FontStretchOption.ExtraExpanded:
                    value = CssFontStretchConstants.ExtraExpanded;
                    break;

                case FontStretchOption.UltraExpanded:
                    value = CssFontStretchConstants.UltraExpanded;
                    break;

                case FontStretchOption.Inherit:
                    value = CssFontStretchConstants.Inherit;
                    break;

                case FontStretchOption.Initial:
                    value = CssFontStretchConstants.Initial;
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
        /// The matching <see cref="FontStretchOption"/> enumerated value.
        /// </returns>
        /// <remarks>
        /// The implementation of this method must be the inverse of the <see cref="Convert" /> method.
        /// </remarks>
        public FontStretchOption ConvertBack(string convertedValue)
        {
            FontStretchOption style = FontStretchOption.NotSpecified;

            if (!string.IsNullOrEmpty(convertedValue))
            {
                switch (convertedValue.ToLower().Trim())
                {
                    case CssFontStretchConstants.Normal:
                        style = FontStretchOption.Normal;
                        break;

                    case CssFontStretchConstants.Condensed:
                        style = FontStretchOption.Condensed;
                        break;

                    case CssFontStretchConstants.SemiCondensed:
                        style = FontStretchOption.SemiCondensed;
                        break;

                    case CssFontStretchConstants.ExtraCondensed:
                        style = FontStretchOption.ExtraCondensed;
                        break;

                    case CssFontStretchConstants.UltraCondensed:
                        style = FontStretchOption.UltraCondensed;
                        break;

                    case CssFontStretchConstants.Expanded:
                        style = FontStretchOption.Expanded;
                        break;

                    case CssFontStretchConstants.SemiExpanded:
                        style = FontStretchOption.SemiExpanded;
                        break;

                    case CssFontStretchConstants.ExtraExpanded:
                        style = FontStretchOption.ExtraExpanded;
                        break;

                    case CssFontStretchConstants.UltraExpanded:
                        style = FontStretchOption.UltraExpanded;
                        break;

                    case CssFontStretchConstants.Initial:
                        style = FontStretchOption.Initial;
                        break;

                    case CssFontStretchConstants.Inherit:
                        style = FontStretchOption.Inherit;
                        break;

                    default:
                        style = FontStretchOption.NotSpecified;
                        break;

                }
            }
            return style;
        }
        /// <summary>
        /// Provides the static implementation to converts the original <see cref="FontStretchOption"/> enumerated value to the string value used in CSS/HTML.
        /// </summary>
        /// <param name="originalValue">
        /// The <see cref="FontStretchOption"/> enumerated value to be converted.
        /// </param>
        /// <returns>
        /// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
        /// <see cref="FontStretchOption.NotSpecified"/> is specified.
        /// </returns>
        public static FontStretchOption? FromText(string originalValue)
        {
            FontStretchOption? value = null;

            if (originalValue != null)
            {
                FontStretchOptionConverter converter = new FontStretchOptionConverter();
                value = converter.ConvertBack(originalValue);
            }

            return value;
        }
        /// <summary>
        /// Provides the static implementation to converts the original <see cref="FontStretchOption"/> enumerated value to the string value used in CSS/HTML.
        /// </summary>
        /// <param name="originalValue">
        /// The <see cref="FontStretchOption"/> enumerated value to be converted.
        /// </param>
        /// <returns>
        /// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
        /// <see cref="FontStretchOption.NotSpecified"/> is specified.
        /// </returns>
        public static string? ToText(FontStretchOption? originalValue)
        {
            string? value = null;

            if (originalValue != null)
            {
                FontStretchOptionConverter converter = new FontStretchOptionConverter();
                value = converter.Convert(originalValue.Value);
            }

            return value;
        }
    }
}
