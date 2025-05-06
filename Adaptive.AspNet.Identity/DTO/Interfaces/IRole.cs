namespace Adaptive.AspNet.Identity.Data
{
    /// <summary>
    /// Represents a record in the data store for defining a role for user(s).
    /// </summary>
    /// <seealso cref="IDataTransfer" />
    public interface IRole : IDataTransfer
    {
        /// <summary>
        /// Gets or sets a value indicating whether the role is an Administrator role.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is an administrator role; otherwise, <c>false</c>.
        /// </value>
        bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets the permissions flags for the role.
        /// </summary>
        /// <remarks>
        /// This is for the implementor to populate based on their own role-based definitions.
        /// </remarks>
        /// <value>
        /// An integer representing the permissions flags for the role.
        /// </value>
        int RoleFlags { get; set; }

        /// <summary>
        /// Gets or sets the ID of the role record.
        /// </summary>
        /// <value>
        /// A <see cref="Guid"/> containing the ID of the record, or 
        /// <b>null</b> if not yet created.
        /// </value>
        Guid? RoleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// A string containing the name of the role.
        /// </value>
        string? RoleName { get; set; }
    }
}
