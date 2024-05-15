using Adaptive.Intelligence.Shared.Logging;
using System.Globalization;

namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides extension methods for string values.
	/// </summary>
	public static class StringExtensions
	{
		#region Private Member Declarations
		/// <summary>
		/// The dollar sign as a string.
		/// </summary>
		private const string DollarSign = "$";
		/// <summary>
		/// The plural ending for singular words that end in Y.
		/// </summary>
		private const string EndingsY = "ies";
		/// <summary>
		/// The plural ending for some words.
		/// </summary>
		private const string EndingsES = "es";
		/// <summary>
		/// The plural ending for other words.
		/// </summary>
		private const string EndingsS = "s";
		/// <summary>
		/// The endings exception strings.
		/// </summary>
		private const string EndingsExceptionAles = "ales";
		/// <summary>
		/// The endings exception strings.
		/// </summary>
		private const string EndingsExceptionIles = "iles";
		/// <summary>
		/// The endings exception strings.
		/// </summary>
		private const string EndingsExceptionUles = "ules";
		/// <summary>
		/// The endings exception strings.
		/// </summary>
		private const string EndingsExceptionRoutes = "routes";
		/// <summary>
		/// The endings exception strings.
		/// </summary>
		private const string EndingsExceptionTypes = "types";
		/// <summary>
		/// The endings exception strings.
		/// </summary>
		private const string EndingsExceptionPages = "pages";
		/// <summary>
		/// The endings exception strings.
		/// </summary>
		private const string EndingsExceptionPlates = "plates";
		#endregion

		#region Public Static String Extension Methods / Functions
		/// <summary>
		/// Provides a string extension method for removing US dollar signs from
		/// the string instance.
		/// </summary>
		/// <param name="s">
		/// The string being extended.
		/// </param>
		/// <returns>
		/// The modified string value.
		/// </returns>
		public static string CleanUpDollarText(this string s)
		{
			return s.Replace(DollarSign, string.Empty);
		}
		/// <summary>
		/// Finds the first non-numeric character in the string.
		/// </summary>
		/// <param name="original">
		/// The string instance.
		/// </param>
		/// <param name="isFloatingPoint">
		/// A value indicating whether to allow for a floating-point number.
		/// </param>
		/// <returns>
		/// An integer indicating the first position of a non-numeric character in the string, or 
		/// -1 if there are no characters to examine or a non-numeric character could not be found.
		/// </returns>
		public static int FindFirstNonNumericCharacter(this string original, bool isFloatingPoint)
		{
			// Allow one period for floating point numbers.
			bool dotFound = !isFloatingPoint;
			int index = 0;
			int position = -1;
			int length = original.Length;

			while (index < length && position == -1)
			{
				char charToExamine = original[index];

				// If the character is not a digit...
				if (!char.IsDigit(charToExamine))
				{
					// If we have not yet encountered a period, treat the period as a number -
					// unless <i>isFloatingPoint</i> is <b>false</b>.
					if (charToExamine == '.' && !dotFound)
						dotFound = true;
					else
						position = index;
				}
				index++;
			};

			return position;
		}
		/// <summary>
		/// Modifies the plural word back to its singular form.
		/// </summary>
		/// <param name="originalValue">The original value to be modified.</param>
		/// <returns>
		/// The English singular form of the provided word.
		/// </returns>
		public static string Singularize(this string originalValue)
		{
			string returnValue = originalValue;

			if (!string.IsNullOrEmpty(originalValue))
			{
				// Create the comparison string.
				string comparisonValue = originalValue.ToLower().Trim();
				originalValue = originalValue.Trim();

				if (comparisonValue.EndsWith(EndingsY))
				{
					// Ends with "ies".
					returnValue = originalValue.Substring(0, comparisonValue.Length - 3) + "y";
				}
				else if (comparisonValue.EndsWith(EndingsES))
				{
					// Exclude the known exceptions.
					if (comparisonValue.EndsWith(EndingsExceptionTypes) ||
						comparisonValue.EndsWith(EndingsExceptionPages) ||
						comparisonValue.EndsWith(EndingsExceptionRoutes) ||
						comparisonValue.EndsWith(EndingsExceptionAles) ||
						comparisonValue.EndsWith(EndingsExceptionIles) ||
						comparisonValue.EndsWith(EndingsExceptionUles) ||
						comparisonValue.EndsWith(EndingsExceptionPlates))
						// In this case, just remove the "s".
						returnValue = originalValue.Substring(0, comparisonValue.Length - 1);
					else
						// Otherwise, remove the "es".
						returnValue = originalValue.Substring(0, comparisonValue.Length - 2);
				}
				else if (comparisonValue.EndsWith(EndingsS))
					// Standard word - remove the "s"
					returnValue = originalValue.Substring(0, comparisonValue.Length - 1);
			}
			return returnValue;
		}
		/// <summary>
		/// Modifies the plural word back to its singular form.
		/// </summary>
		/// <param name="originalValue">The original value to be modified.</param>
		/// <returns>
		/// The English singular form of the provided word.
		/// </returns>
		public static string Pluralize(this string originalValue)
		{
			string returnValue = originalValue;

			if (!string.IsNullOrEmpty(originalValue))
			{
				// Create the comparison string.
				string comparisonValue = originalValue.ToLower().Trim();
				originalValue = originalValue.Trim();

				if (comparisonValue.EndsWith("y"))
				{
					// Ends with "ies".
					returnValue = originalValue + EndingsY;
				}
				else if (comparisonValue.EndsWith("e"))
				{
					returnValue = originalValue + EndingsS;
				}
				else
					returnValue = originalValue + EndingsS;
			}
			return returnValue;
		}
		/// <summary>
		/// Capitalizes the First Letter of Each Word.
		/// </summary>
		/// <param name="s">
		/// The reference to the string to be modified.
		/// </param>
		/// <returns>
		/// The modified string with each work capitalized.
		/// </returns>
		public static string Properize(this string s)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
		}
		/// <summary>
		/// Surrounds a string with double quotes on each side.
		/// </summary>
		/// <param name="originalValue">The original value to be modified.</param>
		/// <returns>
		/// The original value with double-quote characters pre-pended and appended to the string.
		/// </returns>
		public static string SurroundWithQuotes(this string originalValue)
		{
			return $"\"{originalValue}\"";
		}
		/// <summary>
		/// Converts the current string to a <see cref="MemoryStream"/> instance.
		/// </summary>
		/// <param name="originalValue">The original value to be modified.</param>
		/// <returns>
		/// A <see cref="MemoryStream"/> containing the contents of the current string.
		/// </returns>
		public static MemoryStream ToStream(this string originalValue)
		{
			return GeneralUtils.ToStream(originalValue);
		}
		#endregion
	}
}
