namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Represents a record in the data store for defining a user to role relationship.
/// </summary>
/// <seealso cref="IDataTransfer" />
public interface IUserRole : IDataTransfer
{
    /// <summary>
    /// Gets or sets the ID of the related role record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> specifying the role ID value, or <b>null</b> if not yet created.
    /// </value>
    Guid? RoleId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the related user record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> specifying the user ID value, or <b>null</b> if not yet created.
    /// </value>
    Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the related record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> specifying the user-role record ID value, or <b>null</b> if not yet created.
    /// </value>
    Guid? UserRoleId { get; set; }
}