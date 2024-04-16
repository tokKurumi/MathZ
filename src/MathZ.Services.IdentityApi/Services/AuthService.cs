namespace MathZ.Services.IdentityApi.Services;

using AutoMapper;
using FluentResults;
using MassTransit;
using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Services.IdentityApi.Services.IServices;
using MathZ.Shared.Models;
using Microsoft.AspNetCore.Identity;

public class AuthService(
    IMapper mapper,
    UserManager<User> userManager,
    IJwtGeneratorService jwtGeneratorService,
    IPublishEndpoint publishEndpoint)
    : IAuthService
{
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtGeneratorService _jwtGeneratorService = jwtGeneratorService;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

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

        var result = await _userManager.CreateAsync(userToCreate, registrationRequest.Password);

        return result;
    }
}
