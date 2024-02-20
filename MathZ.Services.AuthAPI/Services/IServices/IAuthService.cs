namespace MathZ.Services.AuthAPI.Services.IServices;

using MathZ.Services.AuthAPI.Models.Dto;
using MathZ.Shared.Models.Dto;
using Microsoft.AspNetCore.Identity;

public interface IAuthService
{
    Task<IdentityResult?> RegisterAsync(UserAccountRegistrationRequestDto registrationRequestDto);

    Task<UserAccountLoginResponseDto> LoginAsync(UserAccountLoginRequestDto loginRequestDto);

    Task<IdentityResult?> ConfirmEmailAsync(string email, string confirmCode);
}