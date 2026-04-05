using System.Collections;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides an <see cref="IComparer"/> implementation for the <see cref="ColumnSortListView"/> control.
    /// </summary>
    public sealed class ListViewColumnSorter : IComparer
    {
        #region Private Member Declarations
        /// <summary>
        /// The index of the column to sort.
        /// </summary>
        private int _columnIndex;
        #endregion

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>
        /// The sort direction.
        /// </value>
        public SortOrder SortDirection { get; set; }
        /// <summary>
        /// Gets or sets the index of the column being sorted.
        /// </summary>
        /// <value>
        /// The index of the column being sorted.
        /// </value>
        public int ColumnIndex
        {
            get => _columnIndex;
            set => _columnIndex = value;
        }
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero <paramref name="x" /> is less than <paramref name="y" />. Zero <paramref name="x" /> equals <paramref name="y" />. Greater than zero <paramref name="x" /> is greater than <paramref name="y" />.
        /// </returns>
        public int Compare(object? x, object? y)
        {
            ListViewItem? left = x as ListViewItem;
            ListViewItem? right = y as ListViewItem;

            int returnValue;
            string? leftContent = ReadTextContent(left, _columnIndex);
            string? rightContent = ReadTextContent(right, _columnIndex);

            if ((leftContent == null) && (rightContent == null))
                returnValue = 0;
            else if (rightContent == null)
                returnValue = 1;
            else if (leftContent == null)
                returnValue = -1;
            else
                returnValue = string.Compare(leftContent, rightContent, StringComparison.InvariantCultureIgnoreCase);

            if (SortDirection == SortOrder.Descending)
                returnValue *= -1;

            return returnValue;

        }
        /// <summary>
        /// Reads the text content of the list view item at the specified column index.
        /// </summary>
        /// <param name="item">
        /// The <see cref="ListViewItem"/> to retrieve the text from.
        /// </param>
        /// <param name="columnIndex">
        /// The index of the column in the list view item's sub items collection.
        /// </param>
        /// <returns>
        /// The string content, or <b>null</b> if the content is not present.
        /// </returns>
        private static string? ReadTextContent(ListViewItem? item, int columnIndex)
        {
            if (item == null)
                return null;

            else if (item.SubItems.Count < columnIndex + 1)
                return null;

            else
                return item.SubItems[columnIndex].Text;
        }
    }
}
