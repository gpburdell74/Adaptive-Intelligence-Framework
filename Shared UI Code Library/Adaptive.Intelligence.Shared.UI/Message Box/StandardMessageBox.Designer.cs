namespace Adaptive.Intelligence.Shared.UI.Dialogs
{
	partial class StandardMessageBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components;

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StandardMessageBox));
			StandardIcons = new ImageList(components);
			ContainerPanel = new Panel();
			MessageLabel = new AdvancedLabel();
			IconImage = new PictureBox();
			TopPanel = new Panel();
			CaptionLabel = new AdvancedLabel();
			ButtonPanel = new Panel();
			CloseButton = new AIButton();
			TryContinueButton = new AIButton();
			IgnoreButton = new AIButton();
			RetryButton = new AIButton();
			AbortButton = new AIButton();
			NoButton = new AIButton();
			YesButton = new AIButton();
			OkButton = new AIButton();
			ContainerPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)IconImage).BeginInit();
			TopPanel.SuspendLayout();
			ButtonPanel.SuspendLayout();
			SuspendLayout();
			// 
			// StandardIcons
			// 
			StandardIcons.ColorDepth = ColorDepth.Depth8Bit;
			StandardIcons.ImageStream = (ImageListStreamer)resources.GetObject("StandardIcons.ImageStream");
			StandardIcons.TransparentColor = Color.Transparent;
			StandardIcons.Images.SetKeyName(0, "Security Check.png");
			StandardIcons.Images.SetKeyName(1, "Security Doubtful.png");
			StandardIcons.Images.SetKeyName(2, "Security Risk.png");
			StandardIcons.Images.SetKeyName(3, "Security Warning.png");
			// 
			// ContainerPanel
			// 
			ContainerPanel.BackColor = Color.DimGray;
			ContainerPanel.Controls.Add(MessageLabel);
			ContainerPanel.Controls.Add(IconImage);
			ContainerPanel.Controls.Add(TopPanel);
			ContainerPanel.Controls.Add(ButtonPanel);
			ContainerPanel.Dock = DockStyle.Fill;
			ContainerPanel.Location = new Point(5, 6);
			ContainerPanel.Name = "ContainerPanel";
			ContainerPanel.Size = new Size(1082, 242);
			ContainerPanel.TabIndex = 7;
			// 
			// MessageLabel
			// 
			MessageLabel.AutoSize = true;
			MessageLabel.AutoSizeMaxWidth = 500;
			MessageLabel.Dock = DockStyle.Fill;
			MessageLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
			MessageLabel.ForeColor = Color.White;
			MessageLabel.Location = new Point(32, 48);
			MessageLabel.Name = "MessageLabel";
			MessageLabel.Padding = new Padding(0, 23, 0, 0);
			MessageLabel.Size = new Size(1050, 143);
			MessageLabel.TabIndex = 12;
			MessageLabel.TabStop = false;
			MessageLabel.Text = "When it comes to encryption key management systems for local desktops and filesystems in the context of C# and .NET software development, you might want to consider solutions that integrate well.";
			MessageLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// IconImage
			// 
			IconImage.BackColor = Color.Transparent;
			IconImage.Dock = DockStyle.Left;
			IconImage.Location = new Point(0, 48);
			IconImage.Name = "IconImage";
			IconImage.Size = new Size(32, 143);
			IconImage.SizeMode = PictureBoxSizeMode.Zoom;
			IconImage.TabIndex = 9;
			IconImage.TabStop = false;
			// 
			// TopPanel
			// 
			TopPanel.BackColor = Color.Gray;
			TopPanel.Controls.Add(CaptionLabel);
			TopPanel.Dock = DockStyle.Top;
			TopPanel.Location = new Point(0, 0);
			TopPanel.Name = "TopPanel";
			TopPanel.Padding = new Padding(10);
			TopPanel.Size = new Size(1082, 48);
			TopPanel.TabIndex = 10;
			// 
			// CaptionLabel
			// 
			CaptionLabel.Dock = DockStyle.Top;
			CaptionLabel.Font = new Font("Verdana", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
			CaptionLabel.ForeColor = Color.White;
			CaptionLabel.Location = new Point(10, 10);
			CaptionLabel.Name = "CaptionLabel";
			CaptionLabel.Size = new Size(1062, 28);
			CaptionLabel.TabIndex = 10;
			CaptionLabel.TabStop = false;
			CaptionLabel.Text = "Caption";
			CaptionLabel.TextAlign = ContentAlignment.TopCenter;
			// 
			// ButtonPanel
			// 
			ButtonPanel.BackColor = Color.Transparent;
			ButtonPanel.Controls.Add(CloseButton);
			ButtonPanel.Controls.Add(TryContinueButton);
			ButtonPanel.Controls.Add(IgnoreButton);
			ButtonPanel.Controls.Add(RetryButton);
			ButtonPanel.Controls.Add(AbortButton);
			ButtonPanel.Controls.Add(NoButton);
			ButtonPanel.Controls.Add(YesButton);
			ButtonPanel.Controls.Add(OkButton);
			ButtonPanel.Dock = DockStyle.Bottom;
			ButtonPanel.Location = new Point(0, 191);
			ButtonPanel.Name = "ButtonPanel";
			ButtonPanel.Size = new Size(1082, 51);
			ButtonPanel.TabIndex = 11;
			// 
			// CloseButton
			// 
			CloseButton.BorderWidth = 5;
			CloseButton.Checked = false;
			CloseButton.HoverBorderColor = Color.Maroon;
			CloseButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			CloseButton.HoverEndColor = Color.FromArgb(255, 128, 128);
			CloseButton.HoverFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			CloseButton.HoverForeColor = Color.Black;
			CloseButton.HoverStartColor = Color.FromArgb(255, 192, 192);
			CloseButton.Location = new Point(950, 6);
			CloseButton.Name = "CloseButton";
			CloseButton.NormalBorderColor = Color.FromArgb(255, 128, 128);
			CloseButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			CloseButton.NormalEndColor = Color.MistyRose;
			CloseButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			CloseButton.NormalForeColor = Color.Black;
			CloseButton.NormalStartColor = Color.White;
			CloseButton.PressedBorderColor = Color.Maroon;
			CloseButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			CloseButton.PressedEndColor = Color.Black;
			CloseButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			CloseButton.PressedForeColor = Color.White;
			CloseButton.PressedStartColor = Color.Red;
			CloseButton.Size = new Size(130, 36);
			CloseButton.TabIndex = 7;
			CloseButton.Text = "Cancel";
			CloseButton.UseVisualStyleBackColor = true;
			// 
			// TryContinueButton
			// 
			TryContinueButton.BorderWidth = 5;
			TryContinueButton.Checked = false;
			TryContinueButton.HoverBorderColor = Color.White;
			TryContinueButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			TryContinueButton.HoverEndColor = Color.Gray;
			TryContinueButton.HoverFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			TryContinueButton.HoverForeColor = Color.White;
			TryContinueButton.HoverStartColor = Color.Silver;
			TryContinueButton.Location = new Point(815, 6);
			TryContinueButton.Name = "TryContinueButton";
			TryContinueButton.NormalBorderColor = Color.White;
			TryContinueButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			TryContinueButton.NormalEndColor = Color.Gray;
			TryContinueButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			TryContinueButton.NormalForeColor = Color.White;
			TryContinueButton.NormalStartColor = Color.Gray;
			TryContinueButton.PressedBorderColor = Color.Gray;
			TryContinueButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			TryContinueButton.PressedEndColor = Color.Black;
			TryContinueButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			TryContinueButton.PressedForeColor = Color.Black;
			TryContinueButton.PressedStartColor = Color.White;
			TryContinueButton.Size = new Size(130, 36);
			TryContinueButton.TabIndex = 6;
			TryContinueButton.Text = "&Try Continue";
			TryContinueButton.UseVisualStyleBackColor = true;
			// 
			// IgnoreButton
			// 
			IgnoreButton.BorderWidth = 5;
			IgnoreButton.Checked = false;
			IgnoreButton.HoverBorderColor = Color.White;
			IgnoreButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			IgnoreButton.HoverEndColor = Color.Gray;
			IgnoreButton.HoverFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			IgnoreButton.HoverForeColor = Color.White;
			IgnoreButton.HoverStartColor = Color.Silver;
			IgnoreButton.Location = new Point(680, 6);
			IgnoreButton.Name = "IgnoreButton";
			IgnoreButton.NormalBorderColor = Color.White;
			IgnoreButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			IgnoreButton.NormalEndColor = Color.Gray;
			IgnoreButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			IgnoreButton.NormalForeColor = Color.White;
			IgnoreButton.NormalStartColor = Color.Gray;
			IgnoreButton.PressedBorderColor = Color.Gray;
			IgnoreButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			IgnoreButton.PressedEndColor = Color.Black;
			IgnoreButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			IgnoreButton.PressedForeColor = Color.Black;
			IgnoreButton.PressedStartColor = Color.White;
			IgnoreButton.Size = new Size(130, 36);
			IgnoreButton.TabIndex = 5;
			IgnoreButton.Text = "&Ignore";
			IgnoreButton.UseVisualStyleBackColor = true;
			// 
			// RetryButton
			// 
			RetryButton.BorderWidth = 5;
			RetryButton.Checked = false;
			RetryButton.HoverBorderColor = Color.White;
			RetryButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			RetryButton.HoverEndColor = Color.Gray;
			RetryButton.HoverFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			RetryButton.HoverForeColor = Color.White;
			RetryButton.HoverStartColor = Color.Silver;
			RetryButton.Location = new Point(545, 6);
			RetryButton.Name = "RetryButton";
			RetryButton.NormalBorderColor = Color.White;
			RetryButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			RetryButton.NormalEndColor = Color.Gray;
			RetryButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			RetryButton.NormalForeColor = Color.White;
			RetryButton.NormalStartColor = Color.Gray;
			RetryButton.PressedBorderColor = Color.Gray;
			RetryButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			RetryButton.PressedEndColor = Color.Black;
			RetryButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			RetryButton.PressedForeColor = Color.Black;
			RetryButton.PressedStartColor = Color.White;
			RetryButton.Size = new Size(130, 36);
			RetryButton.TabIndex = 4;
			RetryButton.Text = "&Retry";
			RetryButton.UseVisualStyleBackColor = true;
			// 
			// AbortButton
			// 
			AbortButton.BorderWidth = 5;
			AbortButton.Checked = false;
			AbortButton.HoverBorderColor = Color.White;
			AbortButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			AbortButton.HoverEndColor = Color.Gray;
			AbortButton.HoverFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			AbortButton.HoverForeColor = Color.White;
			AbortButton.HoverStartColor = Color.Silver;
			AbortButton.Location = new Point(410, 6);
			AbortButton.Name = "AbortButton";
			AbortButton.NormalBorderColor = Color.White;
			AbortButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			AbortButton.NormalEndColor = Color.Gray;
			AbortButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			AbortButton.NormalForeColor = Color.White;
			AbortButton.NormalStartColor = Color.Gray;
			AbortButton.PressedBorderColor = Color.Gray;
			AbortButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			AbortButton.PressedEndColor = Color.Black;
			AbortButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			AbortButton.PressedForeColor = Color.Black;
			AbortButton.PressedStartColor = Color.White;
			AbortButton.Size = new Size(130, 36);
			AbortButton.TabIndex = 3;
			AbortButton.Text = "&Abort";
			AbortButton.UseVisualStyleBackColor = true;
			// 
			// NoButton
			// 
			NoButton.BorderWidth = 5;
			NoButton.Checked = false;
			NoButton.HoverBorderColor = Color.White;
			NoButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			NoButton.HoverEndColor = Color.Gray;
			NoButton.HoverFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			NoButton.HoverForeColor = Color.White;
			NoButton.HoverStartColor = Color.Silver;
			NoButton.Location = new Point(275, 6);
			NoButton.Name = "NoButton";
			NoButton.NormalBorderColor = Color.White;
			NoButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			NoButton.NormalEndColor = Color.Gray;
			NoButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			NoButton.NormalForeColor = Color.White;
			NoButton.NormalStartColor = Color.Gray;
			NoButton.PressedBorderColor = Color.Gray;
			NoButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			NoButton.PressedEndColor = Color.Black;
			NoButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			NoButton.PressedForeColor = Color.Black;
			NoButton.PressedStartColor = Color.White;
			NoButton.Size = new Size(130, 36);
			NoButton.TabIndex = 2;
			NoButton.Text = "&No";
			NoButton.UseVisualStyleBackColor = true;
			// 
			// YesButton
			// 
			YesButton.BorderWidth = 5;
			YesButton.Checked = false;
			YesButton.HoverBorderColor = Color.White;
			YesButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			YesButton.HoverEndColor = Color.Gray;
			YesButton.HoverFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			YesButton.HoverForeColor = Color.White;
			YesButton.HoverStartColor = Color.Silver;
			YesButton.Location = new Point(140, 6);
			YesButton.Name = "YesButton";
			YesButton.NormalBorderColor = Color.White;
			YesButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			YesButton.NormalEndColor = Color.Gray;
			YesButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			YesButton.NormalForeColor = Color.White;
			YesButton.NormalStartColor = Color.Gray;
			YesButton.PressedBorderColor = Color.Gray;
			YesButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			YesButton.PressedEndColor = Color.Black;
			YesButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			YesButton.PressedForeColor = Color.Black;
			YesButton.PressedStartColor = Color.White;
			YesButton.Size = new Size(130, 36);
			YesButton.TabIndex = 1;
			YesButton.Text = "&Yes";
			YesButton.UseVisualStyleBackColor = true;
			// 
			// OkButton
			// 
			OkButton.BorderWidth = 5;
			OkButton.Checked = false;
			OkButton.HoverBorderColor = Color.Lime;
			OkButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			OkButton.HoverEndColor = Color.Gray;
			OkButton.HoverFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			OkButton.HoverForeColor = Color.White;
			OkButton.HoverStartColor = Color.Gray;
			OkButton.ImageAlign = ContentAlignment.MiddleLeft;
			OkButton.Location = new Point(5, 6);
			OkButton.Name = "OkButton";
			OkButton.NormalBorderColor = Color.White;
			OkButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			OkButton.NormalEndColor = Color.Gray;
			OkButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			OkButton.NormalForeColor = Color.White;
			OkButton.NormalStartColor = Color.Gray;
			OkButton.PressedBorderColor = Color.FromArgb(0, 64, 0);
			OkButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			OkButton.PressedEndColor = Color.FromArgb(0, 192, 0);
			OkButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			OkButton.PressedForeColor = Color.White;
			OkButton.PressedStartColor = Color.FromArgb(0, 64, 0);
			OkButton.Size = new Size(130, 36);
			OkButton.TabIndex = 0;
			OkButton.Text = "&OK";
			OkButton.UseVisualStyleBackColor = true;
			// 
			// StandardMessageBox
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1092, 254);
			ControlBox = false;
			Controls.Add(ContainerPanel);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			KeyPreview = true;
			Margin = new Padding(4, 5, 4, 5);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "StandardMessageBox";
			Padding = new Padding(5, 6, 5, 6);
			ShowIcon = false;
			StartPosition = FormStartPosition.CenterScreen;
			ContainerPanel.ResumeLayout(false);
			ContainerPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)IconImage).EndInit();
			TopPanel.ResumeLayout(false);
			ButtonPanel.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
		private ImageList StandardIcons;
		private Panel ContainerPanel;
		private PictureBox IconImage;
		private Panel TopPanel;
		private AdvancedLabel CaptionLabel;
		private AdvancedLabel MessageLabel;
		private Panel ButtonPanel;
		private AIButton CloseButton;
		private AIButton TryContinueButton;
		private AIButton IgnoreButton;
		private AIButton RetryButton;
		private AIButton AbortButton;
		private AIButton NoButton;
		private AIButton YesButton;
		private AIButton OkButton;
	}
}