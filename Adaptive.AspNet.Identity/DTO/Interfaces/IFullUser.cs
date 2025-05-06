namespace Adaptive.AspNet.Identity.Data
{
    /// <summary>
    /// Represents a record in the data store for defining a user and contains the 
    /// sub-objects containing the Role and Person relationships and related data.
    /// </summary>
    /// <seealso cref="IDataTransfer" />
    public interface IFullUser : IUser
    {
        /// <summary>
        /// Gets the reference to the related person data.
        /// </summary>
        /// <value>
        /// The reference to the <see cref="IPerson"/> data record.
        /// </value>
        IPerson? Person { get; }

        /// <summary>
        /// Gets the reference to the related role data.
        /// </summary>
        /// <value>
        /// The reference to the <see cref="IRole"/> data record.
        /// </value>
        IRole? Role { get; }
    }
}
