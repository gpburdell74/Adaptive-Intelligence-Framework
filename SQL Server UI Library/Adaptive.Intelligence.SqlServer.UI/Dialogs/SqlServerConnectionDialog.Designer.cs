using Adaptive.Intelligence.Shared.UI;

namespace Adaptive.Intelligence.SqlServer.UI
{
    partial class SqlServerConnectionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlServerConnectionDialog));
            Tabs = new TabControl();
            LoginPage = new TabPage();
            ConnectPanel = new Panel();
            LoginPanel = new Panel();
            PasswordText = new PasswordTextBox();
            RememberCheck = new CheckBox();
            UserIdText = new TextBox();
            PasswordLabel = new AdvancedLabel();
            UserIdLabel = new AdvancedLabel();
            AuthTypeList = new ComboBox();
            AuthTypeLabel = new AdvancedLabel();
            ServerText = new TextBox();
            ServerNameLabel = new AdvancedLabel();
            OptionsPage = new TabPage();
            PoolingPanel = new Panel();
            MaxPoolSizeLabel = new AdvancedLabel();
            MinPoolSizeLabel = new AdvancedLabel();
            MaxPoolSizeText = new IntegerTextBox();
            MinPoolSizeText = new IntegerTextBox();
            DbCheck = new CheckBox();
            DbList = new ComboBox();
            PoolFailIntervalCheck = new IntegerTextBox();
            RetryIntervalText = new IntegerTextBox();
            RetryCountText = new IntegerTextBox();
            PacketSizeText = new IntegerTextBox();
            PollFailLabel = new AdvancedLabel();
            RetryIntervalLabel = new AdvancedLabel();
            RetryCountLabel = new AdvancedLabel();
            PacketSizeLabel = new AdvancedLabel();
            AllowMultiCheck = new CheckBox();
            AsynCheck = new CheckBox();
            PoolingCheck = new CheckBox();
            EnlistCheck = new CheckBox();
            PersistCheck = new CheckBox();
            ErrorProvider = new ErrorProvider(components);
            ttp = new ToolTip(components);
            TestButton = new AIButton();
            ConnectButton = new AIButton();
            CloseButton = new AIButton();
            SqlTitleLabel = new AdvancedLabel();
            Tabs.SuspendLayout();
            LoginPage.SuspendLayout();
            ConnectPanel.SuspendLayout();
            LoginPanel.SuspendLayout();
            OptionsPage.SuspendLayout();
            PoolingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // Tabs
            // 
            Tabs.Controls.Add(LoginPage);
            Tabs.Controls.Add(OptionsPage);
            Tabs.Location = new Point(0, 0);
            Tabs.Name = "Tabs";
            Tabs.SelectedIndex = 0;
            Tabs.Size = new Size(372, 416);
            Tabs.TabIndex = 0;
            // 
            // LoginPage
            // 
            LoginPage.Controls.Add(ConnectPanel);
            LoginPage.Controls.Add(SqlTitleLabel);
            LoginPage.Location = new Point(4, 26);
            LoginPage.Name = "LoginPage";
            LoginPage.Padding = new Padding(3, 50, 3, 3);
            LoginPage.Size = new Size(364, 386);
            LoginPage.TabIndex = 0;
            LoginPage.Text = "Login";
            LoginPage.ToolTipText = "Login parameters.";
            // 
            // ConnectPanel
            // 
            ConnectPanel.Controls.Add(LoginPanel);
            ConnectPanel.Controls.Add(AuthTypeList);
            ConnectPanel.Controls.Add(AuthTypeLabel);
            ConnectPanel.Controls.Add(ServerText);
            ConnectPanel.Controls.Add(ServerNameLabel);
            ConnectPanel.Dock = DockStyle.Top;
            ConnectPanel.Location = new Point(3, 86);
            ConnectPanel.Name = "ConnectPanel";
            ConnectPanel.Size = new Size(358, 193);
            ConnectPanel.TabIndex = 1;
            // 
            // LoginPanel
            // 
            LoginPanel.Controls.Add(PasswordText);
            LoginPanel.Controls.Add(RememberCheck);
            LoginPanel.Controls.Add(UserIdText);
            LoginPanel.Controls.Add(PasswordLabel);
            LoginPanel.Controls.Add(UserIdLabel);
            LoginPanel.Location = new Point(8, 80);
            LoginPanel.Name = "LoginPanel";
            LoginPanel.Size = new Size(356, 104);
            LoginPanel.TabIndex = 4;
            LoginPanel.Visible = false;
            // 
            // PasswordText
            // 
            PasswordText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PasswordText.Location = new Point(140, 40);
            PasswordText.Margin = new Padding(4, 4, 4, 4);
            PasswordText.Name = "PasswordText";
            PasswordText.Size = new Size(173, 25);
            PasswordText.TabIndex = 3;
            ttp.SetToolTip(PasswordText, "Enter the SQL Server password here.  Click the view or hide button to view or hide the text entry.");
            // 
            // RememberCheck
            // 
            RememberCheck.AutoSize = true;
            RememberCheck.Location = new Point(140, 75);
            RememberCheck.Name = "RememberCheck";
            RememberCheck.Size = new Size(151, 21);
            RememberCheck.TabIndex = 4;
            RememberCheck.Text = "&Remember Password";
            ttp.SetToolTip(RememberCheck, "Check here to remember the password that was entered.");
            RememberCheck.UseVisualStyleBackColor = true;
            // 
            // UserIdText
            // 
            UserIdText.Location = new Point(140, 5);
            UserIdText.Name = "UserIdText";
            UserIdText.Size = new Size(173, 25);
            UserIdText.TabIndex = 1;
            ttp.SetToolTip(UserIdText, "Enter the SQL Server User ID.");
            // 
            // PasswordLabel
            // 
            PasswordLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PasswordLabel.Location = new Point(59, 41);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(75, 25);
            PasswordLabel.TabIndex = 2;
            PasswordLabel.TabStop = false;
            PasswordLabel.Text = "&Password:";
            PasswordLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // UserIdLabel
            // 
            UserIdLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserIdLabel.Location = new Point(59, 5);
            UserIdLabel.Name = "UserIdLabel";
            UserIdLabel.Size = new Size(75, 25);
            UserIdLabel.TabIndex = 0;
            UserIdLabel.TabStop = false;
            UserIdLabel.Text = "&User ID:";
            UserIdLabel.TextAlign = ContentAlignment.MiddleRight;
            ttp.SetToolTip(UserIdLabel, "Enter the SQL Server User ID.");
            // 
            // AuthTypeList
            // 
            AuthTypeList.FormattingEnabled = true;
            AuthTypeList.Items.AddRange(new object[] { "Windows Authentication", "SQL Server Authentication" });
            AuthTypeList.Location = new Point(140, 46);
            AuthTypeList.Name = "AuthTypeList";
            AuthTypeList.Size = new Size(188, 25);
            AuthTypeList.TabIndex = 3;
            ttp.SetToolTip(AuthTypeList, "Select the method for logging into SQL Server.");
            // 
            // AuthTypeLabel
            // 
            AuthTypeLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            AuthTypeLabel.Location = new Point(5, 45);
            AuthTypeLabel.Name = "AuthTypeLabel";
            AuthTypeLabel.Size = new Size(130, 25);
            AuthTypeLabel.TabIndex = 2;
            AuthTypeLabel.TabStop = false;
            AuthTypeLabel.Text = "&Authentication Type:";
            AuthTypeLabel.TextAlign = ContentAlignment.MiddleRight;
            ttp.SetToolTip(AuthTypeLabel, "Select the method for logging into SQL Server.");
            // 
            // ServerText
            // 
            ServerText.Location = new Point(140, 10);
            ServerText.Name = "ServerText";
            ServerText.Size = new Size(188, 25);
            ServerText.TabIndex = 1;
            ttp.SetToolTip(ServerText, "Enter the name or address of the SQL Server to connect to.");
            // 
            // ServerNameLabel
            // 
            ServerNameLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ServerNameLabel.Location = new Point(5, 10);
            ServerNameLabel.Name = "ServerNameLabel";
            ServerNameLabel.Size = new Size(130, 25);
            ServerNameLabel.TabIndex = 0;
            ServerNameLabel.TabStop = false;
            ServerNameLabel.Text = "&Server Name:";
            ServerNameLabel.TextAlign = ContentAlignment.MiddleRight;
            ttp.SetToolTip(ServerNameLabel, "Enter the name or address of the SQL Server to connect to.");
            // 
            // OptionsPage
            // 
            OptionsPage.Controls.Add(PoolingPanel);
            OptionsPage.Controls.Add(DbCheck);
            OptionsPage.Controls.Add(DbList);
            OptionsPage.Controls.Add(PoolFailIntervalCheck);
            OptionsPage.Controls.Add(RetryIntervalText);
            OptionsPage.Controls.Add(RetryCountText);
            OptionsPage.Controls.Add(PacketSizeText);
            OptionsPage.Controls.Add(PollFailLabel);
            OptionsPage.Controls.Add(RetryIntervalLabel);
            OptionsPage.Controls.Add(RetryCountLabel);
            OptionsPage.Controls.Add(PacketSizeLabel);
            OptionsPage.Controls.Add(AllowMultiCheck);
            OptionsPage.Controls.Add(AsynCheck);
            OptionsPage.Controls.Add(PoolingCheck);
            OptionsPage.Controls.Add(EnlistCheck);
            OptionsPage.Controls.Add(PersistCheck);
            OptionsPage.Location = new Point(4, 26);
            OptionsPage.Name = "OptionsPage";
            OptionsPage.Padding = new Padding(3);
            OptionsPage.Size = new Size(364, 386);
            OptionsPage.TabIndex = 1;
            OptionsPage.Text = "Options";
            OptionsPage.ToolTipText = "Connection options.";
            // 
            // PoolingPanel
            // 
            PoolingPanel.Controls.Add(MaxPoolSizeLabel);
            PoolingPanel.Controls.Add(MinPoolSizeLabel);
            PoolingPanel.Controls.Add(MaxPoolSizeText);
            PoolingPanel.Controls.Add(MinPoolSizeText);
            PoolingPanel.Location = new Point(38, 125);
            PoolingPanel.Name = "PoolingPanel";
            PoolingPanel.Size = new Size(262, 69);
            PoolingPanel.TabIndex = 20;
            PoolingPanel.Visible = false;
            // 
            // MaxPoolSizeLabel
            // 
            MaxPoolSizeLabel.AutoSize = true;
            MaxPoolSizeLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            MaxPoolSizeLabel.Location = new Point(12, 41);
            MaxPoolSizeLabel.Name = "MaxPoolSizeLabel";
            MaxPoolSizeLabel.Size = new Size(125, 17);
            MaxPoolSizeLabel.TabIndex = 2;
            MaxPoolSizeLabel.TabStop = false;
            MaxPoolSizeLabel.Text = "Maximum Pool Size:";
            MaxPoolSizeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // MinPoolSizeLabel
            // 
            MinPoolSizeLabel.AutoSize = true;
            MinPoolSizeLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            MinPoolSizeLabel.Location = new Point(12, 12);
            MinPoolSizeLabel.Name = "MinPoolSizeLabel";
            MinPoolSizeLabel.Size = new Size(122, 17);
            MinPoolSizeLabel.TabIndex = 0;
            MinPoolSizeLabel.TabStop = false;
            MinPoolSizeLabel.Text = "Minimum Pool Size:";
            MinPoolSizeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // MaxPoolSizeText
            // 
            MaxPoolSizeText.Location = new Point(143, 38);
            MaxPoolSizeText.Name = "MaxPoolSizeText";
            MaxPoolSizeText.Size = new Size(114, 25);
            MaxPoolSizeText.TabIndex = 3;
            MaxPoolSizeText.Text = "0";
            // 
            // MinPoolSizeText
            // 
            MinPoolSizeText.Location = new Point(143, 7);
            MinPoolSizeText.Name = "MinPoolSizeText";
            MinPoolSizeText.Size = new Size(114, 25);
            MinPoolSizeText.TabIndex = 1;
            MinPoolSizeText.Text = "0";
            // 
            // DbCheck
            // 
            DbCheck.AutoSize = true;
            DbCheck.Location = new Point(20, 328);
            DbCheck.Name = "DbCheck";
            DbCheck.Size = new Size(123, 21);
            DbCheck.TabIndex = 19;
            DbCheck.Text = "Select Database:";
            DbCheck.UseVisualStyleBackColor = true;
            // 
            // DbList
            // 
            DbList.FormattingEnabled = true;
            DbList.Location = new Point(38, 355);
            DbList.Name = "DbList";
            DbList.Size = new Size(294, 25);
            DbList.TabIndex = 18;
            DbList.Visible = false;
            // 
            // PoolFailIntervalCheck
            // 
            PoolFailIntervalCheck.Location = new Point(151, 290);
            PoolFailIntervalCheck.Name = "PoolFailIntervalCheck";
            PoolFailIntervalCheck.Size = new Size(100, 25);
            PoolFailIntervalCheck.TabIndex = 16;
            // 
            // RetryIntervalText
            // 
            RetryIntervalText.Location = new Point(151, 259);
            RetryIntervalText.Name = "RetryIntervalText";
            RetryIntervalText.Size = new Size(100, 25);
            RetryIntervalText.TabIndex = 15;
            // 
            // RetryCountText
            // 
            RetryCountText.Location = new Point(151, 228);
            RetryCountText.Name = "RetryCountText";
            RetryCountText.Size = new Size(100, 25);
            RetryCountText.TabIndex = 14;
            // 
            // PacketSizeText
            // 
            PacketSizeText.Location = new Point(151, 197);
            PacketSizeText.Name = "PacketSizeText";
            PacketSizeText.Size = new Size(101, 25);
            PacketSizeText.TabIndex = 13;
            // 
            // PollFailLabel
            // 
            PollFailLabel.AutoSize = true;
            PollFailLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PollFailLabel.Location = new Point(20, 296);
            PollFailLabel.Name = "PollFailLabel";
            PollFailLabel.Size = new Size(125, 17);
            PollFailLabel.TabIndex = 12;
            PollFailLabel.TabStop = false;
            PollFailLabel.Text = "Pool &Failure Interval:";
            PollFailLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // RetryIntervalLabel
            // 
            RetryIntervalLabel.AutoSize = true;
            RetryIntervalLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            RetryIntervalLabel.Location = new Point(58, 265);
            RetryIntervalLabel.Name = "RetryIntervalLabel";
            RetryIntervalLabel.Size = new Size(87, 17);
            RetryIntervalLabel.TabIndex = 11;
            RetryIntervalLabel.TabStop = false;
            RetryIntervalLabel.Text = "Retry &Interval:";
            RetryIntervalLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // RetryCountLabel
            // 
            RetryCountLabel.AutoSize = true;
            RetryCountLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            RetryCountLabel.Location = new Point(66, 231);
            RetryCountLabel.Name = "RetryCountLabel";
            RetryCountLabel.Size = new Size(79, 17);
            RetryCountLabel.TabIndex = 10;
            RetryCountLabel.TabStop = false;
            RetryCountLabel.Text = "&Retry Count:";
            RetryCountLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // PacketSizeLabel
            // 
            PacketSizeLabel.AutoSize = true;
            PacketSizeLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PacketSizeLabel.Location = new Point(70, 200);
            PacketSizeLabel.Name = "PacketSizeLabel";
            PacketSizeLabel.Size = new Size(75, 17);
            PacketSizeLabel.TabIndex = 9;
            PacketSizeLabel.TabStop = false;
            PacketSizeLabel.Text = "&Packet Size:";
            PacketSizeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // AllowMultiCheck
            // 
            AllowMultiCheck.AutoSize = true;
            AllowMultiCheck.Location = new Point(20, 80);
            AllowMultiCheck.Name = "AllowMultiCheck";
            AllowMultiCheck.Size = new Size(207, 21);
            AllowMultiCheck.TabIndex = 8;
            AllowMultiCheck.Text = "Allow &Multiple Open Resultsets";
            AllowMultiCheck.UseVisualStyleBackColor = true;
            // 
            // AsynCheck
            // 
            AsynCheck.AutoSize = true;
            AsynCheck.Location = new Point(20, 60);
            AsynCheck.Name = "AsynCheck";
            AsynCheck.Size = new Size(175, 21);
            AsynCheck.TabIndex = 7;
            AsynCheck.Text = "&Asynchronous Processing";
            AsynCheck.UseVisualStyleBackColor = true;
            // 
            // PoolingCheck
            // 
            PoolingCheck.AutoSize = true;
            PoolingCheck.Location = new Point(20, 100);
            PoolingCheck.Name = "PoolingCheck";
            PoolingCheck.Size = new Size(169, 21);
            PoolingCheck.TabIndex = 2;
            PoolingCheck.Text = "Use &Connection Pooling:";
            PoolingCheck.UseVisualStyleBackColor = true;
            // 
            // EnlistCheck
            // 
            EnlistCheck.AutoSize = true;
            EnlistCheck.Location = new Point(20, 40);
            EnlistCheck.Name = "EnlistCheck";
            EnlistCheck.Size = new Size(278, 21);
            EnlistCheck.TabIndex = 1;
            EnlistCheck.Text = "&Enlist Connection In Distributed Transaction";
            EnlistCheck.UseVisualStyleBackColor = true;
            // 
            // PersistCheck
            // 
            PersistCheck.AutoSize = true;
            PersistCheck.Location = new Point(20, 20);
            PersistCheck.Name = "PersistCheck";
            PersistCheck.Size = new Size(140, 21);
            PersistCheck.TabIndex = 0;
            PersistCheck.Text = "Persist &Security Info";
            PersistCheck.UseVisualStyleBackColor = true;
            // 
            // ErrorProvider
            // 
            ErrorProvider.ContainerControl = this;
            // 
            // TestButton
            // 
            TestButton.BorderWidth = 1;
            TestButton.Checked = false;
            TestButton.HoverBorderColor = Color.Gray;
            TestButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            TestButton.HoverEndColor = Color.FromArgb(224, 224, 224);
            TestButton.HoverFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            TestButton.HoverForeColor = Color.Black;
            TestButton.HoverStartColor = Color.FromArgb(218, 194, 204);
            TestButton.Image = Properties.Resources.Parse16;
            TestButton.ImageAlign = ContentAlignment.MiddleLeft;
            TestButton.Location = new Point(68, 422);
            TestButton.Name = "TestButton";
            TestButton.NormalBorderColor = Color.Gray;
            TestButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            TestButton.NormalEndColor = Color.Silver;
            TestButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            TestButton.NormalForeColor = Color.Black;
            TestButton.NormalStartColor = Color.FromArgb(248, 248, 248);
            TestButton.PressedBorderColor = Color.Gray;
            TestButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            TestButton.PressedEndColor = Color.FromArgb(174, 45, 61);
            TestButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            TestButton.PressedForeColor = Color.White;
            TestButton.PressedStartColor = Color.Gray;
            TestButton.Size = new Size(96, 32);
            TestButton.TabIndex = 1;
            TestButton.Text = "&Test";
            TestButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            ttp.SetToolTip(TestButton, "Test the specified SQL Server connection settings.");
            TestButton.UseVisualStyleBackColor = true;
            // 
            // ConnectButton
            // 
            ConnectButton.BorderWidth = 1;
            ConnectButton.Checked = false;
            ConnectButton.HoverBorderColor = Color.Gray;
            ConnectButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            ConnectButton.HoverEndColor = Color.FromArgb(224, 224, 224);
            ConnectButton.HoverFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ConnectButton.HoverForeColor = Color.Black;
            ConnectButton.HoverStartColor = Color.FromArgb(218, 194, 204);
            ConnectButton.Image = Properties.Resources.Connect;
            ConnectButton.ImageAlign = ContentAlignment.MiddleLeft;
            ConnectButton.Location = new Point(170, 422);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.NormalBorderColor = Color.Gray;
            ConnectButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            ConnectButton.NormalEndColor = Color.Silver;
            ConnectButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ConnectButton.NormalForeColor = Color.Black;
            ConnectButton.NormalStartColor = Color.FromArgb(248, 248, 248);
            ConnectButton.PressedBorderColor = Color.Gray;
            ConnectButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            ConnectButton.PressedEndColor = Color.FromArgb(174, 45, 61);
            ConnectButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ConnectButton.PressedForeColor = Color.White;
            ConnectButton.PressedStartColor = Color.Gray;
            ConnectButton.Size = new Size(96, 32);
            ConnectButton.TabIndex = 2;
            ConnectButton.Text = "&Connect";
            ConnectButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            ttp.SetToolTip(ConnectButton, "Connect to SQL Server with the specified settings.");
            ConnectButton.UseVisualStyleBackColor = true;
            // 
            // CloseButton
            // 
            CloseButton.BorderWidth = 1;
            CloseButton.Checked = false;
            CloseButton.HoverBorderColor = Color.Gray;
            CloseButton.HoverDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            CloseButton.HoverEndColor = Color.FromArgb(224, 224, 224);
            CloseButton.HoverFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CloseButton.HoverForeColor = Color.Black;
            CloseButton.HoverStartColor = Color.FromArgb(218, 194, 204);
            CloseButton.Image = Properties.Resources.CloseButton_Image;
            CloseButton.ImageAlign = ContentAlignment.MiddleLeft;
            CloseButton.Location = new Point(272, 422);
            CloseButton.Name = "CloseButton";
            CloseButton.NormalBorderColor = Color.Gray;
            CloseButton.NormalDirection = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            CloseButton.NormalEndColor = Color.Silver;
            CloseButton.NormalFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CloseButton.NormalForeColor = Color.Black;
            CloseButton.NormalStartColor = Color.FromArgb(248, 248, 248);
            CloseButton.PressedBorderColor = Color.Gray;
            CloseButton.PressedDirection = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            CloseButton.PressedEndColor = Color.FromArgb(174, 45, 61);
            CloseButton.PressedFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CloseButton.PressedForeColor = Color.White;
            CloseButton.PressedStartColor = Color.Gray;
            CloseButton.Size = new Size(96, 32);
            CloseButton.TabIndex = 3;
            CloseButton.Text = "Cancel";
            CloseButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            ttp.SetToolTip(CloseButton, "Cancel the connection and close this window.");
            CloseButton.UseVisualStyleBackColor = true;
            // 
            // SqlTitleLabel
            // 
            SqlTitleLabel.Dock = DockStyle.Top;
            SqlTitleLabel.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            SqlTitleLabel.Location = new Point(3, 50);
            SqlTitleLabel.Name = "SqlTitleLabel";
            SqlTitleLabel.Padding = new Padding(0, 50, 0, 0);
            SqlTitleLabel.Size = new Size(358, 36);
            SqlTitleLabel.TabIndex = 2;
            SqlTitleLabel.TabStop = false;
            SqlTitleLabel.Text = "SQL Server";
            SqlTitleLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // ConnectDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(375, 461);
            ControlBox = false;
            Controls.Add(CloseButton);
            Controls.Add(ConnectButton);
            Controls.Add(TestButton);
            Controls.Add(Tabs);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SqlServerConnectionDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connect To SQL Server";
            Tabs.ResumeLayout(false);
            LoginPage.ResumeLayout(false);
            ConnectPanel.ResumeLayout(false);
            ConnectPanel.PerformLayout();
            LoginPanel.ResumeLayout(false);
            LoginPanel.PerformLayout();
            OptionsPage.ResumeLayout(false);
            OptionsPage.PerformLayout();
            PoolingPanel.ResumeLayout(false);
            PoolingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl Tabs;
        private TabPage LoginPage;
        private TabPage OptionsPage;
        private Panel ConnectPanel;
        private Panel LoginPanel;
        private PasswordTextBox PasswordText;
        private CheckBox RememberCheck;
        private TextBox UserIdText;
        private AdvancedLabel PasswordLabel;
        private AdvancedLabel UserIdLabel;
        private ComboBox AuthTypeList;
        private AdvancedLabel AuthTypeLabel;
        private TextBox ServerText;
        private AdvancedLabel ServerNameLabel;
        private CheckBox AllowMultiCheck;
        private CheckBox AsynCheck;
        private IntegerTextBox MaxPoolSizeText;
        private IntegerTextBox MinPoolSizeText;
        private CheckBox PoolingCheck;
        private CheckBox EnlistCheck;
        private CheckBox PersistCheck;
        private AdvancedLabel PollFailLabel;
        private AdvancedLabel RetryIntervalLabel;
        private AdvancedLabel RetryCountLabel;
        private AdvancedLabel PacketSizeLabel;
        private CheckBox DbCheck;
        private ComboBox DbList;
        private IntegerTextBox PoolFailIntervalCheck;
        private IntegerTextBox RetryIntervalText;
        private IntegerTextBox RetryCountText;
        private IntegerTextBox PacketSizeText;
        private ErrorProvider ErrorProvider;
        private ToolTip ttp;
        private Panel PoolingPanel;
        private AdvancedLabel MaxPoolSizeLabel;
        private AdvancedLabel MinPoolSizeLabel;
        private AIButton CloseButton;
        private AIButton ConnectButton;
        private AIButton TestButton;
        private AdvancedLabel SqlTitleLabel;
    }
}