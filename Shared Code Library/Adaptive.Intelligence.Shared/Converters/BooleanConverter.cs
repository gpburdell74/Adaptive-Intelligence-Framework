namespace Adaptive.Intelligence.Shared
{
	/// <summary>
	/// Provides a converter class for converting boolean value to and from readable
	/// string values.
	/// </summary>
	/// <seealso cref="IValueConverter{Absentee, B}" />
	public sealed class BooleanConverter : IValueConverter<bool, string>
	{
		/// <summary>
		/// Converts the original boolean value to a formatted string value.
		/// </summary>
		/// <param name="originalValue">The original value to be converted.</param>
		/// <returns>
		/// A string for display ("Yes" or "No").
		/// </returns>
		public string Convert(bool originalValue)
		{
			if (originalValue)
				return Constants.TrueFormatted;
			else
				return Constants.FalseFormatted;
		}
		/// <summary>
		/// Converts the converted value to the original representation.
		/// </summary>
		/// <param name="convertedValue">The original string value to be converted.</param>
		/// <returns>
		/// <b>true</b> or <b>false</b> based on the parsed string value.
		/// </returns>
		/// <remarks>
		/// The implementation of this method is the inverse of
		/// the <see cref="Convert(bool)" /> method.
		/// </remarks>
		public bool ConvertBack(string convertedValue)
		{
			bool returnValue = false;

			if (!string.IsNullOrEmpty(convertedValue))
			{
				convertedValue = convertedValue.ToLower();
				switch (convertedValue)
				{
					case Constants.TrueValueYes:
					case Constants.TrueValueSi:
					case Constants.TrueValueTrue:
					case Constants.TrueValueBT:
					case Constants.TrueValueBY:
					case Constants.TrueValueMinus1:
					case Constants.TrueValueOK:
						returnValue = true;
						break;
				}
			}
			return returnValue;
		}
	}
}
