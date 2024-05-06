namespace MathZ.Services.IdentityApi.Controllers;

using Asp.Versioning;
using MathZ.Services.IdentityApi.Features.Commands.CreateUser;
using MathZ.Services.IdentityApi.Features.Queries.GetToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class AuthController(
    IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserCommand registrationRequest, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(registrationRequest, cancellationToken);

        return result.Succeeded ? Ok() : BadRequest(result.Errors);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] GetTokenQuery loginRequest, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(loginRequest, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
}
