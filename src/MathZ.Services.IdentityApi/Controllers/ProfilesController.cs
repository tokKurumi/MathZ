namespace MathZ.Services.IdentityApi.Controllers;

using System.Security.Claims;
using Asp.Versioning;
using MathZ.Services.IdentityApi.Features.Commands.ChangeUserPassword;
using MathZ.Services.IdentityApi.Features.Commands.DeleteUser;
using MathZ.Services.IdentityApi.Features.Commands.PatchUser;
using MathZ.Services.IdentityApi.Features.Commands.UpdateUserPassword;
using MathZ.Services.IdentityApi.Features.Queries.GetUserByEmail;
using MathZ.Services.IdentityApi.Features.Queries.GetUserById;
using MathZ.Services.IdentityApi.Features.Queries.GetUserByUserName;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Models.Dtos;
using MathZ.Shared.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class ProfilesController(
    IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("Me")]
    [Authorize]
    public async Task<IActionResult> GetMyProfileAsync()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserProfileByIdAsync([FromRoute] string id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }

    [HttpGet("UserName/{userName}")]
    public async Task<IActionResult> GetUserProfileByUserNameAsync([FromRoute] string userName)
    {
        var query = new GetUserByUserNameQuery(userName);
        var user = await _mediator.Send(query);

        return user.IsSuccess ? Ok(user.Value) : NotFound(user.Errors);
    }

    [HttpGet("Email/{email}")]
    public async Task<IActionResult> GetUserProfileByEmailAsync([FromRoute] string email)
    {
        var query = new GetUserByEmailQuery(email);
        var user = await _mediator.Send(query);

        return user.IsSuccess ? Ok(user.Value) : NotFound(user.Errors);
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetUsersProfilesAsync([FromQuery] PaginationQuery<ResponseUserDto> pagination, CancellationToken cancellation)
    {
        var users = await _mediator.Send(pagination, cancellation);

        return Ok(users);
    }

    [HttpPatch("Me")]
    [Authorize]
    public async Task<IActionResult> PatchMyProfileAsync([FromBody] JsonPatchDocument<UserPatchProfile> patchProfile)
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var command = new PatchUserCommand(id, patchProfile);
        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> PatchUserProfileByIdAsync([FromRoute] string id, [FromBody] JsonPatchDocument<UserPatchProfile> patchProfile)
    {
        var command = new PatchUserCommand(id, patchProfile);
        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
    }

    [HttpPut("Me/Password")]
    [Authorize]
    public async Task<IActionResult> UpdateMyProfilePasswordAsync([FromBody] UpdatePasswordRequestDto updatePasswordRequest)
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var command = new UpdateUserPasswordCommand(id, updatePasswordRequest.CurrentPassword, updatePasswordRequest.NewPassword);
        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }

    [HttpPut("{id}/Password")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateUserProfilePasswordAsync([FromRoute] string id, [FromBody] string newPassword)
    {
        var command = new ChangeUserPasswordCommand(id, newPassword);
        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }

    [HttpDelete("Me")]
    [Authorize]
    public async Task<IActionResult> DeleteMyProfileAsync()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (id is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteUserProfileByIdAsync([FromRoute] string id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }
}
