namespace Adaptive
{
    /// <summary>
    /// Provides static math functions, like <see cref="System.Math"/>
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// Calculates the percentage value from the provided values.
        /// </summary>
        /// <param name="numerator">
        /// An integer specifying the numerator value.
        /// </param>
        /// <param name="denominator">
        /// An integer specifying the denominator value.
        /// </param>
        /// <returns>
        /// A <see cref="float"/> containing the percentage value.
        /// </returns>
        public static int Percent(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                return 0;
            }

            return (int)(((float)numerator / (float)denominator) * 100);
        }
        /// <summary>
        /// Calculates the percentage value from the provided values.
        /// </summary>
        /// <param name="numerator">
        /// An integer specifying the numerator value.
        /// </param>
        /// <param name="denominator">
        /// An integer specifying the denominator value.
        /// </param>
        /// <returns>
        /// A <see cref="float"/> containing the percentage value.
        /// </returns>
        public static float PercentF(float numerator, float denominator)
        {
            if (denominator == 0)
            {
                return 0;
            }

            return (numerator / denominator) * 100;
        }
    }
}
