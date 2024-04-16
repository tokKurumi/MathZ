namespace MathZ.Services.IdentityApi.Features.Commands.CreateUser;

using System.ComponentModel;
using MediatR;
using Microsoft.AspNetCore.Identity;

[DisplayName("RegistrationRequest")]
public record CreateUserCommand(
    string Email,
    string UserName,
    string Password,
    string FirstName,
    string LastName)
    : IRequest<IdentityResult>;
