namespace MathZ.Services.EmailAPI.Controllers;

using MathZ.Services.EmailAPI.Services.IServices;
using MathZ.Shared.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class EmailController(IEmailService emailService)
    : ControllerBase
{
    private readonly IEmailService _emailService = emailService;

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] Email email)
    {
        await _emailService.SendMessageAsync(email);

        return Ok();
    }
}