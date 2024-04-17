namespace MathZ.Services.ForumApi.Controllers;

using System.Security.Claims;
using Asp.Versioning;
using MathZ.Services.ForumApi.Features.Commands.CreateDislike;
using MathZ.Services.ForumApi.Features.Commands.CreateLike;
using MathZ.Services.ForumApi.Features.Commands.CreateMessage;
using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Services.ForumApi.Pagination;
using MathZ.Services.IdentityApi.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class ForumController(
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

    [HttpPost("Like")]
    [Authorize]
    public async Task<IActionResult> LikeMessageAsync([FromBody] LikeMessageRequestDto likeMessageRequest, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new CreateLikeCommand(
            likeMessageRequest.MessageId,
            userId!);

        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("Like")]
    public async Task<IActionResult> GetLikesAsync([FromQuery] PaginationQuery<MessageLikeDto> pagination, CancellationToken cancellationToken)
    {
        var likes = await _mediator.Send(pagination, cancellationToken);

        return Ok(likes);
    }

    [HttpPost("Dislike")]
    [Authorize]
    public async Task<IActionResult> DislikeMessageAsync([FromBody] DislikeMessageRequestDto dislikeMessageRequest, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new CreateDislikeCommand(
            dislikeMessageRequest.MessageId,
            userId!);

        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("Dislike")]
    public async Task<IActionResult> GetDislikesAsync([FromQuery] PaginationQuery<MessageLikeDto> pagination, CancellationToken cancellationToken)
    {
        var dislikes = await _mediator.Send(pagination, cancellationToken);

        return Ok(dislikes);
    }
}
