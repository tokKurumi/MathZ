namespace MathZ.Services.IdentityApi.Features.Commands.DeleteRole;

using FluentResults;
using MediatR;

public record DeleteRoleCommand(
    string Role)
    : IRequest<Result>;
