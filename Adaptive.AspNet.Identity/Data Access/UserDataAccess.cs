using Adaptive.SqlServer.Client;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the data access methods for User records and <see cref="UserDto"/> instances.
/// </summary>
/// <seealso cref="IdentityDataAccessBase{T}" />
/// <seealso cref="IUserDataAccess" />
/// <seealso cref="UserDto" />
public class UserDataAccess : IdentityDataAccessBase<IUser>, IUserDataAccess
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="UserDataAccess"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public UserDataAccess() : base(
        SqlProcedures.UsersInsert, 
        SqlProcedures.UsersUpdate, 
        SqlProcedures.UsersGetById,
        SqlProcedures.UsersDelete, 
        SqlParams.UserId)
    {
    }
    #endregion

    #region Protected Method Overrides
    /// <summary>
    /// Creates the array of SQL parameters used to insert a new record.
    /// </summary>
    /// <param name="dataRecord">
    /// The <see cref="UserDto"/> data transfer object instance containing the new record.
    /// </param>
    /// <returns>
    /// An array of <see cref="SqlParameter" /> instances to be used in a stored procedure.
    /// </returns>
    protected override SqlParameter[] CreateInsertParameters(IUser dataRecord)
    {
        return CreateParameterList
            (
                SqlParams.LoginName, dataRecord.LoginName!,
                SqlParams.PasswordHash, dataRecord.PasswordHash!
            );
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
    protected override SqlParameter[] CreateUpdateParameters(IUser dataRecord)
    {
        return new SqlParameter[]
            {
                CreateParameter(SqlParams.UserId, dataRecord.UserId),
                CreateParameter(SqlParams.LoginName, dataRecord.LoginName),
                CreateParameter(SqlParams.PasswordHash, dataRecord.PasswordHash),
                CreateParameter(SqlParams.LockedOut, dataRecord.LockedOut),
                CreateParameter(SqlParams.Expiry, dataRecord.Expiry)
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
    protected override void Populate(ISafeSqlDataReader reader, IUser instance)
    {
        int index = 0;

        instance.UserId = reader.GetGuid(index++);
        instance.LoginName = reader.GetString(index++);
        instance.Deleted = reader.GetBoolean(index++);
        instance.LockedOut = reader.GetBoolean(index++);
        instance.Expiry = reader.GetDateTime(index++);
        instance.CreatedDate = reader.GetDateTime(index++);
        instance.LastModifiedDate = reader.GetDateTime(index);
    }
    #endregion

    #region Public Methods / Functions        
    /// <summary>
    /// Creates / inserts a new data record and sets the user up in the system.
    /// </summary>
    /// <param name="dataRecord">
    /// The <see cref="FullUserDto"/> data record to be created.
    /// </param>
    /// <returns>
    /// A <see cref="Guid"/> containing the ID of the new record, if successful; otherwise,
    /// returns <b>null</b>.
    /// </returns>
    public async Task<Guid?> CreateNewAsync(FullUserDto dataRecord)
    {
        Guid? newId = null;

        // Create the user record.
        UserDto userRecord = new UserDto()
        {
            Deleted = false,
            Expiry = dataRecord.Expiry,
            LoginName = dataRecord.LoginName,
            LockedOut = false,
        };
        dataRecord.UserId = await CreateNewAsync(userRecord).ConfigureAwait(false);
        if (dataRecord.UserId != null)
        {
            // Save the related person data.
            dataRecord.Person!.UserId = dataRecord.UserId;
            PersonDataAccess personDa = new PersonDataAccess();
            dataRecord.Person.PersonId = await personDa.CreateNewAsync((PersonDto)dataRecord.Person).ConfigureAwait(false);
            if (dataRecord.Person.PersonId != null)
            {
                UserRoleDataAccess roleDa = new UserRoleDataAccess();
                UserRoleDto userRole = new UserRoleDto
                {
                    RoleId = dataRecord!.Role!.RoleId,
                    UserId = dataRecord!.UserId
                };
                newId = await roleDa.CreateNewAsync(userRole).ConfigureAwait(false);
                roleDa.Dispose();
            }
            personDa.Dispose();
        }

        return newId;
    }

    /// <summary>
    /// Gets the user record by the specified email address value with the associated person and role data.
    /// </summary>
    /// <param name="emailAddress">
    /// A string containing the email address value to query for.
    /// </param>
    /// <returns>
    /// The associated <see cref="FullUserDto"/> record, if successful;
    /// otherwise, returns <b>null</b>.
    /// </returns>
    public async Task<FullUserDto?> GetByEmailAddressAsync(string emailAddress)
    {
        FullUserDto? dto = null;

        ISafeSqlDataReader? reader = await GetReaderForParameterizedStoredProcedureAsync(
            SqlProcedures.UsersGetByEmail,
            CreateParameterList(SqlParams.EmailAddress, emailAddress)).ConfigureAwait(false);

        if (reader != null)
        {
            if (await reader.ReadAsync().ConfigureAwait(false))
            {
                dto = new FullUserDto();
                PersonDto person = new PersonDto();
                RoleDto role = new RoleDto();
                int index = 0;

                // Read the User record.
                dto.UserId = reader.GetGuid(index++);
                dto.LoginName = reader.GetString(index++);
                dto.PasswordHash = reader.GetString(index++);
                dto.Deleted = reader.GetBoolean(index++);
                dto.CreatedDate = reader.GetDateTime(index++);
                dto.LastModifiedDate = reader.GetDateTime(index++);
                dto.LockedOut = reader.GetBoolean(index++);
                dto.Expiry = reader.GetDateTime(index++);

                // Read the Person record.
                dto.Person!.PersonId = reader.GetGuid(index++);
                dto.Person!.FirstName = reader.GetString(index++);
                dto.Person!.MiddleName = reader.GetString(index++);
                dto.Person!.LastName = reader.GetString(index++);
                dto.Person!.Suffix = reader.GetString(index++);
                dto.Person!.Title = reader.GetString(index++);
                dto.Person!.Nickname = reader.GetString(index++);
                dto.Person!.EmailAddress = reader.GetString(index++);
                dto.Person!.UserId = reader.GetGuid(index++);
                dto.Person!.Deleted = reader.GetBoolean(index++);
                dto.Person!.CreatedDate = reader.GetDateTime(index++);
                dto.Person!.LastModifiedDate = reader.GetDateTime(index++);
                          
                // Read the Role record.
                dto.Role!.RoleId = reader.GetGuid(index++);
                dto.Role!.RoleName = reader.GetString(index++);
                dto.Role!.IsAdmin = reader.GetBoolean(index++);
                dto.Role!.Deleted = reader.GetBoolean(index++);
                dto.Role!.CreatedDate = reader.GetDateTime(index++);
                dto.Role!.LastModifiedDate = reader.GetDateTime(index);

            }
            reader.Dispose();
        }
        return dto;
    }

    /// <summary>
    /// Gets the password hash value from the data store.
    /// </summary>
    /// <param name="userId">
    /// A <see cref="Guid"/> containing the ID of the user to query for.
    /// </param>
    /// <returns>
    /// A string containing the password hash value, or <b>null</b> if not present or successful.
    /// </returns>
    public async Task<string?> GetPasswordHashAsync(Guid userId)
    {
        string? hash = null;

        ISafeSqlDataReader? reader = await GetReaderForParameterizedStoredProcedureAsync(
            SqlProcedures.GetPasswordHash, 
            CreateParameterList(SqlParams.UserId, userId)).ConfigureAwait(false);

        if (reader != null)
        {
            if (await reader.ReadAsync().ConfigureAwait(false))
            {
                hash = reader.GetString(0);
            }
            reader.Dispose();
        }

        return hash;
    }

    /// <summary>
    /// Gets the login name for the specified user.
    /// </summary>
    /// <param name="userId">
    /// A <see cref="Guid"/> containing the ID of the user record.
    /// </param>
    /// <returns>
    /// A string containing the login name for the user if successful; otherwise, returns <b>null</b>.
    /// </returns>
    public async Task<string?> GetLoginNameAsync(Guid userId)
    {
        string? loginName = null;

        ISafeSqlDataReader? reader = await GetReaderForParameterizedStoredProcedureAsync(
            SqlProcedures.GetLoginName, CreateParameterList(SqlParams.UserId, userId)).ConfigureAwait(false);

        if (reader != null)
        {
            if (await reader.ReadAsync().ConfigureAwait(false))
            {
                loginName = reader.GetString(0);
            }
            reader.Dispose();
        }
        return loginName;
    }

    /// <summary>
    /// Records the successful login.
    /// </summary>
    /// <param name="userId">
    /// A <see cref="Guid"/> containing the ID of the user record.
    /// </param>
    public async Task RecordLoginAsync(Guid userId)
    {
        await ExecuteStoredProcedureAsync(SqlProcedures.LoginHistoryInsert,
            CreateParameterList( SqlParams.UserId, userId)).ConfigureAwait(false);
    }
    /// <summary>
    /// Sets the email address value for the specified user.
    /// </summary>
    /// <param name="userId">
    /// A <see cref="Guid"/> containing the ID of the user record to update.
    /// </param>
    /// <param name="emailAddress">
    /// A string containing the email address value.
    /// </param>
    public async Task SetEmailAddressAsync(Guid userId, string emailAddress)
    {
        await ExecuteStoredProcedureAsync(SqlProcedures.UpdateEmailAddress,
            CreateParameterList(
                SqlParams.UserId, userId,
                SqlParams.EmailAddress, emailAddress)).ConfigureAwait(false);
    }

    /// <summary>
    /// Sets the password hash value for the specified user.
    /// </summary>
    /// <param name="userId">
    /// A <see cref="Guid"/> containing the ID of the user record to update.
    /// </param>
    /// <param name="passwordHash">
    /// A string containing the password hash value.
    /// </param>
    public async Task SetPasswordHashAsync(Guid userId, string passwordHash)
    {
        await ExecuteStoredProcedureAsync(SqlProcedures.UpdatePwd,
            CreateParameterList(
                SqlParams.UserId, userId,
                SqlParams.Data, passwordHash)).ConfigureAwait(false);
    }
    #endregion
}
