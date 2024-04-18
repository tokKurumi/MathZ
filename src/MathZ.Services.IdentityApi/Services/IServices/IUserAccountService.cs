namespace MathZ.Services.IdentityApi.Services.IServices;

using FluentResults;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Models.Dtos;
using Microsoft.AspNetCore.JsonPatch;

public interface IUserAccountService
{
    /// <summary>
    /// Get a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user information.</returns>
    Task<Result<ResponseUserDto>> GetUserByIdAsync(string id);

    /// <summary>
    /// Get a user by their username.
    /// </summary>
    /// <param name="userName">The username of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user information.</returns>
    Task<Result<ResponseUserDto>> GetUserByUserNameAsync(string userName);

    /// <summary>
    /// Get a user by their email.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user information.</returns>
    Task<Result<ResponseUserDto>> GetUserByEmailAsync(string email);

    /// <summary>
    /// Get all users.
    /// </summary>
    /// <returns>An IQueryable of ResponseUserDto representing all users.</returns>
    IQueryable<ResponseUserDto> GetUsers();

    /// <summary>
    /// Update a user's profile by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="patch">The JSON patch document containing the updates to apply.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated user information.</returns>
    Task<Result<ResponseUserDto>> PatchUserAccountByIdAsync(string id, JsonPatchDocument<UserPatchProfile> patch);

    /// <summary>
    /// Delete a user's profile by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result> DeleteUserProfileByIdAsync(string id);

    /// <summary>
    /// Update a user's password by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="currentPassword">The current password of the user.</param>
    /// <param name="newPassword">The new password to set for the user.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result> UpdateUserPasswordByIdAsync(string id, string currentPassword, string newPassword);

    /// <summary>
    /// Update a user's password by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="newPassword">The new password to set for the user.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result> ChangeUserPasswordAsync(string id, string newPassword);

    /// <summary>
    /// Get a list of users in a specific role.
    /// </summary>
    /// <param name="role">The role to filter by.</param>
    /// <param name="skip">The number of users to skip.</param>
    /// <param name="count">The number of users to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of users.</returns>
    Task<IEnumerable<ResponseUserDto>> GetUsersInRoleAsync(string role, int skip, int count);

    /// <summary>
    /// Get the roles of a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of roles.</returns>
    Task<IEnumerable<string>> GetUserRolesByIdAsync(string id);

    /// <summary>
    /// Add roles to a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="roles">The roles to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result> AddUserRolesByIdAsync(string id, IEnumerable<string> roles);

    /// <summary>
    /// Remove roles from a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="roles">The roles to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result> RemoveUserRolesByIdAsync(string id, IEnumerable<string> roles);
}
