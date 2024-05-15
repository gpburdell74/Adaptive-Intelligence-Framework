using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.UI;
using Adaptive.Intelligence.SqlServer.Analysis;
using Adaptive.Intelligence.SqlServer.CodeDom;
using Adaptive.Intelligence.SqlServer.CodeDom.CodeProvider;
using Adaptive.Intelligence.SqlServer.CodeDom.IO;
using Adaptive.Intelligence.SqlServer.Schema;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Adaptive.Intelligence.SqlServer.UI
{
    /// <summary>
    /// Defines a specialized TreeView control for displaying the object hierarchy in a database.
    /// </summary>
    /// <seealso cref="TreeView" />
    /// <seealso cref="AdaptiveTreeView" />
    public partial class DatabaseTreeView : AdaptiveTreeView
    {
        #region Public Events        
        /// <summary>
        /// Occurs when updating the loading progress.
        /// </summary>
        public event ProgressUpdateEventHandler? ProgressUpdate;
        #endregion

        #region Private Member Declarations

        #region Private Constants
        // Tree Node Objects Names

        private const string NodeNameServer = "RootServer";
        private const string NodeNameDatabasesFolder = "DatabasesFolder";
        private const string NodeNameDatabase = "Database";
        private const string NodeNameTablesFolder = "TablesFolder";
        private const string NodeNameColumnsFolder = "ColumnsFolder";
        private const string NodeNameColumn = "Column";
        private const string NodeNameSpFolder = "ProceduresFolder";

        private const string NodeTextDatabasesFolder = "Databases";
        private const string NodeTextTablesFolder = "Tables";
        private const string NodeTextColumnsFolder = "Columns";
        private const string NodeTextSpFolder = "Standard Procedures";

        private const string StdSpNameInsert = "Insert";
        private const string StdSpNameUpdate = "Update";
        private const string StdSpNameDelete = "Delete";
        private const string StdSpNameGetById = "GetById";
        private const string StdSpNameGetAll = "GetAll";
        private const string StdSpNameGetForCustomer = "GetForCustomer";

        // Image Indexes

        /// <summary>
        /// The image index for the database node.
        /// </summary>
        private const int TreeNodeImageServerIndex = 6;
        /// <summary>
        /// The image index for the database node.
        /// </summary>
        private const int TreeNodeImageDatabaseIndex = 0;
        /// <summary>
        /// The image index for folder nodes.
        /// </summary>
        private const int TreeNodeImageFolderIndex = 1;
        /// <summary>
        /// The image index for SQL table column.
        /// </summary>
        private const int TreeNodeImageTableIndex = 2;
        /// <summary>
        /// The image index for column nodes.
        /// </summary>
        private const int TreeNodeImageColumnIndex = 3;
        /// <summary>
        /// The image index for stored procedure nodes.
        /// </summary>
        private const int TreeNodeImageSpIndex = 4;
        /// <summary>
        /// The image index for missing stored procedure nodes.
        /// </summary>
        private const int TreeNodeImageMissingSpIndex = 5;
        #endregion

        /// <summary>
        /// The reference to the database server.
        /// </summary>
        private Schema.SqlServer? _server;
        /// <summary>
        /// The reference to the table profiles and metadata.
        /// </summary>
        private AdaptiveTableMetadata? _tableData;
        #endregion

        #region Constructor / Dispose Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTreeView"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public DatabaseTreeView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                components?.Dispose();
            }

            _server = null;
            _tableData = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the name of the currently selected database.
        /// </summary>
        /// <value>
        /// A string containing the name of the currently selected database.
        /// </value>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedDatabase
        {
            get
            {
                if (SelectedNode == null)
                    return string.Empty;
                else
                {
                    string nodeText = SelectedNode.Text;
                    object item = SelectedNode.Tag;
                    if (item is Schema.SqlServer || nodeText == "Databases")
                        return "master";
                    else if (item is SqlDatabase)
                        return ((SqlDatabase)item).Name;
                    else
                    {
                        TreeNode ptr = SelectedNode;
                        do
                        {
                            ptr = ptr.Parent;
                        } while (ptr != null && !(ptr.Tag is SqlDatabase));

                        if (ptr != null)
                            return ((SqlDatabase)ptr.Tag).Name;
                        else
                            return "master";
                    }

                }
            }
        }
        /// <summary>
        /// Gets or sets the reference to the SQL server database schema information.
        /// </summary>
        /// <value>
        /// The <see cref="SqlDatabase"/> instance to reference.
        /// </value>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Schema.SqlServer? Server
        {
            get => _server;
            set
            {
                _server?.Dispose();
                GC.Collect();
                _server = value;
                if (!Process.GetCurrentProcess().ProcessName.Contains("devenv"))
                {
                    ContinueInMainThread(SetDbContent);
                }
                SetState();
            }
        }
        /// <summary>
        /// Gets or sets the reference to the table metadata container.
        /// </summary>
        /// <value>
        /// The <see cref="AdaptiveTableMetadata"/> instance to reference.
        /// </value>
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AdaptiveTableMetadata? TableData
        {
            get => _tableData;
            set => _tableData = value;
        }
        #endregion

        #region Protected Method Overrides
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.AfterSelect" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data.</param>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);
            if (e.Node == null)
            {
                TableMenu.Enabled = false;
                SpMenu.Enabled = false;
            }
            else
            {
                TableMenu.Enabled = (e.Node.Tag is SqlTable);
                SpMenu.Enabled = (e.Node.Tag is SqlStoredProcedure);
            }
        }
        #endregion

        #region Private Event Handlers
        /// <summary>
        /// Handles the event when the stored procedure menu is being opened.
        /// </summary>
        /// <param name="sender">
        /// The object raising the event.
        /// </param>
        /// <param name="e">
        /// An <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void HandleSpMenuOpening(object? sender, EventArgs e)
        {
            // Show the menu item only if the SP is currently missing.
            TreeNode tn = SelectedNode;
            SqlStoredProcedure? sp = tn.Tag as SqlStoredProcedure;
            SpMenuGenerate.Enabled = (sp == null);
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Removes any tree nodes that reference this procedure object.
        /// </summary>
        /// <param name="procedure">
        /// The <see cref="SqlStoredProcedure"/> instance to be removed.
        /// </param>
        public void DropStoredProcedureNode(SqlStoredProcedure procedure)
        {
            if (Nodes.Count > 0)
            {
                TreeNode dbNode = Nodes[0];
                if (dbNode.Nodes.Count > 0)
                {
                    TreeNode tableFolderNode = dbNode.Nodes[0];

                    foreach (TreeNode tableNode in tableFolderNode.Nodes)
                    {
                        if (tableNode.Nodes.Count > 1)
                        {
                            TreeNode spFolderNode = tableNode.Nodes[1];
                            if (spFolderNode != null)
                            {
                                foreach (TreeNode spNode in spFolderNode.Nodes)
                                {
                                    if (spNode.Tag is SqlStoredProcedure localSp && localSp.Name == procedure.Name)
                                    {
                                        spNode.Text = spNode.Text + @" (Missing)";
                                        spNode.Tag = null;
                                        spNode.ImageIndex = TreeNodeImageMissingSpIndex;
                                        spNode.SelectedImageIndex = TreeNodeImageMissingSpIndex;
                                        spNode.ToolTipText = "Stored procedure not yet defined.";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Updates the table data.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance.
        /// </param>
        /// <param name="tableProfile">
        /// The <see cref="AdaptiveTableProfile"/> instance containing the table profile and other
        /// related metadata to be updated.
        /// </param>
        public void UpdateTableData(SqlTable table, AdaptiveTableProfile tableProfile)
        {
            if (table != null && tableProfile != null)
            {
                BeginUpdate();

                // Find the table list.
                TreeNodeCollection tableNodes = Nodes[0].Nodes[0].Nodes;
                TreeNode[] tableNodeList = tableNodes.Find(table.TableName, false);
                if (tableNodeList != null && tableNodeList.Length > 0)
                {
                    TreeNode tableNode = tableNodeList[0];
                    TreeNode columnsNode = tableNode.Nodes[0];
                    TreeNode procsNode = tableNode.Nodes[1];

                    // Remove the old entries.
                    procsNode.Nodes.Clear();

                    // Add an SP node for each of the standard stored procedures.
                    AddSpNode(tableProfile, procsNode, StdSpNameInsert);
                    AddSpNode(tableProfile, procsNode, StdSpNameUpdate);
                    AddSpNode(tableProfile, procsNode, StdSpNameDelete);
                    AddSpNode(tableProfile, procsNode, StdSpNameGetById);
                    AddSpNode(tableProfile, procsNode, StdSpNameGetAll);
                    AddSpNode(tableProfile, procsNode, StdSpNameGetForCustomer);
                }
                System.Diagnostics.Debug.WriteLine(table.TableName);
                EndUpdate();
                Invalidate();
            }
        }
        #endregion

        #region Protected Method Overrides
        /// <summary>
        /// Assigns the event handlers.
        /// </summary>
        protected override void AssignEventHandlers()
        {
        }
        /// <summary>
        /// Removes the event handlers.
        /// </summary>
        protected override void RemoveEventHandlers()
        {
        }
        /// <summary>
        /// Sets the state of the UI before performing a load operation.
        /// </summary>
        private void SetPreloadState()
        {
            Cursor = Cursors.WaitCursor;
            Nodes.Clear();
            SuspendLayout();
            Enabled = false;
            Application.DoEvents();
        }
        /// <summary>
        /// Sets the state of the UI after performing a load operation.
        /// </summary>
        private void SetPostloadState()
        {
            Enabled = true;
            ResumeLayout();
            Cursor = Cursors.Default;
        }
        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Updates the control with the content from the schema reference.
        /// </summary>
        private void SetDbContent()
        {
            SetPreloadState();
            Visible = false;
            BeginUpdate();

            // Clear current content.
            Nodes.Clear();

            // Temporarily remove menu event handlers.
            RemoveEventHandlers();
            SpMenu.Opening -= HandleSpMenuOpening;

            // Create the root database node.
            if (_server == null)
                Nodes.Clear();
            else
            {
                OnProgressUpdate(new ProgressUpdateEventArgs("Server", 0));
                TreeNode serverNode = new TreeNode
                {
                    Name = NodeNameServer,
                    Tag = _server,
                    ImageIndex = TreeNodeImageServerIndex,
                    SelectedImageIndex = TreeNodeImageServerIndex,
                    Text = _server.Name!,
                    ToolTipText = "The SQL Server Instance: " + _server.Name
                };
                Nodes.Add(serverNode);

                // Add the databases folder node.
                TreeNode databasesFolderNode = new TreeNode
                {
                    Name = NodeNameDatabasesFolder,
                    Tag = _server.Databases,
                    ImageIndex = TreeNodeImageFolderIndex,
                    SelectedImageIndex = TreeNodeImageFolderIndex,
                    Text = NodeTextDatabasesFolder,
                    ToolTipText = NodeTextDatabasesFolder
                };
                serverNode.Nodes.Add(databasesFolderNode);

                int total = _server.Databases.Count;
                int count = 0;
                foreach (SqlDatabase db in _server.Databases)
                {
                    count++;
                    OnProgressUpdate(new ProgressUpdateEventArgs(db.Name, Math.Percent(count, total)));
                    TreeNode dbNode = new TreeNode
                    {
                        Name = NodeNameDatabase + db.Name,
                        Tag = db,
                        ImageIndex = TreeNodeImageDatabaseIndex,
                        SelectedImageIndex = TreeNodeImageDatabaseIndex,
                        Text = db.Name,
                        ToolTipText = "The source SQL Server database instance: " + db.Name
                    };
                    databasesFolderNode.Nodes.Add(dbNode);

                    // Add the tables folder node.
                    TreeNode tablesFolderNode = new TreeNode
                    {
                        Name = NodeNameTablesFolder,
                        Tag = db.Tables,
                        ImageIndex = TreeNodeImageFolderIndex,
                        SelectedImageIndex = TreeNodeImageFolderIndex,
                        Text = NodeTextTablesFolder,
                        ToolTipText = NodeTextTablesFolder
                    };
                    dbNode.Nodes.Add(tablesFolderNode);

                    // Add all the table and stored procedure sub-nodes.
                    if (db.Tables != null)
                    {
                        foreach (SqlTable table in db.Tables)
                        {
                            AddTableNode(tablesFolderNode, table);
                        }
                    }
                }
            }

            // Re-assign menu event handlers.
            SpMenu.Opening += HandleSpMenuOpening;

            EndUpdate();
            Visible = true;
            SetPostloadState();
        }
        /// <summary>
        /// Adds the tree node for the specified table.
        /// </summary>
        /// <param name="parentNode">
        /// The parent <see cref="TreeNode"/> instance.
        /// </param>
        /// <param name="table">
        /// The <see cref="SqlTable"/> instance being represented by the tree node.
        /// </param>
        private void AddTableNode(TreeNode parentNode, SqlTable table)
        {
            // Create the table tree node.
            TreeNode tableNode = new TreeNode
            {
                Name = table.TableName!,
                Tag = table,
                ImageIndex = TreeNodeImageFolderIndex,
                SelectedImageIndex = TreeNodeImageFolderIndex,
                Text = table.TableName!,
                ToolTipText = table.TableName
            };
            tableNode.ContextMenuStrip = TableMenu;
            parentNode.Nodes.Add(tableNode);

            // Add the columns folder node for that table.
            TreeNode columnsNode = new TreeNode
            {
                Name = table.TableName + NodeNameColumnsFolder,
                Tag = table.Columns,
                ImageIndex = 1,
                SelectedImageIndex = 1,
                Text = NodeTextColumnsFolder,
                ToolTipText = NodeTextColumnsFolder
            };
            tableNode.Nodes.Add(columnsNode);

            // For each column,  add a column node.
            TransactSqlCodeProvider provider = new TransactSqlCodeProvider();
            foreach (SqlColumn col in table.Columns)
            {
                AddColumnNode(columnsNode, col, provider);
            }
            provider.Dispose();
        }
        /// <summary>
        /// Adds the tree node for the specified column.
        /// </summary>
        /// <param name="parentNode">
        /// The parent <see cref="TreeNode"/> instance.
        /// </param>
        /// <param name="column">
        /// The <see cref="SqlColumn"/> instance being represented by the tree node.
        /// </param>
        /// <param name="provider">
        /// The <see cref="TransactSqlCodeProvider"/> instance to use to render tool tip information.
        /// </param>
        private void AddColumnNode(TreeNode parentNode, SqlColumn column, TransactSqlCodeProvider provider)
        {
            // Create the SQL writer for the tool tip.
            StringBuilder builder = new StringBuilder();
            SqlWriter writer = new SqlWriter(provider, builder);

            // Create the Code DOM instance for the column's data type.
            SqlCodeDataTypeSpecificationExpression expression = new SqlCodeDataTypeSpecificationExpression(
                (SqlDataTypes)column.TypeId,
                column.MaxLength,
                column.IsNullable,
                column.Precision,
                column.Scale,
                column.IsAnsiPadded);

            // Generate the T-SQL for the tool tip value.
            writer.WriteExpression(expression);
            string toolTip = builder.ToString();
            writer.Dispose();
            builder.Clear();

            // Create and add the column node.
            TreeNode columnNode = new TreeNode
            {
                Name = column.ColumnName + NodeNameColumn,
                Tag = column,
                ImageIndex = TreeNodeImageColumnIndex,
                SelectedImageIndex = TreeNodeImageColumnIndex,
                Text = column.ColumnName,
                ToolTipText = toolTip
            };
            parentNode.Nodes.Add(columnNode);

        }
        /// <summary>
        /// Adds the tree node for the specified standard stored procedure.
        /// </summary>
        /// <param name="profile">
        /// The <see cref="AdaptiveTableProfile"/> instance representing the meta data for
        /// the parent table.
        /// </param>
        /// <param name="procedureFolderNode">
        /// The <see cref="TreeNode"/> instance representing the Standard Procedures folder node
        /// for the table.
        /// </param>
        /// <param name="procedureName">
        /// A string containing the stored procedure name.
        /// </param>
        private void AddSpNode(AdaptiveTableProfile profile, TreeNode procedureFolderNode, string procedureName)
        {
            // Create the new node object, and try to find the procedure schema object, if it exists.
            TreeNode itemNode = new TreeNode();
            SqlStoredProcedure procedure =
                profile.StandardStoredProcedures.GetItemByFunction(procedureName);

            // Add a tree node for the SP, present or not.
            if (procedure != null)
            {
                // Represent the existing SP definition.
                // "missing" stored procedure; otherwise, represent the stored procedure.
                itemNode.Text = procedureName;
                itemNode.Tag = procedure;
                itemNode.ImageIndex = TreeNodeImageSpIndex;
                itemNode.ImageIndex = TreeNodeImageSpIndex;
                itemNode.ToolTipText = procedure.Name;
            }
            else
            {
                // If the procedure does not exist in the database, set the node to represent a
                // "missing" stored procedure; otherwise, represent the stored procedure.
                itemNode.Text = procedureName + @" (Missing)";
                itemNode.Tag = null;
                itemNode.ImageIndex = TreeNodeImageMissingSpIndex;
                itemNode.SelectedImageIndex = TreeNodeImageMissingSpIndex;
                itemNode.ToolTipText = "Stored procedure not yet defined.";
            }

            // Add the context menu reference and add the node to the tree.
            itemNode.ContextMenuStrip = SpMenu;
            procedureFolderNode.Nodes.Add(itemNode);
        }
        /// <summary>
        /// Creates the table event arguments instance for the currently selected table node.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlTableEventArgs"/> instance.
        /// </returns>
        private SqlTableEventArgs CreateTableEventArguments()
        {
            TreeNode selectedNode = SelectedNode;
            SqlTable? table = null;

            if (selectedNode != null && selectedNode.Tag != null)
            {
                table = (selectedNode.Tag as SqlTable);
                if (table == null)
                {
                    if (selectedNode.Tag is SqlStoredProcedure)
                    {
                        table = selectedNode.Parent.Parent.Tag as SqlTable;
                    }
                    else if (selectedNode.Tag is SqlStoredProcedureCollection)
                        table = selectedNode.Parent.Tag as SqlTable;
                }
            }

            return new SqlTableEventArgs(table);
        }
        /// <summary>
        /// Creates the stored procedure event arguments instance for the currently selected
        /// stored procedure node.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlStoredProcedureEventArgs"/> instance.
        /// </returns>
        private SqlStoredProcedureEventArgs CreateStoredProcedureEventArguments()
        {
            TreeNode selectedNode = SelectedNode;
            SqlStoredProcedure? procedure = null;

            if (selectedNode.Tag != null)
            {
                procedure = (selectedNode.Tag as SqlStoredProcedure);
            }
            return new SqlStoredProcedureEventArgs(procedure);
        }
        #endregion

        private void OnProgressUpdate(ProgressUpdateEventArgs e)
        {
            ContinueInMainThread(() =>
            {
                ProgressUpdate?.Invoke(this, e);
            });
        }

    }
}
