namespace MathZ.Services.AdminAPI.Services.IServices;

using MathZ.Shared.Models;
using MathZ.Shared.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;

public interface IAdminService
{
    Task<IEnumerable<UserAccountDto>> GetAllUsers();

    Task<UserAccountDto> RegisterUser(string authorizationToken, UserAccountRegistrationRequestDto registrationRequestDto);

    Task DeleteUser(string userId);

    Task<UserAccountDto> GetUserByIdAsync(string userId);

    Task<UserAccountDto> PatchProfileAsync(string userId, JsonPatchDocument<UserAccountPatchProfileModels> patchRequest);

    Task<UserAccountDto> AssignUserRoles(string userId, IEnumerable<string> roles);
}