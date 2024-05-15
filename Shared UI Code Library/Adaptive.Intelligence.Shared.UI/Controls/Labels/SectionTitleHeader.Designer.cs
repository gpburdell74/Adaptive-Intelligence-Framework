namespace Adaptive.Intelligence.Shared.UI
{
	partial class SectionTitleHeader
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
			DividerLine = new LineControl();
			TitleLabel = new AdvancedLabel();
			SuspendLayout();
			// 
			// DividerLine
			// 
			DividerLine.BevelBottomColor = SystemColors.ControlLight;
			DividerLine.BevelTopColor = SystemColors.ControlDark;
			DividerLine.Direction = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			DividerLine.Dock = DockStyle.Bottom;
			DividerLine.EndColor = Color.AliceBlue;
			DividerLine.LineWidth = 1;
			DividerLine.Location = new Point(0, 19);
			DividerLine.Margin = new Padding(4);
			DividerLine.Mode = LineControlMode.Line;
			DividerLine.Name = "DividerLine";
			DividerLine.Orientation = LineControlOrientation.Horizontal;
			DividerLine.Size = new Size(400, 1);
			DividerLine.StartColor = Color.DodgerBlue;
			DividerLine.TabIndex = 1;
			// 
			// TitleLabel
			// 
			TitleLabel.Dock = DockStyle.Fill;
			TitleLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			TitleLabel.ForeColor = Color.DodgerBlue;
			TitleLabel.Location = new Point(0, 0);
			TitleLabel.Margin = new Padding(0);
			TitleLabel.Name = "TitleLabel";
			TitleLabel.Size = new Size(400, 19);
			TitleLabel.TabIndex = 2;
			TitleLabel.TabStop = false;
			TitleLabel.Text = "(Header)";
			TitleLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// SectionTitleHeader
			// 
			AutoScaleDimensions = new SizeF(8F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(TitleLabel);
			Controls.Add(DividerLine);
			Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			Margin = new Padding(4);
			Name = "SectionTitleHeader";
			Size = new Size(400, 20);
			ResumeLayout(false);
		}


		#endregion

		private LineControl DividerLine;
		private AdvancedLabel TitleLabel;
	}
}
