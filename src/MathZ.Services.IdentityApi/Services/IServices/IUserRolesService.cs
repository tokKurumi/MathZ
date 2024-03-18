namespace MathZ.Services.IdentityApi.Services.IServices;

using FluentResults;

public interface IUserRolesService
{
    /// <summary>
    /// Retrieves a collection of roles asynchronously.
    /// </summary>
    /// <param name="skip">The number of roles to skip.</param>
    /// <param name="count">The number of roles to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of roles.</returns>
    Task<IEnumerable<string>> GetRolesAsync(int skip, int count, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the count of roles asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the count of roles.</returns>
    Task<int> GetRolesCountAsync(CancellationToken cancellationToken = default);

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
