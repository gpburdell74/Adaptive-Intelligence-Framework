namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Represents a record in the Users table.
/// </summary>
/// <seealso cref="DtoBase" />
public class UserDto : DtoBase, IUser
{
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        Expiry = null;
        LoginName = null;
        PasswordHash = null;
        UserId = null;
        base.Dispose(disposing);
    }

    /// <summary>
    /// Gets or sets the expiration date for the user account.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime" /> specifying the expiration date, or <b>null</b>.
    /// </value>
    public DateTime? Expiry { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the account has been locked out.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the user is locked out; otherwise, <c>false</c>.
    /// </value>
    public bool LockedOut { get; set; }

    /// <summary>
    /// Gets or sets the login name for the user.
    /// </summary>
    /// <value>
    /// A string containing the user's unique login name.
    /// </value>
    public string? LoginName { get; set; }

    /// <summary>
    /// Gets or sets the password hash value.
    /// </summary>
    /// <value>
    /// A string containing the hash value of the user's password.
    /// </value>
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid" /> specifying the user ID value, or <b>null</b> if not yet created.
    /// </value>
    public Guid? UserId { get; set; }

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
        return (UserId == null);
    }
}
