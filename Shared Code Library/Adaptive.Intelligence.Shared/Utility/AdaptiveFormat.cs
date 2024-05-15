using System.Text;

namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides static methods / functions for performing common, standardized data and string formatting operations.
	/// </summary>
	public static class AdaptiveFormat
	{
		/// <summary>
		/// Formats the boolean value as a string.
		/// </summary>
		/// <param name="value">
		/// A boolean  value to be converted to a string.
		/// </param>
		/// <returns>
		/// "Yes" if <i>value</i> is <b>true</b>;
		/// "No" if <i>value</i> is <b>false</b>;
		/// </returns>
		public static string FormatBoolean(bool value)
		{
			if (value)
				return Constants.TrueFormatted;
			else
				return Constants.FalseFormatted;
		}
		/// <summary>
		/// Formats the date value as a string.
		/// </summary>
		/// <param name="original">
		/// The <see cref="DateTime"/> value to be formatted.
		/// </param>
		/// <returns>
		/// A string containing the formatted date value.
		/// </returns>
		public static string FormatDateValue(DateTime original)
		{
			if (original.IsRidiculousDate())
				return string.Empty;
			else
				return original.ToString(Constants.USDateFormat);
		}
		/// <summary>
		/// Formats the gender string into a standard display format.
		/// </summary>
		/// <param name="original">
		/// The original string value containing the content to convert.
		/// </param>
		/// <returns>
		/// The standard display values for the specified gender.
		/// </returns>
		public static string FormatGender(string original)
		{
			if (string.IsNullOrEmpty(original))
				return string.Empty;
			else if (original == Constants.GenderCodeMale)
				return Constants.GenderMale;
			else
				return Constants.GenderFemale;
		}
		/// <summary>
		/// Formats the address content.
		/// </summary>
		/// <param name="line1">A string containing the first address line.</param>
		/// <param name="line2">A string containing the second address line.</param>
		/// <returns>
		/// A string containing the two lines of the address separated by a carriage-return/linefeed pair.
		/// </returns>
		public static string FormatAddress(string line1, string line2)
		{
			StringBuilder builder = new StringBuilder();
			if (!string.IsNullOrEmpty(line1))
				builder.AppendLine(line1);

			if (!string.IsNullOrEmpty(line2))
				builder.AppendLine(line2);

			return builder.ToString();
		}
		/// <summary>
		/// Formats the date value as a string.
		/// </summary>
		/// <param name="dateValue">The <see cref="DateTime"/> value to be formatted.</param>
		/// <returns>
		/// A string containing the date in US date format.
		/// </returns>
		public static string FormatDate(DateTime dateValue)
		{
			return dateValue.ToString(Constants.USDateFormat);
		}
		/// <summary>
		/// Formats the date value as a string.
		/// </summary>
		/// <param name="dateValue">The <see cref="DateTime"/> value to be formatted.</param>
		/// <returns>
		/// A string containing the date and time in US date format.
		/// </returns>
		public static string FormatDateAndTime(DateTime dateValue)
		{
			return dateValue.ToString(Constants.USFullDateFormat);
		}
		/// <summary>
		/// Formats the date and time values as a single string.
		/// </summary>
		/// <remarks>
		/// When using this method, the time component of <i>dateValue</i> is ignored.
		/// </remarks>
		/// <param name="dateValue">The <see cref="DateTime"/> value to be formatted.</param>
		/// <param name="timeValue">The <see cref="Time"/> value to be formatted.</param>
		/// <returns>
		/// A string containing the date and time in US date/time format.
		/// </returns>
		public static string FormatDateAndTime(DateTime dateValue, Time timeValue)
		{
			return FormatDate(dateValue) + " " + timeValue.ToString(true);
		}
		/// <summary>
		/// Formats the mailing address.
		/// </summary>
		/// <param name="address">An <see cref="IStandardPostalAddress"/> implementation representing the address.
		/// </param>
		/// <returns>
		/// A string containing the formatted address.
		/// </returns>
		public static string? FormatMailingAddress(IStandardPostalAddress? address)
		{
			if (address == null)
				return null;
			else
				return address.ToString();
		}
		/// <summary>
		/// Formats the source string to lower case and removes whitespace characters.
		/// </summary>
		/// <param name="sourceString">The source string.</param>
		/// <returns>
		/// A string containing the formatted value.
		/// </returns>
		public static string LowerNormalizeString(string? sourceString)
		{
			string formatted = string.Empty;

			if (!string.IsNullOrEmpty(sourceString))
			{
				StringBuilder builder = new StringBuilder();
				for (int count = 0; count < sourceString.Length; count++)
				{
					char c = sourceString[count];
					if (c != Constants.SpaceChar && 
						c != Constants.SingleQuoteChar && 
						c != Constants.CommaChar &&
						c != Constants.DotChar)
					{
						builder.Append(char.ToLower(c));
					}
				}

				formatted = builder.ToString();
			}

			return formatted;
		}
		/// <summary>
		/// Determines whether the specified string value is numeric.
		/// </summary>
		/// <param name="original">The string value to be examined.</param>
		/// <returns>
		/// <b>true</b> if the specified string contains only numeric digits.
		/// </returns>
		public static bool IsNumeric(string original)
		{
			if (string.IsNullOrEmpty(original))
				return false;

			bool isNumeric;
			int length = original.Length;
			int count = 0;
			do
			{
				isNumeric = (char.IsNumber(original[count]));
				count++;
			} while ((isNumeric) && (count < length));

			return isNumeric;
		}
	}
}
