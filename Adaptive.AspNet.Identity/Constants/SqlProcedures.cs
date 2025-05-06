namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the constant definitions for the names of the stored procedures used in the application.
/// </summary>
public static class SqlProcedures
{
    #region General Use Procedures
    /// <summary>
    /// The create user role stored procedure name.
    /// </summary>
    public const string CreateUserRole = "CreateUserRole";
    /// <summary>
    /// The get login name stored procedure name.
    /// </summary>
    public const string GetLoginName = "GetLoginName";
    /// <summary>
    /// The get password hash stored procedure name.
    /// </summary>
    public const string GetPasswordHash = "GetPasswordHash";
    /// <summary>
    /// The update email address stored procedure name.
    /// </summary>
    public const string UpdateEmailAddress = "UpdateEmailAddress";
    /// <summary>
    /// The update password stored procedure name.
    /// </summary>
    public const string UpdatePwd = "UpdateSecurity";
    #endregion

    #region Login History Stored Procedures
    /// <summary>
    /// The login history insert stored procedure.
    /// </summary>
    public const string LoginHistoryInsert = "LoginHistoryInsert";
    #endregion

    #region Persons CRUD Procedures    
    /// <summary>
    /// The delete procedure for the Persons table.
    /// </summary>
    public const string PersonsDelete = "PersonsDelete";
    /// <summary>
    /// The get by ID procedure for the Persons table.
    /// </summary>
    public const string PersonsGetById = "PersonsGetById";
    /// <summary>
    /// The insert procedure for the Persons table.
    /// </summary>
    public const string PersonsInsert = "PersonsInsert";
    /// <summary>
    /// The update procedure for the Persons table.
    /// </summary>
    public const string PersonsUpdate = "PersonsUpdate";
    #endregion

    #region Roles CRUD Procedures
    /// <summary>
    /// The delete procedure for the Roles table.
    /// </summary>
    public const string RolesDelete = "RolesDelete";
    /// <summary>
    /// The get by ID procedure for the Roles table.
    /// </summary>
    public const string RolesGetById = "RolesGetById";
    /// <summary>
    /// The insert procedure for the Roles table.
    /// </summary>
    public const string RolesInsert = "RolesInsert";
    /// <summary>
    /// The update procedure for the Roles table.
    /// </summary>
    public const string RolesUpdate = "RolesUpdate";
    #endregion

    #region Security Stored Procedures

    public const string SecurityInsert = "SecurityInsert";
    public const string SecurityUpdate = "SecurityUpdate";
    public const string SecurityGetById = "SecurityGetById";
    public const string SecurityDelete = "SecurityDelete";
    #endregion

    #region Users CRUD Procedures
    /// <summary>
    /// The delete procedure for the Users table.
    /// </summary>
    public const string UsersDelete = "UsersDelete";
    /// <summary>
    /// The get by email address procedure for the Users table.
    /// </summary>
    public const string UsersGetByEmail = "GetUserByEmailAddress";
    /// <summary>
    /// The get by ID procedure for the Users table.
    /// </summary>
    public const string UsersGetById = "UsersGetById";
    /// <summary>
    /// The insert procedure for the Users table.
    /// </summary>
    public const string UsersInsert = "UsersInsert";
    /// <summary>
    /// The update procedure for the Users table.
    /// </summary>
    public const string UsersUpdate = "UsersUpdate";
    #endregion

    #region User Roles CRUD Procedures
    /// <summary>
    /// The delete procedure for the UserRoles table.
    /// </summary>
    public const string UserRolesDelete = "UserRolesDelete";
    /// <summary>
    /// The get by id procedure for the UserRoles table.
    /// </summary>
    public const string UserRolesGetById = "UserRolesGetById";
    /// <summary>
    /// The insert procedure for the UserRoles table.
    /// </summary>
    public const string UserRolesInsert = "UserRolesInsert";
    /// <summary>
    /// The update procedure for the UserRoles table.
    /// </summary>
    public const string UserRolesUpdate = "UserRolesUpdate";
    #endregion


}
