namespace MathZ.Services.IdentityApi.Services.IServices;

using FluentResults;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Models.Dtos;
using Microsoft.AspNetCore.JsonPatch;

public interface IUserAccountService
{
    Task<Result<ResponseUserDto>> GetUserByIdAsync(string id);

    Task<Result<ResponseUserDto>> GetUserByUserNameAsync(string userName);

    Task<Result<ResponseUserDto>> GetUserByEmailAsync(string email);

    Task<IEnumerable<ResponseUserDto>> GetUsersAsync(int skip, int count, CancellationToken cancellationToken = default);

    Task<Result<ResponseUserDto>> PatchUserAccountByIdAsync(string id, JsonPatchDocument<UserPatchProfile> patch);

    Task<Result> DeleteUserProfileByIdAsync(string id);

    Task<Result> UpdateUserPasswordByIdAsync(string id, string currentPassword, string newPassword);

    Task<Result> UpdateUserPasswordByIdAsync(string id, string newPassword);

    Task<IEnumerable<ResponseUserDto>> GetUsersInRoleAsync(string role, int skip, int count);

    Task<IEnumerable<string>> GetUserRolesByIdAsync(string id);

    Task<Result> AddUserRolesByIdAsync(string id, IEnumerable<string> roles);

    Task<Result> RemoveUserRolesByIdAsync(string id, IEnumerable<string> roles);
}
