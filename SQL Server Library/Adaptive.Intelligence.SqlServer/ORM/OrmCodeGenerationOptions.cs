// Ignore Spelling: Sql
// Ignore Spelling: ORM

using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.SqlServer.Properties;

namespace Adaptive.Intelligence.SqlServer.ORM
{
	/// <summary>
	/// Contains a list of global options to use when generating .NET code.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class OrmCodeGenerationOptions : DisposableObjectBase
	{
		#region Private Constant Declarations
		#endregion

		#region Private Member Declarations

		#region Data Access Class Generation

		private string? _daClassSummary = Resources.DataAccessXmlSummary;
		private string? _daClassRemarks = Resources.DataAccessXmlRemarks;
		private string? _daBaseClassName = Resources.DefaultDataAccessBaseClassName;

		#endregion

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
		public string? DataAccessClassSummary
		{
			get => _daClassSummary;
			set => _daClassSummary = value;
		}
		public string? DataAccessClassRemarks
		{
			get => _daClassRemarks;
			set => _daClassRemarks = value;
		}
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

		public string SpConstantSummaryForGetAll = "The stored procedure to retrieve all the records in the table.";
		public string SpConstantNameForGetAll = "SqlGetAll";

		public string SpConstantSummaryForGetById = "The stored procedure to retrieve a single record by ID value.";
		public string SpConstantNameForGetById = "SqlGetById";

		public string SpConstantSummaryForInsert = "The stored procedure to insert a record.";
		public string SpConstantNameForInsert = "SqlInsert";

		public string SpConstantSummaryForUpdate = "The stored procedure to update a record.";
		public string SpConstantNameForUpdate = "SqlUpdate";

		public string SpConstantSummaryForDelete = "The stored procedure to delete a record.";
		public string SpConstantNameForDelete = "SqlDelete";

		public string SpConstantSummaryPrefixForParameter = "The @{0} parameter for a stored procedure.";
		public string SpConstantNamePrefixForParameter = "SqlParam";

		#endregion

		#region Public Methods / Functions
		public void Load()
		{

		}
		public void Save()
		{

		}
		#endregion
	}
}