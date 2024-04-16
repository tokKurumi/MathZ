namespace MathZ.Services.IdentityApi.Features.Commands.CreateUser;

using MathZ.Shared.Models;
using MediatR;

public record CreateUserCommand(
    string Email,
    string UserName,
    string Password,
    string FirstName,
    string LastName)
    : IRequest<User>;
