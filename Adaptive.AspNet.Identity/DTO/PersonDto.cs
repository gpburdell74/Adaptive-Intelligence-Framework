

namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Represents a record in the Roles table.
/// </summary>
/// <seealso cref="DtoBase" />
public class PersonDto : DtoBase, IPerson
{
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        EmailAddress = null;
        FirstName = null;
        LastName = null;
        MiddleName = null;
        Nickname = null;
        Suffix = null;
        Title = null;
        PersonId = null;
        UserId = null;

        base.Dispose(disposing);
    }


    /// <summary>
    /// Gets or sets the email address for the person.
    /// </summary>
    /// <value>
    /// A string containing the email address value, or <b>null</b>.
    /// </value>
    public string? EmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    /// <value>
    /// A string containing the first name value, or <b>null</b>.
    /// </value>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the person.
    /// </summary>
    /// <value>
    /// A string containing the last name value, or <b>null</b>.
    /// </value>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the middle name/initial of the person.
    /// </summary>
    /// <value>
    /// A string containing the middle name value, or <b>null</b>.
    /// </value>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Gets or sets the nickname or preferred name for the person.
    /// </summary>
    /// <value>
    /// A string containing the nickname value, or <b>null</b>.
    /// </value>
    public string? Nickname { get; set; }

    /// <summary>
    /// Gets or sets the name suffix value for the person.
    /// </summary>
    /// <value>
    /// A string containing the name suffix value, or <b>null</b>.
    /// </value>
    public string? Suffix { get; set; }

    /// <summary>
    /// Gets or sets the title for the person.
    /// </summary>
    /// <value>
    /// A string containing the title value, or <b>null</b>.
    /// </value>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the ID of the person record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid" /> specifying the person ID value, or <b>null</b> if not yet created.
    /// </value>
    public Guid? PersonId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the related user record.
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
        return (PersonId == null);
    }
}
