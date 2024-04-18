namespace MathZ.Services.EmailApi.Features.Commands.SubscribeMailingById;

using FluentResults;
using MediatR;

public record SubscribeMailingByIdCommand(
    string MailingId,
    string Email)
    : IRequest<Result>;
