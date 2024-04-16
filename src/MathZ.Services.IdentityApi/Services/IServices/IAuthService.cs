namespace MathZ.Services.IdentityApi.Services.IServices;

using System.Threading.Tasks;
using FluentResults;
using MathZ.Services.IdentityApi.Models;
using MathZ.Shared.Models;
using Microsoft.AspNetCore.Identity;

public interface IAuthService
{
    /// <summary>
    /// Logs in a user with the specified credentials.
    /// </summary>
    /// <param name="user">The user to log in.</param>
    /// <param name="password">The password for the user.</param>
    /// <returns>A task that represents the asynchronous login operation. The task result contains the login response DTO.</returns>
    Task<Result<JwtToken>> LoginAsync(User user, string password);

    /// <summary>
    /// Registers a new user with the specified credentials.
    /// </summary>
    /// <param name="user">The user to register.</param>
    /// <param name="password">The password for the user.</param>
    /// <returns>A task that represents the asynchronous registration operation. The task result contains the identity result.</returns>
    Task<IdentityResult> RegisterAsync(User user, string password);
}
