namespace MathZ.Services.ForumApi.Features.Commands.CreateDislike;

using System.ComponentModel;
using MediatR;

[DisplayName("DislikeMessageRequest")]
public record CreateDislikeCommand(
    string MessageId,
    string UserId)
    : IRequest;
