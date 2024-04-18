namespace MathZ.Services.IdentityApi.Features.Queries.GetToken;

using System.ComponentModel;
using FluentResults;
using MathZ.Services.IdentityApi.Models;
using MediatR;

[DisplayName("LoginRequest")]
public record GetTokenQuery(
    string UserName,
    string Password)
    : IRequest<Result<JwtToken>>;
