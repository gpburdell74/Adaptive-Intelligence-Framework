using System.ComponentModel;

namespace Adaptive.Intelligence.Shared.UI
{
    /// <summary>
    /// Provides a simple control for displaying a rating with "star" icons.
    /// </summary>
    /// <seealso cref="Adaptive.Intelligence.Shared.UI.AdaptiveControlBase" />
    public partial class RatingControl : AdaptiveControlBase
    {
        #region Private Member Declarations		
        /// <summary>
        /// The value.
        /// </summary>
        private int _value = 1;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="RatingControl"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public RatingControl()
        {
            InitializeComponent();
        }
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                components?.Dispose();
            }

            components = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the rating value.
        /// </summary>
        /// <value>
        /// An integer specifying the rating value in the range zero (0) to five (5).
        /// </value>
        [Browsable(true),
         Description("Gets or sets the rating value in the control from 0 to 5."),
            Category("Behavior")]
        public int Rating
        {
            get => _value;
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > 5)
                    value = 5;
                _value = value;
                SetStars();
                Invalidate();
            }
        }
        #endregion

        #region Protected Method Overrides		
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            int x = 5;
            int width = Width / 5;

            if (StarA != null)
            {
                StarA.Left = x;
                StarB.Left = x + width;
                StarC.Left = x + (width * 2);
                StarD.Left = x + (width * 3);
                StarE.Left = x + (width * 4);
                SetStars();
            }
        }
        #endregion

        #region Protected Method Overrides		
        /// <summary>
        /// Assigns the event handlers for the controls on the dialog.
        /// </summary>
        protected override void AssignEventHandlers()
        {
            StarA.Click += HandleStarClick;
            StarB.Click += HandleStarClick;
            StarC.Click += HandleStarClick;
            StarD.Click += HandleStarClick;
            StarE.Click += HandleStarClick;

        }
        /// <summary>
        /// Removes the event handlers for the controls on the dialog.
        /// </summary>
        protected override void RemoveEventHandlers()
        {
            StarA.Click -= HandleStarClick;
            StarB.Click -= HandleStarClick;
            StarC.Click -= HandleStarClick;
            StarD.Click -= HandleStarClick;
            StarE.Click -= HandleStarClick;
        }
        /// <summary>
        /// Initializes the control and dialog state according to the form data.
        /// </summary>
        protected override void InitializeDataContent()
        {
            StarA.Tag = 1;
            StarB.Tag = 2;
            StarC.Tag = 3;
            StarD.Tag = 4;
            StarE.Tag = 5;
        }
        #endregion

        #region Private Methods / Functions		
        /// <summary>
        /// Sets the display state for the star images.
        /// </summary>
        private void SetStars()
        {
            SetControl(StarA, 1);
            SetControl(StarB, 2);
            SetControl(StarC, 3);
            SetControl(StarD, 4);
            SetControl(StarE, 5);
        }
        /// <summary>
        /// Sets the image for each control based on the value.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="maxValue">The maximum value.</param>
        private void SetControl(PictureBox control, int maxValue)
        {
            if (_value >= maxValue)
                control.Image = StarImages.Images[1];
            else
                control.Image = StarImages.Images[0];
        }
        #endregion

        #region Private Event Handlers		
        /// <summary>
        /// Handles the event when one of the stars is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleStarClick(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                Control reference = ((Control)sender);
                if (reference.Tag != null)
                    _value = (int)reference.Tag;

                SetStars();
            }
        }
        #endregion
    }
}
