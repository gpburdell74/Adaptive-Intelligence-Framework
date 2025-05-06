namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the signature definition for a record in the data store defining a person's information.
/// </summary>
/// <seealso cref="IDataTransfer" />
public interface IPerson : IDataTransfer
{
    /// <summary>
    /// Gets or sets the email address for the person.
    /// </summary>
    /// <value>
    /// A string containing the email address value, or <b>null</b>.
    /// </value>
    string? EmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    /// <value>
    /// A string containing the first name value, or <b>null</b>.
    /// </value>
    string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the person.
    /// </summary>
    /// <value>
    /// A string containing the last name value, or <b>null</b>.
    /// </value>
    string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the middle name/initial of the person.
    /// </summary>
    /// <value>
    /// A string containing the middle name value, or <b>null</b>.
    /// </value>
    string? MiddleName { get; set; }

    /// <summary>
    /// Gets or sets the nickname or preferred name for the person.
    /// </summary>
    /// <value>
    /// A string containing the nickname value, or <b>null</b>.
    /// </value>
    string? Nickname { get; set; }

    /// <summary>
    /// Gets or sets the name suffix value for the person.
    /// </summary>
    /// <value>
    /// A string containing the name suffix value, or <b>null</b>.
    /// </value>
    string? Suffix { get; set; }

    /// <summary>
    /// Gets or sets the title for the person.
    /// </summary>
    /// <value>
    /// A string containing the title value, or <b>null</b>.
    /// </value>
    string? Title { get; set; }

    /// <summary>
    /// Gets or sets the ID of the person record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> specifying the person ID value, or <b>null</b> if not yet created.
    /// </value>
    Guid? PersonId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the related user record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> specifying the user ID value, or <b>null</b> if not yet created.
    /// </value>
    Guid? UserId { get; set; }
}