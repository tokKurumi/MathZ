namespace MathZ.Services.EmailApi.Features.Commands.UnsibscribeMailing;

using FluentResults;
using MediatR;

public record UnsubscribeMailingCommand(
    string MailingId,
    string Email)
    : IRequest<Result>;
