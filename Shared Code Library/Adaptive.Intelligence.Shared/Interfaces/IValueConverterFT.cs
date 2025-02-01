namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides a signature definition for types that convert one type to another.
    /// </summary>
    /// <remarks>
    /// This is generally used to convert enumerations to strings and back.
    /// </remarks>
    public interface IValueConverter<FromType, ToType>
    {
        /// <summary>
        /// Converts the original value to another value.
        /// </summary>
        /// <param name="originalValue">
        /// The original value to be converted.
        /// </param>
        /// <returns>
        /// The <typeparamref name="ToType"/> converted value.
        /// </returns>
        ToType Convert(FromType originalValue);
        /// <summary>
        /// Converts the converted value to the original representation.
        /// </summary>
        /// <remarks>
        /// The implementation of this method must be the inverse of the <see cref="Convert(FromType)"/> method.
        /// </remarks>
        /// <param name="convertedValue">
        /// The original value to be converted.
        /// </param>
        /// <returns>
        /// The <typeparamref name="FromType"/> value.
        /// </returns>
        FromType ConvertBack(ToType convertedValue);
    }
}