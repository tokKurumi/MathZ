namespace MathZ.Services.IdentityApi.Features.Commands.ChangeUserPassword;

using FluentResults;
using MediatR;

public record ChangeUserPasswordCommand(
    string Id,
    string NewPassword)
    : IRequest<Result>;
