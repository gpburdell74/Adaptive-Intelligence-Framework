using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;

namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the data access methods for User records and <see cref="PersonDto"/> instances.
/// </summary>
/// <seealso cref="IdentityDataAccessBase{T}" />
/// <seealso cref="IUserDataAccess" />
/// <seealso cref="UserDto" />
public class PersonDataAccess : IdentityDataAccessBase<IPerson>, IPersonDataAccess
{
    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonDataAccess"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public PersonDataAccess() : base(
        SqlProcedures.PersonsInsert,
        SqlProcedures.PersonsUpdate,
        SqlProcedures.PersonsGetById,
        SqlProcedures.PersonsDelete,
        SqlParams.PersonId)
    {

    }
    #endregion

    #region Protected Method Overrides
    /// <summary>
    /// Creates the array of SQL parameters used to insert a new record.
    /// </summary>
    /// <param name="dataRecord">
    /// The <see cref="IPerson"/> data transfer object instance containing the new record.
    /// </param>
    /// <returns>
    /// An array of <see cref="SqlParameter" /> instances to be used in a stored procedure.
    /// </returns>
    protected override SqlParameter[] CreateInsertParameters(IPerson dataRecord)
    {
        return new SqlParameter[]
        {
            CreateParameter(SqlParams.FirstName, dataRecord.FirstName!),
            CreateParameter(SqlParams.MiddleName, dataRecord.MiddleName!),
            CreateParameter(SqlParams.LastName, dataRecord.LastName!),
            CreateParameter(SqlParams.Suffix, dataRecord.Suffix!),
            CreateParameter(SqlParams.Title, dataRecord.Title!),
            CreateParameter(SqlParams.Nickname, dataRecord.Nickname!),
            CreateParameter(SqlParams.EmailAddress, dataRecord.EmailAddress!),
            CreateParameter(SqlParams.UserId, dataRecord.UserId!)
        };
    }

    /// <summary>
    /// Creates the array of SQL parameters used to update a record.
    /// </summary>
    /// <param name="dataRecord">
    /// The <see cref="IPerson"/> data transfer object instance containing the data record to be updated.
    /// </param>
    /// <returns>
    /// An array of <see cref="SqlParameter" /> instances to be used in a stored procedure.
    /// </returns>
    protected override SqlParameter[] CreateUpdateParameters(IPerson dataRecord)
    {
        return new SqlParameter[]
        {
            CreateParameter(SqlParams.PersonId, dataRecord.PersonId),
            CreateParameter(SqlParams.FirstName, dataRecord.FirstName!),
            CreateParameter(SqlParams.MiddleName, dataRecord.MiddleName!),
            CreateParameter(SqlParams.LastName, dataRecord.LastName!),
            CreateParameter(SqlParams.Suffix, dataRecord.Suffix!),
            CreateParameter(SqlParams.Title, dataRecord.Title!),
            CreateParameter(SqlParams.Nickname, dataRecord.Nickname!),
            CreateParameter(SqlParams.EmailAddress, dataRecord.EmailAddress!),
            CreateParameter(SqlParams.UserId, dataRecord.UserId!)
        };
    }

    /// <summary>
    /// Populates the supplied DTO instance from the specified reader.
    /// </summary>
    /// <param name="reader">
    /// The <see cref="ISafeSqlDataReader" /> reader instance to use to read the data content.
    /// </param>
    /// <param name="instance">
    /// The <see cref="IPerson"/> data object instance to be populated.
    /// </param>
    protected override void Populate(ISafeSqlDataReader reader, IPerson instance)
    {
        int index = 0;

        instance.PersonId = reader.GetGuid(index++);
        instance.FirstName = reader.GetString(index++);
        instance.MiddleName = reader.GetString(index++);
        instance.LastName = reader.GetString(index++);
        instance.Suffix = reader.GetString(index++);
        instance.Title = reader.GetString(index++);
        instance.Nickname = reader.GetString(index++);
        instance.EmailAddress = reader.GetString(index++);
        instance.UserId = reader.GetGuid(index++);
        instance.Deleted = reader.GetBoolean(index++);
        instance.CreatedDate = reader.GetDateTime(index++);
        instance.LastModifiedDate = reader.GetDateTime(index);
    }
    #endregion
}