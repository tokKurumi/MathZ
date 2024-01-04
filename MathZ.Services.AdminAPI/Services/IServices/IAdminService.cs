namespace MathZ.Services.AdminAPI.Services.IServices;

using MathZ.Shared.Models;
using MathZ.Shared.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;

public interface IAdminService
{
    Task<IEnumerable<UserAccountDto>> GetAllUsersAsync();

    Task<UserAccountDto> GetUserByIdAsync(string userId);

    Task<UserAccountDto> RegisterUserAsync(string authorizationToken, UserAccountRegistrationRequestDto registrationRequestDto);

    Task DeleteUserAsync(string userId);

    Task<UserAccountDto> PatchProfileAsync(string userId, JsonPatchDocument<UserAccountPatchProfileModels> patchRequest);

    Task ChangeUserPasswordAsync(string userId, string newPassword);

    Task<UserAccountDto> AddUserRolesAsync(string userId, IEnumerable<string> roles);

    Task<UserAccountDto> DeleteUserRolesAsync(string userId, IEnumerable<string> roles);
}