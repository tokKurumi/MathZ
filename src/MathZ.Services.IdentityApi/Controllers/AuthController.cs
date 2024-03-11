namespace MathZ.Services.IdentityApi.Controllers;

using Asp.Versioning;
using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Services.IdentityApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class AuthController(IAuthService authService)
    : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequestDto registrationRequest)
    {
        var result = await _authService.RegisterAsync(registrationRequest);

        return result.Succeeded ? Ok() : BadRequest();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequest)
    {
        var tokenResult = await _authService.LoginAsync(loginRequest);

        return tokenResult.IsSuccess ? Ok(tokenResult.Value) : BadRequest(tokenResult.Reasons);
    }
}
