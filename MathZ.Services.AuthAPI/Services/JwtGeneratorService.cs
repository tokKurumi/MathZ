namespace MathZ.Services.AuthAPI.Services
{
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using MathZ.Services.AuthAPI.Services.IServices;
    using MathZ.Shared.Models;
    using Microsoft.IdentityModel.Tokens;

    public class JwtGeneratorService : IJwtGeneratorService
    {
        private readonly IConfiguration _configuration;

        public JwtGeneratorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserAccount user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["ApiSettings:JwtOptions:Secret"] ?? string.Empty);

            var claimList = BuildClaimList(user, roles);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["ApiSettings:JwtOptions:Audience"],
                Issuer = _configuration["ApiSettings:JwtOptions:Issuer"],
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private IEnumerable<Claim> BuildClaimList(UserAccount user, IEnumerable<string> roles)
        {
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
            };

            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claimList;
        }
    }
}