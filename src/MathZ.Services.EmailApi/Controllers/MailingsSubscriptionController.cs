namespace MathZ.Services.EmailApi.Controllers;

using System.Security.Claims;
using Asp.Versioning;
using MathZ.Services.EmailApi.Features.Commands.SubscribeMailingById;
using MathZ.Services.EmailApi.Features.Commands.UnsibscribeMailing;
using MathZ.Services.EmailApi.Features.Queries.GetMailingSubscribersById;
using MathZ.Services.EmailApi.Features.Queries.GetSubscribedMailingsByEmail;
using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Shared.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class MailingsSubscriptionController(
    IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("{mailingId}")]
    [Authorize]
    public async Task<IActionResult> SubscribeMailingByIdAsync([FromRoute] string mailingId, CancellationToken cancellationToken)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (email is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var command = new SubscribeMailingByIdCommand(mailingId, email);
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetSubscribedMailingsByEmailAsync([FromQuery] PaginationQuery<MailingDto> pagination, CancellationToken cancellationToken)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (email is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var query = new GetSubscribedMailingsByEmailQuery(email, pagination);
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{mailingId}/Subscribers")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetMailingSubscribersByIdAsync([FromRoute] string mailingId, [FromQuery] PaginationQuery<string> pagination, CancellationToken cancellationToken)
    {
        var query = new GetMailingSubscribersByIdCommand(mailingId, pagination);
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{mailingId}")]
    [Authorize]
    public async Task<IActionResult> UnsubscribeMailingByIdAsync([FromRoute] string mailingId, CancellationToken cancellationToken)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (email is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var command = new UnsubscribeMailingCommand(mailingId, email);
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }
}
