namespace MathZ.Services.EmailApi.Controllers;

using Asp.Versioning;
using MathZ.Services.EmailApi.Features.Commands.SendEmail;
using MathZ.Services.EmailApi.Features.Commands.SendEmailToTopic;
using MathZ.Services.EmailApi.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Authorize(Roles = "admin")]
[Route("v{version:apiVersion}/[controller]")]
public class EmailController(
    IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> SendEmailAsync([FromBody] SendEmailCommand email, CancellationToken cancellationToken)
    {
        await _mediator.Send(email, cancellationToken);

        return Ok();
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> SendEmailToTopicAsync([FromRoute] string id, [FromBody] SendTopicEmailRequestDto email, CancellationToken cancellationToken)
    {
        var command = new SendEmailToTopicCommand(id, email.Subject, email.Body);
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }
}
