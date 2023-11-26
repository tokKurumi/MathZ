namespace MathZ.Services.AuthAPI.Controllers
{
    using MathZ.Services.AuthAPI.Exceptions;
    using MathZ.Services.AuthAPI.Services.IServices;
    using MathZ.Shared.Models.Dto;
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
    }
}