using Adaptive.Intelligence.SqlServer.UI;

namespace Adaptive.Intelligence.SqlServer.UI
{
    partial class SqlQueryEditorDialog
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
            components = new System.ComponentModel.Container();
            QueryStatus = new StatusStrip();
            QueryStatusText = new ToolStripStatusLabel();
            StatusLabel = new ToolStripStatusLabel();
            QueryToolbar = new ToolStrip();
            SaveButton = new ToolStripButton();
            ExecuteButton = new ToolStripButton();
            ParseButton = new ToolStripButton();
            SqlQueryMenuStrip = new MenuStrip();
            QueryMenu = new ToolStripMenuItem();
            QueryMenuExecute = new ToolStripMenuItem();
            QueryMenuParse = new ToolStripMenuItem();
            ttp = new ToolTip(components);
            Editor = new SqlTextEditor();
            QueryTabs = new TabControl();
            ResultsPage = new TabPage();
            ResultsGrid = new DataGridView();
            MessagesPage = new TabPage();
            MessagesText = new TextBox();
            ResultsSplitter = new Splitter();
            QueryStatus.SuspendLayout();
            QueryToolbar.SuspendLayout();
            SqlQueryMenuStrip.SuspendLayout();
            QueryTabs.SuspendLayout();
            ResultsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ResultsGrid).BeginInit();
            MessagesPage.SuspendLayout();
            SuspendLayout();
            // 
            // QueryStatus
            // 
            QueryStatus.Items.AddRange(new ToolStripItem[] { QueryStatusText, StatusLabel });
            QueryStatus.Location = new Point(0, 754);
            QueryStatus.Name = "QueryStatus";
            QueryStatus.Size = new Size(1091, 22);
            QueryStatus.TabIndex = 4;
            QueryStatus.Text = "statusStrip1";
            // 
            // QueryStatusText
            // 
            QueryStatusText.Name = "QueryStatusText";
            QueryStatusText.Size = new Size(39, 17);
            QueryStatusText.Text = "Ready";
            // 
            // StatusLabel
            // 
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(35, 17);
            StatusLabel.Text = "100%";
            // 
            // QueryToolbar
            // 
            QueryToolbar.Items.AddRange(new ToolStripItem[] { SaveButton, ExecuteButton, ParseButton });
            QueryToolbar.Location = new Point(0, 23);
            QueryToolbar.Name = "QueryToolbar";
            QueryToolbar.Padding = new Padding(0);
            QueryToolbar.RenderMode = ToolStripRenderMode.Professional;
            QueryToolbar.Size = new Size(1091, 27);
            QueryToolbar.TabIndex = 3;
            QueryToolbar.Text = "toolStrip1";
            // 
            // SaveButton
            // 
            SaveButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SaveButton.Image = Properties.Resources.Save;
            SaveButton.ImageTransparentColor = Color.Magenta;
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(23, 24);
            SaveButton.ToolTipText = "Save the current query text. (Ctrl + S)";
            // 
            // ExecuteButton
            // 
            ExecuteButton.Image = Properties.Resources.Exec_16_TP;
            ExecuteButton.ImageAlign = ContentAlignment.MiddleLeft;
            ExecuteButton.ImageTransparentColor = Color.Magenta;
            ExecuteButton.Name = "ExecuteButton";
            ExecuteButton.Size = new Size(68, 24);
            ExecuteButton.Text = "Execute";
            ExecuteButton.ToolTipText = "Execute the current query.";
            // 
            // ParseButton
            // 
            ParseButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ParseButton.Image = Properties.Resources.Parse;
            ParseButton.ImageScaling = ToolStripItemImageScaling.None;
            ParseButton.ImageTransparentColor = Color.Magenta;
            ParseButton.Name = "ParseButton";
            ParseButton.Size = new Size(23, 24);
            ParseButton.ToolTipText = "Parse the current text.";
            // 
            // SqlQueryMenuStrip
            // 
            SqlQueryMenuStrip.Items.AddRange(new ToolStripItem[] { QueryMenu });
            SqlQueryMenuStrip.LayoutStyle = ToolStripLayoutStyle.Table;
            SqlQueryMenuStrip.Location = new Point(0, 0);
            SqlQueryMenuStrip.Name = "SqlQueryMenuStrip";
            SqlQueryMenuStrip.Size = new Size(1091, 23);
            SqlQueryMenuStrip.TabIndex = 5;
            // 
            // QueryMenu
            // 
            QueryMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            QueryMenu.DropDownItems.AddRange(new ToolStripItem[] { QueryMenuExecute, QueryMenuParse });
            QueryMenu.ImageScaling = ToolStripItemImageScaling.None;
            QueryMenu.MergeAction = MergeAction.Insert;
            QueryMenu.MergeIndex = 1;
            QueryMenu.Name = "QueryMenu";
            QueryMenu.Size = new Size(51, 19);
            QueryMenu.Text = "&Query";
            // 
            // QueryMenuExecute
            // 
            QueryMenuExecute.Name = "QueryMenuExecute";
            QueryMenuExecute.ShortcutKeys = Keys.F5;
            QueryMenuExecute.Size = new Size(148, 22);
            QueryMenuExecute.Text = "E&xecute";
            QueryMenuExecute.ToolTipText = "Execute the current query.";
            // 
            // QueryMenuParse
            // 
            QueryMenuParse.Name = "QueryMenuParse";
            QueryMenuParse.ShortcutKeys = Keys.Control | Keys.F5;
            QueryMenuParse.Size = new Size(148, 22);
            QueryMenuParse.Text = "&Parse";
            QueryMenuParse.ToolTipText = "Parse the query text.";
            // 
            // Editor
            // 
            Editor.AcceptsTab = true;
            Editor.BorderStyle = BorderStyle.None;
            Editor.Cursor = Cursors.IBeam;
            Editor.DetectUrls = false;
            Editor.Dock = DockStyle.Fill;
            Editor.Font = new Font("Consolas", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            Editor.ForeColor = Color.Black;
            Editor.HideSelection = false;
            Editor.Location = new Point(0, 50);
            Editor.Name = "Editor";
            Editor.Size = new Size(1091, 437);
            Editor.TabIndex = 3;
            Editor.Text = "";
            Editor.WordWrap = false;
            // 
            // QueryTabs
            // 
            QueryTabs.Controls.Add(ResultsPage);
            QueryTabs.Controls.Add(MessagesPage);
            QueryTabs.Dock = DockStyle.Bottom;
            QueryTabs.Location = new Point(0, 490);
            QueryTabs.Name = "QueryTabs";
            QueryTabs.SelectedIndex = 0;
            QueryTabs.Size = new Size(1091, 264);
            QueryTabs.TabIndex = 6;
            QueryTabs.Visible = false;
            // 
            // ResultsPage
            // 
            ResultsPage.Controls.Add(ResultsGrid);
            ResultsPage.Location = new Point(4, 26);
            ResultsPage.Name = "ResultsPage";
            ResultsPage.Padding = new Padding(3);
            ResultsPage.Size = new Size(1083, 234);
            ResultsPage.TabIndex = 0;
            ResultsPage.Text = "Results";
            ResultsPage.UseVisualStyleBackColor = true;
            // 
            // ResultsGrid
            // 
            ResultsGrid.BackgroundColor = SystemColors.Window;
            ResultsGrid.BorderStyle = BorderStyle.None;
            ResultsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ResultsGrid.Dock = DockStyle.Fill;
            ResultsGrid.Location = new Point(3, 3);
            ResultsGrid.Name = "ResultsGrid";
            ResultsGrid.RowTemplate.Height = 25;
            ResultsGrid.Size = new Size(1077, 228);
            ResultsGrid.TabIndex = 0;
            // 
            // MessagesPage
            // 
            MessagesPage.Controls.Add(MessagesText);
            MessagesPage.Location = new Point(4, 26);
            MessagesPage.Name = "MessagesPage";
            MessagesPage.Padding = new Padding(3);
            MessagesPage.Size = new Size(1083, 234);
            MessagesPage.TabIndex = 1;
            MessagesPage.Text = "Messages";
            MessagesPage.UseVisualStyleBackColor = true;
            // 
            // MessagesText
            // 
            MessagesText.Dock = DockStyle.Fill;
            MessagesText.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            MessagesText.Location = new Point(3, 3);
            MessagesText.Multiline = true;
            MessagesText.Name = "MessagesText";
            MessagesText.Size = new Size(1077, 228);
            MessagesText.TabIndex = 0;
            // 
            // ResultsSplitter
            // 
            ResultsSplitter.Dock = DockStyle.Bottom;
            ResultsSplitter.Location = new Point(0, 487);
            ResultsSplitter.Name = "ResultsSplitter";
            ResultsSplitter.Size = new Size(1091, 3);
            ResultsSplitter.TabIndex = 7;
            ResultsSplitter.TabStop = false;
            // 
            // SqlQueryEditorDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1091, 776);
            Controls.Add(Editor);
            Controls.Add(ResultsSplitter);
            Controls.Add(QueryTabs);
            Controls.Add(QueryStatus);
            Controls.Add(QueryToolbar);
            Controls.Add(SqlQueryMenuStrip);
            KeyPreview = true;
            MainMenuStrip = SqlQueryMenuStrip;
            Margin = new Padding(4, 5, 4, 5);
            Name = "SqlQueryEditorDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Query Editor";
            QueryStatus.ResumeLayout(false);
            QueryStatus.PerformLayout();
            QueryToolbar.ResumeLayout(false);
            QueryToolbar.PerformLayout();
            SqlQueryMenuStrip.ResumeLayout(false);
            SqlQueryMenuStrip.PerformLayout();
            QueryTabs.ResumeLayout(false);
            ResultsPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ResultsGrid).EndInit();
            MessagesPage.ResumeLayout(false);
            MessagesPage.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.StatusStrip QueryStatus;
        private System.Windows.Forms.ToolStripStatusLabel QueryStatusText;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStrip QueryToolbar;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private System.Windows.Forms.ToolStripButton ExecuteButton;
        private System.Windows.Forms.ToolStripButton ParseButton;
        private System.Windows.Forms.MenuStrip SqlQueryMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem QueryMenu;
        private System.Windows.Forms.ToolStripMenuItem QueryMenuExecute;
        private System.Windows.Forms.ToolStripMenuItem QueryMenuParse;
        private System.Windows.Forms.ToolTip ttp;

        private SqlTextEditor Editor;
        private TabControl QueryTabs;
        private TabPage ResultsPage;
        private TabPage MessagesPage;
        private Splitter ResultsSplitter;
        private DataGridView ResultsGrid;
        private TextBox MessagesText;
    }
}