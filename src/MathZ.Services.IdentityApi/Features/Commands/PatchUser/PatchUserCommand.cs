namespace MathZ.Services.IdentityApi.Features.Commands.PatchUser;

using FluentResults;
using MathZ.Services.IdentityApi.Models;
using MathZ.Services.IdentityApi.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

public record PatchUserCommand(
    string Id,
    JsonPatchDocument<UserPatchProfile> Patch)
    : IRequest<Result<ResponseUserDto>>;
