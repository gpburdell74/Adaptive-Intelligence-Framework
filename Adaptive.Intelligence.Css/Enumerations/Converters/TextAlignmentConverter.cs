using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Converts the <see cref="TextAlignment"/> enumerated values to strings.
	/// </summary>
	/// <seealso cref="IOneWayValueConverter{FromType, ToType}" />
	public sealed class TextAlignmentConverter : IValueConverter<TextAlignment, string>
	{
		/// <summary>
		/// Converts the original <see cref="TextAlignment"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="TextAlignment"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="TextAlignments.NotSpecified"/> is specified.
		/// </returns>
		public string Convert(TextAlignment originalValue)
		{
			string value = string.Empty;

			switch (originalValue)
			{
				case TextAlignment.None:
					value = TextAlignmentConstants.None;
					break;

				case TextAlignment.Left:
					value = TextAlignmentConstants.Left;
					break;

				case TextAlignment.Center:
					value = TextAlignmentConstants.Center;
					break;

				case TextAlignment.Right:
					value = TextAlignmentConstants.Right;
					break;

				case TextAlignment.Justify:
					value = TextAlignmentConstants.Justify;
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
		public TextAlignment ConvertBack(string convertedValue)
		{
			TextAlignment unit = TextAlignment.NotSpecified;

			if (!string.IsNullOrEmpty(convertedValue))
			{
				switch (convertedValue.ToLower())
				{
					case TextAlignmentConstants.Justify:
						unit = TextAlignment.Justify;
						break;

					case TextAlignmentConstants.Left:
						unit = TextAlignment.Left;
						break;

					case TextAlignmentConstants.Center:
						unit = TextAlignment.Center;
						break;

					case TextAlignmentConstants.Right:
						unit = TextAlignment.Right;
						break;

					case TextAlignmentConstants.None:
						unit = TextAlignment.None;
						break;

					default:
						unit = TextAlignment.NotSpecified;
						break;
				}
			}
			return unit;
		}

		/// <summary>
		/// Provides the static implementation to converts the original <see cref="TextAlignments"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="TextAlignments"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="TextAlignments.NotSpecified"/> is specified.
		/// </returns>
		public static TextAlignment? FromText(string originalValue)
		{
			TextAlignment? value = null;

			if (originalValue != null)
			{
				TextAlignmentConverter converter = new TextAlignmentConverter();
				value = converter.ConvertBack(originalValue);
			}

			return value;
		}
		/// <summary>
		/// Provides the static implementation to converts the original <see cref="TextAlignment"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="TextAlignment"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="TextAlignment.NotSpecified"/> is specified.
		/// </returns>
		public static string? ToText(TextAlignment? originalValue)
		{
			string? value = null;

			if (originalValue != null)
			{
				TextAlignmentConverter converter = new TextAlignmentConverter();
				value = converter.Convert(originalValue.Value);
			}

			return value;
		}
	}
}
