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
        TestContainer = new Panel();
        TestButton = new Adaptive.Intelligence.Shared.UI.Controls.TemplatedButton();
        NormalEditor = new StateTemplateEditorControl();
        NormalSplitter = new Splitter();
        HoverEditor = new StateTemplateEditorControl();
        HoverSplitter = new Splitter();
        PressedEditor = new StateTemplateEditorControl();
        PressedSplitter = new Splitter();
        DisabledEditor = new StateTemplateEditorControl();
        DisabledSplitter = new Splitter();
        CheckedEditor = new StateTemplateEditorControl();
        OptionsPanel = new Panel();
        DisabledCheck = new CheckBox();
        CheckedCheck = new CheckBox();
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
        // TestButton
        // 
        TestButton.Checked = false;
        TestButton.Dock = DockStyle.Fill;
        TestButton.Location = new Point(5, 5);
        TestButton.Name = "TestButton";
        TestButton.Size = new Size(1014, 41);
        TestButton.TabIndex = 0;
        TestButton.Text = "Test Button";
        TestButton.UseVisualStyleBackColor = true;
        // 
        // NormalEditor
        // 
        NormalEditor.ButtonState = Intelligence.Shared.UI.ButtonState.Normal;
        NormalEditor.Dock = DockStyle.Left;
        NormalEditor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        NormalEditor.Location = new Point(0, 0);
        NormalEditor.Margin = new Padding(48, 22, 48, 22);
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
        HoverEditor.ButtonState = Intelligence.Shared.UI.ButtonState.Hover;
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
        PressedEditor.ButtonState = Intelligence.Shared.UI.ButtonState.Pressed;
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
        DisabledEditor.ButtonState = Intelligence.Shared.UI.ButtonState.Disabled;
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
        DisabledSplitter.Size = new Size(9, 549);
        DisabledSplitter.TabIndex = 8;
        DisabledSplitter.TabStop = false;
        // 
        // CheckedEditor
        // 
        CheckedEditor.ButtonState = Intelligence.Shared.UI.ButtonState.Checked;
        CheckedEditor.Dock = DockStyle.Fill;
        CheckedEditor.Font = new Font("Segoe UI", 9.75F);
        CheckedEditor.Location = new Point(938, 0);
        CheckedEditor.Name = "CheckedEditor";
        CheckedEditor.Size = new Size(86, 549);
        CheckedEditor.TabIndex = 9;
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
    private Intelligence.Shared.UI.Controls.TemplatedButton TestButton;
    private Panel OptionsPanel;
    private CheckBox CheckedCheck;
    private CheckBox DisabledCheck;
}
