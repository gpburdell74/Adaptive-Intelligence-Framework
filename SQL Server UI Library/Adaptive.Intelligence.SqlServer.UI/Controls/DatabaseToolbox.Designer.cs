namespace Adaptive.Intelligence.SqlServer.UI
{
    partial class DatabaseToolbox
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseToolbox));
            TitleBar = new Panel();
            ObjectExplorerLabel = new Shared.UI.AdvancedLabel();
            ToolboxCloseButton = new Shared.UI.AIButton();
            Toolbar = new ToolStrip();
            ConnectTextButton = new ToolStripButton();
            ConnectImageButton = new ToolStripButton();
            DisconnectImageButton = new ToolStripButton();
            StopButton = new ToolStripButton();
            FilterButton = new ToolStripButton();
            RefreshButton = new ToolStripButton();
            DbTree = new DatabaseTreeView();
            LoadingLabel = new Shared.UI.AdvancedLabel();
            TitleBar.SuspendLayout();
            Toolbar.SuspendLayout();
            SuspendLayout();
            // 
            // TitleBar
            // 
            TitleBar.BackColor = SystemColors.Info;
            TitleBar.Controls.Add(ObjectExplorerLabel);
            TitleBar.Controls.Add(ToolboxCloseButton);
            TitleBar.Dock = DockStyle.Top;
            TitleBar.ForeColor = SystemColors.InfoText;
            TitleBar.Location = new Point(0, 0);
            TitleBar.Name = "TitleBar";
            TitleBar.Size = new Size(222, 20);
            TitleBar.TabIndex = 0;
            // 
            // ObjectExplorerLabel
            // 
            ObjectExplorerLabel.Dock = DockStyle.Fill;
            ObjectExplorerLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ObjectExplorerLabel.Location = new Point(0, 0);
            ObjectExplorerLabel.Name = "ObjectExplorerLabel";
            ObjectExplorerLabel.Size = new Size(201, 20);
            ObjectExplorerLabel.TabIndex = 1;
            ObjectExplorerLabel.TabStop = false;
            ObjectExplorerLabel.Text = "DB Object Explorer";
            ObjectExplorerLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ToolboxCloseButton
            // 
            ToolboxCloseButton.BackColor = SystemColors.Info;
            ToolboxCloseButton.BorderWidth = 1;
            ToolboxCloseButton.Checked = false;
            ToolboxCloseButton.Dock = DockStyle.Right;
            ToolboxCloseButton.HoverBorderColor = Color.Gray;
            ToolboxCloseButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            ToolboxCloseButton.HoverEndColor = Color.FromArgb(224, 224, 224);
            ToolboxCloseButton.HoverFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ToolboxCloseButton.HoverForeColor = Color.Black;
            ToolboxCloseButton.HoverStartColor = Color.FromArgb(218, 194, 204);
            ToolboxCloseButton.Image = Properties.Resources.CloseButton_Image;
            ToolboxCloseButton.Location = new Point(201, 0);
            ToolboxCloseButton.Name = "ToolboxCloseButton";
            ToolboxCloseButton.NormalBorderColor = Color.Transparent;
            ToolboxCloseButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            ToolboxCloseButton.NormalEndColor = SystemColors.Info;
            ToolboxCloseButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ToolboxCloseButton.NormalForeColor = SystemColors.InfoText;
            ToolboxCloseButton.NormalStartColor = SystemColors.Info;
            ToolboxCloseButton.PressedBorderColor = Color.Gray;
            ToolboxCloseButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            ToolboxCloseButton.PressedEndColor = Color.FromArgb(174, 45, 61);
            ToolboxCloseButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ToolboxCloseButton.PressedForeColor = Color.White;
            ToolboxCloseButton.PressedStartColor = Color.Gray;
            ToolboxCloseButton.Size = new Size(21, 20);
            ToolboxCloseButton.TabIndex = 0;
            ToolboxCloseButton.UseVisualStyleBackColor = false;
            // 
            // Toolbar
            // 
            Toolbar.Items.AddRange(new ToolStripItem[] { ConnectTextButton, ConnectImageButton, DisconnectImageButton, StopButton, FilterButton, RefreshButton });
            Toolbar.Location = new Point(0, 20);
            Toolbar.Name = "Toolbar";
            Toolbar.Size = new Size(222, 25);
            Toolbar.TabIndex = 1;
            Toolbar.Text = "toolStrip1";
            // 
            // ConnectTextButton
            // 
            ConnectTextButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ConnectTextButton.Image = (Image)resources.GetObject("ConnectTextButton.Image");
            ConnectTextButton.ImageTransparentColor = Color.Magenta;
            ConnectTextButton.Name = "ConnectTextButton";
            ConnectTextButton.Size = new Size(56, 22);
            ConnectTextButton.Text = "Connect";
            ConnectTextButton.ToolTipText = "Click to connect to SQL Server";
            // 
            // ConnectImageButton
            // 
            ConnectImageButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ConnectImageButton.Image = Properties.Resources.Connect1;
            ConnectImageButton.ImageTransparentColor = Color.Magenta;
            ConnectImageButton.Name = "ConnectImageButton";
            ConnectImageButton.Size = new Size(23, 22);
            ConnectImageButton.ToolTipText = "Click to connect to SQL Server";
            // 
            // DisconnectImageButton
            // 
            DisconnectImageButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            DisconnectImageButton.Enabled = false;
            DisconnectImageButton.Image = Properties.Resources.Disconnect_TP;
            DisconnectImageButton.ImageTransparentColor = Color.Magenta;
            DisconnectImageButton.Name = "DisconnectImageButton";
            DisconnectImageButton.Size = new Size(23, 22);
            DisconnectImageButton.ToolTipText = "Disconnect from SQL Server.";
            // 
            // StopButton
            // 
            StopButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            StopButton.Enabled = false;
            StopButton.Image = Properties.Resources.Stop_16x;
            StopButton.ImageTransparentColor = Color.Magenta;
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(23, 22);
            StopButton.ToolTipText = "Cancel Execution";
            // 
            // FilterButton
            // 
            FilterButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            FilterButton.Enabled = false;
            FilterButton.Image = Properties.Resources.Filter2HS;
            FilterButton.ImageTransparentColor = Color.Magenta;
            FilterButton.Name = "FilterButton";
            FilterButton.Size = new Size(23, 22);
            FilterButton.ToolTipText = "Filter";
            // 
            // RefreshButton
            // 
            RefreshButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            RefreshButton.Enabled = false;
            RefreshButton.Image = Properties.Resources.refresh_32x32;
            RefreshButton.ImageTransparentColor = Color.Magenta;
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(23, 22);
            RefreshButton.ToolTipText = "Refresh";
            // 
            // DbTree
            // 
            DbTree.Dock = DockStyle.Fill;
            DbTree.ImageIndex = 0;
            DbTree.Location = new Point(0, 45);
            DbTree.Name = "DbTree";
            DbTree.SelectedImageIndex = 0;
            DbTree.Size = new Size(222, 482);
            DbTree.TabIndex = 2;
            DbTree.Visible = false;
            // 
            // LoadingLabel
            // 
            LoadingLabel.AutoSize = true;
            LoadingLabel.Dock = DockStyle.Fill;
            LoadingLabel.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LoadingLabel.Location = new Point(0, 45);
            LoadingLabel.Name = "LoadingLabel";
            LoadingLabel.Size = new Size(222, 482);
            LoadingLabel.TabIndex = 3;
            LoadingLabel.TabStop = false;
            LoadingLabel.Text = "Loading...";
            LoadingLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DatabaseToolbox
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(LoadingLabel);
            Controls.Add(DbTree);
            Controls.Add(Toolbar);
            Controls.Add(TitleBar);
            Name = "DatabaseToolbox";
            Size = new Size(222, 527);
            TitleBar.ResumeLayout(false);
            Toolbar.ResumeLayout(false);
            Toolbar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel TitleBar;
        private Shared.UI.AdvancedLabel ObjectExplorerLabel;
        private Shared.UI.AIButton ToolboxCloseButton;
        private Shared.UI.AdvancedLabel LoadingLabel;

        private ToolStrip Toolbar;
        private ToolStripButton ConnectTextButton;
        private ToolStripButton ConnectImageButton;
        private ToolStripButton DisconnectImageButton;
        private ToolStripButton StopButton;
        private ToolStripButton FilterButton;
        private ToolStripButton RefreshButton;

        private DatabaseTreeView DbTree;
        
    }
}
