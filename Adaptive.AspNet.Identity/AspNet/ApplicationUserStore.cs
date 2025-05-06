using Adaptive.AspNet.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Adaptive.AspNet.Identity;

/// <summary>
/// Provides the application user store / repository for ASP .NET Identity models.
/// </summary>
/// <seealso cref="IUserStore{T}" />
/// <seealso cref="IUserEmailStore{T}" />
/// <seealso cref="IUserPasswordStore{T}" />
/// <seealso cref="ApplicationUser"/>
public class ApplicationUserStore : IUserStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>
{

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Creates the specified new application user in the user store.
    /// </summary>
    /// <param name="user">
    /// An <see cref="ApplicationUser"/> containing the information for the new user to create.
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    /// The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" /> of the 
    /// creation operation.
    /// </returns>
    public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        IdentityResult result;
        try
        {
            // TODO: Load User Role ID.
            user.RoleId = Guid.Parse("21E3E768-6DFD-45C8-B742-9EAFD63D68DB");
            await user.SaveAsync().ConfigureAwait(false);
            await user.SetPasswordAsync(user.PasswordHash).ConfigureAwait(false);
            result = IdentityResult.Success;
        }
        catch (Exception ex)
        {
            result = IdentityResult.Failed(new IdentityError() { Description = ex.Message });
        }
        return result;
    }

    /// <summary>
    /// Deletes the specified <paramref name="user" /> from the user store.
    /// </summary>
    /// <param name="user">
    /// An <see cref="ApplicationUser"/> containing the information for the user to delete.
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    /// The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" /> of the 
    /// creation operation.
    /// </returns>
    public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        bool success = await user.DeleteAsync().ConfigureAwait(false);
        if (success)
            return IdentityResult.Success;
        else
            return IdentityResult.Failed(new IdentityError { Code = "DeleteFailed", Description = "Failed to delete user." });
    }

    /// <summary>
    /// Gets the user, if any, associated with the specified, normalized email address.
    /// </summary>
    /// <param name="normalizedEmail">
    /// The normalized email address to return the user for.
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    /// The <see cref="ApplicationUser"/> instance if any associated with the specified normalized email address.
    /// </returns>
    public async Task<ApplicationUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        return await ApplicationUser.LoadUserByEmailAddressAsync(normalizedEmail).ConfigureAwait(false);
    }

    /// <summary>
    /// Finds and returns a user, if any, who has the specified <paramref name="userId" />.
    /// </summary>
    /// <param name="userId">
    /// A string containing the user ID to search for.
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    /// The <see cref="ApplicationUser"/> instance for the user matching the specified <paramref name="userId" /> if it exists,
    /// otherwise, returns <b>null</b>.
    /// </returns>
    public async Task<ApplicationUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await ApplicationUser.LoadUserByEmailAddressAsync(userId).ConfigureAwait(false);
    }

    /// <summary>
    /// Finds and returns a user, if any, who has the specified normalized user name.
    /// </summary>
    /// <param name="normalizedUserName">
    /// A string containing the normalized user name to search for.
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    /// The matching <see cref="ApplicationUser"/> instance if found; otherwise, returns <b>null</b>.
    /// </returns>
    public async Task<ApplicationUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return await ApplicationUser.LoadUserByEmailAddressAsync(normalizedUserName).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the email address for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">
    /// The user whose email should be returned.
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    /// A string containing the email address for the specified <paramref name="user" />, or <b>null</b> if not present or
    /// not found.
    /// </returns>
    public async Task<string?> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        if (user.Email == null && user.UserId != null)
        {
            UserDataAccess da = new UserDataAccess();
            IUser dto = await da.GetByIdAsync(user.UserId!.Value).ConfigureAwait(false);
            user.Email = dto.LoginName;
        }
        return user.Email;

    }

    /// <summary>
    /// Gets a flag indicating whether the email address for the specified <paramref name="user" /> has been verified, true if the email address is verified otherwise
    /// false.
    /// </summary>
    /// <param name="user">The user whose email confirmation status should be returned.</param>
    /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// <b>true</b> for now.
    /// </returns>
    public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }

    /// <summary>
    /// Returns the normalized email for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose email address to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The task object containing the results of the asynchronous lookup operation, the normalized email address if any associated with the specified user.
    /// </returns>
    public Task<string?> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.NormalizedEmail);
    }

    /// <summary>
    /// Gets the normalized user name for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose normalized name should be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the normalized user name for the specified <paramref name="user" />.
    /// </returns>
    public Task<string?> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.NormalizedEmail);
    }

    /// <summary>
    /// Gets the password hash for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">
    /// The user whose password hash to retrieve.
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    /// A string containing the password hash for the specified <paramref name="user" />,if found; otherwise,
    /// returns <b>null</b>.
    /// </returns>
    public async Task<string?> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        if (user.UserId != null && user.PasswordHash == null)
        {
            user.PasswordHash = await user.GetPasswordHashAsync().ConfigureAwait(false);
        }
     
        return user.PasswordHash;
    }

    /// <summary>
    /// Gets the user identifier for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose identifier should be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A string containing the identifier for the specified <paramref name="user" />.
    /// </returns>
    public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        if (user.UserId != null && user.UserName == null)
        {
            string? name = await user.GetLoginNameAsync().ConfigureAwait(false);
            if (name == null)
            {
                name = string.Empty;
            }
            user.UserName = name;
        }

        return user.UserName ?? string.Empty;
    }

    /// <summary>
    /// Gets the user name for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose name should be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A string containing the name for the specified <paramref name="user" />.
    /// </returns>
    public async Task<string?> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        if (user.LoginName == null && user.UserId != null)
        {
            user.LoginName = await user.GetLoginNameAsync().ConfigureAwait(false);
        }
        return user.LoginName;
    }

    /// <summary>
    /// Gets a flag indicating whether the specified <paramref name="user" /> has a password.
    /// </summary>
    /// <param name="user">The user to return a flag for, indicating whether they have a password or not.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// <b>true</b>> if the specified <paramref name="user" /> has a password;
    /// otherwise <b>false</b>.
    /// </returns>
    public async Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        string? pwd = await user.GetPasswordHashAsync().ConfigureAwait(false);
        return (!string.IsNullOrEmpty(pwd));
    }

    /// <summary>
    /// Sets the <paramref name="email" /> address for a <paramref name="user" />.
    /// </summary>
    /// <param name="user">
    /// The <see cref="ApplicationUser"/> whose email value should be set.
    /// </param>
    /// <param name="email">
    /// A string containing the new email address for the specified user.
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
    /// </param>
    public async Task SetEmailAsync(ApplicationUser user, string? email, CancellationToken cancellationToken)
    {
        user.Email = email;
        if (user.UserId != null)
            await user.SetEmailAddressAsync(email).ConfigureAwait(false);
    }

    /// <summary>
    /// Sets the flag indicating whether the specified <paramref name="user" />'s email address has been confirmed or not.
    /// </summary>
    /// <param name="user">The user whose email confirmation status should be set.</param>
    /// <param name="confirmed">A flag indicating if the email address has been confirmed, true if the address is confirmed otherwise false.</param>
    /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// The task object representing the asynchronous operation.
    /// </returns>
    public async Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
    {
    }

    /// <summary>
    /// Sets the normalized email for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose email address to set.</param>
    /// <param name="normalizedEmail">The normalized email to set for the specified <paramref name="user" />.</param>
    /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
    public async Task SetNormalizedEmailAsync(ApplicationUser user, string? normalizedEmail, CancellationToken cancellationToken)
    {
        user.NormalizedEmail = normalizedEmail;
        if (user.UserId != null)
            await user.SaveAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Sets the given normalized name for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose name should be set.</param>
    /// <param name="normalizedName">The normalized name to set.</param>
    /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
    public async Task SetNormalizedUserNameAsync(ApplicationUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        user.NormalizedUserName = normalizedName;
        if (user.UserId != null)
            await user.SaveAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Sets the password hash for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">
    /// The <see cref="ApplicationUser"/> whose password hash is to be set.
    /// </param>
    /// <param name="passwordHash">
    /// A string containing the password hash value.
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
    /// </param>
    public async Task SetPasswordHashAsync(ApplicationUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        if (user.UserId != null)
            await user.SaveAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Sets the user name value.
    /// </summary>
    /// <param name="user">
    /// The <see cref="ApplicationUser"/> to be modified.
    /// </param>
    /// <param name="userName">
    /// A string containing the new name of the user.
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
    /// </param>
    public async Task SetUserNameAsync(ApplicationUser user, string? userName, CancellationToken cancellationToken)
    {
        user.LoginName = userName;
        if (user.UserId != null)
            await user.SaveAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Updates the specified <paramref name="user" /> in the user store.
    /// </summary>
    /// <param name="user">
    /// The <see cref="ApplicationUser"/> to be modified.
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    /// An <see cref="IdentityResult" /> containing the result of the update operation.
    /// </returns>
    public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return await user.SaveAsync().ConfigureAwait(false);
    }
}