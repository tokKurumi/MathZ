namespace MathZ.Services.IdentityApi.Services;

using AutoMapper;
using FluentResults;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Services.IdentityApi.Services.IServices;
using Microsoft.AspNetCore.Identity;

public class AuthService(
    IMapper mapper,
    UserManager<User> userManager,
    IJwtGeneratorService jwtGeneratorService)
    : IAuthService
{
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtGeneratorService _jwtGeneratorService = jwtGeneratorService;

    public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequest)
    {
        var user = await _userManager.FindByNameAsync(loginRequest.UserName);

        return user switch
        {
            not null => new LoginResponseDto(await _jwtGeneratorService.GenerateTokenAsync(user)),
            _ => Result.Fail<LoginResponseDto>("Invalid UserName or Password."),
        };
    }

    public async Task<IdentityResult> RegisterAsync(RegistrationRequestDto registrationRequest)
    {
        var userToCreate = _mapper.Map<User>(registrationRequest);

        return await _userManager.CreateAsync(userToCreate, registrationRequest.Password);
    }
}
