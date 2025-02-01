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
            // Directly return true if the convertedValue matches any of the truthy constants.
            return (string.Equals(convertedValue, Constants.TrueValueYes, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(convertedValue, Constants.TrueValueSi, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(convertedValue, Constants.TrueValueTrue, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(convertedValue, Constants.TrueValueBT, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(convertedValue, Constants.TrueValueBY, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(convertedValue, Constants.TrueValueMinus1, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(convertedValue, Constants.TrueValueOK, StringComparison.OrdinalIgnoreCase));
        }
    }
}
