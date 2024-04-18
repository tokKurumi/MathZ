namespace MathZ.Services.IdentityApi.Features.Queries.GetUserByEmail;

using FluentResults;
using MathZ.Services.IdentityApi.Models.Dtos;
using MediatR;

public record GetUserByEmailQuery(
    string Email)
    : IRequest<Result<ResponseUserDto>>;
