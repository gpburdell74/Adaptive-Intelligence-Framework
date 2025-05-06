using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;

namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the data access methods for User records and <see cref="RoleDto"/> instances.
/// </summary>
/// <seealso cref="IdentityDataAccessBase{T}" />
/// <seealso cref="IUserDataAccess" />
/// <seealso cref="UserDto" />
public class RoleDataAccess : IdentityDataAccessBase<IRole>, IRoleDataAccess
{
    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleDataAccess"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public RoleDataAccess() : base(
        SqlProcedures.RolesInsert,
        SqlProcedures.RolesUpdate,
        SqlProcedures.RolesGetById,
        SqlProcedures.RolesDelete,
        SqlParams.RoleId)
    {

    }
    #endregion

    #region Protected Method Overrides
    /// <summary>
    /// Creates the array of SQL parameters used to insert a new record.
    /// </summary>
    /// <param name="dataRecord">
    /// The <see cref="RoleDto"/> data transfer object instance containing the new record.
    /// </param>
    /// <returns>
    /// An array of <see cref="SqlParameter" /> instances to be used in a stored procedure.
    /// </returns>
    protected override SqlParameter[] CreateInsertParameters(IRole dataRecord)
    {
        return new SqlParameter[]
        {
            CreateParameter(SqlParams.RoleName, dataRecord.RoleName!),
            CreateParameter(SqlParams.IsAdmin, dataRecord.IsAdmin!),
            CreateParameter(SqlParams.RoleFlags, dataRecord.RoleFlags!),
        };
    }

    /// <summary>
    /// Creates the array of SQL parameters used to update a record.
    /// </summary>
    /// <param name="dataRecord">
    /// The <see cref="UserDto"/> data transfer object instance containing the data record to be updated.
    /// </param>
    /// <returns>
    /// An array of <see cref="SqlParameter" /> instances to be used in a stored procedure.
    /// </returns>
    protected override SqlParameter[] CreateUpdateParameters(IRole dataRecord)
    {
        return new SqlParameter[]
        {
            CreateParameter(SqlParams.RoleId, dataRecord.RoleId),
            CreateParameter(SqlParams.RoleName, dataRecord.RoleName!),
            CreateParameter(SqlParams.IsAdmin, dataRecord.IsAdmin!),
            CreateParameter(SqlParams.RoleFlags, dataRecord.RoleFlags!),
        };
    }

    /// <summary>
    /// Populates the supplied DTO instance from the specified reader.
    /// </summary>
    /// <param name="reader">
    /// The <see cref="ISafeSqlDataReader" /> reader instance to use to read the data content.
    /// </param>
    /// <param name="instance">
    /// The <see cref="UserDto"/> data object instance to be populated.
    /// </param>
    protected override void Populate(ISafeSqlDataReader reader, IRole instance)
    {
        int index = 0;

        instance.RoleId = reader.GetGuid(index++);
        instance.RoleName = reader.GetString(index++);
        instance.RoleFlags = reader.GetInt32(index++);
        instance.IsAdmin = reader.GetBoolean(index++);
        instance.Deleted = reader.GetBoolean(index++);
        instance.CreatedDate = reader.GetDateTime(index++);
        instance.LastModifiedDate = reader.GetDateTime(index);
    }
    #endregion
}