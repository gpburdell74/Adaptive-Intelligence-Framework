using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Converts the <see cref="LineHeightOptions"/> enumerated values to strings.
	/// </summary>
	/// <seealso cref="IOneWayValueConverter{FromType, ToType}" />
	public sealed class LineHeightOptionsConverter : IValueConverter<LineHeightOptions, string>
	{
		/// <summary>
		/// Converts the original <see cref="LineHeightOptions"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="LineHeightOptions"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="LineHeightOptionss.NotSpecified"/> is specified.
		/// </returns>
		public string Convert(LineHeightOptions originalValue)
		{
			string value = string.Empty;

			switch (originalValue)
			{
				case LineHeightOptions.Normal:
					value = CssLiterals.ValueNormal;
					break;

				case LineHeightOptions.Inherit:
					value = CssLiterals.ValueInherit;
					break;

				case LineHeightOptions.Initial:
					value = CssLiterals.ValueInitial;
					break;

				case LineHeightOptions.IsUnit:
				case LineHeightOptions.NotSpecified:
					value = string.Empty;
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
		public LineHeightOptions ConvertBack(string convertedValue)
		{
			LineHeightOptions lineHeight = LineHeightOptions.NotSpecified;

			if (!string.IsNullOrEmpty(convertedValue))
			{
				switch (convertedValue.ToLower())
				{
					case CssLiterals.ValueNormal:
						lineHeight = LineHeightOptions.Normal;
						break;

					case CssLiterals.ValueInherit:
						lineHeight = LineHeightOptions.Inherit;
						break;

					case CssLiterals.ValueInitial:
						lineHeight = LineHeightOptions.Initial;
						break;

					default:
						if (string.IsNullOrEmpty(convertedValue))
							lineHeight = LineHeightOptions.NotSpecified;
						else
							lineHeight = LineHeightOptions.IsUnit;
						break;
				}
			}
			return lineHeight;
		}

		/// <summary>
		/// Provides the static implementation to converts the original <see cref="LineHeightOptionss"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="LineHeightOptionss"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="LineHeightOptionss.NotSpecified"/> is specified.
		/// </returns>
		public static LineHeightOptions? FromText(string originalValue)
		{
			LineHeightOptions? value = null;

			if (originalValue != null)
			{
				LineHeightOptionsConverter converter = new LineHeightOptionsConverter();
				value = converter.ConvertBack(originalValue);
			}

			return value;
		}
		/// <summary>
		/// Provides the static implementation to converts the original <see cref="LineHeightOptions"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="LineHeightOptions"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="LineHeightOptions.NotSpecified"/> is specified.
		/// </returns>
		public static string? ToText(LineHeightOptions? originalValue)
		{
			string? value = null;

			if (originalValue != null)
			{
				LineHeightOptionsConverter converter = new LineHeightOptionsConverter();
				value = converter.Convert(originalValue.Value);
			}

			return value;
		}
	}
}
