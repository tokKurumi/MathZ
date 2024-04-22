namespace MathZ.Services.ForumApi.Controllers;

using System.Security.Claims;
using Asp.Versioning;
using MathZ.Services.ForumApi.Features.Commands.CreateDislike;
using MathZ.Services.ForumApi.Features.Commands.CreateLike;
using MathZ.Services.ForumApi.Features.Commands.CreateMessage;
using MathZ.Services.ForumApi.Features.Queries.GetDislikes;
using MathZ.Services.ForumApi.Features.Queries.GetLikes;
using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Shared.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class ForumsController(
    IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageRequestDto createMessageRequest, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new CreateMessageCommand(
            createMessageRequest.ParentMessageId,
            userId!,
            createMessageRequest.Text);

        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetMessagesAsync([FromQuery] PaginationQuery<MessageDto> pagination, CancellationToken cancellationToken)
    {
        var messages = await _mediator.Send(pagination, cancellationToken);

        return Ok(messages);
    }

    [HttpPost("Likes/{messageId}")]
    [Authorize]
    public async Task<IActionResult> LikeMessageAsync([FromRoute] string messageId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new CreateLikeCommand(
            messageId,
            userId!);

        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("Likes/{messageId}")]
    public async Task<IActionResult> GetLikesAsync([FromRoute] string messageId, [FromQuery] PaginationQuery<MessageLikeDto> pagination, CancellationToken cancellationToken)
    {
        var query = new GetLikesQuery(messageId, pagination);
        var likes = await _mediator.Send(query, cancellationToken);

        return Ok(likes);
    }

    [HttpPost("Dislikes/{messageId}")]
    [Authorize]
    public async Task<IActionResult> DislikeMessageAsync([FromRoute] string messageId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new CreateDislikeCommand(
            messageId,
            userId!);

        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("Dislikes/{messageId}")]
    public async Task<IActionResult> GetDislikesAsync([FromRoute] string messageId, [FromQuery] PaginationQuery<MessageLikeDto> pagination, CancellationToken cancellationToken)
    {
        var query = new GetDislikesQuery(messageId, pagination);
        var dislikes = await _mediator.Send(query, cancellationToken);

        return Ok(dislikes);
    }
}
