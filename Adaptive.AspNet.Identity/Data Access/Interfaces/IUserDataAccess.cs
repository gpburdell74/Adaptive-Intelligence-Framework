namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the signature definition for the data access implementation for the Users table.
/// </summary>
/// <seealso cref="IDataAccess{T}" />
/// <seealso cref="IUser"/>
public interface IUserDataAccess : IDataAccess<IUser>
{
}
