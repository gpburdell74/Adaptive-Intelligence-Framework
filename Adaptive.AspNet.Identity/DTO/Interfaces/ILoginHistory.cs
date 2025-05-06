namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Represents a record in the data store for recording login history.
/// </summary>
/// <seealso cref="IDataTransfer" />
public interface ILoginHistory : IDataTransfer
{
    /// <summary>
    /// Gets or sets the login date.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> specifying the login date.
    /// </value>
    DateTime? LoginDate { get; set; }

    /// <summary>
    /// Gets or sets the ID of the related record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> containing the ID of the record, or 
    /// <b>null</b> if not yet created.
    /// </value>
    Guid? LoginHistoryId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the related user.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> containing the ID of the user.
    /// </value>
    Guid? UserId { get; set; }
}
