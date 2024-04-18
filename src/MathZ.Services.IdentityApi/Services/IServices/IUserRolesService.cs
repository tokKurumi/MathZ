namespace MathZ.Services.IdentityApi.Services.IServices;

using FluentResults;

public interface IUserRolesService
{
    /// <summary>
    /// Gets the roles.
    /// </summary>
    /// <returns>An IQueryable of string representing the roles.</returns>
    IQueryable<string> GetRoles();

    /// <summary>
    /// Creates a new role asynchronously.
    /// </summary>
    /// <param name="role">The name of the role to create.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result.</returns>
    Task<Result> CreateRoleAsync(string role);

    /// <summary>
    /// Deletes a role asynchronously.
    /// </summary>
    /// <param name="role">The name of the role to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result.</returns>
    Task<Result> DeleteRoleAsync(string role);
}
