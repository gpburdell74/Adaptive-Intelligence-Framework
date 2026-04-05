namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a list view control whose contents may be sorted by clicking on the column header.
    /// </summary>
    public class ColumnSortListView : ListView
    {
        #region Private Member Declarations
        /// <summary>
        /// The sorting class instance.
        /// </summary>
        private readonly ListViewColumnSorter? _sorter;
        /// <summary>
        /// The _last column index
        /// </summary>
        private int _lastColumnIndex = -1;
        /// <summary>
        /// The sort direction flag.
        /// </summary>
        private bool _sortAscending = true;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSortListView"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public ColumnSortListView()
        {
            //Activate double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            //Enable the OnNotifyMessage event so we get a chance to filter out 
            // Windows messages before they get to the form's WndProc.
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);

            _sorter = new ListViewColumnSorter() { ColumnIndex = 0 };
            ListViewItemSorter = _sorter;
        }
        #endregion

        /// <summary>
        /// Raises the <see cref="ListView.ColumnClick" /> event.
        /// </summary>
        /// <param name="e">A <see cref="ColumnClickEventArgs" /> that contains the event data.</param>
        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            base.OnColumnClick(e);
            if (_sorter != null)
            {
                _sorter.ColumnIndex = e.Column;

                // Sort.
                if (e.Column == _lastColumnIndex)
                    _sortAscending = !_sortAscending;
                else
                {
                    _lastColumnIndex = e.Column;
                    _sortAscending = true;
                }
                if (_sortAscending)
                    _sorter.SortDirection = SortOrder.Ascending;
                else
                    _sorter.SortDirection = SortOrder.Descending;

                Sort();
            }
        }
        /// <summary>
        /// Notifies the control of Windows messages.
        /// </summary>
        /// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that represents the Windows message.</param>
        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }
        /// <summary>
        /// This property is not relevant for this class.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }

}
