namespace MathZ.Services.UserAPI.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using MathZ.Services.UserAPI.Models.Dto;
    using MathZ.Services.UserAPI.Services.IServices;
    using MathZ.Shared.Exceptions;
    using MathZ.Shared.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.JsonPatch.Exceptions;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("api/[controller]/profile")]
    public class UserController(IUserAccountService userAccountService)
        : ControllerBase
    {
        private readonly IUserAccountService _userAccountService = userAccountService;

        /// <summary>
        /// Retrieves the user profile.
        /// </summary>
        /// <returns>
        /// Returns an IActionResult.
        /// If the user exists, it returns a result with a 200 (OK) status code and the user profile information.
        /// If the user is not found, it returns a result with a 400 (Bad Request) status code and error information.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetProfileAsync()
        {
            try
            {
                var myProfile = await _userAccountService.GetUserByIdAsync(
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new UserNotExistException());

                return Ok(myProfile);
            }
            catch (UserNotExistException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Updates the user profile partially using a JSON patch document.
        /// </summary>
        /// <param name="patchRequest">The JSON patch document containing the changes to apply to the user profile.</param>
        /// <returns>
        /// Returns an IActionResult.
        /// If the update is successful, it returns a result with a 200 (OK) status code and the updated user profile.
        /// If the user is not found, it returns a result with a 404 (Not Found) status code and error information.
        /// If the provided JSON patch document is invalid, it returns a result with a 400 (Bad Request) status code and error information.
        /// If there is an error during the profile update, it returns a result with a 500 (Internal Server Error) status code and error details.
        /// </returns>
        [HttpPatch]
        public async Task<IActionResult> PatchProfileAsync([FromBody] JsonPatchDocument<UserAccountPatchProfileModels> patchRequest)
        {
            try
            {
                var newProfile = await _userAccountService.PatchProfileAsync(
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UserNotExistException(),
                    patchRequest);

                return Ok(newProfile);
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
        /// Updates the user's password.
        /// </summary>
        /// <param name="updateCredentialsModel">The model containing the updated password information.</param>
        /// <returns>
        /// Returns an IActionResult.
        /// If the password update is successful, it returns a result with a 200 (OK) status code.
        /// If the user is not found, it returns a result with a 400 (Bad Request) status code and error information.
        /// If there is an error during the password update, it returns a result with a 500 (Internal Server Error) status code and error details.
        /// </returns>
        [HttpPut("update-credentials")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UserAccountUpdatePasswordRequestDto updateCredentialsModel)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UserNotExistException();

                await _userAccountService.UpdatePasswordAsync(userId, updateCredentialsModel);

                return Ok();
            }
            catch (UserNotExistException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (UpdateProfileException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Errors);
            }
        }
    }
}