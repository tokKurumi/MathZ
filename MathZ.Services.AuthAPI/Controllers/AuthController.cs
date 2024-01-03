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

        [HttpPost("Register")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Register([FromBody] UserAccountRegistrationRequestDto model)
        {
            try
            {
                var result = await _authService.Register(model);

                return Ok(result);
            }
            catch (CreateUserException ex)
            {
                return BadRequest(ex.Problems);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserAccountLoginRequestDto model)
        {
            try
            {
                var result = await _authService.Login(model);

                return Ok(result);
            }
            catch (AuthenticationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}