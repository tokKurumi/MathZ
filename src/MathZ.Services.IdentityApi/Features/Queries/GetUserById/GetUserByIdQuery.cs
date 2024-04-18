namespace MathZ.Services.IdentityApi.Features.Queries.GetUserById;

using FluentResults;
using MathZ.Services.IdentityApi.Models.Dtos;
using MediatR;

public record GetUserByIdQuery(
    string Id)
    : IRequest<Result<ResponseUserDto>>;