using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;

namespace Adaptive.Intelligence.Shared.UI
{
	/// <summary>
	/// Provides a simple control to use as a title or section header.
	/// </summary>
	/// <seealso cref="UserControl" />
	public partial class SectionTitleHeader : UserControl
	{
		#region Private Constants		
		/// <summary>
		/// The default text.
		/// </summary>
		private const string DefaultText = @"(Header)";
		#endregion

		#region Constructor / Dispose Methpds
		/// <summary>
		/// Initializes a new instance of the <see cref="SectionTitleHeader"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public SectionTitleHeader()
		{
			InitializeComponent();
			TitleLabel.Text = DefaultText;
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
		/// Gets or sets the font of the text displayed by the control.
		/// </summary>
		/// <returns>
		/// The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default
		/// is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.
		/// </returns>
		[Description("Gets or sets font settings applied to the control."),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
		 AllowNull(),
		 Category("Appearance")]
		public override Font Font
		{
			get => TitleLabel.Font;
			set
			{
				TitleLabel.Font = value;
				Invalidate();
			}
		}
		/// <summary>
		/// Gets or sets the text associated with this control.
		/// </summary>
		/// <value>
		/// A string containing the text value to be displayed.
		/// </value>
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), SettingsBindable(true),
		 Bindable(true),
		 AllowNull(),
		 Localizable(true),
		 Category("Appearance"),
		 DefaultValue(""),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
		 Description("The text to be displayed in the title section."),
		 Browsable(true)]
		public override string Text
		{
			get => TitleLabel.Text;
			set
			{
				TitleLabel.Text = value;
				OnTextChanged(EventArgs.Empty);
				Invalidate();
			}
		}
		#endregion
	}
}
