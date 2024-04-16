namespace MathZ.Services.IdentityApi.Services;

using FluentResults;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Services.IServices;
using MathZ.Shared.Models;
using Microsoft.AspNetCore.Identity;

public class AuthService(
    UserManager<User> userManager,
    IJwtGeneratorService jwtGeneratorService)
    : IAuthService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtGeneratorService _jwtGeneratorService = jwtGeneratorService;

    public async Task<Result<JwtToken>> LoginAsync(User user, string password)
    {
        var foundUser = await _userManager.FindByNameAsync(user.UserName!);
        if (foundUser is null)
        {
            return Result.Fail<JwtToken>("This user does not exist.");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(foundUser, password);
        if (!isPasswordValid)
        {
            return Result.Fail<JwtToken>("Invalid UserName or Password.");
        }

        var token = await _jwtGeneratorService.GenerateTokenAsync(foundUser);
        var loginResponse = new JwtToken(token);

        return Result.Ok(loginResponse);
    }

    public async Task<IdentityResult> RegisterAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        return result;
    }
}
