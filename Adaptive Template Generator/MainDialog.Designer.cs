namespace Adaptive.Template.Generator.UI;

partial class MainDialog
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components;

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        Toolbar = new Adaptive.Intelligence.Shared.UI.GradientPanel();
        SaveAsButton = new Adaptive.Intelligence.Shared.UI.AIButton();
        SaveButton = new Adaptive.Intelligence.Shared.UI.AIButton();
        OpenButton = new Adaptive.Intelligence.Shared.UI.AIButton();
        NewButton = new Adaptive.Intelligence.Shared.UI.AIButton();
        TemplateEditor = new ButtonTemplateEditorControl();
        Toolbar.SuspendLayout();
        SuspendLayout();
        // 
        // Toolbar
        // 
        Toolbar.Controls.Add(SaveAsButton);
        Toolbar.Controls.Add(SaveButton);
        Toolbar.Controls.Add(OpenButton);
        Toolbar.Controls.Add(NewButton);
        Toolbar.Dock = DockStyle.Top;
        Toolbar.Location = new Point(0, 0);
        Toolbar.Name = "Toolbar";
        Toolbar.Size = new Size(1092, 59);
        Toolbar.TabIndex = 0;
        // 
        // SaveAsButton
        // 
        SaveAsButton.BorderWidth = 1;
        SaveAsButton.Checked = false;
        SaveAsButton.Font = new Font("Segoe UI", 9F);
        SaveAsButton.HoverBorderColor = Color.Gray;
        SaveAsButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        SaveAsButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        SaveAsButton.HoverFont = new Font("Segoe UI", 9.75F);
        SaveAsButton.HoverForeColor = Color.Black;
        SaveAsButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        SaveAsButton.Location = new Point(389, 5);
        SaveAsButton.Name = "SaveAsButton";
        SaveAsButton.NormalBorderColor = Color.Gray;
        SaveAsButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        SaveAsButton.NormalEndColor = Color.Silver;
        SaveAsButton.NormalFont = new Font("Segoe UI", 9.75F);
        SaveAsButton.NormalForeColor = Color.Black;
        SaveAsButton.NormalStartColor = Color.FromArgb(248, 248, 248);
        SaveAsButton.PressedBorderColor = Color.Gray;
        SaveAsButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        SaveAsButton.PressedEndColor = Color.FromArgb(174, 45, 61);
        SaveAsButton.PressedFont = new Font("Segoe UI", 9.75F);
        SaveAsButton.PressedForeColor = Color.White;
        SaveAsButton.PressedStartColor = Color.Gray;
        SaveAsButton.Size = new Size(122, 48);
        SaveAsButton.TabIndex = 3;
        SaveAsButton.Text = "Save Template As...";
        SaveAsButton.UseVisualStyleBackColor = true;
        SaveAsButton.UseWaitCursor = true;
        SaveAsButton.Visible = false;
        // 
        // SaveButton
        // 
        SaveButton.BorderWidth = 1;
        SaveButton.Checked = false;
        SaveButton.Font = new Font("Segoe UI", 9F);
        SaveButton.HoverBorderColor = Color.Gray;
        SaveButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        SaveButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        SaveButton.HoverFont = new Font("Segoe UI", 9.75F);
        SaveButton.HoverForeColor = Color.Black;
        SaveButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        SaveButton.Location = new Point(261, 5);
        SaveButton.Name = "SaveButton";
        SaveButton.NormalBorderColor = Color.Gray;
        SaveButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        SaveButton.NormalEndColor = Color.Silver;
        SaveButton.NormalFont = new Font("Segoe UI", 9.75F);
        SaveButton.NormalForeColor = Color.Black;
        SaveButton.NormalStartColor = Color.FromArgb(248, 248, 248);
        SaveButton.PressedBorderColor = Color.Gray;
        SaveButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        SaveButton.PressedEndColor = Color.FromArgb(174, 45, 61);
        SaveButton.PressedFont = new Font("Segoe UI", 9.75F);
        SaveButton.PressedForeColor = Color.White;
        SaveButton.PressedStartColor = Color.Gray;
        SaveButton.Size = new Size(122, 48);
        SaveButton.TabIndex = 2;
        SaveButton.Text = "Save Template";
        SaveButton.UseVisualStyleBackColor = true;
        SaveButton.Visible = false;
        // 
        // OpenButton
        // 
        OpenButton.BorderWidth = 1;
        OpenButton.Checked = false;
        OpenButton.Font = new Font("Segoe UI", 9F);
        OpenButton.HoverBorderColor = Color.Gray;
        OpenButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        OpenButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        OpenButton.HoverFont = new Font("Segoe UI", 9.75F);
        OpenButton.HoverForeColor = Color.Black;
        OpenButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        OpenButton.Location = new Point(133, 5);
        OpenButton.Name = "OpenButton";
        OpenButton.NormalBorderColor = Color.Gray;
        OpenButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        OpenButton.NormalEndColor = Color.Silver;
        OpenButton.NormalFont = new Font("Segoe UI", 9.75F);
        OpenButton.NormalForeColor = Color.Black;
        OpenButton.NormalStartColor = Color.FromArgb(248, 248, 248);
        OpenButton.PressedBorderColor = Color.Gray;
        OpenButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        OpenButton.PressedEndColor = Color.FromArgb(174, 45, 61);
        OpenButton.PressedFont = new Font("Segoe UI", 9.75F);
        OpenButton.PressedForeColor = Color.White;
        OpenButton.PressedStartColor = Color.Gray;
        OpenButton.Size = new Size(122, 48);
        OpenButton.TabIndex = 1;
        OpenButton.Text = "&Open Template...";
        OpenButton.UseVisualStyleBackColor = true;
        // 
        // NewButton
        // 
        NewButton.BorderWidth = 1;
        NewButton.Checked = false;
        NewButton.Font = new Font("Segoe UI", 9F);
        NewButton.HoverBorderColor = Color.Gray;
        NewButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        NewButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        NewButton.HoverFont = new Font("Segoe UI", 9.75F);
        NewButton.HoverForeColor = Color.Black;
        NewButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        NewButton.Location = new Point(5, 5);
        NewButton.Name = "NewButton";
        NewButton.NormalBorderColor = Color.Gray;
        NewButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        NewButton.NormalEndColor = Color.Silver;
        NewButton.NormalFont = new Font("Segoe UI", 9.75F);
        NewButton.NormalForeColor = Color.Black;
        NewButton.NormalStartColor = Color.FromArgb(248, 248, 248);
        NewButton.PressedBorderColor = Color.Gray;
        NewButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        NewButton.PressedEndColor = Color.FromArgb(174, 45, 61);
        NewButton.PressedFont = new Font("Segoe UI", 9.75F);
        NewButton.PressedForeColor = Color.White;
        NewButton.PressedStartColor = Color.Gray;
        NewButton.Size = new Size(122, 48);
        NewButton.TabIndex = 0;
        NewButton.Text = "&New Template";
        NewButton.UseVisualStyleBackColor = true;
        // 
        // TemplateEditor
        // 
        TemplateEditor.Dock = DockStyle.Fill;
        TemplateEditor.Font = new Font("Segoe UI", 9.75F);
        TemplateEditor.Location = new Point(0, 59);
        TemplateEditor.Name = "TemplateEditor";
        TemplateEditor.Size = new Size(1092, 572);
        TemplateEditor.TabIndex = 1;
        TemplateEditor.Visible = false;
        // 
        // MainDialog
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(1092, 631);
        Controls.Add(TemplateEditor);
        Controls.Add(Toolbar);
        KeyPreview = true;
        Name = "MainDialog";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Adaptive Button Template Editor";
        Toolbar.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Intelligence.Shared.UI.GradientPanel Toolbar;
    private Intelligence.Shared.UI.AIButton SaveAsButton;
    private Intelligence.Shared.UI.AIButton SaveButton;
    private Intelligence.Shared.UI.AIButton OpenButton;
    private Intelligence.Shared.UI.AIButton NewButton;
    private ButtonTemplateEditorControl TemplateEditor;
}
