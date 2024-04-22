namespace MathZ.Services.EmailApi.Controllers;

using Asp.Versioning;
using MathZ.Services.EmailApi.Features.Commands.CreateMailing;
using MathZ.Services.EmailApi.Features.Commands.DeleteMailing;
using MathZ.Services.EmailApi.Features.Commands.UpdateMailing;
using MathZ.Services.EmailApi.Features.Queries.GetMailingById;
using MathZ.Services.EmailApi.Features.Queries.GetMailingByTopic;
using MathZ.Services.EmailApi.Models;
using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Shared.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class MailingsController(
    IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateMailingAsync([FromBody] CreateMailingCommand createMailingRequest, CancellationToken cancellationToken)
    {
        await _mediator.Send(createMailingRequest, cancellationToken);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMailingByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        var query = new GetMailingByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetMailingsAsync([FromQuery] PaginationQuery<MailingDto> pagination, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(pagination, cancellationToken);

        return Ok(result);
    }

    [HttpGet("Topic/{topic}")]
    public async Task<IActionResult> GetMailingByTopicAsync([FromRoute] string topic, CancellationToken cancellationToken)
    {
        var query = new GetMailingByTopicQuery(topic);
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : StatusCode(500, result.Errors);
    }

    [HttpGet("{id}/Emails")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetMailingEmailsByIdAsync([FromRoute] string id, [FromQuery] PaginationQuery<MailingEmailDto> pagination, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(pagination, cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateMailingByIdAsync([FromRoute] string id, [FromBody] JsonPatchDocument<MailingPatch> patch, CancellationToken cancellationToken)
    {
        var command = new UpdateMailingCommand(id, patch);
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteMailingByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        var command = new DeleteMailingCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }
}
