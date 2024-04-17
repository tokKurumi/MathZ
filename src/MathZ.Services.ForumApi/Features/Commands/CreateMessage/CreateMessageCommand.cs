namespace MathZ.Services.ForumApi.Features.Commands.CreateMessage;

using MediatR;

public record CreateMessageCommand(
    string? ParentMessageId,
    string AuthorId,
    string Text)
    : IRequest;
