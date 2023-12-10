namespace MathZ.Services.UserAPI.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using MathZ.Services.UserAPI.Exceptions;
    using MathZ.Services.UserAPI.Models;
    using MathZ.Services.UserAPI.Models.Dto;
    using MathZ.Services.UserAPI.Services.IServices;
    using MathZ.Shared.Exceptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.JsonPatch.Exceptions;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;

        public UserController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

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

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UserAccountUpdatePasswordRequestDto model)
        {
            try
            {
                await _userAccountService.UpdatePasswordAsync(
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UserNotExistException(),
                    model);

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