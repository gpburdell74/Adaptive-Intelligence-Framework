namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides a signature definition for types that convert one type to another, in
    /// a one-way direction.
    /// </summary>
    /// <remarks>
    /// This is generally used to convert enumerations to strings, with no reverse
    /// conversion.
    /// </remarks>
    public interface IOneWayValueConverter<in FromType, out ToType>
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
    }
}