namespace MathZ.Services.IdentityApi.Features.Commands.DeleteUser;

using FluentResults;
using MediatR;

public record DeleteUserCommand(
    string Id)
    : IRequest<Result>;
