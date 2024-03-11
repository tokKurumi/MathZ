namespace MathZ.Services.IdentityApi.Services.IServices;

using System.Threading.Tasks;
using MathZ.Services.IdentityApi.Models;

public interface IJwtGeneratorService
{
    Task<string> GenerateTokenAsync(User user);
}