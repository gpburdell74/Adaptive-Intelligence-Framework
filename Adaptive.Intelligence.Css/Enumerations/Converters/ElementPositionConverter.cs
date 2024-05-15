using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Converts the <see cref="ElementPosition"/> enumerated values to strings.
	/// </summary>
	/// <seealso cref="IOneWayValueConverter{FromType, ToType}" />
	public sealed class ElementPositionConverter : IValueConverter<ElementPosition, string>
	{
		/// <summary>
		/// Converts the original <see cref="ElementPosition"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="ElementPosition"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="ElementPositions.NotSpecified"/> is specified.
		/// </returns>
		public string Convert(ElementPosition originalValue)
		{
			string value = string.Empty;

			switch (originalValue)
			{
				case ElementPosition.Absolute:
					value = ElementPositionConstants.Absolute;
					break;

				case ElementPosition.Relative:
					value = ElementPositionConstants.Relative;
					break;

				case ElementPosition.Static:
					value = ElementPositionConstants.Static;
					break;

				case ElementPosition.Fixed:
					value = ElementPositionConstants.Fixed;
					break;

				case ElementPosition.Sticky:
					value = ElementPositionConstants.Sticky;
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
		public ElementPosition ConvertBack(string convertedValue)
		{
			ElementPosition unit = ElementPosition.NotSpecified;

			if (!string.IsNullOrEmpty(convertedValue))
			{
				switch (convertedValue.ToLower())
				{
					case ElementPositionConstants.Absolute:
						unit = ElementPosition.Absolute;
						break;

					case ElementPositionConstants.Static:
						unit = ElementPosition.Static;
						break;

					case ElementPositionConstants.Fixed:
						unit = ElementPosition.Fixed;
						break;

					case ElementPositionConstants.Relative:
						unit = ElementPosition.Relative;
						break;

					case ElementPositionConstants.Sticky:
						unit = ElementPosition.Sticky;
						break;

					default:
						unit = ElementPosition.NotSpecified;
						break;
				}
			}
			return unit;
		}

		/// <summary>
		/// Provides the static implementation to converts the original <see cref="ElementPositions"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="ElementPositions"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="ElementPositions.NotSpecified"/> is specified.
		/// </returns>
		public static ElementPosition? FromText(string originalValue)
		{
			ElementPosition? value = null;

			if (originalValue != null)
			{
				ElementPositionConverter converter = new ElementPositionConverter();
				value = converter.ConvertBack(originalValue);
			}

			return value;
		}
		/// <summary>
		/// Provides the static implementation to converts the original <see cref="ElementPosition"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="ElementPosition"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="ElementPosition.NotSpecified"/> is specified.
		/// </returns>
		public static string? ToText(ElementPosition? originalValue)
		{
			string? value = null;

			if (originalValue != null)
			{
				ElementPositionConverter converter = new ElementPositionConverter();
				value = converter.Convert(originalValue.Value);
			}

			return value;
		}
	}
}
