using Adaptive.Intelligence.Shared;

namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the base definition for data transfer object implementations.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public abstract class DtoBase : DisposableObjectBase, IDataTransfer
{
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        CreatedDate = null;
        LastModifiedDate = null;
        base.Dispose(disposing);
    }

    /// <summary>
    /// Gets or sets the date the record was created.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime" /> the record was created, or <b>null</b> if not yet created.
    /// </value>
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IDataTransfer" /> is marked as deleted.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the record has been marked as  deleted; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// This property is used in relation to using "soft" deletes in the data store.
    /// </remarks>
    public bool Deleted { get; set; }

    /// <summary>
    /// Gets or sets the date the record was last modified.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime" /> the record was last modified, or <b>null</b> if not yet created.
    /// </value>
    public DateTime? LastModifiedDate { get; set; }

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
    public abstract bool IsNew();
}
