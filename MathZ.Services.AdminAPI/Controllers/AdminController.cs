namespace MathZ.Services.AdminAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpPost("Dummy")]
        public async Task<IActionResult> Dummy([FromBody] int model)
        {
            await Task.Delay(100);
            return Ok();
        }
    }
}