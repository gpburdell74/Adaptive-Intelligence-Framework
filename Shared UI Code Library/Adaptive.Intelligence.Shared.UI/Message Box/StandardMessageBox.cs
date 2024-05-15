namespace Adaptive.Intelligence.Shared.UI.Dialogs
{
	/// <summary>
	/// Provides a dialog to represent and be used as a standard Windows Message Box replacement.
	/// </summary>
	/// <seealso cref="Form" />
	/// <seealso cref="IMessageBoxWindow" />
	public partial class StandardMessageBox : AdaptiveDialogBase, IMessageBoxWindow
	{
		#region Private Member Declarations

		private const int ImageIndexInformation = 0;
		private const int ImageIndexQuestion = 1;
		private const int ImageIndexError = 2;
		private const int ImageIndexWarning = 3;

		private const int ButtonSpacing = 10;
		private const int ButtonSpacingDouble = 20;

		private const int AutoSizeWidth = 500;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="StandardMessageBox"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public StandardMessageBox()
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

			Caption = null;
			Message = null;
			IconToShow = null;

			components = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets or sets the buttons to be displayed on the message box.
		/// </summary>
		/// <value>
		/// The <see cref="MessageBoxButtons" /> enumeration indicating the buttons to display.
		/// </value>
		public MessageBoxButtons Buttons { get; set; }
		/// <summary>
		/// Gets or sets the caption text for the message box.
		/// </summary>
		/// <value>
		/// A string containing the caption text.
		/// </value>
		public string? Caption { get; set; }
		/// <summary>
		/// Gets or sets the icon to be displayed in the message box.
		/// </summary>
		/// <value>
		/// The <see cref="MessageBoxIcon" /> enumerated value indicating the icon to be displayed.
		/// </value>
		public MessageBoxIcon MessageIcon { get; set; }
		/// <summary>
		/// Gets or sets a reference to the image to be displayed instead of one of the standard images/icons.
		/// </summary>
		/// <value>
		/// The reference to an <see cref="Image"/> to be displayed; or <b>null</b>.
		/// </value>
		public Image? IconToShow { get; set; }
		/// <summary>
		/// Gets or sets the message text for the message box.
		/// </summary>
		/// <value>
		/// A string containing the message text.
		/// </value>
		public string? Message { get; set; }
		#endregion

		#region Protected Method Overrides		
		/// <summary>
		/// Assigns the event handlers for the controls on the dialog.
		/// </summary>
		protected override void AssignEventHandlers()
		{
			OkButton.Click += HandleOkClicked;
			CloseButton.Click += HandleCancelClicked;
			YesButton.Click += HandleYesClicked;
			NoButton.Click += HandleNoClicked;
			TryContinueButton.Click += HandleTryContinueClicked;
			IgnoreButton.Click += HandleIgnoreClicked;
			RetryButton.Click += HandleRetryClicked;
			AbortButton.Click += HandleAbortClicked;

		}
		/// <summary>
		/// Removes the event handlers for the controls on the dialog.
		/// </summary>
		protected override void RemoveEventHandlers()
		{
			OkButton.Click -= HandleOkClicked;
			CloseButton.Click -= HandleCancelClicked;
			YesButton.Click -= HandleYesClicked;
			NoButton.Click -= HandleNoClicked;
			TryContinueButton.Click -= HandleTryContinueClicked;
			IgnoreButton.Click -= HandleIgnoreClicked;
			RetryButton.Click -= HandleRetryClicked;
			AbortButton.Click -= HandleAbortClicked;
		}
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (Visible)
			{
				SetSize();
				CenterButtons();
			}
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Shows the message box window and returns the user response.
		/// </summary>
		/// <returns>
		/// A <see cref="DialogResult" /> enumerated value indicating the user response.
		/// </returns>
		public new DialogResult Show()
		{
			ArrangeSelf();
			return ShowDialog();
		}
		#endregion

		#region Private Event Handlers		
		/// <summary>
		/// Handles the event when the OK button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void HandleOkClicked(object? sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
		/// <summary>
		/// Handles the event when the Cancel button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void HandleCancelClicked(object? sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
		/// <summary>
		/// Handles the event when the Yes button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void HandleYesClicked(object? sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
			Close();
		}
		/// <summary>
		/// Handles the event when the No button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void HandleNoClicked(object? sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
			Close();
		}
		/// <summary>
		/// Handles the event when the Try Continue button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void HandleTryContinueClicked(object? sender, EventArgs e)
		{
			DialogResult = DialogResult.TryAgain;
			Close();
		}
		/// <summary>
		/// Handles the event when the Ignore button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void HandleIgnoreClicked(object? sender, EventArgs e)
		{
			DialogResult = DialogResult.Ignore;
			Close();
		}
		/// <summary>
		/// Handles the event when the Retry button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void HandleRetryClicked(object? sender, EventArgs e)
		{
			DialogResult = DialogResult.Retry;
			Close();
		}
		/// <summary>
		/// Handles the event when the Abort button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void HandleAbortClicked(object? sender, EventArgs e)
		{
			DialogResult = DialogResult.Abort;
			Close();
		}
		#endregion

		#region Private Methods / Functions		
		/// <summary>
		/// Arranges the content and size of the message box window.
		/// </summary>
		private void ArrangeSelf()
		{
			SetImage();

			CaptionLabel.Text = Caption!;
			MessageLabel.Text = Message!;

			OkButton.Visible = true;
			YesButton.Visible = false;
			NoButton.Visible = false;
			TryContinueButton.Visible = false;
			IgnoreButton.Visible = false;
			RetryButton.Visible = false;
			AbortButton.Visible = false;
			CloseButton.Visible = false;

			switch (Buttons)
			{
				case MessageBoxButtons.OK:
					OkButton.Visible = true;
					break;

				case MessageBoxButtons.OKCancel:
					OkButton.Visible = true;
					CloseButton.Visible = true;
					break;

				case MessageBoxButtons.YesNo:
					YesButton.Visible = true;
					NoButton.Visible = true;
					break;

				case MessageBoxButtons.YesNoCancel:
					YesButton.Visible = true;
					NoButton.Visible = true;
					CloseButton.Visible = true;
					break;

				case MessageBoxButtons.AbortRetryIgnore:
					AbortButton.Visible = true;
					RetryButton.Visible = true;
					IgnoreButton.Visible = true;
					break;

				case MessageBoxButtons.RetryCancel:
					RetryButton.Visible = true;
					CloseButton.Visible = false;

					break;

				case MessageBoxButtons.CancelTryContinue:
					TryContinueButton.Visible = true;
					CloseButton.Visible = true;
					break;
			}
			SetSize();
		}
		/// <summary>
		/// Centers the visible buttons within the dialog.
		/// </summary>
		private void CenterButtons()
		{
			// Get the list of currently visible buttons.
			List<Button> buttonList = GetVisibleButtonsList();
			int width = 0;

			// Add the total width for the buttons and spacing.
			foreach (Button visibleButton in buttonList)
			{
				// The button width + 10.
				width += visibleButton.Width + ButtonSpacing;
			}

			// Calculate the starting point for the first button.
			int maxWidth = ButtonPanel.Width - ButtonSpacingDouble;
			int left = ((maxWidth - width) / 2) + ButtonSpacing;

			// Place each button.
			foreach (Button visibleButton in buttonList)
			{
				visibleButton.Left = left;
				visibleButton.Top = 5;
				left += visibleButton.Width + ButtonSpacing;
			}
			buttonList.Clear();
		}
		/// <summary>
		/// Gets the list of currently visible buttons.
		/// </summary>
		/// <returns>
		/// A <see cref="List{T}"/> of <see cref="Button"/> instances that contains only the
		/// currently visible buttons.
		/// </returns>
		private List<Button> GetVisibleButtonsList()
		{
			List<Button> list = new List<Button>(8);

			if (OkButton.Visible)
				list.Add(OkButton);

			if (YesButton.Visible)
				list.Add(YesButton);

			if (NoButton.Visible)
				list.Add(NoButton);

			if (CloseButton.Visible)
				list.Add(CloseButton);

			if (AbortButton.Visible)
				list.Add(AbortButton);

			if (RetryButton.Visible)
				list.Add(RetryButton);

			if (IgnoreButton.Visible)
				list.Add(IgnoreButton);

			if (TryContinueButton.Visible)
				list.Add(TryContinueButton);

			return list;
		}
		/// <summary>
		/// Sets the image/icon for display.
		/// </summary>
		private void SetImage()
		{
			// If the user specified a different image to use, use that image;
			// otherwise, use one of the standard message box images.
			if (IconToShow != null)
				IconImage.Image = IconToShow;
			else
			{
				switch (MessageIcon)
				{
					case MessageBoxIcon.Question:
						IconImage.Image = StandardIcons.Images[ImageIndexQuestion];
						break;

					case MessageBoxIcon.Warning:
						IconImage.Image = StandardIcons.Images[ImageIndexWarning];
						break;

					case MessageBoxIcon.Error:
						IconImage.Image = StandardIcons.Images[ImageIndexError];
						break;

					case MessageBoxIcon.Information:
						IconImage.Image = StandardIcons.Images[ImageIndexInformation];
						break;
				}
			}
		}
		/// <summary>
		/// Sets the size of the dialog based on its content.
		/// </summary>
		private void SetSize()
		{
			if (Visible)
			{
				// 1. Calculate buttons width.
				int width = 0;

				List<Button> buttonList = GetVisibleButtonsList();
				int count = buttonList.Count;
				foreach (Button visibleButton in buttonList)
					width += visibleButton.Width;

				// Minimum spacing for one button required.
				if (count == 0)
					count = 1;

				// Set the spacing width(s).
				width += (count + 1) * ButtonSpacing;
				int minWindowWidth = width;

				// Set the message label's auto size boundaries.
				MessageLabel.AutoSizeMaxWidth = AutoSizeWidth;
				if (minWindowWidth > AutoSizeWidth)
					MessageLabel.AutoSizeMaxWidth = minWindowWidth;

				// Size the dialog based on the buttons and the actual size of the 
				// text message.
				Size messageSize = MessageLabel.GetDesiredSize();

				Height = ButtonPanel.Height + TopPanel.Height + ButtonSpacing + messageSize.Height + 28;
				Width = minWindowWidth + 52;
				MessageLabel.Location = new Point(0, 40);

				// Center the buttons.
				CenterButtons();
			}
		}
		#endregion

	}
}
