// Ignore Spelling: Sql
// Ignore Spelling: ORM

using Adaptive.CodeDom;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.SqlServer.Properties;

namespace Adaptive.Intelligence.SqlServer.ORM
{
	/// <summary>
	/// Contains a list of global options to use when generating .NET code.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class OrmCodeGenerationOptions : DisposableObjectBase
	{
		#region Private Member Declarations

		#region Data Access Class Generation		
		/// <summary>
		/// The name of the base class the data access class descends from.
		/// </summary>
		private string? _daBaseClassName = Resources.DefaultDataAccessBaseClassName;
		/// <summary>
		/// The data access class remarks text.
		/// </summary>
		private string? _daClassRemarks = Resources.DataAccessXmlRemarks;
		/// <summary>
		/// The data access class summary text.
		/// </summary>
		private string? _daClassSummary = Resources.DataAccessXmlSummary;
		/// <summary>
		/// The XML remarks comment template for the single parameter constructor.
		/// </summary>
		private string? _daConstructorXmlRemarksTemplate = Resources.DaClassConstructorXmlRemarksTemplate;
		/// <summary>
		/// The XML summary comment template for the single parameter constructor.
		/// </summary>
		private string? _daConstructorXmlSummaryTemplate = Resources.DaClassConstructorXmlSummaryTemplate;
		/// <summary>
		/// The XML remarks comment template for the parameter-less constructor.
		/// </summary>
		private string? _daParamLessConstructorXmlRemarksTemplate = Resources.DaClassParamLessConstructorXmlRemarksTemplate;
		/// <summary>
		/// The XML summary comment template for the parameter-less constructor.
		/// </summary>
		private string? _daParamLessConstructorXmlSummaryTemplate = Resources.DaClassParamLessConstructorXmlSummaryTemplate;
		/// <summary>
		/// The name of the provider variable for the single parameter constructor.
		/// </summary>
		private string? _daProviderVariableName = Resources.DaClassProviderVariableName;
		/// <summary>
		/// The XML parameter comment template for the provider variable for the single parameter constructor.
		/// </summary>
		private string? _daProviderVariableParamSummary = Resources.DaClassProviderVariableParamSummary;
		/// <summary>
		/// The name of the data type for the provider variable for the single parameter constructor.
		/// </summary>
		private string? _daProviderVariableTypeName = Resources.DaClassProviderVariableTypeName;
		#endregion

		#region Stored Procedure Constant Generation		
		/// <summary>
		/// The XML summary content template for each of the SQL Parameter constants.
		/// </summary>
		private string? _daConstantPrefixForParameter = Resources.DataAccessSpSummaryPrefixForParameter;
		/// <summary>
		/// The XML summary comment for the Get All stored procedure constant.
		/// </summary>
		private string? _spGetAllXmlSummary = Resources.DataAccessSpGetAllXmlSummary;
		/// <summary>
		/// The XML summary comment for the Get By Id stored procedure constant.
		/// </summary>
		private string? _spGetByIdXmlSummary = Resources.DataAccessSpGetByIdXmlSummary;
		/// <summary>
		/// The XML summary comment for the Delete stored procedure constant.
		/// </summary>
		private string? _spDeleteXmlSummary = Resources.DataAccessSpDeleteXmlSummary;
		/// <summary>
		/// The XML summary comment for the Update stored procedure constant.
		/// </summary>
		private string? _spUpdateXmlSummary = Resources.DataAccessSpUpdateXmlSummary;
		/// <summary>
		/// The XML summary comment for the Insert stored procedure constant.
		/// </summary>
		private string? _spInsertXmlSummary = Resources.DataAccessSpInsertXmlSummary;
		/// <summary>
		/// The default for the constant name for the Delete stored procedure. (SqlDelete)
		/// </summary>
		private string? _spConstNameDelete = Resources.DefaultSpConstNameDelete;
		/// <summary>
		/// The default for the constant name for the Get All stored procedure. (SqlGetAll)
		/// </summary>
		private string? _spConstNameGetAll = Resources.DefaultSpConstNameGetAll;
		/// <summary>
		/// The default for the constant name for the Get By Id stored procedure. (SqlGetById)
		/// </summary>
		private string? _spConstNameGetById = Resources.DefaultSpConstNameGetById;
		/// <summary>
		/// The default for the constant name for the Insert stored procedure. (SqlInsert)
		/// </summary>
		private string? _spConstNameInsert = Resources.DefaultSpConstNameInsert;
		/// <summary>
		/// The default for the constant name prefix for the SQL Parameter constants. (SqlParam)
		/// </summary>
		private string? _spConstNameSqlParamPrefix = Resources.DefaultSpConstNameSqlParamPrefix;
		/// <summary>
		/// The default for the constant name for the Update stored procedure. (SqlUpdate)
		/// </summary>
		private string? _spConstNameUpdate = Resources.DefaultSpConstNameUpdate;
		#endregion

		/// <summary>
		/// The language to generate the code in.
		/// </summary>
		private NetLanguage _language = NetLanguage.CSharp;
		#endregion

		#region Static Constructor
		/// <summary>
		/// Initializes the <see cref="OrmCodeGenerationOptions"/> class.
		/// </summary>
		/// <remarks>
		/// This is the static constructor.
		/// </remarks>
		static OrmCodeGenerationOptions()
		{
			Current = new OrmCodeGenerationOptions();

			// Load any saved data.
			Current.Load();
		}
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="OrmCodeGenerationOptions"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public OrmCodeGenerationOptions()
		{

		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		/// <returns></returns>
		protected override void Dispose(bool disposing)
		{
			_daClassSummary = null;
			_daClassRemarks = null;
			_spGetAllXmlSummary = null;
			_spGetByIdXmlSummary = null;
			_spDeleteXmlSummary = null;
			_spUpdateXmlSummary = null;
			_spInsertXmlSummary = null;
			_daConstantPrefixForParameter = null;
			_daParamLessConstructorXmlSummaryTemplate = null;
			_daParamLessConstructorXmlRemarksTemplate = null;
			_daConstructorXmlSummaryTemplate = null;
			_daConstructorXmlRemarksTemplate = null;
			_daProviderVariableName = null;
			_daProviderVariableTypeName = null;
			_daProviderVariableParamSummary = null;
			_spConstNameGetAll = null;
			_spConstNameGetById = null;
			_spConstNameInsert = null;
			_spConstNameUpdate = null;
			_spConstNameDelete = null;
			_spConstNameSqlParamPrefix = null;
			base.Dispose(disposing);
		}
		#endregion

		#region Public Static Properties		
		/// <summary>
		/// Gets the reference to the current global instance.
		/// </summary>
		/// <value>
		/// The global instance of <see cref="OrmCodeGenerationOptions"/>.
		/// </value>
		public static OrmCodeGenerationOptions Current { get; private set; }
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets or sets the name of the provider variable for the single parameter constructor.
		/// </summary>
		/// <value>
		/// A string containing the variable name to use.
		/// </value>
		public string ConstructorProviderVariableName
		{
			get
			{
				if (_daProviderVariableName == null)
					return Resources.DaClassProviderVariableName;
				else
					return _daProviderVariableName;
			}
			set
			{
				_daProviderVariableName = value;
			}
		}
		/// <summary>
		/// Gets or sets The XML parameter comment template for the provider variable for the single parameter constructor.
		/// </summary>
		/// <remarks>
		/// A string containing the XML comment template.
		/// </remarks>
		public string ConstructorProviderVariableParamSummary
		{
			get
			{
				if (_daProviderVariableParamSummary == null)
					return Resources.DaClassProviderVariableParamSummary;
				else
					return _daProviderVariableParamSummary;
			}
			set
			{
				_daProviderVariableParamSummary = value;
			}
		}
		/// <summary>
		/// Gets or sets the name of the data type for the provider variable for the single parameter constructor.
		/// </summary>
		/// <value>
		/// A string containing the data type name to use.
		/// </value>
		public string ConstructorProviderVariableTypeName
		{
			get
			{
				if (_daProviderVariableTypeName == null)
					return Resources.DaClassProviderVariableTypeName;
				else
					return _daProviderVariableTypeName;
			}
			set
			{
				_daProviderVariableTypeName = value;
			}
		}
		/// <summary>
		/// Gets or sets the XML remarks comment template for the single parameter constructor.
		/// </summary>
		/// <value>
		/// A string containing the comment template text.
		/// </value>
		public string ConstructorXmlRemarksTemplate
		{
			get
			{
				if (_daConstructorXmlRemarksTemplate == null)
					return string.Empty;
				else
					return _daConstructorXmlRemarksTemplate;
			}
			set
			{
				_daConstructorXmlRemarksTemplate = value;
			}
		}
		/// <summary>
		/// Gets or sets the XML summary comment template for the single parameter constructor.
		/// </summary>
		/// <value>
		/// A string containing the comment template text.
		/// </value>
		public string ConstructorXmlSummaryTemplate
		{
			get
			{
				if (_daConstructorXmlSummaryTemplate == null)
					return string.Empty;
				else
					return _daConstructorXmlSummaryTemplate;
			}
			set
			{
				_daConstructorXmlSummaryTemplate = value;
			}
		}
		/// <summary>
		/// Gets or sets the name of the data access base class.
		/// </summary>
		/// <value>
		/// A string containing the name of the data access base class to inherit from.
		/// </value>
		public string DataAccessBaseClassName
		{
			get
			{
				if (_daBaseClassName == null)
					return string.Empty;
				else
					return _daBaseClassName;
			}
			set => _daBaseClassName = value;
		}
		/// <summary>
		/// Gets or sets the data access class XML remarks text.
		/// </summary>
		/// <value>
		/// The data access class summary XML remarks text.
		/// </value>
		public string DataAccessClassRemarks
		{
			get
			{
				if (_daClassRemarks == null)
					return string.Empty;
				else
					return _daClassRemarks;
			}
			set => _daClassRemarks = value;
		}
		/// <summary>
		/// Gets or sets the data access class XML summary text.
		/// </summary>
		/// <value>
		/// A string containing the data access class summary XML comment text.
		/// </value>
		public string DataAccessClassSummary
		{
			get
			{
				if (_daClassSummary == null)
					return string.Empty;
				else
					return _daClassSummary;
			}
			set => _daClassSummary = value;
		}
		/// <summary>
		/// Gets or sets the .NET language to render the code in.
		/// </summary>
		/// <value>
		/// A <see cref="NetLanguage"/> enumerated value indicating the language to render the code in.	
		/// </value>
		public NetLanguage Language
		{  
			get => _language; 
			set => _language = value;
		}
		/// <summary>
		/// The XML remarks comment template for the parameter-less constructor.
		/// </summary>
		/// <value>
		/// A string containing the comment template text.
		/// </value>
		public string ParameterLessConstructorXmlRemarksTemplate
		{
			get
			{
				if (_daParamLessConstructorXmlRemarksTemplate == null)
					return string.Empty;
				else
					return _daParamLessConstructorXmlRemarksTemplate;
			}
			set
			{
				_daParamLessConstructorXmlRemarksTemplate = value;
			}
		}
		/// <summary>
		/// Gets or sets the XML summary comment template for the parameter-less constructor.
		/// </summary>
		/// <value>
		/// A string containing the comment template text.
		/// </value>
		public string ParameterLessConstructorXmlSummaryTemplate
		{
			get
			{
				if (_daParamLessConstructorXmlSummaryTemplate == null)
					return string.Empty;
				else
					return _daParamLessConstructorXmlSummaryTemplate;
			}
			set
			{
				_daParamLessConstructorXmlSummaryTemplate = value;
			}
		}
		/// <summary>
		/// Gets or sets the name of the constant definition for the delete stored procedure name.
		/// </summary>
		/// <value>
		/// A string containing the name for the constant definition.
		/// </value>
		public string SpConstantNameForDelete
		{
			get
			{
				if (_spConstNameDelete == null)
					return string.Empty;
				else
					return _spConstNameDelete;
			}
			set => _spConstNameDelete = value;
		}
		/// <summary>
		/// Gets or sets the name of the constant definition the get all records stored procedure name.
		/// </summary>
		/// <value>
		/// A string containing the name for the constant definition.
		/// </value>
		public string SpConstantNameForGetAll
		{
			get
			{
				if (_spConstNameGetAll == null)
					return string.Empty;
				else
					return _spConstNameGetAll;
			}
			set => _spConstNameGetAll = value;
		}
		/// <summary>
		/// Gets or sets the name of the constant definition the get record by ID stored procedure name.
		/// </summary>
		/// <value>
		/// A string containing the name for the constant definition.
		/// </value>
		public string SpConstantNameForGetById
		{
			get
			{
				if (_spConstNameGetById == null)
					return string.Empty;
				else
					return _spConstNameGetById;
			}
			set => _spConstNameGetById = value;
		}
		/// <summary>
		/// Gets or sets the name of the constant definition the insert stored procedure name.
		/// </summary>
		/// <value>
		/// A string containing the name for the constant definition.
		/// </value>
		public string SpConstantNameForInsert
		{
			get
			{
				if (_spConstNameInsert == null)
					return string.Empty;
				else
					return _spConstNameInsert;
			}
			set => _spConstNameInsert = value;
		}
		/// <summary>
		/// Gets or sets the name of the constant definition the update stored procedure name.
		/// </summary>
		/// <value>
		/// A string containing the name for the constant definition.
		/// </value>
		public string SpConstantNameForUpdate
		{
			get
			{
				if (_spConstNameUpdate == null)
					return string.Empty;
				else
					return _spConstNameUpdate;
			}
			set => _spConstNameUpdate = value;
		}
		/// <summary>
		/// Gets or sets the name prefix of the constant definition used for each of the SQL Parameter constants.
		/// </summary>
		/// <value>
		/// A string containing the name prefix for the constant definition.
		/// </value>
		public string SpConstantNamePrefixForParameter
		{
			get
			{
				if (_spConstNameSqlParamPrefix == null)
					return string.Empty;
				else
					return _spConstNameSqlParamPrefix;
			}
			set => _spConstNameSqlParamPrefix = value;
		}
		/// <summary>
		/// Gets or sets the XML comment text for the delete stored procedure constant definition.
		/// </summary>
		/// <value>
		/// A string containing the XML summary comment text.
		/// </value>
		public string SpConstantSummaryForDelete
		{
			get
			{
				if (_spDeleteXmlSummary == null)
					return string.Empty;
				else
					return _spDeleteXmlSummary;
			}
			set => _spDeleteXmlSummary = value;
		}
		/// <summary>
		/// Gets or sets the XML comment text for the get all records stored procedure constant definition.
		/// </summary>
		/// <value>
		/// A string containing the XML summary comment text.
		/// </value>
		public string SpConstantSummaryForGetAll
		{
			get
			{
				if (_spGetAllXmlSummary == null)
					return string.Empty;
				else
					return _spGetAllXmlSummary;
			}
			set => _spGetAllXmlSummary = value;
		}
		/// <summary>
		/// Gets or sets the XML comment text for the get record by ID stored procedure constant definition.
		/// </summary>
		/// <value>
		/// A string containing the XML summary comment text.
		/// </value>
		public string SpConstantSummaryForGetById
		{
			get
			{
				if (_spGetByIdXmlSummary == null)
					return string.Empty;
				else
					return _spGetByIdXmlSummary;
			}
			set => _spGetByIdXmlSummary = value;
		}
		/// <summary>
		/// Gets or sets the XML comment text for the insert stored procedure constant definition.
		/// </summary>
		/// <value>
		/// A string containing the XML summary comment text.
		/// </value>
		public string SpConstantSummaryForInsert
		{
			get
			{
				if (_spInsertXmlSummary == null)
					return string.Empty;
				else
					return _spInsertXmlSummary;
			}
			set => _spInsertXmlSummary = value;
		}
		/// <summary>
		/// Gets or sets the XML comment text for the update stored procedure constant definition.
		/// </summary>
		/// <value>
		/// A string containing the XML summary comment text.
		/// </value>
		public string SpConstantSummaryForUpdate
		{
			get
			{
				if (_spUpdateXmlSummary == null)
					return string.Empty;
				else
					return _spUpdateXmlSummary;
			}
			set => _spUpdateXmlSummary = value;
		}
		/// <summary>
		/// Gets or sets the XML comment text template for each of the SQL Parameter name constants.
		/// </summary>
		/// <value>
		/// A string containing the XML summary comment text.
		/// </value>
		public string SpConstantSummaryPrefixForParameter
		{
			get
			{
				if (_spConstNameSqlParamPrefix == null)
					return string.Empty;
				else
					return _spConstNameSqlParamPrefix;
			}
			set => _spConstNameSqlParamPrefix = value;
		}
		#endregion

		#region Public Methods / Functions		
		/// <summary>
		/// Loads the contents of the options instance from the local file.
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
				string fileName = path + "\\ormcodegenoptions.dat";

				if (SafeIO.FileExists(fileName))
				{
					FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
					SafeBinaryReader reader = new SafeBinaryReader(fs);

					_daClassSummary = reader.ReadString();
					_daClassRemarks = reader.ReadString();
					_spGetAllXmlSummary = reader.ReadString();
					_spGetByIdXmlSummary = reader.ReadString();
					_spInsertXmlSummary = reader.ReadString();
					_spUpdateXmlSummary = reader.ReadString();
					_spDeleteXmlSummary = reader.ReadString();
					_daConstantPrefixForParameter = reader.ReadString();
					_daParamLessConstructorXmlSummaryTemplate = reader.ReadString();
					_daParamLessConstructorXmlRemarksTemplate = reader.ReadString();
					_daConstructorXmlSummaryTemplate = reader.ReadString();
					_daConstructorXmlRemarksTemplate = reader.ReadString();
					_daProviderVariableName = reader.ReadString();
					_daProviderVariableTypeName = reader.ReadString();
					_daProviderVariableParamSummary = reader.ReadString();

					_spConstNameGetAll = reader.ReadString();
					_spConstNameGetById = reader.ReadString();
					_spConstNameInsert = reader.ReadString();
					_spConstNameUpdate = reader.ReadString();
					_spConstNameDelete = reader.ReadString();
					_spConstNameSqlParamPrefix = reader.ReadString();
					_language = (NetLanguage)reader.ReadInt32();

					reader.Dispose();
					fs.Dispose();
				}
			}
		}
		/// <summary>
		/// Saves the contents of the options instance to the local file.
		/// </summary>
		public void Save()
		{
			string? path = SafeIO.GetAppPath();
			if (!string.IsNullOrEmpty(path))
			{
				path += @"\.magicsql";
				if (!SafeIO.DirectoryExists(path))
					System.IO.Directory.CreateDirectory(path);

				string fileName = path + "\\ormcodegenoptions.dat";
				SafeIO.DeleteFile(fileName);
				FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
				SafeBinaryWriter writer = new SafeBinaryWriter(fs);

				writer.Write(_daClassSummary);
				writer.Write(_daClassRemarks);
				writer.Write(_spGetAllXmlSummary);
				writer.Write(_spGetByIdXmlSummary);
				writer.Write(_spInsertXmlSummary);
				writer.Write(_spUpdateXmlSummary);
				writer.Write(_spDeleteXmlSummary);
				writer.Write(_daConstantPrefixForParameter);
				writer.Write(_daParamLessConstructorXmlSummaryTemplate);
				writer.Write(_daParamLessConstructorXmlRemarksTemplate);
				writer.Write(_daConstructorXmlSummaryTemplate);
				writer.Write(_daConstructorXmlRemarksTemplate);
				writer.Write(_daProviderVariableName);
				writer.Write(_daProviderVariableTypeName);
				writer.Write(_daProviderVariableParamSummary);
				writer.Write(_spConstNameGetAll);
				writer.Write(_spConstNameGetById);
				writer.Write(_spConstNameInsert);
				writer.Write(_spConstNameUpdate);
				writer.Write(_spConstNameDelete);
				writer.Write(_spConstNameSqlParamPrefix);
				writer.Write((int)_language);

				writer.Close();
				writer.Dispose();
				fs.Dispose();
			}
		}
		#endregion
	}
}