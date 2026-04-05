using Adaptive.Intelligence.Shared.UI.Properties;
using System.ComponentModel;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a UI control for displaying the status of a data drive.
    /// </summary>
    public class DriveItemControl : AdaptiveControlBase
    {
        #region Public Events
        /// <summary>
        /// Occurs when the control is selected.
        /// </summary>
        public event EventHandler? Selected;
        #endregion

        #region Private Member Declarations
        /// <summary>
        /// The drive letter.
        /// </summary>
        private string? _driveLetter;
        /// <summary>
        /// The drive information container.
        /// </summary>
        private DriveInfo? _info;
        /// <summary>
        /// The mouse in control flag.
        /// </summary>
        private bool _hasMouse;
        /// <summary>
        /// The mouse down flag.
        /// </summary>
        private bool _mouseDown;
        /// <summary>
        /// The selected flag.
        /// </summary>
        private bool _selected;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="DriveItemControl"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public DriveItemControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ContainerControl, false);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.UserPaint, true);

            Height = 48;

            _driveLetter = string.Empty;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            _info = null;
            _driveLetter = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the drive letter.
        /// </summary>
        /// <value>
        /// The drive letter of the drive whose information is to be displayed.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [Browsable(true),
         Description("Gets or sets the drive letter."),
         Category("Behavior")]
        public string? DriveLetter
        {
            get => _driveLetter;
            set
            {
                _driveLetter = value;
                try
                {
                    _info = new DriveInfo(_driveLetter + @":\");
                }
                catch
                {
                    _info = null;
                }

                Invalidate();
            }
        }
        #endregion

        #region Protected Method Overrides
        /// <summary>
        /// Raises the <see cref="E:MouseDown" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
                _mouseDown = true;
        }
        /// <summary>
        /// Raises the <see cref="E:MouseEnter" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hasMouse = true;
            Invalidate();
        }
        /// <summary>
        /// Raises the <see cref="E:MouseLeave" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hasMouse = false;
            Invalidate();
        }
        /// <summary>
        /// Raises the <see cref="E:MouseUp" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if ((e.Button == MouseButtons.Left) && _mouseDown)
            {
                _mouseDown = false;
                _selected = true;
                OnSelected(e);
                Invalidate();
            }

        }
        /// <summary>
        /// Raises the <see cref="E:Resize" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_info != null)
                Height = 48;
            else
                Height = 0;
        }

        #region Painting
        /// <summary>
        /// Raises the <see cref="E:PaintBackground" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (_info != null)
            {
                if (_hasMouse || _selected)
                    DrawSelectedBackgroundRectangle(e.Graphics);
                else
                    DrawNormalBackgroundRectangle(e.Graphics);
            }

            e.Graphics.DrawImage(Resources.UsbDrive, new PointF(0, 0));
        }
        /// <summary>
        /// Raises the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawVolumeLabel(e.Graphics);
            DrawSpaceLabel(e.Graphics);
            DrawPercentageBar(e.Graphics);
        }
        #endregion

        #endregion

        #region Protected Event Methods
        /// <summary>
        /// Raises the <see cref="E:Selected" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnSelected(EventArgs e)
        {
            ContinueInMainThread(() => { Selected?.Invoke(this, e); });
        }
        #endregion

        #region Internal Methods / Functions
        /// <summary>
        /// Un-selects the control.
        /// </summary>
        /// <remarks>
        /// This is called by the parent contains control when another instance is selected.
        /// </remarks>
        internal void UnSelect()
        {
            _selected = false;
            Invalidate();
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Draws the background of the control if and when the control is selected or highlighted.
        /// </summary>
        /// <param name="g">
        /// The <see cref="Graphics"/> object used for drawing.
        /// </param>
        private void DrawSelectedBackgroundRectangle(Graphics g)
        {
            // Calculate the client rectangle.
            Rectangle rect = new Rectangle(
                ClientRectangle.Left,
                ClientRectangle.Top,
                ClientRectangle.Width - 1,
                ClientRectangle.Height - 1);

            // Draw the basic window background.
            g.FillRectangle(SystemBrushes.Window, rect);

            // Calculate the hot-track color.
            int alphaValue = 50;
            if (_selected)
                alphaValue = 90;

            Color transHotTrackColor = Color.FromArgb(
                alphaValue,
                SystemColors.HotTrack.R,
                SystemColors.HotTrack.G,
                SystemColors.HotTrack.B);

            // Recalculate the rectangle.
            rect = new Rectangle(
                ClientRectangle.Left + 48,
                ClientRectangle.Top,
                ClientRectangle.Width - 49,
                ClientRectangle.Height - 1);

            // Draw the border.
            g.DrawRectangle(SystemPens.HotTrack, rect);

            // Fill the inside.
            using (SolidBrush backgroundBrush = new SolidBrush(transHotTrackColor))
            {
                g.FillRectangle(backgroundBrush, rect);
            }
        }
        /// <summary>
        /// Draws the normal background of the control.
        /// </summary>
        /// <param name="g">
        /// The <see cref="Graphics"/> object used for drawing.
        /// </param>
        private void DrawNormalBackgroundRectangle(Graphics g)
        {
            // Calculate the client rectangle.
            Rectangle rect = new Rectangle(
                ClientRectangle.Left,
                ClientRectangle.Top,
                ClientRectangle.Width - 1,
                ClientRectangle.Height - 1);

            // Draw the basic window background.
            g.FillRectangle(SystemBrushes.Window, rect);

        }
        /// <summary>
        /// Draws the volume label text at the top of the control.
        /// </summary>
        /// <param name="g">
        /// The <see cref="Graphics"/> object used for drawing.
        /// </param>
        private void DrawVolumeLabel(Graphics g)
        {
            string label;
            if (_info != null && _info.IsReady)
            {
                label = _info.VolumeLabel + " (" + _info.Name + ")";

            }
            else
            {
                label = "(Design Mode)";
            }

            g.DrawString(label, Font, SystemBrushes.WindowText, new PointF(50, 2));
        }
        /// <summary>
        /// Draws the space available label at the bottom of the control.
        /// </summary>
        /// <param name="g">
        /// The <see cref="Graphics"/> object used for drawing.
        /// </param>
        private void DrawSpaceLabel(Graphics g)
        {
            string sizeText;
            if (_info != null && _info.IsReady)
            {
                // Calculate the spaced used string.

                float availableSpace = ((float)_info.AvailableFreeSpace / (1024 * 1024 * 1024));
                float totalSpace = ((float)_info.TotalSize / (1024 * 1024 * 1024));
                sizeText = availableSpace.ToString("###,##0.0###") + " GB of " +
                           totalSpace.ToString("###,##0.0") + " GB free";

            }
            else
            {
                sizeText = "Size: (Design Mode)";
            }

            g.DrawString(sizeText, Font, SystemBrushes.WindowFrame, new PointF(50, 32));
        }

        private void InitializeComponent()
        {

        }

        /// <summary>
        /// Draws the usage percentage bar.
        /// </summary>
        /// <param name="g">
        /// The <see cref="Graphics"/> object used for drawing.
        /// </param>
        private void DrawPercentageBar(Graphics g)
        {
            Rectangle rect;
            if (_info != null && _info.IsReady)
            {
                // Calculate the % used.
                float pcnt = 1 - (float)_info.AvailableFreeSpace / _info.TotalSize;

                // Draw the border and background rectangles.
                rect = new Rectangle(52, 18, Width - 60, 10);
                g.DrawRectangle(SystemPens.GrayText, rect);

                Color backColor = Color.FromArgb(50, SystemColors.GrayText.R, SystemColors.GrayText.G,
                    SystemColors.GrayText.B);

                using (SolidBrush backBrush = new SolidBrush(backColor))
                {
                    g.FillRectangle(backBrush, rect);
                }

                // Fill the percentage rectangle.
                rect = new Rectangle(52, 18, (int)(rect.Width * pcnt), 10);

            }
            else
            {
                rect = new Rectangle(52, 18, Width - 60, 10);
            }

            g.FillRectangle(SystemBrushes.HotTrack, rect);
        }
        #endregion
    }
}