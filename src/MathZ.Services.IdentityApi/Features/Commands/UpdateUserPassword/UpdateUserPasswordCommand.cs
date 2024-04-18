namespace MathZ.Services.IdentityApi.Features.Commands.UpdateUserPassword;

using FluentResults;
using MediatR;

public record UpdateUserPasswordCommand(
    string Id,
    string CurrentPassword,
    string NewPassword)
    : IRequest<Result>;
