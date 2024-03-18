namespace MathZ.Services.IdentityApi.Services.IServices;

using System.Threading.Tasks;
using FluentResults;
using MathZ.Services.IdentityApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;

public interface IAuthService
{
    /// <summary>
    /// Logs in a user with the provided login request.
    /// </summary>
    /// <param name="loginRequest">The login request containing user credentials.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the login response.</returns>
    Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequest);

    /// <summary>
    /// Registers a new user with the provided registration request.
    /// </summary>
    /// <param name="registrationRequest">The registration request containing user details.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the identity result.</returns>
    Task<IdentityResult> RegisterAsync(RegistrationRequestDto registrationRequest);
}
