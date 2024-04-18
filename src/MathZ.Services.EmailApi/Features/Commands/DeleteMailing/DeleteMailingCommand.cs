namespace MathZ.Services.EmailApi.Features.Commands.DeleteMailing;

using FluentResults;
using MediatR;

public record DeleteMailingCommand(
    string Id)
    : IRequest<Result>;
