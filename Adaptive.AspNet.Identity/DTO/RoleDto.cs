namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Represents a record in the Roles table.
/// </summary>
/// <seealso cref="DtoBase" />
public class RoleDto : DtoBase, IRole
{

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        RoleId = null;
        RoleName = null;
        base.Dispose(disposing);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the role is an Administrator role.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is an administrator role; otherwise, <c>false</c>.
    /// </value>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Gets or sets the permissions flags for the role.
    /// </summary>
    /// <value>
    /// An integer representing the permissions flags for the role.
    /// </value>
    /// <remarks>
    /// This is for the implementor to populate based on their own role-based definitions.
    /// </remarks>
    public int RoleFlags { get; set; }

    /// <summary>
    /// Gets or sets the ID of the role record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid" /> containing the ID of the record, or
    /// <b>null</b> if not yet created.
    /// </value>
    public Guid? RoleId { get; set; }

    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    /// <value>
    /// A string containing the name of the role.
    /// </value>
    public string? RoleName { get; set; }

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
        return (RoleId == null);
    }
}
