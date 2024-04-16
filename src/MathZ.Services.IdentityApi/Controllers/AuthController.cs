namespace MathZ.Services.IdentityApi.Controllers;

using Asp.Versioning;
using MassTransit;
using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Services.IdentityApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class AuthController(
    IAuthService authService,
    IUserAccountService userAccountService,
    IPublishEndpoint publishEndpoint)
    : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IUserAccountService _userAccountService = userAccountService;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequestDto registrationRequest)
    {
        var result = await _authService.RegisterAsync(registrationRequest);
        if (result.Succeeded)
        {
            var createdUser = await _userAccountService.GetUserByUserNameAsync(registrationRequest.UserName);

            if (createdUser.IsSuccess)
            {
                await _publishEndpoint.Publish(createdUser.Value);
            }
        }

        return result.Succeeded ? Ok() : BadRequest(result.Errors);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequest)
    {
        var tokenResult = await _authService.LoginAsync(loginRequest);

        return tokenResult.IsSuccess ? Ok(tokenResult.Value) : BadRequest(tokenResult.Reasons);
    }
}
