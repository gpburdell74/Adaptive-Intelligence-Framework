using Adaptive.Intelligence.Shared.UI;

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
        PropEditor = new PropertyGrid();
        Header = new Adaptive.Intelligence.Shared.UI.SectionTitleHeader();
        SuspendLayout();
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
        Controls.Add(Header);
        Margin = new Padding(3);
        Name = "StateTemplateEditorControl";
        Size = new Size(200, 400);
        
        ResumeLayout(false);
    }

    #endregion

    private PropertyGrid PropEditor;
    private Intelligence.Shared.UI.SectionTitleHeader Header;
}
