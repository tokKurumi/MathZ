namespace MathZ.Services.EmailApi.Features.Commands.SendEmailToTopic;

using FluentResults;
using MediatR;

public record SendEmailToTopicCommand(
    string Id,
    string Subject,
    string Body)
    : IRequest<Result>;
