using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides a class for parsing date values in various forms.
    /// </summary>
    public sealed class DateConverter : IValueConverter<string, DateTime>
    {
        #region Private Member Declarations
        private const int MinYear = 1900;
        private const int MaxYear = 3000;

        /// <summary>
        /// The indexes of months with 31 days.
        /// </summary>
        private static readonly int[] MonthsWith31Days = { 1, 3, 5, 7, 8, 10, 12 };
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Converts the original value to another value.
        /// </summary>
        /// <param name="originalValue">The original value to be converted.</param>
        /// <returns>
        /// The <see cref="DateTime"/> value or <see cref="DateTime.MinValue"/> if the
        /// value cannot be converted.
        /// </returns>
        public DateTime Convert(string originalValue)
        {
            if (string.IsNullOrEmpty(originalValue))
            {
                return new DateTime(1900, 1, 1);
            }
            else
            {
                originalValue = originalValue
                    .Replace(Constants.OpenParen, string.Empty)
                    .Replace(Constants.CloseParen, string.Empty);

                if (originalValue.Contains(Constants.Slash))
                {
                    return ProcessWithDashes(Constants.Slash, originalValue);
                }

                else if (originalValue.Contains(Constants.Dash))
                {
                    return ProcessWithDashes(Constants.Dash, originalValue);
                }

                else if (originalValue.Contains(Constants.Dot))
                {
                    return ProcessWithDashes(Constants.Dot, originalValue);
                }
                else
                {
                    if (DateTime.TryParse(originalValue, out DateTime dt))
                    {
                        return dt;
                    }
                    else
                    {
                        return ProcessWithoutDashes(originalValue);
                    }
                }
            }
        }
        /// <summary>
        /// Converts the converted value to the original representation.
        /// </summary>
        /// <param name="convertedValue">The original value to be converted.</param>
        /// <returns>
        /// The <see cref="DateTime"/> to be converted to a string.
        /// </returns>
        /// <remarks>
        /// The implementation of this method must be the inverse of
        /// the <see cref="Convert" /> method.
        /// </remarks>
        public string ConvertBack(DateTime convertedValue)
        {
            return convertedValue.ToString(Constants.USDateFormat);
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Processes the date string that contains dash or slash characters.
        /// </summary>
        /// <param name="character">
        /// The character being used as the delimiter.
        /// </param>
        /// <param name="dateString">
        /// The date string to be parsed.
        /// </param>
        /// <returns>
        /// The parsed <see cref="DateTime"/> value.
        /// </returns>
        private static DateTime ProcessWithDashes(string character, string dateString)
        {
            int year = 0;
            int month = 0;
            int day = 0;

            if (!string.IsNullOrEmpty(character) && !string.IsNullOrEmpty(dateString))
            {
                int leftIndex = dateString.IndexOf(character, StringComparison.Ordinal);
                int rightIndex = dateString.IndexOf(character, leftIndex + 1, StringComparison.Ordinal);

                if ((leftIndex != -1) || (rightIndex != -1))
                {
                    string leftValue = dateString.Substring(0, leftIndex);
                    if ((rightIndex - leftIndex) - 1 > 0)
                    {
                        string midValue = dateString.Substring(leftIndex + 1, (rightIndex - leftIndex) - 1);
                        string rightValue = dateString.Substring(rightIndex + 1, dateString.Length - (rightIndex + 1));
                        int spaceIndex = rightValue.IndexOf(Constants.Space, StringComparison.Ordinal);
                        if (spaceIndex > -1)
                        {
                            rightValue = rightValue.Substring(0, spaceIndex);
                        }

                        if (leftValue.Length > 2)
                        {
                            year = System.Convert.ToInt32(leftValue);
                            month = System.Convert.ToInt32(midValue);
                            if (month > 12)
                            {
                                day = month;
                                month = System.Convert.ToInt32(rightValue);
                            }
                            else
                            {
                                day = System.Convert.ToInt32(rightValue);
                            }
                        }
                        else if (rightValue.Length > 2)
                        {
                            year = System.Convert.ToInt32(rightValue);
                            month = System.Convert.ToInt32(leftValue);
                            if (month > 12)
                            {
                                day = month;
                                month = System.Convert.ToInt32(midValue);
                            }
                            else
                            {
                                day = System.Convert.ToInt32(midValue);
                            }
                        }
                    }
                    else
                    {
                        return MakeDate(MinYear, 1, 1);
                    }
                }
            }
            return MakeDate(year, month, day);
        }
        /// <summary>
        /// Processes the date string that does not contain a delimiter character.
        /// </summary>
        /// <param name="dateString">
        /// The date string to be parsed.
        /// </param>
        /// <returns>
        /// The parsed <see cref="DateTime"/> value.
        /// </returns>
        private static DateTime ProcessWithoutDashes(string dateString)
        {
            int year = 0;
            int month = 0;
            int day = 0;

            if (!string.IsNullOrEmpty(dateString))
            {
                if (dateString.Length == 8)
                {
                    string yearC = dateString.Substring(0, 4);
                    int yearCandidateValue = System.Convert.ToInt32(yearC);

                    if ((yearCandidateValue >= MinYear) && (yearCandidateValue < MaxYear))
                    {
                        month = System.Convert.ToInt32(dateString.Substring(4, 2));
                        if (month <= 12)
                        {
                            day = System.Convert.ToInt32(dateString.Substring(6, 2));
                        }
                        else
                        {
                            day = month;
                            month = System.Convert.ToInt32(dateString.Substring(6, 2));
                        }
                        year = yearCandidateValue;
                    }
                    else
                    {
                        year = System.Convert.ToInt32(dateString.Substring(4, 4));
                        month = System.Convert.ToInt32(dateString.Substring(2, 2));
                        if (month > 12)
                        {
                            day = month;
                            month = System.Convert.ToInt32(dateString.Substring(0, 2));
                        }
                        else
                        {
                            day = System.Convert.ToInt32(dateString.Substring(0, 2));
                        }
                    }
                }
            }
            return MakeDate(year, month, day);
        }
        /// <summary>
        /// Creates the date/time value.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns>
        /// The parsed <see cref="DateTime"/> value.
        /// </returns>
        private static DateTime MakeDate(int year, int month, int day)
        {
            DateTime returnDate = new DateTime(MinYear, 1, 1);
            if (DateIsValid(year, month, day))
            {
                try
                {
                    returnDate = new DateTime(year, month, day);
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                }
            }

            return returnDate;
        }
        /// <summary>
        /// Determines if the date values are valid.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns>
        /// <b>true</b> if the date values are valid; otherwise,
        /// returns <b>false</b>.
        /// </returns>
        private static bool DateIsValid(int year, int month, int day)
        {
            bool isValid = false;

            if ((year >= MinYear && year < MaxYear) &&
                (month > 0 && month < 13) &&
                (day > 0 && day <= 31))
            {
                if (month == 2 && day <= 28)
                {
                    isValid = true;
                }
                // ReSharper disable once AssignNullToNotNullAttribute
                else if (MonthsWith31Days.Contains(month) && day <= 31)
                {
                    isValid = true;
                }

                else if (day <= 30)
                {
                    isValid = true;
                }
            }
            return isValid;
        }
        #endregion
    }
}
