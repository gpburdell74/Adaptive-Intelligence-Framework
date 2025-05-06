namespace Adaptive.AspNet.Identity.Data;

/// <summary>
/// Represents a record in the Users table and contains the object references
/// to the related Person and Role data.
/// </summary>
/// <seealso cref="DtoBase" />
public class FullUserDto : UserDto, IFullUser
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FullUserDto"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public FullUserDto() : base()
    {
        Person = new PersonDto();
        Role = new RoleDto();
    }
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            Person?.Dispose();
            Role?.Dispose();
        }

        Person = null;
        Role = null;
        base.Dispose(disposing);
    }

    /// <summary>
    /// Gets the reference to the related person data.
    /// </summary>
    /// <value>
    /// The reference to the <see cref="IPerson" /> data record.
    /// </value>
    public IPerson? Person { get; private set; }

    /// <summary>
    /// Gets the reference to the related role data.
    /// </summary>
    /// <value>
    /// The reference to the <see cref="IRole" /> data record.
    /// </value>
    public IRole? Role { get; private set; }
}