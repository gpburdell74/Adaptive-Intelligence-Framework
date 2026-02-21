using Adaptive.Intelligence.Shared.UI;

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
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDialog));
        ButtonTemplatePanel = new TemplatedGradientPanel();
        templatedGradientPanel2 = new TemplatedGradientPanel();
        OpenButtonButton = new AIButton();
        NewButtonPanel = new TemplatedGradientPanel();
        NewButtonButton = new AIButton();
        ButtonHeader = new SectionTitleHeader();
        PanelTemplatePanel = new TemplatedGradientPanel();
        OpenPanelPanel = new TemplatedGradientPanel();
        OpenPanelButton = new AIButton();
        NewPanelPanel = new TemplatedGradientPanel();
        NewPanelButton = new AIButton();
        PanelHeader = new SectionTitleHeader();
        ButtonTemplatePanel.SuspendLayout();
        templatedGradientPanel2.SuspendLayout();
        NewButtonPanel.SuspendLayout();
        PanelTemplatePanel.SuspendLayout();
        OpenPanelPanel.SuspendLayout();
        NewPanelPanel.SuspendLayout();
        SuspendLayout();
        // 
        // ButtonTemplatePanel
        // 
        ButtonTemplatePanel.Controls.Add(templatedGradientPanel2);
        ButtonTemplatePanel.Controls.Add(NewButtonPanel);
        ButtonTemplatePanel.Controls.Add(ButtonHeader);
        ButtonTemplatePanel.Dock = DockStyle.Left;
        ButtonTemplatePanel.Location = new Point(0, 0);
        ButtonTemplatePanel.Name = "ButtonTemplatePanel";
        ButtonTemplatePanel.Size = new Size(200, 130);
        ButtonTemplatePanel.TabIndex = 0;
        ButtonTemplatePanel.TemplateFromFile = null;
        ButtonTemplatePanel.TemplateJson = null;
        // 
        // templatedGradientPanel2
        // 
        templatedGradientPanel2.Controls.Add(OpenButtonButton);
        templatedGradientPanel2.Dock = DockStyle.Top;
        templatedGradientPanel2.Location = new Point(0, 76);
        templatedGradientPanel2.Name = "templatedGradientPanel2";
        templatedGradientPanel2.Padding = new Padding(10);
        templatedGradientPanel2.Size = new Size(200, 54);
        templatedGradientPanel2.TabIndex = 2;
        templatedGradientPanel2.TemplateFromFile = "D:\\Adaptive.Intelligence\\Win32\\Adaptive Data Vault\\Adaptive-Data-Vault\\Resources\\Button Templates\\JSON\\AI Standard.panel.template.json";
        templatedGradientPanel2.TemplateJson = resources.GetString("templatedGradientPanel2.TemplateJson");
        // 
        // OpenButtonButton
        // 
        OpenButtonButton.BorderWidth = 1;
        OpenButtonButton.Checked = false;
        OpenButtonButton.Dock = DockStyle.Fill;
        OpenButtonButton.HoverBorderColor = Color.Gray;
        OpenButtonButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        OpenButtonButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        OpenButtonButton.HoverFont = new Font("Segoe UI", 9.75F);
        OpenButtonButton.HoverForeColor = Color.Black;
        OpenButtonButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        OpenButtonButton.Location = new Point(10, 10);
        OpenButtonButton.Name = "OpenButtonButton";
        OpenButtonButton.NormalBorderColor = Color.Gray;
        OpenButtonButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        OpenButtonButton.NormalEndColor = Color.Silver;
        OpenButtonButton.NormalFont = new Font("Segoe UI", 9.75F);
        OpenButtonButton.NormalForeColor = Color.Black;
        OpenButtonButton.NormalStartColor = Color.FromArgb(248, 248, 248);
        OpenButtonButton.PressedBorderColor = Color.Gray;
        OpenButtonButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        OpenButtonButton.PressedEndColor = Color.FromArgb(174, 45, 61);
        OpenButtonButton.PressedFont = new Font("Segoe UI", 9.75F);
        OpenButtonButton.PressedForeColor = Color.White;
        OpenButtonButton.PressedStartColor = Color.Gray;
        OpenButtonButton.Size = new Size(180, 34);
        OpenButtonButton.TabIndex = 0;
        OpenButtonButton.Text = "Open Button Template";
        OpenButtonButton.UseVisualStyleBackColor = true;
        // 
        // NewButtonPanel
        // 
        NewButtonPanel.Controls.Add(NewButtonButton);
        NewButtonPanel.Dock = DockStyle.Top;
        NewButtonPanel.Location = new Point(0, 32);
        NewButtonPanel.Name = "NewButtonPanel";
        NewButtonPanel.Padding = new Padding(10, 10, 10, 0);
        NewButtonPanel.Size = new Size(200, 44);
        NewButtonPanel.TabIndex = 1;
        NewButtonPanel.TemplateFromFile = "D:\\Adaptive.Intelligence\\Win32\\Adaptive Data Vault\\Adaptive-Data-Vault\\Resources\\Button Templates\\JSON\\AI Standard.panel.template.json";
        NewButtonPanel.TemplateJson = resources.GetString("NewButtonPanel.TemplateJson");
        // 
        // NewButtonButton
        // 
        NewButtonButton.BorderWidth = 1;
        NewButtonButton.Checked = false;
        NewButtonButton.Dock = DockStyle.Fill;
        NewButtonButton.HoverBorderColor = Color.Gray;
        NewButtonButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        NewButtonButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        NewButtonButton.HoverFont = new Font("Segoe UI", 9.75F);
        NewButtonButton.HoverForeColor = Color.Black;
        NewButtonButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        NewButtonButton.Location = new Point(10, 10);
        NewButtonButton.Name = "NewButtonButton";
        NewButtonButton.NormalBorderColor = Color.Gray;
        NewButtonButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        NewButtonButton.NormalEndColor = Color.Silver;
        NewButtonButton.NormalFont = new Font("Segoe UI", 9.75F);
        NewButtonButton.NormalForeColor = Color.Black;
        NewButtonButton.NormalStartColor = Color.FromArgb(248, 248, 248);
        NewButtonButton.PressedBorderColor = Color.Gray;
        NewButtonButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        NewButtonButton.PressedEndColor = Color.FromArgb(174, 45, 61);
        NewButtonButton.PressedFont = new Font("Segoe UI", 9.75F);
        NewButtonButton.PressedForeColor = Color.White;
        NewButtonButton.PressedStartColor = Color.Gray;
        NewButtonButton.Size = new Size(180, 34);
        NewButtonButton.TabIndex = 0;
        NewButtonButton.Text = "New Button Template";
        NewButtonButton.UseVisualStyleBackColor = true;
        // 
        // ButtonHeader
        // 
        ButtonHeader.Dock = DockStyle.Top;
        ButtonHeader.Location = new Point(0, 0);
        ButtonHeader.Margin = new Padding(48, 23, 48, 23);
        ButtonHeader.Name = "ButtonHeader";
        ButtonHeader.Size = new Size(200, 32);
        ButtonHeader.TabIndex = 0;
        ButtonHeader.TabStop = false;
        ButtonHeader.Text = "Button  Templates";
        // 
        // PanelTemplatePanel
        // 
        PanelTemplatePanel.Controls.Add(OpenPanelPanel);
        PanelTemplatePanel.Controls.Add(NewPanelPanel);
        PanelTemplatePanel.Controls.Add(PanelHeader);
        PanelTemplatePanel.Dock = DockStyle.Right;
        PanelTemplatePanel.Location = new Point(200, 0);
        PanelTemplatePanel.Name = "PanelTemplatePanel";
        PanelTemplatePanel.Size = new Size(200, 130);
        PanelTemplatePanel.TabIndex = 1;
        PanelTemplatePanel.TemplateFromFile = null;
        PanelTemplatePanel.TemplateJson = null;
        // 
        // OpenPanelPanel
        // 
        OpenPanelPanel.Controls.Add(OpenPanelButton);
        OpenPanelPanel.Dock = DockStyle.Top;
        OpenPanelPanel.Location = new Point(0, 76);
        OpenPanelPanel.Name = "OpenPanelPanel";
        OpenPanelPanel.Padding = new Padding(10);
        OpenPanelPanel.Size = new Size(200, 54);
        OpenPanelPanel.TabIndex = 2;
        OpenPanelPanel.TemplateFromFile = "D:\\Adaptive.Intelligence\\Win32\\Adaptive Data Vault\\Adaptive-Data-Vault\\Resources\\Button Templates\\JSON\\AI Standard.panel.template.json";
        OpenPanelPanel.TemplateJson = resources.GetString("OpenPanelPanel.TemplateJson");
        // 
        // OpenPanelButton
        // 
        OpenPanelButton.BorderWidth = 1;
        OpenPanelButton.Checked = false;
        OpenPanelButton.Dock = DockStyle.Fill;
        OpenPanelButton.HoverBorderColor = Color.Gray;
        OpenPanelButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        OpenPanelButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        OpenPanelButton.HoverFont = new Font("Segoe UI", 9.75F);
        OpenPanelButton.HoverForeColor = Color.Black;
        OpenPanelButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        OpenPanelButton.Location = new Point(10, 10);
        OpenPanelButton.Name = "OpenPanelButton";
        OpenPanelButton.NormalBorderColor = Color.Gray;
        OpenPanelButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        OpenPanelButton.NormalEndColor = Color.Silver;
        OpenPanelButton.NormalFont = new Font("Segoe UI", 9.75F);
        OpenPanelButton.NormalForeColor = Color.Black;
        OpenPanelButton.NormalStartColor = Color.FromArgb(248, 248, 248);
        OpenPanelButton.PressedBorderColor = Color.Gray;
        OpenPanelButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        OpenPanelButton.PressedEndColor = Color.FromArgb(174, 45, 61);
        OpenPanelButton.PressedFont = new Font("Segoe UI", 9.75F);
        OpenPanelButton.PressedForeColor = Color.White;
        OpenPanelButton.PressedStartColor = Color.Gray;
        OpenPanelButton.Size = new Size(180, 34);
        OpenPanelButton.TabIndex = 0;
        OpenPanelButton.Text = "Open Panel Template";
        OpenPanelButton.UseVisualStyleBackColor = true;
        // 
        // NewPanelPanel
        // 
        NewPanelPanel.Controls.Add(NewPanelButton);
        NewPanelPanel.Dock = DockStyle.Top;
        NewPanelPanel.Location = new Point(0, 32);
        NewPanelPanel.Name = "NewPanelPanel";
        NewPanelPanel.Padding = new Padding(10, 10, 10, 0);
        NewPanelPanel.Size = new Size(200, 44);
        NewPanelPanel.TabIndex = 1;
        NewPanelPanel.TemplateFromFile = "D:\\Adaptive.Intelligence\\Win32\\Adaptive Data Vault\\Adaptive-Data-Vault\\Resources\\Button Templates\\JSON\\AI Standard.panel.template.json";
        NewPanelPanel.TemplateJson = resources.GetString("NewPanelPanel.TemplateJson");
        // 
        // NewPanelButton
        // 
        NewPanelButton.BorderWidth = 1;
        NewPanelButton.Checked = false;
        NewPanelButton.Dock = DockStyle.Fill;
        NewPanelButton.HoverBorderColor = Color.Gray;
        NewPanelButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        NewPanelButton.HoverEndColor = Color.FromArgb(224, 224, 224);
        NewPanelButton.HoverFont = new Font("Segoe UI", 9.75F);
        NewPanelButton.HoverForeColor = Color.Black;
        NewPanelButton.HoverStartColor = Color.FromArgb(218, 194, 204);
        NewPanelButton.Location = new Point(10, 10);
        NewPanelButton.Name = "NewPanelButton";
        NewPanelButton.NormalBorderColor = Color.Gray;
        NewPanelButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        NewPanelButton.NormalEndColor = Color.Silver;
        NewPanelButton.NormalFont = new Font("Segoe UI", 9.75F);
        NewPanelButton.NormalForeColor = Color.Black;
        NewPanelButton.NormalStartColor = Color.FromArgb(248, 248, 248);
        NewPanelButton.PressedBorderColor = Color.Gray;
        NewPanelButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        NewPanelButton.PressedEndColor = Color.FromArgb(174, 45, 61);
        NewPanelButton.PressedFont = new Font("Segoe UI", 9.75F);
        NewPanelButton.PressedForeColor = Color.White;
        NewPanelButton.PressedStartColor = Color.Gray;
        NewPanelButton.Size = new Size(180, 34);
        NewPanelButton.TabIndex = 0;
        NewPanelButton.Text = "New Panel Template";
        NewPanelButton.UseVisualStyleBackColor = true;
        // 
        // PanelHeader
        // 
        PanelHeader.Dock = DockStyle.Top;
        PanelHeader.Location = new Point(0, 0);
        PanelHeader.Margin = new Padding(48, 23, 48, 23);
        PanelHeader.Name = "PanelHeader";
        PanelHeader.Size = new Size(200, 32);
        PanelHeader.TabIndex = 0;
        PanelHeader.TabStop = false;
        PanelHeader.Text = "Panel Templates";
        // 
        // MainDialog
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(400, 130);
        Controls.Add(PanelTemplatePanel);
        Controls.Add(ButtonTemplatePanel);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        KeyPreview = true;
        MaximizeBox = false;
        Name = "MainDialog";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Adaptive Button Template Editor";
        ButtonTemplatePanel.ResumeLayout(false);
        templatedGradientPanel2.ResumeLayout(false);
        NewButtonPanel.ResumeLayout(false);
        PanelTemplatePanel.ResumeLayout(false);
        OpenPanelPanel.ResumeLayout(false);
        NewPanelPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TemplatedGradientPanel ButtonTemplatePanel;
    private SectionTitleHeader ButtonHeader;
    private TemplatedGradientPanel PanelTemplatePanel;
    private SectionTitleHeader PanelHeader;
    private TemplatedGradientPanel templatedGradientPanel2;
    private AIButton OpenButtonButton;
    private TemplatedGradientPanel NewButtonPanel;
    private AIButton NewButtonButton;
    private TemplatedGradientPanel OpenPanelPanel;
    private AIButton OpenPanelButton;
    private TemplatedGradientPanel NewPanelPanel;
    private AIButton NewPanelButton;
}
