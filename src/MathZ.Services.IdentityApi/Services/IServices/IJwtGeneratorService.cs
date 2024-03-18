namespace MathZ.Services.IdentityApi.Services.IServices;

using System.Threading.Tasks;
using MathZ.Services.IdentityApi.Models;

public interface IJwtGeneratorService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token should be generated.</param>
    /// <returns>The generated JWT token.</returns>
    Task<string> GenerateTokenAsync(User user);
}
