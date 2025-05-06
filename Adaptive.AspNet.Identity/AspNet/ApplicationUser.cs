using Adaptive.AspNet.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Adaptive.AspNet.Identity;

/// <summary>
/// Provides the base definition for an ASP.NET Identity User instance.
/// </summary>
/// <seealso cref="IdentityUser" />
public class ApplicationUser : IdentityUser, IDisposable 
{
    #region Private Member Declarations
    /// <summary>
    /// The user data transfer object for data access operations.
    /// </summary>
    private FullUserDto? _dto;

    /// <summary>
    /// The data access instance for person records.
    /// </summary>
    private PersonDataAccess? _personDa;

    /// <summary>
    /// The data access instance for role records.
    /// </summary>
    private RoleDataAccess? _roleDa;

    /// <summary>
    /// The data access instance for user records.
    /// </summary>
    private UserDataAccess? _userDa;
    #endregion

    #region Constructor / Dispose Methods    
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
    /// </summary>
    /// <remarks>
    /// The Id property is initialized to form a new GUID string value.
    /// </remarks>
    public ApplicationUser() : base()
    {
        _dto = new FullUserDto();
        _userDa = new UserDataAccess();
        _roleDa = new RoleDataAccess();
        _personDa = new PersonDataAccess();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
    /// </summary>
    /// <param name="userDto">
    /// The <see cref="FullUserDto"/> instance containing the user data.
    /// </param>
    public ApplicationUser(FullUserDto userDto) : base()
    {
        _dto = userDto;
        _userDa = new UserDataAccess();
        _roleDa = new RoleDataAccess();
        _personDa = new PersonDataAccess();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _userDa?.Dispose();
            _roleDa?.Dispose();
            _personDa?.Dispose();
            _dto?.Dispose();
        }

        _dto = null;
        _userDa = null;
        _roleDa = null;
        _personDa = null;
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets or sets the email address for this user.
    /// </summary>
    /// <value>
    /// A string containing the email address, or <b>null</b>.
    /// </value>
    public override string? Email
    {
        get => _dto?.Person!.EmailAddress;
        set
        {
            _dto!.Person!.EmailAddress = value;
        }
    }
    /// <summary>
    /// Gets or sets the expiration date for the user account.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime" /> specifying the expiration date, or <b>null</b>.
    /// </value>
    public DateTime? Expiry { get => _dto?.Expiry; }

    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    /// <value>
    /// A string containing the first name value, or <b>null</b>.
    /// </value>
    public string? FirstName 
    { 
        get => _dto?.Person!.FirstName; 
        set => _dto!.Person!.FirstName = value; 
    }

    /// <summary>
    /// Gets or sets a value indicating whether the role is an Administrator role.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is an administrator role; otherwise, <c>false</c>.
    /// </value>

    public bool IsAdmin
    {
        get
        {
            if (_dto == null || _dto.Role == null)
                return false;
            else
                return _dto.Role.IsAdmin;
        }
    }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    /// <value>
    /// The last name.
    /// </value>
    public string? LastName 
    { 
        get => _dto!.Person!.LastName; 
        set => _dto!.Person!.LastName = value; 
    }

    /// <summary>
    /// Gets or sets a value indicating whether the account has been locked out.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the user is locked out; otherwise, <c>false</c>.
    /// </value>
    public bool LockedOut
    {
        get
        {
            if (_dto == null)
                return false;
            else
                return _dto.LockedOut;
        }
    }

    /// <summary>
    /// Gets or sets the login name for the user.
    /// </summary>
    /// <value>
    /// A string containing the user's unique login name.
    /// </value>
    public string? LoginName 
    { 
        get => _dto?.LoginName; 
        set => _dto!.LoginName = value; 
    }

    /// <summary>
    /// Gets or sets the middle name/initial of the person.
    /// </summary>
    /// <value>
    /// A string containing the middle name value, or <b>null</b>.
    /// </value>
    public string? MiddleName 
    { 
        get => _dto?.Person!.MiddleName; 
        set => _dto!.Person!.MiddleName = value; 
    }

    /// <summary>
    /// Gets or sets the nickname or preferred name for the person.
    /// </summary>
    /// <value>
    /// A string containing the nickname value, or <b>null</b>.
    /// </value>
    public string? Nickname 
    {
        get => _dto?.Person!.Nickname; 
        set => _dto!.Person!.Nickname = value; 
    }

    /// <summary>
    /// Gets or sets a salted and hashed representation of the password for this user.
    /// </summary>
    /// <value>
    /// A string containing the password hash value.
    /// </value>
    public override string? PasswordHash
    {
        get => _dto?.PasswordHash;
        set => _dto!.PasswordHash = value;
    }
    /// <summary>
    /// Gets or sets the ID of the person record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> specifying the person ID value, or <b>null</b> if not yet created.
    /// </value>
    public Guid? PersonId 
    { 
        get => _dto?.Person!.PersonId; 
        set => _dto!.Person!.PersonId = value; 
    }

    /// <summary>
    /// Gets or sets the permissions flags for the role.
    /// </summary>
    /// <remarks>
    /// This is for the implementor to populate based on their own role-based definitions.
    /// </remarks>
    /// <value>
    /// An integer representing the permissions flags for the role.
    /// </value>
    public int RoleFlags
    {
        get
        {
            if (_dto == null || _dto.Role == null)
                return 0;
            else
                return _dto.Role.RoleFlags;
        }
        set
        {
            _dto!.Role!.RoleFlags = value;
        }
    }

    /// <summary>
    /// Gets or sets the ID of the role record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> containing the ID of the record, or 
    /// <b>null</b> if not yet created.
    /// </value>
    public Guid? RoleId 
    { 
        get => _dto?.Role!.RoleId; 
        set => _dto!.Role!.RoleId = value; 
    }
    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    /// <value>
    /// A string containing the name of the role.
    /// </value>
    public string? RoleName 
    { 
        get => _dto?.Role!.RoleName; 
        set => _dto!.Role!.RoleName = value; 
    }

    /// <summary>
    /// Gets or sets the name suffix value for the person.
    /// </summary>
    /// <value>
    /// A string containing the name suffix value, or <b>null</b>.
    /// </value>
    public string? Suffix 
    { 
        get => _dto?.Person!.Suffix; 
        set => _dto!.Person!.Suffix = value; 
    }

    /// <summary>
    /// Gets or sets the title for the person.
    /// </summary>
    /// <value>
    /// A string containing the title value, or <b>null</b>.
    /// </value>
    public string? Title 
    { 
        get => _dto?.Person!.Title; 
        set => _dto!.Person!.Title = value; 
    }

    /// <summary>
    /// Gets or sets the ID of the related user record.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> specifying the user ID value, or <b>null</b> if not yet created.
    /// </value>
    public Guid? UserId 
    { 
        get => _dto?.UserId; 
    }
    #endregion

    #region Public Methods / Functions    
    /// <summary>
    /// Marks the user record as deleted.
    /// </summary>
    /// <returns>
    /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
    /// </returns>
    public async Task<bool> DeleteAsync()
    {
        if (UserId != null)
            return await _userDa!.DeleteAsync(UserId.Value).ConfigureAwait(false);
        else
            return false;
    }

    /// <summary>
    /// Gets the login name for the current user.
    /// </summary>
    /// <returns>
    /// A string containing the current user's login name.
    /// </returns>
    public async Task<string?> GetLoginNameAsync()
    {
        if (UserId != null)
        {
            LoginName = await _userDa!.GetLoginNameAsync(UserId.Value).ConfigureAwait(false);
        }
        return LoginName;
    }

    /// <summary>
    /// Gets the password hash value for the current user.
    /// </summary>
    /// <returns>
    /// A string containing the password hash value.
    /// </returns>
    public async Task<string?> GetPasswordHashAsync()
    {
        if (UserId != null)
        {
            PasswordHash = await _userDa!.GetPasswordHashAsync(UserId.Value).ConfigureAwait(false);
        }
        return PasswordHash;
    }

    /// <summary>
    /// Loads the user that is identified by the specified email address.
    /// </summary>
    /// <param name="emailAddress">
    /// A string containing the email address value to search for.
    /// </param>
    /// <returns>
    /// The matching <see cref="ApplicationUser"/> instance, if found; otherwise, returns <b>null</b>.
    /// </returns>
    public static async Task<ApplicationUser?> LoadUserByEmailAddressAsync(string emailAddress)
    {
        UserDataAccess dataAccess = new UserDataAccess();
        FullUserDto? dto = await dataAccess.GetByEmailAddressAsync(emailAddress).ConfigureAwait(false);
        dataAccess.Dispose();

        if (dto == null)
            return null;

        return new ApplicationUser(dto);
    }

    /// <summary>
    /// Records the successful login.
    /// </summary>
    public async Task RecordLoginAsync()
    {
        UserDataAccess dataAccess = new UserDataAccess();
        await dataAccess.RecordLoginAsync(UserId!.Value).ConfigureAwait(false);
        dataAccess.Dispose();
    }

    /// <summary>
    /// Renders the user name for display.
    /// </summary>
    /// <param name="prefix">
    /// A string containing the prefix text to pre-pend, or <b>null</b>.
    /// </param>
    /// <returns>
    /// A string containing the user name to be displayed.
    /// </returns>
    public string? RenderUserName(string? prefix)
    {
        StringBuilder builder = new StringBuilder();

        if (prefix != null)
            builder.Append(prefix);

        if (FirstName != null) 
        {
            builder.Append(FirstName + " ");
            if (LastName != null)
                builder.Append(LastName);
        }
        else if (NormalizedUserName != null)
        {
            builder.Append(NormalizedUserName);
            
        }
        else
        {
            builder.Append(LoginName);
        }

        return builder.ToString().Trim();
    }

    /// <summary>
    /// Saves the current user information to the data store.
    /// </summary>
    /// <returns>
    /// An <see cref="IdentityResult"/> instance containing the result of the operation.
    /// </returns>
    public async Task<IdentityResult> SaveAsync()
    {
        IdentityResult result;

        if (UserId == null)
        {
            _dto!.UserId = await _userDa!.CreateNewAsync(_dto).ConfigureAwait(false);
            if (_dto!.UserId == null)
            {
                if (_userDa.HasExceptions)
                    result = IdentityResult.Failed(new IdentityError { Code = "CreateFailed", Description = _userDa.Exceptions[0].Message });
                else
                    result = IdentityResult.Failed(new IdentityError { Code = "CreateFailed", Description = "The user record could not be created." });
            }
            else
                result = IdentityResult.Success;
        }
        else
        {
            if (_dto != null)
            {
                IUser? newDto = await _userDa!.UpdateAsync(_dto).ConfigureAwait(false);
                if (newDto != null)
                {
                    _dto = await _userDa!.GetByEmailAddressAsync(LoginName!).ConfigureAwait(false);
                    result = IdentityResult.Success;
                }
                else
                {
                    if (_userDa.HasExceptions)
                        result = IdentityResult.Failed(new IdentityError { Code = "CreateFailed", Description = _userDa.Exceptions[0].Message });
                    else
                        result = IdentityResult.Failed(new IdentityError { Code = "CreateFailed", Description = "The user record could not be created." });
                }
            }
            else
                result = IdentityResult.Failed(new IdentityError { Code = "CreateFailed", Description = "The user record could not be created." });
        }

        return result;
    }
    /// <summary>
    /// Sets the email address value for the current user.
    /// </summary>
    /// <param name="email">
    /// A string containing the email address value.
    /// </param>
    public async Task SetEmailAddressAsync(string? email)
    {
        Email = email;
        if (UserId != null && !string.IsNullOrEmpty(email))
        {
            await _userDa!.SetEmailAddressAsync(UserId.Value, email).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Sets the password hash value for the current user.
    /// </summary>
    /// <param name="hash">
    /// A string containing the password hash value.
    /// </param>
    public async Task SetPasswordAsync(string? hash)
    {
        PasswordHash = hash;
        if (UserId != null && hash != null)
        {
            await _userDa!.SetPasswordHashAsync(UserId.Value, hash).ConfigureAwait(false);
        }
    }
    #endregion
}
