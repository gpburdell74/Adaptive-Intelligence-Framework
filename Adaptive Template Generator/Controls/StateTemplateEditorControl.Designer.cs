namespace Adaptive.Template.Generator.UI;

partial class StateTemplateEditorControl
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
        PanelContainer = new Adaptive.Intelligence.Shared.UI.GradientPanel();
        ExamplePanel = new Adaptive.Intelligence.Shared.UI.GradientPanel();
        PropEditor = new PropertyGrid();
        Header = new Adaptive.Intelligence.Shared.UI.SectionTitleHeader();
        PanelContainer.SuspendLayout();
        SuspendLayout();
        // 
        // PanelContainer
        // 
        PanelContainer.Controls.Add(ExamplePanel);
        PanelContainer.Dock = DockStyle.Top;
        PanelContainer.Location = new Point(0, 24);
        PanelContainer.Name = "PanelContainer";
        PanelContainer.Padding = new Padding(5);
        PanelContainer.Size = new Size(200, 75);
        PanelContainer.TabIndex = 0;
        // 
        // ExamplePanel
        // 
        ExamplePanel.Dock = DockStyle.Fill;
        ExamplePanel.Location = new Point(5, 5);
        ExamplePanel.Name = "ExamplePanel";
        ExamplePanel.Size = new Size(190, 65);
        ExamplePanel.TabIndex = 0;
        // 
        // PropEditor
        // 
        PropEditor.Dock = DockStyle.Fill;
        PropEditor.Location = new Point(0, 99);
        PropEditor.Name = "PropEditor";
        PropEditor.Size = new Size(200, 301);
        PropEditor.TabIndex = 1;
        // 
        // Header
        // 
        Header.Dock = DockStyle.Top;
        Header.Location = new Point(0, 0);
        Header.Margin = new Padding(48, 23, 48, 23);
        Header.Name = "Header";
        Header.Size = new Size(200, 24);
        Header.TabIndex = 3;
        Header.Text = "State Template";
        // 
        // StateTemplateEditorControl
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        Controls.Add(PropEditor);
        Controls.Add(PanelContainer);
        Controls.Add(Header);
        Margin = new Padding(3);
        Name = "StateTemplateEditorControl";
        Size = new Size(200, 400);
        PanelContainer.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Intelligence.Shared.UI.GradientPanel PanelContainer;
    private PropertyGrid PropEditor;
    private Intelligence.Shared.UI.SectionTitleHeader Header;
    private Intelligence.Shared.UI.GradientPanel ExamplePanel;
}
