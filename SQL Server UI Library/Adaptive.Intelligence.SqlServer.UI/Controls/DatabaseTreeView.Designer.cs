namespace Adaptive.Intelligence.SqlServer.UI
{
    partial class DatabaseTreeView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseTreeView));
			StandardImageList = new ImageList(components);
			TableMenu = new ContextMenuStrip(components);
			TableMenuGenGetAll = new ToolStripMenuItem();
			TableMenuGenGetById = new ToolStripMenuItem();
			TableMenuGenInsert = new ToolStripMenuItem();
			TableMenuGenUpdate = new ToolStripMenuItem();
			TableMenuGenDelete = new ToolStripMenuItem();
			TableMenuGenSpDiv = new ToolStripSeparator();
			TableMenuGenAllSp = new ToolStripMenuItem();
			TableMenuGenAllDiv = new ToolStripSeparator();
			TableMenuGenDataDefinition = new ToolStripMenuItem();
			TableMenuGenDataAccess = new ToolStripMenuItem();
			TableMenuGenClassesDiv = new ToolStripSeparator();
			TableMenuGenEditProfile = new ToolStripMenuItem();
			TableMenuGenEditDiv = new ToolStripSeparator();
			TableMenuScriptCreate = new ToolStripMenuItem();
			SpMenu = new ContextMenuStrip(components);
			SpMenuGenerate = new ToolStripMenuItem();
			SpMenuEdit = new ToolStripMenuItem();
			SpMenuDelete = new ToolStripMenuItem();
			TableMenu.SuspendLayout();
			SpMenu.SuspendLayout();
			SuspendLayout();
			// 
			// StandardImageList
			// 
			StandardImageList.ColorDepth = ColorDepth.Depth32Bit;
			StandardImageList.ImageStream = (ImageListStreamer)resources.GetObject("StandardImageList.ImageStream");
			StandardImageList.TransparentColor = Color.Transparent;
			StandardImageList.Images.SetKeyName(0, "Database TP.png");
			StandardImageList.Images.SetKeyName(1, "Folder.png");
			StandardImageList.Images.SetKeyName(2, "Table.png");
			StandardImageList.Images.SetKeyName(3, "Column.png");
			StandardImageList.Images.SetKeyName(4, "StoredProc TP.png");
			StandardImageList.Images.SetKeyName(5, "MissingStoredProc.png");
			StandardImageList.Images.SetKeyName(6, "Server 16.png");
			// 
			// TableMenu
			// 
			TableMenu.Items.AddRange(new ToolStripItem[] { TableMenuGenGetAll, TableMenuGenGetById, TableMenuGenInsert, TableMenuGenUpdate, TableMenuGenDelete, TableMenuGenSpDiv, TableMenuGenAllSp, TableMenuGenAllDiv, TableMenuGenDataDefinition, TableMenuGenDataAccess, TableMenuGenClassesDiv, TableMenuGenEditProfile, TableMenuGenEditDiv, TableMenuScriptCreate });
			TableMenu.Name = "CMenu";
			TableMenu.Size = new Size(312, 270);
			// 
			// TableMenuGenGetAll
			// 
			TableMenuGenGetAll.Name = "TableMenuGenGetAll";
			TableMenuGenGetAll.Size = new Size(311, 22);
			TableMenuGenGetAll.Text = "Generate Get All Stored Procedure";
			TableMenuGenGetAll.ToolTipText = "Generates the standard CRUD stored procedure for retrieving all records from the table.";
			// 
			// TableMenuGenGetById
			// 
			TableMenuGenGetById.Name = "TableMenuGenGetById";
			TableMenuGenGetById.Size = new Size(311, 22);
			TableMenuGenGetById.Text = "Generate Get By Id Stored Procedure";
			TableMenuGenGetById.ToolTipText = "Generates the standard CRUD stored procedure for retrieving a record from the table by ID value.";
			// 
			// TableMenuGenInsert
			// 
			TableMenuGenInsert.Name = "TableMenuGenInsert";
			TableMenuGenInsert.Size = new Size(311, 22);
			TableMenuGenInsert.Text = "Generate Insert Stored Procedure";
			TableMenuGenInsert.ToolTipText = "Generates the standard CRUD stored procedure for inserting new records.";
			// 
			// TableMenuGenUpdate
			// 
			TableMenuGenUpdate.Name = "TableMenuGenUpdate";
			TableMenuGenUpdate.Size = new Size(311, 22);
			TableMenuGenUpdate.Text = "Generate Update Stored Procedure";
			TableMenuGenUpdate.ToolTipText = "Generates the standard CRUD stored procedure for updating a record.";
			// 
			// TableMenuGenDelete
			// 
			TableMenuGenDelete.Name = "TableMenuGenDelete";
			TableMenuGenDelete.Size = new Size(311, 22);
			TableMenuGenDelete.Text = "Generate Delete Stored Procedure";
			TableMenuGenDelete.ToolTipText = "Generates the standard CRUD stored procedure for marking a record as deleted.";
			// 
			// TableMenuGenSpDiv
			// 
			TableMenuGenSpDiv.Name = "TableMenuGenSpDiv";
			TableMenuGenSpDiv.Size = new Size(308, 6);
			// 
			// TableMenuGenAllSp
			// 
			TableMenuGenAllSp.Name = "TableMenuGenAllSp";
			TableMenuGenAllSp.Size = new Size(311, 22);
			TableMenuGenAllSp.Text = "Generate All Stored Procedures";
			TableMenuGenAllSp.ToolTipText = "Generates all of the standard CRUD stored procedures";
			// 
			// TableMenuGenAllDiv
			// 
			TableMenuGenAllDiv.Name = "TableMenuGenAllDiv";
			TableMenuGenAllDiv.Size = new Size(308, 6);
			// 
			// TableMenuGenDataDefinition
			// 
			TableMenuGenDataDefinition.Name = "TableMenuGenDataDefinition";
			TableMenuGenDataDefinition.Size = new Size(311, 22);
			TableMenuGenDataDefinition.Text = "Generate Data Definition Class";
			TableMenuGenDataDefinition.ToolTipText = "Generates the data definition C# class representing a record in the table.";
			// 
			// TableMenuGenDataAccess
			// 
			TableMenuGenDataAccess.Name = "TableMenuGenDataAccess";
			TableMenuGenDataAccess.Size = new Size(311, 22);
			TableMenuGenDataAccess.Text = "Generate Data Access Class";
			TableMenuGenDataAccess.ToolTipText = "Generates the standard Data Access class for this table.";
			// 
			// TableMenuGenClassesDiv
			// 
			TableMenuGenClassesDiv.Name = "TableMenuGenClassesDiv";
			TableMenuGenClassesDiv.Size = new Size(308, 6);
			// 
			// TableMenuGenEditProfile
			// 
			TableMenuGenEditProfile.Name = "TableMenuGenEditProfile";
			TableMenuGenEditProfile.Size = new Size(311, 22);
			TableMenuGenEditProfile.Text = "Edit Table Profile...";
			TableMenuGenEditProfile.ToolTipText = "Edit the meta data profile for this table.";
			// 
			// TableMenuGenEditDiv
			// 
			TableMenuGenEditDiv.Name = "TableMenuGenEditDiv";
			TableMenuGenEditDiv.Size = new Size(308, 6);
			// 
			// TableMenuScriptCreate
			// 
			TableMenuScriptCreate.Name = "TableMenuScriptCreate";
			TableMenuScriptCreate.Size = new Size(311, 22);
			TableMenuScriptCreate.Text = "Script CREATE TABLE";
			// 
			// SpMenu
			// 
			SpMenu.Items.AddRange(new ToolStripItem[] { SpMenuGenerate, SpMenuEdit, SpMenuDelete });
			SpMenu.Name = "SpMenu";
			SpMenu.Size = new Size(225, 70);
			// 
			// SpMenuGenerate
			// 
			SpMenuGenerate.Name = "SpMenuGenerate";
			SpMenuGenerate.Size = new Size(224, 22);
			SpMenuGenerate.Text = "Generate Stored Procedure";
			// 
			// SpMenuEdit
			// 
			SpMenuEdit.Name = "SpMenuEdit";
			SpMenuEdit.Size = new Size(224, 22);
			SpMenuEdit.Text = "View / Edit Stored Procedure";
			// 
			// SpMenuDelete
			// 
			SpMenuDelete.Name = "SpMenuDelete";
			SpMenuDelete.Size = new Size(224, 22);
			SpMenuDelete.Text = "Delete Stored Procedure";
			// 
			// DatabaseTreeView
			// 
			ImageIndex = 0;
			ImageList = StandardImageList;
			LineColor = Color.Black;
			SelectedImageIndex = 0;
			TableMenu.ResumeLayout(false);
			SpMenu.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.ImageList StandardImageList;
        private System.Windows.Forms.ContextMenuStrip TableMenu;
        private System.Windows.Forms.ToolStripMenuItem TableMenuGenInsert;
        private System.Windows.Forms.ToolStripMenuItem TableMenuGenUpdate;
        private System.Windows.Forms.ToolStripMenuItem TableMenuGenDelete;
        private System.Windows.Forms.ToolStripMenuItem TableMenuGenGetAll;
        private System.Windows.Forms.ToolStripMenuItem TableMenuGenGetById;
        private System.Windows.Forms.ToolStripMenuItem TableMenuGenAllSp;
        private System.Windows.Forms.ToolStripMenuItem TableMenuGenDataDefinition;
        private System.Windows.Forms.ToolStripMenuItem TableMenuGenDataAccess;
        private System.Windows.Forms.ToolStripSeparator TableMenuGenSpDiv;
        private System.Windows.Forms.ToolStripSeparator TableMenuGenAllDiv;
        private System.Windows.Forms.ToolStripSeparator TableMenuGenClassesDiv;
        private System.Windows.Forms.ToolStripMenuItem TableMenuGenEditProfile;
        private System.Windows.Forms.ContextMenuStrip SpMenu;
        private System.Windows.Forms.ToolStripMenuItem SpMenuGenerate;
        private System.Windows.Forms.ToolStripMenuItem SpMenuEdit;
        private System.Windows.Forms.ToolStripMenuItem SpMenuDelete;
        private System.Windows.Forms.ToolStripSeparator TableMenuGenEditDiv;
        private System.Windows.Forms.ToolStripMenuItem TableMenuScriptCreate;
    }
}