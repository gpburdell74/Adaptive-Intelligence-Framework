using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.Css
{
	/// <summary>
	/// Converts the <see cref="VerticalAlignment"/> enumerated values to strings.
	/// </summary>
	/// <seealso cref="IOneWayValueConverter{FromType, ToType}" />
	public sealed class VerticalAlignmentConverter : IValueConverter<VerticalAlignment, string>
	{
		/// <summary>
		/// Converts the original <see cref="VerticalAlignment"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="VerticalAlignment"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="VerticalAlignments.NotSpecified"/> is specified.
		/// </returns>
		public string Convert(VerticalAlignment originalValue)
		{
			string value = string.Empty;

			switch (originalValue)
			{
				case VerticalAlignment.Baseline:
					value = VerticalAlignmentConstants.Baseline;
					break;

				case VerticalAlignment.Bottom:
					value = VerticalAlignmentConstants.Bottom;
					break;

				case VerticalAlignment.Inherit:
					value = VerticalAlignmentConstants.Inherit;
					break;

				case VerticalAlignment.Initial:
					value = VerticalAlignmentConstants.Initial;
					break;

				case VerticalAlignment.IsUnit:
					value = string.Empty;
					break;

				case VerticalAlignment.Middle:
					value = VerticalAlignmentConstants.Middle;
					break;

				case VerticalAlignment.Revert:
					value = VerticalAlignmentConstants.Revert;
					break;

				case VerticalAlignment.RevertLayer:
					value = VerticalAlignmentConstants.RevertLayer;
					break;

				case VerticalAlignment.Sub:
					value = VerticalAlignmentConstants.Sub;
					break;

				case VerticalAlignment.Super:
					value = VerticalAlignmentConstants.Super;
					break;

				case VerticalAlignment.TextBottom:
					value = VerticalAlignmentConstants.TextBottom;
					break;

				case VerticalAlignment.TextTop:
					value = VerticalAlignmentConstants.TextTop;
					break;

				case VerticalAlignment.Top:
					value = VerticalAlignmentConstants.Top;
					break;

				case VerticalAlignment.Unset:
					value = VerticalAlignmentConstants.Unset;
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
		public VerticalAlignment ConvertBack(string convertedValue)
		{
			VerticalAlignment unit = VerticalAlignment.NotSpecified;

			if (!string.IsNullOrEmpty(convertedValue))
			{
				switch (convertedValue.ToLower())
				{
					case VerticalAlignmentConstants.Baseline:
						unit = VerticalAlignment.Baseline;
						break;

					case VerticalAlignmentConstants.Bottom:
						unit = VerticalAlignment.Bottom;
						break;

					case VerticalAlignmentConstants.Inherit:
						unit = VerticalAlignment.Inherit;
						break;

					case VerticalAlignmentConstants.Initial:
						unit = VerticalAlignment.Initial;
						break;

					case VerticalAlignmentConstants.Middle:
						unit = VerticalAlignment.Middle;
						break;

					case VerticalAlignmentConstants.Revert:
						unit = VerticalAlignment.Revert;
						break;

					case VerticalAlignmentConstants.RevertLayer:
						unit = VerticalAlignment.RevertLayer;
						break;

					case VerticalAlignmentConstants.Sub:
						unit = VerticalAlignment.Sub;
						break;

					case VerticalAlignmentConstants.Super:
						unit = VerticalAlignment.Super;
						break;

					case VerticalAlignmentConstants.TextBottom:
						unit = VerticalAlignment.TextBottom;
						break;

					case VerticalAlignmentConstants.TextTop:
						unit = VerticalAlignment.TextTop;
						break;

					case VerticalAlignmentConstants.Top:
						unit = VerticalAlignment.Top;
						break;

					case VerticalAlignmentConstants.Unset:
						unit = VerticalAlignment.Unset;
						break;

					default:
						if (CssParsing.IsUnit(convertedValue))
							unit = VerticalAlignment.IsUnit;
						else
							unit = VerticalAlignment.NotSpecified;
						break;
				}
			}
			return unit;
		}

		/// <summary>
		/// Provides the static implementation to converts the original <see cref="VerticalAlignments"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="VerticalAlignments"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="VerticalAlignments.NotSpecified"/> is specified.
		/// </returns>
		public static VerticalAlignment? FromText(string originalValue)
		{
			VerticalAlignment? value = null;

			if (originalValue != null)
			{
				VerticalAlignmentConverter converter = new VerticalAlignmentConverter();
				value = converter.ConvertBack(originalValue);
			}

			return value;
		}
		/// <summary>
		/// Provides the static implementation to converts the original <see cref="VerticalAlignment"/> enumerated value to the string value used in CSS/HTML.
		/// </summary>
		/// <param name="originalValue">
		/// The <see cref="VerticalAlignment"/> enumerated value to be converted.
		/// </param>
		/// <returns>
		/// A string containing the text representation of the unit, or <see cref="string.Empty"/> if 
		/// <see cref="VerticalAlignment.NotSpecified"/> is specified.
		/// </returns>
		public static string? ToText(VerticalAlignment? originalValue)
		{
			string? value = null;

			if (originalValue != null)
			{
				VerticalAlignmentConverter converter = new VerticalAlignmentConverter();
				value = converter.Convert(originalValue.Value);
			}

			return value;
		}
	}
}
