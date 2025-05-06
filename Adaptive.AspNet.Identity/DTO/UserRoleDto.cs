namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Represents a record in the Roles table.
/// </summary>
/// <seealso cref="DtoBase" />
public class UserRoleDto : DtoBase, IUserRole
{

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        RoleId = null;
        UserId = null;
        UserRoleId = null;

        base.Dispose(disposing);
    }

    /// <summary>
    /// Gets or sets the ID of the related role record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid" /> specifying the role ID value, or <b>null</b> if not yet created.
    /// </value>
    public Guid? RoleId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the related user record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid" /> specifying the user ID value, or <b>null</b> if not yet created.
    /// </value>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the related record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid" /> specifying the user-role record ID value, or <b>null</b> if not yet created.
    /// </value>
    public Guid? UserRoleId { get; set; }

    /// <summary>
    /// Gets a value indicating whether the record has been created in the database.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance has NOT yet been created and has no ID value; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Most implementations will return a value indicating whether the record's specified ID
    /// value is <b>null</b>.
    /// </remarks>
    public override bool IsNew()
    {
        return (UserRoleId == null);
    }
}
