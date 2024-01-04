namespace MathZ.Services.AdminAPI.Controllers;

using MathZ.Services.AdminAPI.Models.Dto;
using MathZ.Services.AdminAPI.Services.IServices;
using MathZ.Shared.Exceptions;
using MathZ.Shared.Models;
using MathZ.Shared.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Authorize(Roles = "admin")]
[Route(@"api/[controller]")]
public class AdminController(IAdminService adminService)
    : ControllerBase
{
    private readonly IAdminService _adminService = adminService;

    [HttpGet(@"Users")]
    public async Task<IActionResult> GetUsersAsync()
    {
        var allUsers = await _adminService.GetAllUsersAsync();

        return Ok(allUsers);
    }

    [HttpGet(@"Users/{userId}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] string userId)
    {
        try
        {
            var user = await _adminService.GetUserByIdAsync(userId);

            return Ok(user);
        }
        catch (UserNotExistException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost(@"Users")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserAccountRegistrationRequestDto model)
    {
        var authorization = HttpContext.Request.Headers.Authorization.Last() ?? string.Empty;

        try
        {
            var createdUser = await _adminService.RegisterUserAsync(authorization, model);
            return Ok(createdUser);
        }
        catch (CreateUserException ex)
        {
            return BadRequest(ex.Problems);
        }
        catch (JsonSerializationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch(@"Users/{userId}")]
    public async Task<IActionResult> PatchUserAsync([FromRoute] string userId, [FromBody] JsonPatchDocument<UserAccountPatchProfileModels> patchRequest)
    {
        try
        {
            var patchedUser = await _adminService.PatchProfileAsync(userId, patchRequest);

            return Ok(patchedUser);
        }
        catch (UserNotExistException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (JsonPatchException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (UpdateProfileException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Errors);
        }
    }

    [HttpPut("Users/{userId}/update-credentials")]
    public async Task<IActionResult> UpdateUserPasswordAsync([FromRoute] string userId, [FromBody] UserAccountChangePasswordRequestDto changePasswordRequest)
    {
        try
        {
            await _adminService.ChangeUserPasswordAsync(userId, changePasswordRequest.NewPassword);

            return Ok();
        }
        catch (UserNotExistException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (UpdateProfileException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    [HttpDelete(@"Users/{userId}")]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] string userId)
    {
        try
        {
            await _adminService.DeleteUserAsync(userId);

            return Ok();
        }
        catch (UserNotExistException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost(@"Users/{userId}/Roles")]
    public async Task<IActionResult> ChangeUserRolesAsync([FromRoute] string userId, [FromBody] IEnumerable<string> roles)
    {
        try
        {
            var user = await _adminService.AddUserRolesAsync(userId, roles);

            return Ok(user);
        }
        catch (UserNotExistException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete(@"Users/{userId}/Roles")]
    public async Task<IActionResult> DeleteUserRolesAsync([FromRoute] string userId, [FromBody] IEnumerable<string> roles)
    {
        try
        {
            var user = await _adminService.DeleteUserRolesAsync(userId, roles);

            return Ok(user);
        }
        catch (UserNotExistException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}