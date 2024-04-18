namespace MathZ.Services.EmailApi.Features.Commands.UpdateMailing;

using FluentResults;
using MathZ.Services.EmailApi.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

public record UpdateMailingCommand(
    string Id,
    JsonPatchDocument<MailingPatch> Patch)
    : IRequest<Result>;
