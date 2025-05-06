namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the signature definition for the data access implementation for the LoginHistory table.
/// </summary>
/// <seealso cref="IDataAccess{T}" />
/// <seealso cref="ILoginHistory"/>
public interface ILoginHistoryDataAccess : IDataAccess<ILoginHistory>
{
}
