namespace MathZ.Services.IdentityApi.Services;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MathZ.Services.IdentityApi.Services.IServices;
using MathZ.Shared.Jwt;
using MathZ.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class JwtGeneratorService(
    IConfiguration configuration,
    UserManager<User> userManager)
    : IJwtGeneratorService
{
    private const int ExpiresInSeconds = 1800;

    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<string> GenerateTokenAsync(User user)
    {
        var audience = _configuration.GetValue<string>(JwtEnvConstants.JwtAudience);
        var issuer = _configuration.GetValue<string>(JwtEnvConstants.JwtIssuer);
        var secretKey = _configuration.GetValue<string>(JwtEnvConstants.JwtSecret)
            ?? throw new InvalidOperationException("JWT secret key is null or empty.");

        var claims = await BuildClaimsAsync(user);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDecriptor = new SecurityTokenDescriptor()
        {
            Audience = audience,
            Issuer = issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(ExpiresInSeconds),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256Signature),
        };

        var securityToken = tokenHandler.CreateToken(tokenDecriptor);

        var token = tokenHandler.WriteToken(securityToken);
        return token;
    }

    protected async Task<IEnumerable<Claim>> BuildClaimsAsync(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}
