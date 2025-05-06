using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;

namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the data access methods for User records and <see cref="UserRoleDto"/> instances.
/// </summary>
/// <seealso cref="IdentityDataAccessBase{T}" />
/// <seealso cref="IUserDataAccess" />
/// <seealso cref="IUserRole" />
public class UserRoleDataAccess : IdentityDataAccessBase<IUserRole>
{
    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoleDataAccess"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public UserRoleDataAccess() : base(
        SqlProcedures.UserRolesInsert,
        SqlProcedures.UserRolesUpdate,
        SqlProcedures.UserRolesGetById,
        SqlProcedures.UserRolesDelete,
        SqlParams.UserRoleId)
    {

    }
    #endregion

    #region Protected Method Overrides
    /// <summary>
    /// Creates the array of SQL parameters used to insert a new record.
    /// </summary>
    /// <param name="dataRecord">
    /// The <see cref="IUserRole"/> data transfer object instance containing the new record.
    /// </param>
    /// <returns>
    /// An array of <see cref="SqlParameter" /> instances to be used in a stored procedure.
    /// </returns>
    protected override SqlParameter[] CreateInsertParameters(IUserRole dataRecord)
    {
        return new SqlParameter[]
        {
            CreateParameter(SqlParams.RoleId, dataRecord.RoleId!),
            CreateParameter(SqlParams.UserId, dataRecord.UserId!),
        };
    }

    /// <summary>
    /// Creates the array of SQL parameters used to update a record.
    /// </summary>
    /// <param name="dataRecord">
    /// The <see cref="IUserRole"/> data transfer object instance containing the data record to be updated.
    /// </param>
    /// <returns>
    /// An array of <see cref="SqlParameter" /> instances to be used in a stored procedure.
    /// </returns>
    protected override SqlParameter[] CreateUpdateParameters(IUserRole dataRecord)
    {
        return new SqlParameter[]
        {
            CreateParameter(SqlParams.UserRoleId, dataRecord.UserRoleId),
            CreateParameter(SqlParams.RoleId, dataRecord.RoleId!),
            CreateParameter(SqlParams.UserId, dataRecord.UserId!),
        };
    }

    /// <summary>
    /// Populates the supplied DTO instance from the specified reader.
    /// </summary>
    /// <param name="reader">
    /// The <see cref="ISafeSqlDataReader" /> reader instance to use to read the data content.
    /// </param>
    /// <param name="instance">
    /// The <see cref="IUserRole"/> data object instance to be populated.
    /// </param>
    protected override void Populate(ISafeSqlDataReader reader, IUserRole instance)
    {
        int index = 0;

        instance.UserRoleId = reader.GetGuid(index++);
        instance.RoleId = reader.GetGuid(index++);
        instance.UserId = reader.GetGuid(index++);
        instance.Deleted = reader.GetBoolean(index++);
        instance.CreatedDate = reader.GetDateTime(index++);
        instance.LastModifiedDate = reader.GetDateTime(index);
    }
    #endregion
}