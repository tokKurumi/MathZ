namespace MathZ.Services.IdentityApi.Services.IServices;

using FluentResults;

public interface IUserRolesService
{
    Task<IEnumerable<string>> GetRolesAsync(int skip, int count, CancellationToken cancellationToken = default);

    Task<Result> CreateRoleAsync(string role);

    Task<Result> DeleteRoleAsync(string role);
}
