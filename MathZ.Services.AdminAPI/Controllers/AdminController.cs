namespace MathZ.Services.AdminAPI.Controllers;

using MathZ.Services.AdminAPI.Services.IServices;
using MathZ.Shared.Exceptions;
using MathZ.Shared.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Authorize(Roles = "admin")]
[Route("api/[controller]")]
public class AdminController(IAdminService adminService)
    : ControllerBase
{
    private readonly IAdminService _adminService = adminService;

    [HttpGet("Users")]
    public async Task<IActionResult> GetUsersAsync()
    {
        var allUsers = await _adminService.GetAllUsers();

        return Ok(allUsers);
    }

    [HttpGet("Users/{userId}")]
    public async Task<IActionResult> GetUser([FromRoute] string userId)
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

    [HttpPost("Register")]
    public async Task<IActionResult> CreateUser([FromBody] UserAccountRegistrationRequestDto model)
    {
        var authorization = HttpContext.Request.Headers.Authorization.Last() ?? string.Empty;

        try
        {
            var createdUser = await _adminService.RegisterUser(authorization, model);
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
}