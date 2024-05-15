using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
    /// <summary>
    /// Converts the <see cref="CssUnit"/> enumerated values to strings.
    /// </summary>
    /// <seealso cref="IOneWayValueConverter{FromType, ToType}" />
    public sealed class CssUnitConverter : IValueConverter<CssUnit, string>
    {
        /// <summary>
        /// Converts the original <see cref="CssUnit"/> enumerated value to the string value used in CSS/HTML.
        /// </summary>
        /// <param name="originalValue">
        /// The <see cref="CssUnit"/> enumerated value to be converted.
        /// </param>
        /// <returns>
        /// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
        /// <see cref="CssUnits.NotSpecified"/> is specified.
        /// </returns>
        public string Convert(CssUnit originalValue)
        {
            string value = string.Empty;

            switch (originalValue)
            {
                case CssUnit.Centimeters:
                    value = CssUnitConstants.UnitCm;
                    break;

                case CssUnit.Ch:
                    value = CssUnitConstants.UnitCh;
                    break;

                case CssUnit.Em:
                    value = CssUnitConstants.UnitEm;
                    break;

                case CssUnit.Ex:
                    value = CssUnitConstants.UnitEx;
                    break;

                case CssUnit.Inches:
                    value = CssUnitConstants.UnitIn;
                    break;

                case CssUnit.Millimeters:
                    value = CssUnitConstants.UnitMm;
                    break;

                case CssUnit.Percent:
                    value = CssUnitConstants.UnitPercent;
                    break;

                case CssUnit.Picas:
                    value = CssUnitConstants.UnitPi;
                    break;

                case CssUnit.Pixels:
                    value = CssUnitConstants.UnitPx;
                    break;

                case CssUnit.Points:
                    value = CssUnitConstants.UnitPt;
                    break;

                case CssUnit.Rem:
                    value = CssUnitConstants.UnitRem;
                    break;

                case CssUnit.ViewportMin:
                    value = CssUnitConstants.UnitVMin;
                    break;

                case CssUnit.ViewportMax:
                    value = CssUnitConstants.UnitVMax;
                    break;

                case CssUnit.ViewpoirtWidth:
                    value = CssUnitConstants.UnitVw;
                    break;

                case CssUnit.ViewportHeight:
                    value = CssUnitConstants.UnitVh;
                    break;
            }

            return value;
        }
        /// <summary>
        /// Converts the converted value to the original representation.
        /// </summary>
        /// <param name="convertedValue">The original value to be converted.</param>
        /// <returns>
        /// The <typeparamref name="FromType" /> value.
        /// </returns>
        /// <remarks>
        /// The implementation of this method must be the inverse of the <see cref="M:Adaptive.Intelligence.Shared.IValueConverter`2.Convert(`0)" /> method.
        /// </remarks>
        public CssUnit ConvertBack(string convertedValue)
        {
            CssUnit unit = CssUnit.NotSpecified;

            if (!string.IsNullOrEmpty(convertedValue))
            {
                switch (convertedValue.ToLower())
                {
                    case CssUnitConstants.UnitCh:
                        unit = CssUnit.Ch;
                        break;

                    case CssUnitConstants.UnitCm:
                        unit = CssUnit.Centimeters;
                        break;

                    case CssUnitConstants.UnitEm:
                        unit = CssUnit.Em;
                        break;

                    case CssUnitConstants.UnitEx:
                        unit = CssUnit.Ex;
                        break;

                    case CssUnitConstants.UnitIn:
                        unit = CssUnit.Inches;
                        break;

                    case CssUnitConstants.UnitMm:
                        unit = CssUnit.Millimeters;
                        break;

                    case CssUnitConstants.UnitPercent:
                        unit = CssUnit.Percent;
                        break;

                    case CssUnitConstants.UnitPi:
                        unit = CssUnit.Picas;
                        break;

                    case CssUnitConstants.UnitPt:
                        unit = CssUnit.Points;
                        break;

                    case CssUnitConstants.UnitPx:
                        unit = CssUnit.Pixels;
                        break;

                    case CssUnitConstants.UnitRem:
                        unit = CssUnit.Rem;
                        break;

                    case CssUnitConstants.UnitVh:
                        unit = CssUnit.ViewportHeight;
                        break;

                    case CssUnitConstants.UnitVMax:
                        unit = CssUnit.ViewportMax;
                        break;

                    case CssUnitConstants.UnitVMin:
                        unit = CssUnit.ViewportMin;
                        break;

                    case CssUnitConstants.UnitVw:
                        unit = CssUnit.ViewpoirtWidth;
                        break;
                }
            }
            return unit;
        }

        /// <summary>
        /// Provides the static implementation to converts the original <see cref="CssUnits"/> enumerated value to the string value used in CSS/HTML.
        /// </summary>
        /// <param name="originalValue">
        /// The <see cref="CssUnits"/> enumerated value to be converted.
        /// </param>
        /// <returns>
        /// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
        /// <see cref="CssUnits.NotSpecified"/> is specified.
        /// </returns>
        public static CssUnit? FromText(string originalValue)
        {
            CssUnit? value = null;

            if (originalValue != null)
            {
                CssUnitConverter converter = new CssUnitConverter();
                value = converter.ConvertBack(originalValue);
            }

            return value;
        }
        /// <summary>
        /// Provides the static implementation to converts the original <see cref="CssUnit"/> enumerated value to the string value used in CSS/HTML.
        /// </summary>
        /// <param name="originalValue">
        /// The <see cref="CssUnit"/> enumerated value to be converted.
        /// </param>
        /// <returns>
        /// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
        /// <see cref="CssUnit.NotSpecified"/> is specified.
        /// </returns>
        public static string? ToText(CssUnit? originalValue)
        {
            string? value = null;

            if (originalValue != null)
            {
                CssUnitConverter converter = new CssUnitConverter();
                value = converter.Convert(originalValue.Value);
            }

            return value;
        }
    }
}
