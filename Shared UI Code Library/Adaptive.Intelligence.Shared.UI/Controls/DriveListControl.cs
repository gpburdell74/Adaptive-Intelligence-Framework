using System.ComponentModel;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a control for selecting from a list of data drives.
    /// </summary>
    public class DriveListControl : AdaptiveControlBase
    {
        #region Public Events
        /// <summary>
        /// Occurs when a drive is selected.
        /// </summary>
        public event EventHandler? SelectedDriveChanged;
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// The list of drives.
        /// </summary>
        private IEnumerable<DriveInfo>? _list;
        /// <summary>
        /// The selected control.
        /// </summary>
        private DriveItemControl? _selectedControl;
        /// <summary>
        /// Flag to show only the removable drives.
        /// </summary>
        private bool _removableOnly;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="DriveListControl"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public DriveListControl()
        {
            SuspendLayout();
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            DoubleBuffered = true;
            Font = UIConstants.CreateStandardFont();
            Name = "DriveListControl";
            Size = new Size(518, 355);
            ResumeLayout(false);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            _list = null;
            _selectedControl = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether to show only removable drives.
        /// </summary>
        /// <value>
        ///   <c>true</c>  to show only removable drives; otherwise, <c>false</c>.
        /// </value>
        public bool RemovableOnly
        {
            get => _removableOnly;
            set
            {
                _removableOnly = value;
                LoadDrives();
                Invalidate();
            }
        }
        /// <summary>
        /// Gets the selected drive letter.
        /// </summary>
        /// <value>
        /// The letter of the selected drive, or <b>null</b> if one is not selected.
        /// </value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string? SelectedDrive
        {
            get
            {
                if (_selectedControl == null)
                    return string.Empty;
                else
                    return _selectedControl.DriveLetter;
            }
        }
        #endregion

        #region Protected Event Methods
        /// <summary>
        /// Raises the <see cref="E:SelectedDriveChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnSelectedDriveChanged(EventArgs e)
        {
            ContinueInMainThread(() => { SelectedDriveChanged?.Invoke(this, e); });
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Loads the list of drives into the control.
        /// </summary>
        private void LoadDrives()
        {
            // Remove old controls.
            Controls.Clear();

            // Populate in reverse because of the order in how added controls are displayed.
            _list = DriveInfo.GetDrives().Reverse();

            foreach (DriveInfo item in _list)
            {
                if ((item.IsReady) && ((!_removableOnly || (item.DriveType == DriveType.Removable))))
                {
                    DriveItemControl newControl = new DriveItemControl
                    {
                        Name = "DriveControl" + item.Name[0],
                        DriveLetter = item.Name[0].ToString(),
                        Dock = DockStyle.Top
                    };
                    Controls.Add(newControl);
                    newControl.Selected += HandleItemSelection;
                }
            }
        }

        private void InitializeComponent()
        {

        }

        /// <summary>
        /// Handles the event when a drive item control is selected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleItemSelection(object? sender, EventArgs e)
        {
            DriveItemControl? selectedControl = sender as DriveItemControl;
            if ((selectedControl != null) && (selectedControl != _selectedControl))
            {
                _selectedControl = selectedControl;

                // De-select all others.
                foreach (DriveItemControl ctl in Controls)
                {
                    if (ctl != _selectedControl)
                        ctl.UnSelect();
                }
                OnSelectedDriveChanged(EventArgs.Empty);
            }
        }
        #endregion
    }
}
