namespace Adaptive.Intelligence.Shared.UI
{
    partial class PasswordTextBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			ViewHideButton = new AIButton();
			PasswordText = new TextBox();
			SuspendLayout();
			// 
			// ViewHideButton
			// 
			ViewHideButton.BorderWidth = 1;
			ViewHideButton.Checked = false;
			ViewHideButton.Dock = DockStyle.Right;
			ViewHideButton.HoverBorderColor = Color.Gray;
			ViewHideButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			ViewHideButton.HoverEndColor = Color.FromArgb(224, 224, 224);
			ViewHideButton.HoverFont = new Font("Segoe UI", 9.75F);
			ViewHideButton.HoverForeColor = Color.Black;
			ViewHideButton.HoverStartColor = Color.FromArgb(218, 194, 204);
			ViewHideButton.Image = Properties.Resources.ViewItem;
			ViewHideButton.Location = new Point(252, 0);
			ViewHideButton.Name = "ViewHideButton";
			ViewHideButton.NormalBorderColor = Color.Gray;
			ViewHideButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			ViewHideButton.NormalEndColor = Color.Silver;
			ViewHideButton.NormalFont = new Font("Segoe UI", 9.75F);
			ViewHideButton.NormalForeColor = Color.Black;
			ViewHideButton.NormalStartColor = Color.FromArgb(248, 248, 248);
			ViewHideButton.PressedBorderColor = Color.Gray;
			ViewHideButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
			ViewHideButton.PressedEndColor = Color.FromArgb(174, 45, 61);
			ViewHideButton.PressedFont = new Font("Segoe UI", 9.75F);
			ViewHideButton.PressedForeColor = Color.White;
			ViewHideButton.PressedStartColor = Color.Gray;
			ViewHideButton.Size = new Size(32, 32);
			ViewHideButton.TabIndex = 0;
			ViewHideButton.TabStop = false;
			ViewHideButton.UseVisualStyleBackColor = true;
			// 
			// PasswordText
			// 
			PasswordText.Dock = DockStyle.Fill;
			PasswordText.Location = new Point(0, 0);
			PasswordText.Name = "PasswordText";
			PasswordText.PasswordChar = '*';
			PasswordText.PlaceholderText = "(Password)";
			PasswordText.Size = new Size(252, 25);
			PasswordText.TabIndex = 1;
			// 
			// PasswordTextBox
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(PasswordText);
			Controls.Add(ViewHideButton);
			Name = "PasswordTextBox";
			Size = new Size(284, 32);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private AIButton ViewHideButton;
        private TextBox PasswordText;
    }
}
