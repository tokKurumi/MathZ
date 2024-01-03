namespace MathZ.Services.UserAPI.Services.IServices
{
    using MathZ.Services.UserAPI.Models.Dto;
    using MathZ.Shared.Models;
    using MathZ.Shared.Models.Dto;
    using Microsoft.AspNetCore.JsonPatch;

    public interface IUserAccountService
    {
        Task<UserAccountDto> GetUserByIdAsync(string userId);

        Task<UserAccountDto> PatchProfileAsync(string userId, JsonPatchDocument<UserAccountPatchProfileModels> patchRequest);

        Task UpdatePasswordAsync(string userId, UserAccountUpdatePasswordRequestDto updatePasswordRequest);
    }
}