namespace MathZ.Services.IdentityApi.Controllers;

using Asp.Versioning;
using MathZ.Services.IdentityApi.Services.IServices;
using MathZ.Shared.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Authorize(Roles = "admin")]
[Route("v{version:apiVersion}/[controller]")]
public class RolesController(
    IUserRolesService userRolesService)
    : ControllerBase
{
    private readonly IUserRolesService _userRolesService = userRolesService;

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAsync([FromQuery] PaginationRequest pagination, CancellationToken cancellationToken)
    {
        var roles = await _userRolesService.GetRolesAsync((pagination.PageNumber - 1) * pagination.PageSize, pagination.PageSize, cancellationToken);
        var total = await _userRolesService.GetRolesCountAsync(cancellationToken);

        return Ok(PaginationResponse<string>.Create(roles, total, pagination));
    }

    [HttpPost("{role}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateAsync([FromRoute] string role)
    {
        var createResult = await _userRolesService.CreateRoleAsync(role);

        return createResult.IsSuccess ? Ok() : StatusCode(500, createResult.Errors);
    }

    [HttpDelete("{role}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string role)
    {
        var deleteResult = await _userRolesService.DeleteRoleAsync(role);

        return deleteResult.IsSuccess ? Ok() : StatusCode(500, deleteResult.Errors);
    }
}
