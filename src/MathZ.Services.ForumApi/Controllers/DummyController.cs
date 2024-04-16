namespace MathZ.Services.ForumApi.Controllers;

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class DummyController
    : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello from Forum API");
    }
}
