namespace MathZ.Services.IdentityApi.Features.Commands.GetToken;

using System.ComponentModel;
using FluentResults;
using MathZ.Services.IdentityApi.Models;
using MediatR;

[DisplayName("LoginRequest")]
public record GetTokenCommand(
    string UserName,
    string Password)
    : IRequest<Result<JwtToken>>;
