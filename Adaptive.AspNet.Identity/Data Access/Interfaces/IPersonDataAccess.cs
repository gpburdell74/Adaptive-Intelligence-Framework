namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Provides the signature definition for the data access implementation for the Persons table.
/// </summary>
/// <seealso cref="IDataAccess{T}" />
/// <seealso cref="PersonDto"/>
/// <seealso cref="IPerson"/>
public interface IPersonDataAccess : IDataAccess<IPerson>
{
}
