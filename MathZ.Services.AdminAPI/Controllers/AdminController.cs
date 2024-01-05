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

    /// <summary>
    /// Retrieves a list of all users.
    /// </summary>
    /// <returns>
    /// Returns an IActionResult with a 200 (OK) status code and a list of all users.
    /// </returns>
    [HttpGet(@"users")]
    public async Task<IActionResult> GetUsersAsync()
    {
        // Retrieves a list of all users
        var allUsers = await _adminService.GetAllUsersAsync();

        // Returns a result with the list of users
        return Ok(allUsers);
    }

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>
    /// Returns an IActionResult.
    /// If the user is found, it returns a result with a 200 (OK) status code and the user information.
    /// If the user is not found, it returns a result with a 404 (Not Found) status code and error information.
    /// </returns>
    [HttpGet(@"users/{userId}")]
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

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="createUserModel">The model containing the registration information for the new user.</param>
    /// <returns>
    /// Returns an IActionResult.
    /// If the user creation is successful, it returns a result with a 200 (OK) status code and the created user information.
    /// If there is an error during user creation, it returns a result with a 400 (Bad Request) status code and error information.
    /// </returns>
    [HttpPost(@"users")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserAccountRegistrationRequestDto createUserModel)
    {
        var authorization = HttpContext.Request.Headers.Authorization.Last() ?? string.Empty;

        try
        {
            var createdUser = await _adminService.RegisterUserAsync(authorization, createUserModel);
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

    /// <summary>
    /// Updates a user's profile partially using a JSON patch document.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="patchUserRequest">The JSON patch document containing the changes to apply to the user profile.</param>
    /// <returns>
    /// Returns an IActionResult.
    /// If the update is successful, it returns a result with a 200 (OK) status code and the updated user profile.
    /// If the user is not found, it returns a result with a 404 (Not Found) status code and error information.
    /// If the provided JSON patch document is invalid, it returns a result with a 400 (Bad Request) status code and error information.
    /// If there is an error during the profile update, it returns a result with a 500 (Internal Server Error) status code and error details.
    /// </returns>
    [HttpPatch(@"users/{userId}")]
    public async Task<IActionResult> PatchUserAsync([FromRoute] string userId, [FromBody] JsonPatchDocument<UserAccountPatchProfileModels> patchUserRequest)
    {
        try
        {
            var patchedUser = await _adminService.PatchProfileAsync(userId, patchUserRequest);

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

    /// <summary>
    /// Updates a user's password.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="changeUserPasswordRequest">The model containing the updated password information.</param>
    /// <returns>
    /// Returns an IActionResult.
    /// If the password update is successful, it returns a result with a 200 (OK) status code.
    /// If the user is not found, it returns a result with a 404 (Not Found) status code and error information.
    /// If there is an error during the password update, it returns a result with a 400 (Bad Request) status code and error details.
    /// </returns>
    [HttpPut("users/{userId}/update-credentials")]
    public async Task<IActionResult> UpdateUserPasswordAsync([FromRoute] string userId, [FromBody] UserAccountChangePasswordRequestDto changeUserPasswordRequest)
    {
        try
        {
            await _adminService.ChangeUserPasswordAsync(userId, changeUserPasswordRequest.NewPassword);

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

    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>
    /// Returns an IActionResult.
    /// If the user deletion is successful, it returns a result with a 200 (OK) status code.
    /// If the user is not found, it returns a result with a 404 (Not Found) status code and error information.
    /// </returns>
    [HttpDelete(@"users/{userId}")]
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

    /// <summary>
    /// Changes the roles of a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="roles">The roles to assign to the user.</param>
    /// <returns>
    /// Returns an IActionResult.
    /// If the role change is successful, it returns a result with a 200 (OK) status code and the updated user information.
    /// If the user is not found, it returns a result with a 404 (Not Found) status code and error information.
    /// </returns>
    [HttpPost(@"users/{userId}/Roles")]
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

    /// <summary>
    /// Deletes roles from a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="roles">The roles to remove from the user.</param>
    /// <returns>
    /// Returns an IActionResult.
    /// If the role deletion is successful, it returns a result with a 200 (OK) status code and the updated user information.
    /// If the user is not found, it returns a result with a 404 (Not Found) status code and error information.
    /// </returns>
    [HttpDelete(@"users/{userId}/Roles")]
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