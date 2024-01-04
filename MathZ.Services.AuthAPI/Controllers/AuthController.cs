namespace MathZ.Services.AuthAPI.Controllers
{
    using System.Security.Authentication;
    using MathZ.Services.AuthAPI.Models.Dto;
    using MathZ.Services.AuthAPI.Services.IServices;
    using MathZ.Shared.Exceptions;
    using MathZ.Shared.Models.Dto;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class AuthController(IAuthService authService)
        : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registrateUserModel">The model containing the registration information for the new user.</param>
        /// <returns>
        /// Returns an IActionResult.
        /// If the user registration is successful, it returns a result with a 200 (OK) status code and the registration result.
        /// If there is an error during user creation, it returns a result with a 400 (Bad Request) status code and error information.
        /// </returns>
        [HttpPost("Register")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Register([FromBody] UserAccountRegistrationRequestDto registrateUserModel)
        {
            try
            {
                var result = await _authService.Register(registrateUserModel);

                return Ok(result);
            }
            catch (CreateUserException ex)
            {
                return BadRequest(ex.Problems);
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginUserModel">The model containing the login information for the user.</param>
        /// <returns>
        /// Returns an IActionResult.
        /// If the login is successful, it returns a result with a 200 (OK) status code and the login result.
        /// If there is an authentication error, it returns a result with a 400 (Bad Request) status code and error information.
        /// </returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserAccountLoginRequestDto loginUserModel)
        {
            try
            {
                var result = await _authService.Login(loginUserModel);

                return Ok(result);
            }
            catch (AuthenticationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}