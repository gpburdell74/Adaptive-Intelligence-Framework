using Adaptive.Intelligence.Shared.UI;
using Adaptive.Intelligence.Shared.UI.TemplatedControls;

namespace Adaptive.Template.Generator.UI;

partial class ButtonTemplateEditorControl
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
        var buttonTemplate2 = new ButtonTemplate();
        var buttonStateTemplate6 = new Intelligence.Shared.UI.TemplatedControls.States.StateTemplate();
        var fontTemplate6 = new FontTemplate();
        var buttonStateTemplate7 = new Intelligence.Shared.UI.TemplatedControls.States.StateTemplate();
        var fontTemplate7 = new FontTemplate();
        var buttonStateTemplate8 = new Intelligence.Shared.UI.TemplatedControls.States.StateTemplate();
        var fontTemplate8 = new FontTemplate();
        var buttonStateTemplate9 = new Intelligence.Shared.UI.TemplatedControls.States.StateTemplate();
        var fontTemplate9 = new FontTemplate();
        var buttonStateTemplate10 = new Intelligence.Shared.UI.TemplatedControls.States.StateTemplate();
        var fontTemplate10 = new FontTemplate();
        TestContainer = new Panel();
        OptionsPanel = new Panel();
        CheckedCheck = new CheckBox();
        DisabledCheck = new CheckBox();
        TestButton = new TemplatedButton();
        NormalEditor = new StateTemplateEditorControl();
        NormalSplitter = new Splitter();
        HoverEditor = new StateTemplateEditorControl();
        HoverSplitter = new Splitter();
        PressedEditor = new StateTemplateEditorControl();
        PressedSplitter = new Splitter();
        DisabledEditor = new StateTemplateEditorControl();
        DisabledSplitter = new Splitter();
        CheckedEditor = new StateTemplateEditorControl();
        TestContainer.SuspendLayout();
        OptionsPanel.SuspendLayout();
        SuspendLayout();
        // 
        // TestContainer
        // 
        TestContainer.Controls.Add(OptionsPanel);
        TestContainer.Controls.Add(TestButton);
        TestContainer.Dock = DockStyle.Bottom;
        TestContainer.Location = new Point(0, 549);
        TestContainer.Name = "TestContainer";
        TestContainer.Padding = new Padding(5);
        TestContainer.Size = new Size(1024, 51);
        TestContainer.TabIndex = 0;
        // 
        // OptionsPanel
        // 
        OptionsPanel.Controls.Add(CheckedCheck);
        OptionsPanel.Controls.Add(DisabledCheck);
        OptionsPanel.Dock = DockStyle.Right;
        OptionsPanel.Location = new Point(851, 5);
        OptionsPanel.Name = "OptionsPanel";
        OptionsPanel.Size = new Size(168, 41);
        OptionsPanel.TabIndex = 2;
        // 
        // CheckedCheck
        // 
        CheckedCheck.AutoSize = true;
        CheckedCheck.Location = new Point(89, 5);
        CheckedCheck.Name = "CheckedCheck";
        CheckedCheck.Size = new Size(76, 21);
        CheckedCheck.TabIndex = 1;
        CheckedCheck.Text = "Checked";
        CheckedCheck.UseVisualStyleBackColor = true;
        // 
        // DisabledCheck
        // 
        DisabledCheck.AutoSize = true;
        DisabledCheck.Location = new Point(5, 5);
        DisabledCheck.Name = "DisabledCheck";
        DisabledCheck.Size = new Size(78, 21);
        DisabledCheck.TabIndex = 0;
        DisabledCheck.Text = "Disabled";
        DisabledCheck.UseVisualStyleBackColor = true;
        // 
        // TestButton
        // 
        TestButton.Checked = false;
        TestButton.Dock = DockStyle.Fill;
        TestButton.Location = new Point(5, 5);
        TestButton.Name = "TestButton";
        TestButton.Size = new Size(1014, 41);
        TestButton.TabIndex = 0;
        buttonStateTemplate6.BorderColor = Color.Gray;
        buttonStateTemplate6.EndColor = Color.FromArgb(128, 128, 128);
        fontTemplate6.FontFamily = "Segoe UI";
        fontTemplate6.GdiCharSet = 1;
        fontTemplate6.GdiVerticalFont = false;
        fontTemplate6.Size = 9.75F;
        fontTemplate6.Style = FontStyle.Regular;
        fontTemplate6.Unit = GraphicsUnit.Point;
        buttonStateTemplate6.Font = fontTemplate6;
        buttonStateTemplate6.ForeColor = Color.White;
        buttonStateTemplate6.Mode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        buttonStateTemplate6.StartColor = Color.FromArgb(64, 64, 64);
        buttonStateTemplate6.TextImageRelation = TextImageRelation.ImageBeforeText;
        buttonTemplate2.Checked = buttonStateTemplate6;
        buttonStateTemplate7.BorderColor = Color.Gray;
        buttonStateTemplate7.EndColor = Color.FromArgb(224, 224, 224);
        fontTemplate7.FontFamily = "Segoe UI";
        fontTemplate7.GdiCharSet = 1;
        fontTemplate7.GdiVerticalFont = false;
        fontTemplate7.Size = 9.75F;
        fontTemplate7.Style = FontStyle.Regular;
        fontTemplate7.Unit = GraphicsUnit.Point;
        buttonStateTemplate7.Font = fontTemplate7;
        buttonStateTemplate7.ForeColor = Color.Gray;
        buttonStateTemplate7.Mode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        buttonStateTemplate7.StartColor = Color.FromArgb(192, 192, 192);
        buttonStateTemplate7.TextImageRelation = TextImageRelation.ImageBeforeText;
        buttonTemplate2.Disabled = buttonStateTemplate7;
        buttonStateTemplate8.BorderColor = Color.Gray;
        buttonStateTemplate8.EndColor = Color.FromArgb(224, 224, 224);
        fontTemplate8.FontFamily = "Segoe UI";
        fontTemplate8.GdiCharSet = 1;
        fontTemplate8.GdiVerticalFont = false;
        fontTemplate8.Size = 9.75F;
        fontTemplate8.Style = FontStyle.Regular;
        fontTemplate8.Unit = GraphicsUnit.Point;
        buttonStateTemplate8.Font = fontTemplate8;
        buttonStateTemplate8.ForeColor = Color.Black;
        buttonStateTemplate8.Mode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        buttonStateTemplate8.StartColor = Color.FromArgb(218, 194, 204);
        buttonStateTemplate8.TextImageRelation = TextImageRelation.ImageBeforeText;
        buttonTemplate2.Hover = buttonStateTemplate8;
        buttonStateTemplate9.BorderColor = Color.Gray;
        buttonStateTemplate9.EndColor = Color.Silver;
        fontTemplate9.FontFamily = "Segoe UI";
        fontTemplate9.GdiCharSet = 1;
        fontTemplate9.GdiVerticalFont = false;
        fontTemplate9.Size = 9.75F;
        fontTemplate9.Style = FontStyle.Regular;
        fontTemplate9.Unit = GraphicsUnit.Point;
        buttonStateTemplate9.Font = fontTemplate9;
        buttonStateTemplate9.ForeColor = Color.Black;
        buttonStateTemplate9.Mode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        buttonStateTemplate9.StartColor = Color.FromArgb(248, 248, 248);
        buttonStateTemplate9.TextImageRelation = TextImageRelation.ImageBeforeText;
        buttonTemplate2.Normal = buttonStateTemplate9;
        buttonStateTemplate10.BorderColor = Color.Gray;
        buttonStateTemplate10.EndColor = Color.FromArgb(174, 45, 61);
        fontTemplate10.FontFamily = "Segoe UI";
        fontTemplate10.GdiCharSet = 1;
        fontTemplate10.GdiVerticalFont = false;
        fontTemplate10.Size = 9.75F;
        fontTemplate10.Style = FontStyle.Regular;
        fontTemplate10.Unit = GraphicsUnit.Point;
        buttonStateTemplate10.Font = fontTemplate10;
        buttonStateTemplate10.ForeColor = Color.White;
        buttonStateTemplate10.Mode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        buttonStateTemplate10.StartColor = Color.Gray;
        buttonStateTemplate10.TextImageRelation = TextImageRelation.ImageBeforeText;
        buttonTemplate2.Pressed = buttonStateTemplate10;
        TestButton.Template = buttonTemplate2;
        TestButton.TemplateFromFile = null;
        TestButton.TemplateJson = null;
        TestButton.Text = "Test Button";
        TestButton.UseVisualStyleBackColor = true;
        // 
        // NormalEditor
        // 
        NormalEditor.ButtonState = ControlStates.Normal;
        NormalEditor.Dock = DockStyle.Left;
        NormalEditor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        NormalEditor.Location = new Point(0, 0);
        NormalEditor.Name = "NormalEditor";
        NormalEditor.Size = new Size(230, 549);
        NormalEditor.TabIndex = 1;
        // 
        // NormalSplitter
        // 
        NormalSplitter.Location = new Point(230, 0);
        NormalSplitter.Name = "NormalSplitter";
        NormalSplitter.Size = new Size(3, 549);
        NormalSplitter.TabIndex = 2;
        NormalSplitter.TabStop = false;
        // 
        // HoverEditor
        // 
        HoverEditor.ButtonState = ControlStates.Hover;
        HoverEditor.Dock = DockStyle.Left;
        HoverEditor.Font = new Font("Segoe UI", 9.75F);
        HoverEditor.Location = new Point(233, 0);
        HoverEditor.Name = "HoverEditor";
        HoverEditor.Size = new Size(230, 549);
        HoverEditor.TabIndex = 3;
        // 
        // HoverSplitter
        // 
        HoverSplitter.Location = new Point(463, 0);
        HoverSplitter.Name = "HoverSplitter";
        HoverSplitter.Size = new Size(3, 549);
        HoverSplitter.TabIndex = 4;
        HoverSplitter.TabStop = false;
        // 
        // PressedEditor
        // 
        PressedEditor.ButtonState = ControlStates.Pressed;
        PressedEditor.Dock = DockStyle.Left;
        PressedEditor.Font = new Font("Segoe UI", 9.75F);
        PressedEditor.Location = new Point(466, 0);
        PressedEditor.Name = "PressedEditor";
        PressedEditor.Size = new Size(230, 549);
        PressedEditor.TabIndex = 5;
        // 
        // PressedSplitter
        // 
        PressedSplitter.Location = new Point(696, 0);
        PressedSplitter.Name = "PressedSplitter";
        PressedSplitter.Size = new Size(3, 549);
        PressedSplitter.TabIndex = 6;
        PressedSplitter.TabStop = false;
        // 
        // DisabledEditor
        // 
        DisabledEditor.ButtonState = ControlStates.Disabled;
        DisabledEditor.Dock = DockStyle.Left;
        DisabledEditor.Font = new Font("Segoe UI", 9.75F);
        DisabledEditor.Location = new Point(699, 0);
        DisabledEditor.Name = "DisabledEditor";
        DisabledEditor.Size = new Size(230, 549);
        DisabledEditor.TabIndex = 7;
        // 
        // DisabledSplitter
        // 
        DisabledSplitter.Location = new Point(929, 0);
        DisabledSplitter.Name = "DisabledSplitter";
        DisabledSplitter.Size = new Size(3, 549);
        DisabledSplitter.TabIndex = 8;
        DisabledSplitter.TabStop = false;
        // 
        // CheckedEditor
        // 
        CheckedEditor.ButtonState = ControlStates.Checked;
        CheckedEditor.Dock = DockStyle.Fill;
        CheckedEditor.Font = new Font("Segoe UI", 9.75F);
        CheckedEditor.Location = new Point(932, 0);
        CheckedEditor.Name = "CheckedEditor";
        CheckedEditor.Size = new Size(92, 549);
        CheckedEditor.TabIndex = 9;
        // 
        // ButtonTemplateEditorControl
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        Controls.Add(CheckedEditor);
        Controls.Add(DisabledSplitter);
        Controls.Add(DisabledEditor);
        Controls.Add(PressedSplitter);
        Controls.Add(PressedEditor);
        Controls.Add(HoverSplitter);
        Controls.Add(HoverEditor);
        Controls.Add(NormalSplitter);
        Controls.Add(NormalEditor);
        Controls.Add(TestContainer);
        Margin = new Padding(3);
        Name = "ButtonTemplateEditorControl";
        Size = new Size(1024, 600);
        TestContainer.ResumeLayout(false);
        OptionsPanel.ResumeLayout(false);
        OptionsPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel TestContainer;
    private StateTemplateEditorControl NormalEditor;
    private Splitter NormalSplitter;
    private StateTemplateEditorControl HoverEditor;
    private Splitter HoverSplitter;
    private StateTemplateEditorControl PressedEditor;
    private Splitter PressedSplitter;
    private StateTemplateEditorControl DisabledEditor;
    private Splitter DisabledSplitter;
    private StateTemplateEditorControl CheckedEditor;
    private TemplatedButton TestButton;
    private Panel OptionsPanel;
    private CheckBox CheckedCheck;
    private CheckBox DisabledCheck;
}
