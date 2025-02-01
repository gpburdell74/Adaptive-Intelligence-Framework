namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides static methods / Functions for sorting.
    /// </summary>
    public static class Sorting
    {
        #region Public Static Methods / Functions
        /// <summary>
        /// Sorts an array of integers using the Quick Sort algorithm.
        /// </summary>
        /// <param name="sourceList">
        /// A <see cref="List{T}"/> of <see cref="int"/> values to be sorted.
        /// </param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="int"/> values sorted in ascending order.
        /// </returns>
        public static List<int> QuickSortInt32(List<int> sourceList)
        {
            List<int> sorted = new List<int>(sourceList.Count);
            sorted.AddRange(sourceList);

            if (sorted.Count >= 2)
            {
                int[] original = sourceList.ToArray();
                QuickSortInt32(original, 0, original.Length - 1);
                sorted.Clear();
                sorted.AddRange(original);
            }
            return sorted;
        }
        #endregion

        #region Private Static QuickSort Methods
        /// <summary>
        /// The main function that implements QuickSort.
        /// </summary>
        /// <param name="array">
        /// The array of integers to be sorted.
        /// </param>
        /// <param name="minIndex">
        /// An integer specifying the minimum ordinal index in the array.
        /// </param>
        /// <param name="maxIndex">
        /// An integer specifying the maximum ordinal index in the array.
        /// </param>
        private static void QuickSortInt32(int[] array, int minIndex, int maxIndex)
        {
            if (minIndex < maxIndex)
            {
                // Find the partition index.
                int partitionIndex = Partition(array, minIndex, maxIndex);

                // Separately sort elements before and after partition index.
                QuickSortInt32(array, minIndex, partitionIndex - 1);
                QuickSortInt32(array, partitionIndex + 1, maxIndex);
            }
        }

        // This function takes last element as pivot,
        // places the pivot element at its correct position
        // in sorted array, and places all smaller to left
        // of pivot and all greater elements to right of pivot
        private static int Partition(int[] array, int low, int high)
        {
            // Choosing the pivot
            int pivot = array[high];

            // Index of smaller element and indicates
            // the right position of pivot found so far
            int index = (low - 1);

            for (int minIndex = low; minIndex <= high - 1; minIndex++)
            {

                // If current element is smaller than the pivot
                if (array[minIndex] < pivot)
                {

                    // Increment index of smaller element
                    index++;
                    UnsafeArrayElementSwap<int>(array, index, minIndex);
                }
            }
            UnsafeArrayElementSwap(array, index + 1, high);
            return (index + 1);
        }
        /// <summary>
        /// Performs the element array value swap.
        /// </summary>
        /// <typeparam name="T">
        /// The data type of the elements in the array.
        /// </typeparam>
        /// <param name="array">
        /// The array to swap elements in.
        /// </param>
        /// <param name="firstIndex">
        /// An integer specifying the ordinal index of the first element.
        /// </param>
        /// <param name="secondIndex">
        /// An integer specifying the ordinal index of the second element.
        /// </param>
        private static void UnsafeArrayElementSwap<T>(T[] array, int firstIndex, int secondIndex)
        {
            (array[firstIndex], array[secondIndex]) = (array[secondIndex], array[firstIndex]);
        }
        #endregion
    }
}
