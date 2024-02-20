namespace MathZ.Services.AuthAPI.Services;

using System.Security.Authentication;
using AutoMapper;
using MathZ.Services.AuthAPI.Models.Dto;
using MathZ.Services.AuthAPI.Services.IServices;
using MathZ.Shared.Data;
using MathZ.Shared.Exceptions;
using MathZ.Shared.Models;
using MathZ.Shared.Models.Dto;
using Microsoft.AspNetCore.Identity;

public class AuthService(
    IMapper mapper,
    UsersDbContext dbContext,
    UserManager<UserAccount> userManager,
    RoleManager<IdentityRole> roleManager,
    IJwtGeneratorService jwtGeneratorService,
    HttpClient emailApi)
    : IAuthService
{
    private readonly IMapper _mapper = mapper;
    private readonly UsersDbContext _dbContext = dbContext;
    private readonly UserManager<UserAccount> _userManager = userManager;
    private readonly IJwtGeneratorService _jwtGeneratorService = jwtGeneratorService;
    private readonly HttpClient _emailApi = emailApi;

    public async Task<UserAccountLoginResponseDto> LoginAsync(UserAccountLoginRequestDto loginRequestDto)
    {
        var user = await _userManager.FindByNameAsync(loginRequestDto.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequestDto.Password))
        {
            throw new AuthenticationException("Invalid username or password.");
        }

        var token = await GenerateJwtTokenAsync(user);

        return new UserAccountLoginResponseDto()
        {
            Token = token,
        };
    }

    public async Task<IdentityResult?> RegisterAsync(UserAccountRegistrationRequestDto registrationRequestDto)
    {
        var user = _mapper.Map<UserAccount>(registrationRequestDto);

        var result = await _userManager.CreateAsync(user, registrationRequestDto.Password ?? string.Empty);

        if (result.Succeeded)
        {
            var emailCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var from = new MailBoxAddress("MathZ Team", "admin@mathz.com");
            var to = new MailBoxAddress(user.FirstName, user.Email);
            var subject = "Email Confirmation";
            var body = emailCode;

            var email = new Email([from], [to], subject, body);

            await _emailApi.PostAsJsonAsync("api/email", email, default);
        }

        return result;
    }

    public async Task<IdentityResult?> ConfirmEmailAsync(string email, string confirmCode)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotExistException();

        return await _userManager.ConfirmEmailAsync(user, confirmCode);
    }

    private async Task<string> GenerateJwtTokenAsync(UserAccount user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtGeneratorService.GenerateToken(user, roles);
        return token;
    }
}