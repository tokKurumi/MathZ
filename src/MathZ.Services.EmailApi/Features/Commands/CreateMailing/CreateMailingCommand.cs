namespace MathZ.Services.EmailApi.Features.Commands.CreateMailing;

using System.ComponentModel;
using MediatR;

[DisplayName("CreateMailingRequest")]
public record CreateMailingCommand(
    string Topic,
    string? Description)
    : IRequest;
