namespace Adaptive.Intelligence.LanguageService.Providers;

/// <summary>
/// Provides the signature definition for a code provider for data type definitions.
/// </summary>
/// <seealso cref="ICodeProvider" />
public interface IDataTypeProvider : ICodeProvider
{
    /// <summary>
    /// Renders the list of data type ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique ID values used for mapping ID to text content.
    /// </returns>
    List<int> RenderDataTypeIds();

    /// <summary>
    /// Renders the list of data type names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of unique name values used for mapping text to ID values.
    /// </returns>
    List<string> RenderDataTypeNames();

    /// <summary>
    /// Maps the name of the data type to a .NET data type.
    /// </summary>
    /// <param name="typeName">
    /// A string containing the name of the type to be mapped.
    /// </param>
    /// <returns>
    /// A <see cref="Type"/> instance if successful;otherwise, returns <b>null</b>.
    /// </returns>
    Type? MapToDotNetType(string typeName);
}
