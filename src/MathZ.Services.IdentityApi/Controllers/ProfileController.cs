namespace MathZ.Services.IdentityApi.Controllers;

using System.Security.Claims;
using Asp.Versioning;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Services.IdentityApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class ProfileController(
    IUserAccountService userAccountService)
    : ControllerBase
{
    private readonly IUserAccountService _userAccountService = userAccountService;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetMyProfileAsync()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var myProfileResult = await _userAccountService.GetUserByIdAsync(id);
        return myProfileResult.IsSuccess ? Ok(myProfileResult.Value) : StatusCode(500, myProfileResult.Errors);
    }

    [HttpGet("Id/{id}")]
    public async Task<IActionResult> GetUserProfileByIdAsync([FromRoute] string id)
    {
        var user = await _userAccountService.GetUserByIdAsync(id);

        return user.IsSuccess ? Ok(user.Value) : NotFound(user.Errors);
    }

    [HttpGet("UserName/{userName}")]
    public async Task<IActionResult> GetUserProfileByUserNameAsync([FromRoute] string userName)
    {
        var user = await _userAccountService.GetUserByUserNameAsync(userName);

        return user.IsSuccess ? Ok(user.Value) : NotFound(user.Errors);
    }

    [HttpGet("Email/{email}")]
    public async Task<IActionResult> GetUserProfileByEmailAsync([FromRoute] string email)
    {
        var user = await _userAccountService.GetUserByEmailAsync(email);

        return user.IsSuccess ? Ok(user.Value) : NotFound(user.Errors);
    }

    [HttpGet("All")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetUsersProfilesAsync([FromQuery] int skip, [FromQuery] int count, CancellationToken cancellation)
    {
        var users = await _userAccountService.GetUsersAsync(skip, count, cancellation);

        return Ok(users);
    }

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> PatchMyProfileAsync(JsonPatchDocument<UserPatchProfile> patchProfile)
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var patchResult = await _userAccountService.PatchUserAccountByIdAsync(id, patchProfile);
        return patchResult.IsSuccess ? Ok(patchResult.Value) : StatusCode(500, patchResult.Errors);
    }

    [HttpPatch("Id/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> PatchUserProfileByIdAsync([FromRoute] string id, [FromBody] JsonPatchDocument<UserPatchProfile> patchProfile)
    {
        var patchResult = await _userAccountService.PatchUserAccountByIdAsync(id, patchProfile);
        return patchResult.IsSuccess ? Ok(patchResult.Value) : StatusCode(500, patchResult.Errors);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateMyProfilePasswordAsync([FromBody] UpdatePasswordRequestDto updatePasswordRequest)
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var updatePasswordResult = await _userAccountService.UpdateUserPasswordByIdAsync(id, updatePasswordRequest.CurrentPassword, updatePasswordRequest.NewPassword);
        return updatePasswordResult.IsSuccess ? Ok() : StatusCode(500, updatePasswordResult.Errors);
    }

    [HttpPut("Id/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateUserProfilePasswordAsync([FromRoute] string id, [FromBody] string newPassword)
    {
        var updatePasswordResult = await _userAccountService.UpdateUserPasswordByIdAsync(id, newPassword);
        return updatePasswordResult.IsSuccess ? Ok() : StatusCode(500, updatePasswordResult.Errors);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteMyProfileAsync()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var deleteProfileResult = await _userAccountService.DeleteUserProfileByIdAsync(id);
        return deleteProfileResult.IsSuccess ? Ok() : StatusCode(500, deleteProfileResult.Errors);
    }

    [HttpDelete("Id/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteUserProfileByIdAsync([FromRoute] string id)
    {
        var deleteProfileResult = await _userAccountService.DeleteUserProfileByIdAsync(id);
        return deleteProfileResult.IsSuccess ? Ok() : StatusCode(500, deleteProfileResult.Errors);
    }
}
