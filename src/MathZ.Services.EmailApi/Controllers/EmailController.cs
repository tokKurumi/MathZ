namespace MathZ.Services.EmailApi.Controllers;

using Asp.Versioning;
using MathZ.Services.EmailApi.Models;
using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Services.EmailApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class EmailController(
    IEmailSenderService emailSenderService,
    IMailingService mailingService)
    : ControllerBase
{
    private readonly IEmailSenderService _emailSenderService = emailSenderService;
    private readonly IMailingService _mailingService = mailingService;

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> SendEmailAsync([FromBody] SendEmailRequestDto email, CancellationToken cancellationToken)
    {
        await _emailSenderService.SendEmailAsync(email.To, email.Subject, email.Body, cancellationToken);

        return Ok();
    }

    [HttpPost("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> SendEmailToTopicAsync([FromRoute] string id, [FromBody] SendTopicEmailRequestDto email, CancellationToken cancellationToken)
    {
        var emailsResult = await _mailingService.SendEmailByIdAsync(id, email.Subject, email.Body, cancellationToken);

        if (!emailsResult.IsSuccess)
        {
            return StatusCode(500, emailsResult.Errors);
        }

        var to = emailsResult.Value.Select(e => new MailAddress(string.Empty, e));

        await _emailSenderService.SendEmailAsync(to, email.Subject, email.Body, cancellationToken);

        return Ok();
    }
}
