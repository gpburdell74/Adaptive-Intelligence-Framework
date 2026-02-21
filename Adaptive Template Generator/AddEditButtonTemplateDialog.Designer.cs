namespace Adaptive.Template.Generator.UI;

partial class AddEditButtonTemplateDialog
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
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditButtonTemplateDialog));
        TemplateEditor = new ButtonTemplateEditorControl();
        SaveCancelPanel = new Panel();
        SavePanel = new Panel();
        SaveButton = new Adaptive.Intelligence.Shared.UI.AIButton();
        SaveAsPanel = new Panel();
        SaveAsButton = new Adaptive.Intelligence.Shared.UI.AIButton();
        ClosePanel = new Panel();
        CloseButton = new Adaptive.Intelligence.Shared.UI.AIButton();
        SaveCancelPanel.SuspendLayout();
        SavePanel.SuspendLayout();
        SaveAsPanel.SuspendLayout();
        ClosePanel.SuspendLayout();
        SuspendLayout();
        // 
        // TemplateEditor
        // 
        TemplateEditor.Dock = DockStyle.Top;
        TemplateEditor.Font = new Font("Segoe UI", 9.75F);
        TemplateEditor.Location = new Point(0, 0);
        TemplateEditor.Name = "TemplateEditor";
        TemplateEditor.Size = new Size(1219, 494);
        TemplateEditor.TabIndex = 0;
        // 
        // SaveCancelPanel
        // 
        SaveCancelPanel.Controls.Add(SavePanel);
        SaveCancelPanel.Controls.Add(SaveAsPanel);
        SaveCancelPanel.Controls.Add(ClosePanel);
        SaveCancelPanel.Dock = DockStyle.Bottom;
        SaveCancelPanel.Location = new Point(0, 493);
        SaveCancelPanel.Name = "SaveCancelPanel";
        SaveCancelPanel.Size = new Size(1219, 42);
        SaveCancelPanel.TabIndex = 1;
        // 
        // SavePanel
        // 
        SavePanel.Controls.Add(SaveButton);
        SavePanel.Dock = DockStyle.Right;
        SavePanel.Location = new Point(769, 0);
        SavePanel.Name = "SavePanel";
        SavePanel.Padding = new Padding(5);
        SavePanel.Size = new Size(150, 42);
        SavePanel.TabIndex = 1;
        // 
        // SaveButton
        // 
        SaveButton.BorderWidth = 1;
        SaveButton.Checked = false;
        SaveButton.Dock = DockStyle.Fill;
        SaveButton.HoverBorderColor = Color.Gray;
        SaveButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        SaveButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        SaveButton.HoverFont = new Font("Segoe UI", 9.75F);
        SaveButton.HoverForeColor = Color.Black;
        SaveButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        SaveButton.Image = (Image)resources.GetObject("SaveButton.Image");
        SaveButton.ImageAlign = ContentAlignment.MiddleLeft;
        SaveButton.Location = new Point(5, 5);
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
        SaveButton.Size = new Size(140, 32);
        SaveButton.TabIndex = 0;
        SaveButton.Text = "Save";
        SaveButton.UseVisualStyleBackColor = true;
        // 
        // SaveAsPanel
        // 
        SaveAsPanel.Controls.Add(SaveAsButton);
        SaveAsPanel.Dock = DockStyle.Right;
        SaveAsPanel.Location = new Point(919, 0);
        SaveAsPanel.Name = "SaveAsPanel";
        SaveAsPanel.Padding = new Padding(5);
        SaveAsPanel.Size = new Size(150, 42);
        SaveAsPanel.TabIndex = 2;
        // 
        // SaveAsButton
        // 
        SaveAsButton.BorderWidth = 1;
        SaveAsButton.Checked = false;
        SaveAsButton.Dock = DockStyle.Fill;
        SaveAsButton.HoverBorderColor = Color.Gray;
        SaveAsButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        SaveAsButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        SaveAsButton.HoverFont = new Font("Segoe UI", 9.75F);
        SaveAsButton.HoverForeColor = Color.Black;
        SaveAsButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        SaveAsButton.Image = (Image)resources.GetObject("SaveAsButton.Image");
        SaveAsButton.ImageAlign = ContentAlignment.MiddleLeft;
        SaveAsButton.Location = new Point(5, 5);
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
        SaveAsButton.Size = new Size(140, 32);
        SaveAsButton.TabIndex = 0;
        SaveAsButton.Text = "Save As...";
        SaveAsButton.UseVisualStyleBackColor = true;
        // 
        // ClosePanel
        // 
        ClosePanel.Controls.Add(CloseButton);
        ClosePanel.Dock = DockStyle.Right;
        ClosePanel.Location = new Point(1069, 0);
        ClosePanel.Name = "ClosePanel";
        ClosePanel.Padding = new Padding(5);
        ClosePanel.Size = new Size(150, 42);
        ClosePanel.TabIndex = 3;
        // 
        // CloseButton
        // 
        CloseButton.BorderWidth = 1;
        CloseButton.Checked = false;
        CloseButton.Dock = DockStyle.Fill;
        CloseButton.HoverBorderColor = Color.Gray;
        CloseButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        CloseButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        CloseButton.HoverFont = new Font("Segoe UI", 9.75F);
        CloseButton.HoverForeColor = Color.Black;
        CloseButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        CloseButton.Image = (Image)resources.GetObject("CloseButton.Image");
        CloseButton.ImageAlign = ContentAlignment.MiddleLeft;
        CloseButton.Location = new Point(5, 5);
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
        CloseButton.Size = new Size(140, 32);
        CloseButton.TabIndex = 0;
        CloseButton.Text = "Cancel";
        CloseButton.UseVisualStyleBackColor = true;
        // 
        // AddEditButtonTemplateDialog
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1219, 535);
        ControlBox = false;
        Controls.Add(SaveCancelPanel);
        Controls.Add(TemplateEditor);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = (Icon)resources.GetObject("$this.Icon");
        KeyPreview = true;
        Name = "AddEditButtonTemplateDialog";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Button Template";
        SaveCancelPanel.ResumeLayout(false);
        SavePanel.ResumeLayout(false);
        SaveAsPanel.ResumeLayout(false);
        ClosePanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private ButtonTemplateEditorControl TemplateEditor;
    private Panel SaveCancelPanel;
    private Panel SavePanel;
    private Panel SaveAsPanel;
    private Panel ClosePanel;
    private Intelligence.Shared.UI.AIButton SaveButton;
    private Intelligence.Shared.UI.AIButton SaveAsButton;
    private Intelligence.Shared.UI.AIButton CloseButton;
}