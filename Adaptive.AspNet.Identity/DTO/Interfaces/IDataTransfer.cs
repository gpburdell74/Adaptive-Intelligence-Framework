namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the signature definition for data transfer object implementations.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IDataTransfer : IDisposable 
{
    /// <summary>
    /// Gets or sets the date the record was created.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> the record was created, or <b>null</b> if not yet created.
    /// </value>
    DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IDataTransfer"/> is marked as deleted.
    /// </summary>
    /// <remarks>
    /// This property is used in relation to using "soft" deletes in the data store.
    /// </remarks>
    /// <value>
    ///   <c>true</c> if the record has been marked as  deleted; otherwise, <c>false</c>.
    /// </value>
    bool Deleted { get; set; }

    /// <summary>
    /// Gets or sets the date the record was last modified.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> the record was last modified, or <b>null</b> if not yet created.
    /// </value>
    DateTime? LastModifiedDate { get; set; }

    /// <summary>
    /// Gets a value indicating whether the record has been created in the database.
    /// </summary>
    /// <remarks>
    /// Most implementations will return a value indicating whether the record's specified ID 
    /// value is <b>null</b>.
    /// </remarks>
    /// <returns>
    ///   <c>true</c> if this instance has NOT yet been created and has no ID value; otherwise, <c>false</c>.
    /// </returns>
    bool IsNew();
}

