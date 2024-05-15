namespace Adaptive.Intelligence.Shared.UI
{
	partial class RatingControl
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RatingControl));
			StarA = new PictureBox();
			StarB = new PictureBox();
			StarC = new PictureBox();
			StarD = new PictureBox();
			StarE = new PictureBox();
			StarImages = new ImageList(components);
			((System.ComponentModel.ISupportInitialize)StarA).BeginInit();
			((System.ComponentModel.ISupportInitialize)StarB).BeginInit();
			((System.ComponentModel.ISupportInitialize)StarC).BeginInit();
			((System.ComponentModel.ISupportInitialize)StarD).BeginInit();
			((System.ComponentModel.ISupportInitialize)StarE).BeginInit();
			SuspendLayout();
			// 
			// StarA
			// 
			StarA.Image = Properties.Resources.DisabledStar;
			StarA.Location = new Point(5, 5);
			StarA.Name = "StarA";
			StarA.Size = new Size(32, 32);
			StarA.TabIndex = 0;
			StarA.TabStop = false;
			// 
			// StarB
			// 
			StarB.Image = Properties.Resources.DisabledStar;
			StarB.Location = new Point(37, 5);
			StarB.Name = "StarB";
			StarB.Size = new Size(32, 32);
			StarB.TabIndex = 1;
			StarB.TabStop = false;
			// 
			// StarC
			// 
			StarC.Image = Properties.Resources.DisabledStar;
			StarC.Location = new Point(69, 5);
			StarC.Name = "StarC";
			StarC.Size = new Size(32, 32);
			StarC.TabIndex = 2;
			StarC.TabStop = false;
			// 
			// StarD
			// 
			StarD.Image = Properties.Resources.DisabledStar;
			StarD.Location = new Point(101, 5);
			StarD.Name = "StarD";
			StarD.Size = new Size(32, 32);
			StarD.TabIndex = 3;
			StarD.TabStop = false;
			// 
			// StarE
			// 
			StarE.Image = Properties.Resources.DisabledStar;
			StarE.Location = new Point(133, 5);
			StarE.Name = "StarE";
			StarE.Size = new Size(32, 32);
			StarE.TabIndex = 4;
			StarE.TabStop = false;
			// 
			// StarImages
			// 
			StarImages.ColorDepth = ColorDepth.Depth32Bit;
			StarImages.ImageStream = (ImageListStreamer)resources.GetObject("StarImages.ImageStream");
			StarImages.TransparentColor = Color.Transparent;
			StarImages.Images.SetKeyName(0, "DisabledStar.png");
			StarImages.Images.SetKeyName(1, "EnabledStar.png");
			// 
			// RatingControl
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(StarE);
			Controls.Add(StarD);
			Controls.Add(StarC);
			Controls.Add(StarB);
			Controls.Add(StarA);
			Margin = new Padding(3);
			MaximumSize = new Size(0, 42);
			MinimumSize = new Size(170, 42);
			Name = "RatingControl";
			Padding = new Padding(5);
			Size = new Size(170, 42);
			((System.ComponentModel.ISupportInitialize)StarA).EndInit();
			((System.ComponentModel.ISupportInitialize)StarB).EndInit();
			((System.ComponentModel.ISupportInitialize)StarC).EndInit();
			((System.ComponentModel.ISupportInitialize)StarD).EndInit();
			((System.ComponentModel.ISupportInitialize)StarE).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private PictureBox StarA;
		private PictureBox StarB;
		private PictureBox StarC;
		private PictureBox StarD;
		private PictureBox StarE;
		private ImageList StarImages;
	}
}
