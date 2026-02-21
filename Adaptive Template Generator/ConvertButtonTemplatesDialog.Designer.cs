namespace Adaptive.Template.Generator.UI;

partial class ConvertButtonTemplatesDialog
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
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvertButtonTemplatesDialog));
        SelectLabel = new Adaptive.Intelligence.Shared.UI.AdvancedLabel();
        SelectText = new TextBox();
        SelectButton = new Button();
        SaveAsLabel = new Adaptive.Intelligence.Shared.UI.AdvancedLabel();
        SaveAsText = new TextBox();
        SaveAsButton = new Button();
        CloseButton = new Adaptive.Intelligence.Shared.UI.AIButton();
        FromGroup = new GroupBox();
        FromJsonOption = new RadioButton();
        FromOldFormatOption = new RadioButton();
        ToGroup = new GroupBox();
        ToJsonOption = new RadioButton();
        ToOldFormatOption = new RadioButton();
        ConvertButton = new Adaptive.Intelligence.Shared.UI.AIButton();
        FromGroup.SuspendLayout();
        ToGroup.SuspendLayout();
        SuspendLayout();
        // 
        // SelectLabel
        // 
        SelectLabel.AutoSize = true;
        SelectLabel.Font = new Font("Segoe UI", 9.75F);
        SelectLabel.Location = new Point(10, 10);
        SelectLabel.Name = "SelectLabel";
        SelectLabel.Size = new Size(135, 17);
        SelectLabel.TabIndex = 0;
        SelectLabel.TabStop = false;
        SelectLabel.Text = "&Select File To Convert:";
        SelectLabel.TextAlign = ContentAlignment.TopCenter;
        // 
        // SelectText
        // 
        SelectText.Location = new Point(10, 33);
        SelectText.Name = "SelectText";
        SelectText.Size = new Size(475, 25);
        SelectText.TabIndex = 1;
        // 
        // SelectButton
        // 
        SelectButton.Location = new Point(485, 32);
        SelectButton.Name = "SelectButton";
        SelectButton.Size = new Size(27, 27);
        SelectButton.TabIndex = 2;
        SelectButton.Text = "...";
        SelectButton.UseVisualStyleBackColor = true;
        // 
        // SaveAsLabel
        // 
        SaveAsLabel.AutoSize = true;
        SaveAsLabel.Font = new Font("Segoe UI", 9.75F);
        SaveAsLabel.Location = new Point(10, 207);
        SaveAsLabel.Name = "SaveAsLabel";
        SaveAsLabel.Size = new Size(109, 17);
        SaveAsLabel.TabIndex = 5;
        SaveAsLabel.TabStop = false;
        SaveAsLabel.Text = "Save New File &As:";
        SaveAsLabel.TextAlign = ContentAlignment.TopCenter;
        // 
        // SaveAsText
        // 
        SaveAsText.Location = new Point(10, 230);
        SaveAsText.Name = "SaveAsText";
        SaveAsText.Size = new Size(475, 25);
        SaveAsText.TabIndex = 6;
        // 
        // SaveAsButton
        // 
        SaveAsButton.Location = new Point(485, 229);
        SaveAsButton.Name = "SaveAsButton";
        SaveAsButton.Size = new Size(27, 27);
        SaveAsButton.TabIndex = 7;
        SaveAsButton.Text = "...";
        SaveAsButton.UseVisualStyleBackColor = true;
        // 
        // CloseButton
        // 
        CloseButton.BorderWidth = 1;
        CloseButton.Checked = false;
        CloseButton.HoverBorderColor = Color.Gray;
        CloseButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        CloseButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        CloseButton.HoverFont = new Font("Segoe UI", 9.75F);
        CloseButton.HoverForeColor = Color.Black;
        CloseButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        CloseButton.Location = new Point(392, 281);
        CloseButton.Name = "CloseButton";
        CloseButton.NormalBorderColor = Color.Gray;
        CloseButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        CloseButton.NormalEndColor = Color.Silver;
        CloseButton.NormalFont = new Font("Segoe UI", 9.75F);
        CloseButton.NormalForeColor = Color.Black;
        CloseButton.NormalStartColor = Color.FromArgb(248, 248, 248);
        CloseButton.PressedBorderColor = Color.Gray;
        CloseButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        CloseButton.PressedEndColor = Color.FromArgb(174, 45, 61);
        CloseButton.PressedFont = new Font("Segoe UI", 9.75F);
        CloseButton.PressedForeColor = Color.White;
        CloseButton.PressedStartColor = Color.Gray;
        CloseButton.Size = new Size(120, 32);
        CloseButton.TabIndex = 9;
        CloseButton.Text = "C&lose";
        CloseButton.UseVisualStyleBackColor = true;
        // 
        // FromGroup
        // 
        FromGroup.Controls.Add(FromJsonOption);
        FromGroup.Controls.Add(FromOldFormatOption);
        FromGroup.Location = new Point(10, 64);
        FromGroup.Name = "FromGroup";
        FromGroup.Size = new Size(500, 60);
        FromGroup.TabIndex = 3;
        FromGroup.TabStop = false;
        FromGroup.Text = "Convert &From:";
        // 
        // FromJsonOption
        // 
        FromJsonOption.AutoSize = true;
        FromJsonOption.Location = new Point(108, 25);
        FromJsonOption.Name = "FromJsonOption";
        FromJsonOption.Size = new Size(103, 21);
        FromJsonOption.TabIndex = 1;
        FromJsonOption.Text = "JSON Format";
        FromJsonOption.UseVisualStyleBackColor = true;
        // 
        // FromOldFormatOption
        // 
        FromOldFormatOption.AutoSize = true;
        FromOldFormatOption.Checked = true;
        FromOldFormatOption.Location = new Point(10, 25);
        FromOldFormatOption.Name = "FromOldFormatOption";
        FromOldFormatOption.Size = new Size(92, 21);
        FromOldFormatOption.TabIndex = 0;
        FromOldFormatOption.TabStop = true;
        FromOldFormatOption.Text = "Old Format";
        FromOldFormatOption.UseVisualStyleBackColor = true;
        // 
        // ToGroup
        // 
        ToGroup.Controls.Add(ToJsonOption);
        ToGroup.Controls.Add(ToOldFormatOption);
        ToGroup.Location = new Point(10, 130);
        ToGroup.Name = "ToGroup";
        ToGroup.Size = new Size(500, 60);
        ToGroup.TabIndex = 4;
        ToGroup.TabStop = false;
        ToGroup.Text = "Convert &To:";
        // 
        // ToJsonOption
        // 
        ToJsonOption.AutoSize = true;
        ToJsonOption.Checked = true;
        ToJsonOption.Location = new Point(108, 25);
        ToJsonOption.Name = "ToJsonOption";
        ToJsonOption.Size = new Size(103, 21);
        ToJsonOption.TabIndex = 1;
        ToJsonOption.TabStop = true;
        ToJsonOption.Text = "JSON Format";
        ToJsonOption.UseVisualStyleBackColor = true;
        // 
        // ToOldFormatOption
        // 
        ToOldFormatOption.AutoSize = true;
        ToOldFormatOption.Location = new Point(10, 25);
        ToOldFormatOption.Name = "ToOldFormatOption";
        ToOldFormatOption.Size = new Size(92, 21);
        ToOldFormatOption.TabIndex = 0;
        ToOldFormatOption.Text = "Old Format";
        ToOldFormatOption.UseVisualStyleBackColor = true;
        // 
        // ConvertButton
        // 
        ConvertButton.BorderWidth = 1;
        ConvertButton.Checked = false;
        ConvertButton.Enabled = false;
        ConvertButton.HoverBorderColor = Color.Gray;
        ConvertButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        ConvertButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        ConvertButton.HoverFont = new Font("Segoe UI", 9.75F);
        ConvertButton.HoverForeColor = Color.Black;
        ConvertButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        ConvertButton.Location = new Point(266, 281);
        ConvertButton.Name = "ConvertButton";
        ConvertButton.NormalBorderColor = Color.Gray;
        ConvertButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        ConvertButton.NormalEndColor = Color.Silver;
        ConvertButton.NormalFont = new Font("Segoe UI", 9.75F);
        ConvertButton.NormalForeColor = Color.Black;
        ConvertButton.NormalStartColor = Color.FromArgb(248, 248, 248);
        ConvertButton.PressedBorderColor = Color.Gray;
        ConvertButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        ConvertButton.PressedEndColor = Color.FromArgb(174, 45, 61);
        ConvertButton.PressedFont = new Font("Segoe UI", 9.75F);
        ConvertButton.PressedForeColor = Color.White;
        ConvertButton.PressedStartColor = Color.Gray;
        ConvertButton.Size = new Size(120, 32);
        ConvertButton.TabIndex = 8;
        ConvertButton.Text = "Con&vert";
        ConvertButton.UseVisualStyleBackColor = true;
        // 
        // ConvertButtonTemplatesDialog
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = CloseButton;
        ClientSize = new Size(518, 319);
        ControlBox = false;
        Controls.Add(ConvertButton);
        Controls.Add(ToGroup);
        Controls.Add(FromGroup);
        Controls.Add(CloseButton);
        Controls.Add(SaveAsButton);
        Controls.Add(SaveAsText);
        Controls.Add(SaveAsLabel);
        Controls.Add(SelectButton);
        Controls.Add(SelectText);
        Controls.Add(SelectLabel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = (Icon)resources.GetObject("$this.Icon");
        KeyPreview = true;
        Name = "ConvertButtonTemplatesDialog";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Convert Button Template Files";
        FromGroup.ResumeLayout(false);
        FromGroup.PerformLayout();
        ToGroup.ResumeLayout(false);
        ToGroup.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Intelligence.Shared.UI.AdvancedLabel SelectLabel;
    private TextBox SelectText;
    private Button SelectButton;
    private Intelligence.Shared.UI.AdvancedLabel SaveAsLabel;
    private TextBox SaveAsText;
    private Button SaveAsButton;
    private Intelligence.Shared.UI.AIButton CloseButton;
    private GroupBox FromGroup;
    private RadioButton FromJsonOption;
    private RadioButton FromOldFormatOption;
    private GroupBox ToGroup;
    private RadioButton ToJsonOption;
    private RadioButton ToOldFormatOption;
    private Intelligence.Shared.UI.AIButton ConvertButton;
}