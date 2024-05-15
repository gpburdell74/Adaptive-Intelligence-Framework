using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Converts the <see cref="AlignContentOption"/> enumerated values to and from strings.
	/// </summary>
	/// <seealso cref="IValueConverter{FromType, ToType}" />
	public sealed class AlignContentOptionConverter : IValueConverter<AlignContentOption, string>
	{
		/// <summary>
		/// Converts the original <see cref="AlignContentOption"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="AlignContentOption"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="AlignContentOption.NotSpecified"/> is specified.
		/// </returns>
		public string Convert(AlignContentOption originalValue)
		{
			string value = string.Empty;

			switch (originalValue)
			{
				case AlignContentOption.Stretch:
					value = CssAlignContentOptionConstants.Stretch;
					break;

				case AlignContentOption.Center:
					value = CssAlignContentOptionConstants.Center;
					break;

				case AlignContentOption.FlexStart:
					value = CssAlignContentOptionConstants.FlexStart;
					break;

				case AlignContentOption.FlexEnd:
					value = CssAlignContentOptionConstants.FlexEnd;
					break;

				case AlignContentOption.SpaceBetween:
					value = CssAlignContentOptionConstants.SpaceBetween;
					break;

				case AlignContentOption.SpaceAround:
					value = CssAlignContentOptionConstants.SpaceAround;
					break;

				case AlignContentOption.SpaceEvenly:
					value = CssAlignContentOptionConstants.SpaceEvenly;
					break;

				case AlignContentOption.Inherit:
					value = CssAlignContentOptionConstants.Inherit;
					break;

				case AlignContentOption.Initial:
					value = CssAlignContentOptionConstants.Initial;
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
		/// The matching <see cref="AlignContentOption"/> enumerated value.
		/// </returns>
		/// <remarks>
		/// The implementation of this method must be the inverse of the <see cref="Convert" /> method.
		/// </remarks>
		public AlignContentOption ConvertBack(string convertedValue)
		{
			AlignContentOption style = AlignContentOption.NotSpecified;

			if (!string.IsNullOrEmpty(convertedValue))
			{
				switch (convertedValue.ToLower().Trim())
				{
					case CssAlignContentOptionConstants.Stretch:
						style = AlignContentOption.Stretch;
						break;

					case CssAlignContentOptionConstants.Center:
						style = AlignContentOption.Center;
						break;

					case CssAlignContentOptionConstants.FlexStart:
						style = AlignContentOption.FlexStart;
						break;

					case CssAlignContentOptionConstants.FlexEnd:
						style = AlignContentOption.FlexEnd;
						break;

					case CssAlignContentOptionConstants.SpaceBetween:
						style = AlignContentOption.SpaceBetween;
						break;

					case CssAlignContentOptionConstants.SpaceAround:
						style = AlignContentOption.SpaceAround;
						break;

					case CssAlignContentOptionConstants.SpaceEvenly:
						style = AlignContentOption.SpaceEvenly;
						break;

					case CssAlignContentOptionConstants.Inherit:
						style = AlignContentOption.Inherit;
						break;

					case CssAlignContentOptionConstants.Initial:
						style = AlignContentOption.Initial;
						break;

					default:
						style = AlignContentOption.NotSpecified;
						break;

				}
			}
			return style;
		}
		/// <summary>
		/// Provides the static implementation to converts the original <see cref="AlignContentOption"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="AlignContentOption"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="AlignContentOption.NotSpecified"/> is specified.
		/// </returns>
		public static AlignContentOption? FromText(string originalValue)
		{
			AlignContentOption? value = null;

			if (originalValue != null)
			{
				AlignContentOptionConverter converter = new AlignContentOptionConverter();
				value = converter.ConvertBack(originalValue);
			}

			return value;
		}
		/// <summary>
		/// Provides the static implementation to converts the original <see cref="AlignContentOption"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="AlignContentOption"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="AlignContentOption.NotSpecified"/> is specified.
		/// </returns>
		public static string? ToText(AlignContentOption? originalValue)
		{
			string? value = null;

			if (originalValue != null)
			{
				AlignContentOptionConverter converter = new AlignContentOptionConverter();
				value = converter.Convert(originalValue.Value);
			}

			return value;
		}
	}
}
