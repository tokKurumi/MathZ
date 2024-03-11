namespace MathZ.Services.IdentityApi.Services.IServices;

using System.Threading.Tasks;
using FluentResults;
using MathZ.Services.IdentityApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;

public interface IAuthService
{
    Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequest);

    Task<IdentityResult> RegisterAsync(RegistrationRequestDto registrationRequest);
}