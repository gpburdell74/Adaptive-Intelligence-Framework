// Ignore Spelling: Sql
// Ignore Spelling: ORM

using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using System.Data;

namespace Adaptive.Intelligence.SqlServer.ORM
{
#pragma warning disable S2292
	/// <summary>
	/// Contains a list of global database options to use when generating T-SQL code.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class OrmDatabaseOptions : DisposableObjectBase
	{
		#region Private Constant Declarations
		private const string DefaultDataAccessClassSuffix = "DataAccess";
		private const string DefaultDefaultQualifiedOwner = "dbo";
		private const string DefaultDeletedColumnName = "Deleted";
		private const string DefaultDeleteStoredProcedureName = "Delete";
		private const string DefaultIdColumnName = "Id";
		private const string DefaultInsertStoredProcedureName = "Insert";
		private const string DefaultQualifiedNameSeparatorOpen = "[";
		private const string DefaultQualifiedNameSeparatorClose = "]";
		private const string DefaultRetrieveAllRecordsStoredProcedureName = "GetAll";
		private const string DefaultRetrieveRecordByIdStoredProcedureName = "GetById";
		private const string DefaultTableNameSuffix = " Table";
		private const string DefaultUpdateStoredProcedureName = "Update";
		private const bool DefaultUseSoftDeletes = true;
		private const bool DefaultUseTableNamesForStoredProcedureNames = true;
		#endregion

		#region Private Member Declarations				
		/// <summary>
		/// The data access class name suffix value.
		/// </summary>
		private string? _dataAccessClassSuffix = DefaultDataAccessClassSuffix;
		/// <summary>
		/// The data access class name suffix value.
		/// </summary>
		private string? _defaultQualifiedOwner = DefaultDefaultQualifiedOwner;
		/// <summary>
		/// The deleted column name
		/// </summary>
		private string? _deletedColumnName = DefaultDeletedColumnName;
		/// <summary>
		/// The default delete stored procedure name.
		/// </summary>
		private string? _deleteStoredProcedureName = DefaultDeleteStoredProcedureName;
		/// <summary>
		/// The identifier column data type
		/// </summary>
		private SqlDbType _idColumnDataType = SqlDbType.UniqueIdentifier;
		/// <summary>
		/// The identifier column name
		/// </summary>
		private string? _idColumnName = DefaultIdColumnName;
		/// <summary>
		/// The default insert stored procedure name.
		/// </summary>
		private string? _insertStoredProcedureName = DefaultInsertStoredProcedureName;
		/// <summary>
		/// The qualified name separator (open) character.
		/// </summary>
		private string? _qualifiedNameSeparatorOpen = DefaultQualifiedNameSeparatorOpen;
		/// <summary>
		/// The qualified name separator (close) character.
		/// </summary>
		private string? _qualifiedNameSeparatorClose = DefaultQualifiedNameSeparatorClose;
		/// <summary>
		/// The retrieve all records stored procedure name
		/// </summary>
		private string? _retrieveAllRecordsStoredProcedureName = DefaultRetrieveAllRecordsStoredProcedureName;
		/// <summary>
		/// The retrieve record by identifier stored procedure name
		/// </summary>
		private string? _retrieveRecordByIdStoredProcedureName = DefaultRetrieveRecordByIdStoredProcedureName;
		/// <summary>
		/// The (friendly) table name suffix value.
		/// </summary>
		private string? _tableNameSuffix = DefaultTableNameSuffix;
		/// <summary>
		/// The default update stored procedure name.
		/// </summary>
		private string? _updateStoredProcedureName = DefaultUpdateStoredProcedureName;
		/// <summary>
		/// The use soft delete
		/// </summary>
		private bool _useSoftDelete = DefaultUseSoftDeletes;
		/// <summary>
		/// The use table names for stored procedure names
		/// </summary>
		private bool _useTableNamesForStoredProcedureNames = DefaultUseTableNamesForStoredProcedureNames;
		#endregion

		#region Static Constructor
		/// <summary>
		/// Initializes the <see cref="OrmDatabaseOptions"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		static OrmDatabaseOptions()
		{
			Current = new OrmDatabaseOptions();

			// Load any saved data.
			Current.Load();
		}
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="OrmDatabaseOptions"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public OrmDatabaseOptions()
		{
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			_dataAccessClassSuffix = null;
			_defaultQualifiedOwner = null;
			_deletedColumnName = null;
			_deleteStoredProcedureName = null;
			_idColumnName = null;
			_insertStoredProcedureName = null;
			_qualifiedNameSeparatorOpen = null;
			_qualifiedNameSeparatorClose = null;
			_retrieveAllRecordsStoredProcedureName = null;
			_retrieveRecordByIdStoredProcedureName = null;
			_tableNameSuffix = null;
			_updateStoredProcedureName = null;

			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the reference to the global <see cref="OrmDatabaseOptions"/> instance.
		/// </summary>
		/// <value>
		/// The static <see cref="OrmDatabaseOptions"/> used by the code-generation library.
		/// </value>
		public static OrmDatabaseOptions Current { get; private set; }
		/// <summary>
		/// Gets or sets the data access class suffix.
		/// </summary>
		/// <value>
		/// A string containing the data access class suffix value.
		/// </value>
		public string? DataAccessClassSuffix { get => _dataAccessClassSuffix; set => _dataAccessClassSuffix = value; }
		/// <summary>
		/// Gets or sets the default qualified owner name value.
		/// </summary>
		/// <value>
		/// A string containing the default qualified owner name value (such as "dbo").
		/// </value>
		public string? DefaultQualifiedOwner { get => _defaultQualifiedOwner; set => _defaultQualifiedOwner = value; }
		/// <summary>
		/// Gets or sets the name of the "Deleted" column when using soft deletes.
		/// </summary>
		/// <value>
		/// A string containing the name of the deleted column.
		/// </value>
		public string DeletedColumnName
		{
			get
			{
				if (_deletedColumnName == null)
					_deletedColumnName = string.Empty;
				return _deletedColumnName;
			}
			set
			{
				_deletedColumnName = value;
			}
		}
		/// <summary>
		/// Gets or sets the name of the DELETE stored procedure.
		/// </summary>
		/// <value>
		/// The name of the DELETE stored procedure.
		/// </value>
		public string? DeleteStoredProcedureName { get => _deleteStoredProcedureName; set => _deleteStoredProcedureName = value; }
		/// <summary>
		/// Gets or sets the type of the identifier column data.
		/// </summary>
		/// <value>
		/// The <see cref="SqlDbType"/> enumerated value indicating the SQL data type of the identifier column in each table.
		/// </value>
		public SqlDbType IdColumnDataType
		{
			get => _idColumnDataType;
			set => _idColumnDataType = value;
		}
		/// <summary>
		/// Gets or sets the name of the identifier column in each table.
		/// </summary>
		/// <value>
		/// The name of the identifier column in each table.
		/// </value>
		public string IdColumnName
		{
			get
			{
				if (_idColumnName == null)
					_idColumnName = string.Empty;
				return _idColumnName;
			}
			set
			{
				_idColumnName = value;
			}
		}
		/// <summary>
		/// Gets or sets the name of the INSERT stored procedure.
		/// </summary>
		/// <value>
		/// The name of the INSERT stored procedure.
		/// </value>
		public string? InsertStoredProcedureName { get => _insertStoredProcedureName; set => _insertStoredProcedureName = value; }
		/// <summary>
		/// Gets or sets the qualified name separator open character.
		/// </summary>
		/// <value>
		/// The qualified name separator open character - such as "[".
		/// </value>
		public string? QualifiedNameSeparatorOpen { get => _qualifiedNameSeparatorOpen; set => _qualifiedNameSeparatorOpen = value; }
		/// <summary>
		/// Gets or sets the qualified name separator close character.
		/// </summary>
		/// <value>
		/// The qualified name separator open character - such as "]".
		/// </value>
		public string? QualifiedNameSeparatorClose { get => _qualifiedNameSeparatorClose; set => _qualifiedNameSeparatorClose = value; }
		/// <summary>
		/// Gets or sets the name of the retrieve all records stored procedure.
		/// </summary>
		/// <value>
		/// A string containing The name of the retrieve all records stored procedure.
		/// </value>
		public string? RetrieveAllRecordsStoredProcedureName { get => _retrieveAllRecordsStoredProcedureName; set => _retrieveAllRecordsStoredProcedureName = value; }
		/// <summary>
		/// Gets or sets the name of the retrieve record by identifier stored procedure.
		/// </summary>
		/// <value>
		/// A string containing the name of the retrieve record by identifier stored procedure.
		/// </value>
		public string? RetrieveRecordByIdStoredProcedureName { get => _retrieveRecordByIdStoredProcedureName; set => _retrieveRecordByIdStoredProcedureName = value; }
		/// <summary>
		/// Gets or sets the friendly table name suffix.
		/// </summary>
		/// <value>
		/// A string containing The table name suffix.
		/// </value>
		public string? TableNameSuffix { get => _tableNameSuffix; set => _tableNameSuffix = value; }
		/// <summary>
		/// Gets or sets the name of the UPDATE stored procedure.
		/// </summary>
		/// <value>
		/// The name of the UPDATE stored procedure.
		/// </value>
		public string? UpdateStoredProcedureName { get => _updateStoredProcedureName; set => _updateStoredProcedureName = value; }
		/// <summary>
		/// Gets or sets a value indicating whether to use soft delete processes when generating code to delete data.
		/// </summary>
		/// <value>
		///   <c>true</c> to generate code to soft-delete data; otherwise, <c>false</c> to generate code to hard-delete data.
		/// </value>
		public bool UseSoftDelete
		{
			get => _useSoftDelete;
			set => _useSoftDelete = value;
		}
		/// <summary>
		/// Gets or sets a value indicating whether to pre-pend the table name when creating stored procedure names.
		/// </summary>
		/// <remarks>
		/// When this value is <b>true</b>, the generated stored procedures will be named in a similar fashion:
		/// (Given the table name of "Customers"):
		/// 
		/// <code>
		///		CustomerInsert	
		///		CustomerUpdate
		///		CustomerDelete
		///		CustomerGetAll
		///		CustomerGetById
		///		... etc.
		/// </code>
		/// </remarks>
		/// <value>
		///   <c>true</c> to generate code to pre-pend the table name when creating stored procedure names; otherwise, <c>false</c>.
		/// </value>
		public bool UseTableNamesForStoredProcedureNames { get => _useTableNamesForStoredProcedureNames; set => _useTableNamesForStoredProcedureNames = value; }
		#endregion

		#region Public Static Methods / Functions
		/// <summary>
		/// Sets the global static options instance to the provided instance.
		/// </summary>
		/// <param name="options">
		/// The <see cref="OrmDatabaseOptions"/> instance used to specify the options to use for the entire application 
		/// or library.
		/// </param>
		public static void SetOptions(OrmDatabaseOptions options)
		{
			Current = options;
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Renders the name of the stored procedure.
		/// </summary>
		/// <param name="table">
		/// A string containing the name of the table the procedure will act against.
		/// </param>
		/// <param name="originalProcedureName">
		/// A string containing the full or partial name of the stored procedure.
		/// </param>
		/// <returns>
		/// A string containing the rendered stored procedure name.
		/// </returns>
		public string RenderStoredProcedureName(string? table, string? originalProcedureName)
		{
			if (string.IsNullOrEmpty(originalProcedureName))
				return string.Empty;
			else
			{
				if (_useTableNamesForStoredProcedureNames && !string.IsNullOrEmpty(table))
				{
					return (table.Properize() + originalProcedureName.Properize()).Trim();
				}
				else
				{
					return originalProcedureName.Trim();
				}
			}
		}

		/// <summary>
		/// Attempts to load the contents of the current instance from a local file.
		/// </summary>
		public void Load()
		{
			string? path = SafeIO.GetAppPath();
			if (!string.IsNullOrEmpty(path))
			{
				// Create the initial default file, if not already there.
				if (!SafeIO.DirectoryExists(path))
					Save();
				
				path += @"\.magicsql";
				string fileName = path + "\\ormoptions.dat";

				if (SafeIO.FileExists(fileName))
				{
					FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
					SafeBinaryReader reader = new SafeBinaryReader(fs);

					_dataAccessClassSuffix = reader.ReadString();
					_defaultQualifiedOwner = reader.ReadString();
					_deletedColumnName = reader.ReadString();
					_deleteStoredProcedureName = reader.ReadString();
					_idColumnDataType = (SqlDbType)reader.ReadInt32();
					_idColumnName = reader.ReadString();
					_insertStoredProcedureName = reader.ReadString();
					_qualifiedNameSeparatorOpen = reader.ReadString();
					_qualifiedNameSeparatorClose = reader.ReadString();
					_retrieveAllRecordsStoredProcedureName = reader.ReadString();
					_retrieveRecordByIdStoredProcedureName = reader.ReadString();
					_tableNameSuffix = reader.ReadString();
					_updateStoredProcedureName = reader.ReadString();
					_useSoftDelete = reader.ReadBoolean();
					_useTableNamesForStoredProcedureNames = reader.ReadBoolean();

					reader.Dispose();
					fs.Dispose();
				}
			}
		}
		/// <summary>
		/// Attempts to save the contents of the current instance to a local file.
		/// </summary>
		public void Save()
		{
			string? path = SafeIO.GetAppPath();
			if (!string.IsNullOrEmpty(path))
			{
				path += @"\.magicsql";
				if (!SafeIO.DirectoryExists(path))
					System.IO.Directory.CreateDirectory(path);

				string fileName = path +"\\ormoptions.dat";
				SafeIO.DeleteFile(fileName);
				FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
				SafeBinaryWriter writer = new SafeBinaryWriter(fs);

				writer.Write(_dataAccessClassSuffix);
				writer.Write(_defaultQualifiedOwner);
				writer.Write( _deletedColumnName);
				writer.Write(_deleteStoredProcedureName);
				writer.Write((int)_idColumnDataType);
				writer.Write(_idColumnName);
				writer.Write(_insertStoredProcedureName);
				writer.Write(_qualifiedNameSeparatorOpen);
				writer.Write(_qualifiedNameSeparatorClose);
				writer.Write(_retrieveAllRecordsStoredProcedureName);
				writer.Write(_retrieveRecordByIdStoredProcedureName);
				writer.Write(_tableNameSuffix);
				writer.Write(_updateStoredProcedureName);
				writer.Write(_useSoftDelete);
				writer.Write(_useTableNamesForStoredProcedureNames);

				writer.Flush();
				writer.Dispose();
				fs.Dispose();
			}
		}
		#endregion
	}
}
