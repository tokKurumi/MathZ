namespace MathZ.Services.IdentityApi.Features.Queries.GetUserByUserName;

using FluentResults;
using MathZ.Services.IdentityApi.Models.Dtos;
using MediatR;

public record GetUserByUserNameQuery(
    string UserName)
    : IRequest<Result<ResponseUserDto>>;
