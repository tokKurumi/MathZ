namespace MathZ.Services.IdentityApi.Controllers;

using Asp.Versioning;
using MathZ.Services.IdentityApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class RolesController(
    IUserRolesService userRolesService)
    : ControllerBase
{
    private readonly IUserRolesService _userRolesService = userRolesService;

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetRolesAsync([FromQuery] int skip, [FromQuery] int count, CancellationToken cancellationToken)
    {
        var roles = await _userRolesService.GetRolesAsync(skip, count, cancellationToken);

        return Ok(roles);
    }

    [HttpPost("{role}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateRoleAsync([FromRoute] string role)
    {
        var createResult = await _userRolesService.CreateRoleAsync(role);

        return createResult.IsSuccess ? Ok() : StatusCode(500, createResult.Errors);
    }

    [HttpDelete("{role}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteRoleAsync([FromRoute] string role)
    {
        var deleteResult = await _userRolesService.DeleteRoleAsync(role);

        return deleteResult.IsSuccess ? Ok() : StatusCode(500, deleteResult.Errors);
    }
}
