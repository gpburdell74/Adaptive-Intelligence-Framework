using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides static methods / functions for implementing standard Euclidean algorithms.
    /// </summary>
    public static class Euclid
    {
        /// <summary>
        /// Finds the greatest common denominator for two integers.
        /// </summary>
        /// <param name="leftValue">
        /// The first integer value.
        /// </param>
        /// <param name="rightValue">
        /// The second integer value.
        /// </param>
        /// <returns>
        /// An integer that is the greatest common denominator for the two provided integers.
        /// </returns>
        public static int FindGreatestCommonDenominator(int leftValue, int rightValue)
        {
            int highValue = 0;
            int lowValue = 0;

            if (rightValue >= leftValue)
            {
                highValue = rightValue;
                lowValue = leftValue;
            }
            else
            {
                highValue = leftValue;
                lowValue = rightValue;
            }

            // 1. Step 1 - Divide the larger by the smaller number:
            int remainder = highValue % lowValue;
            int divisor = lowValue;

            // Continue step 2 until no remainders are left.
            while (remainder > 0)
            {
                // Step 2 - Divide the divisor by the remainder from the previous step: 
                int newRemainder = divisor % remainder;
                divisor = remainder;
                remainder = newRemainder;
            }

            return divisor;
        }
    }
}

