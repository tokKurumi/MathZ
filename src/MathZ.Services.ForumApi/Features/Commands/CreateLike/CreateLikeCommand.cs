namespace MathZ.Services.ForumApi.Features.Commands.CreateLike;

using MediatR;

public record CreateLikeCommand(
    string MessageId,
    string UserId)
    : IRequest;
