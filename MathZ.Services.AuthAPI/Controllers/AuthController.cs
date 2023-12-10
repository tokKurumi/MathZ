namespace MathZ.Services.AuthAPI.Controllers
{
    using System.Security.Authentication;
    using MathZ.Services.AuthAPI.Exceptions;
    using MathZ.Services.AuthAPI.Models.Dto;
    using MathZ.Services.AuthAPI.Services.IServices;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

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