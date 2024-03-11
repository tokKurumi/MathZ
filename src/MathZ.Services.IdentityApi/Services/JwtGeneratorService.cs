namespace MathZ.Services.IdentityApi.Services;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class JwtGeneratorService(
    IConfiguration configuration,
    UserManager<User> userManager)
    : IJwtGeneratorService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<string> GenerateTokenAsync(User user)
    {
        var audience = _configuration.GetValue<string>(Shared.JwtEnvConstants.JwtAudience);
        var issuer = _configuration.GetValue<string>(Shared.JwtEnvConstants.JwtIssuer);
        var secretKey = _configuration.GetValue<string>(Shared.JwtEnvConstants.JwtSecret)
            ?? throw new InvalidOperationException("JWT secret key is null or empty.");

        var claims = await BuildClaimsAsync(user);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenSecriptor = new SecurityTokenDescriptor()
        {
            Audience = audience,
            Issuer = issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenSecriptor);
        return tokenHandler.WriteToken(token);
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
