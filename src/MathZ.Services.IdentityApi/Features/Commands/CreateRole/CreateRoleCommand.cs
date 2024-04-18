namespace MathZ.Services.IdentityApi.Features.Commands.CreateRole;

using FluentResults;
using MediatR;

public record CreateRoleCommand(string Role)
    : IRequest<Result>;
