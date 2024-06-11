using System.Text;

namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides static methods / functions for performing common, standardized data and string formatting operations.
	/// </summary>
	public static class AdaptiveFormat
	{
		#region Private Constants		
		/// <summary>
		/// The actual size of a kilo-byte (KB).
		/// </summary>
		private const long KBSize = 1024;
		/// <summary>
		/// The actual size of a mega-byte (MB).
		/// </summary>
		private const long MBSize = 1048576;
		/// <summary>
		/// The actual size of a giga-byte (GB).
		/// </summary>
		private const long GBSize = MBSize * KBSize;
		/// <summary>
		/// The giga byte string format string.
		/// </summary>
		private const string GigaByteStringFormat = "###,###,###,##0.0 GB";
		/// <summary>
		/// The mega byte string format string.
		/// </summary>
		private const string KiloByteStringFormat = "###,###,###,##0.0 KB";
		/// <summary>
		/// The kilo byte string format string.
		/// </summary>
		private const string MegaByteStringFormat = "###,###,###,##0.0 MB";
		/// <summary>
		/// The string suffix when the value is just in bytes.
		/// </summary>
		private const string BytesStingSuffix = " byte(s)";

		#endregion

		#region Public Static Methods / Functions
		/// <summary>
		/// Formats the address content.
		/// </summary>
		/// <param name="line1">A string containing the first address line.</param>
		/// <param name="line2">A string containing the second address line.</param>
		/// <returns>
		/// A string containing the two lines of the address separated by a carriage-return/linefeed pair.
		/// </returns>
		public static string FormatAddress(string? line1, string? line2)
		{
			StringBuilder builder = new StringBuilder();
			if (!string.IsNullOrEmpty(line1))
				builder.AppendLine(line1);

			if (!string.IsNullOrEmpty(line2))
				builder.AppendLine(line2);

			return builder.ToString();
		}
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
		/// Formats the size value as a value of bytes.
		/// </summary>
		/// <param name="numberOfBytes">
		/// The number of bytes.
		/// </param>
		/// <returns>
		/// A formatted string that auto-determines whether to return the value in GB, MB, KB, or bytes.
		/// </returns>
		public static string FormatByteString(long numberOfBytes)
		{
			string data = numberOfBytes.ToString();

			if (numberOfBytes >= GBSize)
			{
				data = ((float)numberOfBytes / GBSize).ToString(GigaByteStringFormat);
			}
			else if (numberOfBytes > MBSize)
			{
				data = ((float)numberOfBytes / MBSize).ToString(MegaByteStringFormat);
			}
			else if (numberOfBytes > KBSize)
			{
				data = ((float)numberOfBytes / KBSize).ToString(KiloByteStringFormat);
			}
			else
			{
				data += BytesStingSuffix;
			}
			return data;
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
			return FormatDate(dateValue) + Constants.Space + timeValue.ToString(true);
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
		#endregion
	}
}