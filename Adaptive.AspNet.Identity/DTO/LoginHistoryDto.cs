namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Represents a record in the Roles table.
/// </summary>
/// <seealso cref="DtoBase" />
public class LoginHistoryDto : DtoBase, ILoginHistory
{

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        LoginHistoryId = null;
        UserId = null;
        base.Dispose(disposing);
    }

    /// <summary>
    /// Gets or sets the login date.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime" /> specifying the login date.
    /// </value>
    public DateTime? LoginDate { get; set; }

    /// <summary>
    /// Gets or sets the ID of the related record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid" /> containing the ID of the record, or
    /// <b>null</b> if not yet created.
    /// </value>
    public Guid? LoginHistoryId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the related user.
    /// </summary>
    /// <value>
    /// A <see cref="Guid" /> containing the ID of the user.
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
        return (LoginHistoryId == null);
    }
}
