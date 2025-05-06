namespace Adaptive.AspNet.Identity.Data
{
    /// <summary>
    /// Represents a record in the data store for defining a user.
    /// </summary>
    /// <seealso cref="IDataTransfer" />
    public interface IUser : IDataTransfer
    {
        /// <summary>
        /// Gets or sets the expiration date for the user account.
        /// </summary>
        /// <value>
        /// A <see cref="DateTime"/> specifying the expiration date, or <b>null</b>.
        /// </value>
        DateTime? Expiry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the account has been locked out.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the user is locked out; otherwise, <c>false</c>.
        /// </value>
        bool LockedOut { get; set; }

        /// <summary>
        /// Gets or sets the login name for the user.
        /// </summary>
        /// <value>
        /// A string containing the user's unique login name.
        /// </value>
        string? LoginName { get; set; }

        /// <summary>
        /// Gets or sets the password hash value.
        /// </summary>
        /// <value>
        /// A string containing the hash value of the user's password.
        /// </value>
        string? PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user record.
        /// </summary>
        /// <value>
        /// A <see cref="Guid"/> specifying the user ID value, or <b>null</b> if not yet created.
        /// </value>
        Guid? UserId { get; set; }
    }
}
