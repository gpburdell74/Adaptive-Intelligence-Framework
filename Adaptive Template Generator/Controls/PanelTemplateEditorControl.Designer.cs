using Adaptive.Intelligence.Shared.UI;
using Adaptive.Intelligence.Shared.UI.TemplatedControls;

namespace Adaptive.Template.Generator.UI;

partial class PanelTemplateEditorControl
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>1
    private System.ComponentModel.IContainer components;

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        NormalEditor = new StateTemplateEditorControl();
        NormalSplitter = new Splitter();
        HoverEditor = new StateTemplateEditorControl();
        HoverSplitter = new Splitter();
        DisabledEditor = new StateTemplateEditorControl();
        DisabledSplitter = new Splitter();
        TestPanel = new TemplatedGradientPanel();
        SuspendLayout();
        // 
        // NormalEditor
        // 
        NormalEditor.ButtonState = ControlStates.Normal;
        NormalEditor.Dock = DockStyle.Left;
        NormalEditor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        NormalEditor.Location = new Point(0, 0);
        NormalEditor.Name = "NormalEditor";
        NormalEditor.Size = new Size(230, 600);
        NormalEditor.TabIndex = 1;
        // 
        // NormalSplitter
        // 
        NormalSplitter.Location = new Point(230, 0);
        NormalSplitter.Name = "NormalSplitter";
        NormalSplitter.Size = new Size(3, 600);
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
        HoverEditor.Size = new Size(230, 600);
        HoverEditor.TabIndex = 3;
        // 
        // HoverSplitter
        // 
        HoverSplitter.Location = new Point(463, 0);
        HoverSplitter.Name = "HoverSplitter";
        HoverSplitter.Size = new Size(3, 600);
        HoverSplitter.TabIndex = 4;
        HoverSplitter.TabStop = false;
        // 
        // DisabledEditor
        // 
        DisabledEditor.ButtonState = ControlStates.Disabled;
        DisabledEditor.Dock = DockStyle.Left;
        DisabledEditor.Font = new Font("Segoe UI", 9.75F);
        DisabledEditor.Location = new Point(466, 0);
        DisabledEditor.Name = "DisabledEditor";
        DisabledEditor.Size = new Size(230, 600);
        DisabledEditor.TabIndex = 7;
        // 
        // DisabledSplitter
        // 
        DisabledSplitter.Location = new Point(696, 0);
        DisabledSplitter.Name = "DisabledSplitter";
        DisabledSplitter.Size = new Size(3, 600);
        DisabledSplitter.TabIndex = 8;
        DisabledSplitter.TabStop = false;
        // 
        // TestPanel
        // 
        TestPanel.Dock = DockStyle.Fill;
        TestPanel.Location = new Point(699, 0);
        TestPanel.Name = "TestPanel";
        TestPanel.Size = new Size(325, 600);
        TestPanel.TabIndex = 9;

        // 
        // PanelTemplateEditorControl
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        Controls.Add(TestPanel);
        Controls.Add(DisabledSplitter);
        Controls.Add(DisabledEditor);
        Controls.Add(HoverSplitter);
        Controls.Add(HoverEditor);
        Controls.Add(NormalSplitter);
        Controls.Add(NormalEditor);
        Margin = new Padding(3);
        Name = "PanelTemplateEditorControl";
        Size = new Size(1024, 600);
        ResumeLayout(false);
    }

    #endregion
    private StateTemplateEditorControl NormalEditor;
    private Splitter NormalSplitter;
    private StateTemplateEditorControl HoverEditor;
    private Splitter HoverSplitter;
    private StateTemplateEditorControl DisabledEditor;
    private Splitter DisabledSplitter;
    private TemplatedGradientPanel TestPanel;
}
