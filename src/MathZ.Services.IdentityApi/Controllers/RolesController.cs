namespace MathZ.Services.IdentityApi.Controllers;

using Asp.Versioning;
using MathZ.Services.IdentityApi.Features.Commands.CreateRole;
using MathZ.Services.IdentityApi.Features.Commands.DeleteRole;
using MathZ.Shared.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Authorize(Roles = "admin")]
[Route("v{version:apiVersion}/[controller]")]
public class RolesController(
    IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAsync([FromQuery] PaginationQuery<string> pagination, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(pagination, cancellationToken);

        return Ok(result);
    }

    [HttpPost("{role}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateAsync([FromRoute] string role)
    {
        var command = new CreateRoleCommand(role);
        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }

    [HttpDelete("{role}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string role)
    {
        var command = new DeleteRoleCommand(role);
        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }
}
