namespace MathZ.Services.AuthAPI.Services.IServices
{
    using MathZ.Shared.Models;

    public interface IJwtGeneratorService
    {
        string GenerateToken(UserAccount user, IEnumerable<string> roles);
    }
}